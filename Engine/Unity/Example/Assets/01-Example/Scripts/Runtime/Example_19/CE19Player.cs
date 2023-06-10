using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 플레이어 */
public class CE19Player : CComponent {
	#region 변수
	private Animation m_oAni = null;
	[SerializeField] private GameObject m_oContents = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oAni = this.GetComponentInChildren<Animation>();
		m_oAni.Play("Idle");
	}

	/** 상태를 갱신한다 */
	public void Update() {
		/*
		 * Input.GetAxis 계열 메서드를 활용하면 스틱과 같은 디바이스의 축 정보를 가져오는 것이
		 * 가능하다. 또한, 해당 설정은 유니티에서 키보드의 특정 키와 매칭 할 수 있기 때문에
		 * 멀티 플랫폼을 염두해두고 있다면 해당 메서드를 활용하는 것을 추천한다.
		 */
		float fVertical = Input.GetAxisRaw("Vertical");
		float fHorizontal = Input.GetAxisRaw("Horizontal");

		var stVertical = Vector3.forward * fVertical;
		var stHorizontal = Vector3.right * fHorizontal;

		var stDirection = Vector3.Normalize(stVertical + stHorizontal);
		this.transform.localPosition += stDirection * 350.0f * Time.deltaTime;

		// 전방/후방 이동 키를 눌렀을 경우
		if(!fVertical.ExIsEquals(0.0f)) {
			m_oAni.CrossFade(fVertical.ExIsLess(0.0f) ? "RunB" : "RunF", 0.25f);
		}
		// 왼쪽/오른쪽 이동 키를 눌렀을 경우
		else if(!fHorizontal.ExIsEquals(0.0f)) {
			m_oAni.CrossFade(fHorizontal.ExIsLess(0.0f) ? "RunL" : "RunR", 0.25f);
		}
		else {
			m_oAni.CrossFade("Idle", 0.25f);
		}
	}
	#endregion // 함수
}
