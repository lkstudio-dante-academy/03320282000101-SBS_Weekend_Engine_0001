using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 카메라 제어자 */
public class CE19CameraController : CComponent {
	#region 변수
	[SerializeField] private float m_fHeight = 0.0f;
	[SerializeField] private float m_fDistance = 0.0f;

	[SerializeField] private Vector3 m_stOffset = Vector3.zero;
	[SerializeField] private GameObject m_oTarget = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.UpdateCameraState(true);
	}

	/** 상태를 갱신한다 */
	public void LateUpdate() {
		this.UpdateCameraState();
	}

	/** 카메라 상태를 갱신한다 */
	private void UpdateCameraState(bool a_bIsImmediate = false) {
		var stPos = this.transform.position;
		var stTargetPos = m_oTarget.transform.position;

		var stYOffset = Vector3.up * m_fHeight;
		var stZOffset = m_oTarget.transform.forward * -m_fDistance;

		// 즉시 적용 모드 일 경우
		if(a_bIsImmediate) {
			stPos = stTargetPos + (stYOffset + stZOffset);
		} else {
			stPos = Vector3.Lerp(stPos, 
				stTargetPos + (stYOffset + stZOffset), Time.deltaTime * 10.0f);
		}

		this.transform.position = stPos;
		this.transform.LookAt(stTargetPos + m_stOffset);
	}
	#endregion // 함수
}
