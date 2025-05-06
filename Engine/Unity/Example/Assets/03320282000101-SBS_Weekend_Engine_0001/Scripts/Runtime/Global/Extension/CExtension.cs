using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
public static partial class CExtension
{
	#region 클래스 함수
	/** 월드 위치를 반환한다 */
	public static Vector3 ExGetWorldPos(this PointerEventData a_oSender,
		Vector3 a_stDesignSize)
	{
		float fNormPosX = a_oSender.position.x / (CAccess.ScreenSize.x / 2.0f) - 1.0f;
		float fNormPosY = a_oSender.position.y / (CAccess.ScreenSize.y / 2.0f) - 1.0f;

		float fScreenWidth = a_stDesignSize.y * (CAccess.ScreenSize.x / CAccess.ScreenSize.y);
		return new Vector3(fNormPosX * (fScreenWidth / 2.0f), fNormPosY * (a_stDesignSize.y / 2.0f), 0.0f);
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this float a_fSender, float a_fRhs)
	{
		return Mathf.Approximately(a_fSender, a_fRhs);
	}

	/** 동일 여부를 검사한다 */
	public static bool ExIsEquals(this Vector3 a_stSender, Vector3 a_stRhs)
	{
		return a_stSender.x.ExIsEquals(a_stRhs.x) &&
			a_stSender.y.ExIsEquals(a_stRhs.y) &&
			a_stSender.z.ExIsEquals(a_stRhs.z);
	}

	/** 작음 여부를 검사한다 */
	public static bool ExIsLess(this float a_fSender, float a_fRhs)
	{
		return a_fSender < a_fRhs - float.Epsilon;
	}

	/** 작음 여부를 검사한다 */
	public static bool ExIsLessEquals(this float a_fSender, float a_fRhs)
	{
		return a_fSender.ExIsLess(a_fRhs) || Mathf.Approximately(a_fSender, a_fRhs);
	}

	/** 큰 여부를 검사한다 */
	public static bool ExIsGreate(this float a_fSender, float a_fRhs)
	{
		return a_fSender > a_fRhs + float.Epsilon;
	}

	/** 큰 여부를 검사한다 */
	public static bool ExIsGreateEquals(this float a_fSender, float a_fRhs)
	{
		return a_fSender.ExIsGreate(a_fRhs) || Mathf.Approximately(a_fSender, a_fRhs);
	}

	/** 정렬 순서를 변경한다 */
	public static void ExSetSortingOrder(this Renderer a_oSender,
		STSortingOrderInfo a_stOrderInfo)
	{
		a_oSender.sortingOrder = a_stOrderInfo.m_nOrder;
		a_oSender.sortingLayerName = a_stOrderInfo.m_oLayer;
	}

	/** 비교 결과를 반환한다 */
	public static int ExCompare(this float a_fSender, float a_fRhs)
	{
		// 값이 동일 할 경우
		if(Mathf.Approximately(a_fSender, a_fRhs))
		{
			return KDefine.G_COMPARE_EQUALS;
		}

		return a_fSender.ExIsLess(a_fRhs) ? KDefine.G_COMPARE_LESS : KDefine.G_COMPARE_GREATE;
	}

	/** 최소 값을 반환한다 */
	public static float ExGetMinVal(this Vector3 a_stSender)
	{
		float fMaxVal = a_stSender.x.ExIsLess(a_stSender.y) ? a_stSender.x : a_stSender.y;
		return fMaxVal.ExIsLess(a_stSender.z) ? fMaxVal : a_stSender.z;
	}

	/** 최대 값을 반환한다 */
	public static float ExGetMaxVal(this Vector3 a_stSender)
	{
		float fMaxVal = a_stSender.x.ExIsGreate(a_stSender.y) ? a_stSender.x : a_stSender.y;
		return fMaxVal.ExIsGreate(a_stSender.z) ? fMaxVal : a_stSender.z;
	}

	/** 월드 => 지역으로 변환한다 */
	public static Vector3 ExToLocal(this Vector3 a_stSender,
		GameObject a_oParent, bool a_bIsCoord = true)
	{
		var stVec4 = new Vector4(a_stSender.x, a_stSender.y, a_stSender.z,
			a_bIsCoord ? 1.0f : 0.0f);

		return a_oParent.transform.worldToLocalMatrix * stVec4;
	}

	/** 지역 => 월드로 변환한다 */
	public static Vector3 ExToWorld(this Vector3 a_stSender,
		GameObject a_oParent, bool a_bIsCoord = true)
	{
		var stVec4 = new Vector4(a_stSender.x, a_stSender.y, a_stSender.z,
			a_bIsCoord ? 1.0f : 0.0f);

		return a_oParent.transform.localToWorldMatrix * stVec4;
	}

	/** 값을 교환한다 */
	public static void ExSwap<T>(this List<T> a_oSender, int a_nIdx01, int a_nIdx02)
	{
		T tTemp = a_oSender[a_nIdx01];
		a_oSender[a_nIdx01] = a_oSender[a_nIdx02];
		a_oSender[a_nIdx02] = tTemp;
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 컴포넌트를 추가한다 */
	public static T ExAddComponent<T>(this GameObject a_oSender) where T : Component
	{
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

	/** 값을 변경한다 */
	public static void ExReplaceVal<K, V>(this Dictionary<K, V> a_oSender, K a_tKey, V a_tVal)
	{
		Debug.Assert(a_oSender != null);

		// 값이 존재 할 경우
		if(a_oSender.ContainsKey(a_tKey))
		{
			a_oSender[a_tKey] = a_tVal;
		}
		else
		{
			a_oSender.Add(a_tKey, a_tVal);
		}
	}
	#endregion // 제네릭 클래스 함수
}
