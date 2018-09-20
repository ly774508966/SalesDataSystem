using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StoreSystemData : SystemDataBase
{
    /// <summary>
    /// 当前所有数据
    /// </summary>
    public Dictionary<int, StoreInfo> allStoreInfosDict = new Dictionary<int, StoreInfo>();

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitData()
    {
        allStoreInfosDict = ConfigDataManager.StoreInfosDict;
    }

    public void AddProduct(StoreInfo info)
    {
        allStoreInfosDict[info.StoreID] = info;
    }

    public void DeleteProduct(int storeID)
    {
        allStoreInfosDict.Remove(storeID);
    }

    public void SaveChange()
    {
        List<StoreInfo> newData = new List<StoreInfo>();
        foreach (var p in allStoreInfosDict)
        {
            newData.Add(p.Value);
        }
        ConfigDataManager.SaveStoreInfo(newData);
    }
}
