using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> CoinsList = new List<GameObject>();
    private int childCount;
    private int maxChildCount;
    public int ChildCount
    {
        get { return childCount; }
        set
        {
            childCount = value;
            if (childCount <= 0)
            {
                childCount = 0;
                GameManager.sharedInstance.YouWon();
            }
        }
    }
    void Start()
    {
        ChildCount = transform.childCount;
        maxChildCount = ChildCount;
        for (int i = 0; i < ChildCount; i++)
        {
            CoinsList.Add(transform.GetChild(i).gameObject);
        }
    }
    public void ReduceChildCount()
    {
        ChildCount--;
    }
    public void ResetCoins()
    {
        foreach (var item in CoinsList)
        {
            item.SetActive(true);
        }
        ChildCount = maxChildCount;
    }
}
