using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/** Example 6 */
public class CE06SceneManager : CSceneManager {
	#region 변수
	private const float PLAYER_SPEED = 350.0f;
	private const float DELTA_T_OBSTACLE = 1.5f;
	private List<GameObject> m_oObstacleList = new List<GameObject>();

	private int m_nScore = 0;
	private bool m_bIsGameOver = false;

	[SerializeField] private TMP_Text m_oScoreText = null;

	[SerializeField] private GameObject m_oPlayer = null;
	[SerializeField] private GameObject m_oObstacleRoot = null;
	[SerializeField] private GameObject m_oOriginObstacle = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E06;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CE05DataStorage.Instance.Reset();

		var oDispatcher = m_oPlayer.GetComponent<CTriggerDispatcher>();
		oDispatcher.EnterCallbck = this.HandleOnTriggerEnter;
		oDispatcher.ExitCallbck = this.HandleOnTriggerExit;

		/*
		 * StartCoroutine 메서드는 코루틴 메서드를 실행하는 역할을 수행한다. (즉, 일반적인 메서드는 서브 루틴 방식으로
		 * 동작하기 때문에 코루틴 방식으로 동작하기 위해서는 해당 코루틴이 시작 할 진입 메서드가 필요하다는 것을 알 수
		 * 있다.)
		 */
		StartCoroutine(this.TryCreateObstacle());
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();

		// 게임 종료가 아닐 경우
		if(!m_bIsGameOver) {
			for(int i = 0; i < m_oObstacleList.Count; ++i) {
				var stPos = m_oObstacleList[i].transform.localPosition;
				stPos.x -= PLAYER_SPEED * Time.deltaTime;

				m_oObstacleList[i].transform.localPosition = stPos;
			}

			// 스페이스 키를 눌렀을 경우
			if(Input.GetKeyDown(KeyCode.Space)) {
				var oRigidbody = m_oPlayer.GetComponent<Rigidbody>();
				oRigidbody.velocity = Vector3.zero;

				oRigidbody.AddForce(Vector3.up * 600.0f, ForceMode.VelocityChange);
			}
		}
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		m_oScoreText.text = $"{m_nScore}";
	}

	/** 충돌 발생을 처리한다 */
	private void HandleOnTriggerEnter(CTriggerDispatcher a_oSender, Collider a_oCollider) {
		// 장애물 일 경우
		if(!m_bIsGameOver && a_oCollider.CompareTag("E05Obstacle")) {
			m_bIsGameOver = true;
			CE05DataStorage.Instance.Score = m_nScore;

			m_oPlayer.GetComponent<Rigidbody>().useGravity = false;
			m_oPlayer.GetComponent<Rigidbody>().isKinematic = true;

			CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E07, LoadSceneMode.Additive);
		}
	}

	/** 충돌 종료를 처리한다 */
	private void HandleOnTriggerExit(CTriggerDispatcher a_oSender, Collider a_oCollider) {
		// 안전 영역 일 경우
		if(a_oCollider.CompareTag("E05Safe")) {
			m_nScore += 1;
			this.UpdateUIsState();
		}
	}

	/** 장애물 생성을 시도한다 */
	private IEnumerator TryCreateObstacle() {
		do {
			float fMaxHeight = 720.0f * 0.6f;

			float fTopHeight = fMaxHeight * Random.Range(0.1f, 0.9f);
			float fSafeHeight = 720.0f * 0.4f;
			float fBottomHeight = fMaxHeight - fTopHeight;

			var oObstacle = Instantiate(m_oOriginObstacle, Vector3.zero, Quaternion.identity);
			oObstacle.transform.SetParent(m_oObstacleRoot.transform, false);
			oObstacle.transform.localPosition = new Vector3(1000.0f, 0.0f, 0.0f);

			var oTopObjs = oObstacle.transform.GetChild(0).gameObject;
			oTopObjs.transform.localPosition = new Vector3(0.0f, 360.0f - (fTopHeight / 2.0f), 0.0f);

			var oSafeObjs = oObstacle.transform.GetChild(1).gameObject;
			oSafeObjs.transform.localPosition = new Vector3(0.0f, (360.0f - fTopHeight) - (fSafeHeight / 2.0f), 0.0f);

			var oBottomObjs = oObstacle.transform.GetChild(2).gameObject;
			oBottomObjs.transform.localPosition = new Vector3(0.0f, -360.0f + (fBottomHeight / 2.0f), 0.0f);

			var stTopObjScale = oTopObjs.transform.localScale;
			stTopObjScale.y = fTopHeight;

			var stBottomObjsScale = oBottomObjs.transform.localScale;
			stBottomObjsScale.y = fBottomHeight;

			oTopObjs.transform.localScale = stTopObjScale;
			oBottomObjs.transform.localScale = stBottomObjsScale;

			m_oObstacleList.Add(oObstacle);

			/*
			 * yield return 키워드는 코루틴에서 해당 함수의 동작을 잠깐 정지시키는 역할을 수행한다. (즉, 코루틴은
			 * 서브 루틴과 달리 필요에 따라 루틴이 종료 된 위치부터 다시 동작 할 여지가 있기 때문에 일반적인 루틴에서
			 * 사용되는 return 키워드는 사용하는 것이 불가능하다.
			 * 
			 * 즉, yield return 키워드는 코루틴에서 사용되는 return 키워드를 의미한다. 또한, 유니티에서 코루틴은 항상
			 * 반환 형으로 IEnumerator 인터페이스를 따르고 있는 객체를 요구한다는 특징이 있다.
			 */
			yield return new WaitForSeconds(DELTA_T_OBSTACLE);
		} while(!m_bIsGameOver);
	}
	#endregion // 함수
}
