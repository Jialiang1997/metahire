using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public int currentMap;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(0 == 0);
    }
    
    private void SelectMap(int _index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);

        }
        
    }
    public void ChangeMap(int _change)
    {
        currentMap += _change;
        if(currentMap > 3)
        {
            currentMap = 0;
        }
        else if (currentMap < 0)
        {
            currentMap = 3;
        }
        SelectMap(currentMap);
        
    }
}
