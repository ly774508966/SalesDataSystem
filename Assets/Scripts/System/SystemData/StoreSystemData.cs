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

    public List<SalePersonInfo> AllSalePersonInfos = new List<SalePersonInfo>();

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitData()
    {
        allStoreInfosDict = ConfigDataManager.StoreInfosDict;
        AllSalePersonInfos = ConfigDataManager.SalePersonList;
    }

    public void AddProduct(StoreInfo info)
    {
        allStoreInfosDict[info.StoreID] = info;
    }

    public void DeleteProduct(int storeID)
    {
        allStoreInfosDict.Remove(storeID);
    }

    public string GetStoreNameByID(int id)
    {
        foreach (var s in allStoreInfosDict)
        {
            if (s.Value.StoreID == id)
                return s.Value.StoreName;
        }
        return null;
    }

    public int GetStoreIDByName(string name)
    {
        foreach (var s in allStoreInfosDict)
        {
            if (s.Value.StoreName == name)
                return s.Value.StoreID;
        }
        return -1;
    }

    public List<string> GetStoreNameList()
    {
        List<string> names = new List<string>();
        foreach (var s in allStoreInfosDict)
        {
            names.Add(s.Value.StoreName);
        }
        return names;
    }

    public int GetLeastSales(int id)
    {
        foreach (var s in allStoreInfosDict)
        {
            if (s.Value.StoreID == id)
                return s.Value.LeastSales;
        }
        return 0; ;
    }

    public int GetTargetSales(int id)
    {
        foreach (var s in allStoreInfosDict)
        {
            if (s.Value.StoreID == id)
                return s.Value.TargetSales;
        }
        return 0; ;
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
