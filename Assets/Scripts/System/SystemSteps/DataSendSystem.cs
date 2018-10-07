using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSendSystem : SystemStepBase
{
    public Text Text_state;
    public Image StateBack;

    public InputField Input_Ip;
    public InputField Input_Port;

    public string IP;
    public int Port = 12345;
    public eSenderState CurSenderState = eSenderState.Closed;

    private bool isSettingConnected = false;
    private bool isSettingDisConnected = false;

    private void Awake()
    {
        GameEvents.AddGameEvent(eGameEventTypes.DataSendConnected, OnConnected);
        GameEvents.AddGameEvent(eGameEventTypes.DataSendDisConnected, OnDisconnect);
        GameEvents.AddGameEvent(eGameEventTypes.DataSenderSyncOver, OnDataSyncOver);
    }

    private void OnEnable()
    {
        Input_Ip.text = IP;
        Input_Port.text = Port.ToString();
    }

    private void Update()
    {
        if (isSettingConnected && CurSenderState == eSenderState.Connected)
        {
            isSettingConnected = false;
            Text_state.text = "已连接";
            StateBack.color = Color.green;
        }
        else if(isSettingDisConnected)
        {
            isSettingDisConnected = false;
            ChangeSendState(eSenderState.Closed);
        }
    }

    public void OnConnected()
    {
        ChangeSendState(eSenderState.Connected);
    }

    public void OnDisconnect()
    {
        isSettingDisConnected = true;
    }

    public void OnDataSyncOver()
    {
        ChangeSendState(eSenderState.SendOver);
    }

    public void OnClickConnect()
    {
        SalesDataSystem.SyncDataMgr.BeginConnect(IP, Port);
    }

    public void OnClickTransfer()
    {
        if (CurSenderState == eSenderState.Connected)
            ChangeSendState(eSenderState.Sending);
    }

    public void OnClickStopConnect()
    {
        ChangeSendState(eSenderState.Closed);
    }

    public void OnIpValueChanged(string value)
    {
        IP = value;
    }

    public void OnPortValueChanged(string value)
    {
        Port = int.Parse(value);
    }

    private void ChangeSendState(eSenderState state)
    {
        CurSenderState = state;
        switch (state)
        {
            case eSenderState.Closed:
                SalesDataSystem.SyncDataMgr.StopConnect();
                Text_state.text = "未连接";
                StateBack.color = Color.red;
                break;
            case eSenderState.Connected:
                isSettingConnected = true;
                //无法在连接上的线程给text赋值
                //Text_state.text = "已连接";
                break;
            case eSenderState.Sending:
                SalesDataSystem.SyncDataMgr.SendLoalData();
                Text_state.text = "正在传输";
                StateBack.color = Color.yellow;
                break;
            case eSenderState.SendOver:
                Text_state.text = "传输完成";
                StateBack.color = Color.red;
                SalesDataSystem.TipsShow.ShowTipsInfo("传输完成");
                break;
            default:
                break;
        }
    }

    public override void OnClickBack()
    {
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.Menu);
    }
}
