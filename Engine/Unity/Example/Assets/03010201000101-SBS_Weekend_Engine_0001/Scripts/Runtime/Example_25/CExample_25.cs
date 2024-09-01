using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/** 씬 관리자 */
public class CExample_25 : CSceneManager
{
	#region 변수
	private CTimer m_oTimer = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E25;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CScheduleManager.Instance.AddComponent(this);
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();
		CScheduleManager.Instance.RemoveComponent(this);
	}

	/** 타이머 추가 버튼을 눌렀을 경우 */
	public void OnTouchAddTimerBtn()
	{
		m_oTimer = CScheduleManager.Instance.AddTimer(5,
			0.0f, 1.0f, this.HandleOnTimerEvent);
	}

	/** 타이머 제거 버튼을 눌렀을 경우 */
	public void OnTouchRemoveTimerBtn()
	{
		CScheduleManager.Instance.RemoveTimer(m_oTimer);
	}

	/** 타이머 이벤트를 처리한다 */
	private void HandleOnTimerEvent(CTimer a_oTimer, int a_nTimes)
	{
		Debug.Log($"HandleOnTimerEvent: {a_nTimes}");
	}
	#endregion // 함수
}
