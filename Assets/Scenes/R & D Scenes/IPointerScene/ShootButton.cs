using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    float fTimer = 0;
    bool clicked;


    private void Update()
    {
        if (clicked)
        {
            fTimer += Time.deltaTime;
            if (fTimer > 0.5)
            {
                Debug.Log("shoot");
                fTimer = 0f;
            }
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        clicked = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        clicked = false;
    }
}
