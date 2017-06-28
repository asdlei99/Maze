using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

public class CurrentLevelMessage {
	private static CurrentLevelMessage instance = null;
	private const string fileName = "MapInfo.txt";

	public int levelIndex = 0;
	public Vector3 bornPosition;
	public Quaternion bodyRotation;
	public Quaternion headRotation;
	public string name;

	public class ProjectorMessage {
		public ProjectorMessage(){
			position = new Vector3(MazeTool.errorFloat, 0, 0);
			rotation = new Quaternion(MazeTool.errorFloat, 0, 0, 0);
			type = PaintType.Paint0;
		}
		public Vector3 position;
		public Quaternion rotation;
		public PaintType type;
	}
	public List<ProjectorMessage> projectorMessageList = new List<ProjectorMessage>();

	public static CurrentLevelMessage Instance{
		get{
			if (instance == null) {
				instance = new CurrentLevelMessage ();
			}
			return instance;
		}
	}

	public void Save(){
		JsonObject currentLevelMessageJson = new JsonObject();
		currentLevelMessageJson ["levelIndex"] = levelIndex;
		currentLevelMessageJson ["bornPosition"] = bornPosition.ToString();
		currentLevelMessageJson ["bodyRotation"] = bodyRotation.ToString();
		currentLevelMessageJson ["headRotation"] = headRotation.ToString();

		JsonArray projectorMessageArray = new JsonArray ();
		for (int i = 0; i < projectorMessageList.Count; i++) {
			JsonObject projectorMessageJson = new JsonObject();
			projectorMessageJson ["position"] = projectorMessageList[i].position.ToString();
			projectorMessageJson ["rotation"] = projectorMessageList[i].rotation.ToString();
			projectorMessageJson ["type"] = projectorMessageList[i].type.ToString();
			projectorMessageArray.Add (projectorMessageJson);
		}
		currentLevelMessageJson ["projectorMessageList"] = projectorMessageArray;

		FileTool fileTool = new FileTool();
		fileTool.WriteFile (Application.persistentDataPath, fileName, currentLevelMessageJson.ToString ());
	}

	public void Init(){
		projectorMessageList.Clear ();
		levelIndex = 1;
		bornPosition = new Vector3(MazeTool.errorFloat, 0, 0);
		bodyRotation = new Quaternion(MazeTool.errorFloat, 0, 0, 0);
		headRotation = new Quaternion(0, 0, 0, 1);

		FileTool fileTool = new FileTool ();
		ArrayList list = fileTool.LoadFile (Application.persistentDataPath, fileName);
		if (list != null) {
			object msgObj = new object ();
			if (SimpleJson.SimpleJson.TryDeserializeObject (list [0].ToString (), out msgObj)) {
				JsonObject msgJson = (JsonObject)msgObj;
				object levelIndexObj;
				if (msgJson.TryGetValue ("levelIndex", out levelIndexObj)) { 
					int.TryParse (levelIndexObj.ToString (), out levelIndex);
				}

				object bornPositionObj;
				if (msgJson.TryGetValue ("bornPosition", out bornPositionObj)) { 
					bornPosition = MazeTool.StringToVector3 (bornPositionObj.ToString());
				}

				object bodyRotationObj;
				if (msgJson.TryGetValue ("bodyRotation", out bodyRotationObj)) { 
					bodyRotation = MazeTool.StringToQuaternion (bodyRotationObj.ToString());
				}

				object headRotationObj;
				if (msgJson.TryGetValue ("headRotation", out headRotationObj)) { 
					headRotation = MazeTool.StringToQuaternion (headRotationObj.ToString());
				}

				object projectorMessageListObj;
				if (msgJson.TryGetValue ("projectorMessageList", out projectorMessageListObj)) { 
					JsonArray projectorMessageArray = (JsonArray)projectorMessageListObj;
					for (int i = 0; i < projectorMessageArray.Count; i++) {
						JsonObject json = (JsonObject)projectorMessageArray [i];
						ProjectorMessage pm = new ProjectorMessage();
						object positionObj;
						if (json.TryGetValue ("position", out positionObj)) { 
							pm.position = MazeTool.StringToVector3 (positionObj.ToString());
						}

						object rotationObj;
						if (json.TryGetValue ("rotation", out rotationObj)) { 
							pm.rotation = MazeTool.StringToQuaternion (rotationObj.ToString());
						}

						object typeObj;
						if (json.TryGetValue ("type", out typeObj)) { 
							pm.type = (PaintType)System.Enum.Parse (typeof(PaintType), typeObj.ToString());
						}
						projectorMessageList.Add (pm);
					}
				} 
			}
		}
		Log ();
	}

	public void Log(){
		Debug.Log ("CurrentLevelMessage---levelIndex:" + levelIndex + "\t bornPosition:" + bornPosition + "\t bodyRotation:" + bodyRotation
		+ "\t headRotation:" + headRotation + "\t name:" + name);
		if (projectorMessageList.Count > 0) {
			Debug.Log ("projectorMessageList Count:" + projectorMessageList.Count);
			for (int i = 0; i < projectorMessageList.Count; i++) {
				Debug.Log ("projectorMessage---position:" + projectorMessageList [i].position
				+ "\t rotation:" + projectorMessageList [i].rotation
				+ "\t type:" + projectorMessageList [i].type);
			}
		}
	}
}