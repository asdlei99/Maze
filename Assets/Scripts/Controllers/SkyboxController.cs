using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {

	[SerializeField] private Material[] skyMats;

	void Start () {
		RenderSettings.skybox = skyMats[CurrentLevelMessage.Instance.levelIndex];
	}
}
