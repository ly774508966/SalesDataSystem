using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductTransactionInfo
{
    /// <summary>
    /// 产品ID
    /// </summary>
    public int ProductId;
    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName;
    /// <summary>
    /// 产品成交数量
    /// </summary>
    public float TransactionCount;

    public ProductTransactionInfo Clone()
    {
        ProductTransactionInfo newinfo = new ProductTransactionInfo();
        newinfo.ProductId = ProductId;
        newinfo.ProductName = ProductName;
        newinfo.TransactionCount = TransactionCount;
        return newinfo;
    }
}
