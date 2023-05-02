using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 사운드 관리자 */
public class CSndManager : CSingleton<CSndManager> {
	#region 변수
	private bool m_bIsMuteBGSnd = false;
	private bool m_bIsMuteFXSnds = false;

	private float m_fBGSndVolume = 1.0f;
	private float m_fFXSndsVolume = 1.0f;

	private CSnd m_oBGSnd = null;
	private Dictionary<string, List<CSnd>> m_oFXSndsDictContainer = new Dictionary<string, List<CSnd>>();
	#endregion // 변수

	#region 프로퍼티
	public bool IsMuteBGSnd => m_bIsMuteBGSnd;
	public bool IsMuteFXSnds => m_bIsMuteFXSnds;

	public float BGSndVolume => m_fBGSndVolume;
	public float FXSndsVolume => m_fFXSndsVolume;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oBGSnd = CFactory.CreateCloneObj<CSnd>("BGSnd", 
			this.gameObject, "Global/Prefabs/G_BGSnd");
	}

	/** 배경음 음소거 여부를 변경한다 */
	public void SetIsMuteBGSnd(bool a_bIsMute) {
		m_bIsMuteBGSnd = a_bIsMute;
		m_oBGSnd.SetIsMute(a_bIsMute);
	}

	/** 효과음 음소거 여부를 변경한다 */
	public void SetIsMuteFXSnds(bool a_bIsMute) {
		m_bIsMuteFXSnds = a_bIsMute;
		this.EnumerateFXSnds((a_oSnd) => a_oSnd.SetIsMute(a_bIsMute));
	}

	/** 배경음 볼륨을 변경한다 */
	public void SetBGSndVolume(float a_fVolume) {
		m_fBGSndVolume = Mathf.Clamp01(a_fVolume);
		m_oBGSnd.SetVolume(a_fVolume);
	}

	/** 효과음 볼륨을 변경한다 */
	public void SetFXSndsVolume(float a_fVolume) {
		m_fFXSndsVolume = Mathf.Clamp01(a_fVolume);
		this.EnumerateFXSnds((a_oSnd) => a_oSnd.SetVolume(a_fVolume));
	}

	/** 배경음 재생한다 */
	public CSnd PlayBGSnd(string a_oSndPath) {
		// 배경음 재생이 가능 할 경우
		if(!m_oBGSnd.IsPlaying || !m_oBGSnd.PlaySndPath.Equals(a_oSndPath)) {
			m_oBGSnd.Play(a_oSndPath, true);

			this.SetIsMuteBGSnd(m_bIsMuteBGSnd);
			this.SetBGSndVolume(m_fBGSndVolume);
		}

		return m_oBGSnd;
	}

	/** 효과음 재생한다 */
	public CSnd PlayFXSnds(string a_oSndPath) {
		var oSnd = this.FindPlayableFXSnds(a_oSndPath);

		// 효과음 재생이 가능 할 경우
		if(oSnd != null) {
			oSnd.Play(a_oSndPath, false);

			this.SetIsMuteFXSnds(m_bIsMuteFXSnds);
			this.SetFXSndsVolume(m_fFXSndsVolume);
		}

		return oSnd;
	}

	/** 진동 재생한다 */
	public void PlayVibrate() {
		Handheld.Vibrate();
	}

	/** 플레이 가능한 효과음을 탐색한다 */
	public CSnd FindPlayableFXSnds(string a_oSndPath) {
		var oFXSndsList = m_oFXSndsDictContainer.GetValueOrDefault(a_oSndPath);
		oFXSndsList = oFXSndsList ?? new List<CSnd>();

		m_oFXSndsDictContainer.TryAdd(a_oSndPath, oFXSndsList);

		// 최대 중첩 개수를 넘어갔을 경우
		if(oFXSndsList.Count >= 10) {
			for(int i = 0; i < oFXSndsList.Count; ++i) {
				// 플레이 가능 할 경우
				if(!oFXSndsList[i].IsPlaying) {
					return oFXSndsList[i];
				}
			}

			return null;
		}

		var oSnd = CFactory.CreateCloneObj<CSnd>("FXSnds", this.gameObject, "Global/Prefabs/G_FXSnds");
		oFXSndsList.Add(oSnd);

		return oSnd;
	}

	/** 효과음 순회한다 */
	private void EnumerateFXSnds(System.Action<CSnd> a_oCallback) {
		foreach(var stKeyVal in m_oFXSndsDictContainer) {
			for(int i = 0; i < stKeyVal.Value.Count; ++i) {
				a_oCallback(stKeyVal.Value[i]);
			}
		}
	}
	#endregion // 함수
}
