Shader "Camera/Glitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlitchVec("Glich Vector", Vector) = (0, 0, 0, 0)
        _JitterVec("Jitter Vector", Vector) = (0, 0, 0, 0)
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
            float4 _MainTex_ST;
            half4 _MainTex_TexelSize;
            float2 _GlitchVec;
            float2 _JitterVec;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = fixed4(0, 0, 0, 1);
                float2 jitterVec = i.uv + _JitterVec * _MainTex_TexelSize;
                float2 glitchVec = _GlitchVec * _MainTex_TexelSize;
                col.r = tex2D(_MainTex, jitterVec + glitchVec).r;
                col.g = tex2D(_MainTex, jitterVec).g;
                col.b = tex2D(_MainTex, jitterVec - glitchVec).b;
                return col;
            }
            ENDCG
        }
    }
}
