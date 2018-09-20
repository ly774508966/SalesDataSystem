using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductInfoSystem : SystemStepBase
{
    /// <summary>
    /// 产品信息UI父物体
    /// </summary>
    public Transform ProductInfoContent;
    /// <summary>
    /// 产品信息的预制
    /// </summary>
    public GameObject ProductInfoPrefab;

    private List<ProductInfoPrefabInfo> productInfoPrefabList = new List<ProductInfoPrefabInfo>();

    public void OnEnable()
    {
        UpdateProductShow();
    }

    private void UpdateProductShow()
    {
        List<ProductInfo> productInfos = new List<ProductInfo>();
        foreach (var p in ConfigDataManager.ProductInfosDict)
        {
            productInfos.Add(p.Value);
        }

        int curPrefabIndex = 0;
        for (int i = 0; i < productInfos.Count; i++)
        {
            curPrefabIndex++;
            if (curPrefabIndex > productInfoPrefabList.Count)
            {
                CreateProductPrefab(productInfos[i]);
            }
        }
        if (curPrefabIndex < productInfoPrefabList.Count)
        {
            for (int i = curPrefabIndex; i < productInfoPrefabList.Count; i++)
            {
                productInfoPrefabList[i].gameObject.SetActive(false);
            }
        }
    }

    private void CreateProductPrefab(ProductInfo info)
    {
        GameObject newGo = GameObject.Instantiate(ProductInfoPrefab);
        newGo.SetActive(false);
        newGo.transform.SetParent(ProductInfoContent);
        ProductInfoPrefabInfo prefabInfo = newGo.GetComponent<ProductInfoPrefabInfo>();
        prefabInfo.SetValue(info);
        productInfoPrefabList.Add(prefabInfo);
        newGo.SetActive(true);
    }
}
