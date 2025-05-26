using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.InputSystem.Android;
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
    public vysledekPopUp vysPopUp;

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
    public bool a = false;
    public void Start()
    {
        vysPopUp = GameObject.Find("Vysledek").GetComponent<vysledekPopUp>();
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
            if (LizaciBalicek_Hrac)//Z lizacího balíčku do ruky hráče
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
                    if(LizKaret.CisloOdhozenaKarta != 14) {GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite; }
                    Oponent_OdhazovaciBalicek = false; cas = 0; a = false; 
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
                if (cas > 1) { LizaciBalicek_OdhazovaciBalicek = false; a = false; cas = 0; GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
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
                this.transform.position = Vector3.Lerp(RukavHrace.transform.position, PoziceVHracoveRuce.transform.position, cas);
                this.transform.localScale = Vector3.Lerp(RukavHrace.transform.localScale, PoziceVHracoveRuce.transform.localScale, cas);
                if (cas > 1) { Rukav_Hrac = false; cas = 0; LizKaret.PodvodRukavKonec(PoziceVHracoveRuce); }
            }
            if (Hrac_Rukav)//z ruky do rukavu hrace
            {
                cas += Time.deltaTime * 1.5f;
                this.transform.position = Vector3.Lerp(PoziceVHracoveRuce.transform.position, RukavHrace.transform.position, cas);
                this.transform.localScale = Vector3.Lerp(PoziceVHracoveRuce.transform.localScale, RukavHrace.transform.localScale, cas);
                if (cas > 1) { Hrac_Rukav = false; cas = 0;  }
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
        if(LizKaret.HracovoKolo)
        {
            if(!TohleJeKartaVRukavu && LizKaret.KartaVRukavuAktivni ) // ProhozeniSKartvouVRukavu
            {
                LizKaret.PodvodRukav(gameObject);
            }
            else if (!TohleJeKartaVRukavu && !LizKaret.KartaVRukavuAktivni ) // OdhozeniKarty
            {
                if (LizKaret.CisloOdhozenaKarta == 7 && LizKaret.EfektKarty && CisloKarty == 7)                                             { OdhozeniHracoviKarty(); }
                else if (LizKaret.CisloOdhozenaKarta == 14 && LizKaret.EfektKarty && CisloKarty == 14)                                      { OdhozeniHracoviKarty(); }
                else if (CisloKarty == 12  && !LizKaret.EfektKarty)                                                                         { OdhozeniHracoviKarty(); }
                else if (ZnackaKarty == "J" && !LizKaret.EfektKarty)                                                                        { OdhozeniHracoviKarty(); }
                else if (LizKaret.CisloOdhozenaKarta == 1 && LizKaret.EfektKarty && CisloKarty == 1)                                        { OdhozeniHracoviKarty(); }
                else if (ZnackaKarty == LizKaret.ZnackaOdhozenaKarta && !LizKaret.EfektKarty)                                               { OdhozeniHracoviKarty(); }
                else if (CisloKarty == LizKaret.CisloOdhozenaKarta && !LizKaret.EfektKarty)                                                 { OdhozeniHracoviKarty(); }
                else if (LizKaret.ZnackaOdhozenaKarta == "E")                                                                               { OdhozeniHracoviKarty(); }
            }          
            //KartaVRukavu
            if (TohleJeKartaVRukavu && !LizKaret.KartaVRukavuAktivni)
            {
                this.GetComponent<Image>().color = new Color(255f, 100f, 0f,255f);
                LizKaret.KartaVRukavuAktivni = true; 
            }      
            else if (TohleJeKartaVRukavu && LizKaret.KartaVRukavuAktivni )
            {
                this.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
                LizKaret.KartaVRukavuAktivni = false;
            }
        }

    }
    public void OdhozeniHracoviKarty()
    {
        gameObject.transform.SetParent(OdhazovaciBalicek.transform, true);
        //Instantiate(PoziceVHracoveRuce, gameObject.transform);
        //PoziceVHracoveRuce.transform.position = this.transform.position;
        Hrac_OdhazovaciBalicek = true;
        LizKaret.HracovoKolo = false;
        LizKaret.balicekOdhozene.Add(GameObject.Find("HracovaRuka").GetComponent<HracRuka>().HracKarty[0]);
        GameObject.Find("HracovaRuka").GetComponent<HracRuka>().HracKarty.RemoveAt(0);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!TohleJeKartaVRukavu)
        {
            if(a)
            {
                gameObject.transform.position += new Vector3(0, 0.5f, 0);
            }
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!TohleJeKartaVRukavu)
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
            StartCoroutine(vysPopUp.Vysledky(0,0));
        }
        else
        {
            if (CisloKarty == 1) { LizKaret.EfektKarty = true; LizKaret.DialogHrac("A STOP!"); }
            if (CisloKarty == 7) { LizKaret.EfektKarty = true; LizKaret.pocetSedmicek++; LizKaret.DialogHrac("SEDMA KAMARADE!"); }
            if (CisloKarty == 13 && ZnackaKarty == "♠") { LizKaret.EfektKarty = true; LizKaret.DialogHrac("LIZEJ, KAMARADE, LIZEJ!"); }
            if (ZnackaKarty == "J" || CisloKarty == 12) 
            { 
                LizKaret.ZnackaVyberPopUp.SetActive(true); 
                LizKaret.EfektKarty = true; 
                LizKaret.DialogHrac("POJDME TO TROCHU ZMENIT, CO VY NA TO!"); 
                if (CisloKarty == 12) { LizKaret.EfektKarty = false; } }
            else
            {
                LizKaret.CisloOdhozenaKarta = CisloKarty;
                LizKaret.ZnackaOdhozenaKarta = ZnackaKarty;
                StartCoroutine(LizKaret.Kolo());
                GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
            }
            GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        }
    }
}
