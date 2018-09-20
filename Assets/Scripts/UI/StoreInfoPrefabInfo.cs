using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class StoreInfoPrefabInfo : MonoBehaviour
{
    public Text Text_ID;
    public Text Text_Name;
    public Text Text_LeastSales;
    public Text Text_TargetSales;
    public Text Text_SalesPerson;

    public void SetValue(StoreInfo value)
    {
        Text_ID.text = value.StoreID.ToString();
        Text_Name.text = value.StoreName;
        Text_LeastSales.text = value.LeastSales.ToString();
        Text_TargetSales.text = value.TargetSales.ToString();
        StringBuilder strb = new StringBuilder();
        for (int i = 0; i < value.SalesPerson.Count; i++)
        {
            strb.Append(value.SalesPerson[i]);
            strb.Append(',');
        }
        Text_SalesPerson.text = strb.ToString();
    }
}
