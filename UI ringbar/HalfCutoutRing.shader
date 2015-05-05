//Copyright (c) 2015, Felix Kate All rights reserved.

Shader "GUI Shaders / Half Cutout Ring" {
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
		float _Percentage;
		
		struct Input {
			float2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {
			//Define Pi
			fixed PI = 3.14159265359;
			
			//Make the rotation matrix for the gradient
			float sinRot = sin(_Percentage * 2 * PI);
			float cosRot = cos(_Percentage * 2 * PI);
			float2x2 rotMat = float2x2(cosRot, -sinRot, sinRot, cosRot);

			//Take the x uv coordinates as horizontal gradient
			fixed gradient = round(IN.uv_MainTex.x);
								
			//Turn the uv coordinates to get a second turned gradient (full turn over 0 - 1 of _Percentage)
			fixed turnedGradient = round((mul(IN.uv_MainTex - 0.5f, rotMat)+ 0.5f).x);

			//Get the texture and apply it to the emission and alpha
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

			o.Emission = c.rgb;
			o.Alpha = c.a;
			
			//Check if percentage is over 0.5 (Use the unrotated gradient to make sure only one side gets affected by the rotated one)
			if(_Percentage >= 0.5){
				//Unrotated gradient is ensures that the left side is always visible
				o.Alpha *= 1 - gradient + gradient * turnedGradient;
			}else{
				//Unrotated gradient cuts away the right side
				o.Alpha *= (1 - gradient) * turnedGradient;
			}

		}
		ENDCG
	}
}