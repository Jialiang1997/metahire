using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneloader : MonoBehaviour
{
    public GameObject WelcomeWindow;
    public GameObject SelectionWindow;
    public GameObject MapHolder;
    private string _sceneName;

    private void Start()
    {
        SelectionWindow.SetActive(false);

    }
    private void Update()
    {
        
        // Create a temporary reference to the current scene.
        //Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        //string sceneName = currentScene.name;

        if (Input.GetKeyDown(KeyCode.F))
        {
            WelcomeWindow.SetActive(false);
            SelectionWindow.SetActive(true);

            
        }
    }
    public void LoadScene()
    {
        int MapNumber = MapHolder.GetComponent<MapSelection>().currentMap;
        switch (MapNumber)
        {
            case 0:
                _sceneName = "Scene01";
                break;
            case 1:
                _sceneName = "Scene03";
                break;
            case 2:
                _sceneName = "Scene11";
                break;
            case 3:
                _sceneName = "Scene05";
                break;
            case 4:
                _sceneName = "Scene04";
                break;
            case 5:
                _sceneName = "Scene05";
                break;
            case 6:
                _sceneName = "Scene06";
                break;
            case 7:
                _sceneName = "Scene07";
                break;
            case 8:
                _sceneName = "Scene08";
                break;
            case 9:
                _sceneName = "Scene09";
                break;
            case 10:
                _sceneName = "Scene10";
                break;
            case 11:
                _sceneName = "Scene11";
                break;
            case 12:
                _sceneName = "Scene12";
                break;
            case 13:
                _sceneName = "Scene13";
                break;
            case 14:
                _sceneName = "Scene14";
                break;
            case 15:
                _sceneName = "Scene15";
                break;
            case 16:
                _sceneName = "Scene16";
                break;
            case 17:
                _sceneName = "Scene17";
                break;
        }
        PhotonNetwork.LoadLevel(_sceneName);
        SceneManager.LoadSceneAsync("mainlogin", LoadSceneMode.Additive);
    }
}