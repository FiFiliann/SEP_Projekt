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
    public Sprite[] KartySrdce = new Sprite[13];
    public Sprite[] KartyKary = new Sprite[13];
    public Sprite[] KartyPiky = new Sprite[13];
    public Sprite[] KartyKrize = new Sprite[13];
    public Sprite[] KartySpecialni = new Sprite[2];
    public GameObject ZnackaVyberPopUp;
    public string ZnackaOdhozenaKarta;
    public int CisloOdhozenaKarta;
    public int pocetSedmicek = 0;
    public bool EfektKarty = true;
    public bool HracovoKolo = true;
    public bool KonecZacatekRozdavani = false;
    //
    public bool KartaVRukavuAktivni = false;
    public GameObject KartaVRukavuA;
    public GameObject KartaVRukavuB;

    //Podezření
    public Slider PodezreniSlider;
    public float PodezreniValue = 0f;
    public int pocetOponentu = 0;
    public int MaxPokusyPodvadeni = 3; // Maximální počet pokusů
    private int aktualniPokusyPodvadeni = 0; // Počet využitých pokusů

    public void Start()
    {
        hracRuka = GameObject.Find("HracovaRuka").GetComponent<HracRuka>();
        manager = GameObject.Find("GameManager").GetComponent<manager>();
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
            GameObject kartadoRuky = Instantiate(KartaGo, GameObject.Find("HracovaRuka").transform);
            PrideleniKarty(kartadoRuky);
            hracRuka.HracKarty.Add(balicek[0]);
            balicek.RemoveAt(0);
            if (!balicek.Any()) {DoplneniBalicku(); }
            transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + "";
            Destroy(coze);
            Destroy(a);
    }
    public void KartaProHrace()
    {
        if (a == null && HracovoKolo)
        {           
            a = Instantiate(KartaGo, GameObject.Find("LizaciBalicek").transform);
            coze = Instantiate(KartaNeviditelna, GameObject.Find("HracovaRuka").transform);
            PrideleniKarty(a);
            HracovoKolo = false;
            //if (KonecZacatekRozdavani == true) { StartCoroutine(Kolo()); }

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
                    j = manager.OponentiUStolu.Length;
                    yield return new WaitForSeconds(1.5f);
                    manager.sazeciOkenko.SetActive(true);
                    StartCoroutine(manager.NoveKoloPrsi());
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        HracovoKolo = true;
    }
    public IEnumerator StartKolo()
    {
        PripravaBalicku();
        ResetRuk();
        KonecZacatekRozdavani = false;
        for (int i = 0;i < 4;i++) // počet karet
        {
            for (int j = 0; j < manager.OponentiUStolu.Length; j++) // počet hrajících oponentů
            {
                if (manager.OponentiUStolu[j] != null && manager.OponentiUStolu[j].GetComponent<OponentUStolu>().Hraje == true)
                {
                    StartCoroutine(manager.OponentiUStolu[j].GetComponent<OponentUStolu>().LiznutiKartyOponent());//*
                    yield return new WaitForSeconds(0.5f);
                }
            }           
            HracovoKolo = true;           
            KartaProHrace();            
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(StartKartaOdhozeni());
        yield return new WaitForSeconds(0.5f);
        if (manager.KartaVRukavuKoupeno && GameObject.Find("KartaVRukavu").transform.childCount == 0) { KartaVRukavu(); }
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
        if (CisloOdhozenaKarta == 13 && ZnackaOdhozenaKarta == "♠") { EfektKarty = true; }
        if (CisloOdhozenaKarta == 1) { EfektKarty = true;}
        if (ZnackaOdhozenaKarta == "J") { EfektKarty = true;}
        if (ZnackaOdhozenaKarta == "J" || CisloOdhozenaKarta == 13) { ZnackaOdhozenaKarta = "E"; }


        yield return new WaitForSeconds(0.5f);
    }

    //KARTA V RUKAVU
    public void KartaVRukavu()
    {
        GameObject kartadoRukavu = Instantiate(KartaGo, GameObject.Find("KartaVRukavu").transform);
        PrideleniKarty(kartadoRukavu);
        kartadoRukavu.transform.position = GameObject.Find("LizaciBalicek").transform.position;
        kartadoRukavu.GetComponent<Karta>().LizaciBalicek_Rukav = true;
        kartadoRukavu.GetComponent<Karta>().TohleJeKartaVRukavu = true;
        KartaVRukavuA = kartadoRukavu;
        balicekOdhozene.Add(balicek[0]);
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

        if (aktualniPokusyPodvadeni < MaxPokusyPodvadeni)
        {
            aktualniPokusyPodvadeni++;
            // Zvýšení podezření o 1/3
            float zvyseniPodezreni = PodezreniSlider.maxValue / 3f;
            PodezreniSlider.value = Mathf.Min(PodezreniSlider.maxValue, PodezreniSlider.value + zvyseniPodezreni);


            Debug.Log($"Podvádíš! Podezření zvýšeno o {zvyseniPodezreni}. Zbývající pokusy: {MaxPokusyPodvadeni - aktualniPokusyPodvadeni}");
        }
        else
        {
            Debug.Log("Vyčerpal jsi všechny pokusy na podvádění!");
        }
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
    public IEnumerator EfektyKaretNaHrace()
    {      
        int pocetLiznutychKaret;
        // Odhozená karta je PIKOVÝ KRÁL
        if (CisloOdhozenaKarta == 13 && ZnackaOdhozenaKarta == "♠" && EfektKarty)
        {
            Debug.Log("KRÁL");
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
        else if (ZnackaOdhozenaKarta == "J" && EfektKarty)
        {
            pocetLiznutychKaret = 3;
            EfektKarty = false;

        }
        else{pocetLiznutychKaret = 1;}

        if(pocetLiznutychKaret != 0)
        {
            for(int i = 0; i < pocetLiznutychKaret; i++)
            {
                KartaProHrace();        
                yield return new WaitForSeconds(0.5f);
                
            }
        }
        StartCoroutine(Kolo());
    }        
}
