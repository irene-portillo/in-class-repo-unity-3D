Shader "Unlit/UnlitShaderBasic"
{
    Properties {}
    SubShader
    {
        Tags { "RenderType"="Opaque" } // type of object + type of pipeline
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert // defines the function `vert` and assign it to the VERTEX shader
            #pragma fragment frag // defines the function `frag` and assign it to the fragment shader

            #include "UnityCG.cginc" // include unity functions

            struct appdata
            {
                float4 vertex : POSITION; // in take a vertex position
            };

            struct v2f
            {
                float4 vertex : SV_POSITION; // gives the fragment shader an position in clipspace 
            };

            // the vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            // the fragment shader
            fixed4 frag (v2f i) : SV_Target
            {
                return float4(1,.2,0,1);
            }
            ENDCG
        }
    }
}
