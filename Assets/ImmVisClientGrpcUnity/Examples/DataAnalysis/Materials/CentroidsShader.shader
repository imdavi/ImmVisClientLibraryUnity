Shader "ImmVis/CentroidsScatterplotShader" 
{
    Properties{
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
    SubShader{
        Tags{ "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard addshadow
        #pragma multi_compile_instancing
        #pragma instancing_options procedural:setup

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            StructuredBuffer<float3> positionsBuffer;
            StructuredBuffer<float4> colorsBuffer;
            StructuredBuffer<float> sizesBuffer;
            float4x4 _TransformMatrix;
            float _PointMinimumSize;
            float _PointsAlpha;
        #endif

        void setup()
        {
            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                float3 data = positionsBuffer[unity_InstanceID];

                data.xyz = mul(_TransformMatrix, float4(data.xyz, 1)).xyz;

                float pointSize = sizesBuffer[unity_InstanceID];
                float pointPlotSize = _PointMinimumSize * (pointSize + 2);

                unity_ObjectToWorld._11_21_31_41 = float4(pointPlotSize, 0, 0, 0);
                unity_ObjectToWorld._12_22_32_42 = float4(0, pointPlotSize, 0, 0);
                unity_ObjectToWorld._13_23_33_43 = float4(0, 0, pointPlotSize, 0);
                unity_ObjectToWorld._14_24_34_44 = float4(data.xyz, 1);
                unity_WorldToObject = unity_ObjectToWorld;
                unity_WorldToObject._14_24_34 *= -1;
                unity_WorldToObject._11_22_33 = 1.0f / unity_WorldToObject._11_22_33;
            #endif
        }

        half _Glossiness;
        half _Metallic;

        void surf(Input IN, inout SurfaceOutputStandard o) 
        {
            float4 col = float4(1, 1, 1, 1);

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * col;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
