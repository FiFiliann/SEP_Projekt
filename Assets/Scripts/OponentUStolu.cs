using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OponentUStolu : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
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

    public bool Hraje = false;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        //z oponentovy ruky do odhazovazíco balíèku
        OdhozenaKarta = Instantiate(Karta, GameObject.Find("OdhozovaciBalicek").transform);
        OdhozenaKarta.transform.position = gameObject.transform.position;
        OdhozenaKarta.transform.localScale = new Vector3(1f,1f, 0);
        OdhozenaKarta.GetComponent<Karta>().OponentovaRuka = OponentRuka;
        OdhozenaKarta.GetComponent<Karta>().Oponent_OdhazovaciBalicek = true;
    }

/*
    private GameObject Instantiate(GameObject karta, GameObject oponentuvBalicek)
    {
        throw new NotImplementedException();
    }
*/
    public void SkrytKarty()
    {
        KartyGUI.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
            gameObject.transform.position += new Vector3(0, 0.5f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

            gameObject.transform.position -= new Vector3(0, 0.5f, 0);
    }
}
