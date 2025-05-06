using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 씬 로더 */
public class CSceneLoader : CSingleton<CSceneLoader>
{
	#region 함수
	/** 씬을 로드한다 */
	public void LoadScene(string a_oName, LoadSceneMode a_eSceneMode = LoadSceneMode.Single)
	{
		SceneManager.LoadScene(a_oName, a_eSceneMode);
	}

	/** 씬을 비동기 로드한다 */
	public void LoadSceneAsync(string a_oName,
		System.Action<float> a_oCallback, LoadSceneMode a_eSceneMode = LoadSceneMode.Single)
	{
		this.StartCoroutine(this.DoLoadSceneAsync(a_oName, a_oCallback, a_eSceneMode));
	}

	/** 씬을 비동기 로드한다 */
	public IEnumerator DoLoadSceneAsync(string a_oName,
		System.Action<float> a_oCallback, LoadSceneMode a_eSceneMode)
	{
		/*
		 * SceneManager.LoadSceneAsync 메서드는 Unity 씬을 비동기로 로드하는 역할을 수행한다. (즉, 로드 할 씬이 무거 울 경우
		 * 씬을 로드하는데 시간이 다소 필요하기 때문에 해당 씬을 동기로 로드 할 경우 게임 화면이 잠시 멈추는 현상이 발생한다는 것을
		 * 알 수 있다.)
		 * 
		 * 따라서, 많은 객체를 포함하고 있는 씬을 로드 할 경우에는 비동기 로드 방식을 이용해서 사용자가 게임 멈췄다는 착각을 일으키지
		 * 않게 대처하는 것이 좋다.
		 */
		var oAsyncOperation = SceneManager.LoadSceneAsync(a_oName, a_eSceneMode);

		do
		{
			/*
			 * ? 기호는 옵셔널 체이닝을 의미하는 연산자이다.
			 * 
			 * 옵셔널 체이닝이란?
			 * - 특정 참조 형식 변수가 null 일 경우 해당 구문을 무시 할 수 있게 컴파일러가 명령문을 보정해주는 기능을 의미한다.
			 * (즉, 일반적으로 null 이 할당 된 참조 변수를 사용 할 경우 런타임 에러가 발생하지만 옵셔널 체이닝을 활용하면 좀 더
			 * 안전하게 참조 형식 변수를 사용하는 것이 가능하다.)
			 * 
			 * Ex)
			 * var oCallback = null;
			 * 
			 * if(oCallback != null) {
			 *		oCallback();
			 * }
			 * 
			 * 위의 구문과 같이 null 여부를 확인 후 해당 참조 변수가 null 이 아닐 경우에는 변수를 참조하도록 컴파일러가 방어 구문을
			 * 작성해주는 개념과 유사하다.
			 */
			a_oCallback?.Invoke(oAsyncOperation.progress);
			yield return new WaitForEndOfFrame();
		} while(!oAsyncOperation.isDone);
	}
	#endregion // 함수
}
