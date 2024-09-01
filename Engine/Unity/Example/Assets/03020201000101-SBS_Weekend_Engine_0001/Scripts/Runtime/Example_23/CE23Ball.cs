using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 볼 */
public class CE23Ball : CComponent
{
	#region 변수
	private bool m_bIsShoot = false;
	private Vector3 m_stDirection = Vector3.zero;

	private System.Action<CE23Ball> m_oCallback = null;
	private System.Action<CE23Ball, CE23Obstacle> m_oHitCallback = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public virtual void Init(System.Action<CE23Ball, CE23Obstacle> a_oCallback)
	{
		m_oHitCallback = a_oCallback;
	}

	/** 상태를 갱신한다 */
	public void Update()
	{
		// 발사 상태가 아닐 경우
		if(!m_bIsShoot)
		{
			return;
		}

		var stPos = this.transform.position;
		var stNextPos = stPos + (m_stDirection * 1500.0f * Time.deltaTime);

		var stRay = new Ray(stPos, m_stDirection);
		float fRadius = this.transform.localScale.x / 2.0f;

		// 충돌체가 존재 할 경우
		if(Physics.SphereCast(stRay, fRadius, out RaycastHit stRaycastHit))
		{
			var stRayHitPos = (stPos + m_stDirection * stRaycastHit.distance);

			var stDelta01 = stNextPos - stPos;
			var stDelta02 = stRayHitPos - stPos;

			float fDistance01 = stDelta01.magnitude;
			float fDistance02 = stDelta02.magnitude;

			stNextPos = fDistance01.ExIsLessEquals(fDistance02) ? stNextPos
				: stRayHitPos;

			// 방향 전환이 필요 할 경우
			if(stNextPos.ExIsEquals(stRayHitPos))
			{
				m_stDirection = Vector3.Reflect(m_stDirection, stRaycastHit.normal);
				m_stDirection.z = 0.0f;

				// 장애물과 충돌했을 경우
				if(stRaycastHit.collider.CompareTag("E23Obstacle"))
				{
					var oObstacle = stRaycastHit.collider.GetComponentInParent<CE23Obstacle>();
					m_oHitCallback(this, oObstacle);
				}

				// 아래쪽 경계와 충돌했을 경우
				if(stRaycastHit.collider.CompareTag("E23DownBounds"))
				{
					this.SetIsShoot(false);
					m_oCallback?.Invoke(this);
				}
			}
		}

		this.transform.position = stNextPos;
	}

	/** 볼을 발사한다 */
	public void Shoot(Vector3 a_stDirection, System.Action<CE23Ball> a_oCallback)
	{
		m_oCallback = a_oCallback;
		m_stDirection = a_stDirection.normalized;

		this.SetIsShoot(true);
	}
	#endregion // 함수

	#region 접근 함수
	/** 발사 상태 여부를 변경한다 */
	public void SetIsShoot(bool a_bIsShoot)
	{
		m_bIsShoot = a_bIsShoot;
	}
	#endregion // 접근 함수
}
