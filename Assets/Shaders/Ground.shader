Shader "Custom/Ground" {
	Properties {
		_Scale ("Scale", float) = 0.25
		_BumpScale ("BumpScale", float) = 0.25
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_DecalTex ("Decal (RGB)", 2D) = "gray" {}
	}
	SubShader {
		Tags {  "Queue" = "Geometry-500" "RenderType"="Opaque" }
		ZTest Less
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		
		float _Scale;
		float _BumpScale;
		sampler2D _MainTex;
		sampler2D _DecalTex;
		sampler2D _BumpMap;
		
		struct Input {
			float3 worldPos;
			float2 uv_MainTex;
			float2 uv_DecalTex;
			float2 uv_BumpMap;
		};
	    
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.worldPos.xz * _Scale) * tex2D (_DecalTex, IN.uv_DecalTex) * 2;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.worldPos.xz * _BumpScale));
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
