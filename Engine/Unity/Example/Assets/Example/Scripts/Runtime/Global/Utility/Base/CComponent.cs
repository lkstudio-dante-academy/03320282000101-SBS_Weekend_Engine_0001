using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 컴포넌트 */
public class CComponent : MonoBehaviour {
	#region 함수
	/** 초기화 */
	public virtual void Awake() {
		// Do Something
	}

	/** 초기화 */
	public virtual void Start() {
		// Do Something
	}

	/** 제거 되었을 경우 */
	public virtual void OnDestroy() {
		// Do Something
	}
	#endregion // 함수
}
