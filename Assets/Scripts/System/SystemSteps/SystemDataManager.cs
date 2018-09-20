using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDataManager
{
    public static ProductSystemData ProductSysData;

    public static void Init()
    {
        ProductSysData = new ProductSystemData();
        ProductSysData.InitData();
    }
}
