Shader "Custom/StencilObject/Surface"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0, 1)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0.0
        _Emission("Emission", Color) = (0, 0, 0)
        [IntRange] _StencilRef("Stencil Reference", Range(0, 255)) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp("Stencil Comparison", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp("Stencil Operation", Int) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        
		Stencil
		{
			Ref [_StencilRef]
			Comp [_StencilComp]
            Pass [_StencilOp]
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
    }

    FallBack "Diffuse"
}
