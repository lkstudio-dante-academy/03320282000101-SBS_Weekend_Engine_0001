using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 싱글턴이란?
 * - 디자인 패턴 중 하나로서 프로그램 전체에 유일하게 존재하는 객체를 생성 할 수 있는 구조를 의미한다. (즉, 일반적인 객체 지향 언어는
 * 전역 공간이라는 개념이 존재하지 않기 때문에 전역 변수와 같은 유일한 변수를 선언하는 것이 상대적으로 까다롭지만 싱글턴 패턴을 활용하면
 * 좀 더 수월하게 전역 변수와 같은 효과를 지니는 구조를 만드는 것이 가능하다.)
 */
/** 싱글턴 */
public class CSingleton<T> : CComponent where T : CSingleton<T> {
	#region 클래스 변수
	private static bool m_bIsWarning = true;
	private static T m_tInstance = null;
	#endregion // 클래스 변수

	#region 클래스 프로퍼티
	public static T Instance {
		get {
			// 인스턴스가 없을 경우
			if(m_tInstance == null) {
				var oGameObj = new GameObject(typeof(T).Name);

				CSingleton<T>.m_bIsWarning = false;
				CSingleton<T>.m_tInstance = oGameObj.AddComponent<T>();

				/*
				 * DontDestroyOnLoad 메서드는 유니티 씬이 전환 되도 특정 객체를 제거하지 않게 방지하는 역할을 수행한다. (즉, 
				 * 기본적으로 유니티는 씬 전환 될 때 기존 씬에 존재하는 모든 객체를 제거한다는 것을 알 수 있다.)
				 * 
				 * 따라서, 씬이 전환되도 특정 객체를 계속 유지하기 위해서는 반드시 해당 메서드를 활용해야하며 만약 특정 객체의
				 * 부모가 존재 할 경우 반드시 부모 객체를 지정해줘야한다. (즉, 해당 메서드에 입력으로 전달 할 수 있는 객체는
				 * 아무런 부모 객체도 지니고 있지 않는 최상단 객체여야한다.)
				 */
				DontDestroyOnLoad(oGameObj);
			}

			return m_tInstance;
		}
	}
	#endregion // 클래스 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 경고 상태 일 경우
		if(CSingleton<T>.m_bIsWarning) {
			Debug.LogWarning($"{typeof(T).Name} 이 씬 상에 미리 배치 되어있습니다!");
		}

		// 인스턴스가 없을 경우
		if(CSingleton<T>.m_tInstance == null) {
			CSingleton<T>.m_tInstance = this as T;
		}
	}
	#endregion         // 함수

	#region 클래스 함수
	/** 인스턴스를 생성한다 */
	public static T Create() {
		return CSingleton<T>.Instance;
	}
	#endregion // 클래스 함수
}
