using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   // public GameObject bullet;
    public Transform ShootPoint;
    AudioSource audioSource;
    float shootTimer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public  void ShootBullet()
    {
                GameObject newbullet = GetObjectFromPool();
                newbullet.SetActive(true);
        if(GameManager.sharedInstance.settingsData.SoundInfo)
                 audioSource.Play();
                newbullet.transform.position = ShootPoint.position;
                newbullet.transform.rotation = Quaternion.identity;
                newbullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
    }
  

    GameObject GetObjectFromPool()
    {
        return ObjectPoolManager.OBJ_PM_INSTANCE.bulletPrefab.UseFromPoolManager();
    }
}
