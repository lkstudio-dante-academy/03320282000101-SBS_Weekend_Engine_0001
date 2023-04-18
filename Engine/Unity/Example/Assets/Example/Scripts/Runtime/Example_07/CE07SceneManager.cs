using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/** Example 7 */
public class CE07SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private TMP_Text m_oScoreText = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E07;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();
		this.UpdateUIsState();
	}

	/** 다시하기 버튼을 눌렀을 경우 */
	public void OnTouchRetryBtn() {
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E06);
	}

	/** 그만하기 버튼을 눌렀을 경우 */
	public void OnTouchLeaveBtn() {
		CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E05);
	}

	/** UI 상태를 갱신한다 */
	private void UpdateUIsState() {
		/*
		 * 유니티는 씬 간에 데이터를 공유하기 위해서는 일반적으로 저장소 역할을 하는 객체를 생성하는 방법을 사용한다.
		 * (즉, 씬이 전환 될 때 일반적인 객체는 모두 제거가 되어버리기 때문에 만약 특정 씬 간에 서로 데이터를 공유 할 필요가 있을
		 * 경우 씬이 전환되도 제거 되지 않는 객체가 필요하다는 것을 알 수 있다.)
		 * 
		 * 따라서, 데이터의 저장소 역할을 하는 싱글턴 객체 등을 활용하면 씬 간에 데이터를 손쉽게 공유하는 것이 가능하다.
		 */
		m_oScoreText.text = $"Score : {CE05DataStorage.Instance.Score}";
	}
	#endregion // 함수
}
