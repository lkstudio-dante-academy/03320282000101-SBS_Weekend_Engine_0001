using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/** 플랫폼 관리자 */
public class CPlatformManager : CSingleton<CPlatformManager>
{
	#region 변수
	private Dictionary<string, System.Action<CPlatformManager, string>> m_oCallbackDict = new Dictionary<string, System.Action<CPlatformManager, string>>();

#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern void HandleUnityMsg(string a_oCmd, string a_oMsg);
#elif UNITY_ANDROID
	private AndroidJavaClass m_oUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
#endif // #if UNITY_ANDROID
	#endregion // 변수

	#region 함수
	/** 메세지를 전송한다 */
	public void SendUnityMsg(string a_oCmd,
		string a_oMsg, System.Action<CPlatformManager, string> a_oCallback)
	{
		m_oCallbackDict.TryAdd(a_oCmd, a_oCallback);

#if UNITY_IOS
		CPlatformManager.HandleUnityMsg(a_oCmd, a_oMsg);
#elif UNITY_ANDROID
		var oUnityActivity = m_oUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		oUnityActivity.Call("HandleUnityMsg", a_oCmd, a_oMsg);
#endif // #if UNITY_IOS
	}

	/** 플랫폼 메세지를 처리한다 */
	public void HandlePlatformMsg(string a_oCmd, string a_oMsg)
	{
		// 콜백이 존재 할 경우
		if(m_oCallbackDict.ContainsKey(a_oCmd))
		{
			m_oCallbackDict[a_oCmd]?.Invoke(this, a_oMsg);
			m_oCallbackDict.Remove(a_oCmd);
		}
	}
	#endregion // 함수
}
