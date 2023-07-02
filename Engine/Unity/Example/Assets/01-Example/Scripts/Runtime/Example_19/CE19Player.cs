using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 플레이어 */
public class CE19Player : CComponent {
	#region 변수
	private int m_nHP = 0;
	private Animation m_oAni = null;

	[SerializeField] private GameObject m_oContents = null;
	[SerializeField] private GameObject m_oBulletRoot = null;
	[SerializeField] private GameObject m_oBulletSpawnPos = null;
	#endregion // 변수

	#region 프로퍼티
	public int HP => m_nHP;
	public System.Action<CE19Player> HitCallback { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oAni = this.GetComponentInChildren<Animation>();
		m_oAni.Play("Idle");
	}

	/** 초기화 */
	public void Init(int a_nHP, System.Action<CE19Player> a_oHitCallback) {
		m_nHP = a_nHP;
		this.HitCallback = a_oHitCallback;
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

		float fMouseX = Input.GetAxisRaw("Mouse X");

		var stVertical = this.transform.forward * fVertical;
		var stHorizontal = this.transform.right * fHorizontal;

		var stDirection = Vector3.Normalize(stVertical + stHorizontal);
		this.transform.localPosition += stDirection * 350.0f * Time.deltaTime;

		// 발사 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space)) {
			var oSceneManager = CSceneManager.ActiveSceneManager as CE19SceneManager;
			var stBulletPos = m_oBulletSpawnPos.transform.position.ExToLocal(m_oBulletRoot);

			var oBullet = oSceneManager.GameObjPoolManager.SpawnGameObj("Bullet",
				"Example_19/E19Bullet", stBulletPos, Vector3.one, Vector3.zero, m_oBulletRoot);

			oBullet.transform.forward = this.transform.forward;

			var oRigidbody = oBullet.GetComponent<Rigidbody>();
			oRigidbody.useGravity = false;
			oRigidbody.AddForce(this.transform.forward * 2500.0f, ForceMode.VelocityChange);
		}

		// 마우스 오른쪽 버튼을 눌렀을 경우
		if(Input.GetMouseButton((int)EMouseBtn.RIGHT)) {
			this.transform.eulerAngles += Vector3.up * fMouseX * 1500.0f * Time.deltaTime;
		}

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

	/** 타격 되었을 경우 */
	public void OnHit() {
		m_nHP = Mathf.Max(0, m_nHP - 10);
		this.HitCallback(this);
	}

	/** 충돌이 발생했을 경우 */
	public void OnTriggerEnter(Collider a_oCollider) {
		// 적 일 경우
		if(a_oCollider.CompareTag("E19EnemyHand")) {
			this.OnHit();
		}
	}
	#endregion // 함수
}
