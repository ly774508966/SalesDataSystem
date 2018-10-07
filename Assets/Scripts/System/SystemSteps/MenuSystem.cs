using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuSystem : SystemStepBase
{
    public Text Text_time;

    private void OnEnable()
    {
        Text_time.text = System.DateTime.Now.ToString("yyyyMMdd");
    }

    public void OnClickHistory()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.DataHistory);
    }

    public void OnClickDailyData()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.DailyData);
    }

    public void OnClickProduct()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.ProductInfo);
    }

    public void OnClickStore()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.StoreInfo);
    }

    public void OnClickSalesPersonInfo()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.SalePersonInfo);
    }

    public void OnClickOpenFolder()
    {
        string path = Application.persistentDataPath + "/HistoryData";
        ConfigDataManager.OpenFolder(path);
    }

    public void OnClickReceiveData()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.DataReceive);
    }

    public void OnClickSendData()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.DataSend);
    }
}
