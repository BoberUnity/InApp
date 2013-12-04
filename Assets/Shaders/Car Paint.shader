Shader "Custom/Car Paint" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.1
		_Fresnel ("Fresnel", Range (1, 8)) = 2
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Cube ("Reflection Cubemap", Cube) = "" { TexGen CubeReflect }
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong

		float4 _Color;
		float _Shininess;
		float _Fresnel;
		sampler2D _MainTex;
		samplerCUBE _Cube;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float3 worldRefl;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half fresnel = pow(saturate(1.0 - dot(o.Normal, normalize(IN.viewDir))), _Fresnel);
			half4 reflcol = lerp(c, texCUBE (_Cube, IN.worldRefl), fresnel);

			c = lerp (_Color, c * _Color, c.a);
			o.Emission = reflcol;
			o.Gloss = c.a;
			o.Specular = _Shininess;
			o.Albedo = c.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
