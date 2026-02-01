Shader "Custom/URP/StencilMask"
{
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Geometry-1"
        }

        Pass
        {
            ZWrite Off
            ColorMask 0

            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
        }
    }
}
