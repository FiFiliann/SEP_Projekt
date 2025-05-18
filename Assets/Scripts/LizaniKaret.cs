using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;
using static System.Net.Mime.MediaTypeNames;



public class LizaniKaret : MonoBehaviour
{
    public List<string> balicek = new List<string>();
    public List<string> balicekOdhozene = new List<string>();
    public GameObject KartaGo;
    public GameObject KartaNeviditelna;
    public GameObject a;
    GameObject coze;
    public HracRuka hracRuka;
    public OponentUStolu oponentUStolu;
    public manager manager;
    public vysledekPopUp vysPopUp;
    public Sprite[] KartySrdce = new Sprite[13];
    public Sprite[] KartyKary = new Sprite[13];
    public Sprite[] KartyPiky = new Sprite[13];
    public Sprite[] KartyKrize = new Sprite[13];
    public Sprite[] KartySpecialni = new Sprite[2];
    public GameObject ZnackaVyberPopUp;
    public string ZnackaOdhozenaKarta;
    public int CisloOdhozenaKarta;
    public int pocetSedmicek = 0;
    public bool EfektKarty = false;
    public bool HracovoKolo = true;
    public bool KonecZacatekRozdavani = false;

    //KARTA V RUKAVU
    public bool KartaVRukavuAktivni = false;
    public GameObject KartaVRukavuA;
    public GameObject KartaVRukavuB;
    public int hodnotaZvetseniPodezreni = 5;
    //DIALOG
    public GameObject KecaniButton;
    public GameObject DialogPrefab;
    public GameObject DialogPoloha;
    public Slider CekaniSlider;
    public bool KecaniSpustene;
    //Podezření
    public Slider PodezreniSlider;
    public float PodezreniValue = 0f;
    public int pocetOponentu = 0;
    public int MaxPokusyPodvadeni = 3; // Maximální počet pokusů
    public int aktualniPokusyPodvadeni = 0; // Počet využitých pokusů

