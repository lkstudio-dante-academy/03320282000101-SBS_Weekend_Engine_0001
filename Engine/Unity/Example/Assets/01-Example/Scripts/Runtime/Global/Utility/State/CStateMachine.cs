using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 상태 머신 */
public partial class CStateMachine<T> where T : class {
	#region 프로퍼티
	public T Owner { get; private set; } = null;
	public CState<T> CurState { get; private set; } = null;
	public CState<T> PrevState { get; private set; } = null;
	#endregion // 프로퍼티

	#region 함수
	/** 소유자를 변경한다 */
	public void SetOwner(T a_oOwner) {
		this.Owner = a_oOwner;
	}

	/** 상태를 변경한다 */
	public void SetState(CState<T> a_oState) {
		// 상태 변경이 가능 할 경우
		if(this.CurState == null || this.CurState != a_oState) {
			this.PrevState = this.CurState;
			this.PrevState?.OnStateExit(this.Owner);

			this.CurState = a_oState;
			this.CurState?.OnStateEnter(this.Owner);
		}
	}

	/** 상태를 갱신한다 */
	public void Update(float a_fDeltaTime) {
		this.CurState?.OnStateUpdate(this.Owner, a_fDeltaTime);
	}
	#endregion // 함수
}
