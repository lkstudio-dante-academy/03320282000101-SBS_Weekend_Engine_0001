using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 타이머 */
public class CTimer : CComponent {
	/** 상태 */
	private enum EState {
		NONE = -1,
		DELAY,
		REPEAT,
		[HideInInspector] MAX_VAL
	}

	/** 매개 변수 */
	public struct STParams {
		public float m_fDelay;
		public float m_fDeltaTime;

		public int m_nRepeatTimes;
		public System.Action<CTimer, int> m_oCallback;
	}

	#region 변수
	private int m_nApplyTimes = 0;
	private float m_fUpdateSkipTime = 0.0f;

	private EState m_eState = EState.DELAY;
	#endregion // 변수

	#region 프로퍼티
	public STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public virtual void Init(STParams a_stParams) {
		this.Params = a_stParams;
	}

	/** 상태를 갱신한다 */
	public override void OnLateUpdate(float a_fDeltaTime) {
		base.OnLateUpdate(a_fDeltaTime);
		m_fUpdateSkipTime += a_fDeltaTime;

		switch(m_eState) {
			case EState.DELAY: this.HandleDelayState(); break;
			case EState.REPEAT: this.HandleRepeatState(); break;
		}
	}

	/** 지연 상태를 처리한다 */
	private void HandleDelayState() {
		// 지연 시간이 지났을 경우
		if(m_fUpdateSkipTime.ExIsGreateEquals(this.Params.m_fDelay)) {
			m_eState = EState.REPEAT;
			m_fUpdateSkipTime -= this.Params.m_fDelay;
		}
	}

	/** 반복 상태를 처리한다 */
	private void HandleRepeatState() {
		// 일정 시간이 지났을 경우
		if(m_fUpdateSkipTime.ExIsGreateEquals(this.Params.m_fDeltaTime)) {
			m_nApplyTimes += 1;
			m_fUpdateSkipTime -= this.Params.m_fDeltaTime;

			this.Params.m_oCallback?.Invoke(this, m_nApplyTimes);
		}

		// 반복 횟수가 지났을 경우
		if(m_nApplyTimes >= this.Params.m_nRepeatTimes) {
			CScheduleManager.Instance.RemoveTimer(this);
		}
	}
	#endregion // 함수

	#region 팩토리 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(int a_nRepeatTimes, 
		float a_fDelay, float a_fDeltaTime, System.Action<CTimer, int> a_oCallback) {
		return new STParams() {
			m_fDelay = a_fDelay,
			m_fDeltaTime = a_fDeltaTime,

			m_nRepeatTimes = a_nRepeatTimes,
			m_oCallback = a_oCallback
		};
	}
	#endregion // 팩토리 함수
}
