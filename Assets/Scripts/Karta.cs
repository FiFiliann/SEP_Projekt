using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Karta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    //public Image karta;
    public manager manager;
    public GameObject HracovaRuka;
    public GameObject OdhazovaciBalicek;
    public GameObject LizaciBalicek;
    public LizaniKaret lizaniKaret;
    public bool Kolo = false;
    private float cas = 0;
    public string znacka = "7X";
    private bool packa = false;
    private bool packb = false;

    private bool a = true;
    private void Start()
    {
        lizaniKaret = GameObject.Find("HracovaRuka").GetComponent<LizaniKaret>();
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OdhazovaciBalicek = GameObject.Find("OdhozeneKarty");
        LizaciBalicek = GameObject.Find("LizaciBalicek");
    }
    private void Update()
    {
        if(packa)
        {            
            cas += Time.deltaTime;
            this.transform.position = Vector3.Lerp(HracovaRuka.transform.position, OdhazovaciBalicek.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(HracovaRuka.transform.localScale, OdhazovaciBalicek.transform.localScale, cas);
            if (cas > 1) { packa = false; a = false; Kolo = true;  cas = 0; }
        }
        if(packb)
        {
            cas += Time.deltaTime;
            this.transform.position = Vector3.Lerp(GameObject.Find("LizaciBalicek").transform.position, HracovaRuka.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(GameObject.Find("LizaciBalicek").transform.localScale, HracovaRuka.transform.localScale, cas);
            if (cas > 1) { packa = false; cas = 0; }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {     
        gameObject.transform.SetParent(GameObject.Find("Stul").transform,true);
        Instantiate(HracovaRuka, gameObject.transform);
        HracovaRuka.transform.position = this.transform.position;
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
    /*
    public void LizaciBalicekDoRukyHrace()
    {
        Instantiate(HracovaRuka, gameObject.transform);
        HracovaRuka.transform.position = GameObject.Find("HracovaRuka").transform.position; 
        gameObject.transform.position = GameObject.Find("LizaciBalicek").transform.position;
        packb = true;
    }*/
}
