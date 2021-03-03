using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // public GameObject bullet;
    //public Transform ShootPoint;
    AudioSource audioSource;
    GameObject newbullet;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ShootBullet(Vector3 _shootPos, Quaternion _rotation)
    {
        newbullet = GetObjectFromPool();
        newbullet.SetActive(true);
        if (GameManager.sharedInstance.settingsData.SoundInfo)
            audioSource.Play();
        newbullet.transform.position = _shootPos;
        newbullet.transform.localRotation = _rotation;
        newbullet.GetComponent<Rigidbody>().AddForce(newbullet.transform.forward * 500f);
    }


    GameObject GetObjectFromPool()
    {
        return ObjectPoolManager.OBJ_PM_INSTANCE.bulletPrefab.UseFromPoolManager();
    }
}
