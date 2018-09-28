using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalePersonPrefabInfo : MonoBehaviour
{
    public Text Text_ID;
    public Text Text_Name;
    public InputField Input_LeastSales;
    public InputField Input_TargetSales;
    public Text Text_StoreName;
    private SalePersonInfo data;

    public void SetValue(SalePersonInfo value)
    {
        data = value;
        Text_ID.text = value.SalePsersonID.ToString();
        Text_Name.text = value.Name;
        Input_LeastSales.text = value.LeastSales.ToString();
        Input_TargetSales.text = value.TargetSales.ToString();
        Text_StoreName.text = value.StoreName;
    }

    public void OnLeastSalesChanged(string value)
    {
        data.LeastSales = int.Parse(value);
    }

    public void OnTargetSalesChanged(string value)
    {
        data.TargetSales = int.Parse(value);
    }
}

