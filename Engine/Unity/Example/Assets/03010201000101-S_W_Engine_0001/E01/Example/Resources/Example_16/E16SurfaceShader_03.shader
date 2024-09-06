Shader "Example_16/E01S_Shader_16_03" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" { }

		/*
		* 노말 맵이란?
		* - 노말 (법선) 정보를 지니고 있는 텍스처를 의미한다. (즉, 노말 맵을 활용하면 단순한 평면을
		* 깊이감을 느낄 수 있게 음영을 계산하는 것이 가능하다.)
		* 
		* 게임과 실시간 렌더링 프로그래밍은 가능하면 빠른 속도로 물체를 화면 상에 그려야되기 때문에
		* 불필요한 정점 정보는 줄이는 것이 좋다. 단, 정점이 줄었다는 것은 그만큼 물체가 단순한 형태가
		* 된다는 것을 의미하기 때문애 적은 정점만으로도 퀄리티 있는 물체를 표현하기 위한 여러 수단이
		* 존재하며 그중 하나가 노말 맵을 활용하면 노말 맵핑이다.
		*/
		_NormalTex("Normal Texture", 2D) = "bump" { }

		/*
		* 스펙큘러 맵이란?
		* - 물체의 표면에서 반사되는 정반사 성분을 조절 할 수 있는 데이터를 지니고 있는 텍스처를
		* 의미한다. (즉, 스펙큘러 맵을 활용하면 물체의 표면에 재질에 따라 정반사 강도를 조절하는
		* 것이 가능하다.)
		*/
		_SpecularTex("Specular Texture", 2D) = "white" { }
	}
	SubShader{
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Custom

		float4 _Color;
		sampler2D _MainTex;
		sampler2D _NormalTex;
		sampler2D _SpecularTex;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_MainTex;
			float2 uv_NormalTex;
			float2 uv_SpecularTex;
		};

		/*
		* 서피스 쉐이더는 SurfaceOutput 을 비롯한 출력 데이터를 저장하기 위한 구조체를 미리 제작해서
		* 제공하고 있다. 단, 해당 구조체에 속해 있는 데이터에 이외에 커스텀한 데이터를 추가하고 싶다면
		* SurfaceOutputCustom 구조체를 선언 후 SurfaceOutput 에 존재하는 모든 변수를 추가 후 커스텀하게
		* 추가하고 싶은 변수를 선언하면 된다.
		*/
		/** 출력 */
		struct SurfaceOutputCustom {
			half Specular;
			float4 m_stSpecular;

			fixed Gloss;
			fixed Alpha;

			fixed3 Albedo;
			fixed3 Normal;
			fixed3 Emission;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputCustom a_stOutput) {
			float4 stColor = tex2D(_MainTex, a_stInput.uv_MainTex);

			/*
			* UnpackNormal 함수는 오브젝트 (탄젠트) 공간에 속해 있는 법선 정보를 지역 공간으로 변환
			* 해주는 역할을 수행한다. (즉, 노말 맵에 저장 된 법선 정보는 모두 오브젝트 공간에 속해
			* 있기 때문에 해당 정보와 광원을 바로 내적하는 것은 불가능하다는 것을 알 수 있다.)
			* 
			* 따라서, 노말 맵핑을 하기 위해서는 지역 공간에 속해있는 광원을 오브젝트 공간으로 변환하거나
			* 오브젝트 공간에 속해있는 법선 정보를 지역 공간으로 변환해서 두 데이터의 공간을 서로 일치
			* 시킬 필요가 있다.
			*/
			float3 stNormal = UnpackNormal(tex2D(_NormalTex, a_stInput.uv_NormalTex));
			float4 stSpecular = tex2D(_SpecularTex, a_stInput.uv_SpecularTex);

			a_stOutput.Alpha = stColor.a;
			a_stOutput.Albedo = stColor.rgb;
			a_stOutput.Normal = float3(stNormal.r, stNormal.g * -1.0, stNormal.z);
			a_stOutput.m_stSpecular = stSpecular;
		}

		/** 광원을 처리한다 */
		float4 LightingCustom(SurfaceOutputCustom a_stOutput,
			float3 a_stLightDirection, float3 a_stViewDirection, float a_fAttenuation)
		{
			float3 stHalf = normalize(a_stLightDirection + a_stViewDirection);

			float fDiffuse = saturate(dot(a_stOutput.Normal, a_stLightDirection));
			float fSpecular = saturate(dot(a_stOutput.Normal, stHalf));

			float3 stSpecularColor = float3(1.0, 1.0, 1.0) * pow(fSpecular, 20.0) * 
				a_stOutput.m_stSpecular.rgb;

			float3 stFinalColor = a_stOutput.Albedo * fDiffuse + stSpecularColor;
			return float4(stFinalColor * a_fAttenuation, a_stOutput.Alpha);
		}
		ENDCG
	}
}
