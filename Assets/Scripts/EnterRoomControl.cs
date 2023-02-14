using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterRoomControl : MonoBehaviourPunCallbacks
{
    [Header("Room Info")]
    [SerializeField, Range(1, 16)] private int _roomCapacity = 4;
    [Header("Controllers")]
    [SerializeField] private LobbyControl _lobbyControl;
    [SerializeField] private LoginControl _loginControl;
    [SerializeField] private TMP_InputField _roomNameInputField;
    [SerializeField] private Button _joinButton;
    [SerializeField] private Button _lobbyButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _errorText;

    [SerializeField] private Canvas _thisCanvas;

    private void Start()
    {
        if (!_thisCanvas) _thisCanvas = GetComponent<Canvas>();
        _roomNameInputField.onValueChanged.AddListener(input => { _errorText.text = string.Empty; });

        _joinButton.onClick.AddListener(() =>
        {
            if (_roomNameInputField.text == string.Empty)
            {
                _errorText.text = "The room name cannot be empty!";
                return;
            }

            JoinOrCreateRoom();
        });

        _lobbyButton.onClick.AddListener(() =>
        {
            if (!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();
            SetActive(false);
            _errorText.text = string.Empty;
            _lobbyControl.SetActive(true);
        });

        _backButton.onClick.AddListener(() =>
        {
            SetActive(false);
            _errorText.text = string.Empty;
            _loginControl.SetActive(true);
        });

        SetActive(false);
    }

    private void OnValidate()
    {
        if (!_lobbyControl) Debug.LogWarning($"{name}:{nameof(EnterRoomControl)}.{nameof(_lobbyControl)} is not defined");
        if (!_loginControl) Debug.LogWarning($"{name}:{nameof(EnterRoomControl)}.{nameof(_loginControl)} is not defined");
        if (!_roomNameInputField) Debug.LogWarning($"{name}:{nameof(EnterRoomControl)}.{nameof(_roomNameInputField)} is not defined");
        if (!_joinButton) Debug.LogWarning($"{name}:{nameof(EnterRoomControl)}.{nameof(_joinButton)} is not defined");
        if (!_lobbyButton) Debug.LogWarning($"{name}:{nameof(EnterRoomControl)}.{nameof(_lobbyButton)} is not defined");
        if (!_backButton) Debug.LogWarning($"{name}:{nameof(EnterRoomControl)}.{nameof(_backButton)} is not defined");
    }

    /// <summary>
    /// See docs <see href="https://doc.photonengine.com/pun/v2/lobby-and-matchmaking/matchmaking-and-lobby#default_lobby_type">
    /// here</see>.
    /// </summary>
    private void JoinOrCreateRoom()
    {
        var roomOptions = new RoomOptions
        {
            MaxPlayers = System.Convert.ToByte(_roomCapacity),
            CustomRoomProperties = new Hashtable { { LobbyControl.MAP_PROP_KEY, Sceneloader.LobbyName } },
            CustomRoomPropertiesForLobby = new string[] { LobbyControl.MAP_PROP_KEY }
        };
        PhotonNetwork.JoinOrCreateRoom(_roomNameInputField.text, roomOptions, LobbyControl.customLobby);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Room Joined failed with error code {0} and error message {1}", returnCode, message);
        _errorText.text = message;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarningFormat("Room creation failed with error code {0} and error message {1}", returnCode, message);
        _errorText.text = message;
    }

    public void SetActive(bool isActive) => _thisCanvas.enabled = isActive;
}