    public void Start()
    {
        vysPopUp = GameObject.Find("Vysledek").GetComponent<vysledekPopUp>();
        hracRuka = GameObject.Find("HracovaRuka").GetComponent<HracRuka>();
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        PodezreniSlider.maxValue = 10;
        PodezreniSlider.interactable = false;
        CekaniSlider.maxValue = 400;
    }
    // BALICEK
    public void PripravaBalicku()
    {
        balicek.Clear();
            balicek.Add("♣1");
            balicek.Add("♣2");
            balicek.Add("♣3");
            balicek.Add("♣4");
            balicek.Add("♣5");
            balicek.Add("♣6");
            balicek.Add("♣7");
            balicek.Add("♣8");
            balicek.Add("♣9");
            balicek.Add("♣10");
            balicek.Add("♣11");
            balicek.Add("♣12");
            balicek.Add("♣13");

            balicek.Add("♦1");
            balicek.Add("♦2");
            balicek.Add("♦3");
            balicek.Add("♦4");
            balicek.Add("♦5");
            balicek.Add("♦6");
            balicek.Add("♦7");
            balicek.Add("♦8");
            balicek.Add("♦9");
            balicek.Add("♦10");
            balicek.Add("♦11");
            balicek.Add("♦12");
            balicek.Add("♦13");

            balicek.Add("♥1");
            balicek.Add("♥2");
            balicek.Add("♥3");
            balicek.Add("♥4");
            balicek.Add("♥5");
            balicek.Add("♥6");
            balicek.Add("♥7");
            balicek.Add("♥8");
            balicek.Add("♥9");
            balicek.Add("♥10");
            balicek.Add("♥11");
            balicek.Add("♥12");
            balicek.Add("♥13");

            balicek.Add("♠1");
            balicek.Add("♠2");
            balicek.Add("♠3");
            balicek.Add("♠4");
            balicek.Add("♠5");
            balicek.Add("♠6");
            balicek.Add("♠7");
            balicek.Add("♠8");
            balicek.Add("♠9");
            balicek.Add("♠10");
            balicek.Add("♠11");
            balicek.Add("♠12");
            balicek.Add("♠13");

            balicek.Add("J1");
            balicek.Add("J2");

            RozmichaniKaret();    
        for(int i = 0; i < manager.OponentiUStolu.Length; i++)
        {
            if (manager.OponentiUStolu[i] != null)
            {
                manager.OponentiUStolu[i].GetComponent<OponentUStolu>().OponentKarty.Clear();
            }
        }
    }
    public void RozmichaniKaret()
    {
        for (int i = 0; i < 50; i++)
        {
            int vyberJedna = UnityEngine.Random.Range(0, balicek.Count);
            int vyberDva = UnityEngine.Random.Range(0, balicek.Count);
            string podrz = balicek[vyberJedna];
            balicek[vyberJedna] = balicek[vyberDva];
            balicek[vyberDva] = podrz;
        }
    }
    public void ResetRuk()
    {
        GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = KartySpecialni[2]; //VYNULOVÝNÍ ZVETSENE KARTY
        for (int i = 0;i<manager.OponentiUStolu.Length;i++) //VYMAZANI RUK OPONENTU
        {
            if(manager.OponentiUStolu[i] != null)
            {
                manager.OponentiUStolu[i].GetComponent<OponentUStolu>().OponentKarty.Clear();
            }
        }
        while (GameObject.Find("OdhozovaciBalicek").transform.childCount > 0) // VYMAZANI ODHAZOVACIHO BALICKU
        {
            DestroyImmediate(GameObject.Find("OdhozovaciBalicek").transform.GetChild(0).gameObject);
        }    
        
        hracRuka.HracKarty.Clear(); // VYMAZANI OBSAHU RUKY
        while (GameObject.Find("HracovaRuka").transform.childCount > 0) //  VYMAZANI OBJEKTU V RUCE HRACE
        {
            DestroyImmediate(GameObject.Find("HracovaRuka").transform.GetChild(0).gameObject);
        }
    }
    public void DoplneniBalicku()
    {
        Debug.Log("Start");
        while (balicekOdhozene.Count > 1) //  VYMAZANI OBJEKTU V RUCE HRACE
        {
            balicek.Add(balicekOdhozene[0]);
            balicekOdhozene.RemoveAt(0);
            Debug.Log("dalsi");

        }
        while (GameObject.Find("OdhozovaciBalicek").transform.childCount > 1) //  VYMAZANI ODHAZOVACIHO BALICKU
        {
            DestroyImmediate(GameObject.Find("OdhozovaciBalicek").transform.GetChild(0).gameObject);
        }
    }
    //Karty
    public void KonecAnimace()
    {
            transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + "";
            coze.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            Destroy(a);
    }
    public void KartaProHrace()
    {
        if (HracovoKolo)
        {               

            HracovoKolo = false;        
            a = Instantiate(KartaGo, GameObject.Find("LizaciBalicek").transform);
            coze = Instantiate(KartaGo, GameObject.Find("HracovaRuka").transform);
            coze.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
            PrideleniKarty(a);
            PrideleniKarty(coze);           
            coze.GetComponent<Karta>().LizaciBalicek_Hrac = false; 
            hracRuka.HracKarty.Add(balicek[0]);
            balicek.RemoveAt(0);
            if (!balicek.Any()) {DoplneniBalicku(); }
        }
    }

    public void PrideleniKarty(GameObject i)
    {
        i.GetComponent<Karta>().LizaciBalicek_Hrac = true;
        i.GetComponent<Karta>().HracovaRukaPolohaProKartu = coze;
        i.GetComponent<Karta>().ZnackaKarty = balicek[0].Substring(0, 1);
        i.GetComponent<Karta>().CisloKarty = int.Parse(balicek[0].Substring(1, balicek[0].Length - 1));
    }
    //

