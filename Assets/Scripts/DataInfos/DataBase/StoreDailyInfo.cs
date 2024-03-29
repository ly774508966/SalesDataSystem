﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoreDailyInfo
{
    /// <summary>
    /// 门店的ID
    /// </summary>
    public int StoreID;
    /// <summary>
    /// 门店名称
    /// </summary>
    public string StoreName;
    /// <summary>
    /// 保底销售金额
    /// </summary>
    public int LeastSales;
    /// <summary>
    /// 目标销售金额
    /// </summary>
    public int TargetSales;
    /// <summary>
    /// 今日进店人数
    /// </summary>
    public int TodayEnterCustomerNumber;
    /// <summary>
    /// 本月累计进店人数
    /// </summary>
    public int MonthEnterCustomerNubmer;
    /// <summary>
    /// 今日成交人数
    /// </summary>
    public int TodyTransaction;
    /// <summary>
    /// 今日销售额
    /// </summary>
    public float TodaySales;
    /// <summary>
    /// 本月累计成交数
    /// </summary>
    public int MonthTransaction;
    /// <summary>
    /// 当月累计销售额
    /// </summary>
    public float MonthTotalSales;
    /// <summary>
    /// 今日成交产品
    /// </summary>
    public List<ProductTransactionInfo> TodayProductTransactions = new List<ProductTransactionInfo>();
    /// <summary>
    /// 完成率
    /// </summary>
    public float CompletionRate;
    /// <summary>
    /// 老顾客数量
    /// </summary>
    public int OldCustomerCount;
    /// <summary>
    /// 老顾客销售额
    /// </summary>
    public float OldCustomerSales;
    /// <summary>
    /// 新顾客数量
    /// </summary>
    public int NewCustomerCount;
    /// <summary>
    /// 新顾客销售额
    /// </summary>
    public float NewCustomerSales;

    public ProductTransactionInfo GetTransactionInfo(int productId,string productName)
    {
        ProductTransactionInfo trans = null;
        for (int i = 0; i < TodayProductTransactions.Count; i++)
        {
            if (TodayProductTransactions[i].ProductId == productId)
            {
                trans = TodayProductTransactions[i];
                break;
            }
        }
        if (trans == null)
        {
            trans = new ProductTransactionInfo();
            trans.ProductId = productId;
            trans.ProductName = productName;
            TodayProductTransactions.Add(trans);
        }
        return trans;
    }
}
