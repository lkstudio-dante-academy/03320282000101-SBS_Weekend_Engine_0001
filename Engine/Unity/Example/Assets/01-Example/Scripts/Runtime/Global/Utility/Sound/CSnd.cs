using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 사운드 */
public class CSnd : CComponent {
	#region 변수
	private AudioSource m_oAudioSrc = null;
	#endregion // 변수

	#region 프로퍼티
	public string PlaySndPath { get; private set; } = string.Empty;

	public bool IsMute => m_oAudioSrc.mute;
	public bool IsPlaying => m_oAudioSrc.isPlaying;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oAudioSrc = this.GetComponent<AudioSource>();
		m_oAudioSrc.playOnAwake = false;
	}

	/** 음소거 여부를 변경한다 */
	public void SetIsMute(bool a_bIsMute) {
		m_oAudioSrc.mute = a_bIsMute;
	}

	/** 볼륨을 변경한다 */
	public void SetVolume(float a_fVolume) {
		m_oAudioSrc.volume = Mathf.Clamp01(a_fVolume);
	}

	/** 사운드를 재생한다 */
	public void Play(string a_oSndPath, bool a_bIsLoop, bool a_bIs3DSnd = false) {
		m_oAudioSrc.loop = a_bIsLoop;
		m_oAudioSrc.clip = CResManager.Instance.GetRes<AudioClip>(a_oSndPath);
		m_oAudioSrc.spatialBlend = a_bIs3DSnd ? 1.0f : 0.0f;

		m_oAudioSrc.Play();
		this.PlaySndPath = a_oSndPath;
	}
	#endregion // 함수
}
