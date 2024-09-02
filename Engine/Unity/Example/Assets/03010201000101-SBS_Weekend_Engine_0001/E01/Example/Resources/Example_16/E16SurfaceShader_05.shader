Shader "Example_16/E01S_Shader_16_05" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Tex", 2D) = "white" { }
		_NormalTex("Normal Tex", 2D) = "bump" { }
		_AmbientTex("Ambient Tex", CUBE) = "" { }
	}
	SubShader{
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom noambient

		float4 _Color;
		sampler2D _MainTex;
		sampler2D _NormalTex;
		samplerCUBE _AmbientTex;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_MainTex;
			float2 uv_NormalTex;

			float3 worldRefl;
			INTERNAL_DATA
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			half Specular;
			float3 m_stReflect;

			fixed Gloss;
			fixed Alpha;

			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputCustom a_stOutput) {
			a_stOutput.Alpha = _Color.a;
			a_stOutput.Albedo = tex2D(_MainTex, a_stInput.uv_MainTex).rgb;
			a_stOutput.Normal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));

			/*
			* WorldReflectionVector 함수는 법선 정보를 기반으로 반사 정보를 계산하는 역할을 수행한다.
			* 
			* 유니티는 노말 맵과 반사 벡터를 같이 사용 할 경우 내부적으로 에러를 발생시키며 이는 반사
			* 벡터와 노말 맵이 존재하는 법선 정보가 서로 다른 공간에 속해있기 때문이다.
			* 
			* 따라서, 노말 맵을 사용 할 경우 반사 벡터 또한 속해 있는 공간을 보정 할 필요가 있으며 이때
			* 사용되는 함수가 WorldReflectionVector 이다. 또한, Input 구조체는 INTERNAL_DATA 키워드를
			* 추가시켜줘야한다. (즉, 해당 키워드를 추가하지 않으면 쉐이더 컴파일 에러가 발생한다는 것을
			* 알 수 있다.)
			*/
			a_stOutput.m_stReflect = WorldReflectionVector(a_stInput, a_stOutput.Normal);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation)
		{
			float4 stReflectColor = texCUBE(_AmbientTex, a_stOutput.m_stReflect);

			float3 stFinalColor = (a_stOutput.Albedo * 0.25) + (stReflectColor.rgb * 0.75);
			return float4(stFinalColor, a_stOutput.Alpha);
		}
		ENDCG
	}
}
