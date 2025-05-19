using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections;

public class OponentUStolu : MonoBehaviour
{
    public manager manager;
    public TextMeshProUGUI PocetKaret;
    public GameObject Karta;
    public GameObject OdhozenaKarta;
    public GameObject LizaciBalicek;
    public GameObject OponentRuka;
    public GameObject KartyGUI;
    public Karta kartasc;
    public LizaniKaret LizKaret;
    public vysledekPopUp vysPopUp;

    public List<string> OponentKarty = new List<string>();

    public bool Hraje = true;
    public int CisloOponenta;
    public int otocka;
    private void Start()
    {
        vysPopUp = GameObject.Find("Vysledek").GetComponent<vysledekPopUp>();
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        if (otocka == 4 || otocka == 5)
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
            LizKaret.EfektKarty = false;
            for (int i = 0; i < 5; i++)
            {
                StartCoroutine(LiznutiKartyOponent());
                yield return new WaitForSeconds(0.3f);
            }
        }
        else if (LizKaret.CisloOdhozenaKarta == 12 &&  LizKaret.EfektKarty)
        {
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
            if (!sedmaVRuce)
            {
                LizKaret.EfektKarty = false;
                for (int i = 0; i < (LizKaret.pocetSedmicek * 3); i++)
                {
                    StartCoroutine(LiznutiKartyOponent());
                    yield return new WaitForSeconds(0.3f);
                }
                LizKaret.pocetSedmicek = 0;
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
        else if (LizKaret.CisloOdhozenaKarta == 14 && LizKaret.EfektKarty)
        {
            Debug.Log("Coze");
            for (int i = 0; i < 3; i++)
            {
                StartCoroutine(LiznutiKartyOponent());
                yield return new WaitForSeconds(0.3f);

            }            
            LizKaret.EfektKarty = false;
        }

        // Odhozená karta je NORMÁLNÍ
        else
        {
            bool odhozenaKarta = false;
            for (int i = 0; i < OponentKarty.Count; i++)
            {
                if (OponentKarty[i].Substring(0, 1) == LizKaret.ZnackaOdhozenaKarta || int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == LizKaret.CisloOdhozenaKarta || LizKaret.ZnackaOdhozenaKarta == "E" || OponentKarty[i].Substring(0, 1) == "J" || int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 12)
                {
                    OdhozeniKartyS(i);
                    i = OponentKarty.Count;
                    odhozenaKarta = true;
                }


            }
            if (!odhozenaKarta)
            {
                StartCoroutine(LiznutiKartyOponent());
                yield return new WaitForSeconds(0.3f);
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
    public IEnumerator LiznutiKartyOponent() // Lízmutí karty pro oponenta
    {
        GameObject a = Instantiate(Karta, GameObject.Find("LizaciBalicek").transform);
        a.transform.position = GameObject.Find("LizaciBalicek").transform.position;
        a.transform.localScale = new Vector3(0.3f, 0.3f, 0);

        a.GetComponent<Karta>().a = false;
        a.GetComponent<Karta>().ZnackaKarty = "J"; 
        a.GetComponent<Karta>().CisloKarty = 3; // OTOČENÁ KARTA
        a.GetComponent<Karta>().OponentovaRuka = OponentRuka;
        a.GetComponent<Karta>().LizaciBalicek_Oponent = true;

        OponentKarty.Add(LizKaret.balicek[0]);
        PocetKaret.text = OponentKarty.Count + "";
        LizKaret.balicek.RemoveAt(0);
        if (!LizKaret.balicek.Any()) { LizKaret.DoplneniBalicku(); }
        GameObject.Find("LizaciBalicekPocetKaret").GetComponent<TextMeshProUGUI>().text = LizKaret.balicek.Count + "";

        yield return new WaitForSeconds(0.3f);
    }

    public void OdhozeniKartyS(int i)
    {
        OdhozenaKarta = Instantiate(Karta, GameObject.Find("OdhozovaciBalicek").transform);
        OdhozenaKarta.transform.position = gameObject.transform.position;
        OdhozenaKarta.transform.localScale = new Vector3(1f, 1f, 0);

        SpecialniKartyOponent(i);

        //LizKaret.ZnackaOdhozenaKarta = OdhozenaKarta.GetComponent<Karta>().ZnackaKarty;
        //LizKaret.CisloOdhozenaKarta = OdhozenaKarta.GetComponent<Karta>().CisloKarty;

        OdhozenaKarta.GetComponent<Karta>().OponentovaRuka = OponentRuka;
        OdhozenaKarta.GetComponent<Karta>().Oponent_OdhazovaciBalicek = true;
        LizKaret.balicekOdhozene.Add(OponentKarty[i]);
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
                default:Debug.Log("jok"); break;
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
    public void UkazatKarty()
    {
        KartyGUI.SetActive(true);
    }
    public void SpecialniKartyOponent(int i)
    {
        if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 12 || OponentKarty[i].Substring(0,1) == "J") //Výběr znaku 
        {
            DialogOponent("TROCHU TO ZMENIME! CO VY NA TO?");
            if (OponentKarty[i].Substring(0, 1) == "J") { LizKaret.EfektKarty = true; }
            OdhozenaKarta.GetComponent<Karta>().ZnackaKarty = OponentKarty[i].Substring(0, 1);
            OdhozenaKarta.GetComponent<Karta>().CisloKarty = int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1));
            LizKaret.CisloOdhozenaKarta = 14;
            LizKaret.ZnackaOdhozenaKarta = SecteniZnacek();

            switch (LizKaret.ZnackaOdhozenaKarta)
            {
                case "♣": Debug.Log("KRIZ"); GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = LizKaret.KartyKrize[13]; break;
                case "♦": Debug.Log("KAR"); GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = LizKaret.KartyKary[13]; break;
                case "♥": Debug.Log("SRDCE"); GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = LizKaret.KartySrdce[13]; break;
                case "♠": Debug.Log("PIK"); GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = LizKaret.KartyPiky[13]; break;
                default: Debug.Log("CHYBA"); break;
            }
        }
        else
        {
            if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 7) //SEDMA
            { 
                LizKaret.EfektKarty = true; LizKaret.pocetSedmicek++; DialogOponent("SEDMA KAMARADE!");
                //KartaOznaceni(i);

            }
            else if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 13 && OponentKarty[i].Substring(0, 1) == "♠")  //PIKOVÝ KRÁL
            { 
                LizKaret.EfektKarty = true; DialogOponent("LIZEJ, KAMARADE, LIZEJ");
                //KartaOznaceni(i);
            }
            else if (int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == 1)  // ESO
            { 
                LizKaret.EfektKarty = true; DialogOponent("A STOP!");
                //KartaOznaceni(i);
            }
            //else { KartaOznaceni(i); }
            KartaOznaceni(i);
        }
    }
    private void KartaOznaceni(int i)
    {
        OdhozenaKarta.GetComponent<Karta>().ZnackaKarty = OponentKarty[i].Substring(0, 1);
        OdhozenaKarta.GetComponent<Karta>().CisloKarty = int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1));
        //OdhozenaKarta.GetComponent<Karta>().Obrazek();
        //GameObject.Find("OdhozenaKartaZvetseni").GetComponent<Image>().sprite = OdhozenaKarta.GetComponent<Image>().sprite;
        LizKaret.ZnackaOdhozenaKarta = OdhozenaKarta.GetComponent<Karta>().ZnackaKarty;
        LizKaret.CisloOdhozenaKarta = OdhozenaKarta.GetComponent<Karta>().CisloKarty;
    }
    public void PrideleniPenezOponentovy()
    {
        manager.OponentiDohromady[CisloOponenta].GetComponent<OponentovaIkonka>().Penize += manager.secteni - manager.sazky[CisloOponenta];
        manager.OponentiDohromady[CisloOponenta].GetComponent<OponentovaIkonka>().OponentCelkovePenize.text = manager.OponentiDohromady[CisloOponenta].GetComponent<OponentovaIkonka>().Penize + ",-";
    }
    public void OdecteniPenezOponentovy()
    {
        manager.OponentiDohromady[CisloOponenta].GetComponent<OponentovaIkonka>().Penize -= manager.sazky[CisloOponenta] ;
        manager.OponentiDohromady[CisloOponenta].GetComponent<OponentovaIkonka>().OponentCelkovePenize.text = manager.OponentiDohromady[CisloOponenta].GetComponent<OponentovaIkonka>().Penize + ",-";
    }
    public void DialogOponent(string text)
    {
        if(!LizKaret.KecaniSpustene)
        {
            GameObject OponentDialog = Instantiate(LizKaret.DialogPrefab, LizKaret.DialogPoloha.transform);
            OponentDialog.GetComponent<Dialog>().OponentDialogIkonka.GetComponent<Image>().sprite = manager.OponentiDohromady[CisloOponenta].GetComponent<OponentovaIkonka>().OponentIkonka.GetComponent<Image>().sprite;
            OponentDialog.GetComponent<Dialog>().OponentDialog.SetActive(true);
            OponentDialog.GetComponent<Dialog>().HracDialog.SetActive(false);
            OponentDialog.GetComponent<Dialog>().TohleJe = "Oponent";
            OponentDialog.GetComponent<Dialog>().OponentDialogText.text = text; 
        }
    }
}
