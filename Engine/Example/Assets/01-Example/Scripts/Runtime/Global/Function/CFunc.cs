using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 전역 메서드 */
public static partial class CFunc {
	#region 함수
	/** 팝업을 출력한다 */
	public static void ShowPopup(string a_oName, 
		GameObject a_oParent, string a_oPath, System.Action<CPopup> a_oCallback) {
		CFunc.ShowPopup(a_oName, a_oParent, Resources.Load<GameObject>(a_oPath), a_oCallback);
	}

	/** 팝업을 출력한다 */
	public static void ShowPopup(string a_oName, 
		GameObject a_oParent, GameObject a_oOrigin, System.Action<CPopup> a_oCallback) {
		/*
		 * Debug.Assert 메서드는 입력으로 전달 된 조건이 거짓 일 경우 에러를 발생시키는 역할을 수행한다. (즉, 해당 메서드를 활용하면
		 * 의도하지 않는 데이터가 전달됨에 따라 문제가 발생하는 부분을 검출해내는 것이 가능하다.)
		 * 
		 * 또한, 해당 메서드는 디버그 (개발) 환경에서만 동작하기 때문에 릴리즈 (배포) 환경에서는 해당 메서드는 모두 제외가 된 상태에서
		 * 컴파일이 이루어지는 특징이 있다.
		 */
		Debug.Assert(a_oParent != null && a_oOrigin != null);

		// 팝업이 없을 경우
		if(a_oParent.transform.Find(a_oName) == null) {
			var oPopup = CFactory.CreateCloneObj<CPopup>(a_oName, a_oParent, a_oOrigin);
			a_oCallback?.Invoke(oPopup);

			oPopup.Show();
		}
	}
	#endregion // 함수
}
