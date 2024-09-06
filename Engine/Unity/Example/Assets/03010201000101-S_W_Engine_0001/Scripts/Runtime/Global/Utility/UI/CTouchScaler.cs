using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

/** 터치 비율 처리자 */
public partial class CTouchScaler : CComponent, IPointerUpHandler, IPointerDownHandler
{
	#region 변수
	private Vector3 m_stOriginScale = Vector3.one;
	private Tween m_oAni = null;

	[SerializeField] private Vector3 m_stScaleRate = new Vector3(1.05f, 1.05f, 1.05f);
	#endregion // 변수

	#region IPointerUpHandler
	/** 터치가 종료 되었을 경우 */
	public void OnPointerUp(PointerEventData a_oEventData)
	{
		this.ResetAniState();
		m_oAni = this.transform.DOScale(m_stOriginScale.ExGetMaxVal(), 0.25f).SetAutoKill();
	}
	#endregion // IPointerUpHandler

	#region IPointerDownHandler
	/** 터치가 시작 되었을 경우 */
	public void OnPointerDown(PointerEventData a_oEventData)
	{
		this.ResetAniState();
		m_oAni = this.transform.DOScale(m_stOriginScale * m_stScaleRate.ExGetMaxVal(), 0.25f).SetAutoKill();
	}
	#endregion // IPointerDownHandler

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
		this.SetOriginScale(this.transform.localScale);
	}

	/** 애니메이션 상태를 리셋한다 */
	public virtual void ResetAniState()
	{
		m_oAni?.Kill();
	}

	/** 제거 되었을 경우 */
	public override void OnDestroy()
	{
		base.OnDestroy();

		try
		{
			this.ResetAniState();
		}
		catch(System.Exception oException)
		{
			Debug.LogWarning($"CTouchScaler.OnDestroy Exception : {oException.Message}");
		}
	}
	#endregion // 함수
}

/** 터치 비율 처리자 - 접근 */
public partial class CTouchScaler : CComponent, IPointerUpHandler, IPointerDownHandler
{
	#region 함수
	/** 원본 비율을 변경한다 */
	public void SetOriginScale(Vector3 a_stScale)
	{
		m_stOriginScale = a_stScale;
	}
	#endregion // 함수
}
