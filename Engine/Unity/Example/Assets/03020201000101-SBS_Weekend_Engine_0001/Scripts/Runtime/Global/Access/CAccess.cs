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
public static partial class CAccess
{
	#region 프로퍼티
	public static Vector3 ScreenSize
	{
		get
		{
#if UNITY_EDITOR
			/*
			 * 에디터에서는 Screen 클래스를 사용해서 해상도를 가져 올 경우
			 * Game 뷰의 해상도가 아닌 모니터의 해상도를 가져오기 때문에 
			 * 에디터에서 정확한 Game 뷰의 해상도를 가져오기 위해서는 카메라를
			 * 이용해야한다.
			 */
			return new Vector3(Camera.main.pixelWidth,
				Camera.main.pixelHeight, 0.0f);
#else
			return new Vector3(Screen.width, Screen.height, 0.0f);
#endif // #if UNITY_EDITOR
		}
	}
	#endregion // 프로퍼티

	#region 함수
	/** 저장 가능한 경로를 반환한다 */
	public static string GetWriteablePath(string a_oPath)
	{
		/*
		 * Application.persistentDataPath 프로퍼티는 저장 가능한
		 * 경로를 가져오는 역할을 수행한다. (즉, 맥/윈도우즈 플랫폼에서는
		 * 시스템에 의해서 보호 되는 영역이 아니라면 어떤 경로에서는 파일을
		 * 생성하고 제거하는 행위가 가능하지만 모바일 플랫폼에서는 특정 앱이
		 * 접근 할 수 있는 공간이 따로 정해져있기 때문에 멀티 플랫폼 환경을
		 * 고려한 프로그램을 제작하기 위해서는 유니티에서 제공하는 해당 프로퍼티를
		 * 활용하는 것을 추천한다.
		 */
		return string.Format("{0}/{1}",
			Application.persistentDataPath, a_oPath);
	}
	#endregion // 함수
}
