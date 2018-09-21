using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDataManager
{
    public ProductSystemData ProductSysData;
    public StoreSystemData StoreSysData;
    public DailySalesSystemData DailySysData;
    public HistorySalesSystemData HistoryData;

    public void Init()
    {
        ProductSysData = new ProductSystemData();
        ProductSysData.InitData();
        StoreSysData = new StoreSystemData();
        StoreSysData.InitData();
        DailySysData = new DailySalesSystemData();
        DailySysData.InitData();
        HistoryData = new HistorySalesSystemData();
        HistoryData.InitData();
    }
}
