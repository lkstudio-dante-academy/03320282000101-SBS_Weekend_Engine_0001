using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 타겟 */
public class CE14Target : CComponent
{
	/** 타겟 종류 */
	public enum ETargetKinds
	{
		NONE = -1,
		_1,
		_2,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	private bool m_bIsEnableCatch = false;
	private Animator m_oAnimator = null;
	#endregion // 변수

	#region 프로퍼티
	public ETargetKinds TargetKinds = ETargetKinds.NONE;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		m_oAnimator = this.GetComponent<Animator>();
		m_oAnimator.SetBool("IsActive", false);

		var oDispatcher = this.GetComponent<CEventDispatcher>();
		oDispatcher.EventCallback = this.HandleOnEvent;
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		StartCoroutine(this.TryOpenState());
	}

	/** 캐치 상태로 전환한다 */
	public bool Catch()
	{
		// 액티브 상태 일 경우
		if(m_bIsEnableCatch && m_oAnimator.GetBool("IsActive"))
		{
			m_bIsEnableCatch = false;

			m_oAnimator.SetTrigger("Catch");
			m_oAnimator.ResetTrigger("Open");

			return true;
		}

		return false;
	}

	/** 이벤트를 처리한다 */
	private void HandleOnEvent(CEventDispatcher a_oSender, string a_oEvent)
	{
		m_oAnimator.ResetTrigger("Open");
		m_oAnimator.ResetTrigger("Catch");

		m_oAnimator.SetBool("IsActive", false);
		StartCoroutine(this.TryOpenState());
	}

	/** 오픈 상태를 시도한다 */
	private IEnumerator TryOpenState()
	{
		yield return new WaitForSeconds(Random.Range(0.1f, 6.0f));
		var oSceneManager = CSceneManager.GetSceneManager<CE01Example_14>(KDefine.G_SCENE_N_E14);

		// 플레이 상태 일 경우
		if(oSceneManager.State == CE01Example_14.EState.PLAY)
		{
			this.TargetKinds = (ETargetKinds)Random.Range(0, 2);

			string oControllerPath = (this.TargetKinds <= ETargetKinds._1) ? "Example_14/E14TargetController_01" :
				"Example_14/E14TargetController_02";

			m_oAnimator.runtimeAnimatorController =
				CResManager.Instance.GetRes<RuntimeAnimatorController>(oControllerPath);

			m_bIsEnableCatch = true;

			m_oAnimator.SetBool("IsActive", true);
			m_oAnimator.SetTrigger("Open");
		}
	}
	#endregion // 함수
}
