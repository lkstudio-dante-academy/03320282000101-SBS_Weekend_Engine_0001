using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 컴포넌트 */
public class CComponent : MonoBehaviour, IUpdatable {
	#region 프로퍼티
	public bool IsDestroy { get; private set; } = false;
	#endregion // 프로퍼티

	#region IUpdatable
	/** 상태를 갱신한다 */
	public virtual void OnUpdate(float a_fDeltaTime) {
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void OnLateUpdate(float a_fDeltaTime) {
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void OnFixedUpdate(float a_fDeltaTime) {
		// Do Something
	}
	#endregion // IUpdatable

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
		this.IsDestroy = true;
	}
	#endregion // 함수
}
