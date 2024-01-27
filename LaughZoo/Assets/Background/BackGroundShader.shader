Shader "Unlit/BackGroundShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EffectTime("Time", Float) = 0
        _ScrollSpeed("Scroll Speed", Vector) = (1,1,1,1)
        _WarpSize("Warp Scale", Float) = 2
        _WarpIntensity("Warp Intensity", Float) = 2
        _ColorShiftSpeed("Color Shift Speed", Float) = 2
        _ColorShiftIntensity("Color Shift Intensity", Float) = 2

        _HSVShift("HSV Shift", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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

            float _EffectTime;

            float2 _ScrollSpeed;
            float _WarpSize;
            float _WarpIntensity;
            float _ColorShiftSpeed;
            float _ColorShiftIntensity;

            float4 _HSVShift;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            float3 rgb2hsv(float3 c)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
                float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }
            float3 hsv2rgb(float3 c)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                uv = uv + _EffectTime * _ScrollSpeed;
                uv = uv + sin(i.uv.y * _WarpSize + _EffectTime) * _WarpIntensity;
                // uv = frac(uv);
                
                fixed4 col = tex2D(_MainTex, uv);
                float3 hsv = rgb2hsv(col.rgb);
                hsv.x = frac(hsv.x + _EffectTime);
                hsv += _HSVShift.xyz;
                
                col.rgb = hsv2rgb(hsv);
        
                return col;
            }
            ENDCG
        }
    }
}
