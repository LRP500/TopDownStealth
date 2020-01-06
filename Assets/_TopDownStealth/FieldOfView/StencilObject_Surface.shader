Shader "Custom/StencilObject/Surface"
{
    Properties
    {
        
        [Header(Visible)]
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
        _Glossiness("Smoothness", Range(0, 1)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0.0
        _Emission("Emission", Color) = (0, 0, 0)
        [IntRange] _StencilRef("Stencil Reference", Range(0, 255)) = 0

        [Header(Hidden)]
        [Toggle] _EnableOutline("Outline", Float) = 0
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.2)) = 0.05
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Geometry"
        }

		Stencil
		{
			Ref [_StencilRef]
			Comp Equal
            Pass Keep
		}

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _Emission;

        void surf(Input i, inout SurfaceOutputStandard o)
        {
            fixed4 col = tex2D (_MainTex, i.uv_MainTex) * _Color;
            o.Alpha = col.a;
            o.Albedo = col.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Emission = _Emission;
        }

        ENDCG

        // 1st outline pass (outline)
        Pass
        {
            Stencil
            {
                Ref [_StencilRef]
                Comp NotEqual
                Pass Keep
            }

            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            half _OutlineWidth;
            half4 _OutlineColor;
            float _EnableOutline;

            struct appdata
            {
                float4 uv : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.position = v.uv;

                // apply outline offset only if outline enabled
                float3 offset = v.normal * _OutlineWidth;
                o.position.xyz += lerp(0, offset, _EnableOutline);

                o.position = UnityObjectToClipPos(o.position);
                return o;
            }

            fixed4 frag() : SV_TARGET
            {
                return _OutlineColor;
            }

            ENDCG
        }

        // 2nd outline pass (front)
        Pass
        {
            Stencil
            {
                Ref [_StencilRef]
                Comp NotEqual
                Pass Keep
            }

            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            half _OutlineWidth;
            half4 _OutlineColor;

            struct appdata
            {
                float4 uv : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.uv);
                return o;
            }

            fixed4 frag() : SV_TARGET
            {
                return fixed4(0, 0, 0, 1);
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
