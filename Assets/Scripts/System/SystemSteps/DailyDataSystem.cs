using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DailyDataSystem : SystemStepBase
{
    public GameObject ProductInfoPanel;
    public GameObject StoreDailyInfoPanel;
    public GameObject SalesPersonRankPanel;
    /// <summary>
    /// 产品信息父物体
    /// </summary>
    public Transform DailySingleSaleDataContent;
    /// <summary>
    /// 显示每日数据的Prefab
    /// </summary>
    public GameObject SingleSaleDataPrefab;
    /// <summary>
    /// 产品销售列表
    /// </summary>
    private List<SingleSaleDataPrefabInfo> singleDataInfosPrefabList = new List<SingleSaleDataPrefabInfo>();
    /// <summary>
    /// 编辑每日信息界面
    /// </summary>
    public GameObject EditDailyDataPanel;
    public EditorSingleDataPrefabInfo EditorDailyPrefabInfo;
    /// <summary>
    /// 门店信息父物体
    /// </summary>
    public Transform StoreDailyDataContent;
    public GameObject StoreDailyDataPrefab;
    /// <summary>
    /// 门店每日销售数据
    /// </summary>
    private List<StoreDailySalePrefabInfo> storeDailyDataPrefabList = new List<StoreDailySalePrefabInfo>();

    public Transform SalesPersonRankingDataContent;
    /// <summary>
    /// 显示销售排名数据的prefab
    /// </summary>
    public GameObject SalePsersonRankinDataPrefab;
    /// 列表<summary>
    /// 销售排名
    /// </summary>
    private List<SingleSalePersonRankPrefabInfo> salePersonRankingDataPrefabList = new List<SingleSalePersonRankPrefabInfo>();



    public string CurDateTime;
    public InputField DateTimeInput;

    private eDailiPanelType curDailyInfoType = eDailiPanelType.ProductInfo;

    #region DailySystem
    private void OnEnable()
    {
        CurDateTime = System.DateTime.Now.ToString("yyyyMMdd");
        DateTimeInput.text = CurDateTime;
        OnClickRefreshData();
    }

    public void OnDateValueChanged(string value)
    {
        if (value.Length != 8)
        {
            SalesDataSystem.TipsShow.ShowTipsInfo("日期格式错误");
            return;
        }
        CurDateTime = value;
    }

    public void OnClickRefreshData()
    {
        if (curDailyInfoType == eDailiPanelType.ProductInfo)
            RefreshProductDailyDataShow();
        else if (curDailyInfoType == eDailiPanelType.StoreDailyData)
            RefreshStoreDailyDataShow();
    }

    public void OnClickProductInfo(bool value)
    {
        if (value)
        {
            curDailyInfoType = eDailiPanelType.ProductInfo;
            ProductInfoPanel.SetActive(true);
            RefreshProductDailyDataShow();
        }
        else
        {
            ProductInfoPanel.SetActive(false);
        }
    }

    public void OnClickStoreDailyInfo(bool value)
    {
        if (value)
        {
            curDailyInfoType = eDailiPanelType.StoreDailyData;
            StoreDailyInfoPanel.SetActive(true);
            RefreshStoreDailyDataShow();
        }
        else
        {
            StoreDailyInfoPanel.SetActive(false);
        }
    }

    public void OnClickSalesPersonSaleRank(bool value)
    {
        if (value)
        {
            curDailyInfoType = eDailiPanelType.SalesPersonRank;
            SalesPersonRankPanel.SetActive(true);
            RefreshSalesRankDailyDataShow();
        }
        else
        {
            SalesPersonRankPanel.SetActive(false);
        }
    }

    public void OnClickExportDailyInfo()
    {
        SalesDataSystem.SystemDatas.DailySysData.ExportData(CurDateTime);
        SalesDataSystem.TipsShow.ShowTipsInfo("导出数据成功");
    }

    #endregion

    #region 今日产品信息
    public void RefreshProductDailyDataShow()
    {
        List<SingleSalesData> singleSaleInfos = SalesDataSystem.SystemDatas.DailySysData.LoadAllSingleProductData(CurDateTime);
        singleSaleInfos.Sort((a, b) =>
        {
            if (a.Index > b.Index)
                return 1;
            else if (a.Index == b.Index)
                return 0;
            else
                return -1;
        });
        int curPrefabIndex = 0;
        for (int i = 0; i < singleSaleInfos.Count; i++)
        {
            curPrefabIndex++;
            if (curPrefabIndex > singleDataInfosPrefabList.Count)
            {
                CreateSingleSaleDataPrefab(singleSaleInfos[i]);
            }
            else
            {
                singleDataInfosPrefabList[i].SetValue(singleSaleInfos[i]);
                singleDataInfosPrefabList[i].gameObject.SetActive(true);
            }
        }
        if (curPrefabIndex < singleDataInfosPrefabList.Count)
        {
            for (int i = curPrefabIndex; i < singleDataInfosPrefabList.Count; i++)
            {
                singleDataInfosPrefabList[i].gameObject.SetActive(false);
            }
        }
    }

    private void CreateSingleSaleDataPrefab(SingleSalesData info)
    {
        GameObject newGo = GameObject.Instantiate(SingleSaleDataPrefab);
        newGo.transform.SetParent(DailySingleSaleDataContent);
        newGo.transform.SetAsLastSibling();
        SingleSaleDataPrefabInfo prefabInfo = newGo.GetComponent<SingleSaleDataPrefabInfo>();
        prefabInfo.SetValue(info);
        singleDataInfosPrefabList.Add(prefabInfo);
        newGo.SetActive(true);
    }

    public override void OnClickBack()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.Menu);
    }

    public void OnClickAddSingleData()
    {
        EditDailyDataPanel.SetActive(true);
        SingleSalesData data = new SingleSalesData();
        data.Index = SalesDataSystem.SystemDatas.DailySysData.LoadAllSingleProductData(CurDateTime).Count + 1;
        EditorDailyPrefabInfo.SetValue(CurDateTime, data);
    }

    public void OnClickEditorSingleSalesData()
    {
        foreach (var s in singleDataInfosPrefabList)
        {
            if (s.IsSelected)
            {
                EditDailyDataPanel.SetActive(true);
                EditorDailyPrefabInfo.SetValue(CurDateTime, s.data);
                break;
            }
        }
    }

    public void OnClickDeletSingleSalesData()
    {
        foreach (var s in singleDataInfosPrefabList)
        {
            if (s.IsSelected)
            {
                SalesDataSystem.SystemDatas.DailySysData.DeleteSingleSaleData(CurDateTime, s.data.Index);
                break;
            }
        }
        RefreshProductDailyDataShow();
    }

    public void OnClickEditPanelBack()
    {
        EditDailyDataPanel.SetActive(false);
    }

    public void OnSubmitSingleSalesEditData()
    {
        EditorDailyPrefabInfo.OnClickSubmitData();
        SalesDataSystem.SystemDatas.DailySysData.AddSingleSaleData(CurDateTime, EditorDailyPrefabInfo.EditorSingleData);
        RefreshProductDailyDataShow();
        EditDailyDataPanel.SetActive(false);
    }

    public void OnClickSaveSingleSalesData()
    {
        SalesDataSystem.SystemDatas.DailySysData.SaveChange(CurDateTime);
        SalesDataSystem.TipsShow.ShowTipsInfo("保存数据成功");
    }

    #endregion

    #region 门店日信息
    public void RefreshStoreDailyDataShow()
    {
        List<StoreDailyInfo> storeDailySaleInfos = SalesDataSystem.SystemDatas.DailySysData.LoadAllStoreDailyData(CurDateTime);
        storeDailySaleInfos.Sort((a, b) =>
        {
            if (a.StoreID > b.StoreID)
                return 1;
            else if (a.StoreID == b.StoreID)
                return 0;
            else
                return -1;
        });
        int curPrefabIndex = 0;
        for (int i = 0; i < storeDailySaleInfos.Count; i++)
        {
            curPrefabIndex++;
            if (curPrefabIndex > storeDailyDataPrefabList.Count)
            {
                CreateStoreDailySaleDataPrefab(storeDailySaleInfos[i]);
            }
            else
            {
                storeDailyDataPrefabList[i].SetValue(CurDateTime, storeDailySaleInfos[i]);
                storeDailyDataPrefabList[i].gameObject.SetActive(true);
            }
        }
        if (curPrefabIndex < storeDailyDataPrefabList.Count)
        {
            for (int i = curPrefabIndex; i < storeDailyDataPrefabList.Count; i++)
            {
                storeDailyDataPrefabList[i].gameObject.SetActive(false);
            }
        }
    }

    private void CreateStoreDailySaleDataPrefab(StoreDailyInfo info)
    {
        GameObject newGo = GameObject.Instantiate(StoreDailyDataPrefab);
        newGo.transform.SetParent(StoreDailyDataContent);
        newGo.transform.SetAsLastSibling();
        StoreDailySalePrefabInfo prefabInfo = newGo.GetComponent<StoreDailySalePrefabInfo>();
        prefabInfo.SetValue(CurDateTime, info);
        storeDailyDataPrefabList.Add(prefabInfo);
        newGo.SetActive(true);
    }

    public void OnClickSaveStoreDailyData()
    {
        SalesDataSystem.SystemDatas.DailySysData.SaveChange(CurDateTime);
        SalesDataSystem.TipsShow.ShowTipsInfo("保存数据成功");
    }
    #endregion

    #region 销售人员排名
    public void RefreshSalesRankDailyDataShow()
    {
        List<SalesPersonRankInfo> rankingDailySaleInfos = SalesDataSystem.SystemDatas.DailySysData.LoadAllSalesPersonRankingInfo(CurDateTime);
        rankingDailySaleInfos.Sort((a, b) =>
        {
            if (a.Ranking > b.Ranking)
                return 11;
            else if (a.Ranking == b.Ranking)
                return 0;
            else
                return -1;
        });
        int curPrefabIndex = 0;
        for (int i = 0; i < rankingDailySaleInfos.Count; i++)
        {
            curPrefabIndex++;
            if (curPrefabIndex > salePersonRankingDataPrefabList.Count)
            {
                CreateSalesPersonRankDailySaleDataPrefab(rankingDailySaleInfos[i]);
            }
            else
            {
                salePersonRankingDataPrefabList[i].SetValue(rankingDailySaleInfos[i]);
                salePersonRankingDataPrefabList[i].gameObject.SetActive(true);
            }
        }
        if (curPrefabIndex < salePersonRankingDataPrefabList.Count)
        {
            for (int i = curPrefabIndex; i < salePersonRankingDataPrefabList.Count; i++)
            {
                salePersonRankingDataPrefabList[i].gameObject.SetActive(false);
            }
        }
    }

    private void CreateSalesPersonRankDailySaleDataPrefab(SalesPersonRankInfo info)
    {
        GameObject newGo = GameObject.Instantiate(SalePsersonRankinDataPrefab);
        newGo.transform.SetParent(SalesPersonRankingDataContent);
        newGo.transform.SetAsLastSibling();
        SingleSalePersonRankPrefabInfo prefabInfo = newGo.GetComponent<SingleSalePersonRankPrefabInfo>();
        prefabInfo.SetValue(info);
        salePersonRankingDataPrefabList.Add(prefabInfo);
        newGo.SetActive(true);
    }

    public void OnClickSaveSalePersonRankData()
    {
        SalesDataSystem.SystemDatas.DailySysData.SaveChange(CurDateTime);
        SalesDataSystem.TipsShow.ShowTipsInfo("保存数据成功");
    }
    #endregion
}
