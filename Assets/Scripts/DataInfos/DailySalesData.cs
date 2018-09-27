﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailySalesData
{
    /// <summary>
    /// 当前日期
    /// </summary>
    public string Date;
    /// <summary>
    /// 总销售金额
    /// </summary>
    public float TotalSales;
    /// <summary>
    /// 今日销售产品
    /// </summary>
    public List<ProductTransactionInfo> ProductTransactionInfos = new List<ProductTransactionInfo>();
    /// <summary>
    /// 当日销售数据
    /// </summary>
    public List<SingleSalesData> AllSingleSalesData = new List<SingleSalesData>();
    /// <summary>
    /// 今日店面数据
    /// </summary>
    public List<StoreDailyInfo> AllStoreDailySaleData = new List<StoreDailyInfo>();
    /// <summary>
    /// 本月至今日的销售数据
    /// </summary>
    public MonthSalesData MonthSaleDataToDate = new MonthSalesData();

    public DailySalesData()
    {
        InitAllStoreDailyInfos();
    }

    public ProductTransactionInfo GetTransactionInfo(int productId, string productName)
    {
        ProductTransactionInfo trans = null;
        for (int i = 0; i < ProductTransactionInfos.Count; i++)
        {
            if (ProductTransactionInfos[i].ProductId == productId)
            {
                trans = ProductTransactionInfos[i];
                break;
            }
        }
        if (trans == null)
        {
            trans = new ProductTransactionInfo();
            trans.ProductId = productId;
            trans.ProductName = productName;
            ProductTransactionInfos.Add(trans);
        }
        return trans;
    }

    public StoreDailyInfo GetStoreDailyInfo(int storeId)
    {
        StoreDailyInfo storeDailyInfo = null;
        for (int i = 0; i < AllStoreDailySaleData.Count; i++)
        {
            if (AllStoreDailySaleData[i].StoreID == storeId)
            {
                storeDailyInfo = AllStoreDailySaleData[i];
                break;
            }
        }
        if (storeDailyInfo == null)
        {
            storeDailyInfo = new StoreDailyInfo();
            storeDailyInfo.StoreID = storeId;
            storeDailyInfo.StoreName = SalesDataSystem.SystemDatas.StoreSysData.GetStoreNameByID(storeId);
            storeDailyInfo.LeastSales = SalesDataSystem.SystemDatas.StoreSysData.GetLeastSales(storeId);
            storeDailyInfo.TargetSales = SalesDataSystem.SystemDatas.StoreSysData.GetTargetSales(storeId);
            AllStoreDailySaleData.Add(storeDailyInfo);
        }
        return storeDailyInfo;
    }

    private void InitAllStoreDailyInfos()
    {
        List<int> storeIDs = SalesDataSystem.SystemDatas.StoreSysData.GetStoreIdList();
        foreach (var id in storeIDs)
        {
            StoreDailyInfo storeDailyInfo = new StoreDailyInfo();
            storeDailyInfo.StoreID = id;
            storeDailyInfo.StoreName = SalesDataSystem.SystemDatas.StoreSysData.GetStoreNameByID(id);
            storeDailyInfo.LeastSales = SalesDataSystem.SystemDatas.StoreSysData.GetLeastSales(id);
            storeDailyInfo.TargetSales = SalesDataSystem.SystemDatas.StoreSysData.GetTargetSales(id);
            AllStoreDailySaleData.Add(storeDailyInfo);
        }
    }

    public void Reset()
    {
        TotalSales = 0;
        ProductTransactionInfos.Clear();
        AllStoreDailySaleData.Clear();
        MonthSaleDataToDate = new MonthSalesData();
    }
}