Shader "Hidden/Custom/depthfog"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        sampler2D _CameraDepthTexture;
        float4 _FogColor;
        float fardis;
        float closedis;

        float4 Frag(VaryingsDefault i) : SV_Target
        {

            float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
            float depth = Linear01Depth(tex2D(_CameraDepthTexture, i.texcoord).r);
            //float4 worldPos = mul(unity_ObjectToWorld, i.texcoord);
            float4 result;
            if(depth == 1)
                return color;
            if(depth > fardis)
            {
                return _FogColor;
            }
            if(depth < closedis)
                return color;
            depth = (depth-closedis)/(fardis - closedis);
            result = lerp(color, _FogColor, depth);
            return result;

        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}