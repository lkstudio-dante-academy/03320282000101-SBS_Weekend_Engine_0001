using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/** 점수 텍스트 */
public class CE14ScoreText : CComponent {
	#region 변수
	private Tween m_oAni = null;
	private TMP_Text m_oTMPText = null;
	#endregion // 변수

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		m_oTMPText = this.GetComponent<TMP_Text>();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy() {
		base.OnDestroy();
		m_oAni?.Kill();
	}

	/** 점수 애니메이션을 시작한다 */
	public void StartScoreAni(int a_nScore) {
		m_oTMPText.text = string.Format("{0}{1}", (a_nScore < 0) ? string.Empty : "+", a_nScore);
		m_oTMPText.color = (a_nScore < 0) ? Color.red : Color.white;

		var stPos = this.transform.localPosition;

		var oSequence = DOTween.Sequence().SetAutoKill().SetEase(Ease.Linear);
		oSequence.Append(this.transform.DOLocalMoveY(stPos.y + 50.0f, 0.5f));
		oSequence.AppendCallback(this.OnCompleteScoreAni);

		m_oAni = oSequence;
	}

	/** 점수 애니메이션이 완료 되었을 경우 */
	private void OnCompleteScoreAni() {
		m_oAni?.Kill();
		Destroy(this.gameObject);
	}
	#endregion // 함수
}
