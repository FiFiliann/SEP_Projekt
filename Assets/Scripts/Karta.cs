using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Karta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    //public Image karta;
    public manager manager;
    public GameObject start;
    public GameObject konec;
    public bool Kolo = false;
    private float cas = 0;
    public string znacka = "7X";
    private bool packa = false;
    private bool a = true;
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();

        konec = GameObject.Find("OdhozeneKarty");
    }
    private void Update()
    {
        if(packa)
        {            
            cas += Time.deltaTime;
            this.transform.position = Vector3.Lerp(start.transform.position,  konec.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(start.transform.localScale, konec.transform.localScale, cas);
            if (cas > 1) { packa = false; a = false; Kolo = true; }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        gameObject.transform.SetParent(GameObject.Find("Stul").transform,true);
        GameObject startovniBod = Instantiate(start, gameObject.transform);
        startovniBod.transform.position = this.transform.position;
        start = startovniBod;
        packa = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(a)
        {
            gameObject.transform.position += new Vector3(0, 0.5f, 0);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (a)
        {
            gameObject.transform.position -= new Vector3(0, 0.5f, 0);
        }
    }
}
