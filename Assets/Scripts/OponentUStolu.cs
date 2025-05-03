using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections;

public class OponentUStolu : MonoBehaviour
{
    public TextMeshProUGUI PocetKaret;
    public GameObject Karta;
    public GameObject OdhozenaKarta;
    public GameObject LizaciBalicek;
    public GameObject OponentRuka;
    public GameObject KartyGUI;
    public Karta kartasc;
    public LizaniKaret LizKaret;
    public List<string> OponentKarty = new List<string>();

    public bool Hraje = true;
    public int CisloOponenta;
    private void Start()
    {
        if (gameObject.name == "Oponent3" || gameObject.name == "Oponent4")
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1); 
            PocetKaret.transform.localScale = new Vector3(-1, 1, 1);
            OponentRuka.transform.localScale = new Vector3(1, 1, 1);
        }
        LizaciBalicek = GameObject.Find("LizaciBalicek");
        LizKaret = GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>();
        PocetKaret.text = OponentKarty.Count + "";
    }
    public IEnumerator KontrolaProOdhozeniOponent()
    {
        // Odhozená karta je PIKOVÝ KRÁL
        if (LizKaret.CisloOdhozenaKarta == 13 && LizKaret.ZnackaOdhozenaKarta == "♠" && LizKaret.EfektKarty)
        {
            for (int i = 0; i < 5; i++)
            {
                StartCoroutine(LiznutiKartyOponent());//*StartCoroutine(LizKaret.LiznutiKartyOponent(CisloOponenta));
                yield return new WaitForSeconds(0.5f);
            }
            LizKaret.EfektKarty = false;
        }

        // Odhozená karta je SEDMA
        else if (LizKaret.CisloOdhozenaKarta == 7 && LizKaret.EfektKarty)
        {
            bool sedmaVRuce = false;
            for (int i = 0; i < OponentKarty.Count; i++)
            { 
                if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 7) 
                { 
                    sedmaVRuce = true; 
                    OdhozeniKartyS(i); 
                    i = OponentKarty.Count;
                }
            }
            if(!sedmaVRuce)
            {
                for (int i = 0; i < (LizKaret.pocetSedmicek * 3); i++)
                {
                    StartCoroutine(LiznutiKartyOponent());//*StartCoroutine(LizKaret.LiznutiKartyOponent(CisloOponenta));
                    yield return new WaitForSeconds(0.5f);
                }
                LizKaret.pocetSedmicek = 1;
                LizKaret.EfektKarty = false;
            }
        }

        // Odhozená karta je ESO
        else if (LizKaret.CisloOdhozenaKarta == 1 && LizKaret.EfektKarty)
        {
            bool EsoVRuce = false;
            for (int i = 0; i < OponentKarty.Count; i++)
            { 
                if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 1) 
                { 
                    EsoVRuce = true;
                    OdhozeniKartyS(i);
                    i = OponentKarty.Count;
                }
            }
            if (!EsoVRuce) { LizKaret.EfektKarty = false; }
        }

        // Odhozená karta je ŽOLÍK
        else if (LizKaret.ZnackaOdhozenaKarta == "J" && LizKaret.EfektKarty)
        {
            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(LiznutiKartyOponent());//*StartCoroutine(LizKaret.LiznutiKartyOponent(CisloOponenta));
                yield return new WaitForSeconds(0.5f);

            }
            LizKaret.EfektKarty = false;
        }
        
        // Odhozená karta je NORMÁLNÍ
        else
        {
            bool odhozenaKarta = false;
            for (int i = 0; i < OponentKarty.Count; i++)
            {
                if (OponentKarty[i].Substring(0, 1) == LizKaret.ZnackaOdhozenaKarta || int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == LizKaret.CisloOdhozenaKarta || OponentKarty[i].Substring(0, 1) == "J" || int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 12)
                {
                    OdhozeniKartyS(i);
                    i = OponentKarty.Count;
                    odhozenaKarta = true;
                }
            }
            if (!odhozenaKarta)
            {
                StartCoroutine(LiznutiKartyOponent());//*StartCoroutine(LizKaret.LiznutiKartyOponent(CisloOponenta));
                yield return new WaitForSeconds(0.5f);
            }
        }        
        yield return new WaitForSeconds(0.2f);
    }
    public IEnumerator LiznutiKartyOponent() // Lízmutí karty pro oponenta
    {
        GameObject a = Instantiate(Karta, GameObject.Find("LizaciBalicek").transform);
        a.transform.position = GameObject.Find("LizaciBalicek").transform.position;
        a.transform.localScale = new Vector3(0.5f, 0.5f, 0);

        a.GetComponent<Karta>().a = false;
        a.GetComponent<Karta>().ZnackaKarty = "J"; a.GetComponent<Karta>().CisloKarty = 3; // OTOČENÁ KARTA
        a.GetComponent<Karta>().OponentovaRuka = OponentRuka;
        a.GetComponent<Karta>().LizaciBalicek_Oponent = true;

        OponentKarty.Add(LizKaret.balicek[0]);
        PocetKaret.text = OponentKarty.Count + "";
        LizKaret.balicek.RemoveAt(0);
        //transform.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = LizKaret.balicek.Count + "";

        yield return new WaitForSeconds(1.5f);
    }

    public void OdhozeniKartyS(int i)
    {
                OdhozenaKarta = Instantiate(Karta, GameObject.Find("OdhozovaciBalicek").transform);
                OdhozenaKarta.transform.position = gameObject.transform.position;
                OdhozenaKarta.transform.localScale = new Vector3(1f, 1f, 0);

                SpecialniKartyOponent(i);

                LizKaret.ZnackaOdhozenaKarta = OdhozenaKarta.GetComponent<Karta>().ZnackaKarty;
                LizKaret.CisloOdhozenaKarta = OdhozenaKarta.GetComponent<Karta>().CisloKarty;

                OdhozenaKarta.GetComponent<Karta>().OponentovaRuka = OponentRuka;
                OdhozenaKarta.GetComponent<Karta>().Oponent_OdhazovaciBalicek = true;

                OponentKarty.RemoveAt(i);
                PocetKaret.text = OponentKarty.Count + "";

    }
    public string SecteniZnacek()
    {        
        int[] znacky = {0,0,0,0};
        for (int i = 0; i<OponentKarty.Count; i++)
        {
            switch(OponentKarty[i].Substring(0, 1)) 
            {
                case "♦":znacky[0]++; break;
                case "♣":znacky[1]++; break;
                case "♥":znacky[2]++; break;
                case "♠":znacky[3]++; break;
                default:Debug.Log("Chyba - secteniZnacek"); break;
            }
        }
        if (znacky[0] == znacky.Max()) { Debug.Log("♦"); return "♦"; }
        else if (znacky[1] == znacky.Max()) { Debug.Log("♣"); return "♣"; }
        else if (znacky[2] == znacky.Max()) { Debug.Log("♥"); return "♥"; }
        else if (znacky[3] == znacky.Max()) { Debug.Log("♠"); return "♠"; }
        else {Debug.Log("Chyba - secteniZnacek"); return "chyba"; } 
    }
    public void SkrytKarty()
    {
        KartyGUI.SetActive(false);
    }
    public void SpecialniKartyOponent(int i)
    {
        if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 12 || OponentKarty[i].Substring(0,1) == "J")
        {            
            LizKaret.CisloOdhozenaKarta = 14;
            LizKaret.ZnackaOdhozenaKarta = SecteniZnacek();
            OdhozenaKarta.GetComponent<Karta>().ZnackaKarty = LizKaret.ZnackaOdhozenaKarta;
            OdhozenaKarta.GetComponent<Karta>().CisloKarty = LizKaret.CisloOdhozenaKarta;
        }
        else if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 7) 
        { 
            LizKaret.EfektKarty = true; LizKaret.pocetSedmicek++;
            OdhozenaKarta.GetComponent<Karta>().ZnackaKarty = OponentKarty[i].Substring(0, 1);
            OdhozenaKarta.GetComponent<Karta>().CisloKarty = int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1));
        }
        else if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 13 && OponentKarty[i].Substring(0, 1) == "♠") 
        { 
            LizKaret.EfektKarty = true;
            OdhozenaKarta.GetComponent<Karta>().ZnackaKarty = OponentKarty[i].Substring(0, 1);
            OdhozenaKarta.GetComponent<Karta>().CisloKarty = int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1));
        }
        else if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 1) 
        { 
            LizKaret.EfektKarty = true;
            OdhozenaKarta.GetComponent<Karta>().ZnackaKarty = OponentKarty[i].Substring(0, 1);
            OdhozenaKarta.GetComponent<Karta>().CisloKarty = int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1));
        }
        else
        {
            OdhozenaKarta.GetComponent<Karta>().ZnackaKarty = OponentKarty[i].Substring(0, 1);
            OdhozenaKarta.GetComponent<Karta>().CisloKarty = int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1));
        }
    }
}
