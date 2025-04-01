Shader "E01/Example_16/E01S_Shader_16_04" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_NormalTex("Normal Texture", 2D) = "bump" { }
	}
	SubShader{
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom noambient alpha:fade

		float4 _Color;
		sampler2D _NormalTex;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_NormalTex;

			float3 worldPos;
		};

		/** 출력 */
		struct SurfaceOutputCustom {
			half Specular;
			float3 m_stWorldPos;

			fixed Gloss;
			fixed Alpha;

			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputCustom a_stOutput) {
			float3 stNormal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));

			a_stOutput.Alpha = _Color.a;
			a_stOutput.Albedo = _Color.rgb;
			a_stOutput.Normal = float3(stNormal.x, stNormal.y * -1.0, stNormal.z);

			a_stOutput.m_stWorldPos = a_stInput.worldPos;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation)
		{
			float fRim = pow(1.0 - saturate(dot(a_stOutput.Normal, a_stViewDirection)), 3.0);
			float3 stRimColor = float3(0.0, 1.0, 0.0) * fRim;
			
			/*
			* frac 함수는 실수에서 정수 부분은 버리고 소수점 부분만 반환해주는 역할을 수행한다.
			*/
			float3 stExtraRimColor = float3(0.0, 1.0, 0.0) * 
				saturate(frac(a_stOutput.m_stWorldPos.y * 0.01 + _Time.y));

			float3 stFinalColor = a_stOutput.Albedo + stRimColor + stExtraRimColor;
			return float4(stFinalColor * a_fAttenuation, (fRim + 0.15) * (sin(_Time.y * 2500.0) * 0.5 + 0.5));
		}
		ENDCG
	}
}
