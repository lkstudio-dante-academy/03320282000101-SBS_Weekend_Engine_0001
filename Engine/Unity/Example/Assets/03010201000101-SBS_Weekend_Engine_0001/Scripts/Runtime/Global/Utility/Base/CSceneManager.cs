using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/** 씬 관리자 */
public abstract class CSceneManager : CComponent
{
	#region 클래스 변수
	private static bool m_bIsFirstAwake = true;
	private static bool m_bIsDirtyUpdateScreenSize = true;
	private static Dictionary<string, CSceneManager> m_oSceneManagerDict = new Dictionary<string, CSceneManager>();
	#endregion // 클래스 변수

	/*
	 * Active 씬 이란?
	 * - 기본적으로 유니티는 여러 씬을 중첩해서 특정 게임 공간을 표현하는 것이 가능하다.
	 * 
	 * 이때, 가장 기본이 되는 씬을 Active 씬이라고 하며 유니티의 특정 메서드는 Active 씬을 대상으로 동작하기 때문에 특정 씬을 대상으로
	 * 메서드를 호출하기 위해서는 Active 씬을 대상으로 하는 메서드를 사용하면 안된다. (Ex. GameObject.Find 메서드 등등...)
	 * 
	 * 또한, 유니티의 모든 게임 객체는 내부적으로 scene 프로퍼티를 지니고 있으며 해당 프로퍼티를 활용하면 특정 객체가 속해 있는 씬의 정보를
	 * 가져오는 것이 가능하다.
	 */
	#region 프로퍼티
	public Light MainLight { get; private set; } = null;
	public Camera MainCamera { get; private set; } = null;
	public EventSystem UIsEventSystem { get; private set; } = null;

	public GameObject UIs { get; private set; } = null;
	public GameObject PopupUIs { get; private set; } = null;

	public GameObject Objs { get; private set; } = null;
	public GameObject StaticObjs { get; private set; } = null;

	public bool IsActiveScene => this.SceneName.Equals(SceneManager.GetActiveScene().name);
	public abstract string SceneName { get; }
	#endregion // 프로퍼티

	#region 클래스 프로퍼티
	public static Light ActiveSceneMainLight { get; private set; } = null;
	public static Camera ActiveSceneMainCamera { get; private set; } = null;
	public static EventSystem ActiveSceneUIsEventScene { get; private set; } = null;

	public static GameObject ActiveSceneUIs { get; private set; } = null;
	public static GameObject ActiveScenePopupUIs { get; private set; } = null;

	public static GameObject ActiveSceneObjs { get; private set; } = null;
	public static GameObject ActiveSceneStaticObjs { get; private set; } = null;

	public static CSceneManager ActiveSceneManager =>
		CSceneManager.m_oSceneManagerDict.GetValueOrDefault(SceneManager.GetActiveScene().name);
	#endregion // 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		Physics.gravity = new Vector3(0.0f, -1750.0f, 0.0f);
		CSceneManager.m_oSceneManagerDict.TryAdd(this.SceneName, this);

		// 최초 초기화 일 경우
		if(CSceneManager.m_bIsFirstAwake)
		{
			Debug.Log($"Persistent Path: {Application.persistentDataPath}");
			CSceneManager.m_bIsFirstAwake = false;
		}

#if UNITY_STANDALONE
		// 해상도 설정이 필요 할 경우
		if(this.IsActiveScene && CSceneManager.m_bIsDirtyUpdateScreenSize)
		{
			/*
			 * Screen 클래스를 활용하면 현재 디스플레이의 해상도를 변경하거나 정보를 가져오는
			 * 것이 가능하다. (즉, 해당 클래스를 활용하면 수직 동기화에 필요한 디스플레이의
			 * 화면 갱신 주기 등을 가져오는 것이 가능하다.)
			 */
			Screen.SetResolution(960, 540, FullScreenMode.Windowed);
			CSceneManager.m_bIsDirtyUpdateScreenSize = false;
		}
#endif // #if UNITY_STANDALONE

		/*
		 * scene 프로퍼티를 활용하면 특정 게임 객체가 속해 있는 씬에 접근하는 것이 가능하다. 
		 * 따라서, 해당 프로퍼티를 활용하면 특정 씬에 속해 있는 게임 객체를 탐색하기 위한 구문을 
		 * 작성 할 수 있다.
		 * 
		 * 단, 씬을 통해 접근 할 수 있는 게임 객체는 최상단 객체만 접근 할 수 있기 때문에 해당
		 * 객체를 통해 자식 게임 객체를 탐색하기 위한 구문을 추가적으로 작성해야되는 단점이
		 * 존재한다.
		 */
		var oRootGameObjs = this.gameObject.scene.GetRootGameObjects();

