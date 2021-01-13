using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepCounter : MonoBehaviour
{
    public Text text;
    public MoveScript move;
    private void Awake()
    {
        move = GetComponent<MoveScript>();
    }
    public void StepDisplay(int c)
    {
        text.text = "Steps : " + c.ToString();
    }
}
