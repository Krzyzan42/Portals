// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/PortalShader"
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float4 col : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			float4 RotateAroundYInDegrees(float4 vertex, float degrees)
			{
				float alpha = degrees * UNITY_PI / 180.0;
				float sina, cosa;
				sincos(alpha, sina, cosa);
				float2x2 m = float2x2(cosa, -sina, sina, cosa);
				return float4(mul(m, vertex.xz), vertex.yw).xzyw;
			}

            v2f vert (appdata v)
            {
                v2f o;
				v.vertex = RotateAroundYInDegrees(v.vertex, 90); 
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.col = o.vertex;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				float2 screenCoord = float2(i.col.x / i.col.w, -i.col.y / i.col.w);
				screenCoord +=1;
				screenCoord /= 2;
                fixed4 col = tex2D(_MainTex, screenCoord);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, i.uv);
				return col;
				return fixed4(screenCoord, 0, 1);
            }
            ENDCG
        }
    }
}
