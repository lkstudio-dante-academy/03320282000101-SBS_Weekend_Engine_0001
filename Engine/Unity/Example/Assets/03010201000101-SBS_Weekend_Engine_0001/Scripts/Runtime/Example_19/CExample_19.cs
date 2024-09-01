using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

/** Example 19 */
public class CExample_19 : CSceneManager
{
	/** 상태 */
	public enum EState
	{
		NONE = -1,
		PLAY,
		GAME_OVER,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	private int m_nScore = 0;
	private int m_nMaxPlayerHP = 30;
	private List<GameObject> m_oEnemyList = new List<GameObject>();

	[SerializeField] private float m_fEnemyViewRange = 0.0f;
	[SerializeField] private float m_fEnemyAttackRange = 0.0f;

	[Header("=====> UIs <=====")]
	[SerializeField] private Text m_oScoreText = null;
	[SerializeField] private Image m_oPlayerHPImg = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oPlayer = null;
	[SerializeField] private GameObject m_oEnemyRoot = null;
	[SerializeField] private GameObject m_oOriginEnemy = null;

	[SerializeField] private List<GameObject> m_oEnemySpawnPosList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E19;

	public EState State { get; private set; } = EState.PLAY;
	public CGameObjPoolManager GameObjPoolManager { get; private set; } = null;

	public float EnemyViewRange => m_fEnemyViewRange;
	public float EnemyAttackRange => m_fEnemyAttackRange;

	public GameObject Player => m_oPlayer;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		CE19DataStorage.Instance.Reset();

		m_oPlayer.GetComponent<CE19Player>().Init(m_nMaxPlayerHP, this.OnHitPlayer);

		this.GameObjPoolManager = CFactory.CreateObj<CGameObjPoolManager>("GameObjPoolManager",
			this.gameObject);
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		StartCoroutine(this.TryCreateEnemies());
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState()
	{
		// 텍스트를 갱신한다
		m_oScoreText.text = $"{m_nScore}";

		// 이미지를 갱신한다
		m_oPlayerHPImg.fillAmount = m_oPlayer.GetComponent<CE19Player>().HP / (float)m_nMaxPlayerHP;
	}

	/** 플레이어가 타격 되었을 경우 */
	private void OnHitPlayer(CE19Player a_oPlayer)
	{
		// 체력이 없을 경우
		if(a_oPlayer.HP <= 0 && this.State == EState.PLAY)
		{
			this.State = EState.GAME_OVER;
			CE19DataStorage.Instance.Score = m_nScore;

			CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E20,
				UnityEngine.SceneManagement.LoadSceneMode.Additive);
		}

		this.UpdateUIsState();
	}

	/** 적이 타격 되었을 경우 */
	private void OnHitEnemy(CE19Enemy a_oEnemy)
	{
		// 적이 제거 되었을 경우
		if(!a_oEnemy.IsSurvive)
		{
			m_nScore += 10;
		}

		this.UpdateUIsState();
	}

	/** 적이 제거 되었을 경우 */
	private void OnDestroyEnemy(CE19Enemy a_oEnemy)
	{
		m_oEnemyList.Remove(a_oEnemy.gameObject);
	}

	/** 적 존재 여부를 검사한다 */
	private bool IsExistsEnemyAtPos(Vector3 a_stPos)
	{
		for(int i = 0; i < m_oEnemyList.Count; ++i)
		{
			// 특정 거리보다 작을 경우
			if(Vector3.Distance(a_stPos, m_oEnemyList[i].transform.position) <= 30.0f)
			{
				return true;
			}
		}

		return false;
	}

	/** 적을 생성한다 */
	private IEnumerator TryCreateEnemies()
	{
		do
		{
			yield return new WaitForSeconds(2.0f);

			int nIdx = Random.Range(0, m_oEnemySpawnPosList.Count);
			var oSpawnPos = m_oEnemySpawnPosList[nIdx];

			// 적이 생성 위치에 존재 할 경우
			if(this.IsExistsEnemyAtPos(oSpawnPos.transform.position))
			{
				continue;
			}

			var oEnemy = this.GameObjPoolManager.SpawnGameObj("Enemy",
				"Example_19/E19Enemy", m_oEnemyRoot);

			m_oEnemyList.Add(oEnemy);
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
			oEnemyComp.Init(this.OnHitEnemy, this.OnDestroyEnemy);
		} while(this.State != EState.GAME_OVER);
	}
	#endregion // 함수
}
