using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 마우스 버튼 */
public enum EMouseBtn {
	NONE = -1,
	LEFT,
	RIGHT,
	MIDDLE,
	[HideInInspector] MAX_VAL
}

/** 정렬 순서 정보 */
public struct STSortingOrderInfo {
	public int m_nOrder;
	public string m_oLayer;
}

/** 전역 상수 */
public static partial class KDefine {
	#region 기본
	// 기타
	public const int G_COMPARE_LESS = -1;
	public const int G_COMPARE_EQUALS = 0;
	public const int G_COMPARE_GREATE = 1;

	// 경로
	public const string G_OBJ_P_ALERT_POPUP = "Global/Prefabs/G_AlertPopup";

	// 씬 이름
	public const string G_SCENE_N_E00 = "Example_00 (메뉴)";
	public const string G_SCENE_N_E01 = "Example_01 (유니티 기초)";
	public const string G_SCENE_N_E02 = "Example_02 (카메라)";
	public const string G_SCENE_N_E03 = "Example_03 (스크립트)";
	public const string G_SCENE_N_E04 = "Example_04 (물리 엔진)";
	public const string G_SCENE_N_E05 = "Example_05 (플래피 버드 - 시작)";
	public const string G_SCENE_N_E06 = "Example_06 (플래피 버드 - 플레이)";
	public const string G_SCENE_N_E07 = "Example_07 (플래피 버드 - 결과)";
	public const string G_SCENE_N_E08 = "Example_08 (UGUI - 로그인)";
	public const string G_SCENE_N_E09 = "Example_09 (UGUI - 서버 선택)";
	public const string G_SCENE_N_E10 = "Example_10 (사운드)";
	public const string G_SCENE_N_E11 = "Example_11 (스프라이트)";
	public const string G_SCENE_N_E12 = "Example_12 (애니메이션)";
	public const string G_SCENE_N_E13 = "Example_13 (두더지 잡기 - 시작)";
	public const string G_SCENE_N_E14 = "Example_14 (두더지 잡기 - 플레이)";
	public const string G_SCENE_N_E15 = "Example_15 (두더지 잡기 - 결과)";
	public const string G_SCENE_N_E16 = "Example_16 (쉐이더)";
	#endregion // 기본
}
