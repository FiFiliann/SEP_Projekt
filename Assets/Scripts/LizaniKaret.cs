using NUnit.Framework;
using System;
using UnityEngine;

public class LizaniKaret : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject HracovaKarta;
    public GameObject LizaciBalicek;

    public Karta karta;

    public void Start()
    {
        LizaciBalicek = GameObject.Find("LizaciBalicek");

    }
    public void KartaProHrace()
    {
        Instantiate(HracovaKarta, gameObject);
        //HracovaKarta.transform.position = new Vector3(0, 3, 11);
        //HracovaKarta.transform.position = GameObject.Find("LizaciBalicek").transform.position;
    }

    private void Instantiate(GameObject hracovaKarta, GameObject hracovaRuka)
    {
        throw new NotImplementedException();
    }
}
