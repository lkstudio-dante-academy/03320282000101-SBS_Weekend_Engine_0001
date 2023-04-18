using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 확장 메서드란?
 * - 특정 클래스가 지니고 있는 기능을 메서드를 통해서 확장 시킬 수 있는 기능을 의미한다. (즉, 확장 메서드를 활용하면 제 3 자가 만들어서
 * 제공하는 여러 클래스를 좀 더 쉽게 사용할 수 있게 유틸리티 메서드를 추가하는 것이 가능하다.)
 * 
 * 기존에 특정 클래스가 지니고 있는 기능을 확장하기 위해서는 상속을 통한 확장 방식을 사용했지만 해당 방식은 별도의 자료형이 만들어지는
 * 구조이기 때문에 단순한 기능을 추가하기 위해서 새로운 자료형을 만드는 것은 비효율적이라는 것을 알 수 있다.
 * 
 * 따라서, C# 은 확장 메서드를 통해 기존 클래스가 지니고 있던 기능을 확장함으로서 단순한 기능들은 새로운 클래스 만들 필요 없이 메서드로
 * 간단하게 기능을 추가하는 방법을 제공한다.
 * 
 * 단, 확장 메서드를 구현하기 위해서는 반드시 해당 메서드를 어떤 공간에도 포함되어있지 않는 최상단 정적 클래스에 구현 할 필요가 있다.
 * 
 * 또한, 일반적인 정적 메서드와 확장 메서드를 구분하기 위해서 확장 메서드는 반드시 첫번째 매개 변수에 this 명시해줘야한다.
 * (즉, 컴파일러는 this 키워드를 통해 구현 된 정적 메서드가 해당 자료형에 새롭게 추가 된 확장 메서드라고 인식한다.)
 */
/** 확장 클래스 */
public static partial class CExtension {
	#region 클래스 함수
	/** 작음 여부를 검사한다 */
	public static bool ExIsLess(this float a_fSender, float a_fRhs) {
		return a_fSender < a_fRhs - float.Epsilon;
	}

	/** 작음 여부를 검사한다 */
	public static bool ExIsLessEquals(this float a_fSender, float a_fRhs) {
		return a_fSender <= a_fRhs - float.Epsilon;
	}

	/** 큰 여부를 검사한다 */
	public static bool ExIsGreate(this float a_fSender, float a_fRhs) {
		return a_fSender > a_fRhs + float.Epsilon;
	}

	/** 큰 여부를 검사한다 */
	public static bool ExIsGreateEquals(this float a_fSender, float a_fRhs) {
		return a_fSender >= a_fRhs + float.Epsilon;
	}

	/** 비교 결과를 반환한다 */
	public static int ExCompare(this float a_fSender, float a_fRhs) {
		// 값이 동일 할 경우
		if(Mathf.Approximately(a_fSender, a_fRhs)) {
			return KDefine.G_COMPARE_EQUALS;
		}

		return a_fSender.ExIsLess(a_fRhs) ? KDefine.G_COMPARE_LESS : KDefine.G_COMPARE_GREATE;
	}

	/** 최소 값을 반환한다 */
	public static float ExGetMinVal(this Vector3 a_stSender) {
		float fMaxVal = a_stSender.x.ExIsLess(a_stSender.y) ? a_stSender.x : a_stSender.y;
		return fMaxVal.ExIsLess(a_stSender.z) ? fMaxVal : a_stSender.z;
	}

	/** 최대 값을 반환한다 */
	public static float ExGetMaxVal(this Vector3 a_stSender) {
		float fMaxVal = a_stSender.x.ExIsGreate(a_stSender.y) ? a_stSender.x : a_stSender.y;
		return fMaxVal.ExIsGreate(a_stSender.z) ? fMaxVal : a_stSender.z;
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 컴포넌트를 추가한다 */
	public static T ExAddComponent<T>(this GameObject a_oSender) where T : Component {
		Debug.Assert(a_oSender != null);

		/*
		 * ?? 연산자는 옵셔널 체이닝 중 하나로서 해당 연산자를 기준으로 좌항에 존재하는 피 연산자가 null 일 경우 우항을 실행하는
		 * 특징이 있으며 null 병합 연산자라고 불린다.
		 * 
		 * 따라서, 해당 연산자를 활용하면 특정 객체가 null 일 경우 새로운 객체를 생성하도록 명령문의 구조를 단순하게 작성하는데
		 * 활용 할 수 있다.
		 */
		return a_oSender.GetComponent<T>() ?? a_oSender.AddComponent<T>();
	}
	#endregion // 제네릭 클래스 함수
}
