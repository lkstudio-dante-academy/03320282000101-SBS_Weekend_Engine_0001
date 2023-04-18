using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** Example 10 */
public class CE10SceneManager : CSceneManager {
	#region 변수
	[SerializeField] private Button m_oEquipBtn = null;
	[SerializeField] private Button m_oStoreBtn = null;
	[SerializeField] private Button m_oInventoryBtn = null;
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E10;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake() {
		base.Awake();

		m_oEquipBtn.onClick.AddListener(this.OnTouchEquipBtn);
		m_oStoreBtn.onClick.AddListener(this.OnTouchStoreBtn);
		m_oInventoryBtn.onClick.AddListener(this.OnTouchInventoryBtn);
	}

	/** 장비 버튼을 눌렀을 경우 */
	private void OnTouchEquipBtn() {
		CFunc.ShowPopup("EquipPopup", this.PopupUIs, "Example_09/E09EquipPoup", (a_oSender) => {
			// Do Something
		});
	}

	/** 상점 버튼을 눌렀을 경우 */
	private void OnTouchStoreBtn() {
		CFunc.ShowPopup("StorePopup", this.PopupUIs, "Example_09/E09StorePoup", (a_oSender) => {
			// Do Something
		});
	}

	/** 인벤토리 버튼을 눌렀을 경우 */
	private void OnTouchInventoryBtn() {
		CFunc.ShowPopup("InventoryPopup", this.PopupUIs, "Example_09/E09InventoryPoup", (a_oSender) => {
			// Do Something
		});
	}
	#endregion // 함수
}
