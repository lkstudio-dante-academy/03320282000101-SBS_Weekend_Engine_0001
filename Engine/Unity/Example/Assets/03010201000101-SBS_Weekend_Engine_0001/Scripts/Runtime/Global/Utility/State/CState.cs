using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 상태 */
public partial class CState<T> where T : class
{
	#region 함수
	/** 해당 상태로 변경 되었을 경우 */
	public virtual void OnStateEnter(T a_oOwner)
	{
		// Do Something
	}

	/** 다른 상태로 변경 되었을 경우 */
	public virtual void OnStateExit(T a_oOwner)
	{
		// Do Something
	}

	/** 상태를 갱신한다 */
	public virtual void OnStateUpdate(T a_oOwner, float a_fDeltaTime)
	{
		// Do Something
	}
	#endregion // 함수
}
