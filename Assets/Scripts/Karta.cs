using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Karta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    //public Image karta;
    public manager manager;
    public GameObject PoziceVHracoveRuce;
    public GameObject HracovaRukaPolohaProKartu;
    public GameObject OdhazovaciBalicek;
    public GameObject LizaciBalicek;
    public GameObject OponentovaRuka;
    public LizaniKaret LizKaret;
    public string kartaInfo;
    //public LizaniKaret lizaniKaret;
    public bool Kolo = false;
    private float cas = 0;
    public Image obrazek;    

    public bool Hrac_OdhazovaciBalicek = false;
    public bool LizaciBalicek_Hrac = false;
    public bool Oponent_OdhazovaciBalicek = false;
    public bool LizaciBalicek_Oponent = false;

    private bool a = true;
    private void Start()
    {
        //lizaniKaret = GameObject.Find("HracovaRuka").GetComponent<LizaniKaret>();
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OdhazovaciBalicek = GameObject.Find("OdhozovaciBalicek");
        LizKaret = GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>();
        LizaciBalicek = GameObject.Find("LizaciBalicek");
        gameObject.GetComponent<Image>().sprite = LizKaret.KartySrdce[1];
    }
    private void Update()
    {
        if(Hrac_OdhazovaciBalicek)//z ruky hráèe do odhazovacího balíèku
        {            
            cas += Time.deltaTime*1.2f;
            this.transform.position = Vector3.Lerp(PoziceVHracoveRuce.transform.position, OdhazovaciBalicek.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(PoziceVHracoveRuce.transform.localScale, OdhazovaciBalicek.transform.localScale, cas);
            if (cas > 1) { Hrac_OdhazovaciBalicek = false; a = false; Kolo = true;  cas = 0;}
        }
        if (LizaciBalicek_Hrac && HracovaRukaPolohaProKartu != null)//Z lizacího balíèku do ruky hráèe
        {
            cas += Time.deltaTime;
            this.transform.position = Vector3.Lerp(LizaciBalicek.transform.position, HracovaRukaPolohaProKartu.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(LizaciBalicek.transform.localScale, HracovaRukaPolohaProKartu.transform.localScale, cas);
            if (cas > 1) { LizaciBalicek_Hrac = false; cas = 0;        Debug.Log(kartaInfo);
 }
        }
        
        if (Oponent_OdhazovaciBalicek)//z oponentovy ruky do odhazovazíco balíèku
        {
            cas += Time.deltaTime;
            this.transform.position = Vector3.Lerp(OponentovaRuka.transform.position, OdhazovaciBalicek.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(OponentovaRuka.transform.localScale, OdhazovaciBalicek.transform.localScale, cas);
            if (cas > 1) { Oponent_OdhazovaciBalicek = false; cas = 0; }
        }

        if (LizaciBalicek_Oponent)//z lízacího balíèku do oponentovi ruky
        {
            cas += Time.deltaTime;
            this.transform.position = Vector3.Lerp(LizaciBalicek.transform.position, OponentovaRuka.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(LizaciBalicek.transform.localScale, OponentovaRuka.transform.localScale, cas);
            if (cas > 1) { LizaciBalicek_Oponent = false; cas = 0; gameObject.SetActive(false); }
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {     
        if(!LizaciBalicek_Hrac)
        {
            gameObject.transform.SetParent(OdhazovaciBalicek.transform,true);
            Instantiate(PoziceVHracoveRuce, gameObject.transform);
            PoziceVHracoveRuce.transform.position = this.transform.position;
            Hrac_OdhazovaciBalicek = true;
        }
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
