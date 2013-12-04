Shader "Custom/Tree Brunch" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
		_Shadow ("Shadow", Range(0,1)) = 1
	}

	SubShader {
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		LOD 200
		Cull Off
		
		CGPROGRAM
		#pragma surface surf Unlit alphatest:_Cutoff

		sampler2D _MainTex;
		fixed4 _Color;
		fixed _Shadow;
		
		fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten) {
	        fixed4 c;
	        c.rgb = s.Albedo * _LightColor0.rgb * atten * 2; 
	        c.a = s.Alpha;
	        return c;
	    }

		struct Input {
			float2 uv_MainTex;
			float4 color : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb * IN.color.rgb * _Shadow;
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "Transparent/Cutout/VertexLit"
}
