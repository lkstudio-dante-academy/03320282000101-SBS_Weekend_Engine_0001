using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 설정 팝업 */
public class CE10SettingsPopup : CPopup {
	#region 변수
	[SerializeField] private Button m_oBGSndBtn = null;
	[SerializeField] private Button m_oFXSndsBtn = null;
	[SerializeField] private Button m_oVibrateBtn = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oBGSndBtn.onClick.AddListener(this.OnTouchBGSndBtn);
		m_oFXSndsBtn.onClick.AddListener(this.OnTouchFXSndsBtn);
		m_oVibrateBtn.onClick.AddListener(this.OnTouchVibrateBtn);
	}

	/** 배경음 버튼을 눌렀을 경우 */
	private void OnTouchBGSndBtn() {

	}

	/** 효과음 버튼을 눌렀을 경우 */
	private void OnTouchFXSndsBtn() {

	}

	/** 진동 버튼을 눌렀을 경우 */
	private void OnTouchVibrateBtn() {

	}
	#endregion // 함수
}
