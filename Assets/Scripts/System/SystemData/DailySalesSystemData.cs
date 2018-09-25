using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DailySalesSystemData : SystemDataBase
{
    /// <summary>
    /// 当前所有数据
    /// </summary>
    public Dictionary<string, DailySalesData> allDailydataSaleDataDict = new Dictionary<string, DailySalesData>();

    public DailySalesData DailyData;

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitData()
    {

    }

    public List<SingleSalesData> LoadData(string date)
    {
        if (allDailydataSaleDataDict.ContainsKey(date))
            return allDailydataSaleDataDict[date].AllSingleSalesData;
        else
        {
            DailySalesData record = ConfigDataManager.LoadDailyData(date);
            allDailydataSaleDataDict[date] = record;
            return record.AllSingleSalesData;
        }
    }

    public void AddSingleSaleData(string date, SingleSalesData info)
    {
        if (!allDailydataSaleDataDict.ContainsKey(date))
        {
            DailySalesData dailydata = new DailySalesData();
            dailydata.Date = date;
            allDailydataSaleDataDict[date] = dailydata;
        }
        allDailydataSaleDataDict[date].AllSingleSalesData.Add(info);
    }

    public void DeleteSingleSaleData(string date, int index)
    {
        if (allDailydataSaleDataDict.ContainsKey(date))
        {
            for (int i = 0; i < allDailydataSaleDataDict[date].AllSingleSalesData.Count; i++)
            {
                if (allDailydataSaleDataDict[date].AllSingleSalesData[i].Index == index)
                {
                    allDailydataSaleDataDict[date].AllSingleSalesData.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public void CalculateData()
    {

    }

    public void SaveChange(string date)
    {
        if (!allDailydataSaleDataDict.ContainsKey(date))
        {
            DailySalesData dailydata = new DailySalesData();
            dailydata.Date = date;
            allDailydataSaleDataDict[date] = dailydata;
        }
        ConfigDataManager.SaveDailyData(date, allDailydataSaleDataDict[date]);
    }
}
