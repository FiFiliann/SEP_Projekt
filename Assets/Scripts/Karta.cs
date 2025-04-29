using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Karta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public manager manager;
    public GameObject PoziceVHracoveRuce;
    public GameObject HracovaRukaPolohaProKartu;
    public GameObject OdhazovaciBalicek;
    public GameObject LizaciBalicek;
    public GameObject OponentovaRuka;
    public LizaniKaret LizKaret;

    public string ZnackaKarty;
    public int CisloKarty;

    public bool Kolo = false;
    private float cas = 0;

    public bool Hrac_OdhazovaciBalicek = false;
    public bool LizaciBalicek_Hrac = false;
    public bool Oponent_OdhazovaciBalicek = false;
    public bool LizaciBalicek_Oponent = false;
    public bool LizaciBalicek_OdhazovaciBalicek = false;
    private bool Odhozena = false;
    public bool a = true;
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OdhazovaciBalicek = GameObject.Find("OdhozovaciBalicek");
        LizKaret = GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>();
        LizaciBalicek = GameObject.Find("LizaciBalicek");
        Obrazek();
    }
    private void Update()
    {
        if(Hrac_OdhazovaciBalicek)//z ruky hráče do odhazovacího balíčku
        {            
            cas += Time.deltaTime*1.5f;
            this.transform.position = Vector3.Lerp(PoziceVHracoveRuce.transform.position, OdhazovaciBalicek.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(PoziceVHracoveRuce.transform.localScale, OdhazovaciBalicek.transform.localScale, cas);
            if (cas > 1) { Hrac_OdhazovaciBalicek = false; a = false; Kolo = true;LizKaret.posledniKartaOdhazovaciBalicek = ZnackaKarty + CisloKarty; Odhozena = true; cas = 0;}
        }
        if (LizaciBalicek_Hrac && HracovaRukaPolohaProKartu != null)//Z lizacího balíčku do ruky hráče
        {
            cas += Time.deltaTime * 1.5f;
            this.transform.position = Vector3.Lerp(LizaciBalicek.transform.position, HracovaRukaPolohaProKartu.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(LizaciBalicek.transform.localScale, HracovaRukaPolohaProKartu.transform.localScale, cas);
            if (cas > 1) { LizaciBalicek_Hrac = false; cas = 0;        Debug.Log(ZnackaKarty + "" + CisloKarty);}
        }
     
        if (Oponent_OdhazovaciBalicek)//z oponentovy ruky do odhazovazíco balíčku
        {
            cas += Time.deltaTime * 1.5f;
            this.transform.position = Vector3.Lerp(OponentovaRuka.transform.position, OdhazovaciBalicek.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(OponentovaRuka.transform.localScale, OdhazovaciBalicek.transform.localScale, cas);
            if (cas > 1) { Oponent_OdhazovaciBalicek = false; cas = 0; Odhozena = true; }
        }

        if (LizaciBalicek_Oponent)//z lízacího balíčku do oponentovi ruky
        {
            cas += Time.deltaTime * 1.5f;
            this.transform.position = Vector3.Lerp(LizaciBalicek.transform.position, OponentovaRuka.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(LizaciBalicek.transform.localScale, OponentovaRuka.transform.localScale, cas);
            if (cas > 1) { LizaciBalicek_Oponent = false; cas = 0; Destroy(gameObject); }
        }

        if (LizaciBalicek_OdhazovaciBalicek)//z oponentovy ruky do odhazovazíco balíčku
        {
            cas += Time.deltaTime * 1.5f;
            this.transform.position = Vector3.Lerp(LizaciBalicek.transform.position, OdhazovaciBalicek.transform.position, cas);
            this.transform.localScale = Vector3.Lerp(LizaciBalicek.transform.localScale, OdhazovaciBalicek.transform.localScale, cas);
            if (cas > 1) { LizaciBalicek_OdhazovaciBalicek = false; cas = 0; Odhozena = true; }
        }
    }

    public void Obrazek()
    {
        switch(ZnackaKarty)
        {
            case "♣": gameObject.GetComponent<Image>().sprite = LizKaret.KartyKrize[CisloKarty-1]; break;
            case "♦": gameObject.GetComponent<Image>().sprite = LizKaret.KartyKary[CisloKarty-1]; break;
            case "♥": gameObject.GetComponent<Image>().sprite = LizKaret.KartySrdce[CisloKarty - 1]; break;
            case "♠": gameObject.GetComponent<Image>().sprite = LizKaret.KartyPiky[CisloKarty- 1]; break;
            case "J": gameObject.GetComponent<Image>().sprite = LizKaret.KartySpecialni[CisloKarty - 1];break;
            default:break;             
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {    
        if(!Odhozena)
        {
            if(ZnackaKarty == LizKaret.ZnackaOdhozenaKarta || CisloKarty == LizKaret.CisloOdhozenaKarta || ZnackaKarty == "J" || CisloKarty == 12 )
            {
                if(LizaciBalicek_Hrac)
                {
                    gameObject.transform.SetParent(OdhazovaciBalicek.transform,true);
                    Instantiate(PoziceVHracoveRuce, gameObject.transform);
                    PoziceVHracoveRuce.transform.position = this.transform.position;
                    Hrac_OdhazovaciBalicek = true;
                }
            }
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!Odhozena)
        {
            if(a)
            {
                gameObject.transform.position += new Vector3(0, 0.5f, 0);
            }
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!Odhozena)
        {
            if (a)
            {
                gameObject.transform.position -= new Vector3(0, 0.5f, 0);
            }
        }
    }
}
