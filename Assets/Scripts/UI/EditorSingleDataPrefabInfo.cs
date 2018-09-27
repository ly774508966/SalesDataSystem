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
    public bool IsNewCustomer;
    public int ProductId;
    public string ProductName;
    public float ProductUnitPrice;
    public float TransactionCount;
    public float TotalSales;
    public string SalePerson1;
    public string SalePerson2;

    private SingleSalesData prefabdata;
    public SingleSalesData EditorSingleData
    {
        get
        {
            return prefabdata;
        }
    }

    private string curDate;
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
        salePersonInfos.Add(new Dropdown.OptionData("None"));
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

    private void Awake()
    {
        Init();
    }


    public void SetValue(string date, SingleSalesData data)
    {
        curDate = date;
        prefabdata = data;
        Index = data.Index;
        Input_Index.text = Index.ToString();

        StoreName = data.StoreName;
        int index = 0;
        if (!string.IsNullOrEmpty(StoreName))
        {
            for (int i = 0; i < DropDown_StoreName.options.Count; i++)
            {
                if (DropDown_StoreName.options[i].text == StoreName)
                {
                    index = i;
                    break;
                }
            }
        }
        DropDown_StoreName.value = index;
        DropDown_StoreName.RefreshShownValue();
        StoreName = DropDown_StoreName.options[index].text;

        StoreID = SalesDataSystem.SystemDatas.StoreSysData.GetStoreIDByName(StoreName);
        Text_StoreID.text = StoreID.ToString();

        CustomerName = data.CustomerName;
        if (!string.IsNullOrEmpty(CustomerName))
        {
            Input_CustomerName.text = CustomerName;
        }
        CustomerDetailInfo = data.CustomerDetailInfo;
        if (!string.IsNullOrEmpty(CustomerDetailInfo))
        {
            Input_CustomerDetailInfo.text = CustomerDetailInfo;
        }
        IsNewCustomer = data.IsNewCustomer;
        index = 0;
        if (IsNewCustomer)
        {
            index = 0;
        }
        else
        {
            index = 1;
        }
        DropDown_IsNewCustomer.value = index;
        DropDown_IsNewCustomer.RefreshShownValue();
        ProductName = data.ProductName;
        index = 0;
        if (!string.IsNullOrEmpty(ProductName))
        {
            for (int i = 0; i < DropDown_ProductName.options.Count; i++)
            {
                if (ProductName == DropDown_ProductName.options[i].text)
                {
                    index = i;
                    break;
                }
            }
        }
        DropDown_ProductName.value = index;
        DropDown_ProductName.RefreshShownValue();
        ProductName = DropDown_ProductName.options[index].text;

        ProductId = SalesDataSystem.SystemDatas.ProductSysData.GetProductIdByName(ProductName);
        if (ProductId != 0)
        {
            Text_ProductId.text = ProductId.ToString();
        }

        ProductUnitPrice = SalesDataSystem.SystemDatas.ProductSysData.GetProductPrice(ProductId);
        if (ProductUnitPrice != 0)
        {
            Input_ProductPrice.text = ProductUnitPrice.ToString();
        }

        TransactionCount = data.SaleCount;
        Input_TransactionCount.text = TransactionCount.ToString();
        TotalSales = data.TotalPrice;
        Input_TotalSales.text = TotalSales.ToString();

        SalePerson1 = data.SalesPerson1;
        index = 0;
        if (!string.IsNullOrEmpty(SalePerson1))
        {
            for (int i = 0; i < DropDown_SalePerspon1.options.Count; i++)
            {
                if (SalePerson1 == DropDown_SalePerspon1.options[i].text)
                {
                    index = i;
                    break;
                }
            }
        }
        DropDown_SalePerspon1.value = index;
        DropDown_SalePerspon1.RefreshShownValue();
        SalePerson1 = DropDown_SalePerspon1.options[index].text;
        SalePerson2 = data.SalesPersion2;
        index = 0;
        if (!string.IsNullOrEmpty(SalePerson2))
        {
            for (int i = 0; i < DropDown_SalePerson2.options.Count; i++)
            {
                if (SalePerson2 == DropDown_SalePerson2.options[i].text)
                {
                    index = i;
                    break;
                }
            }
        }
        DropDown_SalePerson2.value = index;
        DropDown_SalePerson2.RefreshShownValue();
        SalePerson2 = DropDown_SalePerson2.options[index].text;
    }

    void OnDisable()
    {
        Reset();
    }

    public void OnClickSubmitData()
    {
        prefabdata.Index = Index;
        prefabdata.StoreID = StoreID;
        prefabdata.StoreName = StoreName;
        prefabdata.CustomerName = CustomerName;
        prefabdata.CustomerDetailInfo = CustomerDetailInfo;
        prefabdata.IsNewCustomer = IsNewCustomer;
        prefabdata.ProductID = ProductId;
        prefabdata.ProductName = ProductName;
        prefabdata.UnitPrice = ProductUnitPrice;
        prefabdata.SaleCount = TransactionCount;
        prefabdata.TotalPrice = TotalSales;
        prefabdata.SalesPerson1 = SalePerson1;
        prefabdata.SalesPersion2 = SalePerson2;
    }

    public void OnIndexChange(string value)
    {
        int newIndex = int.Parse(value);
        if (CheckIndexIsValid(newIndex))
        {
            Index = newIndex;
        }
        else
        {
            SalesDataSystem.TipsShow.ShowTipsInfo("序号重复");
        }
    }
    /// <summary>
    /// 检测index是否合法
    /// </summary>
    /// <param name="newIndex"></param>
    /// <returns></returns>
    private bool CheckIndexIsValid(int newIndex)
    {
        List<SingleSalesData> singleSaleInfos = SalesDataSystem.SystemDatas.DailySysData.LoadAllSingleProductData(curDate);
        for (int i = 0; i < singleSaleInfos.Count; i++)
        {
            if (singleSaleInfos[i].Index == newIndex)
            {
                return false;
            }
        }
        return true;
    }

    public void OnStoreNameChange(int optionID)
    {
        Dropdown.OptionData data = DropDown_StoreName.options[optionID];
        StoreName = data.text;
        StoreID = SalesDataSystem.SystemDatas.StoreSysData.GetStoreIDByName(StoreName);
        Text_StoreID.text = StoreID.ToString();
    }

    public void OnCustomerNameChanged(string value)
    {
        CustomerName = value;
    }

    public void OnCustomerDetailInfoChanged(string value)
    {
        CustomerDetailInfo = value;
    }

    public void OnIsNewCustomerChanged(int optionID)
    {
        if (optionID == 0)
        {
            IsNewCustomer = true;
        }
        else
        {
            IsNewCustomer = false;
        }
    }

    public void OnProductNameChanged(int optionID)
    {
        Dropdown.OptionData data = DropDown_ProductName.options[optionID];
        ProductName = data.text;
        ProductId = SalesDataSystem.SystemDatas.ProductSysData.GetProductIdByName(ProductName);
        Text_ProductId.text = ProductId.ToString();
        ProductUnitPrice = SalesDataSystem.SystemDatas.ProductSysData.GetProductPrice(ProductId);
        Input_ProductPrice.text = ProductUnitPrice.ToString();
    }

    public void OnProductPriceChanged(string value)
    {
        ProductUnitPrice = float.Parse(value);
    }

    public void OnSaleCountChanged(string value)
    {
        TransactionCount = float.Parse(value);
        TotalSales = ProductUnitPrice * TransactionCount;
        Input_TotalSales.text = TotalSales.ToString();
    }

    public void OnTotalSalesChanged(string value)
    {
        TotalSales = float.Parse(value);
    }

    public void OnSalesPerson1Changed(int optionID)
    {
        Dropdown.OptionData data = DropDown_SalePerspon1.options[optionID];
        SalePerson1 = data.text;
    }

    public void OnSalesPerson2Changed(int optionID)
    {
        Dropdown.OptionData data = DropDown_SalePerson2.options[optionID];
        SalePerson2 = data.text;
    }
}
