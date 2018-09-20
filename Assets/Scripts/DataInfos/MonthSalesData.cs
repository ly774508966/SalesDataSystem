using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonthSalesData
{
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
    /// <summary>
    /// 每日数据
    /// </summary>
    public List<DailySalesData> DailySalesData = new List<DailySalesData>();
}
