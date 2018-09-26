using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DailyDataSystem : SystemStepBase
{
    public Transform DailySingleSaleDataContent;
    /// <summary>
    /// 显示每日数据的Prefab
    /// </summary>
    public GameObject SingleSaleDataPrefab;

    private List<SingleSaleDataPrefabInfo> singleDataInfosPrefabList = new List<SingleSaleDataPrefabInfo>();
    /// <summary>
    /// 编辑每日信息界面
    /// </summary>
    public GameObject EditDailyDataPanel;
    public EditorSingleDataPrefabInfo EditorDailyPrefabInfo;

    public string CurDateTime;
    public InputField DateTimeInput;

    private void OnEnable()
    {
        CurDateTime = System.DateTime.Now.ToString("yyyyMMdd");
        DateTimeInput.text = CurDateTime;
        RefreshDailyDataShow();
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
        RefreshDailyDataShow();
    }

    public void RefreshDailyDataShow()
    {
        List<SingleSalesData> singleSaleInfos = SalesDataSystem.SystemDatas.DailySysData.LoadData(CurDateTime);
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
        data.Index = SalesDataSystem.SystemDatas.DailySysData.LoadData(CurDateTime).Count + 1;
        EditorDailyPrefabInfo.SetValue(CurDateTime, data);
    }

    public void OnClickEditorData()
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

    public void OnClickDeletData()
    {
        foreach (var s in singleDataInfosPrefabList)
        {
            if (s.IsSelected)
            {
                SalesDataSystem.SystemDatas.DailySysData.DeleteSingleSaleData(CurDateTime, s.data.Index);
                break;
            }
        }
        RefreshDailyDataShow();
    }

    public void OnClickEditPanelBack()
    {
        EditDailyDataPanel.SetActive(false);
    }

    public void OnSubmitEditData()
    {
        EditorDailyPrefabInfo.OnClickSubmitData();
        SalesDataSystem.SystemDatas.DailySysData.AddSingleSaleData(CurDateTime, EditorDailyPrefabInfo.EditorSingleData);
        RefreshDailyDataShow();
        EditDailyDataPanel.SetActive(false);
    }

    public void OnClickSaveData()
    {
        SalesDataSystem.SystemDatas.DailySysData.SaveChange(CurDateTime);
        SalesDataSystem.TipsShow.ShowTipsInfo("保存数据成功");
    }
}
