using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> poweruplist = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            poweruplist.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replay()
    {
        foreach(var item in poweruplist)
        {
            item.SetActive(true);
        }   
    }

   
}
public enum PowerType
{
    None,
    CoinDouble,
    Immune,
    AntiGravity
}
