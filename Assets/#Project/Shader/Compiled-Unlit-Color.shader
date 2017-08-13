Shader "Unlit Single Color" {
    Properties {
        _Color ("Main Color", COLOR) = (1,1,1,1)
    }
    SubShader {
        Pass {
            Material {
                Diffuse [_Color]
            }
            Lighting Off
			Fog {Mode Off}
        }
    }
}