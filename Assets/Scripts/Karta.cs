using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public class Karta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public manager manager;
    public GameObject PoziceVHracoveRuce;
    public GameObject HracovaRukaPolohaProKartu;
    public GameObject OdhazovaciBalicek;
    public GameObject LizaciBalicek;
    public GameObject PolohaVRuce;
    public GameObject OponentovaRuka;
    public GameObject RukavHrace;
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
    public bool LizaciBalicek_Rukav = false;
    public bool Rukav_Hrac = false;
    public bool Hrac_Rukav = false;
    public bool TohleJeKartaVRukavu = false;
    public bool KartaVRukavuAktivni = false;
    public bool a = true;
    public void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OdhazovaciBalicek = GameObject.Find("OdhozovaciBalicek");
        LizKaret = GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>();
        LizaciBalicek = GameObject.Find("LizaciBalicek");
        RukavHrace = GameObject.Find("KartaVRukavu");
        Obrazek();
    }
    public void Update()
    {
        if(manager.zacatekSazeni)
        {
            if(Hrac_OdhazovaciBalicek)//z ruky hráče do odhazovacího balíčku
            {            
                cas += Time.deltaTime*1.5f;
                this.transform.position = Vector3.Lerp(PoziceVHracoveRuce.transform.position, OdhazovaciBalicek.transform.position, cas);
                this.transform.localScale = Vector3.Lerp(PoziceVHracoveRuce.transform.localScale, OdhazovaciBalicek.transform.localScale, cas);
                if (cas > 1) {  Hrac_OdhazovaciBalicek = false; a = false; Kolo = true;  cas = 0; HracuvTah(); }
            }
            if (LizaciBalicek_Hrac && HracovaRukaPolohaProKartu != null)//Z lizacího balíčku do ruky hráče
            {
                cas += Time.deltaTime * 1.5f;
                this.transform.position = Vector3.Lerp(LizaciBalicek.transform.position, HracovaRukaPolohaProKartu.transform.position, cas);
                this.transform.localScale = Vector3.Lerp(LizaciBalicek.transform.localScale, HracovaRukaPolohaProKartu.transform.localScale, cas);
                if (cas > 1) { LizaciBalicek_Hrac = false; cas = 0; LizKaret.KonecAnimace();  }
            }
     
            if (Oponent_OdhazovaciBalicek)//z oponentovy ruky do odhazovazíco balíčku
            {
                cas += Time.deltaTime * 1.5f;
                this.transform.position = Vector3.Lerp(OponentovaRuka.transform.position, OdhazovaciBalicek.transform.position, cas);
                this.transform.localScale = Vector3.Lerp(OponentovaRuka.transform.localScale, OdhazovaciBalicek.transform.localScale, cas);
                a = true;
                if (cas > 1) 
                {
                    if (CisloKarty == 1 || CisloKarty == 7 || ZnackaKarty == "J") { LizKaret.EfektKarty = true; }
                    Oponent_OdhazovaciBalicek = false; cas = 0; GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
                }
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
                if (cas > 1) { LizaciBalicek_OdhazovaciBalicek = false; cas = 0; GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
                }
            }

            //Karta V rukavu
            if (LizaciBalicek_Rukav)//z lízacího balíčku do hracoveho rukavu
            {
                cas += Time.deltaTime * 1.5f;
                this.transform.position = Vector3.Lerp(LizaciBalicek.transform.position, RukavHrace.transform.position, cas);
                this.transform.localScale = Vector3.Lerp(LizaciBalicek.transform.localScale, RukavHrace.transform.localScale, cas);
                if (cas > 1) { LizaciBalicek_Rukav = false; cas = 0; a = true; }
            }
            if (Rukav_Hrac)//z rukavu do ruky hrace
            {
                cas += Time.deltaTime * 1.5f;
                this.transform.position = Vector3.Lerp(LizaciBalicek.transform.position, OponentovaRuka.transform.position, cas);
                this.transform.localScale = Vector3.Lerp(LizaciBalicek.transform.localScale, OponentovaRuka.transform.localScale, cas);
                if (cas > 1) { LizaciBalicek_Oponent = false; cas = 0; Destroy(gameObject); }
            }
            if (Hrac_Rukav)//z ruky do rukavu hrace
            {
                cas += Time.deltaTime * 1.5f;
                this.transform.position = Vector3.Lerp(PoziceVHracoveRuce.transform.position, RukavHrace.transform.position, cas);
                this.transform.localScale = Vector3.Lerp(PoziceVHracoveRuce.transform.localScale, RukavHrace.transform.localScale, cas);
                if (cas > 1) { LizaciBalicek_Oponent = false; cas = 0; Destroy(gameObject); }
            }

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
        if(LizKaret.KartaVRukavuAktivni && a && LizKaret.HracovoKolo && gameObject != TohleJeKartaVRukavu)
        {
            LizKaret.KartaDoRukavu(gameObject);
        }
        else if (LizKaret.HracovoKolo && !LizKaret.KartaVRukavuA && a && !LizKaret.KartaVRukavuAktivni)
        {
            if (ZnackaKarty == LizKaret.ZnackaOdhozenaKarta || CisloKarty == LizKaret.CisloOdhozenaKarta || ZnackaKarty == "J" || CisloKarty == 12 || LizKaret.ZnackaOdhozenaKarta == "E")
            {
                if (CisloKarty == 12 || ZnackaKarty == "J")
                {
                    if (!LizKaret.EfektKarty && LizaciBalicek_Hrac) { OdhozeniHracoviKarty(); }
                }
                else if (LizaciBalicek_Hrac) { OdhozeniHracoviKarty(); }
            }
        }          
        if (TohleJeKartaVRukavu && !KartaVRukavuAktivni)
        {
            this.GetComponent<Image>().color = new Color(255f, 100f, 0f,255f);
            LizKaret.KartaVRukavuAktivni = true; 
        }      
        else if (TohleJeKartaVRukavu && KartaVRukavuAktivni)
        {
            this.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            LizKaret.KartaVRukavuAktivni = false;
        }


    }
    public void OdhozeniHracoviKarty()
    {
        gameObject.transform.SetParent(OdhazovaciBalicek.transform, true);
        Instantiate(PoziceVHracoveRuce, gameObject.transform);
        PoziceVHracoveRuce.transform.position = this.transform.position;
        Hrac_OdhazovaciBalicek = true;
        LizKaret.HracovoKolo = false;
        LizKaret.balicekOdhozene.Add(GameObject.Find("HracovaRuka").GetComponent<HracRuka>().HracKarty[0]);
        GameObject.Find("HracovaRuka").GetComponent<HracRuka>().HracKarty.RemoveAt(0);
        if (!GameObject.Find("HracovaRuka").GetComponent<HracRuka>().HracKarty.Any())
        {
            manager.sazeciOkenko.SetActive(true);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(LizKaret.HracovoKolo && !TohleJeKartaVRukavu)
        {
            if(a)
            {
                gameObject.transform.position += new Vector3(0, 0.5f, 0);
            }
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(LizKaret.HracovoKolo && !TohleJeKartaVRukavu)
        {
            if (a)
            {
                gameObject.transform.position -= new Vector3(0, 0.5f, 0);
            }
        }
    }
    public void HracuvTah()
    {
        if (!GameObject.Find("HracovaRuka").GetComponent<HracRuka>().HracKarty.Any())
        {
            manager.sazeciOkenko.SetActive(true);
            StartCoroutine(manager.NoveKoloPrsi());
        }
        else
        {
            if (CisloKarty == 1) { LizKaret.EfektKarty = true; }
            if (CisloKarty == 7) { LizKaret.EfektKarty = true; LizKaret.pocetSedmicek++; }
            if (CisloKarty == 13 && ZnackaKarty == "♠") { LizKaret.EfektKarty = true; }
            if (CisloKarty == 12) { LizKaret.ZnackaVyberPopUp.SetActive(true); LizKaret.EfektKarty = true; }
            if (ZnackaKarty == "J") { LizKaret.ZnackaVyberPopUp.SetActive(true); LizKaret.EfektKarty = true; }
            else
            {
                LizKaret.CisloOdhozenaKarta = CisloKarty;
                LizKaret.ZnackaOdhozenaKarta = ZnackaKarty;
                /*if (LizKaret.KonecZacatekRozdavani == true) {*/ StartCoroutine(LizKaret.Kolo());// }
                GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
            }
            GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        }
    }
}
