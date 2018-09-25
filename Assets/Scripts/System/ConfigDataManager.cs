using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ConfigDataManager
{
    public static Dictionary<int, ProductInfo> ProductInfosDict = new Dictionary<int, ProductInfo>();
    public static Dictionary<int, StoreInfo> StoreInfosDict = new Dictionary<int, StoreInfo>();
    public static List<SalePersonInfo> SalePersonList = new List<SalePersonInfo>();

    public static void LoadData()
    {
        LoadProductInfo();
        LoadStoreInfo();
        LoadSalePersonInfo();
    }

    private static void LoadProductInfo()
    {
        string persistPath = Application.persistentDataPath + "Config/ProductCfg.txt";
        if (File.Exists(persistPath))
        {
            string info = File.ReadAllText(persistPath);
            ProductCfgNormal productcfgnormal = JsonUtility.FromJson<ProductCfgNormal>(info);
            if (productcfgnormal != null)
            {
                foreach (var p in productcfgnormal.Products)
                {
                    ProductInfosDict[p.ProductID] = p;
                }
            }
        }
        else
        {
            ProductCfg productcfg = Resources.Load<ProductCfg>("Configs/ProductCfg");
            if (productcfg != null)
            {
                foreach (var p in productcfg.Products)
                {
                    ProductInfosDict[p.ProductID] = p;
                }
            }
        }

    }

    public static void SaveProductInfo()
    {
        List<ProductInfo> newInfo = new List<ProductInfo>();
        foreach (var p in ProductInfosDict)
        {
            newInfo.Add(p.Value);
        }
        SaveProductInfo(newInfo);
    }

    public static void SaveProductInfo(List<ProductInfo> newInfo)
    {
        ProductCfg newCfg = new ProductCfg();
        newCfg.Products = newInfo;
        string info = JsonUtility.ToJson(newCfg);
        string persistPath = Application.persistentDataPath + "Config/ProductCfg.txt";
        CreateDir(persistPath);
        File.WriteAllText(persistPath, info);
        LoadProductInfo();
    }

    private static void LoadStoreInfo()
    {
        string persistPath = Application.persistentDataPath + "Config/StoreCfg.txt";
        if (File.Exists(persistPath))
        {
            string info = File.ReadAllText(persistPath);
            StoreCfgNormal storeCfgNormal = JsonUtility.FromJson<StoreCfgNormal>(info);
            foreach (var s in storeCfgNormal.Stores)
            {
                StoreInfosDict[s.StoreID] = s;
            }
        }
        else
        {
            StoreCfg storeCfg = Resources.Load<StoreCfg>("Configs/StoreCfg");
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
        string persistPath = Application.persistentDataPath + "Config/StoreCfg.txt";
        CreateDir(persistPath);
        File.WriteAllText(persistPath, info);
        LoadStoreInfo();
    }

    private static void LoadSalePersonInfo()
    {
        string persistPath = Application.persistentDataPath + "Config/SalePersonCfg.txt";
        if (File.Exists(persistPath))
        {
            string info = File.ReadAllText(persistPath);
            SalePersonCfgNormal storeCfgNormal = JsonUtility.FromJson<SalePersonCfgNormal>(info);
            foreach (var s in storeCfgNormal.SalePerson)
            {
                SalePersonList.Add(s);
            }
        }
        else
        {
            SalePersonCfg salePersonCfg = Resources.Load<SalePersonCfg>("Configs/SalePersonCfg");
            foreach (var s in salePersonCfg.SalePerson)
            {
                SalePersonList.Add(s);
            }
        }
    }

    public static void SaveSalePersonInfo(List<SalePersonInfo> newinfo)
    {
        SalePersonCfg newCfg = new SalePersonCfg();
        newCfg.SalePerson = newinfo;
        string info = JsonUtility.ToJson(newCfg);
        string persistPath = Application.persistentDataPath + "Config/SalePersonCfg.txt";
        CreateDir(persistPath);
        File.WriteAllText(persistPath, info);
        LoadStoreInfo();
    }

    public static DailySalesData LoadDailyData(string date)
    {
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);
        string persistPath = string.Format("{0}/{1}/{2}/{3}/{4}.txt", Application.persistentDataPath, "HistoryData", year, month, day);
        if (File.Exists(persistPath))
        {
            string info = File.ReadAllText(persistPath);
            DailySalesData dailydata = JsonUtility.FromJson<DailySalesData>(info);
            return dailydata;
        }
        else
        {
            DailySalesData dailydata = new DailySalesData();
            dailydata.Date = date;
            return dailydata;
        }
    }

    public static void SaveDailyData(string date, DailySalesData newinfo)
    {
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);
        string persistPath = string.Format("{0}/{1}/{2}/{3}/{4}.txt", Application.persistentDataPath, "HistoryData", year, month, day);
        CreateDir(persistPath);
        string info = JsonUtility.ToJson(newinfo);
        File.WriteAllText(persistPath, info);
    }

    private static void  CreateDir(string path)
    {
        string parent = Path.GetDirectoryName(path);
        if (!Directory.Exists(parent))
        {
            Directory.CreateDirectory(parent);
            CreateDir(parent);
        }

    }
}
