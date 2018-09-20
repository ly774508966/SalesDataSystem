using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProductSystemData : SystemDataBase
{
    public Dictionary<int, ProductInfo> allProductInfosDict = new Dictionary<int, ProductInfo>();

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitData()
    {
        allProductInfosDict = ConfigDataManager.ProductInfosDict;
    }

    public void AddProduct()
    {

    }

    public void DeleteProduct()
    {

    }

    public void SaveChange()
    {
        List<ProductInfo> newData = new List<ProductInfo>();
        foreach (var p in allProductInfosDict)
        {
            newData.Add(p.Value);
        }
        ConfigDataManager.SaveProductInfo(newData);
    }
}
