Shader "Custom/StencilObject/SpritePlanarMapping"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Sharpness("Blend Sharpness", Range(0, 1)) = 1
        [IntRange] _StencilRef("Stencil Reference", Range(0, 255)) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp("Stencil Comparison", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp("Stencil Operation", Int) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "Minimap" = "Obstacle"
        }

		Stencil
		{
			Ref [_StencilRef]
			Comp [_StencilComp]
            Pass [_StencilOp]
		}

        Blend SrcAlpha OneMinusSrcAlpha
        Zwrite Off
        // Cull Off

        Pass
        {
            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _ TRANSPARENCY

            sampler2D _MainTex;
            fixed4 _MainTex_ST;
            fixed4 _Color;
            float _Sharpness;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normal : NORMAL;
                fixed4 color : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
                o.normal = normalize(worldNormal);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
#ifdef TRANSPARENCY
                float2 uv_top = TRANSFORM_TEX(i.worldPos.xz, _MainTex);
                fixed4 col_top = tex2D(_MainTex, uv_top);
                return col_top * _Color * i.color;
#endif
                return fixed4(0, 0, 0, 1);
            }

            ENDCG
        }

    }
}
