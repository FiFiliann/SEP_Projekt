using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OponentovaIkonka : MonoBehaviour
{
    public manager manager;
    public TextMeshProUGUI OponentCelkovePenize;
    public TextMeshProUGUI OponentSazka;
    public Image OponentIkonka;
    public GameObject opponentPrefabs; // Pokud chceš rùzné postavy, jinak jen GameObject opponentPrefab;
    public Transform[] spawnPoints;
    public Transform OponentiPozice;
    public int CisloOponenta;
    public int Ikonka;
    public int Penize;
    public int Sazka;
    public bool dovysovani = false;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OponentCelkovePenize = transform.Find("OponentoviPenize").GetComponent<TextMeshProUGUI>();
        OponentSazka = transform.Find("OponentovaSazka").GetComponent<TextMeshProUGUI>();
        OponentIkonka = transform.Find("OponentVzhled").GetComponent<Image>();
        OponentiPozice = GameObject.Find("ZidleProOponenty").GetComponent<Transform>();
        //OponentIkonka.GetComponent<Image>().sprite = OponentSprity[Ikonka];
        Penize = CelkovePenizeRandom(); OponentCelkovePenize.text = Penize + ",-";
        StartI();
        SazkaRandom(0);
        Sazky();
    }
    public void StartI()
    {
        for (int j = 0; j < manager.OponentiUStolu.Length; j++)
        {
            if (manager.OponentiUStolu[j] == null)
            {
                manager.OponentiUStolu[j] = Instantiate(opponentPrefabs, spawnPoints[j].position, spawnPoints[j].rotation, OponentiPozice);
                manager.OponentiUStolu[j].GetComponent<Image>().sprite = OponentIkonka.GetComponent<Image>().sprite;
                manager.OponentiUStolu[j].name = "Oponent" +j;
                manager.OponentiUStolu[j].GetComponent<OponentUStolu>().CisloOponenta = CisloOponenta;

                j = manager.OponentiDohromady.Length;                
            }
        }
    }

    public int CelkovePenizeRandom()
    {
        
        return Random.Range(400, 800);
    }
    public void SazkaRandom(int i)
    {
        if(!dovysovani)
        {
            Sazka = Random.Range(0, Penize); dovysovani = true;
            OponentSazka.text =Sazka + ",-";
            manager.secteni += Sazka;
            manager.OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = true;

        }
        else
        {
            if(manager.nejvyssiSazka != Sazka)
            {
                if(manager.nejvyssiSazka < Penize && Random.Range(0, 8)>2)
                {                  
                        Sazka = manager.nejvyssiSazka;
                        OponentSazka.text =Sazka + "";
                        manager.secteni += Sazka;
                        manager.OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = true;
                }
                else
                { 
                    Sazka = 0; OponentSazka.text = "OUT";
                    manager.OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = false;
                    manager.OponentiUStolu[i].GetComponent<OponentUStolu>().SkrytKarty();
                }
            }
            else 
            { 
                manager.secteni += Sazka; 
                manager.OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = true;
            }
        }
    }
    public void Sazky()
    {
        for(int i = 0; i < manager.sazky.Length; i++)
        {
            if (manager.sazky[i] == 0) 
            {
                manager.sazky[i] = Sazka;
                i = manager.sazky.Length;
            }
        }                
    }
}
