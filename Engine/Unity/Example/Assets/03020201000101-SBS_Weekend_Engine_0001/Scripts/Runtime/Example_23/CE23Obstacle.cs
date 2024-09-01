using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/** 장애물 */
public class CE23Obstacle : CComponent
{
	#region 변수
	[SerializeField] private TMP_Text m_oHitText = null;
	#endregion // 변수

	#region 프로퍼티
	public int HitCount { get; private set; } = 0;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public void Init(int a_nHitCount)
	{
		this.HitCount = a_nHitCount;
	}

	/** 상태를 갱신한다 */
	public void UpdateState()
	{
		m_oHitText.text = $"{this.HitCount}";
	}

	/** 충돌했을 경우 */
	public void OnHit(System.Action<CE23Obstacle> a_oCallback)
	{
		this.HitCount = Mathf.Max(0, this.HitCount - 1);

		// 제거 되었을 경우
		if(this.HitCount <= 0)
		{
			a_oCallback?.Invoke(this);
			this.gameObject.SetActive(false);
		}
	}
	#endregion // 함수
}
