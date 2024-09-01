using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 5 */
public class CExample_05 : CSceneManager
{
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E05;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}

	/** 플레이 버튼을 눌렀을 경우 */
	public void OnTouchPlayBtn()
	{
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E06);
	}
	#endregion // 함수
}
