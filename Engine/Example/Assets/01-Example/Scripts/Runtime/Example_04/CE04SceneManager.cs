using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 4 */
public class CE04SceneManager : CSceneManager {
	#region 변수
	private float m_fPower = 0.0f;
	private const float MAX_POWER = 2500.0f;

	[SerializeField] private Image m_oGaugeImg = null;
	[SerializeField] private GameObject m_oTarget = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E04;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public void Update() {
		/** 스페이스 키를 눌렀을 경우 */
		if(Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space)) {
			m_fPower = Input.GetKeyDown(KeyCode.Space) ? 0.0f : m_fPower;
			m_fPower = Mathf.Clamp(m_fPower + (MAX_POWER * Time.deltaTime), 0.0f, MAX_POWER);

			m_oGaugeImg.fillAmount = m_fPower / MAX_POWER;
		}

		/** 스페이스 키를 땠을 경우 */
		if(Input.GetKeyUp(KeyCode.Space)) {
			var oRigidbody = m_oTarget.GetComponent<Rigidbody>();
			oRigidbody.useGravity = true;
			oRigidbody.isKinematic = false;

			var stPos = m_oTarget.transform.position;
			oRigidbody.AddForceAtPosition(m_oTarget.transform.right * m_fPower, stPos + (Vector3.up * 10.0f), ForceMode.VelocityChange);
		}

		/** 방향 키를 눌렀을 경우 */
		if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
			float fAngle = m_oTarget.transform.localEulerAngles.z;
			fAngle += Input.GetKey(KeyCode.UpArrow) ? 90.0f * Time.deltaTime : -90.0f * Time.deltaTime;

			var stAngles = m_oTarget.transform.localEulerAngles;
			m_oTarget.transform.localEulerAngles = new Vector3(stAngles.x, stAngles.y, Mathf.Clamp(fAngle, 0.0f, 80.0f));
		}
	}

	/** 기즈모를 그린다 */
	public void OnDrawGizmos() {
		// 타겟이 존재 할 경우
		if(m_oTarget != null) {
			Color stPrevColor = Gizmos.color;

			try {
				var stPos = m_oTarget.transform.position;

				Gizmos.color = Color.red;
				Gizmos.DrawLine(stPos, stPos + (m_oTarget.transform.right * 300.0f));
			} finally {
				Gizmos.color = stPrevColor;
			}
		}
	}
	#endregion // 함수
}
