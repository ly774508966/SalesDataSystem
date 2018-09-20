using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDataManager
{
    public ProductSystemData ProductSysData;

    public void Init()
    {
        ProductSysData = new ProductSystemData();
        ProductSysData.InitData();
    }
}
