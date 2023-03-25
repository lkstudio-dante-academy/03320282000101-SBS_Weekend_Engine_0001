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
	public bool IsActiveScene => this.SceneName.Equals(SceneManager.GetActiveScene().name);
	public abstract string SceneName { get; }
	#endregion // 프로퍼티

	#region 클래스 프로퍼티
	public static CSceneManager ActiveSceneManager =>
		CSceneManager.m_oSceneManagerDict.GetValueOrDefault(SceneManager.GetActiveScene().name);
	#endregion // 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		CSceneManager.m_oSceneManagerDict.TryAdd(this.SceneName, this);
	}

	/** 초기화 */
	public override void Start() {
		base.Start();
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
