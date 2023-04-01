using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 알림 팝업 */
public partial class CAlertPopup : CPopup {
	/** 매개 변수 */
	public struct STParams {
		public string m_oMsg;
		public string m_oTitle;

		public string m_oOKBtnText;
		public string m_oCanceBtnText;

		public System.Action<CAlertPopup, bool> m_oCallback;
	}

	#region 변수
	private Text m_oMsgText = null;
	private Text m_oTitleText = null;

	private Button m_oOKBtn = null;
	private Button m_oCancelBtn = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 텍스트 설정한다
		m_oMsgText = this.ContentsUIs.transform.Find("MsgText").GetComponent<Text>();
		m_oTitleText = this.ContentsUIs.transform.Find("TitleText").GetComponent<Text>();

		// 버튼을 설정한다
		m_oOKBtn = this.ContentsUIs.transform.Find("BottomUIs/OKBtn").GetComponent<Button>();
		m_oCancelBtn = this.ContentsUIs.transform.Find("BottomUIs/CancelBtn").GetComponent<Button>();
	}
	#endregion // 함수
}
