using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** 텍스트 지역화 처리자 */
public class CTextLocalizer : CComponent {
	#region 변수
	private Text m_oText = null;
	private TMP_Text m_oTMPText = null;

	[SerializeField] private string m_oKey = string.Empty;
	[SerializeField] private SystemLanguage m_eLanguage = SystemLanguage.Unknown;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oText = this.GetComponent<Text>();
		m_oTMPText = this.GetComponent<TMP_Text>();
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
		this.SetupText();
	}

	/** 텍스트를 설정한다 */
	private void SetupText() {
		// 텍스트가 존재 할 경우
		if(m_oText != null) {
			m_oText.text = CStrTable.Instance.GetStr(m_oKey);
		}

		// TMP 텍스트가 존재 할 경우
		if(m_oTMPText != null) {
			m_oTMPText.text = CStrTable.Instance.GetStr(m_oKey);
		}
	}
	#endregion // 함수
}
