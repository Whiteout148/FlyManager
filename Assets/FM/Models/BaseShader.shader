
Shader "Custom/AnimeToon"
{
    Properties
    {
        _BaseMap ("Base Texture", 2D) = "white" {}
        _TintColor ("Tint Color", Color) = (1, 1, 1, 1)

        _OverlayTex ("Overlay Texture (UV)", 2D) = "white" {}
        _OverlayStrength ("Overlay Strength", Range(0, 1)) = 1

        _ShadowColor ("Shadow Color", Color) = (0.35, 0.35, 0.5, 1)
        _ShadowThreshold ("Shadow Threshold", Range(0, 1)) = 0.3

        _AmbientStrength ("Ambient Strength", Range(0, 1)) = 0.15
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile_fragment _ _SHADOWS_SOFT

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            TEXTURE2D(_OverlayTex);
            SAMPLER(sampler_OverlayTex);

            CBUFFER_START(UnityPerMaterial)

                float4 _BaseMap_ST;
                float4 _OverlayTex_ST;

                half4 _TintColor;
                half4 _ShadowColor;

                half _OverlayStrength;
                half _ShadowThreshold;
                half _AmbientStrength;

            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float2 overlayUV : TEXCOORD2;
                float4 shadowCoord : TEXCOORD3;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                VertexPositionInputs positionInputs =
                    GetVertexPositionInputs(IN.positionOS.xyz);

                VertexNormalInputs normalInputs =
                    GetVertexNormalInputs(IN.normalOS);

                OUT.positionCS = positionInputs.positionCS;
                OUT.normalWS = normalInputs.normalWS;
                OUT.positionCS = positionInputs.positionCS;

                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                OUT.overlayUV = TRANSFORM_TEX(IN.uv, _OverlayTex);

                OUT.shadowCoord = GetShadowCoord(positionInputs);

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Основная текстура
                half4 baseColor = SAMPLE_TEXTURE2D(
                    _BaseMap,
                    sampler_BaseMap,
                    IN.uv
                );

                // Цвет модели
                baseColor.rgb *= _TintColor.rgb;

                // Дополнительная текстура
                half4 overlay = SAMPLE_TEXTURE2D(
                    _OverlayTex,
                    sampler_OverlayTex,
                    IN.overlayUV
                );

                baseColor.rgb = lerp(
                    baseColor.rgb,
                    overlay.rgb,
                    overlay.a * _OverlayStrength
                );

                half3 normalWS = normalize(IN.normalWS);

                Light mainLight = GetMainLight(IN.shadowCoord);

                half NdotL = saturate(
                    dot(normalWS, mainLight.direction)
                );

                // Учитываем тени от объектов
                half lighting = NdotL * mainLight.shadowAttenuation;

                // Два цвета: основной и теневой
                half3 toonColor;

                if (lighting < _ShadowThreshold)
                {
                    toonColor = _ShadowColor.rgb;
                }
                else
                {
                    toonColor = _TintColor.rgb;
                }

                half3 ambient = SampleSH(normalWS) * _AmbientStrength;

                half3 finalColor =
                    baseColor.rgb *
                    (toonColor * mainLight.color + ambient);

                return half4(finalColor, baseColor.a);
            }

            ENDHLSL
        }

        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
    }
}
