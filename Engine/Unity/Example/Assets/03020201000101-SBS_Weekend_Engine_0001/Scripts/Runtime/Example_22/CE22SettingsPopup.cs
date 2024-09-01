using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 설정 팝업 */
public class CE22SettingsPopup : CPopup
{
	#region 변수
	private bool m_bIsDirtySaveInfo = false;

	[SerializeField] private Button m_oBGSndBtn = null;
	[SerializeField] private Button m_oFXSndsBtn = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.UpdateUIsState();
	}

	/** 상태를 갱신한다 */
	public void Update()
	{
		// 정보 저장이 필요 할 경우
		if(m_bIsDirtySaveInfo)
		{
			this.UpdateUIsState();
			CGameInfoStorage.Instance.SaveGameInfo();

			m_bIsDirtySaveInfo = false;
		}
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState()
	{
		bool bIsMuteBGSnd = CGameInfoStorage.Instance.GameInfo.IsMuteBGSnd;
		bool bIsMuteFXSnds = CGameInfoStorage.Instance.GameInfo.IsMuteFXSnds;

		m_oBGSndBtn.image.color = bIsMuteBGSnd ? Color.gray : Color.white;
		m_oFXSndsBtn.image.color = bIsMuteFXSnds ? Color.gray : Color.white;
	}

	/** 닫기 버튼을 눌렀을 경우 */
	public void OnTouchCloseBtn()
	{
		this.Close();
	}

	/** 배경음 버튼을 눌렀을 경우 */
	public void OnTouchBGSndBtn()
	{
		bool bIsMute = CGameInfoStorage.Instance.GameInfo.IsMuteBGSnd;
		CGameInfoStorage.Instance.GameInfo.IsMuteBGSnd = !bIsMute;

		m_bIsDirtySaveInfo = true;
		CSndManager.Instance.SetIsMuteBGSnd(!bIsMute);
	}

	/** 효과음 버튼을 눌렀을 경우 */
	public void OnTouchFXSndsBtn()
	{
		bool bIsMute = CGameInfoStorage.Instance.GameInfo.IsMuteFXSnds;
		CGameInfoStorage.Instance.GameInfo.IsMuteFXSnds = !bIsMute;

		m_bIsDirtySaveInfo = true;
		CSndManager.Instance.SetIsMuteFXSnds(!bIsMute);
	}

	/** 평가하기 버튼을 눌렀을 경우 */
	public void OnTouchReviewBtn()
	{
		/*
		 * 모바일 플랫폼 스토어 URL
		 * - iOS : https://itunes.apple.com/app/id[번들식별자]
		 * - 안드로이드 : https://play.google.com/store/apps/details?id=[패키지이름]
		 */
		Application.OpenURL("http://www.google.com");
	}

	/** 메일 보내기 버튼을 눌렀을 경우 */
	public void OnTouchSupportsBtn()
	{
		Application.OpenURL("mailto:are2341@nate.com");
	}
	#endregion // 함수
}
