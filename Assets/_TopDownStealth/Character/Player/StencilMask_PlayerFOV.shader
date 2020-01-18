Shader "Custom/StencilMask/PlayerFOV"
{
    Properties
    {
        [IntRange] _StencilRef("Stencil Reference", Range(0, 255)) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp("Stencil Comparison", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp("Stencil Operation", Int) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry-100"
        }
		
		Stencil
		{
			Ref [_StencilRef]
            Comp [_StencilComp]
			Pass [_StencilOp]
		}

        Pass
        {
            // don't draw color or depth
            // ColorMask 0
            Blend Zero One
            ZWrite Off

            CGPROGRAM
            
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag
            
            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                return 0;
            }

            ENDCG
        }
    }
}
