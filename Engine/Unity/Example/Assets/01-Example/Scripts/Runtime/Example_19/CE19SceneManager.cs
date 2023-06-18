using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** Example 19 */
public class CE19SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E19;
	public CObjPoolManager ObjPoolManager { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		this.ObjPoolManager = CFactory.CreateObj<CObjPoolManager>("ObjPoolManager", 
			this.gameObject);
	}
	#endregion // 함수
}
