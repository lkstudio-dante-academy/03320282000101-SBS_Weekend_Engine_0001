using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/** 적 */
public class CE19Enemy : CComponent {
	#region 변수
	private Animator m_oAnimator = null;
	private NavMeshAgent m_oNavMeshAgent = null;

	[SerializeField] private GameObject m_oPlayer = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_oAnimator = this.GetComponentInChildren<Animator>();

		m_oNavMeshAgent = this.GetComponentInChildren<NavMeshAgent>();
		m_oNavMeshAgent.enabled = false;
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		m_oNavMeshAgent.enabled = true;
	}

	/** 상태를 갱신한다 */
	public void Update() {
		m_oAnimator.SetBool("IsMove", true);
		m_oNavMeshAgent.SetDestination(m_oPlayer.transform.position);
	}
	#endregion // 함수
}
