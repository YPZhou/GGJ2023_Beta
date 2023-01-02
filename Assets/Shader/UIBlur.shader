Shader "Unlit/UIBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DownSample("Down Sample", Range(0, 1024)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        ZWrite Off
        Blend Off
        LOD 100

        // Down Sample pass
        Pass
        {
            ZTest off
            Cull off

            CGPROGRAM
            #pragma vertex vert_DownSamp
            #pragma fragment frag_DownSamp

            ENDCG
        }

        // Vertical Blur pass
        Pass
        {
            ZTest Always
            Cull off

            CGPROGRAM
            #pragma vertex vert_BlurVert
            #pragma fragment frag_Blur
            
            ENDCG
        }

        // Horizontal Blur Pass
        Pass
        {
            ZTest Always
            Cull off

            CGPROGRAM
            #pragma vertex vert_BlurHori
            #pragma fragment frag_Blur

            
            ENDCG
        }

        // Begin CG include part...
        CGINCLUDE

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        half4 _MainTex_TexelSize;
        float _DownSample;

        static const half4 GaussCore[7] = {
            half4(0.0205, 0.0205, 0.0205, 0),
            half4(0.0855,0.0855,0.0855,0),
            half4(0.232,0.232,0.232,0),
            half4(0.324,0.324,0.324,1),
            half4(0.232,0.232,0.232,0),
            half4(0.0855,0.0855,0.0855,0),
            half4(0.0205,0.0205,0.0205,0)
        };

        struct VertData
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        // Down sample part
        struct VertData_DownSamp
        {
            float4 pos : SV_POSITION;
            float2 uv0 : TEXCOORD0;
            float2 uv1 : TEXCOORD1;
            float2 uv2 : TEXCOORD2;
            float2 uv3 : TEXCOORD3;
        };
        VertData_DownSamp vert_DownSamp(VertData v)
        {
            VertData_DownSamp o;

            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv0 = v.uv + _MainTex_TexelSize.xy * half2(0.5, 0.5);
            o.uv1 = v.uv + _MainTex_TexelSize.xy * half2(0.5, -0.5);
            o.uv2 = v.uv + _MainTex_TexelSize.xy * half2(-0.5, 0.5);
            o.uv3 = v.uv + _MainTex_TexelSize.xy * half2(-0.5, -0.5);

            return o;
        }
        fixed4 frag_DownSamp(VertData_DownSamp i) : SV_TARGET
        {
            half4 color = half4(0, 0, 0, 0);

            color += tex2D(_MainTex, i.uv0);
            color += tex2D(_MainTex, i.uv1);
            color += tex2D(_MainTex, i.uv2);
            color += tex2D(_MainTex, i.uv3);

            return color / 4;
        }

        // Blur part
        struct VertData_Blur
        {
            float4 pos : SV_POSITION;
            half4 uv : TEXCOORD0;
            half2 offset : TEXCOORD1;
        };
        VertData_Blur vert_BlurVert(VertData v)
        {
            VertData_Blur o;

            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = half4(v.uv.xy, 1, 1);
            o.offset = _MainTex_TexelSize.xy * half2(1.0, 0.0) * _DownSample;

            return o;
        }
        VertData_Blur vert_BlurHori(VertData v)
        {
            VertData_Blur o;

            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = half4(v.uv.xy, 1, 1);
            o.offset = _MainTex_TexelSize.xy * half2(0.0, 1.0) * _DownSample;

            return o;
        }
        half4 frag_Blur(VertData_Blur i) : SV_TARGET
        {
            half2 uv = i.uv.xy;

            half2 offsetWidth = i.offset;
            half2 uv_withOffset = uv - offsetWidth * 3.0;

            half4 color = half4(0, 0, 0, 0);
            for (int j = 0; j < 7; j++)
            {
                half4 texCol = tex2D(_MainTex, uv_withOffset);
                color += texCol * GaussCore[j];
                uv_withOffset += offsetWidth;
            }

            return color;
        }
        ENDCG
    }
}
