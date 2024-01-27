Shader "Unlit/Sprite_ColorSplitGlichShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        _SplitAmount("Split", Float) = 0
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 color   : COLOR;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _RendererColor;

            float _SplitAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color * _RendererColor;

                return o;
            }

            float randomNoise(float x, float y)
            {
                return frac(sin(dot(float2(x, y), float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float split = _SplitAmount * randomNoise(floor(_Time.x * 100), 20);
                
                float2 uv = i.uv * 1;
                fixed4 colorR = tex2D(_MainTex, float2(uv.x, uv.y) + split);
                fixed4 colorG = tex2D(_MainTex, float2(uv.x, uv.y));
                fixed4 colorB = tex2D(_MainTex, float2(uv.x, uv.y) - split);
                colorR.rgb *= colorR.a;
                colorG.rgb *= colorG.a;
                colorB.rgb *= colorB.a;

                float alpha = max(colorR.a, max(colorG.a, colorB.b));

                return fixed4(colorR.r, colorG.g, colorB.b, alpha);

                fixed3 color = fixed3(colorR.r, colorG.g, colorB.b);


                




                return fixed4(color, alpha);
                

                // return col;
            }
            ENDCG
        }
    }
}
