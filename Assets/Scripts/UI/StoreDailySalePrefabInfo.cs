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
    public Text Text_MonthEnterCustomerCount;
    public Text Text_TodayTransaction;
    public Text Text_TodaySales;
    public Text Text_MonthTransaction;
    public Text Text_MonthSales;
    public Text Text_TodayProductTransactions;
    public Text Text_CompleteRate;
    public Text Text_OldCustomerCount;
    public Text Text_OldCustomerSales;
    public Text Text_NewCustomerCount;
    public Text Text_NewCustomerSales;
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
        Text_MonthEnterCustomerCount.text = value.MonthEnterCustomerNubmer.ToString();
        Text_TodayTransaction.text = value.TodyTransaction.ToString();
        Text_TodaySales.text = value.TodaySales.ToString();
        Text_MonthTransaction.text = value.MonthTransaction.ToString();
        Text_MonthSales.text = value.MonthTotalSales.ToString();

        Text_TodayProductTransactions.text = "";
        for (int i = 0; i < value.TodayProductTransactions.Count; i++)
        {
            ProductTransactionInfo trans = value.TodayProductTransactions[i];
            Text_TodayProductTransactions.text += string.Format("{0}*{1}  ", trans.ProductName, trans.TransactionCount);
        }
        Text_CompleteRate.text = value.CompletionRate.ToString();
        Text_OldCustomerCount.text = value.OldCustomerCout.ToString();
        Text_OldCustomerSales.text = value.OldCustomerSales.ToString();
        Text_NewCustomerCount.text = value.NewCustomerCount.ToString();
        Text_NewCustomerSales.text = value.NewCustomerSales.ToString();
        Toggle.group = gameObject.GetComponentInParent<ToggleGroup>();
    }

    public void OnTodayEnterCustomerCountChanged(string value)
    {
        data.TodayEnterCustomerNumber = int.Parse(value);
        int lasterDaymonthEnterNumber = SalesDataSystem.SystemDatas.DailySysData.GetLastDayMonthEnterCustomerNumber(curDateTime, data.StoreID);
        data.MonthEnterCustomerNubmer = data.TodayEnterCustomerNumber + lasterDaymonthEnterNumber;
        Text_MonthEnterCustomerCount.text = data.MonthEnterCustomerNubmer.ToString();
    }

    public void OnClickPrefab(bool isSelect)
    {
        IsSelected = isSelect;
    }
}
