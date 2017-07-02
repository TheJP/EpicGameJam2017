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
         _Thickness("Thickness", float) = 20
     }
     SubShader 
     {
     
		Tags {  "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True"}
        //Tags {"RenderType"="Transparent" }
		Zwrite Off

		//Blend SrcFactor DstFactor
		Blend OneMinusDstAlpha One

		 //Blend Off
         Cull Back
		 //AlphaToMask On

		 //#2 render full size
		  Pass
         {
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
                 float3 normals : NORMAL;
             };
          
 
             v2f vert(appdata_base v)
             {
                 v2f OUT;
                 OUT.pos = UnityObjectToClipPos(v.vertex*(1 - _Thickness / 100));
                 OUT.uv = v.texcoord; 
                 OUT.normals = v.normal;
                 return OUT;
             }
             
             half4 frag(v2f IN) : COLOR
             {   
				return half4(0,0,0,1);
             }
             ENDCG
         }

		//#1 render full size
         Pass
         {
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_fog
             
             #include "UnityCG.cginc"

			 half4 _Color;

             struct v2f 
             {
                 float4  pos : SV_POSITION;
                 float2  uv : TEXCOORD0;
                 float3 normals : NORMAL;
             };
          
 
             v2f vert(appdata_base v)
             {
                 v2f OUT;
                 OUT.pos = UnityObjectToClipPos(v.vertex);
                 OUT.uv = v.texcoord; 
                 OUT.normals = v.normal;
                 return OUT;
             }
             
             half4 frag(v2f IN) : COLOR
             {   
                return _Color;
             }
             ENDCG
         }
		 
		 
     }
     FallBack "Sprites/Default"
 }