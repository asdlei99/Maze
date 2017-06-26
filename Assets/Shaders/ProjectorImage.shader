// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'

Shader "Projector/Image" {
	Properties {
     _ShadowTex ("Cookie", 2D) = "" { }
  }
  Subshader {
     pass {
     	cull off
		Blend DstColor Zero
       	CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        sampler2D _ShadowTex;
        float4x4 unity_Projector;

        struct v2f {
            float4 pos: SV_POSITION;
            float4 texc: TEXCOORD0;
        };
        v2f vert(appdata_base v) {
            v2f o;
            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
            o.texc = mul(unity_Projector, v.vertex);
            return o;
        }
        float4 frag(v2f i):COLOR {
            float4 c = tex2Dproj(_ShadowTex, i.texc);
            return c;
        }
        ENDCG
    }
//    pass {
//    Cull Front 
//		Blend DstColor Zero
//       	CGPROGRAM
//        #pragma vertex vert
//        #pragma fragment frag
//        #include "UnityCG.cginc"
//
//        sampler2D _ShadowTex;
//        float4x4 unity_Projector;
//
//        struct v2f {
//            float4 pos: SV_POSITION;
//            float4 texc: TEXCOORD0;
//        };
//        v2f vert(appdata_base v) {
//            v2f o;
//            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//            o.texc = mul(unity_Projector, v.vertex);
//            return o;
//        }
//        float4 frag(v2f i):COLOR {
//            float4 c = tex2Dproj(_ShadowTex, i.texc);
//            return c;
//        }
//        ENDCG
//    }
  }
}