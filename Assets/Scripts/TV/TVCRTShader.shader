Shader "Custom/TVCRTShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplacementTex("Displacement Texture", 2D) = "black" {}
		_Strength("Displacement Strength", Range(0, 1)) = 0 

		_NoiseTex("Noise Texture", 2D) = "white" {}
		_NoiseXSpeed("Noise X Speed", float) = 100.0
		_NoiseYSpeed("Noise Y Speed", float) = 100.0
		_VignetteTex("Vignette Texture", 2D) = "white" {}
		_LineTex("Line Texture", 2D) = "white" {}
		_LineColor("Line Color", Color) = (1, 1, 1, 1)
		_DistortionSrength("Distortion Strength", float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float4 _MainTex_ST;

			uniform sampler2D _NoiseTex;
			uniform fixed _NoiseXSpeed;
			uniform fixed _NoiseYSpeed;
			uniform fixed _NoiseCutoff;
			uniform sampler2D _VignetteTex;
			uniform sampler2D _LineTex;
			uniform fixed4 _LineColor;
			uniform fixed _DistortionSrength;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					o.uv.y = 1 - o.uv.y;
#endif

				return o;
			}

			fixed2 LensDistortion(fixed2 uv)
			{
				fixed2 center = uv - 0.5;
				fixed r2 = center.x * center.x + center.y * center.y;
				fixed ratio = 1.0 + r2 * _DistortionSrength * sqrt(r2);

				return center * ratio + 0.5;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed2 distortionUV = LensDistortion(i.uv);

				fixed4 mainTex = tex2D(_MainTex, distortionUV);

				fixed4 vignetteTex = tex2D(_VignetteTex, i.uv);

				fixed2 noiseUV = distortionUV + fixed2(_NoiseXSpeed * _SinTime.z, _NoiseYSpeed * _SinTime.z);
				fixed4 noiseTex = tex2D(_NoiseTex, noiseUV);
				if (noiseTex.r > _NoiseCutoff)
					noiseTex = fixed4(1, 1, 1, 1);

				fixed4 lineTex = tex2D(_LineTex, distortionUV);
				lineTex *= _LineColor;
				lineTex *= noiseTex;

				fixed4 finalColor = mainTex;
				finalColor *= vignetteTex;
				finalColor += lineTex * vignetteTex;

				return finalColor;
			}
			ENDCG
		}
	}
}
