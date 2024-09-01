using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 적 상태 */
public abstract class CE19EnemyState : CState<CE19Enemy>
{
	#region 프로퍼티
	public CE19Player Player { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 플레이어를 변경한다 */
	public void SetPlayer(CE19Player a_oPlayer)
	{
		this.Player = a_oPlayer;
	}
	#endregion // 함수
}

/** 적 대기 상태 */
public class CE19EnemyIdleState : CE19EnemyState
{
	#region 함수
	/** 상태를 갱신한다 */
	public override void OnStateUpdate(CE19Enemy a_oOwner, float a_fDeltaTime)
	{
		var oSceneManager = CSceneManager.ActiveSceneManager as CExample_19;

		float fDistance = Vector3.Distance(oSceneManager.Player.transform.position,
			a_oOwner.transform.position);

		// 인식 범위 안에 플레이어가 존재 할 경우
		if(fDistance.ExIsLessEquals(oSceneManager.EnemyViewRange))
		{
			var oState = CObjPoolManager.Instance.SpawnObj<CE19EnemyMoveState>();
			a_oOwner.StateMachine.SetState(oState);
		}
	}
	#endregion // 함수
}

/** 적 이동 상태 */
public class CE19EnemyMoveState : CE19EnemyState
{
	#region 함수
	/** 현재 상태로 변경 되었을 경우 */
	public override void OnStateEnter(CE19Enemy a_oOwner)
	{
		base.OnStateEnter(a_oOwner);
		a_oOwner.AnimatorComp.SetBool("IsMove", true);
	}

	/** 다른 상태로 변경 되었을 경우 */
	public override void OnStateExit(CE19Enemy a_oOwner)
	{
		base.OnStateExit(a_oOwner);

		a_oOwner.AnimatorComp.SetBool("IsMove", false);
		a_oOwner.NavMeshAgentComp.isStopped = true;
	}

	/** 상태를 갱신한다 */
	public override void OnStateUpdate(CE19Enemy a_oOwner, float a_fDeltaTime)
	{
		base.OnStateUpdate(a_oOwner, a_fDeltaTime);
		var oSceneManager = CSceneManager.ActiveSceneManager as CExample_19;

		float fDistance = Vector3.Distance(oSceneManager.Player.transform.position,
			a_oOwner.transform.position);

		a_oOwner.NavMeshAgentComp.isStopped = false;
		a_oOwner.NavMeshAgentComp.SetDestination(oSceneManager.Player.transform.position);

		// 플레이어가 공격 범위 안에 존재 할 경우
		if(fDistance.ExIsLessEquals(oSceneManager.EnemyAttackRange - 50.0f))
		{
			var oState = CObjPoolManager.Instance.SpawnObj<CE19EnemyBattleState>();
			a_oOwner.StateMachine.SetState(oState);
		}
		// 플레이어가 인식 범위를 벗어났을 경우
		else if(fDistance.ExIsGreate(oSceneManager.EnemyViewRange))
		{
			var oState = CObjPoolManager.Instance.SpawnObj<CE19EnemyIdleState>();
			a_oOwner.StateMachine.SetState(oState);
		}
	}
	#endregion // 함수
}

/** 적 전투 상태 */
public class CE19EnemyBattleState : CE19EnemyState
{
	#region 변수
	private float m_fUpdateSkipTime = 0.0f;
	#endregion // 변수

	#region 함수
	/** 다른 상태로 변경 되었을 경우 */
	public override void OnStateExit(CE19Enemy a_oOwner)
	{
		base.OnStateExit(a_oOwner);

		m_fUpdateSkipTime = 0.0f;
		a_oOwner.AnimatorComp.ResetTrigger("AttackTrigger");
	}

	/** 상태를 갱신한다 */
	public override void OnStateUpdate(CE19Enemy a_oOwner, float a_fDeltaTime)
	{
		base.OnStateUpdate(a_oOwner, a_fDeltaTime);
		m_fUpdateSkipTime += a_fDeltaTime;

		// 일정 시간이 지났을 경우
		if(m_fUpdateSkipTime.ExIsGreateEquals(1.0f))
		{
			m_fUpdateSkipTime = 0.0f;
			a_oOwner.AnimatorComp.SetTrigger("AttackTrigger");
		}

		var oSceneManager = CSceneManager.ActiveSceneManager as CExample_19;

		float fDistance = Vector3.Distance(oSceneManager.Player.transform.position,
			a_oOwner.transform.position);

		// 플레이어가 공격 범위를 벗어났을 경우
		if(fDistance.ExIsGreate(oSceneManager.EnemyAttackRange))
		{
			var oState = CObjPoolManager.Instance.SpawnObj<CE19EnemyMoveState>();
			a_oOwner.StateMachine.SetState(oState);
		}
	}
	#endregion // 함수
}

/** 적 타격 상태 */
public class CE19EnemyHitState : CE19EnemyState
{
	#region 함수
	/** 현재 상태로 전환 되었을 경우 */
	public override void OnStateEnter(CE19Enemy a_oOwner)
	{
		base.OnStateEnter(a_oOwner);
		a_oOwner.AnimatorComp.SetTrigger("HitTrigger");
	}

	/** 다른 상태로 전환 되었을 경우 */
	public override void OnStateExit(CE19Enemy a_oOwner)
	{
		base.OnStateExit(a_oOwner);
		a_oOwner.AnimatorComp.ResetTrigger("HitTrigger");
	}
	#endregion // 함수
}

/** 적 제거 상태 */
public class CE19EnemyDieState : CE19EnemyState
{
	#region 변수
	private float m_fUpdateSkipTime = 0.0f;
	#endregion // 변수

	#region 함수
	/** 현재 상태로 전환 되었을 경우 */
	public override void OnStateEnter(CE19Enemy a_oOwner)
	{
		base.OnStateEnter(a_oOwner);

		a_oOwner.AnimatorComp.SetTrigger("DieTrigger");
		a_oOwner.AnimatorComp.ResetTrigger("HitTrigger");
		a_oOwner.AnimatorComp.ResetTrigger("AttackTrigger");
		a_oOwner.AnimatorComp.ResetTrigger("ActiveTrigger");
	}

	/** 다음 상태로 전환 되었을 경우 */
	public override void OnStateExit(CE19Enemy a_oOwner)
	{
		base.OnStateExit(a_oOwner);
		m_fUpdateSkipTime = 0.0f;
	}

	/** 상태를 갱신한다 */
	public override void OnStateUpdate(CE19Enemy a_oOwner, float a_fDeltaTime)
	{
		base.OnStateUpdate(a_oOwner, a_fDeltaTime);
		m_fUpdateSkipTime += a_fDeltaTime;

		// 일정 시간이 지났을 경우
		if(m_fUpdateSkipTime.ExIsGreateEquals(5.0f))
		{
			a_oOwner.DestroyCallback(a_oOwner);

			var oScecneManager = CSceneManager.ActiveSceneManager as CExample_19;
			oScecneManager.GameObjPoolManager.DespawnGameObj("Example_19/E19Enemy", a_oOwner.gameObject);
		}
	}
	#endregion // 함수
}
