using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
[System.Serializable]
public class StoreCfg:ScriptableObject
{
    public List<StoreInfo> Stores = new List<StoreInfo>();
}
