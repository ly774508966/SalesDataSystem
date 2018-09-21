using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class SalePersonCfg : ScriptableObject
{

    public List<SalePersonInfo> SalePerson = new List<SalePersonInfo>();
}
