using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameManager gameManager;

    public Image imageComponent;

    public void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageComponent.color = Color.HSVToRGB(0f, 0f, 0.9f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageComponent.color = Color.HSVToRGB(0f, 0f, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameManager != null)
        {
            gameManager.SaveCurrentGame();
        }
        else
        {
            Debug.LogError("GameManager не найден на сцене!", this);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        imageComponent.color = Color.HSVToRGB(0f, 0f, 0.7f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imageComponent.color = Color.HSVToRGB(0f, 0f, 0.9f);
    }
}
