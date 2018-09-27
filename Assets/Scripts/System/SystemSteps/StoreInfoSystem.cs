using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreInfoSystem : SystemStepBase
{
    /// <summary>
    /// 门店信息UI父物体
    /// </summary>
    public Transform StoreInfoContent;
    /// <summary>
    /// 门店信息的预制
    /// </summary>
    public GameObject StoreInfoPrefab;

    private List<StoreInfoPrefabInfo> storeInfoPrefabList = new List<StoreInfoPrefabInfo>();

    private void OnEnable()
    {
        UpdateStoreShow();
    }

    public override void OnClickBack()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.Menu);
    }

    private void UpdateStoreShow()
    {
        List<StoreInfo> storeInfos = new List<StoreInfo>();
        foreach (var s in ConfigDataManager.StoreInfosDict)
        {
            storeInfos.Add(s.Value);
        }

        int curPrefabIndex = 0;
        for (int i = 0; i < storeInfos.Count; i++)
        {
            curPrefabIndex++;
            if (curPrefabIndex > storeInfoPrefabList.Count)
            {
                CreateStorePrefab(storeInfos[i]);
            }
        }
        if (curPrefabIndex < storeInfoPrefabList.Count)
        {
            for (int i = curPrefabIndex; i < storeInfoPrefabList.Count; i++)
            {
                storeInfoPrefabList[i].gameObject.SetActive(false);
            }
        }
    }

    private void CreateStorePrefab(StoreInfo info)
    {
        GameObject newGo = GameObject.Instantiate(StoreInfoPrefab);
        newGo.transform.SetParent(StoreInfoContent);
        newGo.transform.SetAsLastSibling();
        StoreInfoPrefabInfo prefabInfo = newGo.GetComponent<StoreInfoPrefabInfo>();
        prefabInfo.SetValue(info);
        storeInfoPrefabList.Add(prefabInfo);
        newGo.SetActive(true);
    }

    public void OnClickSaveData()
    {
        SalesDataSystem.SystemDatas.StoreSysData.SaveChange();
    }
}
