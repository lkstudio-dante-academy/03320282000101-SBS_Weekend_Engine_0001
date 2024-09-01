using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 카메라란?
 * - 게임 공간을 바라보는 시야를 의미하며 유니티는 카메라를 통해서 실제 유저가 사용하는 화면에 그려지는 장면을 결정한다.
 * (즉, 기본적으로 게임 공간은 특정 범위가 존재하지않는 무한한 공간이기 때문에 해당 공간을 카메라를 통해서 화면 상에 보여질
 * 특정 영역을 제한한다는 것을 알 수 있다.)
 * 
 * 따라서, 다양한 해상도를 지닌 화면에 처음 작업했던 화면과 동일한 결과를 출력하기 위해서는 반드시 현재 화면의 해상도에 맞춰서
 * 카메라의 수치를 보정해 줄 필요가 있다.
 */
/** Example 2 */
public class CExample_02 : CSceneManager
{
	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E02;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}
	#endregion // 함수
}
