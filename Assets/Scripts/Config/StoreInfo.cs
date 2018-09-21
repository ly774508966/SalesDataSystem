using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoreInfo
{
    /// <summary>
    /// 店面名称
    /// </summary>
    public string StoreName;
    /// <summary>
    /// 店面ID
    /// </summary>
    public int StoreID;
    /// <summary>
    /// 保底销售额
    /// </summary>
    public int LeastSales;
    /// <summary>
    /// 目标销售额
    /// </summary>
    public int TargetSales;
}
