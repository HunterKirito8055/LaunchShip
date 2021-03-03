using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Listener listener;
    [SerializeField] DirectionEnum dirEnum;
    public DirectionEnum DirEnum
    {
        get { return dirEnum; }
        set
        {
            dirEnum = value;
            if(dirEnum>DirectionEnum.down)
            { dirEnum = 0; 
            }
            switch (dirEnum)
            {
                case DirectionEnum.left:
                    this.transform.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
                    break;
                case DirectionEnum.right:
                    this.transform.rotation = Quaternion.AngleAxis(270f, Vector3.forward);
                    break;
                case DirectionEnum.up:
                    this.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                    break;
                case DirectionEnum.down:
                    this.transform.rotation = Quaternion.AngleAxis(180f, Vector3.forward);
                    break;
                default:
                    break;
            }

        }
    }
    public bool toggle ;
    public bool Toggle
    {
        get
        { return toggle; }
        set
        {
            toggle = value;
            DirectionEnum temp;
            if (toggle)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                this.listener.MoveAction += MoveNow;
                this.listener.DirAction += NavigationSide;
                temp = DirEnum;
            }
            else
            {
               
                this.listener.MoveAction -= MoveNow;
                this.listener.DirAction -= NavigationSide;
                 temp = DirEnum;
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

        }

    }
    //public bool sideways;
    // public bool SideBool
    // {
    //     get
    //     { return sideways; }
    //     set
    //     {
    //         sideways = value;
    //         if (sideways)
    //             listener.SideAction += MoveSide;
    //         else
    //             listener.SideAction -= MoveSide;

    //     }
    // }


    private void Awake()
    {
        listener = FindObjectOfType<Listener>();
    }
    private void Start()
    {
        Toggle = false;
       // this.listener.DirectEnum = DirEnum;
    }
    public void MoveNow()
    {

        this.transform.position += transform.up;
        if (this.transform.position.y > 4.5f)
        {
            this.transform.position = new Vector3(this.transform.position.x, -4.5f, this.transform.position.z);
        }
        if (this.transform.position.y < -4.5f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 4.5f, this.transform.position.z);
        }
        if (this.transform.position.x < -9f)
        {
            this.transform.position = new Vector3(9f, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x > 9f)
        {
            this.transform.position = new Vector3(-9f, this.transform.position.y, this.transform.position.z);
        }
    }
    public void NavigationSide(int dir)
    {
        DirEnum = (DirectionEnum)dir;
        //print(dir);
        switch (DirEnum)
        {
            case DirectionEnum.left:
                this.transform.rotation = Quaternion.AngleAxis(90f, Vector3.forward);
                break;
            case DirectionEnum.right:
                this.transform.rotation = Quaternion.AngleAxis(270f, Vector3.forward);
                break;
            case DirectionEnum.up:
                this.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                break;
            case DirectionEnum.down:
                this.transform.rotation = Quaternion.AngleAxis(180f, Vector3.forward);
                break;
            default:
                break;
        }
        
    }
    private void OnMouseDown()
    {
        Toggle = !Toggle;
        //SideBool = !SideBool;
    }


}
public enum DirectionEnum
{
    left, up, right, down
}
