using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {

	[SerializeField] private Material[] skyMats;
	public static Material m;

	void Start () {
//		RenderSettings.skybox = skyMats[CurrentLevelMessage.Instance.levelIndex];
		RenderSettings.skybox = m;
	}
}
