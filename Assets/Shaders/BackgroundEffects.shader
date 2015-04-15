Shader "Custom/BackgroundEffects"
{
  Properties {
    _NoiseTex ("Noise (RGB)", 2D) = "white" {}
    _Beat ("Beat", Float) = 0
  }
  SubShader {
    Pass {
      CGPROGRAM

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

#define TILES 16

        uniform float _Beat;
        uniform sampler2D _NoiseTex;

        struct vertOut {
          float4 pos:SV_POSITION;
          float4 scrPos;
        };

        float random(float2 uv)
        {
          float2 r = float2(23.14069263277926, 2.665144142690225);
          return cos(fmod(123456789.0, 256.0 * dot(uv, r)));
        }

        vertOut vert(appdata_base v) : POSITION {
          vertOut o;
          o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
          o.scrPos = ComputeScreenPos(o.pos);
          return o;
        }

        fixed4 frag(vertOut sp) : SV_Target {
          float2 uv = sp.scrPos.xy / sp.scrPos.w;
          //uv.x *= _ScreenParams.x / _ScreenParams.y;

          float4 noise = tex2D(_NoiseTex, floor(uv * float(TILES)) / float(TILES));
          float p = 1.0 - fmod(noise.r + noise.g + noise.b + _Beat * _Time, 1.0); //_Beat;
          p = min(max(p * 3.0 - 1.8, 0.1), 2.0);

          float2 r = fmod(uv * float(TILES), 1.0);
          r = float2(pow(r.x - 0.5, 2.0), pow(r.y - 0.5, 2.0));
          p *= 1.0 - pow(min(1.0, 12.0 * dot(r, r)), 2.0);

          //return fixed4(0.3, 0.7, 0.5, 0.1) * p * (_Beat + 0.2);
          return fixed4(0.7, 0.5, 0.2, 0.1) * p * (_Beat + 0.2) / 2.0;
        }

      ENDCG
    }
  }
}
