sampler3D _OcclusionProbes;
float4x4 _OcclusionProbesWorldToLocal;
sampler3D _OcclusionProbesDetail;
float4x4 _OcclusionProbesWorldToLocalDetail;

void SampleOcclusionProbes_float(float3 positionWS, out float occlusionProbes)
{
	occlusionProbes = 1;

	float3 pos = mul(_OcclusionProbesWorldToLocalDetail, float4(positionWS, 1)).xyz;

	if (all(pos > 0) && all(pos < 1))
	{
		occlusionProbes = tex3D(_OcclusionProbesDetail, pos).a;
	}
	else
	{
		pos = mul(_OcclusionProbesWorldToLocal, float4(positionWS, 1)).xyz;
		occlusionProbes = tex3D(_OcclusionProbes, pos).a;
	}
}