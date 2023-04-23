using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 사운드 관리자 */
public class CSndManager : CSingleton<CSndManager> {
	#region 변수
	private CSnd m_oBGSnd = null;
	private Dictionary<string, List<CSnd>> m_oFXSndsDictContainer = new Dictionary<string, List<CSnd>>();
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_oBGSnd = CFactory.CreateCloneObj<CSnd>("BGSnd", this.gameObject, "Global/Prefabs/G_BGSnd");
	}

	/** 배경음 재생한다 */
	public CSnd PlayBGSnd(string a_oSndPath) {
		// 배경음 재생이 가능 할 경우
		if(!m_oBGSnd.IsPlaying || !m_oBGSnd.PlaySndPath.Equals(a_oSndPath)) {
			m_oBGSnd.Play(a_oSndPath, true);
		}

		return m_oBGSnd;
	}

	/** 효과음 재생한다 */
	public CSnd PlayFXSnds(string a_oSndPath) {
		var oSnd = this.FindPlayableFXSnds(a_oSndPath);

		// 효과음 재생이 가능 할 경우
		if(oSnd != null) {
			oSnd.Play(a_oSndPath, false);
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
	#endregion // 함수
}
