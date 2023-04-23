using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ResDict = System.Collections.Generic.Dictionary<string, UnityEngine.Object>;
using ResLoaderDict = System.Collections.Generic.Dictionary<System.Type, System.Func<string, UnityEngine.Object>>;
using ResDictContainer = System.Collections.Generic.Dictionary<System.Type, System.Collections.Generic.Dictionary<string, UnityEngine.Object>>;

/** 리소스 관리자 */
public class CResManager : CSingleton<CResManager> {
	#region 변수
	private ResLoaderDict m_oResLoaderDict = new ResLoaderDict() {
		[typeof(Shader)] = Shader.Find,
		[typeof(Sprite)] = Resources.Load<Sprite>,
		[typeof(Texture)] = Resources.Load<Texture>,
		[typeof(Material)] = Resources.Load<Material>,
		[typeof(TextAsset)] = Resources.Load<TextAsset>,
		[typeof(GameObject)] = Resources.Load<GameObject>
	};

	private ResDictContainer m_oResDictContainer = new ResDictContainer();
	#endregion // 변수

	#region 제네릭 함수
	/** 리소스를 추가한다 */
	public void AddRes<T>(string a_oKey, T a_tRes) where T : Object {
		// 리소스 테이블이 존재 할 경우
		if(m_oResDictContainer.TryGetValue(typeof(T), out ResDict oResDict)) {
			oResDict.TryAdd(a_oKey, a_tRes);
		}
	}

	/** 리소스 로더를 추가한다 */
	public void AddResLoader<T>(System.Func<string, Object> a_oLoader) {
		m_oResLoaderDict.TryAdd(typeof(T), a_oLoader);
	}
	
	/** 리소스를 반환한다 */
	public T GetRes<T>(string a_oKey) where T : Object {
		var oResDict = m_oResDictContainer.GetValueOrDefault(typeof(T));
		oResDict = oResDict ?? new ResDict();

		var oResLoader = m_oResLoaderDict.GetValueOrDefault(typeof(T));
		oResLoader = oResLoader ?? Resources.Load<T>;

		m_oResLoaderDict.TryAdd(typeof(T), oResLoader);
		m_oResDictContainer.TryAdd(typeof(T), oResDict);

		// 리소스가 없을 경우
		if(!oResDict.ContainsKey(a_oKey) || oResDict[a_oKey] == null) {
			oResDict.ExReplaceVal(a_oKey, oResLoader(a_oKey));
		}

		return oResDict[a_oKey] as T;
	}
	#endregion // 제네릭 함수
}
