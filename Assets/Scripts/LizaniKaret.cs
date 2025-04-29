using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Sprites;
using UnityEngine;

public class LizaniKaret : MonoBehaviour
{
    public List<string> balicek = new List<string>();
    public GameObject KartaGo;
    public GameObject a;
    GameObject coze;
    public HracRuka hracRuka;
    public OponentUStolu oponentUStolu;
    public manager manager;
    public Sprite[] KartySrdce = new Sprite[13];
    public Texture[] KartyKary = new Texture[13];
    public Texture[] KartyPiky = new Texture[13];
    public Texture[] KartyKrize = new Texture[13];
    public Texture[] KartySpecialni = new Texture[2];


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
            balicek.Add("♣J");
            balicek.Add("♣Q");
            balicek.Add("♣K");

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
            balicek.Add("♦J");
            balicek.Add("♦Q");
            balicek.Add("♦K");

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
            balicek.Add("♥J");
            balicek.Add("♥Q");
            balicek.Add("♥K");

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
            balicek.Add("♠J");
            balicek.Add("♠Q");
            balicek.Add("♠K");

            balicek.Add("J");
            balicek.Add("J");

            RozmichaniKaret();
        } //přirazení karet
    }
    private void Update()
    {
        if (a != null && a.GetComponent<Karta>().LizaciBalicek_Hrac == false)
        {
            Destroy(coze); Destroy(a); Instantiate(KartaGo, GameObject.Find("HracovaRuka").transform);
        }
    }
    public void KartaProHrace()
    {
        if (a == null)
        {           
            a = Instantiate(KartaGo, GameObject.Find("LizaciBalicek").transform);            
            a.GetComponent<Karta>().kartaInfo = balicek[5];
            GameObject.Find("HracovaRuka").GetComponent<HracRuka>().HracKarty.Add(balicek[5]);
            balicek.RemoveAt(5);
            coze = Instantiate(KartaGo, GameObject.Find("HracovaRuka").transform);
            a.GetComponent<Karta>().LizaciBalicek_Hrac = true;
            a.GetComponent<Karta>().HracovaRukaPolohaProKartu = coze;
        }
    }
    public void KartyPng()
    {

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
