using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Example 2 */
public class CE02SceneManager : CSceneManager {
	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		Debug.Log("Hello, World!");
	}
	#endregion // 함수
}
