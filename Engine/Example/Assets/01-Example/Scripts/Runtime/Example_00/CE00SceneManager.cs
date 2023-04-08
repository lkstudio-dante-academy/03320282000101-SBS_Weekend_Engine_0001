using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** Example 0 */
public class CE00SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private GameObject m_oContents = null;
	[SerializeField] private GameObject m_oOriginBtn = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E00;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		/*
		 * UnityEngine.SceneManagement 하위에 존재하는 SceneManager 클래스는 현재
		 * 로드가 완료 된 씬을 대상으로 정보를 가져 올 수 있는 여러 메서드를 제공한다.
		 * 
		 * (즉, 로드가 아직 되지 않는 씬은 해당 클래스를 통해 정보를 가져오는 것이
		 * 불가능하기 때문에 로드가 되지 않은 씬은 SceneUtility 클래스를 활용해야한다.)
		 */
		for(int i = 1; i < SceneManager.sceneCountInBuildSettings; ++i) {
			var oBtn = CFactory.CreateCloneObj<Button>("Btn",
				m_oContents, m_oOriginBtn);

			string oScenePath = SceneUtility.GetScenePathByBuildIndex(i);

			var oText = oBtn.GetComponentInChildren<Text>();
			oText.text = Path.GetFileNameWithoutExtension(oScenePath);

			/*
			 * 람다 내부에서 해당 람다 구현 된 외부에 존재하는 변수에 접근 가능했던
			 * 원리는 C# 컴파일러가 해당 변수를 접근 할 수 있도록 별도의 클래스를 생성
			 * 후 외부에 존재하는 변수를 클래스 내부에 복사를 하고 있었기 때문이다.
			 * 
			 * 따라서, for 문과 같은 반복문 내부에서 람다를 사용 할 경우에는 해당 특징을
			 * 파악하고 있어야 람다 내부에서 올바른 데이터를 사용하는 것이 가능하다.
			 * 
			 * (즉, 람다 내부에서 외부에 존재하는 변수를 좀 더 안전하게 사용하고 싶다면
			 * 해당 변수를 람다와 가장 가까운 지역 변수에다가 데이터를 복사 후 해당 지역
			 * 변수를 사용하면 된다.)
			 */
			int nIdx = i;
			oBtn.onClick.AddListener(() => this.OnTouchBtn(nIdx));
		}
	}

	/** 버튼이 눌렸을 경우 */
	private void OnTouchBtn(int a_nIdx) {
		CSceneLoader.Instance.LoadScene(SceneUtility.GetScenePathByBuildIndex(a_nIdx));
	}
	#endregion // 함수
}
