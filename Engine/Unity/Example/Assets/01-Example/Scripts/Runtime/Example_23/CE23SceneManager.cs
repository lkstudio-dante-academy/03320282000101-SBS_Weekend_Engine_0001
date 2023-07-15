using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** Example 23 */
public class CE23SceneManager : CSceneManager {
	#region 변수
	private bool m_bIsTouch = false;

	[SerializeField] private GameObject m_oBall = null;
	[SerializeField] private LineRenderer m_oAimingLine = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E23;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		// 전달자를 설정한다
		var oDispatcher = this.UIs.GetComponentInChildren<CTouchDispatcher>();
		oDispatcher.BeginCallback = this.HandleTouchBegin;
		oDispatcher.MoveCallback = this.HandleTouchMove;
		oDispatcher.EndCallback = this.HandleTouchEnd;
	}

	/** 터치 시작을 처리한다 */
	private void HandleTouchBegin(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {
		m_bIsTouch = true;
		m_oAimingLine.gameObject.SetActive(true);

		this.HandleTouchMove(a_oSender, a_oEventData);
	}

	/** 터치 이동을 처리한다 */
	private void HandleTouchMove(CTouchDispatcher a_oSender,
		PointerEventData a_oEventData) {
		// 터치 중 일 경우
		if(m_bIsTouch) {
			var stTouchPos = a_oEventData.ExGetWorldPos(KDefine.G_DESIGN_SIZE);

			m_oAimingLine.SetPositions(new Vector3[] {
				m_oBall.transform.localPosition,
				stTouchPos
			});

			var stPos = m_oBall.transform.position;
			var stDirection = m_oBall.transform.position - stTouchPos;

			// 충돌체가 존재 할 경우
			if(Physics.Raycast(stPos, 
				stDirection.normalized, out RaycastHit stRaycastHit, stDirection.magnitude)) {

			}
		}
	}

	/** 터치 종료를 처리한다 */
	private void HandleTouchEnd(CTouchDispatcher a_oSender, 
		PointerEventData a_oEventData) {
		m_bIsTouch = false;
		m_oAimingLine.gameObject.SetActive(false);
	}
	#endregion // 함수
}
