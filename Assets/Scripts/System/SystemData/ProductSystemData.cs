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

    public void AddProduct(ProductInfo info)
    {
        allProductInfosDict[info.ProductID] = info;
    }

    public void DeleteProduct(int productId)
    {
        allProductInfosDict.Remove(productId);
    }

    public List<string> GetAllProductsNames()
    {
        List<string> names = new List<string>();
        foreach (var p in allProductInfosDict)
        {
            names.Add(p.Value.ProductName);
        }
        return names;
    }

    public int GetProductPrice(int ID)
    {
        foreach (var p in allProductInfosDict)
        {
            if (p.Value.ProductID == ID)
            {
                return p.Value.UnitPrice;
            }
        }
        return 0;
    }

    public string GetProductNameByID(int ID)
    {
        foreach (var p in allProductInfosDict)
        {
            if (p.Value.ProductID == ID)
            {
                return p.Value.ProductName;
            }
        }
        return null;
    }

    public int GetProductIdByName(string _name)
    {
        foreach (var p in allProductInfosDict)
        {
            if (p.Value.ProductName == _name)
            {
                return p.Value.ProductID;
            }
        }
        return -1;
    }

    public eProductType GetProductTypeByID(int id)
    {
        foreach (var p in allProductInfosDict)
        {
            if (p.Value.ProductID == id)
            {
                return p.Value.ProductType;
            }
        }
        return eProductType.None;
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
