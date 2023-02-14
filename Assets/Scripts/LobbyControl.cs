using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyControl : MonoBehaviourPunCallbacks
{
    private readonly Dictionary<string, RoomItemControl> _roomItemsDic = new();

    [SerializeField] private EnterRoomControl _enterRoomControl;
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _roomItemImageP;
    [SerializeField] private Transform _roomsListContentT;
    [SerializeField] private Canvas _thisCanvas;


    public static TypedLobby customLobby;
    public const string MAP_PROP_KEY = "C0";
    public static readonly string sqlMapLobbyFilter = $"{MAP_PROP_KEY} = '{Sceneloader.LobbyName}'";

    private void Awake()
    {
        customLobby = new TypedLobby(Sceneloader.LobbyName, LobbyType.SqlLobby);
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (!_thisCanvas) _thisCanvas = GetComponent<Canvas>();

        _backButton.onClick.AddListener(() =>
        {
            SetActive(false);
            _enterRoomControl.SetActive(true);
        });

        SetActive(false);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var item in roomList)
            if (item.RemovedFromList || item.PlayerCount == item.MaxPlayers || !item.IsOpen)
            {
                if (_roomItemsDic.ContainsKey(item.Name))
                {
                    Destroy(_roomItemsDic[item.Name].gameObject);
                    _roomItemsDic.Remove(item.Name);
                }
            }
            else
            {
                if (_roomItemsDic.ContainsKey(item.Name))
                    _roomItemsDic[item.Name].SetGamersCount(item.PlayerCount);

                else
                {
                    var roomItemT = Instantiate(_roomItemImageP).transform;
                    roomItemT.SetParent(_roomsListContentT, false);
                    var roomItemControl = roomItemT.GetComponent<RoomItemControl>();
                    roomItemControl.Init(item.Name, item.PlayerCount, item.MaxPlayers);
                    _roomItemsDic.Add(item.Name, roomItemControl);
                }
            }
    }

    private void ClearList()
    {
        foreach (var item in _roomItemsDic.Values)
            if (item) Destroy(item.gameObject);
        
        _roomItemsDic.Clear();
    }

    public override void OnJoinedLobby()
    {
        ClearList();
        GetCustomRoomList(sqlMapLobbyFilter);
    }

    public override void OnLeftLobby() => ClearList();

    public override void OnDisconnected(DisconnectCause cause) => ClearList();

    private void OnValidate()
    {
        if (!_enterRoomControl) Debug.LogWarning($"{name}:{nameof(LobbyControl)}.{nameof(_enterRoomControl)} is not defined");
        if (!_roomItemImageP) Debug.LogWarning($"{name}:{nameof(LobbyControl)}.{nameof(_roomItemImageP)} is not defined");
        if (!_roomsListContentT) Debug.LogWarning($"{name}:{nameof(LobbyControl)}.{nameof(_roomsListContentT)} is not defined");
        if (!_backButton) Debug.LogWarning($"{name}:{nameof(LobbyControl)}.{nameof(_backButton)} is not defined");
    }

    private void GetCustomRoomList(string sqlLobbyFilter)
    {
        PhotonNetwork.GetCustomRoomList(customLobby, sqlLobbyFilter);
    }

    public void SetActive(bool isActive)
    {
        _thisCanvas.enabled = isActive;
    }
}