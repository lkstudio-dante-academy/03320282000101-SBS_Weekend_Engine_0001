using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/** Example 21 - 채팅 */
public partial class CExample_21 : CSceneManager
{
	#region 변수
	private Thread m_oChattingClientThread = null;

#if UNITY_EDITOR
	private Thread m_oChattingServerThread = null;
	private List<TcpClient> m_oChattingServerSocketList = new List<TcpClient>();
#endif // #if UNITY_EDITOR

	[Header("=====> UIs <=====")]
	[SerializeField] private InputField m_oChattingServerInput = null;
	[SerializeField] private ScrollRect m_oChatterServerScrollRect = null;

	[Header("=====> Game Objects <=====")]
	[SerializeField] private GameObject m_oChatterServerOriginMsgText = null;
	[SerializeField] private GameObject m_oChatterServerScrollViewContents = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	private void AwakeChattingServer()
	{
		CE21CSDataStorage.Create();

		m_oChattingClientThread = new Thread(this.ClientThreadChattingServer);
		m_oChattingClientThread.Start();

#if UNITY_EDITOR
		m_oChattingServerThread = new Thread(this.ServerThreadChattingServer);
		m_oChattingServerThread.Start();
#endif // UNITY_EDITOR
	}

	/** 상태를 갱신한다 */
	private void UpdateChattingServer()
	{
		// 메세지 입력 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Return) && m_oChattingServerInput.text.Length > 0)
		{
			CE21CSDataStorage.Instance.WriteMsgQueue.Enqueue(m_oChattingServerInput.text);
			m_oChattingServerInput.text = string.Empty;
		}

		// 메세지가 존재 할 경우
		if(CE21CSDataStorage.Instance.ReadMsgQueue.Count > 0)
		{
			var oMsgText = CFactory.CreateCloneObj<Text>("MsgText",
				m_oChatterServerScrollViewContents, m_oChatterServerOriginMsgText);

			oMsgText.text = CE21CSDataStorage.Instance.ReadMsgQueue.Dequeue();
			m_oChatterServerScrollRect.verticalNormalizedPosition = 1.0f;
		}
	}

	/** 어플리케이션이 종료 되었을 경우 */
	private void OnApplicationQuitChattingServer()
	{
		m_oChattingClientThread?.Abort();

#if UNITY_EDITOR
		m_oChattingServerThread?.Abort();
#endif // #if UNITY_EDITOR
	}

	/** 클라이언트 쓰레드 */
	private void ClientThreadChattingServer()
	{
		var oSocket = new TcpClient(AddressFamily.InterNetwork);
		oSocket.Connect(IPAddress.Loopback, 18080);

		// 서버 소켓과 연결 되었을 경우
		if(oSocket.Connected)
		{
			this.HandleSocketReadState(oSocket);
			this.HandleSocketWriteState(oSocket);

			oSocket.Close();
		}
	}

	/** 클라이언트 소켓 입력 상태를 처리한다 */
	private async void HandleSocketReadState(TcpClient a_oSocket)
	{
		var oBuffer = new byte[1024];

		do
		{
			var oTask = a_oSocket.GetStream().ReadAsync(oBuffer, 0, oBuffer.Length);
			await oTask;

			// 연결이 끊어졌을 경우
			if(oTask.Result <= 0)
			{
				break;
			}

			string oMsg = System.Text.Encoding.Default.GetString(oBuffer, 0, oTask.Result);
			CE21CSDataStorage.Instance.ReadMsgQueue.Enqueue(oMsg);
		} while(a_oSocket.Connected);
	}

	/** 클라이언트 소켓 출력 상태를 처리한다 */
	private void HandleSocketWriteState(TcpClient a_oSocket)
	{
		do
		{
			// 출력 메세지가 존재 할 경우
			if(CE21CSDataStorage.Instance.WriteMsgQueue.Count > 0)
			{
				string oMsg = CE21CSDataStorage.Instance.WriteMsgQueue.Dequeue();
				var oBytes = System.Text.Encoding.Default.GetBytes(oMsg);

				a_oSocket.GetStream().Write(oBytes, 0, oBytes.Length);
			}
		} while(a_oSocket.Connected);
	}

#if UNITY_EDITOR
	/** 서버 쓰레드 */
	private void ServerThreadChattingServer()
	{
		var oListener = new TcpListener(IPAddress.Loopback, 18080);
		oListener.Start();

		do
		{
			var oSocket = oListener.AcceptTcpClient();

			// 연결 되었을 경우
			if(oSocket.Connected)
			{
				m_oChattingServerSocketList.Add(oSocket);
				this.HandleConnectSocket(oSocket);
			}
		} while(true);
	}

	/*
	 * async 키워드는 특정 메서드가 비동기로 동작한다는 것을 C# 컴파일러에게 알리는 역할을 수행
	 * 한다. (즉, 해당 키워드를 사용하면 C# 컴파일러가 해당 메서드를 비동기 메서드로 인식한다는
	 * 것을 알 수 있다.)
	 * 
	 * 단, 해당 키워드는 단순히 비동기 사실을 컴파일러에게 알리는 역할이기 때문에 실질적이 해당
	 * 키워드로 명시된 메서드가 비동기로 동작하기 위해서는 await 키워드를 통한 비동기 처리 구문이
	 * 반드시 해당 메서드 내부에 작성 되어있어야한다. (즉, await 키워드는 특정 작업을 비동기로
	 * 처리 할 수 있게 내부적으로 구조를 형성해주는 키워드라는 것을 알 수 있다.)
	 * 
	 * Thread vs Task
	 * - 두 시스템 모두 비동기 처리를 수행할 수 있는 기능을 제공한다. Thread 는 서로 연관성이
	 * 적은 작업을 비동기 (병렬) 처리 할 때 주로 활용되며 Task 는 하나의 작업을 병렬로 처리하고
	 * 싶을 때 주로 활용된다는 차이점이 존재한다.
	 * 
	 * 단, Thread 내부적으로 쓰레드로 전환 될 때 발생하는 오버헤드가 존재하기 때문에 하나의 작업을
	 * 비동기로 처리하는데는 적합하지 않다는 것을 알 수 있다.
	 */
	/** 연결 된 소켓을 처리한다 */
	private async void HandleConnectSocket(TcpClient a_oSocket)
	{
		var oBuffer = new byte[1024];
		var oEndPoint = a_oSocket.Client.RemoteEndPoint as IPEndPoint;

		string oMsg = string.Format("{0}:{1} 이(가) 접속했습니다.",
				oEndPoint.Address.ToString(), oEndPoint.Port);

		var oBytes = System.Text.Encoding.Default.GetBytes(oMsg);

		for(int i = 0; i < m_oChattingServerSocketList.Count; ++i)
		{
			m_oChattingServerSocketList[i].GetStream().Write(oBytes, 0, oBytes.Length);
		}

		do
		{
			var oTask = a_oSocket.GetStream().ReadAsync(oBuffer, 0, oBuffer.Length);
			await oTask;

			// 연결이 끊어졌을 경우
			if(oTask.Result <= 0)
			{
				break;
			}

			for(int i = 0; i < m_oChattingServerSocketList.Count; ++i)
			{
				m_oChattingServerSocketList[i].GetStream().Write(oBuffer, 0, oTask.Result);
			}
		} while(true);

		a_oSocket.Close();
		m_oChattingServerSocketList.Remove(a_oSocket);
	}
#endif // #if UNITY_EDITOR
	#endregion // 함수
}
