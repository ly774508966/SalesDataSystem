using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

}
