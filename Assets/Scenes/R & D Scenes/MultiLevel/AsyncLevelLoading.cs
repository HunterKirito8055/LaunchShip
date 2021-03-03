using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLevelLoading : MonoBehaviour
{
    [SerializeField] OnFloatMod onChangingScene;
    [SerializeField] OnVectorMod onTeleportEvent;
    [SerializeField] OnBoolMod onPlayerStatus;
    public static AsyncLevelLoading sharedInstance;
    Vector3 pos;
    public Transform playerTransform;
    private void Awake()
    {
        sharedInstance = this;
    }
    public void LoadNextLevel(int nextlvlInd, int prevLvlInd, Vector3 position)
    {
        pos = position;
        StartCoroutine(Load(nextlvlInd));
        if (prevLvlInd>0)
        {
            StartCoroutine(Unload(prevLvlInd));
        }
    }
    public void UnloadCurrentLevel(int nextlvlInd, int prevLvlInd, Vector3 position)
    {
        pos = position;
        StartCoroutine(Unload(nextlvlInd));
        if (prevLvlInd > 0)
        {
            StartCoroutine(Load(prevLvlInd));
        }
    }

    IEnumerator Load(int ind)
    {
        onPlayerStatus.Invoke(false);
        yield return StartCoroutine(Fadein());
        AsyncOperation async = SceneManager.LoadSceneAsync(ind, LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        onTeleportEvent.Invoke(pos);
        yield return StartCoroutine(Fadeout());
        onPlayerStatus.Invoke(true);
    }
    IEnumerator Unload(int ind)
    {
        onPlayerStatus.Invoke(false);
        yield return StartCoroutine(Fadein());
        AsyncOperation async = SceneManager.UnloadSceneAsync(ind);
        while(!async.isDone)
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        onTeleportEvent.Invoke(pos);
        yield return StartCoroutine(Fadeout());
        onPlayerStatus.Invoke(true);
    }

    IEnumerator Fadein()
    {
        float _0timer = 0;
        while (_0timer < 1f)
        {
            _0timer += Time.deltaTime;
            onChangingScene.Invoke(_0timer);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    IEnumerator Fadeout()
    {
        float _1timer =1;
        while (_1timer > 0f)
        {
            _1timer -= Time.deltaTime;
            onChangingScene.Invoke(_1timer);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
