using System.Collections;
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
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OponentCelkovePenize = transform.Find("OponentoviPenize").GetComponent<TextMeshProUGUI>();
        OponentIkonka = transform.Find("OponentVzhled").GetComponent<Image>();
        OponentSazka = transform.Find("OponentovaSazka").GetComponent<TextMeshProUGUI>();
        OponentiPozice = GameObject.Find("ZidleProOponenty").GetComponent<Transform>();
        OponentSazka.text = "";
        Penize = CelkovePenizeRandom(manager.novaScena); OponentCelkovePenize.text = Penize + ",-";
        Sazky();
       // StartI(0);

    }
    public void VynulovaniIkonky()
    {
        OponentCelkovePenize.text = Penize + ",-";
    }
    public void VypsaniIkonky()
    {
        OponentCelkovePenize.text = Penize + ",-";
        OponentSazka.text = "";
    }
    public void StartI(int j)
    {
        CisloOponenta = j;
        manager.OponentiUStolu[j] = Instantiate(opponentPrefabs, spawnPoints[j].position, spawnPoints[j].rotation, OponentiPozice);
        manager.OponentiUStolu[j].GetComponent<Image>().sprite = OponentIkonka.GetComponent<Image>().sprite;
        manager.OponentiUStolu[j].name = "Oponent" +j;
        manager.OponentiUStolu[j].GetComponent<OponentUStolu>().CisloOponenta = CisloOponenta;
    }

    public int CelkovePenizeRandom(int i)
    {
        
        switch(i)
        {
            case 1: return Random.Range(200, 300);
            case 2: return Random.Range(300, 500); 
            case 3: return Random.Range(500, 750); 
            case 4: return Random.Range(750, 1000);
            default: return Random.Range(400, 800);
        }
    }
    public void SazkaRandom(int i)
    {
        if (!manager.dovysovani)
        {
            Sazka = Random.Range(Mathf.FloorToInt((Penize/100)*12), Mathf.FloorToInt((Penize / 100) * 30));
            OponentSazka.text =Sazka + ",-";
            manager.sazky[i] = Sazka;

            manager.OponentiUStolu[i].GetComponent<OponentUStolu>().UkazatKarty();
            manager.OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = true;

        }
        else
        {
            if(manager.nejvyssiSazka != Sazka)
            {
                if(manager.nejvyssiSazka < Penize && Random.Range(0, 8)>2)
                {
                    Sazka = manager.nejvyssiSazka;
                    OponentSazka.text =Sazka + ",-";
                    manager.secteni += Sazka;
                    manager.sazky[i] = Sazka;

                    manager.OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = true;
                    manager.OponentiUStolu[i].GetComponent<OponentUStolu>().UkazatKarty();

                }
                else
                { 
                    Sazka = 0; OponentSazka.text = "OUT";
                    manager.sazky[i] = 0;

                    manager.OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = false;
                    manager.OponentiUStolu[i].GetComponent<OponentUStolu>().SkrytKarty();
                }
            }
            else 
            {             
                manager.secteni += Sazka;
                manager.OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = true;
                manager.OponentiUStolu[i].GetComponent<OponentUStolu>().UkazatKarty();
            
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
