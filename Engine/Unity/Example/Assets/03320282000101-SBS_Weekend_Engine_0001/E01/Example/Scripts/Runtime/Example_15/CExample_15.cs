using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/** Example 15 */
public class CE01Example_15 : CSceneManager
{
	#region 변수
	[SerializeField] private TMP_Text m_oScoreText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E15;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		m_oScoreText.text = string.Format("Socre : {0}", CE05DataStorage.Instance.Score);
	}

	/** 다시하기 버튼을 눌렀을 경우 */
	public void OnTouchRetryBtn()
	{
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E14);
	}

	/** 그만두기 버튼을 눌렀을 경우 */
	public void OnTouchLeaveBtn()
	{
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E13);
	}
	#endregion // 함수
}
