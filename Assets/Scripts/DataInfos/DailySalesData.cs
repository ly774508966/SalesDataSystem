using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailySalesData
{
    /// <summary>
    /// 当前日期
    /// </summary>
    public int MonthDay;
    /// <summary>
    /// 总销售金额
    /// </summary>
    public int TotalSales;
    /// <summary>
    /// 销售产品
    /// </summary>
    public List<ProductTransactionInfo> ProductTransactionInfos = new List<ProductTransactionInfo>();
    /// <summary>
    /// 新客户成交数
    /// </summary>
    public int NewCustomerTransactions;
    /// <summary>
    /// 新客户成交金额
    /// </summary>
    public int NewCustomerSales;
    /// <summary>
    /// 老客户成交数
    /// </summary>
    public int OldCustomerTransactions;
    /// <summary>
    /// 老客户成交金额
    /// </summary>
    public int OldCustomerSales;
}