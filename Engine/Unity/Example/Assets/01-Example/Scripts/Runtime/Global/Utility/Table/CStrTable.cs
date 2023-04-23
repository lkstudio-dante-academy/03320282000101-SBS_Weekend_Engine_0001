using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * using 키워드를 활용하면 특정 자료형에 새로운 이름을 부여하는 것이 가능하다.
 * (즉, C/C++ 의 typedef 키워드와 유사한 역할을 수행한다.)
 * 
 * 따라서, 복잡한 컬렉션은 여러 자료형을 명시함으로서 자료형의 구조가 길어지는 단점이 존재하지만 using 키워드를
 * 활용하면 복잡한 구조의 컬렉션 자료형을 특정 이름으로 축약해서 사용하는 것이 가능하다.
 */
using StrDict = System.Collections.Generic.Dictionary<string, string>;
using StrDictContainer = System.Collections.Generic.Dictionary<UnityEngine.SystemLanguage, System.Collections.Generic.Dictionary<string, string>>;

/** 문자열 테이블 */
public class CStrTable : CSingleton<CStrTable> {
	private StrDictContainer m_oStrDictContainer = new StrDictContainer();

	#region 함수
	/** 문자열을 반환한다 */
	public string GetStr(string a_oKey) {
		/*
		 * TryGetValue 메서드는 Dictionary 컬렉션으로부터 키에 해당하는 값을 가져오는 역할을 수행한다.
		 * (즉, 해당 메서드는 활용하면 좀더 안전하게 Dictionary 컬렉션으로부터 값을 가져오는 것이 가능하다.)
		 * 
		 * Dictionary 컬렉션은 존재하지 않는 키를 [ ] (인덱스 연산자) 명시 할 경우 내부적으로 예외가 발생하기
		 * 때문에 해당 예외를 처리해주지 않으면 프로그램이 원치 않는 동작을 수행한다.
		 * 
		 * 따라서, 컬렉션에서 특정 데이터를 가져오기 위해서는 반드시 그 전에 특정 키에 해당하는 값이 존재하는
		 * 존재 여부를 검사해 줄 필요가 있다.
		 */
		// 문자열 테이블이 존재 할 경우
		if(m_oStrDictContainer.TryGetValue(Application.systemLanguage, out StrDict oStrTable)) {
			/*
			 * GetValueOrDefault 메서드는 키에 해당하는 값을 가져오는 역할을 수행한다. 만약, 명시 된 키에 해당하는
			 * 값이 존재하지 않을 경우에는 2 번째 인자로 입력한 기본 값이 반환 된다는 특징이 있다.
			 */
			return oStrTable.GetValueOrDefault(a_oKey, a_oKey);
		}

		return a_oKey;
	}

	/** 문자열을 추가한다 */
	public void AddStr(string a_oKey, string a_oStr) {
		// 문자열 테이블이 존재 할 경우
		if(m_oStrDictContainer.TryGetValue(Application.systemLanguage, out StrDict oStrTable)) {
			/*
			 * TryAdd 메서드는 Dictionary 컬렉션에 데이터를 추가하는 역할을 수행한다. 단, 일반적인 Add 메서드와
			 * 달리 해당 메서드는 이미 동일한 키를 지니는 데이터가 컬렉션에 존재 할 경우 데이터를 추가하는 과정을
			 * 생략한다. (즉, 일반적인 Add 메서드는 이미 존재하는 키에 해당하는 새로운 데이터를 추가 할 경우
			 * 내부적으로 예외가 발생하기 때문에 반드시 데이터를 추가하기 전에 해당 데이터의 존재 여부를 검사해 줄
			 * 필요가 있다.)
			 * 
			 * 따라서, TryAdd 메서드를 활용하면 좀더 안전하게 Dictionary 컬렉션에 데이터를 추가하는 것이 가능하다.
			 */
			oStrTable.TryAdd(a_oKey, a_oStr);
		}
	}

	/** 문자열을 로드한다 */
	public void LoadStrs(string a_oFilePath) {
		var oTextAsset = CResManager.Instance.GetRes<TextAsset>(a_oFilePath);
		var oStrs = oTextAsset.text.Split("\n");

		// 문자열이 존재 할 경우
		if(oStrs.Length >= 2) {
			var oHeaders = oStrs[0].Split(",");

			for(int i = 1; i < oStrs.Length; ++i) {
				var oTokens = oStrs[i].Split(",");

				for(int j = 2; j < oTokens.Length; ++j) {
					var eLanguage = (SystemLanguage)System.Enum.Parse(typeof(SystemLanguage), oHeaders[j]);

					var oStrDict = m_oStrDictContainer.GetValueOrDefault(eLanguage);
					oStrDict = oStrDict ?? new StrDict();

					oStrDict.TryAdd(oTokens[0], oTokens[j]);
					m_oStrDictContainer.TryAdd(eLanguage, oStrDict);
				}
			}
		}
	}
	#endregion // 함수
}
