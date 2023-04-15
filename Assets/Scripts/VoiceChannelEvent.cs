using Byn.Unity.Examples;
using System;
using TMPro;
using UnityEngine;

public class VoiceChannelEvent : MonoBehaviour
{

    private TMP_InputField uRoomName;
    private ConferenceApp conferenceApp;

    private void Awake()
    {
        conferenceApp = gameObject.GetComponent<ConferenceApp>();
        uRoomName = conferenceApp.uRoomName;
    }

    private void OnEnable()
    {
        VoiceTrigger.EnterRoomEvent += OnEnterRoom;
        VoiceTrigger.LeaveChannelEvent += OnLeaveRoom;
    }


    private void OnDisable()
    {
        VoiceTrigger.EnterRoomEvent -= OnEnterRoom;
        VoiceTrigger.LeaveChannelEvent -= OnLeaveRoom;
    }


    void OnEnterRoom(object sender, EnterRoomEventArgs roomArgs)
    {
        uRoomName.text = roomArgs.RoomChannel;
        conferenceApp.JoinButtonPressed();
    }

    void OnLeaveRoom(object sender, EventArgs args)
    {
        conferenceApp.ShutdownButtonPressed();
    }
}

public class EnterRoomEventArgs : EventArgs
{
    public string RoomChannel { get; set; }
}
