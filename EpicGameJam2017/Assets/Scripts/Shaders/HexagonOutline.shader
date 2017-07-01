// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Outline/HexagonOutline" 
 {
    /*
    basically this post from unity forums
    http://answers.unity3d.com/questions/60155/is-there-a-shader-to-only-add-an-outline.html
    with some minor adaptions
    */
     Properties 
     {
         _Color("Color", Color) = (1,0,0,1)
         _Thickness("Thickness", float) = 1
     }
     SubShader 
     {
     
         Tags { "RenderType"="Transparent" }
         Blend SrcAlpha OneMinusSrcAlpha
         Cull Back
         ZTest always
         Pass
         {
			  Stencil {
                 Ref 1
                 Comp always
                 Pass replace
             }

             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_fog
             
             #include "UnityCG.cginc"

             half4 _Color;
             float _Thickness;
             
             struct v2f 
             {
                 float4  pos : SV_POSITION;
                 float2  uv : TEXCOORD0;
                 float3 viewT : TANGENT;
                 float3 normals : NORMAL;
                 float3 localPos : TEXCOORD1;
                 float3 localPosOutline : TEXCOORD2;

             };
          
 
             v2f vert(appdata_base v)
             {
                 v2f OUT;
                 OUT.localPos = v.vertex;
                 OUT.localPosOutline = v.vertex * (1-_Thickness/100);
                 OUT.pos = UnityObjectToClipPos(v.vertex);
                 OUT.uv = v.texcoord; 
                 OUT.normals = v.normal;
                 OUT.viewT = ObjSpaceViewDir(v.vertex);
                 
                 return OUT;
             }
             
             half4 frag(v2f IN) : COLOR
             {              
                if()
                
                 return _Color;
             }
             ENDCG
         }
     }
     FallBack "Sprites/Default"
 }