using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleSaleDataPrefabInfo : MonoBehaviour
{
    public Text Text_Index;
    public Text Text_StoreID;
    public Text Text_StoreName;
    public Text Text_Transactionpercent;
    public Text Text_CustomerName;
    public Text Text_CustormerDetailInfo;
    public Text Text_IsNewCustomer;
    public Text Text_ProductID;
    public Text Text_ProductName;
    public Text Text_UnitPrice;
    public Text Text_SaleCount;
    public Text Text_TotalPrice;
    public Text Text_SalesPerson1;
    public Text Text_SalesPerson2;
    public Toggle Toggle;
    public bool IsSelected = false;
    public SingleSalesData data;

    public void SetValue(SingleSalesData value)
    {
        data = value;
        Text_Index.text = value.Index.ToString();
        Text_StoreID.text = value.StoreID.ToString();
        Text_StoreName.text = value.StoreName;
        Text_Transactionpercent.text = value.TransactionPercent.ToString();
        Text_CustomerName.text = value.CustomerName;
        Text_CustormerDetailInfo.text = value.CustomerDetailInfo;
        Text_IsNewCustomer.text = value.IsNewCustomer ? "是" : "否";
        Text_ProductID.text = value.ProductID.ToString();
        Text_ProductName.text = value.ProductName;
        Text_UnitPrice.text = value.UnitPrice.ToString();
        Text_SaleCount.text = value.SaleCount.ToString();
        Text_TotalPrice.text = value.TotalPrice.ToString();
        Text_SalesPerson1.text = value.SalesPerson1;
        Text_SalesPerson2.text = value.SalesPersion2;
        Toggle.group = gameObject.GetComponentInParent<ToggleGroup>();
    }

    public void OnClick(bool isSelect)
    {
        IsSelected = isSelect;
    }
}
