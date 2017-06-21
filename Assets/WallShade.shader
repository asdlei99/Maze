Shader "Unlit/WallShader"
{
	Properties {  
    _Color ("Main Color", Color) = (1,1,1,1)//Tint Color  
    _MainTex ("Base (RGB)", 2D) = "white" {}  
    _MainTex_2 ("Base (RGB)", 2D) = "white" {}  
}  
  
SubShader {  
    Tags { "RenderType"="Opaque" }  
    LOD 100  
      
    Pass {  
        Cull Front  
        Lighting Off  
        SetTexture [_MainTex] { combine texture }   
        SetTexture [_MainTex]  
        {  
            ConstantColor [_Color]  
            Combine Previous * Constant  
        }  
    }  
  
    Pass  
    {  
        Cull Back  
        Lighting Off  
        SetTexture [_MainTex_2] { combine texture }   
        SetTexture [_MainTex_2]  
        {  
            ConstantColor [_Color]  
            Combine Previous * Constant  
        }  
    }  
}  
}