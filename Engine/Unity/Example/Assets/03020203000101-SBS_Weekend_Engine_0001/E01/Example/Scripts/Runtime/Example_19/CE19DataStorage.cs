using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 데이터 저장소 */
public class CE19DataStorage : CSingleton<CE19DataStorage>
{
	#region 프로퍼티
	public int Score { get; set; } = 0;
	#endregion // 프로퍼티

	#region 함수
	/** 상태를 리셋한다 */
	public virtual void Reset()
	{
		this.Score = 0;
	}
	#endregion // 함수
}
