using UnityEngine;
using UnityEngine.EventSystems;

public class Karta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.position += new Vector3(0, 0.5f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.position -= new Vector3(0, 0.5f, 0);
    }
}
