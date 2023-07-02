using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using UnityEngine;

/** Example 21 */
public class CE21SceneManager : CSceneManager {
	#region 변수
	private Thread m_oServerThread = null;
	private Thread m_oClientThread = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E21;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oServerThread = new Thread(this.ServerThread);
		m_oClientThread = new Thread(this.ClientThread);

		m_oServerThread.Start();
		m_oClientThread.Start();
	}

	/** 어플리케이션이 종료 되었을 경우 */
	public void OnApplicationQuit() {
		m_oServerThread.Abort();
		m_oClientThread.Abort();
	}

	/** 서버 쓰레드 */
	private void ServerThread() {
		var oListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 9080);
	}

	/** 클라이언트 쓰레드 */
	private void ClientThread() {

	}
	#endregion // 함수
}
