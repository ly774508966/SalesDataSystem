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

    /// <summary>
    /// 初始化数据
    /// </summary>
    public override void InitData()
    {

    }

    public List<SingleSalesData> LoadData(string date)
    {
        LoadLastDailyData(date);
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
        for (int i = 0; i < allDailydataSaleDataDict[date].AllSingleSalesData.Count; i++)
        {
            SingleSalesData data = allDailydataSaleDataDict[date].AllSingleSalesData[i];
            if (data.Index == info.Index)
            {
                data = info;
                return;
            }
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
    /// <summary>
    /// 根据输入的数据计算每日销售信息
    /// 门店销售信息，月销售信息
    /// </summary>
    /// <param name="date"></param>
    public void CalculateData(string date)
    {
        string day = date.Substring(6, 2);
        DailySalesData thisDayData = LoadDailyData(date);
        if (int.Parse(day) == 1)
        {
            thisDayData.Reset();
        }
        else
        {
            DailySalesData lastDayData = LoadLastDailyData(date);
            thisDayData.TotalSales = 0;
            thisDayData.MonthSaleDataToDate = lastDayData.MonthSaleDataToDate.Clone();
            for (int i = 0; i < lastDayData.AllStoreDailySaleData.Count; i++)
            {
                StoreDailyInfo lastStoreDayInfo = lastDayData.AllStoreDailySaleData[i];
                StoreDailyInfo todaystoreDayinfo = thisDayData.GetStoreDailyInfo(lastStoreDayInfo.StoreID);
                todaystoreDayinfo.MonthEnterCustomerNubmer = lastStoreDayInfo.MonthEnterCustomerNubmer;
                todaystoreDayinfo.MonthTransaction = lastStoreDayInfo.MonthTransaction;
                todaystoreDayinfo.MonthTotalSales = lastStoreDayInfo.MonthTotalSales;
            }
            for (int i = 0; i < thisDayData.AllStoreDailySaleData.Count; i++)
            {
                StoreDailyInfo todayStoreDayInfo = thisDayData.AllStoreDailySaleData[i];
                todayStoreDayInfo.TodaySales = 0;
                todayStoreDayInfo.TodayProductTransactions.Clear();
                todayStoreDayInfo.OldCustomerCout = 0;
                todayStoreDayInfo.OldCustomerSales = 0;
                todayStoreDayInfo.NewCustomerCount = 0;
                todayStoreDayInfo.NewCustomerSales = 0;
            }
        }
        for (int i = 0; i < thisDayData.AllSingleSalesData.Count; i++)
        {
            SingleSalesData singleSaleData = thisDayData.AllSingleSalesData[i];
            thisDayData.TotalSales += singleSaleData.TotalPrice;
            ProductTransactionInfo trans = thisDayData.GetTransactionInfo(singleSaleData.ProductID, singleSaleData.ProductName);
            trans.TransactionCount += singleSaleData.SaleCount;
            #region 计算门店每日数据
            StoreDailyInfo storeDayInfo = thisDayData.GetStoreDailyInfo(singleSaleData.StoreID);
            storeDayInfo.TodyTransaction += 1;
            storeDayInfo.MonthTransaction += 1;
            storeDayInfo.TodaySales += singleSaleData.TotalPrice;
            ProductTransactionInfo transaction = storeDayInfo.GetTransactionInfo(singleSaleData.ProductID, singleSaleData.ProductName);
            transaction.TransactionCount += singleSaleData.SaleCount;
            storeDayInfo.MonthTotalSales += singleSaleData.TotalPrice;
            storeDayInfo.CompletionRate = storeDayInfo.MonthTotalSales / storeDayInfo.TargetSales;
            if (singleSaleData.IsNewCustomer)
            {
                storeDayInfo.NewCustomerCount += 1;
                storeDayInfo.NewCustomerSales += singleSaleData.TotalPrice;
            }
            else
            {
                storeDayInfo.OldCustomerCout += 1;
                storeDayInfo.OldCustomerSales += singleSaleData.TotalPrice;
            }
            #endregion
            #region 计算本月数据
            thisDayData.MonthSaleDataToDate.TotalSales += singleSaleData.TotalPrice;
            if (singleSaleData.IsNewCustomer)
            {
                thisDayData.MonthSaleDataToDate.NewCustomerTransactions += 1;
                thisDayData.MonthSaleDataToDate.NewCustomerSales += singleSaleData.TotalPrice;
            }
            else
            {
                thisDayData.MonthSaleDataToDate.OldCustomerTransactions += 1;
                thisDayData.MonthSaleDataToDate.OldCustomerSales += singleSaleData.TotalPrice;
            }
            #endregion
        }
    }

    public void SaveChange(string date)
    {
        if (!allDailydataSaleDataDict.ContainsKey(date))
        {
            DailySalesData dailydata = new DailySalesData();
            dailydata.Date = date;
            allDailydataSaleDataDict[date] = dailydata;
        }
        CalculateData(date);
        ConfigDataManager.SaveDailyData(date, allDailydataSaleDataDict[date]);
    }

    public DailySalesData LoadLastDailyData(string date)
    {
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);
        int intDay = int.Parse(day);
        if (intDay == 1) return null;
        string lastDayDate = string.Format("{0}{1}{2}", year, month, (intDay - 1).ToString());
        return LoadDailyData(lastDayDate);
    }

    private DailySalesData LoadDailyData(string date)
    {
        if (!allDailydataSaleDataDict.ContainsKey(date))
        {
            allDailydataSaleDataDict[date] = ConfigDataManager.LoadDailyData(date);
        }
        return allDailydataSaleDataDict[date];
    }
}
