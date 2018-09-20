using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsShow : MonoBehaviour
{
    public GameObject TipInfo;
    public Text text;

    public void ShowTipsInfo(string info)
    {
        if (text != null)
            text.text = info;
        if (TipInfo != null)
            TipInfo.SetActive(true);
    }

    public void CloseTip()
    {
        if (TipInfo != null)
            TipInfo.SetActive(false);
    }
}
