using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnnualSalesData
{
    /// <summary>
    /// 年数
    /// </summary>
    public int Year;
    /// <summary>
    /// 月销售记录
    /// </summary>
    public List<MonthSalesData> DailySalesData = new List<MonthSalesData>();
}
