Shader "Custom/TentacleSHader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Normal("Normal", 2D) = "bump" {}
        _Occlusion("Occlusion", 2D) = "white" {}
        _PulseAmount("Pulse amount", Range(0,0.5)) = 0.1 // metres
        _PulsePeriod("Pulse period", Float) = 1          // seconds
        _PulseTex("Pulse tex", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types

        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _Normal;
        sampler2D _Occlusion;
        float _PulsePeriod;
        float _PulseAmount;
        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        sampler2D _PulseTex;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert(inout appdata_full v)
        {
            // Position component
            float y = v.texcoord.xy + 0.5;

            // Time component
            const float TAU = 6.28318530718;
            float time = (sin(_Time.y / _PulsePeriod * TAU) + 1.0) / 2.0; // [0,1]

            // Final extrusion
            v.vertex.xyz += v.normal * time * _PulseAmount;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            o.Occlusion = tex2D(_Occlusion, IN.uv_MainTex).r;
            o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_MainTex));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
