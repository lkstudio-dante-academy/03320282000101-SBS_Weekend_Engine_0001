using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 오브젝트 풀링이란?
 * - 특정 객체를 생성하고 제거하는 과정은 많은 부하를 일으킬 수 있기 때문에 가능하면 해당 과정에서
 * 발생하는 성능 저하를 최소화 시킬 필요가 있다. 
 * 
 * 오브젝트 풀링은 객체 생성 및 제거 과정에서 발생하는 부하를 줄일 수 있는 방법으로서 특정 객체를
 * 생성 후 더이상 필요없으면 해당 객체를 제거하는 것이 아니라 비활성화 후 다시 새로운 객체가 필요한
 * 시점이 되었을때 비활성화 시킨 객체를 재활용함으로서 객체 생성과 관련 된 부하를 줄이는 것이 가능하다.
 */
/** 게임 객체 풀 관리자 */
public class CGameObjPoolManager : CComponent
{
	#region 변수
	private Dictionary<string, Queue<GameObject>> m_oObjDictContainer = new Dictionary<string, Queue<GameObject>>();
	#endregion // 변수

	#region 함수
	/** 객체를 활성한다 */
	public GameObject SpawnGameObj(string a_oName,
		string a_oObjPath, GameObject a_oParent = null)
	{
		return this.SpawnGameObj(a_oName,
			a_oObjPath, Vector3.zero, Vector3.one, Vector3.zero, a_oParent);
	}

	/** 객체를 활성한다 */
	public GameObject SpawnGameObj(string a_oName,
		string a_oObjPath, Vector3 a_stPos, Vector3 a_stScale, Vector3 a_stAngle,
		GameObject a_oParent = null)
	{
		var oQueue = m_oObjDictContainer.GetValueOrDefault(a_oObjPath) ??
			new Queue<GameObject>();

		var oGameObj = (oQueue.Count >= 1) ? oQueue.Dequeue() :
			CFactory.CreateCloneObj(a_oName, a_oParent, a_oObjPath);

		oGameObj.transform.localScale = a_stScale;
		oGameObj.transform.localPosition = a_stPos;
		oGameObj.transform.localEulerAngles = a_stAngle;

		oGameObj.SetActive(true);
		m_oObjDictContainer.TryAdd(a_oObjPath, oQueue);

		return oGameObj;
	}

	/** 객체를 비활성한다 */
	public void DespawnGameObj(string a_oObjPath, GameObject a_oGameObj)
	{
		// 큐가 존재 할 경우
		if(m_oObjDictContainer.TryGetValue(a_oObjPath, out Queue<GameObject> a_oObjQueue))
		{
			a_oGameObj.SetActive(false);
			a_oObjQueue.Enqueue(a_oGameObj);
		}
	}
	#endregion // 함수
}
