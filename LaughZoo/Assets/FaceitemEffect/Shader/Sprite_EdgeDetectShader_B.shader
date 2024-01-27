Shader "Unlit/Sprite_EdgeDetectShader_B"
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
            float4 _MainTex_TexelSize;

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

			float colorSobel(float2 uv)
			{
				float x = 0;
				float y = 0;
				float2 texelSize = _MainTex_TexelSize;
                // float2 texelSize = 1 / _ScreenParams;

				x += tex2D(_MainTex, uv + float2(-texelSize.x, -texelSize.y)) * -1;
				x += tex2D(_MainTex, uv + float2(-texelSize.x, 			  0)) * -2;
				x += tex2D(_MainTex, uv + float2(-texelSize.x,  texelSize.y)) * -1;

				x += tex2D(_MainTex, uv + float2( texelSize.x, -texelSize.y)) *  1;
				x += tex2D(_MainTex, uv + float2( texelSize.x, 			  0)) *  2;
				x += tex2D(_MainTex, uv + float2( texelSize.x,  texelSize.y)) *  1;

				y += tex2D(_MainTex, uv + float2(-texelSize.x, -texelSize.y)) * -1;
				y += tex2D(_MainTex, uv + float2(			0, -texelSize.y)) * -2;
				y += tex2D(_MainTex, uv + float2( texelSize.x, -texelSize.y)) * -1;
                
				y += tex2D(_MainTex, uv + float2(-texelSize.x,  texelSize.y)) *  1;
				y += tex2D(_MainTex, uv + float2(			0,  texelSize.y)) *  2;
				y += tex2D(_MainTex, uv + float2( texelSize.x,  texelSize.y)) *  1;

				return sqrt(x * x + y * y);
			}

            fixed4 frag (v2f i) : SV_Target
            {
                float a = colorSobel(i.uv);

                float d = dot(i.uv, float2(1, 1));
                float r = sin(d + _Time.r * 3);
                float g = sin(d + _Time.g * 3);
                float b = sin(d + _Time.b * 3);

                return fixed4(r,g,b,a);
            }
            ENDCG
        }
    }
}
