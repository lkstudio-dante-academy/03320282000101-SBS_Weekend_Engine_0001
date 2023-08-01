using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

/** Example 23 */
public class CE23SceneManager : CSceneManager {
	/** 상태 */
	private enum EState {
		NONE = -1,
		SHOOT,
		AIMING,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	private bool m_bIsTouch = false;
	private bool m_bIsDirtyUpdateState = true;

	private int m_nScore = 0;
	private EState m_eState = EState.AIMING;
	private Vector3 m_stShootPos = Vector3.zero;

	private Tween m_oDropAni = null;
	private List<Vector3> m_oLinePosList = new List<Vector3>();
	private List<CE23Obstacle> m_oObstacleList = new List<CE23Obstacle>();

	[Header("=====> UIs <=====")]
	[SerializeField] private Text m_oScoreText = null;
	[SerializeField] private Button m_oDropBtn = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oBall = null;
	[SerializeField] private GameObject m_oObstacleRoot = null;
	[SerializeField] private GameObject m_oOriginObstacle = null;

	[SerializeField] private LineRenderer m_oAimingLine = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E23;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.SetupObstacles();

		CE23DataStorage.Instance.Reset();
		m_oDropBtn.gameObject.SetActive(false);

		// 볼을 설정한다
		m_oBall.GetComponent<CE23Ball>().Init(this.HandleOnHit);

		// 전달자를 설정한다
		var oDispatcher = this.UIs.GetComponentInChildren<CTouchDispatcher>();
		oDispatcher.BeginCallback = this.HandleTouchBegin;
		oDispatcher.MoveCallback = this.HandleTouchMove;
		oDispatcher.EndCallback = this.HandleTouchEnd;
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();
		m_oDropAni?.Kill();
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();

		// 상태 갱신이 필요 할 경우
		if(m_bIsDirtyUpdateState) {
			for(int i = 0; i < m_oObstacleList.Count; i++) {
				m_oObstacleList[i].UpdateState();
			}

			this.UpdateUIsState();
			m_bIsDirtyUpdateState = false;
		}
	}

	/** 낙하 버튼을 눌렀을 경우 */
	public void OnTouchDropBtn() {
		var oDropAni = DOTween.Sequence().SetAutoKill();
		oDropAni.Append(m_oBall.transform.DOLocalMove(m_stShootPos, 0.5f));
		oDropAni.AppendCallback(this.OnCompleteDrop);

		m_oDropAni?.Kill();
		m_oDropAni = oDropAni;
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		m_oScoreText.text = $"{m_nScore}";
		m_oDropBtn.gameObject.SetActive(m_eState == EState.SHOOT);
	}

	/** 장애물을 설정한다 */
	private void SetupObstacles() {
		var oIdxList = new List<int>();

		for(int i = 0; i < 100; ++i) {
			oIdxList.Add(i);
		}

		for(int i = 0; i < oIdxList.Count; ++i) {
			int nIdx = Random.Range(0, oIdxList.Count);
			oIdxList.ExSwap(nIdx, i);
		}

		float fWidth = KDefine.G_DESIGN_WIDTH / 10.0f;
		float fHeight = KDefine.G_DESIGN_HEIGHT / 13.0f;

		float fTotalWidth = fWidth * 10.0f;
		float fTotalHeight = fHeight * 10.0f;

		Vector3 stPivotPos = new Vector3(fTotalWidth / -2.0f,
			fTotalHeight / 2.0f, 0.0f);

		for(int i = 0; i < 15; ++i) {
			int nRow = oIdxList[i] / 10;
			int nCol = oIdxList[i] % 10;

			var oObstacle = CFactory.CreateCloneObj<CE23Obstacle>("Obstacle",
				m_oObstacleRoot, m_oOriginObstacle);

			oObstacle.transform.localPosition = stPivotPos +
				new Vector3(fWidth * nCol, fHeight * -nRow, 0.0f) +
				new Vector3(fWidth / 2.0f, fHeight / -2.0f, 0.0f);

			oObstacle.Init(Random.Range(1, 6));
			m_oObstacleList.Add(oObstacle);
		}
	}

	/** 장애물이 제거 되었을 경우 */
	private void OnDestroyObstacle(CE23Obstacle a_oSender) {
		m_nScore += 10;
		m_oObstacleList.Remove(a_oSender);

		m_bIsDirtyUpdateState = true;
	}

	/** 충돌을 처리한다 */
	private void HandleOnHit(CE23Ball a_oSender, CE23Obstacle a_oObstacle) {
		a_oObstacle.OnHit(this.OnDestroyObstacle);
		m_bIsDirtyUpdateState = true;
	}

