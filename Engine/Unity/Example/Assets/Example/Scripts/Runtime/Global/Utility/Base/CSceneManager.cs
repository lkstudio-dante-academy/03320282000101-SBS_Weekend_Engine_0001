using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 씬 관리자 */
public abstract class CSceneManager : CComponent {
	#region 클래스 변수
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
	public GameObject UIs { get; private set; } = null;
	public GameObject PopupUIs { get; private set; } = null;

	public GameObject Objs { get; private set; } = null;
	public GameObject StaticObjs { get; private set; } = null;

	public bool IsActiveScene => this.SceneName.Equals(SceneManager.GetActiveScene().name);
	public abstract string SceneName { get; }
	#endregion // 프로퍼티

	#region 클래스 프로퍼티
	public static GameObject ActiveSceneUIs { get; private set; } = null;
	public static GameObject ActiveScenePopupUIs { get; private set; } = null;

	public static GameObject ActiveSceneObjs { get; private set; } = null;
	public static GameObject ActiveSceneStaticObjs { get; private set; } = null;

	public static CSceneManager ActiveSceneManager =>
		CSceneManager.m_oSceneManagerDict.GetValueOrDefault(SceneManager.GetActiveScene().name);
	#endregion // 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CSceneManager.m_oSceneManagerDict.TryAdd(this.SceneName, this);

		/*
		 * GameObject.Find 메서드는 액티브 씬에 특정 게임 객체를 탐색하는 역할을 수행한다. (즉, 해당 메서드는 활용하면 액티브 씬에
		 * 속해있는 객체를 탐색 할 수 있지만 해당 씬에 많은 게임 객체가 존재 할 경우 모든 게임 객체를 탐색하기 때문에 프로그램 성능이
		 * 저하된다.)
		 * 
		 * 따라서, 해당 메서드 또한 한번 실행 후 결과를 캐시함으로서 메서드 호출 횟수를 줄여 줄 필요가 있다.
		 */
		this.UIs = GameObject.Find("UIs");
		this.PopupUIs = GameObject.Find("PopupUIs");

		this.Objs = GameObject.Find("Objs");
		this.StaticObjs = GameObject.Find("StaticObjs");

		// 액티브 씬 일 경우
		if(this.IsActiveScene) {
			CSceneManager.ActiveSceneUIs = this.UIs;
			CSceneManager.ActiveScenePopupUIs = this.PopupUIs;

			CSceneManager.ActiveSceneObjs = this.Objs;
			CSceneManager.ActiveSceneStaticObjs = this.StaticObjs;
		}
	}

	/** 초기화 */
	public override void Start() {
		base.Start();

		// 액티브 씬 일 경우
		if(this.IsActiveScene) {
			Time.timeScale = 1.0f;
		}
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		try {
			// 씬 관리자가 존재 할 경우
			if(CSceneManager.m_oSceneManagerDict.ContainsKey(this.SceneName)) {
				CSceneManager.m_oSceneManagerDict.Remove(this.SceneName);
			}
		} catch(System.Exception oException) {
			Debug.LogWarning($"CSceneManager.OnDestroy Exception : {oException.Message}");
		}
	}

	/** 상태를 갱신한다 */
	public virtual void Update() {
		// ESC 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Escape) &&
			!this.SceneName.Equals(KDefine.G_SCENE_N_E00)) {
			CFunc.ShowAlertPopup("현재 씬을 종료하시겠습니까?", this.OnReceiveAlertPopupCallback);
		}
	}

	/** 알림 팝업 콜백을 수신했을 경우 */
	private void OnReceiveAlertPopupCallback(CAlertPopup a_oSender, bool a_bIsOK) {
		// 확인을 눌렀을 경우
		if(a_bIsOK) {
			CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E00);
		}
	}
	#endregion // 함수

	#region 제네릭 클래스 메서드
	/** 씬 관리자를 반환한다 */
	public static T GetSceneManager<T>(string a_oName) where T : CSceneManager {
		return CSceneManager.m_oSceneManagerDict.GetValueOrDefault(a_oName) as T;
	}

	/** 액티브 씬 관리자를 반환한다 */
	public static T GetActiveSceneManager<T>() where T : CSceneManager {
		return CSceneManager.ActiveSceneManager as T;
	}
	#endregion // 제네릭 클래스 메서드
}