    //
    public IEnumerator Kolo()
    {
        for (int j = 0; j < manager.OponentiUStolu.Length; j++)
        {
            if (manager.OponentiUStolu[j] != null && manager.OponentiUStolu[j].GetComponent<OponentUStolu>().Hraje == true)
            {
                manager.OponentiUStolu[j].transform.position += new Vector3(0, 0.3f, 0); yield return new WaitForSeconds(0.5f);
                StartCoroutine(manager.OponentiUStolu[j].GetComponent<OponentUStolu>().KontrolaProOdhozeniOponent());
                manager.OponentiUStolu[j].transform.position -= new Vector3(0, 0.3f, 0); yield return new WaitForSeconds(0.5f);
                
                if (!manager.OponentiUStolu[j].GetComponent<OponentUStolu>().OponentKarty.Any()) 
                { 
                    StartCoroutine(vysPopUp.Vysledky(1,j));

                    j = manager.OponentiUStolu.Length;
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        HracovoKolo = true;
    }

    public IEnumerator OponentVyhra(int j)
    {
        manager.sazeciOkenko.SetActive(true);
        for (int a = 0; a < manager.OponentiUStolu.Length; a++) // rozdeleni Penez
        {
            if (manager.OponentiUStolu[a] != null && manager.OponentiUStolu[a].GetComponent<OponentUStolu>().Hraje == true && j != a)
                { manager.OponentiUStolu[a].GetComponent<OponentUStolu>().OdecteniPenezOponentovy(); }
            else if(manager.OponentiUStolu[a] != null && manager.OponentiUStolu[a].GetComponent<OponentUStolu>().Hraje == true && j == a)
                { manager.OponentiUStolu[a].GetComponent<OponentUStolu>().PrideleniPenezOponentovy(); }
            yield return new WaitForSeconds(0.2f);
        }
        manager.penize -= manager.nejvyssiSazka;
        GameObject.Find("HracovaSazka").GetComponent<TextMeshProUGUI>().text = manager.penize + "KC";
                    
        StartCoroutine(manager.NoveKoloPrsi());
    }
    public IEnumerator StartKolo()
    {
        PripravaBalicku();
        ResetRuk();
        KonecZacatekRozdavani = false;
        CekaniSlider.value = CekaniSlider.maxValue;
        if (manager.Kecanikoupeno) { KecaniButton.SetActive(true); }
        for (int i = 0;i < 4;i++) // počet karet
        {
            for (int j = 0; j < manager.OponentiUStolu.Length; j++) // počet hrajících oponentů
            {
                if (manager.OponentiUStolu[j] != null && manager.OponentiUStolu[j].GetComponent<OponentUStolu>().Hraje == true)
                {
                    StartCoroutine(manager.OponentiUStolu[j].GetComponent<OponentUStolu>().LiznutiKartyOponent());//*
                    transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + "";
                    yield return new WaitForSeconds(0.5f);
                }
            }           
            HracovoKolo = true;           
            KartaProHrace();
            transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + "";
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(StartKartaOdhozeni());
        transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + "";
        yield return new WaitForSeconds(0.5f);
        if (manager.KartaVRukavuKoupeno && GameObject.Find("KartaVRukavu").transform.childCount == 0) 
        { 
            KartaVRukavu(); 
            transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + ""; 
        }
        HracovoKolo = true;

    }
    //
    public IEnumerator StartKartaOdhozeni() // První odhozená karta
    {
        GameObject j = Instantiate(KartaGo, GameObject.Find("OdhozovaciBalicek").transform);
        PrideleniKarty(j);
        j.GetComponent<Karta>().LizaciBalicek_Hrac = false;
        j.GetComponent<Karta>().a = false;
        j.GetComponent<Karta>().LizaciBalicek_OdhazovaciBalicek = true;
        ZnackaOdhozenaKarta = balicek[0].Substring(0, 1);
        CisloOdhozenaKarta = int.Parse(balicek[0].Substring(1, balicek[0].Length-1));
        balicekOdhozene.Add(balicek[0]);
        balicek.RemoveAt(0);
        transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + "";

        if (CisloOdhozenaKarta == 7) { EfektKarty = true; pocetSedmicek++; }
        if (CisloOdhozenaKarta == 1) { EfektKarty = true;}
        if (ZnackaOdhozenaKarta == "J") { EfektKarty = true;}
        if (ZnackaOdhozenaKarta == "J" || CisloOdhozenaKarta == 12) { ZnackaOdhozenaKarta = "E"; }
        if (CisloOdhozenaKarta == 13 && ZnackaOdhozenaKarta == "♠") { EfektKarty = true; }

        yield return new WaitForSeconds(0.5f);
    }

    //KARTA V RUKAVU
    public void KartaVRukavu()
    {
        GameObject kartadoRukavu = Instantiate(KartaGo, GameObject.Find("KartaVRukavu").transform);
        PrideleniKarty(kartadoRukavu);
        kartadoRukavu.transform.position = GameObject.Find("LizaciBalicek").transform.position;
        kartadoRukavu.GetComponent<Karta>().LizaciBalicek_Hrac = false;
        kartadoRukavu.GetComponent<Karta>().LizaciBalicek_Rukav = true;
        kartadoRukavu.GetComponent<Karta>().TohleJeKartaVRukavu = true;
        KartaVRukavuA = kartadoRukavu;
        //balicekOdhozene.Add(balicek[0]);
        balicek.RemoveAt(0);
    }
    public void PodvodRukav(GameObject i)
    {
        GameObject j = Instantiate(KartaGo, GameObject.Find("KartaVRukavu").transform);
        KartaVRukavuB = KartaVRukavuA;
        KartaVRukavuA = j;
        KartaZRukavu(i);
        i.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        j.transform.position = i.transform.position;
        j.GetComponent<Karta>().PoziceVHracoveRuce = i;
        j.GetComponent<Karta>().ZnackaKarty = i.GetComponent<Karta>().ZnackaKarty;
        j.GetComponent<Karta>().CisloKarty = i.GetComponent<Karta>().CisloKarty;
        j.GetComponent<Karta>().Hrac_Rukav = true;
        j.GetComponent<Karta>().TohleJeKartaVRukavu = true;
        KartaVRukavuAktivni = false;

    }
    public void KartaZRukavu(GameObject i)
    {
        KartaVRukavuB.GetComponent<Karta>().Rukav_Hrac = true;
        KartaVRukavuB.GetComponent<Karta>().PoziceVHracoveRuce = i;
    }
    public void PodvodRukavKonec(GameObject i) //sprite nahrazen a alpha obnovena
    {
        i.GetComponent<Image>().color = new Color(255f, 255, 255f, 255f);
        i.GetComponent<Karta>().ZnackaKarty = KartaVRukavuB.GetComponent<Karta>().ZnackaKarty;
        i.GetComponent<Karta>().CisloKarty = KartaVRukavuB.GetComponent<Karta>().CisloKarty;
        i.GetComponent<Karta>().Obrazek();
        Destroy(KartaVRukavuB);

        int randomPodezreni = UnityEngine.Random.Range(1, hodnotaZvetseniPodezreni);
        PodezreniSlider.value += randomPodezreni;
        if(PodezreniSlider.value == PodezreniSlider.maxValue)
        {
            
            StartCoroutine(vysPopUp.Vysledky(2,0)) ;
            
        }
    }
    public void RukavChycen()
    {
            manager.ZmenaSceny(0);
            manager.NovyDen();

            manager.penize -= manager.nejvyssiSazka;
            manager.reputace -= 5;
            if(manager.reputace < 10) { manager.reputace = 10; }

            manager.BytMenuPromene();
    }
    // DIALOG
    public void DialogButton()
    {
        if(CekaniSlider.value == CekaniSlider.maxValue) 
        {
            CekaniSlider.value = CekaniSlider.minValue;
            StartCoroutine(DoplnovaniCekani());
            KecaniSpustene = true;
            GameObject HracDialog = Instantiate(DialogPrefab,DialogPoloha.transform);
            GameObject.Find("KecaniButton").GetComponent<Button>().interactable = false;
            HracDialog.GetComponent<Dialog>().HracDialog.SetActive(true);
            HracDialog.GetComponent<Dialog>().OponentDialog.SetActive(false);
            HracDialog.GetComponent<Dialog>().TohleJe = "Hrac";
            HracDialog.GetComponent<Dialog>().TextProHrace();
        }
    }
    public void DialogHrac(string text)
    {
        GameObject HracDialog = Instantiate(DialogPrefab, DialogPoloha.transform);
        HracDialog.GetComponent<Dialog>().OponentDialog.SetActive(false);
        HracDialog.GetComponent<Dialog>().HracDialog.SetActive(true);
        HracDialog.GetComponent<Dialog>().TohleJe = "HracKteryNechceMluvit";
        HracDialog.GetComponent<Dialog>().HracDialogText.text = text;
    }
    public IEnumerator DoplnovaniCekani()
    {        
        while(CekaniSlider.value != CekaniSlider.maxValue)
        {
            CekaniSlider.value += 1;
            yield return new WaitForSeconds(0.1f);
        }
        GameObject.Find("KecaniButton").GetComponent<Button>().interactable = true;
    }
    //Specialni Karty
    public void ZnackaVyber(int vyber)
    {
        switch (vyber)
        {
            case 0: ZnackaOdhozenaKarta = "♦"; CisloOdhozenaKarta = 14; GameObject.Find("OdhozenaKartaZvetseni").GetComponent<UnityEngine.UI.Image>().sprite = KartyKary[13]; break;
            case 1: ZnackaOdhozenaKarta = "♣"; CisloOdhozenaKarta = 14; GameObject.Find("OdhozenaKartaZvetseni").GetComponent<UnityEngine.UI.Image>().sprite = KartyKrize[13]; break;
            case 2: ZnackaOdhozenaKarta = "♥"; CisloOdhozenaKarta = 14; GameObject.Find("OdhozenaKartaZvetseni").GetComponent<UnityEngine.UI.Image>().sprite = KartySrdce[13]; break;
            case 3: ZnackaOdhozenaKarta = "♠"; CisloOdhozenaKarta = 14; GameObject.Find("OdhozenaKartaZvetseni").GetComponent<UnityEngine.UI.Image>().sprite = KartyPiky[13]; break;
            default: break;
        }
        ZnackaVyberPopUp.SetActive(false);
        StartCoroutine(Kolo());
    }
    public void HracLizani()
    {
        StartCoroutine(EfektyKaretNaHrace());
    }
   public void VynechatKoloButton()
    {
        GameObject.Find("Vynechat").GetComponent<Button>().interactable = false;
        GameObject.Find("Odejit").GetComponent<Button>().interactable = false;
        GameObject.Find("PotvrditSazku").GetComponent<Button>().interactable = false;
        StartCoroutine(VynechatKolo());
        
    }
    public IEnumerator VynechatKolo()
    {
        List<int> hraciOponenti = new List<int>();
        for (int a = 0; a < manager.OponentiUStolu.Length; a++) // rozdeleni Penez
        {
            if (manager.OponentiUStolu[a] != null && manager.OponentiUStolu[a].GetComponent<OponentUStolu>().Hraje == true)
            { hraciOponenti.Add(manager.OponentiUStolu[a].GetComponent<OponentUStolu>().CisloOponenta); }
        }
        int j = UnityEngine.Random.Range(0, hraciOponenti.Count);
        Debug.Log(j);
        for (int a = 0; a < manager.OponentiUStolu.Length; a++) // rozdeleni Penez
        {
            if (manager.OponentiUStolu[a] != null && manager.OponentiUStolu[a].GetComponent<OponentUStolu>().Hraje == true && a != hraciOponenti[j])
            { manager.OponentiUStolu[a].GetComponent<OponentUStolu>().OdecteniPenezOponentovy(); }
            else if (manager.OponentiUStolu[a] != null && manager.OponentiUStolu[a].GetComponent<OponentUStolu>().Hraje == true && j == hraciOponenti[j])
            { manager.OponentiUStolu[a].GetComponent<OponentUStolu>().PrideleniPenezOponentovy(); }
            yield return new WaitForSeconds(0.2f);
        }
        StartCoroutine(manager.NoveKoloPrsi());


    }
    public IEnumerator EfektyKaretNaHrace()
    {      
        int pocetLiznutychKaret;
        Debug.Log("Efeck Karty");

        // Odhozená karta je PIKOVÝ KRÁL
        if (CisloOdhozenaKarta == 13 && ZnackaOdhozenaKarta == "♠" && EfektKarty)
        {
                pocetLiznutychKaret = 5;
                EfektKarty = false;
        }

        // Odhozená karta je SEDMA
        else if (CisloOdhozenaKarta == 7 && EfektKarty)
        {
            pocetLiznutychKaret = (pocetSedmicek * 3);
            EfektKarty = false;
            pocetSedmicek = 0;
        }

        // Odhozená karta je ESO
        else if (CisloOdhozenaKarta == 1 && EfektKarty)
        {
            pocetLiznutychKaret = 0;
            EfektKarty = false;
        }
        // Odhozená karta je ŽOLÍK
        else if (CisloOdhozenaKarta == 14 && EfektKarty)
        {
            Debug.Log("Žolda");
            pocetLiznutychKaret = 3;
            EfektKarty = false;

        }
        else{pocetLiznutychKaret = 1;}

        if (pocetLiznutychKaret != 0)
        {
            for(int i = 0; i < pocetLiznutychKaret; i++)
            {
                KartaProHrace();
                HracovoKolo = true;
                yield return new WaitForSeconds(1f);               
            }
        }
        HracovoKolo = false;
        StartCoroutine(Kolo());
    }
    public void HracVyhra()
    {
        manager.sazeciOkenko.SetActive(true);
        manager.penize += manager.secteni - manager.hracSazka;
        GameObject.Find("HracovaSazka").GetComponent<TextMeshProUGUI>().text = manager.penize + "KC";
        manager.reputace += 3;

        StartCoroutine(manager.NoveKoloPrsi());
    }
}
