using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SalesPersonRankInfo
{
    /// <summary>
    /// 销售ID
    /// </summary>
    public int SalePersonID;
    /// <summary>
    /// 销售人员名称
    /// </summary>
    public string SalesPsersonName;
    /// <summary>
    /// 门店名称
    /// </summary>
    public string StoreName;
    /// <summary>
    /// 保底销售额
    /// </summary>
    public float LeastSales;
    /// <summary>
    /// 目标销售额
    /// </summary>
    public float TargetSales;
    /// <summary>
    /// 排名
    /// </summary>
    public int Ranking;
    /// <summary>
    /// 业绩
    /// </summary>
    public float PerformanceSales;
    /// <summary>
    /// 完成率
    /// </summary>
    public float CompleteRate;
}
