//#define E12_TWEEN
#define E12_LEGACY
#define E12_MECANIM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * 트윈 애니메이션이란?
 * - 현재 상태와 변화하기 위한 상태 간에 보간을 통해서 애니메이션 효과를 만들어내는 것을
 * 의미한다. (즉, 트윈 애니메이션을 활용하면 단순한 애니메이션을 효과적으로 처리 할 수 있지만
 * 복잡한 애니메이션을 연출하는데에는 적합하지 않다.)
 * 
 * 레거시 애니메이션이란?
 * - 키 프레임 애니메이션을 의미하면 트윈 애니메이션과 같이 특정 상태 간에 보간을 통해서
 * 애니메이션 효과를 연출하는 방법을 의미한다. 단, 레거시 애니메이션은 복잡한 연출이라고
 * 하더라도 트윈 애니메이션에 비해 좀 더 수월하게 상태 변화를 설정 할 수 있기 때문에 현재까지도
 * 많이 활용되는 애니메이션 처리 방식이다.
 * 
 * 메카님 애니메이션이란?
 * - 유니티가 제공하는 FSM (Finite State Machine) 을 기반으로 특정 대상의 상태에 따라
 * 애니메이션을 전환해서 효과를 연출하는 방식을 의미한다. (즉, 메카님 시스템 자체는 애니메이션이
 * 아니지만 애니메이션 효과를 연출하는데 활용하는 것이 가능하다.)
 */
/** Example 12 */
public class CE12SceneManager : CSceneManager {
	#region 변수
#if E12_TWEEN
	private Vector3 m_stOriginPos = Vector3.zero;
	private Vector3 m_stOriginScale = Vector3.one;
	private Vector3 m_stOriginRoate = Vector3.zero;

	private Sequence m_oSequence01 = null;
	private Sequence m_oSequence02 = null;
#endif // #if E12_TWEEN

	[SerializeField] private GameObject m_oTweenTarget01 = null;
	[SerializeField] private GameObject m_oTweenTarget02 = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E12;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

#if E12_TWEEN
		m_stOriginScale = m_oTweenTarget01.transform.localScale;

		m_stOriginPos = m_oTweenTarget02.transform.localPosition;
		m_stOriginRoate = m_oTweenTarget02.transform.localEulerAngles;
#endif // #if E12_TWEEN
	}

	/** 상태를 갱신한다 */
	public override void Update() {
		base.Update();

#if E12_TWEEN
		// 스페이스 키를 눌렀을 경우
		if(Input.GetKeyDown(KeyCode.Space)) {
			m_oTweenTarget01.transform.localScale = m_stOriginScale;

			m_oTweenTarget02.transform.localPosition = m_stOriginPos;
			m_oTweenTarget02.transform.localEulerAngles = m_stOriginRoate;

			/*
			 * DOTween 사용해서 트윈 애니메이션을 처리할 때 동일한 대상에 애니메이션이
			 * 중첩으로 적용 되는 현상을 해결하기 위해서는 이전에 적용 된 애니메이션을
			 * Kill 메서드를 사용해서 제거해 줄 필요가 있다.
			 */
			m_oSequence01?.Kill();
			m_oSequence02?.Kill();

			/*
			 * Sequence 를 활용하면 여러 트윈 애니메이션을 순차적으로 적용하거나 동시에
			 * 적용하는 것이 가능하다. (즉, Append 메서드는 애니메이션 순차적으로 적용하는데
			 * 활용되며 Join 메서드는 애니메이션을 동시에 적용하는데 활용된다는 것을 알 수
			 * 있다.)
			 */
			m_oSequence01 = DOTween.Sequence().SetAutoKill();
			m_oSequence01.Append(m_oTweenTarget01.transform.DOScale(m_stOriginScale * 1.25f, 0.25f));
			m_oSequence01.Append(m_oTweenTarget01.transform.DOScale(m_stOriginScale, 0.25f));

			m_oSequence02 = DOTween.Sequence().SetAutoKill();
			m_oSequence02.Join(m_oTweenTarget02.transform.DOMove(m_stOriginPos + new Vector3(250.0f, 0.0f, 0.0f), 1.0f));
			m_oSequence02.Join(m_oTweenTarget02.transform.DORotate(new Vector3(0.0f, 0.0f, -90.0f), 1.0f));
		}
#elif E12_LEGACY

#elif E12_MECANIM

#endif // #if E12_TWEEN
	}
	#endregion // 함수
}
