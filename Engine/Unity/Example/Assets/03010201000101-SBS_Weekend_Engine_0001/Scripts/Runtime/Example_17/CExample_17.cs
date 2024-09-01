using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

/*
 * 내비게이션이란?
 * - 유니티가 지원하는 AI 관련 기능 중 하나로서 경로를 탐색하는 시스템을 의미한다. (즉, 
 * 내비게이션을 활용하면 특정 대상을 원하는 위치로 이동시키는 결과를 손쉽게 구현하는 것이 
 * 가능하다.)
 * 
 * 유니티는 특정 경로를 탐색하기 위해서 내부적으로 A* 알고리즘을 활용하기 때문에 빠른 시간에
 * 특정 경로를 탐색하는 것이 가능하다는 것을 알 수 있다.
 * 
 * 유니티에서 특정 게임 객체가 내비게이션 기능을 활용해서 경로를 탐색하고 싶다면 NavMeshAgent
 * 컴포넌트를 추가 시켜주면 된다. 단, NavMeshAgent 컴포넌트가 경로를 탐색하기 위해서는 반드시
 * 베이킹 된 맵 정보가 필요하며 해당 정보를 일반적으로 정적으로 생성하지만 필요에 따라 동적으로
 * 내비게이션 맵을 베이킹하는 기능도 지원한다.
 */
/** Example 17 */
public class CExample_17 : CSceneManager
{
	#region 변수
	[SerializeField] private GameObject m_oTarget = null;
	[SerializeField] private GameObject m_oObstacleTarget = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E17;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		var stPos = m_oObstacleTarget.transform.localPosition;

		var stTargetPos01 = new Vector3(250.0f, stPos.y, stPos.z);
		var stTargetPos02 = new Vector3(-250.0f, stPos.y, stPos.z);

		var oObstacleAni01 = m_oObstacleTarget.transform.DOLocalMove(stTargetPos01, 2.0f).SetEase(Ease.Linear);
		var oObstacleAni02 = m_oObstacleTarget.transform.DOLocalMove(stTargetPos02, 2.0f).SetEase(Ease.Linear);

		var oSequence = DOTween.Sequence().SetAutoKill().SetLoops(-1, LoopType.Restart);
		oSequence.Append(oObstacleAni01);
		oSequence.Append(oObstacleAni02);
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		// 마우스 버튼을 눌렀을 경우
		if(Input.GetMouseButtonDown((int)EMouseBtn.LEFT))
		{
			var stRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			bool bIsHit = Physics.Raycast(stRay, out RaycastHit stRaycastHit);

			bool bIsGround = stRaycastHit.collider.CompareTag("E17Ground");
			bool bIsObstacle = stRaycastHit.collider.CompareTag("E17Obstacle");

			// 바닥, 장애물을 클릭했을 경우
			if(bIsHit && (bIsGround || bIsObstacle))
			{
				var oNavMeshAgent = m_oTarget.GetComponent<NavMeshAgent>();
				oNavMeshAgent.SetDestination(stRaycastHit.point);
			}
		}
	}
	#endregion // 함수
}
