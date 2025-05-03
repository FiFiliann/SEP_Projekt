using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public void OdhozeniKarty()
    {
        bool odhozenaKarta = false;
        for(int i = 0;i<OponentKarty.Count;i++)
        {
            if (OponentKarty[i].Substring(0, 1) == LizKaret.ZnackaOdhozenaKarta || int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1)) == LizKaret.CisloOdhozenaKarta)
            {
                OdhozenaKarta = Instantiate(Karta, GameObject.Find("OdhozovaciBalicek").transform);
                OdhozenaKarta.transform.position = gameObject.transform.position;
                OdhozenaKarta.transform.localScale = new Vector3(1f, 1f, 0);

                OdhozenaKarta.GetComponent<Karta>().ZnackaKarty = OponentKarty[i].Substring(0, 1);
                OdhozenaKarta.GetComponent<Karta>().CisloKarty = int.Parse(OponentKarty[i].Substring(1, OponentKarty[i].Length - 1));

                LizKaret.ZnackaOdhozenaKarta = OdhozenaKarta.GetComponent<Karta>().ZnackaKarty;
                LizKaret.CisloOdhozenaKarta = OdhozenaKarta.GetComponent<Karta>().CisloKarty;

                OdhozenaKarta.GetComponent<Karta>().OponentovaRuka = OponentRuka;
                OdhozenaKarta.GetComponent<Karta>().Oponent_OdhazovaciBalicek = true;

                OponentKarty.RemoveAt(i);
                PocetKaret.text = OponentKarty.Count + "";
                i = OponentKarty.Count;
                odhozenaKarta = true;
            }
        }
        if(!odhozenaKarta)
        {
            LizKaret.KartaOponent(CisloOponenta);
        }
    }
    public void SkrytKarty()
    {
        KartyGUI.SetActive(false);
    }
}
