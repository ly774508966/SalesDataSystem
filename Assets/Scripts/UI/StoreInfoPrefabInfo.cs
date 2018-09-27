using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class StoreInfoPrefabInfo : MonoBehaviour
{
    public Text Text_ID;
    public Text Text_Name;
    public InputField Input_LeastSales;
    public InputField Input_TargetSales;
    public Text Text_StoreType;
    private StoreInfo data;

    public void SetValue(StoreInfo value)
    {
        data = value;
        Text_ID.text = value.StoreID.ToString();
        Text_Name.text = value.StoreName;
        Input_LeastSales.text = value.LeastSales.ToString();
        Input_TargetSales.text = value.TargetSales.ToString();
        Text_StoreType.text = value.StoreType.ToString();
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
