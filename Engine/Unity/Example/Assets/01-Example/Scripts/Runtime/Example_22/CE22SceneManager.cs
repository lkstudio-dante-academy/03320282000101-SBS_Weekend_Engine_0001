using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 22 */
public class CE22SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E22;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CGameInfoStorage.Instance.LoadGameInfo();
	}

	/** 플레이 버튼을 눌렀을 경우 */
	public void OnTouchPlayBtn() {
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E23);
	}

	/** 설정 버튼을 눌렀을 경우 */
	public void OnTouchSettingsBtn() {
		string oName = "SettingsPopup";
		string oPath = "Example_22/E22SettingsPopup";

		CFunc.ShowPopup(oName, this.PopupUIs, oPath, (a_oSender) => {
			// Do Something
		});
	}
	#endregion // 함수
}
