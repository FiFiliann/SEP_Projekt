using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Karta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    //public Image karta;
    public GameObject start, konec;
    private bool zacatek;
    private float cas;
    private bool packa;
    private void Start()
    {
        GameObject startovniBod = Instantiate(start, gameObject.transform);
        startovniBod.transform.position = this.transform.position;       
    }
    private void Update()
    {
        if(packa)
        {
            zacatek = true;
            cas = 0;
        }
        if (zacatek)
        {
            cas += Time.deltaTime;
            this.transform.position = Vector3.Lerp(start.transform.position,  konec.transform.position, cas);
            if (cas > 160) { zacatek = false; }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        packa = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.position += new Vector3(0, 0.5f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.position -= new Vector3(0, 0.5f, 0);
    }
}
