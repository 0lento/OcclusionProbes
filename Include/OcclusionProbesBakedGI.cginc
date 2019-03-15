float4 _AmbientProbeSH[7];

void SampleBakedGI_float(float3 normalWS, float skyOcclusion, out float3 bakedGI)
{
	float4 SHCoefficients[7];

	SHCoefficients[0] = unity_SHAr +_AmbientProbeSH[0] * skyOcclusion;
	SHCoefficients[1] = unity_SHAg + _AmbientProbeSH[1] * skyOcclusion;
	SHCoefficients[2] = unity_SHAb +_AmbientProbeSH[2] * skyOcclusion;
	SHCoefficients[3] = unity_SHBr + _AmbientProbeSH[3] * skyOcclusion;
	SHCoefficients[4] = unity_SHBg + _AmbientProbeSH[4] * skyOcclusion;
	SHCoefficients[5] = unity_SHBb + _AmbientProbeSH[5] * skyOcclusion;
	SHCoefficients[6] = unity_SHC + _AmbientProbeSH[6] * skyOcclusion;
	bakedGI = SampleSH9(SHCoefficients, normalWS);
}

