float3 g_light_pos : register( b1 );
float2 g_texture_size : register( b2 );
Texture2D g_texture : register( t0 );
Texture2D g_norm : register( t1 );
SamplerState g_sampler : register( s0 );

struct PS_Input
{
    float4 SV_Position : SV_POSITION;
    float4 Position : POSITION;
    float2 UV : UV;
    float4 Color : COLOR;
};

float4 main( const PS_Input Input ) : SV_Target
{
    float3 norm = normalize(g_norm.Sample(g_sampler, Input.UV).rgb*2 - 1);
    float3 pos = float3(Input.UV*g_texture_size, 0);
    float3 light = normalize(g_light_pos - pos);

    float3 diffuse = saturate(dot(norm, - light)*0.6 + 0.4);
    return float4(diffuse, 1)*g_texture.Sample(g_sampler, Input.UV);
}

