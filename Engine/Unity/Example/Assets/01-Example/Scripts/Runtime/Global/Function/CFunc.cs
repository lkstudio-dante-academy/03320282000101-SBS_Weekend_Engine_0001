using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

/** 전역 메서드 */
public static partial class CFunc {
	#region 함수
	/** 정수를 로드한다 */
	public static int LoadInt(string a_oKey, int a_nDefVal = 0) {
		/*
		 * PlayerPrefs 클래스를 활용하면 프로그램이 실행 중에 사용되던 데이터를
		 * 파일로 저장하는 것이 가능하다. (즉, 해당 클래스를 활용하면 프로그램이 종료
		 * 된 후 다시 실행이 되었을 때 이전에 사용 중이던 데이터를 다시 재활용 할 수 있다
		 * 는 것을 의미한다.)
		 * 
		 * 단, 해당 클래스는 단순한 데이터만 저장 가능하기 때문에 복잡한 구조를 지니고 있는
		 * 클래스와 같은 데이터를 저장하는데는 적합하지 않다는 단점이 존재한다.
		 * 
		 * 따라서, 클래스와 같은 복잡한 데이터를 저장 및 로드하기 위해서는 C# 에서 제공하는
		 * 파일 시스템을 활용 할 필요가 있다.
		 */
		return PlayerPrefs.GetInt(a_oKey, a_nDefVal);
	}

	/** 실수를 로드한다 */
	public static float LoadReal(string a_oKey, float a_fDefVal = 0.0f) {
		return PlayerPrefs.GetFloat(a_oKey, a_fDefVal);
	}

	/** 문자열을 로드한다 */
	public static string LoadStr(string a_oKey, string a_oDefStr = "") {
		return PlayerPrefs.GetString(a_oKey, a_oDefStr);
	}

	/** 정수를 저장한다 */
	public static void SaveInt(string a_oKey, int a_nVal) {
		PlayerPrefs.SetInt(a_oKey, a_nVal);
	}

	/** 실수를 저장한다 */
	public static void SaveReal(string a_oKey, float a_fVal) {
		PlayerPrefs.SetFloat(a_oKey, a_fVal);
	}

	/** 문자열을 저장한다 */
	public static void SaveStr(string a_oKey, string a_oStr) {
		PlayerPrefs.SetString(a_oKey, a_oStr);
	}

	/** 문자열을 읽어들인다 */
	public static string ReadStr(string a_oFilePath) {
		/*
		 * File.Open 메서드를 활용하면 파일에 데이터를 저장하거나 읽어들일 수 있는
		 * 스트림을 생성하는 것이 가능하다.
		 * 
		 * 또한, 해당 메서드는 스트림을 연결 할 파일이 존재하지 않을 경우 해당 파일을
		 * 생성하는 역할을 같이 수행하기 때문에 데이터를 저장하는 과정에서는 파일이
		 * 없어도 따로 예외 처리를 할 필요가 없다.
		 * 
		 * 단, 해당 메서드가 파일을 생성해주는 경우는 쓰기용으로 스트림 생성을 시도 할
		 * 경우에만 해당하며 읽기용으로 스트림 생성을 시도 할 때는 별도로 파일을 생성하는
		 * 작업을 수행하지 않기 때문에 특정 스트림을 읽기용으로 생성하기 위해서는 반드시
		 * 그 전에 파일의 존재 여부를 검사 할 필요가 있다. (즉, C# 은 파일의 존재 여부를
		 * 검사 할 수 있는 File.Exists 를 제공하기 때문에 해당 메서드를 활용하면 손쉽게
		 * 파일 입력이 가능한지 검사 할 수 있다.)
		 * 
		 * 스트림이란?
		 * - 프로그램과 프로그램 간에 데이터를 주고 받을 수 있는 통로를 의미한다. 단, 해당
		 * 통로는 단방향이기 때문에 데이터 입/출력이 모두 필요한 경우에는 입력용과 출력용
		 * 스트림을 따로 생성 할 필요가 있다.
		 * 
		 * 또한, 스트림은 단순히 프로그램 간에 데이터를 전달 할 수 있을 뿐만 파일 등과 같은
		 * 컴퓨터 하드웨어 가까운 시스템 간에도 데이터를 전달하는 것이 가능하다. (즉, 스트림이
		 * 파일과 연결 되어있으면 파일에 데이터를 입/출력 할 수 있고 소켓에 연결 되어 있으면
		 * 해당 소켓을 통해 다른 네트워크에 연결 되어있는 호스트에 데이터를 전달하거나 가져오는
		 * 것이 가능하다.)
		 * 
		 * C# 은 특정 스트림을 생성 후 해당 스트림이 더이상 필요하지 않을 경우 반드시
		 * Close 메서드를 통해서 해당 스트림을 제거해 줄 필요가 있다. (즉, 일반적으로
		 * C# 은 메모리를 관리해주는 언어지만 몇가지 예외사항이 있으며 그 중 하나가 스트림
		 * 이라는 것을 알 수 있다.)
		 */
		/** 파일이 존재 할 경우 */
		if(File.Exists(a_oFilePath)) {
			/*
			 * using 키워드를 활용하면 스트림을 생성 후 사용자가 명시적으로 해당 스트림을
			 * 제거 할 필요가 없다. (즉, 해당 키워드를 활용하면 C# 컴파일러가 자동으로
			 * 스트림을 제거하기 위한 명령문을 작성해주기 때문에 사용자가 실수로 스트림을
			 * 제거하지 않고 생략해버리는 실수를 방지하는 것이 가능하다.)
			 * 
			 * 단, 해당 키워드를 사용하기 위해서는 반드시 해당 키워드에 의해 관리되는
			 * 객체가 IDispose 인터페이스를 따르고 있어야한다. (즉, 스트림은 IDispose
			 * 인터페이스를 따르고 있기 때문에 using 키워드에 의해 스트림을 관리하는
			 * 것이 가능하다는 것을 알 수 있다.)
			 */
			using(var oRStream = File.Open(a_oFilePath,
				FileMode.Open, FileAccess.Read)) {
				var oBytes = new byte[oRStream.Length];
				oRStream.Read(oBytes, 0, oBytes.Length);

				/*
				 * System.Text.Encoding 클래스를 활용하면 문자열과 바이트 스트림 간에
				 * 엔코딩/디코딩을 손쉽게 처리하는 것이 가능하다. (즉, 컴퓨터는 기본적으로
				 * 숫자만을 인식 할 수 있기 때문에 해당 클래스 내부에는 문자열 테이블에 해당하는
				 * 여러 프로퍼티가 존재하며 해당 프로퍼티를 통해 바이트 스트림을 특정 테이블에
				 * 맞는 문자열 데이터로 변환하는 것이 가능하다는 것을 알 수 있다.)
				 */
				return System.Text.Encoding.Default.GetString(oBytes);
			}
		}

		return string.Empty;
	}

