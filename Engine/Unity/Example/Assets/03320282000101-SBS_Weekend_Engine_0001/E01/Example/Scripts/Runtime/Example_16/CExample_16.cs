using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 쉐이더란?
 * - 화면에 출력 될 픽셀의 색상을 제어 할 수 있는 프로그램을 의미한다. (즉, 쉐이더를 이용하면
 * 물체의 다양한 재질을 표현하는 것이 가능하다.)
 * 
 * Unity 쉐이더 종류
 * - 정점 쉐이더					<- 필수
 * - 픽셀 (프래그먼트) 쉐이더		<- 필수
 * - 컴퓨트 쉐이더				<- 옵션
 * 
 * Unity 는 내부적으로 특정 물체를 화면 상에 그리기 위해 여러 단계를 거치며 해당 단계를
 * 렌더링 (그래픽스) 파이프라인이라고 한다. 이때, 렌더링 파이프라인 중에 가장 핵심이 되는
 * 처리는 정점, 래스터, 픽셀 처리가 있다.
 * 
 * 정점 처리는 정점 쉐이더에 의해서 처리되며 픽셀 처리는 픽셀 쉐이더에 의해서 처리된다.
 * 마지막으로 래스터 처리는 GPU (그래픽 카드) 에 의해서 처리되기 때문에 사용자 (프로그래머)
 * 가 해당 처리를 제어하는 것은 불가능하다.
 * 
 * 반면, 정점과 픽셀 처리는 쉐이더에 의해서 처리되기 때문에 사용자가 원하는데로 해당 처리를
 * 제어하는 것이 가능하며 이때 사용되는 것이 쉐이더 프로그램이다. (즉, 쉐이더 프로그램은
 * GPU 상에서 구동되는 프로그램을 의미한다.)
 * 
 * 정점 쉐이더란?
 * - 3 차원 공간 상에 존재하는 점 (버텍스) 의 정보를 2 차원 공간 상으로 변환 시키는 역할을
 * 수행한다. (즉, 정점 쉐이더 프로그램을 작성하면 공간에 대한 좌표 정보를 프로그램의 목적에
 * 맞게 제어하는 것이 가능하다.)
 * 
 * 래스터 처리란?
 * - 실제 화면 상에 출력 될 픽셀을 결정하는 처리를 의미한다. 해당 처리에는 굉장히 많은 연산이
 * 필요하기 때문에 해당 처리는 GPU 가 전용으로 처리하며 사용자는 이에 관여 할 수 없다.
 * 
 * 픽셀 (프래그먼트) 쉐이더란?
 * - 래스터 처리 과정에서 결정 된 픽셀의 실제 색상을 연산하는 역할을 수행한다. (즉, 픽셀
 * 쉐이더 프로그램을 작성하면 다양한 효과를 연출하는 것이 가능하다.)
 * 
 * Unity 에서 쉐이더 작성 방법
 * - Shader Lab
 * - Surface Shader
 * - Vertex & Fragment Shader
 * 
 * Shader Lab 이란?
 * - 과거 버전의 Unity 에서 쉐이더를 작성 할 때 주로 활용하던 방법으로 간단한 키워드를 통해
 * 쉐이더를 작성하는 것이 가능하다. (단, 다양한 효과를 연출하기에는 많은 제약 사항이 있기
 * 때문에 현재는 거의 사용되지 않는 방법이지만 여러 플랫폼에 호환되는 쉐이더를 간단하게 작성
 * 할 수 있기 때문에 단순한 쉐이더는 해당 방법으로 제작하는 것을 고려해 볼 수 있다.)
 * 
 * Surface Shader 란?
 * - 물리 기반 쉐이더를 손쉽게 제작 할 수 있는 방법으로서 해당 방식을 사용하면 많은 연산을
 * Unity 가 자동으로 처리해주기 때문에 최소한의 명령문 작성으로 쉐이더 프로그램을 제작하
 * 는 것이 가능하다. (단, 많은 연산을 Unity 가 처리해주기 때문에 퀄리티 좋은 쉐이더
 * 프로그램을 제작하기에는 적합하지 않다.)
 * 
 * Vertex & Fragment Shader 란?
 * - 과거부터 사용되던 전통적인 쉐이더 제작 방법으로서 해당 방식을 사용하면 퀄리티 좋은 다양한
 * 효과를 구현하는 것이 가능하다. (단, Unity 가 처리되는 부분이 거의 없기 때문에 쉐이더를
 * 작성하기 위한 많은 배경 지식을 알아야 할 필요가 있다.)
 * 
 * 쉐이더 프로그램 작성 언어 종류
 * - CG (C for Graphic)
 * - GLSL (OpenGL Shader Language)
 * - HLSL (High Level Shader Language)
 * 
 * CG 언어는 Built In 렌더링 파이프라인에 공식적으로 사용되는 표준 쉐이더 언어로서 해당
 * 언어로 작성 된 쉐이더 프로그램은 호환성이 좋기 때문에 여러 플랫폼에 무리 없이 포팅하는
 * 것이 가능하다.
 * 
 * GLSL 언어는 OpenGL 과 Vulkan 진영에서 사용되는 쉐이더 언어를 의미하며 HLSL 언어는
 * DirectX 진영에서 사용되는 쉐이더 언어이다.
 * 
 * 최근 Unity 는 기존에 사용되던 Built In 렌더링 파이프라인 이외에도 URP 와 HDRP 를
 * 지원하며 URP 와 HDRP 에서는 기존에 사용하던 CG 언어 대신에 HLSL 언어를 채택했다.
 * (즉, CG 언어는 새로운 렌더링 파이프라인의 기능을 모두 활용 할 수 없는 단점이 있기 때문에
 * URP 와 HDRP 를 기반으로 프로그램을 작성 할 경우 HLSL 을 사용하는 것을 추천한다.)
 */
/** Example 16 */
public class CE01Example_16 : CSceneManager
{
	#region 변수
	[SerializeField] private List<GameObject> m_oTargetList = new List<GameObject>();
	#endregion // 변수

	#region 프로퍼티
	public override string SceneName => KDefine.G_SCENE_N_E16;
	#endregion // 프로퍼티

	#region 함수
	/** 초기화 */
	public override void Awake()
	{
		base.Awake();
	}

	/** 상태를 갱신한다 */
	public override void Update()
	{
		base.Update();

		for(int i = 0; i < m_oTargetList.Count; ++i)
		{
			m_oTargetList[i].transform.Rotate(Vector3.up * (90.0f * Time.deltaTime), Space.World);
		}
	}
	#endregion // 함수
}
