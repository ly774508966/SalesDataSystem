using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ProductCfg : ScriptableObject
{
    /// <summary>
    /// 所有产品信息
    /// </summary>
    public List<ProductInfo> Products;
}