	/** 터치 시작을 처리한다 */
	private void HandleTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {
		// 조준 상태 일 경우
		if(m_eState == EState.AIMING) {
			m_bIsTouch = true;
			m_oAimingLine.gameObject.SetActive(true);

			this.HandleTouchMove(a_oSender, a_oEventData);
		}
	}

	/** 터치 이동을 처리한다 */
	private void HandleTouchMove(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {
		m_oLinePosList.Clear();
		var stTouchPos = a_oEventData.ExGetWorldPos(KDefine.G_DESIGN_SIZE);

		// 터치 중 일 경우
		if(m_bIsTouch && stTouchPos.y.ExIsGreate(m_oBall.transform.position.y)) {
			m_oLinePosList.Add(m_oBall.transform.localPosition);

			var stPos = m_oBall.transform.position;
			var stDirection = stTouchPos - m_oBall.transform.position;

			/*
			 * LayerMask 를 활용하면 레이어 번호를 가져오는 것이 가능하다.
			 * (즉, 유니티는 내부적으로 게임 객체를 분류하기 위해서 레이어라는
			 * 번호를 사용하면 해당 번호는 0 ~ 31 까지의 범위를 지니고 있다.)
			 * 
			 * 따라서, 유니티는 특정 객체의 그룹을 가능하면 빠르게 판단하기
			 * 32 비트 정수와 비트 연산자를 활용한다는 것을 알 수 있다.
			 * 
			 * Tag vs Layer
			 * - 두 기능 모두 물체를 식별하는데 활용하는 것이 가능하다. Tag 는
			 * 문자열을 사용하기 때문에 필요한 만큼 Tag 를 지정하는 것이 가능하지만
			 * 문자열을 비교하기 때문에 Layer 보다 상대적으로 성능이 떨어지는 단점이
			 * 존재한다.
			 * 
			 * 반면, Layer 는 컴퓨터가 가장 빠르게 처리 할 수 있는 32 비트 정수로 
			 * 되어있기 때문에 특정 물체를 식별하는 연산을 빠르게 수행하는 것이 가능하다.
			 */
			int nLayerMask = 1 << LayerMask.NameToLayer("E23Bounds");
			nLayerMask |= 1 << LayerMask.NameToLayer("E23Obstacle");

			/*
			 * LayerMask.GetMask 메서드를 활용하면 여러 레이어를 검사 할 수 있는
			 * 비트 마스크를 계산하는 것이 가능하다.
			 */
#if DISABLE_THIS
			int nLayerMask = LayerMask.GetMask("E23Bounds", "E23Obstacle");
#endif // DISABLE_THIS

			// 충돌체가 존재 할 경우
			if(Physics.Raycast(stPos,
				stDirection.normalized, out RaycastHit stRaycastHit, float.MaxValue, nLayerMask)) {
				m_oLinePosList.Add(stRaycastHit.point);

				var stNormal = stRaycastHit.normal;
				var stReflect = Vector3.Reflect(stDirection.normalized, stNormal);

				m_oLinePosList.Add(stRaycastHit.point + (stReflect * 150.0f));
			}
		}

		m_oAimingLine.positionCount = m_oLinePosList.Count;
		m_oAimingLine.SetPositions(m_oLinePosList.ToArray());
	}

	/** 터치 종료를 처리한다 */
	private void HandleTouchEnd(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {
		var stTouchPos = a_oEventData.ExGetWorldPos(KDefine.G_DESIGN_SIZE);

		// 볼을 발사 할 수 있을 경우
		if(m_bIsTouch && stTouchPos.y.ExIsGreate(m_oBall.transform.position.y)) {
			var stDirection = stTouchPos - m_oBall.transform.position;
			m_stShootPos = m_oBall.transform.localPosition;

			this.SetState(EState.SHOOT);
			m_oBall.GetComponent<CE23Ball>().Shoot(stDirection, this.OnCompleteMove);
		}

		m_bIsTouch = false;
		m_oAimingLine.gameObject.SetActive(false);
	}

	/** 볼 낙하가 완료 되었을 경우 */
	private void OnCompleteDrop() {
		this.SetState(EState.AIMING);
		m_oBall.GetComponent<CE23Ball>().SetIsShoot(false);
	}

	/** 볼 이동이 완료 되었을 경우 */
	private void OnCompleteMove(CE23Ball a_oSender) {
		this.SetState(EState.AIMING);

		// 모든 장애물을 제거했을 경우
		if(m_oObstacleList.Count <= 0) {
			CE23DataStorage.Instance.Score = m_nScore;

			CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E24,
				UnityEngine.SceneManagement.LoadSceneMode.Additive);
		}
	}
	#endregion // 함수

	#region 접근 함수
	/** 상태를 변경한다 */
	private void SetState(EState a_eState) {
		m_eState = a_eState;
		m_bIsDirtyUpdateState = true;
	}
	#endregion // 접근 함수
}