		for(int i = 0; i < oRootGameObjs.Length; ++i)
		{
			var oMainLight = oRootGameObjs[i].transform.Find("DirectionalLight");
			var oMainCamera = oRootGameObjs[i].transform.Find("MainCamera");
			var oEventSystem = oRootGameObjs[i].transform.Find("EventSystem");

			var oUIs = oRootGameObjs[i].transform.Find("Canvas/UIs");
			var oPopupUIs = oRootGameObjs[i].transform.Find("Canvas/PopupUIs");

			var oObjs = oRootGameObjs[i].transform.Find("Objs");
			var oStaticObjs = oRootGameObjs[i].transform.Find("StaticObjs");

			// 메인 광원이 존재 할 경우
			if(oMainLight != null && this.MainLight == null)
			{
				this.MainLight = oMainLight.GetComponent<Light>();
			}

			// 메인 카메라가 존재 할 경우
			if(oMainCamera != null && this.MainCamera == null)
			{
				this.MainCamera = oMainCamera.GetComponent<Camera>();
			}

			// 이벤트 시스템이 존재 할 경우
			if(oEventSystem != null && this.UIsEventSystem == null)
			{
				this.UIsEventSystem = oEventSystem.GetComponent<EventSystem>();
			}

			// UIs 가 존재 할 경우
			if(oUIs != null && this.UIs == null)
			{
				this.UIs = oUIs.gameObject;
			}

			// Popup UIs 가 존재 할 경우
			if(oPopupUIs != null && this.PopupUIs == null)
			{
				this.PopupUIs = oPopupUIs.gameObject;
			}

			// Objs 가 존재 할 경우
			if(oObjs != null && this.Objs == null)
			{
				this.Objs = oObjs.gameObject;
			}

			// Static Objs 가 존재 할 경우
			if(oStaticObjs != null && this.StaticObjs == null)
			{
				this.StaticObjs = oStaticObjs.gameObject;
			}
		}

		this.MainLight.gameObject.SetActive(this.IsActiveScene);
		this.MainCamera.gameObject.SetActive(this.IsActiveScene);
		this.UIsEventSystem.gameObject.SetActive(this.IsActiveScene);

		// 액티브 씬 일 경우
		if(this.IsActiveScene)
		{
			/*
			 * GameObject.Find 메서드는 액티브 씬에 특정 게임 객체를 탐색하는 역할을 수행한다. (즉, 해당 메서드는 활용하면 액티브 씬에
			 * 속해있는 객체를 탐색 할 수 있지만 해당 씬에 많은 게임 객체가 존재 할 경우 모든 게임 객체를 탐색하기 때문에 프로그램 성능이
			 * 저하된다.)
			 * 
			 * 따라서, 해당 메서드 또한 한번 실행 후 결과를 캐시함으로서 메서드 호출 횟수를 줄여 줄 필요가 있다.
			 */
			CSceneManager.ActiveSceneMainLight = GameObject.Find("DirectionalLight").GetComponent<Light>();
			CSceneManager.ActiveSceneMainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
			CSceneManager.ActiveSceneUIsEventScene = GameObject.Find("EventSystem").GetComponent<EventSystem>();

			CSceneManager.ActiveSceneUIs = GameObject.Find("UIs");
			CSceneManager.ActiveScenePopupUIs = GameObject.Find("PopupUIs");

			CSceneManager.ActiveSceneObjs = GameObject.Find("Objs");
			CSceneManager.ActiveSceneStaticObjs = GameObject.Find("StaticObjs");
		}
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();

		try
		{
			// 씬 관리자가 존재 할 경우
			if(CSceneManager.m_oSceneManagerDict.ContainsKey(this.SceneName))
			{
				CSceneManager.m_oSceneManagerDict.Remove(this.SceneName);
			}
		}
		catch(System.Exception oException)
		{
			Debug.LogWarning($"CSceneManager.OnDestroy Exception : {oException.Message}");
		}
	}

	/** 상태를 갱신한다 */
	public virtual void Update()
	{
		// ESC 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Escape) &&
			!this.SceneName.Equals(KDefine.G_SCENE_N_E00))
		{
			CFunc.ShowAlertPopup("현재 씬을 종료하시겠습니까?", this.OnReceiveAlertPopupCallback);
		}
	}

	/** 알림 팝업 콜백을 수신했을 경우 */
	private void OnReceiveAlertPopupCallback(CAlertPopup a_oSender, bool a_bIsOK)
	{
		// 확인을 눌렀을 경우
		if(a_bIsOK)
		{
			CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E00);
		}
	}
	#endregion // 함수

	#region 제네릭 클래스 메서드
	/** 씬 관리자를 반환한다 */
	public static T GetSceneManager<T>(string a_oName) where T : CSceneManager
	{
		return CSceneManager.m_oSceneManagerDict.GetValueOrDefault(a_oName) as T;
	}

	/** 액티브 씬 관리자를 반환한다 */
	public static T GetActiveSceneManager<T>() where T : CSceneManager
	{
		return CSceneManager.ActiveSceneManager as T;
	}
	#endregion // 제네릭 클래스 메서드
}
