using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 18 */
public class CE18SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E18;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 시작 버튼을 눌렀을 경우 */
	public void OnTouchPlayBtn() {
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E19);
	}
	#endregion // 함수
}
