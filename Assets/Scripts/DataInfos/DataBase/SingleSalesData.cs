using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleSalesData
{
    /// <summary>
    /// 序号
    /// </summary>
    public int Index;
    /// <summary>
    /// 店面ID
    /// </summary>
    public int StoreID;
    /// <summary>
    /// 店面名称
    /// </summary>
    public string StoreName;
    /// <summary>
    /// 顾客名称
    /// </summary>
    public string CustomerName;
    /// <summary>
    /// 顾客其他信息
    /// </summary>
    public string CustomerDetailInfo;
    /// <summary>
    /// 是否新顾客
    /// </summary>
    public bool IsNewCustomer;
    /// <summary>
    /// 商品ID
    /// </summary>
    public int ProductID;
    /// <summary>
    /// 商品名称
    /// </summary>
    public string ProductName;
    /// <summary>
    /// 产品单价
    /// </summary>
    public float UnitPrice = 0;
    /// <summary>
    /// 销售数量
    /// </summary>
    public float SaleCount = 0;
    /// <summary>
    /// 销售总价
    /// </summary>
    public float TotalPrice;
    /// <summary>
    /// 销售人员1
    /// </summary>
    public string SalesPerson1;
    /// <summary>
    /// 销售人员2
    /// </summary>
    public string SalesPersion2;
}
