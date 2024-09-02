using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 9 */
public class CE01Example_09 : CSceneManager
{
	/** 서버 UI */
	[System.Serializable]
	private struct STServerUIs
	{
		public GameObject m_oRootUIs;

		[HideInInspector] public Text m_oStateText;
		[HideInInspector] public Button m_oEnterBtn;
	}

	#region 변수
	[SerializeField] private List<STServerUIs> m_oServerUIsList = new List<STServerUIs>();
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E09;
	#endregion // 프로퍼티

	public class CArray
	{
		public List<int> m_oValList = new List<int>();

		public int this[int a_nIdx]
		{
			get
			{
				return m_oValList[a_nIdx];
			}
			set
			{
				m_oValList[a_nIdx] = value;
			}
		}
	}

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();

		for(int i = 0; i < m_oServerUIsList.Count; ++i)
		{
			int nIdx = i;

			/*
			 * 특정 컬렉션이 구조체와 같은 값 형식 자료형을 관리하고 있을 경우 인덱스 연산자를 활용할 때 주의가
			 * 필요하다. (즉, 인덱스 연산자를 내부적으로 인덱서라고 하는 특별한 이름을 지닌 메서드를 호출하기
			 * 때문에 해당 연산자의 결과는 값 형식 일 경우 원본이 아닌 사본이 반환 된다는 것을 알 수 있다.)
			 * 
			 * 따라서, 값 형식 데이터를 관리하는 컬렉션에 경우에는 해당 요소 하위에 존재하는 데이터를 변경하기
			 * 위해서는 새로운 데이터를 생성해줘야한다. 또는, 사본 요소를 가져온 후 하위에 존재하는 데이터를
			 * 변경하고 나서 사본 데이터를 다시 원본 데이터에 할당해줘야한다. 
			 */
			m_oServerUIsList[i] = new STServerUIs()
			{
				m_oStateText = m_oServerUIsList[i].m_oRootUIs.GetComponentInChildren<Text>(),
				m_oEnterBtn = m_oServerUIsList[i].m_oRootUIs.GetComponentInChildren<Button>(),
				m_oRootUIs = m_oServerUIsList[i].m_oRootUIs
			};

			m_oServerUIsList[i].m_oStateText.text = Random.Range(0, 2) <= 0 ? "혼잡" : "원활";

			m_oServerUIsList[i].m_oStateText.color = m_oServerUIsList[i].m_oStateText.text.Equals("혼잡") ?
				Color.red : Color.green;

			m_oServerUIsList[i].m_oEnterBtn.onClick.AddListener(() =>
			{
				this.OnTouchEnterBtn(nIdx);
			});
		}
	}

	/** 입장 버튼을 눌렀을 경우 */
	private void OnTouchEnterBtn(int a_nIdx)
	{
		// 혼잡한 서버 일 경우
		if(m_oServerUIsList[a_nIdx].m_oStateText.text.Equals("혼잡"))
		{
			CFunc.ShowAlertPopup("입장이 불가능합니다.", null, string.Empty);
		}
		else
		{
			CSceneLoader.Instance.LoadScene(KDefine.G_SCENE_N_E10);
		}
	}
	#endregion // 함수
}
