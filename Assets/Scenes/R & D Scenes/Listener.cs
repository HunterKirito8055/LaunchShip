using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Listener : MonoBehaviour
{
                            //public delegate void clickAction();
                            //public static event clickAction onClick;
    //public Vector3 directionVector;
    Text text;
  [SerializeField] DirectionEnum directionEnum;
    public DirectionEnum DirectEnum
    {
        get { return directionEnum; }
        set
        {
            directionEnum = value;
            //switch (directionEnum)
            //{
            //    case DirectionEnum.left:
            //        directionVector = Vector3.left;
            //        break;
            //    case DirectionEnum.right:
            //        directionVector = Vector3.right;
            //        break;
            //    case DirectionEnum.up:
            //        directionVector = Vector3.up;
            //        break;
            //    case DirectionEnum.down:
            //        directionVector = Vector3.down;
            //        break;
            //    default:
            //        break;
            //}
            text.text = "Dir : " + directionEnum.ToString();
        }
    }
    Action moveAction;
   public  Action MoveAction
    {
        get
        { return moveAction; }
        set
        {
            moveAction = value;
            moveBtn.interactable = !(moveAction == null) ;
        }
    }
    Action<int> dirAction;
    public Action<int> DirAction
    {
        get
        { return dirAction; }
        set
        {
            dirAction = value;
            dirChangeBtn.interactable = !(dirAction == null);
        }
    }

    MoveScript moveScript;
    public Button moveBtn,dirChangeBtn;
    //  public event Action<GameObject> action1;
    private void Awake()
    {
        moveScript = FindObjectOfType<MoveScript>();
    }

    private void Start()
    {
        moveBtn.onClick.AddListener(MoveNow);
        dirChangeBtn.onClick.AddListener(ChangeDirection);
        text = dirChangeBtn.gameObject.GetComponentInChildren<Text>();
    }
   
  public void MoveNow()
    {
        this.MoveAction?.Invoke();
    }
    public int iDir = 0;
    public void ChangeDirection()
    {
        iDir = (int)moveScript.DirEnum;
        iDir++;
        if(iDir>3)
        { iDir = 0; }
        DirAction?.Invoke(iDir);
       // DirectEnum = moveScript.DirEnum;
    }
}
