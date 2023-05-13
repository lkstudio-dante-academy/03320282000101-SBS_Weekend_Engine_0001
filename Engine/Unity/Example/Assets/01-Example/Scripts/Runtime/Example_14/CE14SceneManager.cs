using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 14 */
public class CE14SceneManager : CSceneManager {
	/** 상태 */
	public enum EState {
		NONE = -1,
		PLAY,
		GAME_OVER,
		[HideInInspector] MAX_VAL
	}

	#region 변수
	private int m_nScore = 0;
	private float m_fPlayTime = 30.0f;

	[SerializeField] private Text m_oTimeText = null;
	[SerializeField] private Text m_oScoreText = null;

	[SerializeField] private List<GameObject> m_oTargetList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E14;
	public EState State { get; private set; } = EState.PLAY;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();
		m_fPlayTime = Mathf.Max(m_fPlayTime - Time.deltaTime, 0.0f);

		// 플레이 상태 일 경우
		if(this.State == EState.PLAY) {
			// 플레이 시간이 종료 되었을 경우
			if(m_fPlayTime.ExIsLessEquals(0.0f)) {
				this.State = EState.GAME_OVER;
			}

			// 마우스 버튼을 눌렀을 경우
			if(Input.GetMouseButtonDown((int)EMouseBtn.LEFT)) {
				var oRay = Camera.main.ScreenPointToRay(Input.mousePosition);

				// 클릭 된 대상이 존재 할 경우
				if(Physics.Raycast(oRay, out RaycastHit stRaycastHit) &&
					stRaycastHit.collider.CompareTag("E14Target")) {
					var oTarget = stRaycastHit.collider.GetComponent<CE14Target>();

					// 타겟을 잡았을 경우
					if(oTarget.Catch()) {
						int nExtraScore = (oTarget.TargetKinds <= CE14Target.ETargetKinds._1) ? 10 : -20;
						m_nScore = Mathf.Max(m_nScore + nExtraScore, 0);
					}
				}
			}

			this.UpdateUIsState();
		}
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		m_oTimeText.text = $"{m_fPlayTime:00.00}";
		m_oScoreText.text = $"{m_nScore}";
	}
	#endregion // 함수
}
