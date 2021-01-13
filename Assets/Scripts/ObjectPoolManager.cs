using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
     static ObjectPoolManager objPM_instance;
  public   static ObjectPoolManager OBJ_PM_INSTANCE
    {
        get 
        {
            if(objPM_instance == null)
            {

                objPM_instance = FindObjectOfType<ObjectPoolManager>();
                //if we cant object of the above type in scene, 
                //then we must load the object from resources
                if(objPM_instance == null)
                {
                    GameObject newObject = new GameObject("Object Pool Manager: Instance");
                    newObject.AddComponent<ObjectPoolManager>();

                    //if there is nothin assigned , then we must have to create
                    //new instance of the object 
                    objPM_instance.bulletPrefab = new PoolClassifier();
                    objPM_instance = newObject.GetComponent<ObjectPoolManager>();
                    return objPM_instance;
                }
            }
            return objPM_instance; 
        }
        set
        {
            objPM_instance = value;
        }
    }

    public GameObject coinParticleEffect;
    public List<GameObject> coinEffectList = new List<GameObject>();
    public int poolSize = 5, extraIncreaseAmount = 5;

    public PoolClassifier bulletPrefab;
   
    private void Awake()
    {
        OBJ_PM_INSTANCE = this;
    }
    void Start()
    {
       //newChildObj = new GameObject(bulletPrefab.prefabObject.name + " child");
       // newChildObj.transform.parent = transform;
        InitialisePool();
        if(bulletPrefab!=null)
        {
        bulletPrefab.Initialise();
        }

    }
    void InitialisePool()
    {
        IncreasePool(poolSize);
    }

    public void PlayEffect(Vector3 _playAtPosition)
    {
        foreach (var item in coinEffectList)
        {
            // if none is active, then we should increase pool size
            //and control does not goes into below if condition
            if (!item.activeSelf) 
            {
                item.GetComponent<ParticleEffect>().Play(_playAtPosition);
                return;
            }
        }
        IncreasePool(extraIncreaseAmount);
        PlayEffect(_playAtPosition);
    }
    public void IncreasePool(int incrAmount)
    {
        for (int i = 0; i < incrAmount; i++)
        {
            var gob = Instantiate(coinParticleEffect, transform);
            gob.SetActive(false);
            coinEffectList.Add(gob);
        }
    }

}


[System.Serializable]
public class PoolClassifier 
{
   // ObjectPoolManager objpoolMan;
    public GameObject prefabObject;
    public List<GameObject> poolObjectList;
    public int poolSize;
    public int extraIncreaseSize;
   
    public void Initialise()
    {
        poolObjectList = new List<GameObject>();
        if(poolSize<1)
        { 
        poolSize = 5 ;
        }
        IncreasePool(poolSize);
    }
    public GameObject UseFromPoolManager()
    {
        //if no object is assigned, then take from resouces folder in assets
        if (prefabObject==null)
        {
          prefabObject = (GameObject)  Resources.Load("Bullet");
            Initialise();
        }
        foreach (var item in poolObjectList)
        {
            if(!item.activeSelf)
            {
                return item;
            }
        }
        IncreasePool(extraIncreaseSize);
        return UseFromPoolManager();
    }
    void IncreasePool(int size)
    {
        for(int i=0;i<size;i++)
        {
            GameObject go = MonoBehaviour.Instantiate(prefabObject/*, objpoolMan.newChildObj.transform*/);
            go.SetActive(false);
            poolObjectList.Add(go);
        }
    }
}