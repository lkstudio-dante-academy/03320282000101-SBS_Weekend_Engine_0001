//#define E21_ECHO_SERVER
#define E21_CHATTING_SERVER

using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using UnityEngine;

/** Example 21 */
public partial class CE21SceneManager : CSceneManager {
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E21;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

#if E21_ECHO_SERVER
		this.AwakeEchoServer();
#elif E21_CHATTING_SERVER
		this.AwakeChattingServer();
#endif // #if E21_ECHO_SERVER
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();

#if E21_CHATTING_SERVER
		this.UpdateChattingServer();
#endif // #if E21_CHATTING_SERVER
	}

	/** 어플리케이션이 종료 되었을 경우 */
	public void OnApplicationQuit() {
#if E21_ECHO_SERVER
		this.OnApplicationQuitEchoServer();
#elif E21_CHATTING_SERVER
		this.OnApplicationQuitChattingServer();
#endif // #if E21_ECHO_SERVER
	}
	#endregion // 함수
}
