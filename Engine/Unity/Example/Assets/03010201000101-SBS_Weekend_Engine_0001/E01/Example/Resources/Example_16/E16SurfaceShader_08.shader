Shader "Example_16/E01S_Shader_16_08" {
	Properties{
		_Strength("Strength", Range(0, 0.5)) = 0

		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" { }
		_NoiseTex("Noise Texture", 2D) = "white" { }
	}
		SubShader{
			Tags {
				"Queue" = "Transparent+2"
				"RenderType" = "Transparent"
			}

			zwrite off

		/*
		* GrabPass 는 렌더 타겟에 그려지는 색상 정보를 _GrabTexture 에 복사하는 역할을 수행한다.
		* (즉, 해당 패스를 이용하면 그려진 장면을 기반으로 여러가지 효과를 줄수 있는 후처리를 구현
		* 하는 것이 가능하다.)
		*/
		GrabPass { }

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom noambient alpha:fade

		float _Strength;
		float4 _Color;

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		sampler2D _GrabTexture;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_MainTex;
			float2 uv_NoiseTex;

			float4 screenPos;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutput a_stOutput) {
			float3 stScreenUV = a_stInput.screenPos.xyz / a_stInput.screenPos.w;
			float4 stNoise = tex2D(_NoiseTex, stScreenUV.xy);

			float fStrength = _Strength * sin(_Time.y * 5.0);

			a_stOutput.Alpha = _Color.a;
			a_stOutput.Albedo = tex2D(_GrabTexture, (stScreenUV.xy + (stNoise.xy * fStrength)) % 1);
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutput a_stOutput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation)
		{
			return float4(a_stOutput.Albedo * _Color.rgb, a_stOutput.Alpha);
		}
		ENDCG
	}
}
