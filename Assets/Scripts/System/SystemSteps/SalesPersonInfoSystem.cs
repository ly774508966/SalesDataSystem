using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesPersonInfoSystem : SystemStepBase
{
    /// <summary>
    /// 门店信息UI父物体
    /// </summary>
    public Transform SalesPersonInfoContent;
    /// <summary>
    /// 门店信息的预制
    /// </summary>
    public GameObject SalePersonInfoPrefab;

    private List<SalePersonPrefabInfo> salepersonInfoPrefabList = new List<SalePersonPrefabInfo>();

    private void OnEnable()
    {
        UpdateSalePsersonShow();
    }

    public override void OnClickBack()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.Menu);
    }

    private void UpdateSalePsersonShow()
    {
        List<SalePersonInfo> storeInfos = new List<SalePersonInfo>();
        foreach (var s in SalesDataSystem.SystemDatas.SalesPersonData.AllSalePersonInfos)
        {
            storeInfos.Add(s);
        }

        int curPrefabIndex = 0;
        for (int i = 0; i < storeInfos.Count; i++)
        {
            curPrefabIndex++;
            if (curPrefabIndex > salepersonInfoPrefabList.Count)
            {
                CreateSalePersonInfoPrefab(storeInfos[i]);
            }
        }
        if (curPrefabIndex < salepersonInfoPrefabList.Count)
        {
            for (int i = curPrefabIndex; i < salepersonInfoPrefabList.Count; i++)
            {
                salepersonInfoPrefabList[i].gameObject.SetActive(false);
            }
        }
    }

    private void CreateSalePersonInfoPrefab(SalePersonInfo info)
    {
        GameObject newGo = GameObject.Instantiate(SalePersonInfoPrefab);
        newGo.transform.SetParent(SalesPersonInfoContent);
        newGo.transform.SetAsLastSibling();
        newGo.transform.localScale = Vector3.one;
        SalePersonPrefabInfo prefabInfo = newGo.GetComponent<SalePersonPrefabInfo>();
        prefabInfo.SetValue(info);
        salepersonInfoPrefabList.Add(prefabInfo);
        newGo.SetActive(true);
    }

    public void OnClickSaveData()
    {
        SalesDataSystem.SystemDatas.SalesPersonData.SaveChange();
        SalesDataSystem.TipsShow.ShowTipsInfo("保存成功");
    }
}
