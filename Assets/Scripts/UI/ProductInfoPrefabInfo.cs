using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductInfoPrefabInfo : MonoBehaviour
{
    public Text Text_ID;
    public Text Text_Name;
    public Text Text_UnitPrice;

    public void SetValue(ProductInfo productinfo)
    {
        Text_ID.text = productinfo.ProductID.ToString();
        Text_Name.text = productinfo.ProductName;
        Text_UnitPrice.text = productinfo.UnitPrice.ToString();
    }
}
