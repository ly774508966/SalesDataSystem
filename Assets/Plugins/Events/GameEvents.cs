using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 游戏事件调度器
/// </summary>
public class GameEvents
{
    private static Dictionary<eGameEventTypes, Delegate> eventsDict = new Dictionary<eGameEventTypes, Delegate>();

    private static void OnAddGameEvent(eGameEventTypes type, Delegate d)
    {
        if (!eventsDict.ContainsKey(type))
        {
            eventsDict.Add(type, null);
        }
        Delegate ed = eventsDict[type];
        if (ed != null && ed.GetType() != d.GetType())
        {
            Debug.LogError(string.Format("添加监听事件错误,EventType {0}  添加的事件类型{1},已存在事件类型{2}", type.ToString(), d.GetType().Name, ed.GetType().Name));
        }
    }

    private static void OnGameEventRemoved(eGameEventTypes gType)
    {
        if (eventsDict[gType] == null)
        {
            eventsDict.Remove(gType);
        }
    }

    public static void AddGameEvent(eGameEventTypes gType, GameCallBack callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack)eventsDict[gType] + callBack;
    }

    public static void AddGameEvent<T>(eGameEventTypes gType, GameCallBack<T> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T>)eventsDict[gType] + callBack;
    }

    public static void AddGameEvent<T, U>(eGameEventTypes gType, GameCallBack<T, U> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U>)eventsDict[gType] + callBack;
    }

    public static void AddGameEvent<T, U, V>(eGameEventTypes gType, GameCallBack<T, U, V> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V>)eventsDict[gType] + callBack;
    }

    public static void AddGameEvent<T, U, V, X>(eGameEventTypes gType, GameCallBack<T, U, V, X> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V, X>)eventsDict[gType] + callBack;
    }

    public static void AddGameEvent<T, U, V, X, Y>(eGameEventTypes gType, GameCallBack<T, U, V, X, Y> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V, X, Y>)eventsDict[gType] + callBack;
    }

    public static void AddGameEvent<T, U, V, X, Y, Z>(eGameEventTypes gType, GameCallBack<T, U, V, X, Y, Z> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V, X, Y, Z>)eventsDict[gType] + callBack;
    }

    public static void AddGameEvent<T, U, V, X, Y, Z, M>(eGameEventTypes gType, GameCallBack<T, U, V, X, Y, Z, M> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V, X, Y, Z, M>)eventsDict[gType] + callBack;
    }

    public static void RemoveGameEvent(eGameEventTypes gType, GameCallBack callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack)eventsDict[gType] - callBack;
        OnGameEventRemoved(gType);
    }

    public static void RemoveGameEvent<T>(eGameEventTypes gType, GameCallBack<T> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T>)eventsDict[gType] - callBack;
        OnGameEventRemoved(gType);
    }

    public static void RemoveGameEvent<T, U>(eGameEventTypes gType, GameCallBack<T, U> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U>)eventsDict[gType] - callBack;
        OnGameEventRemoved(gType);
    }

    public static void RemoveGameEvent<T, U, V>(eGameEventTypes gType, GameCallBack<T, U, V> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V>)eventsDict[gType] - callBack;
        OnGameEventRemoved(gType);
    }

    public static void RemoveGameEvent<T, U, V, X>(eGameEventTypes gType, GameCallBack<T, U, V, X> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V, X>)eventsDict[gType] - callBack;
        OnGameEventRemoved(gType);
    }

    public static void RemoveGameEvent<T, U, V, X, Y>(eGameEventTypes gType, GameCallBack<T, U, V, X, Y> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V, X, Y>)eventsDict[gType] - callBack;
        OnGameEventRemoved(gType);
    }

    public static void RemoveGameEvent<T, U, V, X, Y, Z>(eGameEventTypes gType, GameCallBack<T, U, V, X, Y, Z> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V, X, Y, Z>)eventsDict[gType] - callBack;
        OnGameEventRemoved(gType);
    }

    public static void RemoveGameEvent<T, U, V, X, Y, Z, M>(eGameEventTypes gType, GameCallBack<T, U, V, X, Y, Z, M> callBack)
    {
        OnAddGameEvent(gType, callBack);
        eventsDict[gType] = (GameCallBack<T, U, V, X, Y, Z, M>)eventsDict[gType] - callBack;
        OnGameEventRemoved(gType);
    }

    public static void TriggerEvent(eGameEventTypes gType)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d)) return;
        GameCallBack callback = (GameCallBack)d;
        if (callback != null)
        {
            callback();
        }
    }

    public static void TriggerCheckEvent(eGameEventTypes gType)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d, true)) return;
        GameCallBack callback = (GameCallBack)d;
        if (callback != null)
        {
            callback();
        }
    }

    public static void TriggerEvent<T>(eGameEventTypes gType, T arg1)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d)) return;
        GameCallBack<T> callback = (GameCallBack<T>)d;
        if (callback != null)
        {
            callback(arg1);
        }
    }

    public static void TriggerCheckEvent<T>(eGameEventTypes gType, T arg1)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d, true)) return;
        GameCallBack<T> callback = (GameCallBack<T>)d;
        if (callback != null)
        {
            callback(arg1);
        }
    }

    public static void TriggerEvent<T, U>(eGameEventTypes gType, T arg1, U arg2)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d)) return;
        GameCallBack<T, U> callback = (GameCallBack<T, U>)d;
        if (callback != null)
        {
            callback(arg1, arg2);
        }
    }

    public static void TriggerCheckEvent<T, U>(eGameEventTypes gType, T arg1, U arg2)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d, true)) return;
        GameCallBack<T, U> callback = (GameCallBack<T, U>)d;
        if (callback != null)
        {
            callback(arg1, arg2);
        }
    }

    public static void TriggerEvent<T, U, V>(eGameEventTypes gType, T arg1, U arg2, V arg3)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d)) return;
        GameCallBack<T, U, V> callback = (GameCallBack<T, U, V>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3);
        }
    }

    public static void TriggerCheckEvent<T, U, V>(eGameEventTypes gType, T arg1, U arg2, V arg3)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d, true)) return;
        GameCallBack<T, U, V> callback = (GameCallBack<T, U, V>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3);
        }
    }

    public static void TriggerEvent<T, U, V, X>(eGameEventTypes gType, T arg1, U arg2, V arg3, X arg4)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d)) return;
        GameCallBack<T, U, V, X> callback = (GameCallBack<T, U, V, X>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3, arg4);
        }
    }

    public static void TriggerCheckEvent<T, U, V, X>(eGameEventTypes gType, T arg1, U arg2, V arg3, X arg4)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d, true)) return;
        GameCallBack<T, U, V, X> callback = (GameCallBack<T, U, V, X>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3, arg4);
        }
    }

    public static void TriggerEvent<T, U, V, X, Y>(eGameEventTypes gType, T arg1, U arg2, V arg3, X arg4, Y arg5)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d)) return;
        GameCallBack<T, U, V, X, Y> callback = (GameCallBack<T, U, V, X, Y>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3, arg4, arg5);
        }
    }

    public static void TriggerCheckEvent<T, U, V, X, Y>(eGameEventTypes gType, T arg1, U arg2, V arg3, X arg4, Y arg5)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d, true)) return;
        GameCallBack<T, U, V, X, Y> callback = (GameCallBack<T, U, V, X, Y>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3, arg4, arg5);
        }
    }

    public static void TriggerEvent<T, U, V, X, Y, Z>(eGameEventTypes gType, T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d)) return;
        GameCallBack<T, U, V, X, Y, Z> callback = (GameCallBack<T, U, V, X, Y, Z>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    public static void TriggerCheckEvent<T, U, V, X, Y, Z>(eGameEventTypes gType, T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d, true)) return;
        GameCallBack<T, U, V, X, Y, Z> callback = (GameCallBack<T, U, V, X, Y, Z>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    public static void TriggerEvent<T, U, V, X, Y, Z, M>(eGameEventTypes gType, T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, M arg7)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d)) return;
        GameCallBack<T, U, V, X, Y, Z, M> callback = (GameCallBack<T, U, V, X, Y, Z, M>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    public static void TriggerCheckEvent<T, U, V, X, Y, Z, M>(eGameEventTypes gType, T arg1, U arg2, V arg3, X arg4, Y arg5, Z arg6, M arg7)
    {
        Delegate d = null;
        if (!GetCallBack(gType, ref d, true)) return;
        GameCallBack<T, U, V, X, Y, Z, M> callback = (GameCallBack<T, U, V, X, Y, Z, M>)d;
        if (callback != null)
        {
            callback(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    private static bool GetCallBack(eGameEventTypes gType, ref Delegate d, bool checkNull = false)
    {
        bool needCheck = false;

#if UNITY_EDITOR
        needCheck = true;
#endif
        if (checkNull) needCheck = true;
        if (needCheck)
        {
            if (!eventsDict.TryGetValue(gType, out d))
            {
#if UNITY_EDITOR 
                Debug.LogError("TriggerEvent  NO Such Type" + gType);
#endif
                return false;
            }
        }
        else
        {
            d = eventsDict[gType];

        }
        return true;
    }
}
