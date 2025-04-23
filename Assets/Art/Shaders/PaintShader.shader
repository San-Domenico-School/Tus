Shader "Unlit/PaintShader"
{
    Properties {
        _MainTex ("Previous", 2D) = "white" {}
        _PaintUV ("Paint UV", Vector) = (0,0,0,0)
        _Radius ("Radius", Float) = 0.05
        _PaintColor ("Paint Color", Color) = (1,0,0,1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            HLSLPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _PaintUV;
            float _Radius;
            float4 _PaintColor;

            float4 frag(v2f_img i) : SV_Target {
                float2 uv = i.uv;
                float dist = distance(uv, _PaintUV.xy);
                float mask = smoothstep(_Radius, _Radius * 0.8, dist);
                float4 baseColor = tex2D(_MainTex, uv);
                return lerp(_PaintColor, baseColor, mask);
            }

            ENDHLSL
        }
    }
}
