using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebVoiceTrigger : MonoBehaviour
{
    public GameObject tipImage;//

    public GameObject enterRoomImage;//


    // Start is called before the first frame update
    void Start()
    {
        tipImage.SetActive(true);//
        enterRoomImage.SetActive(true); //
    }

    /// <summary>
    /// 
    /// </summary>

    public void ShowDialog()
    {
        tipImage.SetActive(false);
        enterRoomImage.SetActive(true);
        //PhotonVoiceNetwork.Instance.PrimaryRecorder.TransmitEnabled = true;
        //foreach (Speaker item in Component.FindObjectsOfType<Speaker>())
        //{
        //    item.enabled = true;
        //}
    }

    public void RemoveDialog()
    {
        tipImage.SetActive(true);
        enterRoomImage.SetActive(false);
        //PhotonVoiceNetwork.Instance.PrimaryRecorder.TransmitEnabled = false;
        //foreach (Speaker item in Component.FindObjectsOfType<Speaker>())
        //{
        //    item.enabled = false;
        //}
    }
}