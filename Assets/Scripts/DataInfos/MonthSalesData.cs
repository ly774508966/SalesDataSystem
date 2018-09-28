using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonthSalesData
{
    /// <summary>
    /// 总销售金额
    /// </summary>
    public float TotalSales;
    /// <summary>
    /// 新客户成交数
    /// </summary>
    public float NewCustomerTransactions;
    /// <summary>
    /// 新客户成交金额
    /// </summary>
    public float NewCustomerSales;
    /// <summary>
    /// 老客户成交数
    /// </summary>
    public float OldCustomerTransactions;
    /// <summary>
    /// 老客户成交金额
    /// </summary>
    public float OldCustomerSales;
    /// <summary>
    /// 圈层销售
    /// </summary>
    public float CircleSales;

    public MonthSalesData Clone()
    {
        MonthSalesData newinfo = new MonthSalesData();
        newinfo.TotalSales = TotalSales;
        newinfo.NewCustomerSales = NewCustomerSales;
        newinfo.NewCustomerTransactions = NewCustomerTransactions;
        newinfo.OldCustomerSales = OldCustomerSales;
        newinfo.OldCustomerTransactions = OldCustomerTransactions;
        newinfo.CircleSales = CircleSales;
        return newinfo;
    }
}
