Shader "Custom/Replacement/Minimap"
{
    Properties
    {
        [Header(Detectable)]
        _FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1)
        _FresnelBias("Fresnel Bias", Float) = 0
		_FresnelScale("Fresnel Scale", Float) = 1
        [Power(4)] _FresnelExponent("Fresnel Exponent", Range(0, 4)) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "Minimap" = "Visible"
        }
        
        ZTest Always
        ZWrite Off
        Blend One One

        Pass
        {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            fixed4 _OverDrawColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OverDrawColor;
            }

            ENDCG
        }
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "Minimap" = "Detectable"
        }
        
        ZTest Always
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {

            CGPROGRAM

            #pragma shader_feature MINIMAP_ENABLED

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            fixed4 _DetectableColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
            #if MINIMAP_ENABLED
                fixed4 col = _DetectableColor;
            #endif

            #if !MINIMAP_ENABLED
                fixed4 col = fixed4(0, 0, 0 , 0);
            #endif

                return col;
            }

            ENDCG
        }
    }
}
