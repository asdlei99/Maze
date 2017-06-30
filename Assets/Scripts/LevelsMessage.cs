using UnityEngine;
using System.Collections;

public class LevelsMessage {
	public static int allLevelCount = 2;
	public static int allPassCount = 3;

	public static Vector3 level1BornPosition = new Vector3 (5f, 2.5f, 5f);
	public static Quaternion level1BodyRotation = new Quaternion (0.0f, -0.7f, 0.0f, -0.7f);
	public static string level1Name = "第一关";

	public static Vector3 level2BornPosition = new Vector3 (50f, 2.5f, 5f);
	public static Quaternion level2BodyRotation = new Quaternion (0.0f, -0.7f, 0.0f, -0.7f);
	public static string level2Name = "第二关";
}
