using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonSpawner : MonoBehaviour
{
    public GameObject ButtonPrefab;
    LevelManager levelManagerRef;
    private void Awake()
    {
        levelManagerRef = FindObjectOfType<LevelManager>();
    }
    private void Start()
    {
        //we dont need 0th index, as it is starting panel
        for (int i = 1; i < levelManagerRef.GetTotalScenes(); i++)
        {
            GameObject go = Instantiate(ButtonPrefab, transform);
            go.GetComponent<LevelButtonScript>().Initialise(i,levelManagerRef);
        }
    }
}
//public List<ObjectData> objectDatasList;
//private void Start()
//{
//    for (int i = 0; i < objectDatasList.Count; i++)
//    {
//        GameObject newObject = new GameObject(objectDatasList[i].name);
//        newObject.AddComponent<MeshFilter>();
//        newObject.AddComponent<MeshRenderer>();

//        newObject.transform.parent = transform;

//        newObject.GetComponent<MeshFilter>().mesh = objectDatasList[i].meshType;
//        newObject.GetComponent<MeshRenderer>().material = objectDatasList[i].materialType;

//        newObject.transform.position = objectDatasList[i].position;
//        newObject.transform.localScale = objectDatasList[i].scale;
//    }
//}
//[System.Serializable]
//public class ObjectData
//{

//    public string name;
//    public Mesh meshType;
//    public Material materialType;
//    public Vector3 position;
//    public Vector3 scale;
//}
