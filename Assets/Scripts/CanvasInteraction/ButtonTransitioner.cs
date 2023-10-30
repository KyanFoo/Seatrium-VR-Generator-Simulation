
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    public Color32 normalColor = Color.white;
    public Color32 hoverColor = Color.grey;
    public Color32 downColor = Color.white;

    private Image Image = null;

    private void Awake()
    {
        Image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter");

        Image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");

        Image.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("Down");

        Image.color = downColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("Up");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("Click");

        Image.color = hoverColor;
    }
}
