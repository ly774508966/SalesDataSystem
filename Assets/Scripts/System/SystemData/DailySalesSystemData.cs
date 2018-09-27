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

    public void LoadDailyData()
    {

    }

    public List<SingleSalesData> LoadAllSingleProductData(string date)
    {
        LoadLastDailyData(date);
        LoadDailyData(date);
        return allDailydataSaleDataDict[date].AllSingleSalesData;
    }

    public List<StoreDailyInfo> LoadAllStoreDailyData(string date)
    {
        LoadLastDailyData(date);
        LoadDailyData(date);
        return allDailydataSaleDataDict[date].AllStoreDailySaleData;
    }

    public int GetLastDayMonthEnterCustomerNumber(string date, int storeID)
    {
        DailySalesData lastDayData = LoadLastDailyData(date);
        if (lastDayData == null) return 0;
        StoreDailyInfo storeinfo = lastDayData.GetStoreDailyInfo(storeID);
        return storeinfo.MonthEnterCustomerNubmer;
    }

    public void AddSingleSaleData(string date, SingleSalesData info)
    {
        LoadDailyData(date);
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
                todaystoreDayinfo.MonthEnterCustomerNubmer = lastStoreDayInfo.MonthEnterCustomerNubmer + todaystoreDayinfo.TodayEnterCustomerNumber;
                todaystoreDayinfo.MonthTransaction = lastStoreDayInfo.MonthTransaction;
                todaystoreDayinfo.MonthTotalSales = lastStoreDayInfo.MonthTotalSales;
                todaystoreDayinfo.OldCustomerCount = lastStoreDayInfo.OldCustomerCount; ;
                todaystoreDayinfo.OldCustomerSales = lastStoreDayInfo.OldCustomerSales;
                todaystoreDayinfo.NewCustomerCount = lastStoreDayInfo.NewCustomerCount;
                todaystoreDayinfo.NewCustomerSales = lastStoreDayInfo.NewCustomerSales;
            }
            for (int i = 0; i < thisDayData.AllStoreDailySaleData.Count; i++)
            {
                StoreDailyInfo todayStoreDayInfo = thisDayData.AllStoreDailySaleData[i];
                todayStoreDayInfo.TodaySales = 0;
                todayStoreDayInfo.TodayProductTransactions.Clear();
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
                storeDayInfo.OldCustomerCount += 1;
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
        for (int i = 0; i < thisDayData.AllStoreDailySaleData.Count; i++)
        {
            StoreDailyInfo todayStoreDayInfo = thisDayData.AllStoreDailySaleData[i];
            todayStoreDayInfo.CompletionRate = todayStoreDayInfo.MonthTotalSales / todayStoreDayInfo.TargetSales;
        }
    }

    public void SaveChange(string date)
    {
        LoadDailyData(date);
        CalculateData(date);
        ConfigDataManager.SaveDailyData(date, allDailydataSaleDataDict[date]);
    }

    public void ExportData(string date)
    {
        DailySalesData dailydata = LoadDailyData(date);
        ConfigDataManager.ExportDailyData(date, dailydata);
    }

    public DailySalesData LoadLastDailyData(string date)
    {
        string year = date.Substring(0, 4);
        string month = date.Substring(4, 2);
        string day = date.Substring(6, 2);
        int intDay = int.Parse(day);
        if (intDay == 1) return null;
        string newDay = (intDay - 1).ToString();
        if (intDay <= 10)
        {
            newDay = "0" + newDay;
        }
        string lastDayDate = string.Format("{0}{1}{2}", year, month, newDay);
        return LoadDailyData(lastDayDate);
    }

    /// <summary>
    /// 加载每日数据
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    private DailySalesData LoadDailyData(string date)
    {
        if (!allDailydataSaleDataDict.ContainsKey(date))
        {
            bool isCreate = false;
            DailySalesData dailySalesData = ConfigDataManager.LoadDailyData(date);
            if (dailySalesData == null)
            {
                dailySalesData = new DailySalesData();
                dailySalesData.Date = date;
                isCreate = true;
            }
            allDailydataSaleDataDict[date] = dailySalesData;
            if (isCreate)
            {
                CalculateData(date);
                SaveChange(date);
            }
        }
        return allDailydataSaleDataDict[date];
    }
}
