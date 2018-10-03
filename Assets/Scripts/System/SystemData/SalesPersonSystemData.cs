using System.Collections.Generic;

public class SalesPersonSystemData : SystemDataBase
{
    private  List<SalePersonInfo> allSalePersonInfos = new List<SalePersonInfo>();

    public List<SalePersonInfo> AllSalePersonInfos
    {
        get { return allSalePersonInfos; }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitData()
    {
        allSalePersonInfos = ConfigDataManager.SalePersonList;
    }

    public void AddSalesPerson(StoreInfo info)
    {

    }

    public void DeleteSalesPerson(int storeID)
    {

    }

    public int GetSalesPersonIdByName(string sname)
    {
        for (int i = 0; i < allSalePersonInfos.Count; i++)
        {
            if (allSalePersonInfos[i].Name == sname)
            {
                return allSalePersonInfos[i].SalePsersonID;
            }
        }
        return -1;
    }

    public string GetSalesPersonStoreName(int salesPersonID)
    {
        for (int i = 0; i < allSalePersonInfos.Count; i++)
        {
            if (allSalePersonInfos[i].SalePsersonID == salesPersonID)
            {
                return allSalePersonInfos[i].StoreName;
            }
        }
        return string.Empty;
    }

    public string GetSalesNameByPersonID(int salePersonID)
    {
        for (int i = 0; i < allSalePersonInfos.Count; i++)
        {
            if (allSalePersonInfos[i].SalePsersonID == salePersonID)
            {
                return allSalePersonInfos[i].Name;
            }
        }
        return string.Empty;
    }

    public float GetLeastSalesByPersonID(int salePersonID)
    {
        for (int i = 0; i < allSalePersonInfos.Count; i++)
        {
            if (allSalePersonInfos[i].SalePsersonID == salePersonID)
            {
                return allSalePersonInfos[i].LeastSales;
            }
        }
        return 0;
    }

    public float GetTargetSalesByPersonID(int salePersonID)
    {
        for (int i = 0; i < allSalePersonInfos.Count; i++)
        {
            if (allSalePersonInfos[i].SalePsersonID == salePersonID)
            {
                return allSalePersonInfos[i].TargetSales;
            }
        }
        return 0;
    }

    public void SaveChange()
    {
        ConfigDataManager.SaveSalePersonInfo(allSalePersonInfos);
    }
}
