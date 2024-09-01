Shader "Example_16/E16SurfaceShader_06" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
	}
	SubShader{
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		/*
		* 3 차원 그래픽스는 내부적으로 물체를 화면 상에 그릴 때 불필요한 부분은 그리지 않도록 잘라내는
		* 연산을 수행하며 해당 연산은 은면 제거라고 한다. (즉, 불필요한 연산을 줄임으로서 렌더링 성능을
		* 향상 시키는 것이 가능하다.)
		* 
		* 따라서, 은면 제거를 활용하면 특정 표면을 제거하는 방식을 제어하는 것이 가능하며 은면 제거에는
		* 크게 전면 제거와 후면 제거가 있다.
		*/
		cull front

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom noambient vertex:VSMain

		float4 _Color;

		/** 입력 */
		struct Input {
			float4 color;
		};

		/** 정점 쉐이더 */
		void VSMain(inout appdata_full a_stOutput) {
			a_stOutput.vertex.xyz += a_stOutput.normal.xyz * 0.01;
		}

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutput a_stOutput) {
			a_stOutput.Alpha = _Color.a;
			a_stOutput.Albedo = _Color.rgb;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutput a_stOutput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation)
		{
			return float4(0.0, 0.0, 0.0, 1.0);
		}
		ENDCG
			
		cull back

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom noambient

		float4 _Color;

		/** 입력 */
		struct Input {
			float4 color;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutput a_stOutput) {
			a_stOutput.Alpha = _Color.a;
			a_stOutput.Albedo = _Color.rgb;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutput a_stOutput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation)
		{
			float fDiffuse = saturate(dot(a_stOutput.Normal, a_stLightDirection) * 0.5 + 0.5);
			fDiffuse = ceil(fDiffuse * 3.0) / 3.0;

			float3 stFinalColor = a_stOutput.Albedo * fDiffuse;
			return float4(stFinalColor * a_fAttenuation, a_stOutput.Alpha);
		}
		ENDCG
	}
}
