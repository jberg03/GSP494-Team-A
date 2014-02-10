using UnityEngine;
using System.Collections;
//
// you chave to just put this script on object that uses ReliefTerrainVertexBlendTriplanar shader in it's renderer
//
[ExecuteInEditMode]
public class ReliefTerrainVertexBlendTriplanar : MonoBehaviour {

	public void SetupMIPOffsets() {
		SetupTex("_SplatA0", "rtp_mipoffset_color");
		SetupTex("_BumpMap01", "rtp_mipoffset_bump");
		SetupTex("_TERRAIN_HeightMap", "rtp_mipoffset_height");
		if (renderer.sharedMaterial.HasProperty("_BumpMapGlobal")) {
			SetupTex("_BumpMapGlobal", "rtp_mipoffset_globalnorm", renderer.sharedMaterial.GetFloat("_BumpMapGlobalScale"), renderer.sharedMaterial.GetFloat("rtp_mipoffset_globalnorm_offset"));
			if (renderer.sharedMaterial.HasProperty("_SuperDetailTiling")) {
				SetupTex("_BumpMapGlobal", "rtp_mipoffset_superdetail", renderer.sharedMaterial.GetFloat("_SuperDetailTiling"));
			}
			if (renderer.sharedMaterial.HasProperty("TERRAIN_FlowScale")) {
				SetupTex("_BumpMapGlobal", "rtp_mipoffset_flow", renderer.sharedMaterial.GetFloat("TERRAIN_FlowScale"), renderer.sharedMaterial.GetFloat("TERRAIN_FlowMipOffset"));
			}
		}
		if (renderer.sharedMaterial.HasProperty("TERRAIN_RippleMap")) {
			SetupTex("TERRAIN_RippleMap", "rtp_mipoffset_ripple", renderer.sharedMaterial.GetFloat("TERRAIN_RippleScale"));
		}
	}

	private void SetupTex(string tex_name, string param_name, float _mult=1, float _add=0) {
		int tex_width=512;
		if (renderer.sharedMaterial.GetTexture(tex_name)!=null) {
			tex_width=renderer.sharedMaterial.GetTexture(tex_name).width;
		}
		renderer.sharedMaterial.SetFloat(param_name, -Mathf.Log(1024.0f/(tex_width*_mult))/Mathf.Log(2) + _add );
	}

	void Awake() {
		SetupMIPOffsets();
	}
	void Update () {
		if (!Application.isPlaying) {
			SetupMIPOffsets();
		}
	}
}
