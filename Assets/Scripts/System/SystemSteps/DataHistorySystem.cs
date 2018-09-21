using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHistorySystem : SystemStepBase
{

    public override void OnClickBack()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.Menu);
    }
}
