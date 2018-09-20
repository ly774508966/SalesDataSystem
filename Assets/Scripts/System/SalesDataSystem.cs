using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesDataSystem : MonoBehaviour
{
    [SerializeField]
    private static SalesDataSystem _instance;
    public static SalesDataSystem Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private TipsShow _tipsShow;
    public static TipsShow TipsShow
    {
        get
        {
            return _instance != null ? _instance._tipsShow : null;
        }
    }

    public GameObject Login;
    public GameObject Menu;
    public GameObject HistoryData;
    public GameObject DailyData;
    public GameObject ProductInfo;
    public GameObject StoreInfo;

    public ProductCfg ProductCfg;
    public StoreCfg StoreCfg;


    private Dictionary<eSystemStep, SystemStepBase> systemStepDict = new Dictionary<eSystemStep, SystemStepBase>();
    private eSystemStep currentStep;

    void Awake()
    {
        _instance = this;
        InitGameSteps();
        ConfigDataManager.LoadData();
    }

    private void OnEnable()
    {
        currentStep = eSystemStep.Login;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private void InitGameSteps()
    {
        systemStepDict[eSystemStep.Login] = Login.GetComponent<LoginSystem>();
        systemStepDict[eSystemStep.Menu] = Menu.GetComponent<MenuSystem>();
        systemStepDict[eSystemStep.DataHistory] = HistoryData.GetComponent<DataHistorySystem>();
        systemStepDict[eSystemStep.DataHistory] = DailyData.GetComponent<DailyDataSystem>();
        systemStepDict[eSystemStep.ProductInfo] = ProductInfo.GetComponent<ProductInfoSystem>();
        systemStepDict[eSystemStep.StoreInfo] = StoreInfo.GetComponent<StoreInfoSystem>();
    }

    public void ChangeSystemStep(eSystemStep nextStep)
    {
        SystemStepBase curStep = GetSystemStep(currentStep);
        SystemStepBase next = GetSystemStep(nextStep);
        if (next != null)
        {
            curStep.Hide();
            next.Show();
            currentStep = nextStep;
        }
    }

    private SystemStepBase GetSystemStep(eSystemStep step)
    {
        if (systemStepDict.ContainsKey(step))
        {
            return systemStepDict[step];
        }
        return null;
    }
}
