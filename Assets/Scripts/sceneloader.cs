using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{

    [field: SerializeField, Range(0, 17)]
    public int SceneToLoad { get; private set; }

    [field: SerializeField, Tooltip("[ReadOnly] The Scene Name to be loaded")]
    public string SceneName { get; private set; }

    [field: SerializeField, Tooltip("Orthographic Size of Player Cinemachine Camera"), Range(0, 15)]
    public float OrthographicSizeToLoad { get; set; }


    [Header("Cinemachine Camera Preset")]
    public float[] OrthographicSizePresetForSence = new float[17];

    public static string LobbyName = string.Empty;
    public static float OrthographicSize = -1f;

    private void OnValidate()
    { 
        SceneName = $"Scene{SceneToLoad:D2}";
        
        if (SceneToLoad > OrthographicSizePresetForSence.Length)
            Debug.LogWarning($"{nameof(Sceneloader)}.{nameof(OrthographicSizePresetForSence)}: {nameof(SceneToLoad)} is out of range");
        else
            OrthographicSizeToLoad = OrthographicSizePresetForSence[SceneToLoad] > 0? 
                OrthographicSizePresetForSence[SceneToLoad]: -1;
    }

    private void Awake() => LoadScene();

    private void Start()
    {
        if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
    }

    public void LoadScene()
    {
        LobbyName = SceneName;
        OrthographicSize = OrthographicSizeToLoad;
        PhotonNetwork.LoadLevel(SceneName);
        SceneManager.LoadSceneAsync("mainlogin", LoadSceneMode.Additive);
    }
}