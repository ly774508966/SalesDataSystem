using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorSingleDataPrefabInfo : MonoBehaviour
{
    public InputField Input_Index;
    public Text Text_StoreID;
    public Dropdown DropDown_StoreName;
    public InputField Input_CustomerName;
    public InputField Input_CustomerDetailInfo;
    public Dropdown DropDown_IsNewCustomer;
    public Text Text_ProductId;
    public Dropdown DropDown_ProductName;
    public InputField Input_ProductPrice;
    public InputField Input_TransactionCount;
    public InputField Input_TotalSales;
    public Dropdown DropDown_SalePerspon1;
    public Dropdown DropDown_SalePerson2;

    public int Index;
    public int StoreID;
    public string StoreName;
    public string CustomerName;
    public string CustomerDetailInfo;
    public bool IsNewCustoner;
    public int ProductId;
    public string ProductName;
    public int ProductUnitPrice;
    public int TransactionCount;
    public int TotalSales;
    public string SalePerson1;
    public string SalePerson2;

    private SingleSalesData prefabdata;
    /// <summary>
    /// 初始化编辑选项
    /// </summary>
    public void Init()
    {
        List<Dropdown.OptionData> storeOpdata = new List<Dropdown.OptionData>();
        List<string> storenames = SalesDataSystem.SystemDatas.StoreSysData.GetStoreNameList();
        foreach (var s in storenames)
        {
            storeOpdata.Add(new Dropdown.OptionData(s));
        }
        DropDown_StoreName.options = storeOpdata;
        List<Dropdown.OptionData> productOpdata = new List<Dropdown.OptionData>();
        List<string> pnames = SalesDataSystem.SystemDatas.ProductSysData.GetAllProductsNames();
        foreach (var s in pnames)
        {
            productOpdata.Add(new Dropdown.OptionData(s));
        }
        DropDown_ProductName.options = productOpdata;

        List<Dropdown.OptionData> salePersonInfos = new List<Dropdown.OptionData>();
        foreach (var s in SalesDataSystem.SystemDatas.StoreSysData.AllSalePersonInfos)
        {
            salePersonInfos.Add(new Dropdown.OptionData(s.Name));
        }
        DropDown_SalePerspon1.options = salePersonInfos;
        DropDown_SalePerson2.options = salePersonInfos;
    }


    private void Reset()
    {
        Input_Index.text = null;
        Text_StoreID.text = null;
        DropDown_StoreName.value = 0;
        DropDown_StoreName.RefreshShownValue();
        Input_CustomerName.text = null;
        Input_CustomerDetailInfo.text = null;
        DropDown_IsNewCustomer.value = 0;
        DropDown_IsNewCustomer.RefreshShownValue();
        Text_ProductId.text = null;
        DropDown_ProductName.value = 0;
        DropDown_ProductName.RefreshShownValue();
        Input_ProductPrice.text = null;
        Input_TransactionCount.text = null;
        Input_TotalSales.text = null;
        DropDown_SalePerspon1.value = 0;
        DropDown_SalePerspon1.RefreshShownValue();
        DropDown_SalePerson2.value = 0;
        DropDown_SalePerson2.RefreshShownValue();
    }

    private void Start()
    {
        Init();
    }


    public void SetValue(SingleSalesData data)
    {
        prefabdata = data;

    }

    void OnDisable()
    {
        Reset();
    }

    public void Submit()
    {

    }

    public void OnIndexChange(string value)
    {
        Index = int.Parse(value);
    }

    public void OnStoreNameChange(int optionID)
    {

    }


}
