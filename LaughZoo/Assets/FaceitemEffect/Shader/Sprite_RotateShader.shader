Shader "Unlit/Sprite_RotateShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color * _RendererColor;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4x4 rotateMat = 0;
                float r = _Time.x * 100;
                rotateMat[0] = float4(cos(r), -sin(r), 0, 1);
                rotateMat[1] = float4(sin(r), +cos(r), 0, 1);
                rotateMat[2] = float4(0, 0, 1, 1);
                rotateMat[3] = float4(0, 0, 0, 1);

                // return float4(i.uv, 0, 1);

                float2 uv = mul(i.uv - 0.5, rotateMat) + 0.5;
                fixed4 col = tex2D(_MainTex, uv) * i.color;
                
                
                

                return col;
            }
            ENDCG
        }
    }
}
