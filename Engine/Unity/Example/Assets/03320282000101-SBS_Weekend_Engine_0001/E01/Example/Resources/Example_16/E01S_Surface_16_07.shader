Shader "E01/Example_16/E01S_Shader_16_07" {
	Properties{
		_Cutout("Alpha Cutout", Range(0, 1)) = 0
		_Thinkness("Outline Thinkness", Range(1, 1.5)) = 1

		_Color("Color", Color) = (1, 1, 1, 1)
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)

		_MainTex("Main Texture", 2D) = "white" { }
		_NoiseTex("Noise Texture", 2D) = "white" { }
	}
	SubShader{
		Tags {
			"Queue" = "Transparent+1"
			"RenderType" = "Transparent"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom noambient alpha:fade

		float _Cutout;
		float _Thinkness;

		float4 _Color;
		float4 _OutlineColor;

		sampler2D _MainTex;
		sampler2D _NoiseTex;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_MainTex;
			float2 uv_NoiseTex;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutput a_stOutput) {
			a_stOutput.Alpha = tex2D(_NoiseTex, a_stInput.uv_NoiseTex).r;
			a_stOutput.Albedo = tex2D(_MainTex, a_stInput.uv_MainTex);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutput a_stOutput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation)
		{
			float fAlpha = 0.0;
			float fOutline = 1.0;

			// 텍스처 알파 값이 클 경우
			if (min(0.99999, a_stOutput.Alpha * 5.0) >= _Cutout) {
				fAlpha = 1.0;
			}

			// 텍스처 알파 값이 클 경우
			if (min(0.99999, a_stOutput.Alpha * 5.0) >= _Cutout * _Thinkness) {
				fOutline = 0.0;
			}

			float3 stOutlineColor = _OutlineColor.rgb * fOutline;
			return float4(a_stOutput.Albedo + stOutlineColor, fAlpha);
		}
		ENDCG
	}
}
