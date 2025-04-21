using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using System;
using UnityEditor.Sprites;
using UnityEngine;

public class LizaniKaret : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject KartaGo;
    GameObject a;
    GameObject coze;
    private void Update()
    {
        if (a != null && a.GetComponent<Karta>().packb == false)
        {
            Destroy(coze); Destroy(a); Instantiate(KartaGo, GameObject.Find("HracovaRuka").transform);
        }
    }
    public void KartaProHrace()
    {
        a = Instantiate(KartaGo, GameObject.Find("LizaciBalicek").transform);
        coze = Instantiate(KartaGo, GameObject.Find("HracovaRuka").transform);
        a.GetComponent<Karta>().packb = true;
        a.GetComponent<Karta>().HracovaRukaPolohaProKartu = coze;
        //coze.GetComponent<SpriteRenderer>().material.color = new Color(0,0,0,0);
        //HracovaKarta.transform.position = GameObject.Find("LizaciBalicek").transform.position;
    }

}
