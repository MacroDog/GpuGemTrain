Shader "GPUGem/wave"
{
	Properties
	{
		_MainTex("RGB",2D) = "white"{}
		_DirectionX("DirectionX",float) = 1.0
		_DirectionY("DirectionY",float) = 1.0
		_Amplitude("Amplitude",float) = 20.0
		_Length("Lenght",float) = 1.0
		_Speed("Speed",float) = 10.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			float _DirectionX;
			float _DirectionY;
			float _Amplitude;
			float _Length;
			float _Speed;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 normal:TEXCOORD1;
				float3 binormal:TEXCOORD2;
				float tangent:TEXCOORD3;
			};

			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.y += sin(float2(_DirectionX,_DirectionY)*v.vertex.xz*2/_Length + _Amplitude*_Time.w);
				o.vertex = UnityObjectToClipPos(v.vertex);
				float s=cos(float2(_DirectionX,_DirectionY)*v.vertex.xz*2/_Length + _Amplitude*_Time.w);
				o.binormal = float3(1,0, s);
				s=_DirectionY*2/_Length*cos(float2(_DirectionX,_DirectionY)*v.vertex.xz*_Length + _Amplitude*_Time.w);
				o.tangent = float3(0,1,s);
				o.normal = mul(o.binormal,o.tangent);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
