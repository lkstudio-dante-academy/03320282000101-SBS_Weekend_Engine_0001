using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** Example 20 */
public class CE01Example_20 : CSceneManager
{
	#region 변수
	[SerializeField] private TMP_Text m_oScoreText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E20;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		this.UpdateUIsState();
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState()
	{
		// 텍스트를 갱신한다
		m_oScoreText.text = string.Format("Score : {0}", CE19DataStorage.Instance.Score);
	}

	/** 다시하기 버튼을 눌렀을 경우 */
	public void OnTouchRetryBtn()
	{
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E19);
	}

	/** 그만두기 버튼을 눌렀을 경우 */
	public void OnTouchLeaveBtn()
	{
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E18);
	}
	#endregion // 함수
}
