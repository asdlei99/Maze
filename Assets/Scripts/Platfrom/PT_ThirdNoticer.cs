using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

/// <summary>
/// 平台交互类。
/// 所有平台向Unity发送消息的统一入口。
/// 提供统一的外部调用函数，用于字符串的传递。
/// 用法：
///     1、游戏初始化时，将PT_ThirdNoticer.prefab加入到游戏中。
///     
/// </summary>
public class PT_ThirdNoticer : MonoBehaviour {
    private  static Dictionary<string, PCallback<string>> mThirdDataCache = new Dictionary<string, PCallback<string>>();  //保存所有平台发送过来的消息数据

	void Awake() {
        DontDestroyOnLoad(gameObject);  //常驻
	}

    /// <summary>
    /// 初始化监听对象
    /// </summary>
    public static void InitThirdNoticer() {
        GameObject obj = new GameObject();
        obj.name = "PT_ThirdNoticer";
        obj.AddComponent<PT_ThirdNoticer>();
    }

    public static void RegisterMessageCallback(string callbackName, PCallback<string> cb) {
		if (callbackName == null || cb == null) {
			return;
		}
		if (mThirdDataCache.ContainsKey (callbackName)) {
			mThirdDataCache [callbackName] = cb;
		} else {
			mThirdDataCache.Add (callbackName, cb);
		}
	}

	public static void UnRegisterMessageCallback(string callbackName) {
		if (callbackName == null) {
			return;
		}
		if (mThirdDataCache.ContainsKey (callbackName)) {
			mThirdDataCache.Remove (callbackName);
		} else {
			Debug.LogError ("PT_ThirdNoticer RemoveMessageCallback failed. Can not find callbackName," + callbackName);
		}
	}

    public static void RemoveAllMessageCallback() {
        mThirdDataCache.Clear();
    }

    /// <summary>
    /// Demo:平台向Unity发送成功消息。
    /// </summary>
    /// <param name="content"></param>
    void OnGetMessageSuc(string content) {
		//TODO 可以将content统一接口
		object json;
		if (SimpleJson.SimpleJson.TryDeserializeObject (content, out json)) {
			JsonObject obj = (JsonObject)json;
			string callbackName = obj ["callbackName"] as string;
			string messageContent = obj ["messageContent"] as string;
			if (mThirdDataCache.ContainsKey (callbackName)) {
				PCallback<string> cb = mThirdDataCache [callbackName];
				if (cb != null) {
					cb (messageContent);
				}
			}
		}
	}
}
