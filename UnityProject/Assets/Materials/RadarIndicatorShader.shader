// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/RadarIndicatorShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "main" {}
		_ColorGradient ("ColorGradient", 2D) = "gradient" {}
		_Dist ("Distance", Float) = 0.5
	}
	SubShader
	{
		Tags { 
		"RenderType"="Opaque"
		"Queue" = "Transparent+1"
		}

		Pass
		{
			ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _ColorGradient;
			float _Dist;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = fixed4(tex2D(_ColorGradient, float2(_Dist, 0.5)).rgb, tex2D(_MainTex, i.uv).a);
				return col;
			}
			ENDCG
		}
	}
}
