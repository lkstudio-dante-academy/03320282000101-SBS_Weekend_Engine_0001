using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 10 */
public class CE10SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private Button m_oBGSndBtn = null;
	[SerializeField] private Button m_oFXSndsBtn = null;
	[SerializeField] private Button m_oBGSndMuteBtn = null;
	[SerializeField] private Button m_oFXSndsMuteBtn = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E10;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oBGSndBtn.onClick.AddListener(this.OnTouchBGSndBtn);
		m_oFXSndsBtn.onClick.AddListener(this.OnTouchFXSndsBtn);
		m_oBGSndMuteBtn.onClick.AddListener(this.OnTouchBGSndMuteBtn);
		m_oFXSndsMuteBtn.onClick.AddListener(this.OnTouchFXSndsMuteBtn);
	}

	/** 배경음 버튼을 눌렀을 경우 */
	private void OnTouchBGSndBtn() {
		CSndManager.Instance.PlayBGSnd("Example_10/E10BGSnd");
	}

	/** 효과음 버튼을 눌렀을 경우 */
	private void OnTouchFXSndsBtn() {
		CSndManager.Instance.PlayFXSnds("Example_10/E10FXSnds");
	}

	/** 배경음 음소거 버튼을 눌렀을 경우 */
	private void OnTouchBGSndMuteBtn() {
		//CSndManager.Instance.SetIsBGSndMute(true);
	}

	/** 효과음 음소거 버튼을 눌렀을 경우 */
	private void OnTouchFXSndsMuteBtn() {
		//CSndManager.Instance.SetIsFXSndsMute(true);
	}
	#endregion // 함수
}
