using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public  class BaseInputter : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Color color;
    Color highlightedColor = Color.HSVToRGB(1, 1, 0.3f);
    [SerializeField] UnityEvent onTap;
    [SerializeField] UnityEvent onReleased;
    [SerializeField] UnityEvent onHold;

    public bool clicked = false;
    Image img;
    public virtual void Start()
    {
        img = GetComponent<Image>();
        color = img.color;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        clicked = true;
        img.color = highlightedColor;
        onTap.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clicked = false;
        img.color = color;
        onReleased.Invoke();
    }

    protected virtual void Update()
    {
        if (clicked)
        {
            onHold.Invoke();
        }
    }

}
