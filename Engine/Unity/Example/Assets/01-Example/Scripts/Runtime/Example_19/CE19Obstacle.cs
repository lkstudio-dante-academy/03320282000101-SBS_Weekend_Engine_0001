using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 장애물 */
public class CE19Obstacle : CComponent {
	#region 변수
	private int m_nHitCount = 3;
	#endregion // 변수

	#region 함수
	/** 타격 되었을 경우 */
	public void OnHit() {
		m_nHitCount -= 1;

		// 히트 횟수가 모두 차감 되었을 경우
		if(m_nHitCount <= 0) {
			var oRigidbody = this.GetComponent<Rigidbody>();
			oRigidbody.mass = 1.0f;
			oRigidbody.constraints = RigidbodyConstraints.None;
			oRigidbody.freezeRotation = false;

			float fOffsetX = Random.Range(-75.0f, 75.0f);
			float fOffsetZ = Random.Range(-75.0f, 75.0f);

			var stExplosionOffset = new Vector3(fOffsetX, -1.0f, fOffsetZ);

			oRigidbody.AddExplosionForce(1000.0f, 
				this.transform.position + stExplosionOffset, 50.0f, 1.0f, ForceMode.VelocityChange);

			Destroy(this.gameObject, 5.0f);
		}
	}
	#endregion // 함수
}
