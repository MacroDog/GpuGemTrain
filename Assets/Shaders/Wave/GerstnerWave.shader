Shader "GPUGem/GerstnerWave"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
				float4 vertex : SV_POSITION;
				float3 testPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4x4 _WaveMatrix;
			float4x4 _WaveDirection;
			v2f vert (appdata v)
			{
				v2f o;
				float3 pos=float3(0,0,0);
				for(int i=0;i<4;i++){
					v.vertex.x += _WaveMatrix[i].x * _WaveMatrix[i].y * _WaveDirection[i].x * cos(_WaveMatrix[i].z * dot(_WaveDirection[i].xy,v.vertex.xz) + _Time.w*_WaveMatrix[i].w);
					v.vertex.z += _WaveMatrix[i].x * _WaveMatrix[i].y * _WaveDirection[i].y * cos(_WaveMatrix[i].z * dot(_WaveDirection[i].xy,v.vertex.xz) + _Time.w*_WaveMatrix[i].w);
					v.vertex.y += _WaveMatrix[i].y * sin(_WaveMatrix[i].z * dot(_WaveDirection[i].xy,v.vertex.xz) + _Time.w * _WaveMatrix[i].w);
				}
				
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.testPos = pos;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
