using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

/** Example 19 */
public class CE19SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private float m_fEnemyViewRange = 0.0f;
	[SerializeField] private float m_fEnemyAttackRange = 0.0f;

	[SerializeField] private GameObject m_oPlayer = null;
	[SerializeField] private GameObject m_oEnemyRoot = null;
	[SerializeField] private GameObject m_oOriginEnemy = null;

	[SerializeField] private List<GameObject> m_oEnemySpawnPosList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E19;
	public CGameObjPoolManager GameObjPoolManager { get; private set; } = null;

	public float EnemyViewRange => m_fEnemyViewRange;
	public float EnemyAttackRange => m_fEnemyAttackRange;

	public GameObject Player => m_oPlayer;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.GameObjPoolManager = CFactory.CreateObj<CGameObjPoolManager>("GameObjPoolManager", 
			this.gameObject);
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		StartCoroutine(this.TryCreateEnemies());
	}

	/** 적을 생성한다 */
	private IEnumerator TryCreateEnemies() {
		do {
			yield return new WaitForSeconds(2.0f);
			int nIdx = Random.Range(0, m_oEnemySpawnPosList.Count);

			var oEnemy = this.GameObjPoolManager.SpawnGameObj("Enemy", 
				"Example_19/E19Enemy", m_oEnemyRoot);

			var oEnemyComp = oEnemy.GetComponent<CE19Enemy>();

			var stSrcPos = m_oEnemySpawnPosList[nIdx].transform.position;
			stSrcPos.y = 0.0f;

			/*
			 * NavMeshAgent 는 반드시 NavMesh 표면 상에 위치해야지만 컴포넌트를 사용하는 것이
			 * 가능하기 때문에 만약 NavMeshAgent 컴포넌트를 포함하고 있는 특정 객체를 생성하기
			 * 위해서는 해당 객체의 위치가 NavMesh 표면 상에 위치하기 전에 NavMeshAgent 컴포넌트를
			 * 비활성화시켜야한다. (즉, NavMeshAgent 컴포넌트가 활성화되어있는 상태에서 해당 객체의
			 * 위치를 변경하면 내부적으로 오류가 발생한다는 것을 알 수 있다.)
			 * 
			 * 따라서, NavMesh.SamplePosition 메서드를 활용해서 NavMesh 표면 상의 정확한 위치를
			 * 계산 후 해당 위치에 게임 객체를 위치하고 NavMeshAgent 컴포넌트를 다시 활성화하는
			 * 것을 추천한다.
			 */
			NavMesh.SamplePosition(stSrcPos, 
				out NavMeshHit stNavMeshHit, float.MaxValue, NavMesh.AllAreas);

			oEnemyComp.transform.position = stNavMeshHit.position;
			oEnemyComp.Init();
		} while(true);
	}
	#endregion // 함수
}
