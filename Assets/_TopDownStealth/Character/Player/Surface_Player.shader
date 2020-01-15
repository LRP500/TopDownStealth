Shader "Custom/Surface/Player"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1, 1, 1, 1)
        _CamouflageColor("Camouflage Color", Color) = (1, 1, 1, 1)
        
        [Toggle(_ENABLE_CAMOUFLAGE)]
        _EnableCamouflage("Enable Camouflage", float) = 0
        
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }
        
        CGPROGRAM

        #pragma shader_feature _ENABLE_CAMOUFLAGE

        #pragma surface surf Standard fullforwardshadows alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _MainColor;
        fixed4 _CamouflageColor;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 col = tex2D (_MainTex, IN.uv_MainTex);

        #if defined(_ENABLE_CAMOUFLAGE)
            col = col * _CamouflageColor;
        #else
            col = col * _MainColor;
        #endif

            o.Albedo = col.rgb;
            o.Alpha = col.a;
        }

        ENDCG
    }

    FallBack "Diffuse"
}
