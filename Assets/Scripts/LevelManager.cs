using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int currentIndex;
    int totalIndex;

    private void Awake()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
        totalIndex = SceneManager.sceneCountInBuildSettings;
        //if (currentIndex > 0)
        //{
        //    Play();
        //}
    }
    public int GetTotalScenes()
    {
        return totalIndex;
    }
    public void LoadLevelByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadNextLevel()
    {

        if (totalIndex > currentIndex + 1)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
            print("All Levels Completed! ");
    }
    public void LoadPreviousLevel()
    {
        int prevSceneIndex = currentIndex - 1;
        if (prevSceneIndex >= 0)
        {
            SceneManager.LoadScene(prevSceneIndex);
        }
        else { print("No Previous Level"); }
    }
    private void Play()
    {
        GameManager.sharedInstance.Play();
    }

    public int GetCurrentSceneIndex()
    {
        return currentIndex;
            
    }
   

    public bool IsNextLevelThere()
    {
        return totalIndex > currentIndex + 1;
    }
    public bool IsPrevLevelThere()
    {
       
        return currentIndex - 1 >= 1;
    }
}
