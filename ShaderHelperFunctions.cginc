float radialGradient (float2 uv){
	return 1 - (distance(uv, 0.5) * 2);
}

float reflectedGradient (float uv){
	return 1 - (distance(uv, 0.5) * 2);
}

float diamondGradient (float2 uv){
	return 1 - (distance(uv.x, 0.5) * 2 + distance(uv.y, 0.5) * 2);
}

float borderGradient (float2 uv){
	return (1 - distance(uv.x, 0.5) * 2) * (1 - distance(uv.y, 0.5) * 2);
}

float angleGradient (float2 uv){
	fixed PI = 3.14159265359;
	
	return 1 - ((atan2(1 - uv.x - 0.5, 1 - uv.y - 0.5) + PI) / (2 * PI));
}

float waveGradient (float2 uv, float amplitude){
	fixed PI = 3.14159265359;
	
	return (uv.y * 0.5 + sin(uv.x * PI * 2) * amplitude) * 2;
}

float3 hue (float angle){
	fixed PI = 3.14159265359;
	angle = angle * PI * 2;
	
	float3 c;
	c.r = clamp(2/PI * asin(cos(angle)) * 1.5 + 0.5, 0, 1);
	c.g = clamp(2/PI * asin(cos(2 * PI * (1.0/3.0) - angle)) * 1.5 + 0.5, 0, 1);
	c.b = clamp(2/PI * asin(cos(2 * PI * (2.0/3.0) - angle)) *  1.5 + 0.5, 0, 1);
	return c;
}

float2 rotateUV (float2 uv, float angle){
	fixed PI = 3.14159265359;
			
	float sinRot = sin(angle * 2 * PI);
	float cosRot = cos(angle * 2 * PI);
	float2x2 rotMat = float2x2(cosRot, -sinRot, sinRot, cosRot);
	
	return (mul(uv - 0.5f, rotMat) + 0.5f);
}
