using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleSalePersonRankPrefabInfo : MonoBehaviour
{
    public Text Text_StoreName;
    public Text Text_SalePersonName;
    public Text Text_LeastSales;
    public Text Text_TargetSales;
    public Text Text_Ranking;
    public Text Text_PerformanceaSales;
    public Text Text_completeRate;

    public void SetValue(SalesPersonRankInfo info)
    {
        Text_StoreName.text = info.StoreName;
        Text_SalePersonName.text = info.SalesPsersonName;
        Text_LeastSales.text = info.LeastSales.ToString();
        Text_TargetSales.text = info.TargetSales.ToString();
        Text_Ranking.text = info.Ranking.ToString();
        Text_PerformanceaSales.text = info.PerformanceSales.ToString();
        Text_completeRate.text = (info.CompleteRate * 100).ToString("f2");
    }
}
