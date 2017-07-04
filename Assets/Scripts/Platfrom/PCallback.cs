/// <summary>
/// 平台回调类
/// </summary>
public delegate void PCallback();
public delegate void PCallback<T>(T arg1);
public delegate void PCallback<T, U>(T arg1, U arg2);
public delegate void PCallback<T, U, V>(T arg1, U arg2, V arg3);