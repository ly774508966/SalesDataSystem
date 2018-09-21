using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DailySalesSystemData : SystemDataBase
{
    /// <summary>
    /// 当前所有数据
    /// </summary>
    public Dictionary<int, SingleSalesData> allSingleSaleDataDict = new Dictionary<int, SingleSalesData>();

    public DailySalesData DailyData;

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitData()
    {

    }

    public void AddSingleSaleData(SingleSalesData info)
    {
        allSingleSaleDataDict[info.Index] = info;
    }

    public void DeleteSingleSaleData(int index)
    {
        if (allSingleSaleDataDict.ContainsKey(index))
        {
            allSingleSaleDataDict.Remove(index);
        }
    }

    public void CalculateData()
    {

    }

    public void SaveChange()
    {

    }
}
