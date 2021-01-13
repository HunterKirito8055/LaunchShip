using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour
{
    Button button;
    int sceneIndex;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Initialise(int _index,LevelManager levelManager)
    {
        sceneIndex = _index;
        button.onClick.AddListener(
            ()=> { levelManager.LoadLevelByIndex(sceneIndex);  }
            );
        button.GetComponentInChildren<Text>().text = "Level"+(_index+1);
    }
    
}
