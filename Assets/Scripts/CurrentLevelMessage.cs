using UnityEngine;
using System.Collections;

public class CurrentLevelMessage {
	private static CurrentLevelMessage instance = null;

	public int levelIndex;
	public Vector3 bornPosition;
	public string name;

	public static CurrentLevelMessage Instance{
		get{
			if (instance == null) {
				instance = new CurrentLevelMessage ();
			}
			return instance;
		}
	}
}