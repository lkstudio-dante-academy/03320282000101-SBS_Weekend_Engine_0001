using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;

/*
 * partial 키워드란?
 * - 클래스 또는 구조체를 여러 파일에 나누어서 작성 할 수 있게 해주는 키워드를 의미한다. (즉, C# 은 기본적으로 메서드 선언이 존재
 * 하지 않기 때문에 특정 클래스에 많은 메서드가 존재 할 경우 명령문 라인이 길어지는 단점이 존재한다.)
 * 
 * 따라서, 해당 키워드를 활용하면 특정 분류에 따라 하나의 클래스 여러 파일에 나누어서 작성함으로서 코드 관리를 좀 더 수월하게 할 수
 * 있다는 장점이 존재한다.
 * 
 * 정적 클래스란?
 * - 객체화 시킬 수 없는 클래스를 의미하며 해당 클래스에는 클래스 변수와 같은 클래스에 종속되는 맴버만 추가하는 것이 가능하다.
 * 또한, 클래스 맴버만 사용 할 수 있기 때문에 다른 클래스 의해서 상속되는 기능 또한 지원하지 않는다.
 * 
 * 즉, 정적 클래스는 상속하는 것이 불가능하다.
 * 
 * InitializeOnLoad 속성이란?
 * - C# 스크립트 사용 완료 되었을 경우 에디터 상에서 초기화 구문이 동작하도록 해주는 속성을 의미한다. (즉, 에디터 스크립트는 별도로
 * 초기화하기 위한 Awake 등의 메서드가 따로 존재하지 않기 때문에 해당 속성을 활용해서 에디터 상에서 정적 생성자가 실행되도록 지정
 * 해 줄 필요가 있다.)
 * 
 * 따라서, 해당 속성을 지정 된 클래스는 C# 컴파일이 완료되는 순간 생성자가 호출 된다는 것을 알 수 있다.
 * (단, 일반적인 생성자는 객체가 생성 될 때만 호출되기 때문에 해당 상황에서는 일반적인 생성자가 아닌 정적 생성자를 활용해야한다.)
 */
/** 씬 추가자 */
[InitializeOnLoad]
public static partial class CSceneImporter {
	#region 클래스 함수
	/*
	 * 클래스 생성자란?
	 * - 해당 클래스가 사용 완료 되었을 경우 호출되는 생성자를 의미하며 일반적으로 정적 클래스를 사용하기 전에 초기화 시키는 역할로
	 * 활용된다.
	 */
	/** 클래스 생성자 */
	static CSceneImporter() {
		// 플레이 모드가 아닐 경우
		if(!EditorApplication.isPlaying) {
			EditorApplication.projectChanged -= CSceneImporter.OnChangeProjectState;
			EditorApplication.projectChanged += CSceneImporter.OnChangeProjectState;
		}
	}

	/** 프로젝트 상태가 변경 되었을 경우 */
	private static void OnChangeProjectState() {
		/*
		 * GUID (Global Unique ID) 란?
		 * - 유니티 엔진은 Assets 폴더 하위 존재하는 모든 에셋을 구분 할 수 있게 별도의 식별자가 존재하며 해당 식별자를 GUID 라고
		 * 한다.
		 * 
		 * 따라서, GUID 를 활용하면 특정 에셋을 검색하거나 해당 에셋 활용해서 씬에 배치하는 등의 행위를 하는 것이 가능하다.
		 * 
		 * 또한, 일반적으로 유니티 에셋은 AssetDatabase 클래스가 관리하기 때문에 해당 클래스를 활용하면 에셋이 추가되기 전에
		 * 여러 설정을 자동으로 처리하는 것이 가능하다. (즉, 자동화 기능을 구현하는 것이 가능하다는 것을 의미한다.)
		 */
		var oAssetGUIDs = AssetDatabase.FindAssets("Example_", new string[] {
			"Assets/Example/Scenes"
		});

		var oEditorSceneList = new List<EditorBuildSettingsScene>();

		for(int i = 0; i < oAssetGUIDs.Length; ++i) {
			string oAssetPath = AssetDatabase.GUIDToAssetPath(oAssetGUIDs[i]);

			/*
			 * Path 클래스는 경로와 관련 된 여러 기능을 지니고 있는 클래스로서 해당 클래스를 활용하면 특정 경로에 디렉토리를 생성하거나
			 * 파일 확장자 등을 가져와서 관련 된 처리를 수행하는 것이 가능하다.
			 * 
			 * 단, 일반적으로 운영체제마다 경로를 구분하는 기호 (심볼) 이 다르기 때문에 특정 플랫폼에 맞춰서 경로를 설정하거나 C# 자체는
			 * / 를 활용해서 경로를 구분하기 때문에 멀티 플랫폼 상에서 구동되는 프로그램을 제작하기 위해서는 / 기호 경로를 처리해주는 것이
			 * 좋다.
			 * 
			 * 또한, 해당 클래스 특정 메서드는 운영체제에 종속적인 기호 경로를 반환하기 때문에 해당 경우는 경로를 처리하기 전에 \\ (역 슬래시)
			 * 기호를 모두 / 기호 변경해주는 것을 추천한다. (즉, string 클래스의 replace 메서드를 활용하는 것을 추천한다.)
			 * 
			 * Ex)
			 * string oPath = "A\\B\\C";
			 * string oRepalcePath = oPath.Replace("\\", "/")
			 * 
			 * 위의 경우 역 슬래시를 모두 슬래시로 바꾸는 명령문이라는 것을 알 수 있다.
			 */
			// 씬 에셋 일 경우
			if(Path.GetExtension(oAssetPath).Contains("unity")) {
				oEditorSceneList.Add(new EditorBuildSettingsScene(oAssetPath, true));
			}
		}

		EditorBuildSettings.scenes = oEditorSceneList.ToArray();
	}
	#endregion // 클래스 함수
}
#endif // #if UNITY_EDITOR
