using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 충돌 전달자 */
public class CTriggerDispatcher : CComponent {
	#region 프로퍼티
	public System.Action<CTriggerDispatcher, Collider> EnterCallbck { get; set; } = null;
	public System.Action<CTriggerDispatcher, Collider> StayCallbck { get; set; } = null;
	public System.Action<CTriggerDispatcher, Collider> ExitCallbck { get; set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 충돌이 발생했을 경우 */
	public void OnTriggerEnter(Collider a_oCollider) {
		this.EnterCallbck?.Invoke(this, a_oCollider);
	}

	/** 충돌이 진행 중 일 경우 */
	public void OnTriggerStay(Collider a_oCollider) {
		this.StayCallbck?.Invoke(this, a_oCollider);
	}

	/** 충돌이 종료 되었을 경우 */
	public void OnTriggerExit(Collider a_oCollider) {
		this.ExitCallbck?.Invoke(this, a_oCollider);
	}
	#endregion // 함수
}
