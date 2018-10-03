using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreDailySalePrefabInfo : MonoBehaviour
{
    public Text Text_StoreID;
    public Text Text_StoreName;
    public Text Text_LeastSales;
    public Text Text_TargetSales;
    public InputField Input_TodayEnterCustomerCount;
    public InputField Input_MonthEnterCustomerCount;
    public Text Text_TodayTransaction;
    public Text Text_TodaySales;
    public InputField Input_MonthTransaction;
    public InputField Input_MonthSales;
    public Text Text_TodayProductTransactions;
    public InputField Input_CompleteRate;
    public InputField Input_OldCustomerCount;
    public InputField Input_OldCustomerSales;
    public InputField Input_NewCustomerCount;
    public InputField Input_NewCustomerSales;
    public Toggle Toggle;
    public bool IsSelected = false;
    private StoreDailyInfo data;
    private string curDateTime;

    public void SetValue(string curDate,StoreDailyInfo value)
    {
        data = value;
        curDateTime = curDate;
        Text_StoreID.text = value.StoreID.ToString();
        Text_StoreName.text = value.StoreName; ;
        Text_LeastSales.text = value.LeastSales.ToString();
        Text_TargetSales.text = value.TargetSales.ToString();
        Input_TodayEnterCustomerCount.text = value.TodayEnterCustomerNumber.ToString();
        Input_MonthEnterCustomerCount.text = value.MonthEnterCustomerNubmer.ToString();
        Text_TodayTransaction.text = value.TodyTransaction.ToString();
        Text_TodaySales.text = value.TodaySales.ToString();
        Input_MonthTransaction.text = value.MonthTransaction.ToString();
        Input_MonthSales.text = value.MonthTotalSales.ToString();

        Text_TodayProductTransactions.text = "";
        for (int i = 0; i < value.TodayProductTransactions.Count; i++)
        {
            ProductTransactionInfo trans = value.TodayProductTransactions[i];
            Text_TodayProductTransactions.text += string.Format("{0}*{1}  ", trans.ProductName, trans.TransactionCount);
        }
        Input_CompleteRate.text = value.CompletionRate.ToString();
        Input_OldCustomerCount.text = value.OldCustomerCount.ToString();
        Input_OldCustomerSales.text = value.OldCustomerSales.ToString();
        Input_NewCustomerCount.text = value.NewCustomerCount.ToString();
        Input_NewCustomerSales.text = value.NewCustomerSales.ToString();
        Toggle.group = gameObject.GetComponentInParent<ToggleGroup>();
    }

    public void OnTodayEnterCustomerCountChanged(string value)
    {
        data.TodayEnterCustomerNumber = int.Parse(value);
        int lasterDaymonthEnterNumber = SalesDataSystem.SystemDatas.DailySysData.GetLastDayMonthEnterCustomerNumber(curDateTime, data.StoreID);
        data.MonthEnterCustomerNubmer = data.TodayEnterCustomerNumber + lasterDaymonthEnterNumber;
        Input_MonthEnterCustomerCount.text = data.MonthEnterCustomerNubmer.ToString();
    }

    public void OnMonthEnterCustomerCountChanged(string value)
    {
        data.MonthEnterCustomerNubmer = int.Parse(value);
    }

    public void OnMonthTransactionChanged(string value)
    {
        data.MonthTransaction = int.Parse(value);
    }

    public void OnMonthTotalSalesChanged(string value)
    {
        data.MonthTotalSales = float.Parse(value);
    }

    public void OnCompleteRateChanged(string value)
    {
        data.CompletionRate = float.Parse(value);
    }

    public void OnOldCustomerCountChange(string value)
    {
        data.OldCustomerCount = int.Parse(value);
    }

    public void OnOldCustomerSalesChange(string value)
    {
        data.OldCustomerSales = float.Parse(value);
    }

    public void OnNewCustomerCountChange(string value)
    {
        data.NewCustomerCount = int.Parse(value);
    }

    public void OnNewCustomerSalesChange(string value)
    {
        data.NewCustomerSales = float.Parse(value);
    }

    public void OnClickPrefab(bool isSelect)
    {
        IsSelected = isSelect;
    }
}
