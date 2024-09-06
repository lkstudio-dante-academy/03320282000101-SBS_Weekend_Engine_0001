Shader "Example_16/E01S_Shader_16_01" {
	/*
	* Properties 영역은 유니티 에디터 상에 쉐이더 동작하는데 필요한 정보를 설정 할 수 있게
	* 옵션을 제공하는 역할을 수행한다. 따라서, 해당 영역을 활용하면 특정 데이터를 에디터 상에
	* 바로 설정 후 결과를 확인하는 것이 가능하다.
	* 
	* Properties 옵션 설정 방법
	* - 내부 옵션 이름 + 외부 옵션 이름 + 옵션 타입 + 기본 값 (선택 사항)
	* 
	* 많이 활용되는 Properties 옵션 타입
	* - int		<- 정수
	* - float	<- 실수
	* - Color	<- 색상
	* - 2D		<- 2 차원 텍스처
	* 
	* 내부 옵션 이름은 쉐이더 명령문 상에서 사용 될 이름을 의미하기 때문에 알파벳 대/소문자,
	* _ (언더 스코어), 숫자 이외에는 사용하는 것이 불가능하다.
	* 
	* 반면, 외부 옵션 이름은 유니티 에디터 상에 표시되는 이름이기 때문에 한글을 비롯한 다양한
	* 문자를 사용하는 것이 가능하다.
	*/
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
	}
	SubShader{
		/*
		* Tags 영역은 쉐이더가 동작하는데 필요한 부가적인 정보를 설정 할 수 있는 영역을 의미한다.
		* 
		* Tags 옵션 종류
		* - RenderType			<- 물체의 불투명/반투명 여부
		* - Queue			<- 물체가 그려지는 순서
		* - RenderPipeline		<- 렌더링 파이프라인 종류
		*/
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		/*
		* pragma 키워드는 전처리기 명령어로서 쉐이더에 대한 부가적인 정보를 명시하는 역할을 수행
		* 한다.
		* 
		* 많이 활용되는 pragma 옵션 종류
		* - target			<- 쉐이더 모델 버전
		* - vertex			<- 정점 쉐이더 진입 메서드 명시
		* - fragment		<- 픽셀 쉐이더 진입 메서드 명시
		* - surface			<- 서피스 쉐이더 진입 메서드 명시
		* - Standard		<- PBS (Physic Base Shading) 적용
		* - alpha:fade		<- 알파 값을 이용해서 투명한 물체를 표현
		*/
		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Standard

		/*
		* Properties 영역에 명시 된 옵션은 쉐이더 내부에서 상수의 역할을 수행한다. (즉, 유니티
		* 에디터 상에서 설정 된 데이터를 쉐이더 내부에서는 상수를 통해 활용하는 것이 가능하다.)
		*/
		float4 _Color;

		/*
		* 3 차원 그래픽스는 점에 대한 데이터를 다루는 것이기 때문에 특정 쉐이더 동작 할 때 점에
		* 대한 정보가 필요하며 해당 정보에는 위치를 비롯한 다양한 부가 정보를 추가하는 것이
		* 가능하다. 이렇게 설정 된 점의 정보는 쉐이더 내부에서 구조체의 형태로 전달되며 해당 구조체를
		* 활용하면 입력으로 전달 된 점의 정보를 이용해서 다양한 연산을 수행하는 것이 가능하다.
		*/
		/** 입력 */
		struct Input {
			float4 color;
		};

		/*
		* SurfaceOutputStandard 구조체는 서피스 쉐이더의 연산 결과를 저장하는 역할을 수행한다.
		* (즉, 해당 구조체에 색상을 비롯한 여러 결과를 설정함으로서 다양한 재질을 표현하는 것이
		* 가능하다.)
		* 
		* SurfaceOutputStandard 주요 멤버 변수
		* - Albedo			<- 표면의 난반사 색상
		* - Emission		<- 표면의 자체 색상
		* - Normal			<- 표면의 방향
		* - Smoothness		<- 표면의 부드러움 정도
		* - Metallic		<- 물체의 금속성 정도
		* - Alpha			<- 물체의 불투명 정도
		*/
		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputStandard a_stOutput) {
			a_stOutput.Albedo = _Color.rgb;
		}
		ENDCG
	}
}