	/** 문자열을 기록한다 */
	public static void WriteStr(string a_oStr, string a_oFilePath) {
		/*
		 * Directory.Exists 메서드는 주어진 경로에 폴더가 존재하는 여부를 검사하는
		 * 역할을 수행한다.
		 * 
		 * C# 파일 시스템에 존재하는 스트림 생성 메서드는 파일을 생성해주는 기능이 지원하지만
		 * 폴더를 생성하는 기능은 지원하지 않기 때문에 만약 특정 경로에 파일을 좀 더 안전하게
		 * 생성하기 위해서는 반드시 해당 경로 사이에 존재하는 폴더가 있는지 검사해 줄 필요가
		 * 있다.
		 * 
		 * 따라서, 파일을 쓰기용으로 스트림을 생성 할 때 폴더를 생성해주는 구문도 같이 작성해
		 * 주는 것을 추천한다. (즉, Directory.CreateDirectory 메서드를 활용하면 폴더를
		 * 손쉽게 생성하는 것이 가능하다는 것을 알 수 있다.)
		 * 
		 * 단, Path.GetDirectoryName 메서드는 폴더 + 파일명으로 이루어진 경로 중에 폴더 경로만을
		 * 반환해주는 역할을 수행하지만 각 폴더를 구분하는 기호는 해당 메서드가 동작하는 플랫폼에
		 * 따라 달라지기 때문에 멀티 플랫폼을 염두해서 프로그램을 작성한다면 해당 메서드가 반환해주는
		 * 경로 구분 기호를 보정해 줄 필요가 있다. (즉, 윈도우즈 플랫폼은 폴더 구분을 \ (역슬래시) 로
		 * 구분하기 때문에 다른 운영체제와 경로 포맷이 다르다는 것을 알 수 있다.)
		 * 
		 * 따라서, 가장 안전하게 해당 메서드를 활용하면 방법은 해당 메서드가 반환해주는 폴더 경로를
		 * 모두 / (슬래시) 기호로 통일시켜주는 것이다.
		 * 
		 * Ex)
		 * Path.GetDirectoryName("A/B/C/Test.txt").Replace("\\", "/")
		 */		
		// 디렉토리가 존재하지 않을 경우
		if(!Directory.Exists(Path.GetDirectoryName(a_oFilePath))) {
			Directory.CreateDirectory(Path.GetDirectoryName(a_oFilePath));
		}

		/*
		 * 주요 FileMode 종류
		 * - Open
		 * - OpenOrCreate
		 * - Create
		 * - Truncate
		 * 
		 * Create vs OpenOrCreate
		 * - 두 파일 모드 모두 파일이 존재하지 않을 경우 새롭게 파일을 생성해주는
		 * 특징이 존재한다. Create 모드는 파일이 이미 존재 할 경우 파일에 존재하는
		 * 데이터를 모두 제거 (Truncate) 하는 반면 OpenOrCreate 모든 기존 데이터
		 * 유지하는 차이점이 존재한다.
		 * 
		 * 따라서, 파일에 존재하는 데이터를 주 기억 장치 (메모리) 에 로드 후 데이터를
		 * 제어하는 구문을 작성 할 때에는 가능하면 Create 모드를 사용하는 것을 추천한다.
		 */
		using(var oWStream = File.Open(a_oFilePath, 
			FileMode.Create, FileAccess.Write)) {
			var oBytes = System.Text.Encoding.Default.GetBytes(a_oStr);
			oWStream.Write(oBytes, 0, oBytes.Length);
		}
	}
	
