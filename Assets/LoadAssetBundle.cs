using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;

public class LoadAssetBundle : MonoBehaviour
{
    public PercentageEvent percentageEvent;
    public Image fillImage;
    AssetBundle assetBundle;
    public List<string> assetLists;
    public GameObject[] gameObjects;
    //const string prefabBundlePath = @"V:\work\LAUNCH SHIP OFFICE\launchShipTrain\AssetBundle\prefab bundle";
    public string persistentPath = "";
     const string dataDownloadURL = @"https://drive.google.com/u/1/uc?id=1gmPLDd_gF6Q0bpATaaVY6dvocUQoSv8a&export=download";
     byte[] memoryByte;
    private void Start()
    {
        persistentPath = Application.persistentDataPath;
        // LoadAssets();
        StartCoroutine(GetDownloadedFile());
    }
    IEnumerator GetDownloadedFile()
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(dataDownloadURL);
        DownloadHandler downloadHandler = unityWebRequest.downloadHandler;
        AsyncOperation async = unityWebRequest.SendWebRequest();
        //  unityWebRequest.SendWebRequest();

        while (!async.isDone)
        {
            //fillImage.fillAmount = async.progress;
            percentageEvent?.Invoke(async.progress);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (downloadHandler.isDone)
        {
            fillImage.fillAmount = 1;
            Debug.Log("downloaded file is : " + downloadHandler.isDone);
            memoryByte = downloadHandler.data;
        }
        SaveDataFile();
        LoadAssets(memoryByte);
        yield return new WaitForSeconds(Time.deltaTime);
    }
    void SaveDataFile()
    {
        File.WriteAllBytes(persistentPath + "/characters", memoryByte);

    }
    void LoadAssets(byte[] _memoryData)
    {
        //assetBundle = AssetBundle.LoadFromFile(prefabBundlePath);
        assetBundle = AssetBundle.LoadFromFile(persistentPath + "/characters");
        //assetBundle = AssetBundle.LoadFromMemory(_memoryData);
        if (assetBundle)
        {
            print("File Exists already");
        }
        else
        {
            return;
        }
        gameObjects = assetBundle.LoadAllAssets<GameObject>();
        for (int i = 0; i < gameObjects.Length; i++)
        {
           Instantiate(gameObjects[i],Vector3.right * 2*i ,Quaternion.identity);
        }
    }
}

[System.Serializable]
public class PercentageEvent : UnityEvent<float>
{

}