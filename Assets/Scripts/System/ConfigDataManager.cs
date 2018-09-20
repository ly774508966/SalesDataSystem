using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConfigDataManager
{
    public static Dictionary<int, ProductInfo> ProductInfosDict = new Dictionary<int, ProductInfo>();
    public static Dictionary<int, StoreInfo> StoreInfosDict = new Dictionary<int, StoreInfo>();

    public static void LoadData()
    {
        LoadProductInfo();
        LoadStoreInfo();
    }

    private static void LoadProductInfo()
    {
        string persistPath = Application.persistentDataPath + "/config/ProductCfg.txt";
        ProductCfg productcfg = null;
        if (File.Exists(persistPath))
        {
            string info = File.ReadAllText(persistPath);
            productcfg = JsonUtility.FromJson<ProductCfg>(info);
        }
        else
        {
            productcfg = Resources.Load<ProductCfg>("Configs/ProductCfg");

        }
        if (productcfg != null)
        {
            foreach (var p in productcfg.Products)
            {
                ProductInfosDict[p.ProductID] = p;
            }
        }
    }


    public static void SaveProductInfo(List<ProductInfo> newInfo)
    {
        ProductCfg newCfg = new ProductCfg();
        newCfg.Products = newInfo;
        string info = JsonUtility.ToJson(newCfg);
        string persistPath = Application.persistentDataPath + "/config/ProductCfg.txt";
        File.WriteAllText(persistPath, info);
        LoadProductInfo();
    }

    private static void LoadStoreInfo()
    {
        string persistPath = Application.persistentDataPath + "/config/StoreCfg.txt";
        StoreCfg storeCfg = null;
        if (File.Exists(persistPath))
        {
            string info = File.ReadAllText(persistPath);
            storeCfg = JsonUtility.FromJson<StoreCfg>(info);
        }
        else
        {
            storeCfg = Resources.Load<StoreCfg>("Configs/StoreCfg");
            foreach (var s in storeCfg.Stores)
            {
                StoreInfosDict[s.StoreID] = s;
            }
        }
    }

    public static void SaveStoreInfo(List<StoreInfo> newinfo)
    {
        StoreCfg newCfg = new StoreCfg();
        newCfg.Stores = newinfo;
        string info = JsonUtility.ToJson(newCfg);
        string persistPath = Application.persistentDataPath + "/config/StoreCfg.txt";
        File.WriteAllText(persistPath, info);
        LoadStoreInfo();
    }
}
