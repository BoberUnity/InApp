Shader "Custom/Billboard" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Texture Image", 2D) = "white" {}
	}
	SubShader {
	
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Zwrite Off
		Cull Off
		
		Pass {
			Name "BASE"
			
			CGPROGRAM
	 
			#pragma vertex vert
			#pragma fragment frag
	          
			uniform sampler2D _MainTex;
			uniform float4 _Color;
 
			 struct appdata_base {
				float4 vertex : POSITION;
				float2 texcoord0 : TEXCOORD0;
			};

			struct v2f_base {
				float4 position : SV_POSITION;
				float2 texcoord0 : TEXCOORD0;
			};

			v2f_base vert(appdata_base i) {
				v2f_base o;
				o.position = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0)) - float4(i.vertex.x, i.vertex.z, 0.0, 0.0));
				o.texcoord0 = i.texcoord0;
				return o;
			}

			float4 frag(v2f_base i) : COLOR {
				return (tex2D(_MainTex, i.texcoord0) * _Color);
			}
 
         	ENDCG
		}
   	}
	FallBack "Diffuse"
}
