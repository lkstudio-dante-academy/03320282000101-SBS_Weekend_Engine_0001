using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 카메라 제어자 */
public class CCameraController : CComponent
{
	#region 변수
	private Camera m_oCamera = null;

	/*
	 * SerializeField 속성은 private 제한자로 선언 된 변수를 Unity 상에서 수정 할 수 있도록 노출시키는 역할을 수행한다.
	 * (즉, 해당 속성을 사용하면 public 제한자를 사용하는 것보다 좀 더 안전하게 변수를 활용하는 것이 가능하다는 것을 알 수
	 * 있다.)
	 */
	[SerializeField] private bool m_bIsResetPos = true;
	[SerializeField] private float m_fFOV = 45.0f;
	[SerializeField] private Vector3 m_stDefSize = new Vector3(1280.0f, 720.0f, 0.0f);

	[SerializeField] private GameObject m_oObjsRoot = null;
	#endregion // 변수

	#region 함수
	/*
	 * Awake vs Start 메서드
	 * - 두 메서드 모두 해당 컴포넌트 사용 완료 된 후 딱 한번 호출되는 특징이 존재한다. (즉, 해당 메서드를 활용하면 특정 객체를
	 * 생성 후 초기화하는 역할로 사용하는 것이 가능하다.)
	 * 
	 * 단, Awake 는 해당 컴포넌트가 사용 할 수 있는 상태가 되었을 때 바로 호출이 이루어지는 반면 Start 메서드는 해당 컴포넌트의
	 * Update 차례가 되었을 때 호출되는 차이점이 존재한다.
	 * 
	 * 따라서, 특정 객체를 생성 후 바로 초기화 구문을 실행하고 싶을 경우에는 Start 메서드가 아닌 Awake 메서드를 사용하는 것이
	 * 좋다.
	 */
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		/*
		 * GetComponent 메서드는 특정 게임 객체가 지니고 있는 컴포넌트를 가져오는 역할을 수행한다. 만약, 특정 게임 객체를
		 * 명시하지 않았을 경우에는 해당 구문을 실행하는 스크립트가 포함되어있는 게임 객체의 컴포넌트를 가져온다.
		 * (즉, this.gameObject 를 의미한다는 것을 알 수 있다.)
		 */
		m_oCamera = this.GetComponent<Camera>();
	}

	/** 초기화 */
	public override void Start()
	{
		base.Start();
		this.Setup3DCamera(m_stDefSize);
	}

	/** 2 차원 투영을 설정한다 */
	public void Setup2DCamera(Vector3 a_stSize)
	{
		this.SetupDefOpts(a_stSize);

		m_oCamera.orthographic = true;
		m_oCamera.orthographicSize = a_stSize.y / 2.0f;
	}

	/** 3 차원 투영을 설정한다 */
	public void Setup3DCamera(Vector3 a_stSize)
	{
		this.SetupDefOpts(a_stSize);

		float fHeight = a_stSize.y / 2.0f;
		float fDistance = fHeight / Mathf.Tan((m_fFOV / 2.0f) * Mathf.Deg2Rad);

		m_oCamera.orthographic = false;
		m_oCamera.fieldOfView = m_fFOV;

		// 위치 설정이 가능 할 경우
		if(m_bIsResetPos)
		{
			m_oCamera.transform.localPosition = new Vector3(0.0f, 0.0f, -fDistance);
		}
	}

	/** 기본 옵션을 설정한다 */
	private void SetupDefOpts(Vector3 a_stSize)
	{
		m_oCamera.nearClipPlane = 0.1f;
		m_oCamera.farClipPlane = 10000.0f;

		float fAspect = a_stSize.x / a_stSize.y;
		float fScreenWidth = CAccess.ScreenSize.y * fAspect;

		// 현재 해상도가 작을 경우
		if(CAccess.ScreenSize.x < fScreenWidth)
		{
			float fScale = CAccess.ScreenSize.x / fScreenWidth;
			m_oObjsRoot.transform.localScale = new Vector3(fScale, fScale, fScale);
		}
	}
	#endregion // 함수
}
