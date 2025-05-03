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

public class LizaniKaret : MonoBehaviour
{
    public List<string> balicek = new List<string>();
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

    public string ZnackaOdhozenaKarta;
    public int CisloOdhozenaKarta;
    public bool HracovoKolo = true;
    public bool KonecZacatekRozdavani = false;
    private void Start()
    {
        hracRuka = GameObject.Find("HracovaRuka").GetComponent<HracRuka>();
        manager = GameObject.Find("GameManager").GetComponent<manager>();
    }    
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
        while (GameObject.Find("OdhozovaciBalicek").transform.childCount != 0)
        {
            Destroy(GameObject.Find("OdhozovaciBalicek").transform.GetChild(0));
        }
        hracRuka.HracKarty.Clear();
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

    public void KonecAnimace()
    {
            GameObject kartadoRuky = Instantiate(KartaGo, GameObject.Find("HracovaRuka").transform);
            PrideleniKarty(kartadoRuky);
            hracRuka.HracKarty.Add(balicek[0]);
            balicek.RemoveAt(0);
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
        }
    }
    public void PrideleniKarty(GameObject i)
    {
        i.GetComponent<Karta>().LizaciBalicek_Hrac = true;
        i.GetComponent<Karta>().HracovaRukaPolohaProKartu = coze;
        i.GetComponent<Karta>().ZnackaKarty = balicek[0].Substring(0, 1);
        i.GetComponent<Karta>().CisloKarty = int.Parse(balicek[0].Substring(1, balicek[0].Length - 1));
    }
    public void PocatekHry()
    {        
        StartCoroutine(ZacatekRozdani());        
    }
    public void KoloOponenti()
    {
        StartCoroutine(Kolo());
    }   
    public IEnumerator Kolo()
    {
        for (int j = 0; j < manager.OponentiUStolu.Length; j++)
        {
            if (manager.OponentiUStolu[j] != null && manager.OponentiUStolu[j].GetComponent<OponentUStolu>().Hraje == true)
            {
                manager.OponentiUStolu[j].GetComponent<OponentUStolu>().OdhozeniKarty();
                if (!manager.OponentiUStolu[j].GetComponent<OponentUStolu>().OponentKarty.Any()) 
                { 
                    j = manager.OponentiUStolu.Length;
                    manager.sazeciOkenko.SetActive(true);
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        HracovoKolo = true;
    }
    public void KartaOponent(int j)
    {
        StartCoroutine(KartaOponentIE(j));
    }


    public IEnumerator StartRukaHrace()
    {

            KartaProHrace();
            yield return new WaitForSeconds(1f);
    
    }
    public IEnumerator StartKartaOdhozeni()
    {
        GameObject j = Instantiate(KartaGo, GameObject.Find("OdhozovaciBalicek").transform);
        PrideleniKarty(j);
        j.GetComponent<Karta>().LizaciBalicek_Hrac = false;
        j.GetComponent<Karta>().a = false;
        j.GetComponent<Karta>().LizaciBalicek_OdhazovaciBalicek = true;
        ZnackaOdhozenaKarta = balicek[0].Substring(0, 1);
        CisloOdhozenaKarta = int.Parse(balicek[0].Substring(1, balicek[0].Length-1));
        balicek.RemoveAt(0);
        transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + "";
        yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator ZacatekRozdani()
    {
        PripravaBalicku();
        KonecZacatekRozdavani = false;
        for (int i = 0;i < 4;i++) // počet karet
        {
            for (int j = 0; j < manager.OponentiUStolu.Length; j++) // počet hrajících oponentů
            {
                if (manager.OponentiUStolu[j] != null && manager.OponentiUStolu[j].GetComponent<OponentUStolu>().Hraje == true)
                {
                    KartaOponent(j);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            StartCoroutine(StartRukaHrace());
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(StartKartaOdhozeni());
        yield return new WaitForSeconds(1f);
        KonecZacatekRozdavani = true;
    }
    public IEnumerator KartaOponentIE(int j)
    {
        GameObject a = Instantiate(KartaGo, GameObject.Find("LizaciBalicek").transform);
        PrideleniKarty(a);
        a.GetComponent<Karta>().LizaciBalicek_Hrac = false;
        a.GetComponent<Karta>().a = false;
        a.GetComponent<Karta>().LizaciBalicek_Oponent = true;
        a.GetComponent<Karta>().ZnackaKarty = "J"; a.GetComponent<Karta>().CisloKarty = 3; // OTOČENÁ KARTA
        a.GetComponent<Karta>().OponentovaRuka = manager.OponentiUStolu[j].GetComponent<OponentUStolu>().OponentRuka;

        manager.OponentiUStolu[j].GetComponent<OponentUStolu>().OponentKarty.Add(balicek[0]);
        manager.OponentiUStolu[j].GetComponent<OponentUStolu>().PocetKaret.text = manager.OponentiUStolu[j].GetComponent<OponentUStolu>().OponentKarty.Count + "";
        balicek.RemoveAt(0);
        transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = balicek.Count + "";
        yield return new WaitForSeconds(1.5f);
    }

}
