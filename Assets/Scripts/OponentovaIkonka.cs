using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OponentovaIkonka : MonoBehaviour
{
    public manager manager;
    public TextMeshProUGUI OponentCelkovePenize;
    public TextMeshProUGUI OponentSazka;
    public Image OponentIkonka;
    public Sprite[] OponentSprity = new Sprite[10];
    public GameObject opponentPrefabs; // Pokud chceš rùzné postavy, jinak jen GameObject opponentPrefab;
    public Transform[] spawnPoints;
    public Transform OponentiPozice;
    public int Ikonka;
    //public Texture OponentObrazek;
    public int Penize;
    public int Sazka;
    public bool dovysovani = false;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OponentCelkovePenize = transform.Find("OponentoviPenize").GetComponent<TextMeshProUGUI>();
        OponentSazka = transform.Find("OponentovaSazka").GetComponent<TextMeshProUGUI>();
        OponentIkonka = transform.Find("OponentVzhled").GetComponent<Image>();
        OponentiPozice = GameObject.Find("OponentiPozice").GetComponent<Transform>();

        Penize = CelkovePenizeRandom(); OponentCelkovePenize.text = Penize + ",-";
        SazkaRandom();
        Ikonka = IkonkaRandom(); OponentIkonka.GetComponent<Image>().sprite = OponentSprity[Ikonka];
        StartI();
        Sazky();
    }
    public void StartI()
    {
        for (int j = 0; j < manager.OponentiUStolu.Length; j++)
        {
            if (manager.OponentiUStolu[j] == null)
            {
                manager.OponentiUStolu[j] = Instantiate(opponentPrefabs, spawnPoints[j].position, spawnPoints[j].rotation, OponentiPozice);
                manager.OponentiUStolu[j].GetComponent<Image>().sprite = OponentSprity[Ikonka];
                manager.OponentiUStolu[j].name = "Oponent" +j;
                j = manager.OponentiDohromady.Length;                
            }
        }
    }
    public int IkonkaRandom()
    {      
        return Random.Range(1, OponentSprity.Length);
    }

    public int CelkovePenizeRandom()
    {
        
        return Random.Range(400, 800);
    }
    public void SazkaRandom()
    {
        if(!dovysovani)
        {
            Sazka = Random.Range(0, Penize); dovysovani = true;
            OponentSazka.text =Sazka + ",-";
            manager.secteni += Sazka;
        }
        else
        {
            if(manager.nejvyssiSazka != Sazka)
            {
                int i = Random.Range(0, 8);
                if(manager.nejvyssiSazka < Penize && i>2)
                {                  
                        Sazka = manager.nejvyssiSazka;
                        OponentSazka.text =Sazka + "";
                        manager.secteni += Sazka;
                        //transform.GetComponent<Image>().material.color = new Color(255, 255, 0);
                }
                else{ Sazka = 0; OponentSazka.text = "OUT";  } //transform.GetComponent<Image>().material.color = new Color(255, 0, 0);
            }
            else { manager.secteni += Sazka;}
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
