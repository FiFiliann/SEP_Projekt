using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public string posledniKartaOdhazovaciBalicek;
    public string ZnackaOdhozenaKarta;
    public int CisloOdhozenaKarta;

    private void Start()
    {
        hracRuka = GameObject.Find("HracovaRuka").GetComponent<HracRuka>();
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        if (true)
        {
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
        } //přirazení karet
    }
    private void Update()
    {
        if (a != null && a.GetComponent<Karta>().LizaciBalicek_Hrac == false)
        {
            GameObject kartadoRuky = Instantiate(KartaGo, GameObject.Find("HracovaRuka").transform);
            PrideleniKarty(kartadoRuky);
            hracRuka.HracKarty.Add(balicek[0]);
            balicek.RemoveAt(0);
            Destroy(coze); 
            Destroy(a);
        }
    }
    public void KartaProHrace()
    {
        if (a == null)
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
        if (balicek[0].Length == 2) { i.GetComponent<Karta>().CisloKarty = int.Parse(balicek[0].Substring(1, 1)); }
        else { i.GetComponent<Karta>().CisloKarty = int.Parse(balicek[0].Substring(1, 2)); }
       
    }
    public void PocatekHry()
    {        
        StartCoroutine(StartKartaOdhozeni());
        StartCoroutine(StartKartaOponent());
    }
    public IEnumerator StartRukaHrace()
    {

            KartaProHrace();
            yield return new WaitForSeconds(0.5f);
    
    }
    public IEnumerator StartKartaOdhozeni()
    {
        GameObject j = Instantiate(KartaGo, GameObject.Find("OdhozovaciBalicek").transform);
        PrideleniKarty(j);
        j.GetComponent<Karta>().LizaciBalicek_Hrac = false;
        j.GetComponent<Karta>().a = false;
        j.GetComponent<Karta>().LizaciBalicek_OdhazovaciBalicek = true;
        posledniKartaOdhazovaciBalicek = balicek[0];
        balicek.RemoveAt(0);
        yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator StartKartaOponent()
    {
        for (int i = 0;i < 4;i++)
        {
            for (int j = 0; j < manager.OponentiUStolu.Length; j++)
            {
                if (manager.OponentiUStolu[j] != null && manager.OponentiUStolu[j].GetComponent<OponentUStolu>().Hraje == true)
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
                    yield return new WaitForSeconds(0.5f);
                }
            }
            StartCoroutine(StartRukaHrace());
            yield return new WaitForSeconds(0.7f);
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

}
