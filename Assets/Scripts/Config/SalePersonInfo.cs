using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SalePersonInfo
{
    /// <summary>
    /// 人员ID
    /// </summary>
    public int SalePsersonID;
    /// <summary>
    /// 名字
    /// </summary>
    public string Name;
    /// <summary>
    /// 门店
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
}
