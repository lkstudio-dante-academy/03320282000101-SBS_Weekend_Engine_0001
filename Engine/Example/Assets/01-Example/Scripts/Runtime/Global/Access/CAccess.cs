using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 정적 클래스란?
 * - 일반적인 클래스와 달리 클래스 맴버를 비롯한 클래스에 종속되는 맴버들만 선언 및 구현 할 수 있는 클래스를 의미한다.
 * 따라서, 정적 클래스는 일반적인 맴버가 존재하지 않기 때문에 객체화 시켜도 아무런 의미가 없기 때문에 객체화 시키는 구문을
 * 작성 할 경우 컴파일 에러가 발생한다는 특징이 존재한다.
 *
 * partial 키워드란?
 * - partial 키워드는 특정 클래스 또는 구조체를 여러 파일에 나누어서 구현 할 수 있는 역할을 수행한다. (즉, 특정 클래스의 
 * 맴버가 너무 많아서 하나의 파일에 작성 및 관리가 어려울 경우 partial 키워드를 활용 할 수 있다는 것을 알 수 있다.)
 */
/** 전역 접근자 */
public static partial class CAccess {
	#region 프로퍼티
	public static Vector3 ScreenSize {
		get {
			return new Vector3(Screen.width, Screen.height, 0.0f);
		}
	}
	#endregion // 프로퍼티
}
