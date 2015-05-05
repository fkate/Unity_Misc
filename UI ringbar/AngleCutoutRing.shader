//Copyright (c) 2015, Felix Kate All rights reserved.

Shader "GUI Shaders / Angle Cutout Ring" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Percentage ("Percentage", Range(0, 1)) = 1
	}

	SubShader {
		Lighting Off
		
		CGPROGRAM
		#pragma surface surf Unlit alpha
		#include "UnityCG.cginc" 
		
		//Since I use the emmisve channel the rgb will be 0 and only the alpha will be used
		fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten){
		    fixed4 c;
		    c.rgb = 0;
		    c.a = s.Alpha;
		    
		    return c;
		}
		
		sampler2D _MainTex;
		fixed _Percentage;
		
		struct Input {
			fixed2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {
			//Make an angle gradient from the uvs(Don't use a texture for this use uvs to get sharp edges)
			fixed PI = 3.14159265359;
			
		    fixed angleGradient = (atan2(1 - IN.uv_MainTex.x - 0.5, 1 - IN.uv_MainTex.y - 0.5) + PI) / (2 * PI);
			
			//Get the texture and apply it to the emission and alpha
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

			o.Emission = c.rgb;
			o.Alpha = c.a;
			
			//Combine the angle gradient with the percentage and clamp it to 0 - 1 range
			fixed cutoff = clamp(angleGradient + _Percentage, 0, 1);
			
			//And use it to cut the alpha off
			o.Alpha *= floor(cutoff);
			
	
		}
		ENDCG
	}
}