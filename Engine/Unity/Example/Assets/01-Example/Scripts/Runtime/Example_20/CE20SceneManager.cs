using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 20 */
public class CE20SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E20;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		// Do Something
	}

	/** 다시하기 버튼을 눌렀을 경우 */
	public void OnTouchRetryBtn() {
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E19);
	}

	/** 그만두기 버튼을 눌렀을 경우 */
	public void OnTouchLeaveBtn() {
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E18);
	}
	#endregion // 함수
}
