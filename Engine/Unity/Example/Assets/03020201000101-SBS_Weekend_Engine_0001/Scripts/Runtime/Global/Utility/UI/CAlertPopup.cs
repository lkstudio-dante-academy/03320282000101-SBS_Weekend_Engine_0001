using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 알림 팝업 */
public partial class CAlertPopup : CPopup
{
	/** 매개 변수 */
	public struct STParams
	{
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

	#region 프로퍼티
	public STParams Params { get; private set; }
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		// 텍스트 설정한다
		m_oMsgText = this.ContentsUIs.transform.Find("MsgText").GetComponent<Text>();
		m_oTitleText = this.ContentsUIs.transform.Find("TitleText").GetComponent<Text>();

		// 버튼을 설정한다 {
		m_oOKBtn = this.ContentsUIs.transform.Find("BottomUIs/OKBtn").GetComponent<Button>();
		m_oCancelBtn = this.ContentsUIs.transform.Find("BottomUIs/CancelBtn").GetComponent<Button>();

		m_oOKBtn.onClick.AddListener(this.OnTouchOKBtn);
		m_oCancelBtn.onClick.AddListener(this.OnTouchCancelBtn);
		// 버튼을 설정한다 }
	}

	/** 초기화 */
	public virtual void Init(STParams a_stParams)
	{
		this.Params = a_stParams;

		m_oMsgText.text = a_stParams.m_oMsg;
		m_oTitleText.text = a_stParams.m_oTitle;
		m_oOKBtn.GetComponentInChildren<Text>().text = a_stParams.m_oOKBtnText;

		m_oCancelBtn.GetComponentInChildren<Text>().text = a_stParams.m_oCanceBtnText;
		m_oCancelBtn.gameObject.SetActive(!string.IsNullOrEmpty(a_stParams.m_oCanceBtnText));
	}

	/** 확인 버튼을 눌렀을 경우 */
	private void OnTouchOKBtn()
	{
		this.Params.m_oCallback?.Invoke(this, true);
		GameObject.Destroy(this.gameObject);
	}

	/** 취소 버튼을 눌렀을 경우 */
	private void OnTouchCancelBtn()
	{
		this.Params.m_oCallback?.Invoke(this, false);
		GameObject.Destroy(this.gameObject);
	}
	#endregion // 함수

	#region 클래스 함수
	/** 매개 변수를 생성한다 */
	public static STParams MakeParams(string a_oTitle,
		string a_oMsg,
		string a_oOKBtnText,
		string a_oCancelBtnText = "",
		System.Action<CAlertPopup, bool> a_oCallback = null)
	{
		return new STParams()
		{
			m_oTitle = a_oTitle,
			m_oMsg = a_oMsg,
			m_oOKBtnText = a_oOKBtnText,
			m_oCanceBtnText = a_oCancelBtnText,
			m_oCallback = a_oCallback
		};
	}
	#endregion // 클래스 함수
}
