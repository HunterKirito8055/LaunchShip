using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;

public class LoadAssetBundle : MonoBehaviour
{
    public OnFloatMod percentageEvent;
    public Image fillImage;
    AssetBundle assetBundle;
    public List<string> assetLists;
    public GameObject[] gameObjects;
    //const string prefabBundlePath = @"V:\work\LAUNCH SHIP OFFICE\launchShipTrain\AssetBundle\prefab bundle";
    public string persistentPath = "";
    const string dataDownloadURL = @"https://drive.google.com/u/1/uc?id=1qk3djmFmlETjo9-96n5FuUk36_T67MHt&export=download";
    //const string dataDownloadURL = @"https://drive.googgle.com/u/0/uc?id=1h8Fst_490qKwh-J2itkti3uuZ2fTsNry&export=download"; //Anands' File link
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
        }
        memoryByte = downloadHandler.data;
        SaveDataFile();
       StartCoroutine(LoadAssets());
        yield return new WaitForSeconds(Time.deltaTime);
    }
    void SaveDataFile()
    {
        File.WriteAllBytes(persistentPath + "/characters", memoryByte);

    }
    IEnumerator LoadAssets()
    {
        //assetBundle = AssetBundle.LoadFromFile(prefabBundlePath);
        assetBundle = AssetBundle.LoadFromFile(persistentPath + "/characters");
        //assetBundle = AssetBundle.LoadFromMemory(_memoryData);
        if (assetBundle)
        {
            gameObjects = assetBundle.LoadAllAssets<GameObject>();
            for (int i = 0; i < gameObjects.Length; i++)
            {
                GameObject go = Instantiate(gameObjects[i], Vector3.right * 2 * i, Quaternion.identity);
                go.SetActive(false);
                yield return new WaitForSeconds(Random.Range(0.4f, 0.9f));
            }
        }
        else
        {
            print("no assetBundle exists");
        }
        
    }

}//class

