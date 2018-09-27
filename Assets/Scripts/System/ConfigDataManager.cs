using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

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
        string persistPath = Application.persistentDataPath + "/Config/ProductCfg.txt";
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
        ProductCfgNormal newCfg = new ProductCfgNormal();
        newCfg.Products = newInfo;
        string info = JsonUtility.ToJson(newCfg);
        string persistPath = Application.persistentDataPath + "/Config/ProductCfg.txt";
        CreateDir(persistPath);
        File.WriteAllText(persistPath, info);
        LoadProductInfo();
    }

    private static void LoadStoreInfo()
    {
        string persistPath = Application.persistentDataPath + "/Config/StoreCfg.txt";
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
        StoreCfgNormal newCfg = new StoreCfgNormal();
        newCfg.Stores = newinfo;
        string info = JsonUtility.ToJson(newCfg);
        string persistPath = Application.persistentDataPath + "/Config/StoreCfg.txt";
        CreateDir(persistPath);
        File.WriteAllText(persistPath, info);
        LoadStoreInfo();
    }

    private static void LoadSalePersonInfo()
    {
        string persistPath = Application.persistentDataPath + "/Config/SalePersonCfg.txt";
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
        string persistPath = Application.persistentDataPath + "/Config/SalePersonCfg.txt";
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
            return null;
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

    public static void ExportDailyData(string date, DailySalesData newinfo)
    {
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);
        string persistPath = string.Format("{0}/{1}/{2}/{3}/{4}ExportData.txt", Application.persistentDataPath, "HistoryData", year, month, day);
        CreateDir(persistPath);
        string DailySalesDataMonthPart = (Resources.Load("Configs/DailySalesDataMonthPart") as TextAsset).text;
        string DailySalesDataStorePart = (Resources.Load("Configs/DailySalesDataStorePart") as TextAsset).text;
        string DailySalesDataCustomerServicePart = (Resources.Load("Configs/DailySalesDataCustomerServicePart") as TextAsset).text;
        string productTransactionInfo = GetMonthProductTransactionInfo(newinfo);
        MonthSalesData thisMonthData = newinfo.MonthSaleDataToDate;

        System.Text.StringBuilder strb = new System.Text.StringBuilder(500);
        string exportInfo = null;
        string exportmonthInfo = string.Format(DailySalesDataMonthPart, year, month, day, newinfo.TotalSales, productTransactionInfo,
           thisMonthData.TotalSales, thisMonthData.NewCustomerTransactions, thisMonthData.NewCustomerSales, thisMonthData.OldCustomerTransactions,
           thisMonthData.OldCustomerSales);
        strb.Append(exportmonthInfo);

        for (int i = 0; i < newinfo.AllStoreDailySaleData.Count; i++)
        {
            StoreDailyInfo sdi = newinfo.AllStoreDailySaleData[i];
            string storeproductInfo = GetStoreProductTransactionInfo(sdi);
            string exportStoreDailyInfo = string.Format(DailySalesDataStorePart, sdi.StoreID, sdi.StoreName, month, sdi.LeastSales, sdi.TargetSales,
                year,month,day, sdi.TodayEnterCustomerNumber, sdi.MonthEnterCustomerNubmer, sdi.TodyTransaction,sdi.MonthTransaction, sdi.TodaySales, storeproductInfo,
                sdi.MonthTotalSales, (sdi.CompletionRate * 100).ToString("F2"), sdi.OldCustomerSales, sdi.OldCustomerCount,
                sdi.NewCustomerSales, sdi.NewCustomerCount);
            strb.Append(exportStoreDailyInfo);
        }

        exportInfo = strb.ToString();
        File.WriteAllText(persistPath, exportInfo);
    }

    /// <summary>
    /// 格式化销售产品数据
    /// </summary>
    /// <param name="newinfo"></param>
    /// <returns></returns>
    private static string GetMonthProductTransactionInfo(DailySalesData newinfo)
    {
        string productTransactionInfo = string.Empty;
        Dictionary<string, string> productTransactionDict = new Dictionary<string, string>();
        for (int i = 0; i < newinfo.ProductTransactionInfos.Count; i++)
        {
            ProductTransactionInfo trans = newinfo.ProductTransactionInfos[i];
            eProductType productType = SalesDataSystem.SystemDatas.ProductSysData.GetProductTypeByID(trans.ProductId);
            string key = null;
            string addValue = null;
            switch (productType)
            {
                case eProductType.Classic:
                    key = "经典款：";
                    addValue = string.Format("{0} * {1}份、", trans.ProductName, trans.TransactionCount);
                    break;
                case eProductType.Glory:
                    key = "荣耀款：";
                    addValue = string.Format("{0} * {1}份、", trans.ProductName, trans.TransactionCount);
                    break;
                case eProductType.Exclusive:
                    key = "尊享款：";
                    addValue = string.Format("{0} * {1}份、", trans.ProductName, trans.TransactionCount);
                    break;
                case eProductType.DrySwallow:
                    key = "干燕盏：";
                    addValue = string.Format("{0} * {1}份、", trans.ProductName, trans.TransactionCount);
                    break;
                case eProductType.Single:
                    key = trans.ProductName + "：";
                    addValue = string.Format("{0} * {1}份、", trans.ProductName, trans.TransactionCount);
                    break;
                case eProductType.None:
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(key))
            {
                if (!productTransactionDict.ContainsKey(key))
                {
                    productTransactionDict[key] = "";
                }
                productTransactionDict[key] += addValue;
            }
        }

        System.Text.StringBuilder strb = new System.Text.StringBuilder(100);
        foreach (var p in productTransactionDict)
        {
            strb.Append(p.Key);
            strb.Append(p.Value);
        }
        productTransactionInfo = strb.ToString();
        return productTransactionInfo;
    }

    private static string GetStoreProductTransactionInfo(StoreDailyInfo storeinfo)
    {
        string productTransactionInfo = string.Empty;
        System.Text.StringBuilder strb = new System.Text.StringBuilder(100);
        foreach (var p in storeinfo.TodayProductTransactions)
        {
            string info = string.Format("{0} * {1}份、", p.ProductName, p.TransactionCount);
            strb.Append(info);
        }
        productTransactionInfo = strb.ToString();
        return productTransactionInfo;
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
