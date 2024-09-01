using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/** 스케줄 관리자 */
public class CScheduleManager : CSingleton<CScheduleManager>
{
	#region 변수
	private List<CComponent> m_oComponentList = new List<CComponent>();
	private List<CComponent> m_oAddComponentList = new List<CComponent>();
	private List<CComponent> m_oRemoveComponentList = new List<CComponent>();
	#endregion // 변수

	#region 프로퍼티
	public float DeltaTime { get; private set; } = 0.0f;
	public float FixedDeltaTime { get; private set; } = 0.0f;
	public float UnscaleDeltaTime { get; private set; } = 0.0f;
	#endregion // 프로퍼티

	#region 함수
	/** 상태를 갱신한다 */
	public void Update()
	{
		this.DeltaTime = Time.deltaTime;
		this.FixedDeltaTime = Time.fixedDeltaTime;
		this.UnscaleDeltaTime = Time.unscaledDeltaTime;

		this.UpdateComponentsState();

		for(int i = 0; i < m_oComponentList.Count; ++i)
		{
			bool bIsInvalid01 = m_oComponentList[i] == null;
			bool bIsInvalid02 = m_oComponentList[i]?.gameObject == null;

			// 컴포넌트가 제거가 필요 할 경우
			if(bIsInvalid01 || bIsInvalid02)
			{
				this.RemoveComponent(m_oComponentList[i]);
			}
			// 컴포넌트 상태 갱신이 가능 할 경우
			else if(this.IsEnableUpdateState(m_oComponentList[i]))
			{
				m_oComponentList[i].OnUpdate(this.DeltaTime);
			}
		}
	}

	/** 상태를 갱신한다 */
	public void LateUpdate()
	{
		for(int i = 0; i < m_oComponentList.Count; ++i)
		{
			m_oComponentList[i].OnLateUpdate(this.DeltaTime);
		}
	}

	/*
	 * FixedUpdate 메서드는 고정 시간 간격으로 특정 게임 객체의 상태를
	 * 갱신하는 용도로 활용된다. (즉, Update 및 LateUpdate 메서드는 가변
	 * 시간 간격으로 동작하기 때문에 메서드가 호출 되었을때 현재 프로그램의
	 * 진행 상태에 따라 프레임 간에 시간 간격이 가변적으로 변하는 반면
	 * FixedUpdate 항상 일정한 간격으로 호출 되기 때문에 가변 시간 간격을
	 * 사용하는 방법보다 좀 더 안전하게 프로그램을 제작하는 것이 가능하다.)
	 */
	/** 상태를 갱신한다 */
	public void FixedUpdate()
	{
		for(int i = 0; i < m_oComponentList.Count; ++i)
		{
			m_oComponentList[i].OnFixedUpdate(this.DeltaTime);
		}
	}

	/** 타이머를 추가한다 */
	public CTimer AddTimer(int a_nRepeatTimes,
		float a_fDelay, float a_fDeltaTime, System.Action<CTimer, int> a_oCallback)
	{
		var stParams = CTimer.MakeParams(a_nRepeatTimes,
			a_fDelay, a_fDeltaTime, a_oCallback);

		var oTimer = CFactory.CreateObj<CTimer>("Timer", this.gameObject);
		oTimer.Init(stParams);

		this.AddComponent(oTimer);
		return oTimer;
	}

	/** 타이머를 제거한다 */
	public void RemoveTimer(CTimer a_oTimer)
	{
		this.RemoveComponent(a_oTimer);
	}

	/** 컴포넌트를 추가한다 */
	public void AddComponent(CComponent a_oComponent)
	{
		// 추가 대기 컴포넌트가 아닐 경우
		if(!a_oComponent.IsDestroy && !m_oAddComponentList.Contains(a_oComponent))
		{
			m_oAddComponentList.Add(a_oComponent);
		}
	}

	/** 컴포넌트를 제거한다 */
	public void RemoveComponent(CComponent a_oComponent)
	{
		// 제거 대기 컴포넌트가 아닐 경우
		if(!a_oComponent.IsDestroy && !m_oRemoveComponentList.Contains(a_oComponent))
		{
			m_oRemoveComponentList.Add(a_oComponent);
		}
	}

	/** 컴포넌트 상태를 갱신한다 */
	private void UpdateComponentsState()
	{
		for(int i = 0; i < m_oAddComponentList.Count; ++i)
		{
			// 추가 된 컴포넌트가 아닐 경우
			if(!m_oComponentList.Contains(m_oAddComponentList[i]))
			{
				m_oComponentList.Add(m_oAddComponentList[i]);
			}
		}

		for(int i = 0; i < m_oRemoveComponentList.Count; ++i)
		{
			bool bIsValid = m_oRemoveComponentList[i]?.gameObject != null;
			m_oComponentList.Remove(m_oRemoveComponentList[i]);

			// 타이머 일 경우
			if(bIsValid && m_oRemoveComponentList[i] is CTimer)
			{
				Destroy(m_oRemoveComponentList[i].gameObject);
			}
		}

		m_oAddComponentList.Clear();
		m_oRemoveComponentList.Clear();
	}
	#endregion // 함수

	#region 접근 함수
	/** 컴포넌트 상태 갱신 가능 여부를 검사한다 */
	private bool IsEnableUpdateState(CComponent a_oComponent)
	{
		/*
		 * activeSelf vs activeInHierarchy
		 * - 두 프로퍼티 모두 게임 객체의 활성 여부를 검사하는 역할을 수행한다.
		 * activeSelf 프로퍼티는 자신의 활성 여부만을 검사하는 반면 activeInHierarchy
		 * 프로퍼티는 자신을 포함한 부모 게임 객체의 활성 여부도 검사하는 차이점이
		 * 존재한다. (즉, 부모 객체가 비활성화 되면 자식 객체도 비활성화되는 특징이
		 * 존재하기 때문에 씬 상에서 특정 게임 객체의 활성 여부를 올바르게 검사하기
		 * 위해서는 activeInHierarchy 프로퍼티를 활용해야한다.)
		 */
		return a_oComponent.enabled && !a_oComponent.IsDestroy && a_oComponent.gameObject.activeInHierarchy;
	}
	#endregion // 접근 함수
}
