using UnityEngine;
using System.Collections;

public class Player {

	private static Player instance = null;

	public string uid;
	public int maxLevel;

	public static Player Instance{
		get{
			if (instance == null) {
				instance = new Player ();
			}
			return instance;
		}
	}
}
