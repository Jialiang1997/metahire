using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoiceTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tipImage;
    [SerializeField] private TextMeshProUGUI _enterRoomImage;
    [SerializeField] private Collider2D _voiceTriggerArea;
    public GameObject voiceCanvas;
    public GameObject voiceWindow;

    private bool _entered = false;

    private void Start()
    {
        if (!_voiceTriggerArea) _voiceTriggerArea = GetComponent<Collider2D>();
        _tipImage.enabled = true;
        _enterRoomImage.enabled = false;
    }

    private void Update()
    {
        if (_entered && Input.GetKeyDown(KeyCode.E))
        {
            Scene second = SceneManager.GetSceneByName("mainlogin");
            GameObject[] gameObjects = second.GetRootGameObjects();
            voiceCanvas = GameObject.FindGameObjectWithTag("VoiceCanvas");
            voiceWindow = voiceCanvas.transform.GetChild(0).gameObject;

            if (voiceWindow.activeSelf)
            {
                voiceWindow.SetActive(false);
            }
            else if (!voiceWindow.activeSelf)
            {
                voiceWindow.SetActive(true);
            }

            ToggleDialog();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetPhotonView().IsMine)
            _entered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetPhotonView().IsMine)
        {
            ToggleDialog(false);
            _entered = false;
        }
    }

    private void OnValidate()
    {
        if (!_tipImage) Debug.LogWarning("Missing tip for triggering voice chat");
        if (!_enterRoomImage) Debug.LogWarning("Missing tip for chatting");
    }

    /// <summary>
    ///     Toggle the status of microphone and speaker
    /// </summary>
    public void ToggleDialog(bool value)
    {
        _tipImage.enabled = !value;
        _enterRoomImage.enabled = value;
        //PhotonVoiceNetwork.Instance.PrimaryRecorder.TransmitEnabled = value;
        //foreach (var item in FindObjectsOfType<Speaker>())
        //{
        //    item.enabled = value;
        //}
    }

    public void ToggleDialog()
    {
        ToggleDialog(!_enterRoomImage.enabled);
    }
}