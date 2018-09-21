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
        Text_SalesPerson.text = null;
    }
}
