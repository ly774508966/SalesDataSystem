using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginSystem : SystemStepBase
{
    public InputField AccountInput;

    private string Account;
    private string PassWord;
    // Use this for initialization

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("lastaccount"))
        {
            string lastaccount = PlayerPrefs.GetString("lastaccount");
            AccountInput.text = lastaccount;
            Account = lastaccount;
        }
    }

    public void SetAccount(string value)
    {
        Account = value;
    }

    public void SetPassword(string value)
    {
        PassWord = value;
    }

    public void OnClickLogin()
    {
        if (CheckUserInfo())
        {
            SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.Menu);
            PlayerPrefs.SetString("lastaccount", Account);
        }
        else
        {
            SalesDataSystem.TipsShow.ShowTipsInfo("账号或者密码错误！");
        }
    }

    private bool CheckUserInfo()
    {
        if (Account == "grace" && PassWord == "loveharry")
        {
            return true;
        }
        return false;
    }
}
