using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OponentovaIkonka : MonoBehaviour
{
    public manager manager;
    public TextMeshProUGUI OponentJmeno;
    public TextMeshProUGUI OponentCelkovePenize;
    public TextMeshProUGUI OponentSazka;
    public Image OponentIkonka;
    public Sprite[] OponentSprity = new Sprite[10];
    public int Ikonka;
    //public Texture OponentObrazek;
    public string[] jmena = { "Stuart", "Billy", "Jan", "Lukáš", "Kenny", "Petr", };
    public int Penize;
    public int Sazka;
    public bool dovysovani = false;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OponentJmeno = transform.Find("OponentovoJmeno").GetComponent<TextMeshProUGUI>();
        OponentCelkovePenize = transform.Find("OponentoviPenize").GetComponent<TextMeshProUGUI>();
        OponentSazka = transform.Find("OponentovaSazka").GetComponent<TextMeshProUGUI>();
        OponentIkonka = transform.Find("OponentVzhled").GetComponent<Image>();


        Penize = CelkovePenizeRandom();
        OponentCelkovePenize.text = "Peníze: " +Penize + "";

        SazkaRandom();


        OponentJmeno.text = JmenoRandom();

        Ikonka = IkonkaRandom();
        OponentIkonka.GetComponent<Image>().sprite = OponentSprity[Ikonka];

        Sazky();
    }

    public int IkonkaRandom()
    {      
        return Random.Range(1, OponentSprity.Length);;
    }

    public int CelkovePenizeRandom()
    {
        
        return Random.Range(400, 800);;
    }
    public void SazkaRandom()
    {
        if(!dovysovani)
        {
            Sazka = Random.Range(0, Penize); dovysovani = true;
            OponentSazka.text = "Sázka: " + Sazka + "";
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
                        OponentSazka.text = "Sázka: " + Sazka + "";
                        manager.secteni += Sazka;
                        transform.GetComponent<Image>().material.color = new Color(255, 255, 0);
                }
                else{ Sazka = 0; OponentSazka.text = "Vynechává"; transform.GetComponent<Image>().material.color = new Color(255, 0, 0); }
            }
            else { manager.secteni += Sazka;}
        }
    }
    public string JmenoRandom()
    {
        int i = Random.Range(0, jmena.Length);   
        return jmena[i];
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
