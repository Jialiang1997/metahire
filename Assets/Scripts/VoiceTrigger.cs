using Photon.Pun;
using System;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;

public class VoiceTrigger : MonoBehaviour
{
    public static event EventHandler<EnterRoomEventArgs> EnterRoomEvent;
    public static event EventHandler LeaveChannelEvent;

    [SerializeField] private TextMeshProUGUI _tipImage;
    [SerializeField] private TextMeshProUGUI _enterRoomImage;
    [SerializeField] private Collider2D _voiceTriggerArea;
    [SerializeField] private Canvas _canvas;

    [Header("Read-Only")]
    [SerializeField] private long instanceID;

    private bool _entered = false;

    private void Start()
    {
        _tipImage.enabled = true;
        _enterRoomImage.enabled = false;
    }

    private void Update()
    {
        if (_entered && Input.GetKeyDown(KeyCode.E))
            ToggleDialog();
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
        if (!_voiceTriggerArea) _voiceTriggerArea = GetComponent<Collider2D>();
        if (!_canvas)
        {
            _canvas = transform.parent.GetComponentInChildren<Canvas>();
            if (!_canvas) Debug.LogError($"{gameObject.name}:{nameof(VoiceTrigger)}.{nameof(_canvas)} is not defined");
        }

#if UNITY_EDITOR
        if (_canvas) instanceID = GetObjectLocalIdInFile(_canvas);
#endif
    }

    /// <summary>
    ///     Toggle the status of microphone and speaker
    /// </summary>
    public void ToggleDialog(bool value)
    {
        if (!_canvas)
        {
            Debug.LogError($"{gameObject.name}:{nameof(VoiceTrigger)}.{nameof(_canvas)} is not defined. Please notify Admin.");
            return;
        }

        _tipImage.enabled = !value;
        _enterRoomImage.enabled = value;

        if (value)
        {
            var args = new EnterRoomEventArgs() { RoomChannel = instanceID.ToString() };
            EnterRoomEvent?.Invoke(this, args);
        }
        else
        {
            LeaveChannelEvent?.Invoke(this, EventArgs.Empty);
        }

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

#if UNITY_EDITOR
    public static long GetObjectLocalIdInFile(UnityEngine.Object _object)
    {
        long idInFile = 0;
        SerializedObject serialize = new SerializedObject(_object);
        PropertyInfo inspectorModeInfo =
            typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
        if (inspectorModeInfo != null)
            inspectorModeInfo.SetValue(serialize, InspectorMode.Debug, null);
        SerializedProperty localIdProp = serialize.FindProperty("m_LocalIdentfierInFile");
#if UNITY_5 || UNITY2017_2_OR_NEWER
            idInFile = localIdProp.longValue;
#else
        idInFile = localIdProp.intValue;
#endif
        return idInFile;
    }
#endif
}