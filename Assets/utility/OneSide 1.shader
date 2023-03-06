Shader "Unlit/OneSide"
{
 
    Properties{ 
        _MainTex("Font Texture", 2D) = "white" {} 
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 0
    }
    SubShader
    { 
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" } 
            Lighting Off ZWrite Off 
            Cull [_Cull]
            Pass
            { 
                CGPROGRAM
                #pragma vertex vertexFunc
                #pragma fragment fragmentFunc

                #include "UnityCG.cginc"



                sampler2D _MainTex;

                struct v2f{

                    float4 position:SV_POSITION;
                    float2 uv:TEXCOORD0;
                };

                v2f vertexFunc (appdata_base v){

                    v2f o;
                    o.position=UnityObjectToClipPos(v.vertex);
                    o.uv=v.texcoord;
                    return o;

                }

                fixed4 fragmentFunc(v2f i):COLOR
                {
                    half4 c=tex2D(_MainTex, i.uv);
                    return c;
                }

                ENDCG
            } 
    } 

}
