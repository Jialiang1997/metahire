using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    [field: SerializeField, Range(0, 17)]
    public int SceneToLoad { get; private set; }

    [field: SerializeField, Tooltip("[ReadOnly] The Scene Name to be loaded")]
    public string SceneName { get; private set; }

    public static string LobbyName = string.Empty;

    private void OnValidate() =>
        SceneName = $"Scene{SceneToLoad:D2}";

    private void Awake() => LoadScene();

    private void Start()
    {
        if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
    }

    public void LoadScene()
    {
        LobbyName = SceneName;
        PhotonNetwork.LoadLevel(SceneName);
        SceneManager.LoadSceneAsync("mainlogin", LoadSceneMode.Additive);
    }
}