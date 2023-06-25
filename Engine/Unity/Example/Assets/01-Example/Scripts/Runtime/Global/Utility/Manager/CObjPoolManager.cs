using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 객체 풀 관리자 */
public class CObjPoolManager : CSingleton<CObjPoolManager> {
	#region 변수
	private Dictionary<System.Type, Queue<object>> m_oObjDictContainer = new Dictionary<System.Type, Queue<object>>();
	#endregion // 변수

	#region 함수
	/** 객체를 활성한다 */
	public T SpawnObj<T>() where T : class, new() {
		var oQueue = m_oObjDictContainer.GetValueOrDefault(typeof(T)) ??
			new Queue<object>();

		var oObj = (oQueue.Count >= 1) ? oQueue.Dequeue() : new T();

		m_oObjDictContainer.TryAdd(typeof(T), oQueue);
		return oObj as T;
	}

	/** 객체를 비활성한다 */
	public void DespawnObj<T>(T a_oObj) where T : class, new() {
		// 큐가 존재 할 경우
		if(m_oObjDictContainer.TryGetValue(typeof(T), out Queue<object> oQueue)) {
			oQueue.Enqueue(a_oObj);
		}
	}
	#endregion // 함수
}
