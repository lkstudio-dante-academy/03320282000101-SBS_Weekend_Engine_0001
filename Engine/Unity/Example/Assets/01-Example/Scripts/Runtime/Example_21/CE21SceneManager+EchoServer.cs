using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using UnityEngine;

/** Example 21 - 에코 */
public partial class CE21SceneManager : CSceneManager {
	#region 변수
	private Thread m_oEchoServerThread = null;
	private Thread m_oEchoClientThread = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	private void AwakeEchoServer() {
		m_oEchoServerThread = new Thread(this.ServerThreadEchoServer);
		m_oEchoServerThread.Start();

		m_oEchoClientThread = new Thread(this.ClientThreadEchoServer);
		m_oEchoClientThread.Start();
	}

	/** 어플리케이션이 종료 되었을 경우 */
	private void OnApplicationQuitEchoServer() {
		m_oEchoServerThread?.Abort();
		m_oEchoClientThread?.Abort();
	}

	/** 서버 쓰레드 */
	private void ServerThreadEchoServer() {
		/*
		 * 리스너 소켓이란?
		 * - 클라이언트 소켓의 연결 요청을 처리 할 수 있는 소켓을 의미한다. (즉, 일반적으로
		 * TCP 방식으로 데이터를 송/수신 할 경우 송/수신 할 소켓 간에 연결이 먼저 선행 되어야
		 * 하기 때문에 TCP 방식에서는 반드시 먼저 리스너 소켓을 생성 할 필요가 있다.)
		 * 
		 * 따라서, 리스너 소켓을 생성하는 호스트가 서버가 되며 클라이언트는 리스너 소켓에게
		 * 연결을 요청한 후 연결이 완료되면 해당 소켓을 기반으로 데이터를 송/수신하면 된다.
		 */
		var oListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 18080);
		oListener.Start();

		/*
		 * AcceptTcpClient 메서드는 리스너 소켓에 입력 된 연결 요청을 기반으로 해당 요청을
		 * 한 소켓과 데이터 송/수신 할 수 있는 소켓을 생성 후 반환하는 역할을 수행한다.
		 * (즉, TCP 방식에서는 하나의 소켓은 하나의 호스트하고만 데이터를 송/수신 할 수 있다는
		 * 것을 알 수 있다.)
		 * 
		 * 단, 해당 메서드는 블럭킹 메서드이기 때문에 만약 해당 메서드를 호출 한 시점에 아직
		 * 아무런 연결 요청이 없을 경우에는 새로운 연결 요청이 들어올때까지 대기하는 특징이
		 * 있다.
		 */
		var oSocket = oListener.AcceptTcpClient();
		var oBuffer = new byte[short.MaxValue];

		while(true) {
			int nBytes = oSocket.GetStream().Read(oBuffer, 0, oBuffer.Length);

			// 연결이 끊어졌을 경우
			if(nBytes <= 0) {
				break;
			}

			string oStr = System.Text.Encoding.Default.GetString(oBuffer, 0, nBytes);
			Debug.Log($"클라이언트로부터 받은 메세지 : {oStr}");

			oSocket.GetStream().Write(oBuffer, 0, nBytes);
		}

		/*
		 * 소켓은 컴퓨터의 공유 자원을 사용하기 때문에 더이상 필요 없을 경우 반드시 Close
		 * 메서드를 호출해서 소켓을 해제해 줄 필요가 있다. (즉, Close 메서드를 호출하지 않으면
		 * 불필요한 소켓이 계속 늘어나며 이는 곧 필요 할 때 소켓을 더이상 생성 할 수 없는 문제를
		 * 발생 시킬 수 있다는 것을 알 수 있다.)
		 */
		oSocket.Close();
		oListener.Stop();
	}

	/** 클라이언트 쓰레드 */
	private void ClientThreadEchoServer() {
		/*
		 * Connect 메서드는 서버 소켓 (리스너 소켓) 에게 연결은 요청하는 역할을 수행한다. (즉,
		 * 해당 메서드 호출이 완료되고 나면 서버와 데이터를 송/수신 할 수 있는 상태가 되었다는
		 * 것을 의미한다.)
		 * 
		 * 단, 연결 요청에 대한 응답이 지연 된 경우 Timeout 으로 인해 메서드 호출이 종료 될 수
		 * 있기 때문에 해당 메서드를 호출하고 나면 반드시 Connected 프로퍼티를 활용해서 정상적으로
		 * 연결이 된 상태인지 검사해 줄 필요가 있다.
		 * 
		 * 또한, 일반적으로 연결을 요청하는 클라이언트 소켓은 IP 주소와 포트 번호를 지정하기 않기
		 * 때문에 Connect 메서드는 소켓에 IP 주소와 포트 번호를 설정해주는 역할도 수행하는 특징이
		 * 있다. (즉, 서버 소켓과 연결이 완료되고 나면 자동으로 IP 주소와 포트 번호가 지정 되어
		 * 있다는 것을 알 수 있다.)
		 */
		var oSocket = new TcpClient(AddressFamily.InterNetwork);
		oSocket.Connect(IPAddress.Parse("127.0.0.1"), 18080);

		var oBuffer = new byte[short.MaxValue];

		// 서버 소켓과 연결 되었을 경우
		if(oSocket.Connected) {
			/*
			 * Encoding 클래스를 이용하면 서로 다른 문자열 테이블을 사용하는 프로그램에 데이터를
			 * 올바르게 송/수신하는 것이 가능하다. (즉, 일반적으로 유니코드 (UTF-8) 를 사용하면
			 * 문자열을 별도로 엔코딩/디코딩 할 필요가 없지만 아직 유니코드를 공식적으로 지원하지 
			 * 않는 플랫폼도 존재하기 때문에 문자열 데이터를 송/수신 할 때는 반드시 현재 플랫폼에서
			 * 사용되는 문자열 테이블에 맞게 문자열 데이터를 엔코딩/디코딩 할 필요가 있다.)
			 */
			var oBytes = System.Text.Encoding.Default.GetBytes("Hello, World!");
			oSocket.GetStream().Write(oBytes, 0, oBytes.Length);

			int nBytes = oSocket.GetStream().Read(oBuffer, 0, oBuffer.Length);
			string oStr = System.Text.Encoding.Default.GetString(oBytes, 0, nBytes);

			Debug.Log($"서버로부터 받은 메세지 : {oStr}");
			oSocket.Close();
		}
	}
	#endregion // 함수
}
