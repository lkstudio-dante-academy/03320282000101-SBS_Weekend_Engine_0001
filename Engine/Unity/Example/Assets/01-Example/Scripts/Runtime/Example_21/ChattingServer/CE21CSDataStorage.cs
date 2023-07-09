using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 채팅 서버 데이터 저장소 */
public class CE21CSDataStorage : CSingleton<CE21CSDataStorage> {
	#region 프로퍼티
	public Queue<string> ReadMsgQueue { get; } = new Queue<string>();
	public Queue<string> WriteMsgQueue { get; } = new Queue<string>();
	#endregion // 프로퍼티

	#region 함수
	/** 상태를 리셋한다 */
	public virtual void Reset () {
		this.ReadMsgQueue.Clear();
		this.WriteMsgQueue.Clear();
	}
	#endregion // 함수
}
