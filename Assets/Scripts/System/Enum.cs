using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSystemStep
{
    Login,
    Menu,
    DataHistory,
    DailyData,
    ProductInfo,
    StoreInfo,
    SalePersonInfo,
}

public enum eDailiPanelType
{
    ProductInfo,
    StoreDailyData,
    SalesPersonRank,
}

public enum eProductType
{
    Exclusive,      //尊享
    Classic,        //经典
    Glory,          //荣耀
    Single,         //单独的一类
    DrySwallow,     //干燕盏
    None,
}

public enum eStoreType
{
    Store,
    BroadCast,
    AfterSales,
}
