using System.Collections.Generic;

public class SalesPersonSystemData : SystemDataBase
{

    public List<SalePersonInfo> AllSalePersonInfos = new List<SalePersonInfo>();

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitData()
    {
        AllSalePersonInfos = ConfigDataManager.SalePersonList;
    }

    public void AddSalesPerson(StoreInfo info)
    {

    }

    public void DeleteSalesPerson(int storeID)
    {

    }

    public void SaveChange()
    {
        ConfigDataManager.SaveSalePersonInfo(AllSalePersonInfos);
    }
}
