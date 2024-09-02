Shader "Example_16/E01S_Shader_16_02" {
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" { }
	}
	SubShader{
		Tags {
			"Queue" = "Geometry+1"
			"RenderType" = "Opaque"
		}

		CGPROGRAM
		#pragma target 3.0
		#pragma surface SSMain Standard

		float4 _Color;

		/*
		* 특정 텍스처로부터 데이터를 가져오기 위해서는 텍스처 이외에도 Filter 모드와 같은 부가적인
		* 설정이 필요하다. 따라서, 쉐이더에서는 해당 정보를 지니고 있는 Sampler 자료형을 제공하며
		* 해당 자료형을 활용하면 유니티에서 설정한 텍스처를 쉐이더 내부에서 사용하는 것이 가능하다.
		*/
		sampler2D _MainTex;

		/** 입력 */
		struct Input {
			float4 color;
			float2 uv_MainTex;
		};

		/** 서피스 쉐이더 */
		void SSMain(Input a_stInput, inout SurfaceOutputStandard a_stOutput) {
			/*
			* tex2D 함수는 입력으로 넘겨진 텍스처로부터 데이터를 가져오는 역할을 수행한다.
			* (즉, 샘플링을 수행한다는 것을 알 수 있다.)
			*/
			float4 stColor = tex2D(_MainTex, a_stInput.uv_MainTex);

			a_stOutput.Alpha = stColor.a;
			a_stOutput.Albedo = stColor.rgb;
		}
		ENDCG
	}
}
