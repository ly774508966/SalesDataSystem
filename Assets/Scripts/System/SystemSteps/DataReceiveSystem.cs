using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataReceiveSystem : SystemStepBase
{
    public Text Text_ButtonShow;
    public Text Text_IP;
    public InputField Input_Port;
    public Image StateBack;
    public Text Text_State;
    public int Port = 12345;
    public string IP;
    public eReceiverState CurReceiveState = eReceiverState.Closed;

    private bool isSettingConnected = false;
    private bool isSettingDisconnected = false;

    private void Awake()
    {
        GameEvents.AddGameEvent(eGameEventTypes.DataReceiverConnected,OnConnected);
        GameEvents.AddGameEvent(eGameEventTypes.DataReceiverDisConnected, OnDisconnected);
        GameEvents.AddGameEvent(eGameEventTypes.DataReceiverSyncing, StartReceiving);
        GameEvents.AddGameEvent(eGameEventTypes.DataReceiverSyncOver, ReceivingOver);
    }

    private void OnEnable()
    {
        IP = Utility.GetLocalIpv4().ToString();
        Text_IP.text = IP;
        Input_Port.text = Port.ToString();
    }

    private void Update()
    {
        if (isSettingConnected && CurReceiveState == eReceiverState.Connected)
        {
            isSettingConnected = false;
            Text_State.text = "客户端已连接";
            StateBack.color = Color.green;
        }
        else if (isSettingDisconnected)
        {
            isSettingDisconnected = false;
            ChangeReceiveState(eReceiverState.Closed);
        }
    }

    public void OnClickListen()
    {
        switch (CurReceiveState)
        {
            case eReceiverState.Closed:
                ChangeReceiveState(eReceiverState.Wait);
                break;
        }
    }

    public void OnClickStopListen()
    {
        ChangeReceiveState(eReceiverState.Closed);
    }

    public void OnConnected()
    {
        ChangeReceiveState(eReceiverState.Connected);
    }

    public void OnDisconnected()
    {
        isSettingDisconnected = true;
    }

    public void StartReceiving()
    {
        ChangeReceiveState(eReceiverState.Receiving);
    }

    public void ReceivingOver()
    {
        ChangeReceiveState(eReceiverState.SyncOver);
    }

    private void ChangeReceiveState(eReceiverState state)
    {
        CurReceiveState = state;
        switch (state)
        {
            case eReceiverState.Closed:
                SalesDataSystem.SyncDataMgr.StopListen();
                Text_State.text = "未开始接收";
                StateBack.color = Color.red;
                break;
            case eReceiverState.Wait:
                SalesDataSystem.SyncDataMgr.StartListen(Port);
                Text_State.text = "等待客户端连接";
                StateBack.color = Color.blue;
                break;
            case eReceiverState.Connected:
                isSettingConnected = true;
                //无法在回调中直接给text赋值
                //Text_State.text = "客户端已连接";
                break;
            case eReceiverState.Receiving:
                Text_State.text = "正在接收数据";
                StateBack.color = Color.yellow;
                break;
            case eReceiverState.SyncOver:
                Text_State.text = "接收完毕";
                StateBack.color = Color.red;
                SalesDataSystem.TipsShow.ShowTipsInfo("接收完毕");
                break;
            default:
                break;
        }
    }

    public void OnPortValueChanged(string value)
    {
        Port = int.Parse(value);
    }

    public override void OnClickBack()
    {
        ChangeReceiveState(eReceiverState.Closed);
        SalesDataSystem.Instance.ChangeSystemStep(eSystemStep.Menu);
    }
}
