using UnityEngine;
using System.Collections;
/// <summary>
/// Android基础交互类
/// </summary>
public class PAndroidInterface
{
	/// <summary>
	/// JNI 调用静态方法
	/// </summary>
	/// <param name="className">类路径名</param>
	/// <param name="methodName">方法名</param>
	/// <param name="methodParams">方法参数</param>
	public static void CallStaticMethod(string className, string methodName, params object[] methodParams)
	{
#if UNITY_ANDROID && !UNITY_EDITOR

		AndroidJavaClass clazz = null;
		using (clazz = new AndroidJavaClass(className))
        {
			clazz.CallStatic(methodName, methodParams);
        }
		clazz.Dispose();
		clazz = null;
#endif
	}

	/// <summary>
	/// JNI 调用静态方法
	/// </summary>
	/// <param name="className">类路径名</param>
	/// <param name="methodName">方法名</param>
	/// <param name="methodParams">方法参数</param>
	public static T CallStaticMethod<T>(string className, string methodName, params object[] methodParams)
	{
		T res = default(T);
#if UNITY_ANDROID && !UNITY_EDITOR

		AndroidJavaClass clazz = null;
		using (clazz = new AndroidJavaClass(className))
		{
			res = clazz.CallStatic<T>(methodName, methodParams);
		}
		clazz.Dispose();
		clazz = null;
		
#endif
		return res;
	}

	/// <summary>
	/// JNI 调用实例方法
	/// </summary>
	/// <param name="className">类路径名</param>
	/// <param name="methodName">方法名</param>
	/// <param name="methodParams">方法参数</param>
	public static void CallMethod(string className, string methodName,params object[] methodParams)
	{
#if UNITY_ANDROID && !UNITY_EDITOR

		AndroidJavaObject clazzObj = null;
		using (clazzObj = new AndroidJavaObject(className))
		{
			clazzObj.Call(methodName, methodParams);
		}
		clazzObj.Dispose();
		clazzObj = null;
		
#endif
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	/// <summary>
	/// JNI 调用实例方法
	/// </summary>
	/// <param name="clazzObject">类</param>
	/// <param name="methodName">方法名</param>
	/// <param name="methodParams">方法参数</param>
	public static void CallMethod(AndroidJavaObject clazzObject, string methodName, params object[] methodParams)
	{
		clazzObject.Call(methodName, methodParams);
	}
#endif

	/// <summary>
	/// JNI 调用实例方法
	/// </summary>
	/// <param name="className">类路径名</param>
	/// <param name="methodName">方法名</param>
	/// <param name="methodParams">方法参数</param>
	public static T CallMethod<T>(string className, string methodName, params object[] methodParams)
	{
		T res = default(T);
#if UNITY_ANDROID && !UNITY_EDITOR

		AndroidJavaObject clazzObj = null;
		using (clazzObj = new AndroidJavaObject(className))
		{
			res = clazzObj.Call<T>(methodName, methodParams);
		}
		clazzObj.Dispose();
		clazzObj = null;
#endif
		return res;
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	/// <summary>
	/// JNI 调用实例方法
	/// </summary>
	/// <param name="clazzObject">类</param>
	/// <param name="methodName">方法名</param>
	/// <param name="methodParams">方法参数</param>
	public static T CallMethod<T>(AndroidJavaObject clazzObject, string methodName, params object[] methodParams)
	{
		T res = default(T);
	    res = clazzObject.Call<T>(methodName, methodParams);
		
		return res;
	}
#endif

	/// <summary>
	/// JNI 获取静态属性值
	/// </summary>
	/// <param name="className">类路径</param>
	/// <param name="filedName">静态属性名</param>
	public static T GetStaticField<T>(string className, string fieldName)
	{
		T res = default(T);
#if UNITY_ANDROID && !UNITY_EDITOR
		
	    AndroidJavaClass clazz = null;
		using (clazz = new AndroidJavaClass(className))
		{
			res = clazz.GetStatic<T>(fieldName);
		}
		clazz.Dispose();
		clazz = null;
#endif
		return res;
	}

	/// <summary>
	/// JNI 获取属性值
	/// </summary>
	/// <param name="className">类路径</param>
	/// <param name="filedName">静态属性名</param>
	public static T GetField<T>(string className, string fieldName)
	{
		T res = default(T);
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject clazzObj = null;
		using (clazzObj = new AndroidJavaObject(className))
		{
			res = clazzObj.Get<T>(fieldName);
		}
		clazzObj.Dispose();
		clazzObj = null;
		
#endif
		return res;
	}

	/// <summary>
	/// JNI 设置静态属性值
	/// </summary>
	/// <param name="className">类路径</param>
	/// <param name="fieldName">属性名</param>
	/// <param name="fieldValue">属性值</param>
	public static void SetStaticField<T>(string className, string fieldName, T fieldValue)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass clazz = null;
		using (clazz = new AndroidJavaClass(className))
		{
			clazz.SetStatic<T>(fieldName, fieldValue);
		}
		clazz.Dispose();
		clazz = null;
#endif
	}

	/// <summary>
	/// JNI 设置属性值
	/// </summary>
	/// <param name="className">类路径</param>
	/// <param name="fieldName">属性名</param>
	/// <param name="fieldValue">属性值</param>
	public static void SetField<T>(string className, string fieldName, T fieldValue)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject clazzObj = null;
		using (clazzObj = new AndroidJavaObject(className))
		{
			clazzObj.Set<T>(fieldName, fieldValue);
		}
		clazzObj.Dispose();
		clazzObj = null;
#endif
	}

	public static void CallCurrentActivity(string methodName, params object[] methodParams)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject clazz = null;
		using (clazz = PAndroidInterface.GetStaticField<AndroidJavaObject>(PClazzNames.ANDROID_CURRENT_ACTIVITY, "currentActivity"))
		{
			clazz.Call(methodName, methodParams);
		}
		clazz.Dispose();
		clazz = null;
#endif
	}

	public static T CallCurrentActivity<T>(string methodName, params object[] methodParams)
	{
		T res = default(T);

#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaObject clazz = null;
		using (clazz = PAndroidInterface.GetStaticField<AndroidJavaObject>(PClazzNames.ANDROID_CURRENT_ACTIVITY, "currentActivity"))
		{
			res = clazz.Call<T>(methodName, methodParams);
		}
		clazz.Dispose();
		clazz = null;
#endif

		return res;
	}
}
