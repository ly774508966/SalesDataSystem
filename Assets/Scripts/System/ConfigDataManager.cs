﻿using System.Collections;
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
        Utility.CreateDir(persistPath);
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
        Utility.CreateDir(persistPath);
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
        SalePersonCfgNormal newCfg = new SalePersonCfgNormal();
        newCfg.SalePerson = newinfo;
        string info = JsonUtility.ToJson(newCfg);
        string persistPath = Application.persistentDataPath + "/Config/SalePersonCfg.txt";
        Utility.CreateDir(persistPath);
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
        Utility.CreateDir(persistPath);
        string info = JsonUtility.ToJson(newinfo);
        File.WriteAllText(persistPath, info);
    }

    public static void ExportDailyData(string date, DailySalesData newinfo)
    {
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);
        string persistPath = string.Format("{0}/{1}/{2}/{3}/{4}ExportData.txt", Application.persistentDataPath, "ExportData", year, month, day);
        string folder = Path.GetDirectoryName(persistPath);
        Utility.CreateDir(persistPath);
        string DailySalesDataMonthPart = (Resources.Load("Configs/DailySalesDataMonthPart") as TextAsset).text;
        string DailySalesDataStorePart = (Resources.Load("Configs/DailySalesDataStorePart") as TextAsset).text;
        string DailySalesDataCustomerServicePart = (Resources.Load("Configs/DailySalesDataCustomerServicePart") as TextAsset).text;
        string DailySalesDataDejiStorePart = (Resources.Load("Configs/DailySalesDataDejiStorePart") as TextAsset).text;
        string DailySalesDataCustomerServiceStorePart = (Resources.Load("Configs/DailySalesDataCustomersServiceStorePart") as TextAsset).text;
        string productTransactionInfo = GetMonthProductTransactionInfo(newinfo);
        MonthSalesData thisMonthData = newinfo.MonthSaleDataToDate;

        System.Text.StringBuilder strb = new System.Text.StringBuilder(500);
        string exportInfo = null;
        string exportmonthInfo = string.Format(DailySalesDataMonthPart, year, month, day, newinfo.TotalSales, productTransactionInfo,
           thisMonthData.TotalSales, thisMonthData.NewCustomerTransactions, thisMonthData.NewCustomerSales, thisMonthData.OldCustomerTransactions,
           thisMonthData.OldCustomerSales);
        strb.Append(exportmonthInfo);

        int index = 1;
        #region 一般的店面处理
        for (int i = 0; i < newinfo.AllStoreDailySaleData.Count; i++)
        {
            StoreDailyInfo sdi = newinfo.AllStoreDailySaleData[i];
            if (sdi.StoreName == "德基广场店" || sdi.StoreName == "德基BHG店"
                || sdi.StoreName == "广播进线" || sdi.StoreName == "售后维护") continue;
            string storeproductInfo = GetStoreProductTransactionInfo(sdi);
            string exportStoreDailyInfo = string.Format(DailySalesDataStorePart, index, sdi.StoreName, month, sdi.LeastSales / 10000f, sdi.TargetSales / 10000f,
                year, month, day, sdi.TodayEnterCustomerNumber, sdi.MonthEnterCustomerNubmer, sdi.TodyTransaction, sdi.MonthTransaction, sdi.TodaySales, storeproductInfo,
                sdi.MonthTotalSales, (sdi.CompletionRate * 100).ToString("F2"), sdi.OldCustomerSales, sdi.OldCustomerCount,
                sdi.NewCustomerSales, sdi.NewCustomerCount);
            strb.Append(exportStoreDailyInfo);
            index++;
        }
        #endregion

        #region 德基广场店处理
        int dejistoreId = SalesDataSystem.SystemDatas.StoreSysData.GetStoreIDByName("德基广场店");
        StoreDailyInfo dejiStoreInfo = newinfo.GetStoreDailyInfo(dejistoreId);
        string dejiStoreProductInfo = GetStoreProductTransactionInfo(dejiStoreInfo);
        int dejiBHGstoreId = SalesDataSystem.SystemDatas.StoreSysData.GetStoreIDByName("德基BHG店");
        StoreDailyInfo dejiBhgStoreInfo = newinfo.GetStoreDailyInfo(dejiBHGstoreId);
        string dejiBhgProductInfo = GetStoreProductTransactionInfo(dejiBhgStoreInfo);
        string dejiStoreDailyInfo = string.Format(DailySalesDataDejiStorePart, index, month, dejiStoreInfo.LeastSales / 10000f, dejiStoreInfo.TargetSales / 10000f,
                year, month, day, (dejiStoreInfo.TodayEnterCustomerNumber + dejiBhgStoreInfo.TodayEnterCustomerNumber), (dejiBhgStoreInfo.MonthEnterCustomerNubmer + dejiBhgStoreInfo.MonthEnterCustomerNubmer), (dejiStoreInfo.TodyTransaction + dejiBhgStoreInfo.TodyTransaction), (dejiStoreInfo.MonthTransaction + dejiBhgStoreInfo.MonthTransaction)
                , (dejiStoreInfo.TodaySales + dejiBhgStoreInfo.TodaySales), dejiStoreProductInfo, dejiBhgProductInfo,(dejiStoreInfo.MonthTotalSales + dejiBhgStoreInfo.MonthTotalSales), ((dejiStoreInfo.CompletionRate + dejiBhgStoreInfo.CompletionRate) * 100).ToString("F2"), (dejiStoreInfo.OldCustomerSales), (dejiStoreInfo.OldCustomerCount),
                (dejiStoreInfo.NewCustomerSales), (dejiStoreInfo.NewCustomerCount), dejiBhgStoreInfo.MonthTotalSales, (dejiBhgStoreInfo.NewCustomerCount + dejiBhgStoreInfo.OldCustomerCount));
        strb.Append(dejiStoreDailyInfo);
        index++;
        #endregion
        strb.Append(Environment.NewLine);
        #region 客服部处理
        int broadcastStoreId = SalesDataSystem.SystemDatas.StoreSysData.GetStoreIDByName("广播进线");
        StoreDailyInfo broadCastStoreInfo = newinfo.GetStoreDailyInfo(broadcastStoreId);
        string broadCastProductInfo = GetStoreProductTransactionInfo(broadCastStoreInfo);
        int afterSaleStoreId = SalesDataSystem.SystemDatas.StoreSysData.GetStoreIDByName("售后维护");
        StoreDailyInfo afterSaleStoreInfo = newinfo.GetStoreDailyInfo(afterSaleStoreId);
        string afterSaleProductInfo = GetStoreProductTransactionInfo(afterSaleStoreInfo);
        string customerServiceDailyInfo = string.Format(DailySalesDataCustomerServiceStorePart, index, month, broadCastStoreInfo.LeastSales / 10000f, broadCastStoreInfo.TargetSales / 10000f,
            year, month, day, broadCastStoreInfo.TodyTransaction, broadCastStoreInfo.TodaySales, broadCastProductInfo, broadCastStoreInfo.MonthTotalSales,
            afterSaleStoreInfo.TodyTransaction, afterSaleStoreInfo.TodaySales, afterSaleProductInfo, afterSaleStoreInfo.MonthTotalSales,
            (broadCastStoreInfo.MonthTotalSales + afterSaleStoreInfo.MonthTotalSales), (broadCastStoreInfo.NewCustomerSales + afterSaleStoreInfo.NewCustomerSales), (broadCastStoreInfo.NewCustomerCount + afterSaleStoreInfo.NewCustomerCount),
            (broadCastStoreInfo.OldCustomerSales + afterSaleStoreInfo.OldCustomerSales), (broadCastStoreInfo.OldCustomerCount + afterSaleStoreInfo.OldCustomerCount), (((broadCastStoreInfo.CompletionRate + afterSaleStoreInfo.CompletionRate) * 100).ToString("F2")));
        strb.Append(customerServiceDailyInfo);
        #endregion

        exportInfo = strb.ToString();
        File.WriteAllText(persistPath, exportInfo);

        string rankingPersistPath = string.Format("{0}/{1}/{2}/{3}/{4}ExportData_Ranking.txt", Application.persistentDataPath, "ExportData", year, month, day);
        System.Text.StringBuilder rankstrb = new System.Text.StringBuilder(500);
        string rankingheadtitle = "门店,姓名,保底（万）,目标（万）,排名,业绩（元）,完成率（100%）" + Environment.NewLine;
        rankstrb.Append(rankingheadtitle);
        string rankinfoformat = "{0},{1},{2},{3},{4},{5},{6}%" + Environment.NewLine;
        for (int i = 0; i < newinfo.AllSalesPersonRankInfos.Count; i++)
        {
            SalesPersonRankInfo rankinginfo = newinfo.AllSalesPersonRankInfos[i];
            string singleinfo = string.Format(rankinfoformat, rankinginfo.StoreName,rankinginfo.SalesPsersonName,rankinginfo.LeastSales / 10000,
                rankinginfo.TargetSales/10000,rankinginfo.Ranking,rankinginfo.PerformanceSales,(int)(rankinginfo.CompleteRate * 100));
            rankstrb.Append(singleinfo);
        }
        exportInfo = rankstrb.ToString();
        File.WriteAllText(rankingPersistPath, exportInfo);
        OpenFolder(folder);
    }

    public static void OpenFolder(string path)
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        System.Diagnostics.Process.Start(path);
#endif
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
            string exportName =  SalesDataSystem.SystemDatas.ProductSysData.GetProductExportNameByID(trans.ProductId);
            string exportSuffix = SalesDataSystem.SystemDatas.ProductSysData.GetProductExportSuffixByID(trans.ProductId);
            string key = null;
            string addValue = null;
            switch (productType)
            {
                case eProductType.Classic:
                    key = "经典款：";
                    addValue = string.Format("{0} * {1}{2}、", exportName, trans.TransactionCount,exportSuffix);
                    break;
                case eProductType.Glory:
                    key = "荣耀款：";
                    addValue = string.Format("{0} * {1}{2}、", exportName, trans.TransactionCount,exportSuffix);
                    break;
                case eProductType.Exclusive:
                    key = "尊享款：";
                    addValue = string.Format("{0} * {1}{2}、", exportName, trans.TransactionCount,exportSuffix);
                    break;
                case eProductType.DrySwallow:
                    key = "干燕盏：";
                    addValue = string.Format("{0} * {1}{2}、", exportName, trans.TransactionCount,exportSuffix);
                    break;
                case eProductType.Single:
                    key = trans.ProductName + "：";
                    addValue = string.Format("{0} * {1}{2}、", exportName, trans.TransactionCount,exportSuffix);
                    break;
                case eProductType.None:
                    break;
                case eProductType.GuanYan:
                    key = "冰糖官燕：";
                    addValue = string.Format("{0} * {1}{2}、", exportName, trans.TransactionCount,exportSuffix);
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
                productTransactionDict[key] += (addValue);
            }
        }

        System.Text.StringBuilder strb = new System.Text.StringBuilder(100);
        foreach (var p in productTransactionDict)
        {
            strb.Append(p.Key);
            strb.Append(p.Value);
            strb.Append(Environment.NewLine);
        }
        productTransactionInfo = strb.ToString();
        return productTransactionInfo;
    }

    private static string GetStoreProductTransactionInfo(StoreDailyInfo storeinfo)
    {
        string productTransactionInfo = string.Empty;
        System.Text.StringBuilder strb = new System.Text.StringBuilder(100);
        strb.Append(": ");
        foreach (var p in storeinfo.TodayProductTransactions)
        {
            string exportName =  SalesDataSystem.SystemDatas.ProductSysData.GetProductExportNameByID(p.ProductId);
            string exportSuffix = SalesDataSystem.SystemDatas.ProductSysData.GetProductExportSuffixByID(p.ProductId);
            string info = string.Format("{0} * {1}{2}、", exportName, p.TransactionCount,exportSuffix);
            strb.Append(info);
        }
        productTransactionInfo = strb.ToString();
        return productTransactionInfo;
    }
}
