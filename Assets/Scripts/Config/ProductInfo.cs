using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductInfo
{
    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName;
    /// <summary>
    /// 产品编号
    /// </summary>
    public int ProductID;
    /// <summary>
    /// 产品单价
    /// </summary>
    public int UnitPrice;
    /// <summary>
    /// 产品分类
    /// </summary>
    public eProductType ProductType;
    ///
    ///导出时显示的名字
    ///
    public string ExportProductName;
    ///
    ///单位
    ///
    public string ExportSaleSuffix;
}
