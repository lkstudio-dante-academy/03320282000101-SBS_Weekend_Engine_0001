using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/** 팝업 */
public partial class CPopup : CComponent {
	#region 변수
	private float m_fPrevTimeScale = 0.0f;
	private Tween m_oAni = null;
	#endregion // 변수

	#region 프로퍼티
	public Image BlindImg { get; private set; } = null;

	public GameObject Contents { get; private set; } = null;
	public GameObject ContentsUIs { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.transform.localScale = Vector3.zero;

		/*
		 * 유니티 게임 객체는 계층적인 구조를 지닐 수 있으며 해당 구조는 Transform 컴포넌트에 의해서 관리되고 있기 때문에 만약 특정
		 * 게임 객체 하위에 존재하는 객체를 탐색하고 싶다면 Transform 컴포넌트가 지니고 있는 Find 메서드를 활용하면 된다.
		 * 
		 * 즉, 해당 메서드는 하위 객체를 탐색하는 역할을 수행하면 만약 탐색하고자 하는 객체가 해당 메서드를 호출한 객체 바로 하위에
		 * 위치하지 않았을 경우는 / 기호를 통해 경로를 입력하듯 계층 구조에 포함 된 객체의 이름을 상위부터 순차적으로 경로를 명시하면
		 * 된다.
		 * 
		 * 단, 해당 메서드는 자주 호출 될 경우 프로그램의 성능을 저하 시킬 수 있기 때문에 가능하면 사용 횟수를 줄이는 것이 좋으며
		 * 가장 좋은 방법은 Awake 와 같은 한번만 호출되는 메서드에서 해당 메서드를 호출 후 탐색한 객체를 변수 등을 활용해서 캐시하는
		 * 것이다.
		 */
		// 컨텐츠를 설정한다
		this.Contents = this.transform.Find("Contents").gameObject;
		this.ContentsUIs = this.transform.Find("Contents/BGImg/ContentsUIs").gameObject;

		/*
		 * Resources.Load 메서드를 사용하면 프로그램이 실행 중에 필요한 에셋을 동적으로 로드하는 것이 가능하다.
		 * (즉, 특정 에셋을 미리 에디터 상에서 설정하지 않고 프로그램 실행되는 상황에 동적으로 필요한 에셋을 즉시 로드 할 수 있다는
		 * 것을 알 수 있다.)
		 * 
		 * 따라서, 테이블과 같은 특정 데이터에 따라 필요한 에셋이 서로 다를 경우 에디터 상에 미리 설정하는 방식은 비효율적이기 때문에 
		 * 해당 상황에서는 Resource.Load 메서드와 같은 동적으로 에셋을 로드하는 구조로 프로그램을 작성 할 필요가 있다.
		 * 
		 * 단, Resources.Load 유니티 에셋 폴더 상에 Resources 라는 이름을 지닌 폴더만 인식 할 수 있기 때문에 해당 메서드를 사용해서
		 * 에셋을 동적으로 로드하기 위해서는 반드시 해당 에셋이 Resources 폴더 하위에 위치 할 필요가 있다. (즉, 해당 메서드에 명시되는
		 * 경로는 Resources 폴더를 기준으로 하는 상대 경로를 입력하면 된다.)
		 * 
		 * 또한, 입력되는 경로 중 에셋의 확장자는 명시 할 필요가 없다.
		 */
		// 블라인드 이미지를 설정한다 {
		this.BlindImg = CFactory.CreateCloneObj<Image>("BlindImg", this.ContentsUIs, "Global/Prefabs/G_BlindImg");
		this.BlindImg.color = new Color(0.0f, 0.0f, 0.0f, 0.8f);
		this.BlindImg.transform.localScale = CAccess.ScreenSize * 2.5f;

		this.BlindImg.transform.SetAsFirstSibling();
		// 블라인드 이미지를 설정한다 }
	}

	/** 애니메이션 상태를 리셋한다 */
	public virtual void ResetAniState() {
		m_oAni?.Kill();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();

		try {
			this.ResetAniState();
		} catch(System.Exception oException) {
			Debug.LogWarning($"CPopup.OnDestroy Exception : {oException.Message}");
		}
	}

	/** 팝업을 출력한다 */
	public void Show() {
		this.ResetAniState();
		this.transform.localScale = Vector3.zero;

		m_oAni = this.transform.DOScale(1.0f, 0.25f).SetAutoKill().SetUpdate(true);

		/*
		 * Time.timeScale 은 유니티 상에서 흘러가는 시간의 비율을 조절하는데 사용된다. (즉, Time.deltaTime 은 이전 프레임과 현재 
		 * 프레임 사이에 흘러간 시간 차를 계산 후 최종적으로 Time.timeScale 을 곱해서 프레임 간에 흘러간 시간을 산출한다는 것을 
		 * 알 수 있다.)
		 * 
		 * 따라서, 해당 값을 조절함으로서 게임 플레이를 빠르게 또는 느리게 진행하도록 시간을 제어 할 수 있다.
		 * 
		 * 만약, Time.timeScale 에 영향을 받지 않는 프레임 시간이 필요 할 경우에는 Time.deltaTime 이 아닌 Time.unscaledDeltaTime
		 * 을 활용해야한다. (즉, Time.unscaledDeltaTime 현실 상에 시간과 같은 특정 값이 연산되지 않은 순수한 시간 정보를 필요로 할 
		 * 때 사용된다는 것을 알 수 있다.)
		 */
		m_fPrevTimeScale = Time.timeScale;
		Time.timeScale = 0.0f;
	}

	/** 팝업을 닫는다 */
	public void Close() {
		this.ResetAniState();
		this.transform.localScale = Vector3.one;

		m_oAni = this.transform.DOScale(0.0f, 0.25f).SetAutoKill().SetUpdate(true);
		Time.timeScale = m_fPrevTimeScale;
	}
	#endregion // 함수
}
