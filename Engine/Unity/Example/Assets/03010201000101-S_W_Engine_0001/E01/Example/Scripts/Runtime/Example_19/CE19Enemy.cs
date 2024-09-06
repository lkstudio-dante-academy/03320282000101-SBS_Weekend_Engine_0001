using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/** 적 */
public class CE19Enemy : CComponent
{
	#region 변수
	private bool m_bIsSurvive = false;
	private int m_nHitCount = 3;
	#endregion // 변수

	#region 프로퍼티
	public bool IsSurvive => m_bIsSurvive;

	public Animator AnimatorComp { get; private set; } = null;
	public NavMeshAgent NavMeshAgentComp { get; private set; } = null;

	public CStateMachine<CE19Enemy> StateMachine { get; private set; } = new CStateMachine<CE19Enemy>();

	public System.Action<CE19Enemy> HitCallback { get; private set; } = null;
	public System.Action<CE19Enemy> DestroyCallback { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		this.AnimatorComp = this.GetComponentInChildren<Animator>();

		this.NavMeshAgentComp = this.GetComponentInChildren<NavMeshAgent>();
		this.NavMeshAgentComp.enabled = false;

		this.StateMachine.SetOwner(this);
	}

	/** 초기화 */
	public void Init(System.Action<CE19Enemy> a_oHitCallback,
		System.Action<CE19Enemy> a_oDestroyCallback)
	{
		m_nHitCount = 3;
		m_bIsSurvive = true;

		this.HitCallback = a_oHitCallback;
		this.DestroyCallback = a_oDestroyCallback;
		this.NavMeshAgentComp.enabled = true;

		this.AnimatorComp.SetTrigger("ActiveTrigger");
		this.StateMachine.SetState(CObjPoolManager.Instance.SpawnObj<CE19EnemyIdleState>());
	}

	/** 비활성화 되었을 경우 */
	public void OnDisable()
	{
		this.StateMachine.SetState(null);
		this.NavMeshAgentComp.enabled = false;
	}

	/** 상태를 갱신한다 */
	public void Update()
	{
		this.StateMachine.Update(Time.deltaTime);
	}

	/** 충돌이 발생했을 경우 */
	public void OnTriggerEnter(Collider a_oCollider)
	{
		// 플레이어 일 경우
		if(a_oCollider.CompareTag("E19Player"))
		{
			a_oCollider.GetComponentInParent<CE19Player>().OnHit();
		}
	}

	/** 타격 되었을 경우 */
	public void OnHit()
	{
		// 생존 상태 일 경우
		if(m_bIsSurvive)
		{
			m_nHitCount = Mathf.Max(0, m_nHitCount - 1);
			CE19EnemyState oState = null;

			// 타격 횟수가 모두 차감 되었을 경우
			if(m_nHitCount <= 0)
			{
				oState = CObjPoolManager.Instance.SpawnObj<CE19EnemyDieState>();
				m_bIsSurvive = false;
			}
			else
			{
				oState = CObjPoolManager.Instance.SpawnObj<CE19EnemyHitState>();
			}

			this.HitCallback(this);
			this.StateMachine.SetState(oState);
		}
	}

	/** 타격이 완료 되었을 경우 */
	public void OnCompleteHit()
	{
		// 생존 상태 일 경우
		if(m_bIsSurvive)
		{
			var oState = CObjPoolManager.Instance.SpawnObj<CE19EnemyIdleState>();
			this.StateMachine.SetState(oState);
		}
	}
	#endregion // 함수
}
