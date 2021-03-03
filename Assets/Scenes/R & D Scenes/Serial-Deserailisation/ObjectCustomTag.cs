using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCustomTag : MonoBehaviour
{
    public Tag tagName;
}

public enum Tag
{
    sphere, cube, cylinder,capsule
}