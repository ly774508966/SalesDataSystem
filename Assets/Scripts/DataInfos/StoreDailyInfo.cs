using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTransactionInfo
{
    /// <summary>
    /// 产品ID
    /// </summary>
    public int ProductId;
    /// <summary>
    /// 产品成交数量
    /// </summary>
    public int TransactionCount;
}

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
    /// 今日进店人数
    /// </summary>
    public int TodayEnterCustomerNumber;
    /// <summary>
    /// 本月累计进店人数
    /// </summary>
    public int MounthEnterCustomerNubmer;
    /// <summary>
    /// 今日成交人数
    /// </summary>
    public int TodyTransaction;
    /// <summary>
    /// 本月累计成交数
    /// </summary>
    public int MonthTransaction;
    /// <summary>
    /// 今日销售额
    /// </summary>
    public int TodaySales;
    /// <summary>
    /// 今日成交产品
    /// </summary>
    public List<ProductTransactionInfo> TodayProductTransactions = new List<ProductTransactionInfo>();
    /// <summary>
    /// 当月累计销售额
    /// </summary>
    public int MonthTotalSales;
    /// <summary>
    /// 完成率
    /// </summary>
    public int CompletionRate;
    /// <summary>
    /// 老顾客销售额
    /// </summary>
    public int OldCustomerSales;
    /// <summary>
    /// 新顾客销售额
    /// </summary>
    public int NewCustomerSales;
}