	/** 팝업을 출력한다 */
	public static void ShowPopup(string a_oName, 
		GameObject a_oParent, string a_oPath, System.Action<CPopup> a_oCallback) {
		CFunc.ShowPopup(a_oName, a_oParent, Resources.Load<GameObject>(a_oPath), a_oCallback);
	}

	/** 팝업을 출력한다 */
	public static void ShowPopup(string a_oName, 
		GameObject a_oParent, GameObject a_oOrigin, System.Action<CPopup> a_oCallback) {
		/*
		 * Debug.Assert 메서드는 입력으로 전달 된 조건이 거짓 일 경우 에러를 발생시키는 역할을 수행한다. (즉, 해당 메서드를 활용하면
		 * 의도하지 않는 데이터가 전달됨에 따라 문제가 발생하는 부분을 검출해내는 것이 가능하다.)
		 * 
		 * 또한, 해당 메서드는 디버그 (개발) 환경에서만 동작하기 때문에 릴리즈 (배포) 환경에서는 해당 메서드는 모두 제외가 된 상태에서
		 * 컴파일이 이루어지는 특징이 있다.
		 */
		Debug.Assert(a_oParent != null && a_oOrigin != null);

		// 팝업이 없을 경우
		if(a_oParent.transform.Find(a_oName) == null) {
			var oPopup = CFactory.CreateCloneObj<CPopup>(a_oName, a_oParent, a_oOrigin);
			a_oCallback?.Invoke(oPopup);

			oPopup.Show();
		}
	}

	/** 알림 팝업을 출력한다 */
	public static void ShowAlertPopup(string a_oMsg,
		System.Action<CAlertPopup, bool> a_oCallback, string a_oCancelBtnText = "취소", GameObject a_oParent = null) {
		var stParams = CAlertPopup.MakeParams("알림", a_oMsg, "확인", a_oCancelBtnText, a_oCallback);

		/*
		 * ?? (널 병합 연산자) 는 참조 데이터를 대상으로 사용 가능한 연산자로서 해당 연산자를 활용하면 특정 참조
		 * 데이터가 null 일 경우를 처리 할 수 있는 구문을 제작하는 것이 가능하다. (즉, 해당 연산자는 좌항 데이터가
		 * null 일 경우 우항에 명시 된 구문을 실행하는 특징이 존재한다는 것을 알 수 있다.)
		 */
		CFunc.ShowPopup("AlertPopup",
				a_oParent ?? CSceneManager.ActiveScenePopupUIs,
				KDefine.G_OBJ_P_ALERT_POPUP,
				(a_oSender) => {
					CFunc.SetupAlertPopup(a_oSender, stParams);
				});
	}

	/** 알림 팝업을 설정한다 */
	private static void SetupAlertPopup(CPopup a_oSender, CAlertPopup.STParams a_stParams) {
		(a_oSender as CAlertPopup).Init(a_stParams);
	}
	#endregion // 함수

	#region 제네릭 함수
	/** 객체를 읽어들인다 */
	public static T ReadObj<T>(string a_oFilePath) where T : CBaseInfo {
		string oJSON = CFunc.ReadStr(a_oFilePath);
		return JsonConvert.DeserializeObject<T>(oJSON);
	}

	/** 객체를 기록한다 */
	public static void WriteObj<T>(T a_tObj, string a_oFilePath) where T : CBaseInfo {
		string oJSON = JsonConvert.SerializeObject(a_tObj);
		CFunc.WriteStr(oJSON, a_oFilePath);
	}
	#endregion // 제네릭 함수
}
