using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
    
}

[System.Serializable] public class OnFloatMod : UnityEvent<float> { }
[System.Serializable] public class OnVectorMod : UnityEvent<Vector3> { }
[System.Serializable] public class OnBoolMod : UnityEvent<bool> { }

