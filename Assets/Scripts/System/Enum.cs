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
    DataSend,
    DataReceive,
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
    GuanYan,        //冰糖官燕
}

public enum eStoreType
{
    Store,
    BroadCast,
    AfterSales,
}

public enum eReceiverState
{
    Closed,
    Wait,
    Connected,
    Receiving,
    SyncOver,
}

public enum eSenderState
{
    Closed,
    Connected,
    Sending,
    SendOver,
}
