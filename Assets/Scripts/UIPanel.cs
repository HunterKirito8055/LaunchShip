using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    public EUI_PANEL eui_Panel;
    CanvasGroup canvasGroup;
    float alpha;
    public  float transitionSpeed = 0.5f;
    public float Alpha
    {
        get
        {
            return alpha;
        }
        set
        {
            alpha = value;
            if (alpha >= 1)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                isTransitioned = true;
            }
            else if(alpha <=0)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                isTransitioned = true;
            }
            canvasGroup.alpha = alpha;
        }
    }

   public bool isTransitioned,isHidden;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        if (isTransitioned)
        {
            return;
        }
        if (isHidden)
        {
            if (Alpha > 0)
            {
                Alpha -= Time.unscaledDeltaTime * transitionSpeed;
            }
        }
        else
        {
            if (Alpha < 1)
            {
                Alpha += Time.unscaledDeltaTime * transitionSpeed;
            }
        }
       
    }
    public void Show()
    {
        // gameObject.SetActive(true);
        Alpha = 0; //zero -> 1 we must not directly show, we should transition
        isTransitioned = false;
        isHidden = false;
    }
    public void Hide()
    {
        //gameObject.SetActive(false);
        Alpha = 1; // 1 -> 0  we must not directly show, we should transition
        isTransitioned = false;
        isHidden = true;
    }

}
