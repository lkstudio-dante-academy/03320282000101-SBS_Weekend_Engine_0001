using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 팩토리 */
public static partial class CFactory {
	#region 클래스 함수
	/** 객체를 생성한다 */
	public static GameObject CreateObj(string a_oName, GameObject a_oParent) {
		return CFactory.CreateObj(a_oName, a_oParent, Vector3.zero, Vector3.one, Vector3.zero);
	}

	/** 객체를 생성한다 */
	public static GameObject CreateObj(string a_oName,
		GameObject a_oParent, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle) {
		var oGameObj = new GameObject(a_oName);
		oGameObj.transform.SetParent(a_oParent?.transform, false);

		oGameObj.transform.localPosition = a_stPos;
		oGameObj.transform.localScale = a_stScale;
		oGameObj.transform.localEulerAngles = a_stAngle;

		return oGameObj;
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName, GameObject a_oParent, string a_oPath) {
		return CFactory.CreateCloneObj(a_oName, a_oParent, a_oPath, Vector3.zero, Vector3.one, Vector3.zero);
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName, GameObject a_oParent, GameObject a_oOrigin) {
		return CFactory.CreateCloneObj(a_oName, a_oParent, a_oOrigin, Vector3.zero, Vector3.one, Vector3.zero);
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName,
		GameObject a_oParent, string a_oPath, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle) {

		return CFactory.CreateCloneObj(a_oName,
			a_oParent, Resources.Load<GameObject>(a_oPath), a_stPos, a_stScale, a_stAngle);
	}

	/** 사본 객체를 생성한다 */
	public static GameObject CreateCloneObj(string a_oName,
		GameObject a_oParent, GameObject a_oOrigin, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle) {
		Debug.Assert(a_oOrigin != null);

		var oGameObj = GameObject.Instantiate(a_oOrigin, Vector3.zero, Quaternion.identity);
		oGameObj.name = a_oName;
		oGameObj.transform.SetParent(a_oParent?.transform, false);

		oGameObj.transform.localPosition = a_stPos;
		oGameObj.transform.localScale = a_stScale;
		oGameObj.transform.localEulerAngles = a_stAngle;

		return oGameObj;
	}
	#endregion // 클래스 함수

	#region 제네릭 클래스 함수
	/** 객체를 생성한다 */
	public static T CreateObj<T>(string a_oName, GameObject a_oParent) where T : Component {
		return CFactory.CreateObj(a_oName, a_oParent).ExAddComponent<T>();
	}

	/** 객체를 생성한다 */
	public static T CreateObj<T>(string a_oName,
		GameObject a_oParent, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle) where T : Component {
		return CFactory.CreateObj(a_oName, a_oParent, a_stPos, a_stScale, a_stAngle).ExAddComponent<T>();
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName, GameObject a_oParent, string a_oPath) where T : Component {
		return CFactory.CreateCloneObj(a_oName, a_oParent, a_oPath).GetComponentInChildren<T>();
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName, GameObject a_oParent, GameObject a_oOrigin) where T : Component {
		return CFactory.CreateCloneObj(a_oName, a_oParent, a_oOrigin).GetComponentInChildren<T>();
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName,
		GameObject a_oParent, string a_oPath, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle) where T : Component {
		return CFactory.CreateCloneObj(a_oName, a_oParent, a_oPath, a_stPos, a_stScale, a_stAngle).GetComponentInChildren<T>();
	}

	/** 사본 객체를 생성한다 */
	public static T CreateCloneObj<T>(string a_oName,
		GameObject a_oParent, GameObject a_oOrigin, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle) where T : Component {
		return CFactory.CreateCloneObj(a_oName, a_oParent, a_oOrigin, a_stPos, a_stScale, a_stAngle).GetComponentInChildren<T>();
	}
	#endregion // 제네릭 클래스 함수
}
