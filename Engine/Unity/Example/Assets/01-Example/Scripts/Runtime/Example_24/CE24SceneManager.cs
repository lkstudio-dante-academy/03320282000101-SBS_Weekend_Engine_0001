using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** Example 24 */
public class CE24SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private TMP_Text m_oScoreText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E24;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		m_oScoreText.text = $"Score : {CE23DataStorage.Instance.Score}";
	}

	/** 다시하기 버튼을 눌렀을 경우 */
	public void OnTouchRetryBtn() {
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E23);
	}

	/** 그만두기 버튼을 눌렀을 경우 */
	public void OnTouchLeaveBtn() {
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E22);
	}
	#endregion // 함수
}
