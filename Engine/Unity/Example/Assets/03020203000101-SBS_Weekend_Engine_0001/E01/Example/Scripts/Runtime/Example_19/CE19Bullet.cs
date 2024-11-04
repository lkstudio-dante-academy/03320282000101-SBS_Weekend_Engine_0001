using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 총알 */
public class CE19Bullet : CComponent
{
	#region 함수
	/** 충돌이 발생했을 경우 */
	public void OnCollisionEnter(Collision a_oCollision)
	{
		// 장애물과 충돌했을 경우
		if(a_oCollision.collider.CompareTag("E19Obstacle"))
		{
			var oObstacle = a_oCollision.collider.GetComponentInParent<CE19Obstacle>();
			oObstacle.OnHit();
		}
		// 적과 충돌했을 경우
		else if(a_oCollision.collider.CompareTag("E19Enemy"))
		{
			var oEnemy = a_oCollision.collider.GetComponentInChildren<CE19Enemy>();
			oEnemy.OnHit();
		}

		var oTrail = this.GetComponentInChildren<TrailRenderer>();
		oTrail.Clear();

		var oRigidbody = this.GetComponent<Rigidbody>();
		oRigidbody.velocity = Vector3.zero;

		var oSceneManager = CSceneManager.ActiveSceneManager as CE01Example_19;
		oSceneManager.GameObjPoolManager.DespawnGameObj("Example_19/E19Bullet", this.gameObject);
	}
	#endregion // 함수
}
