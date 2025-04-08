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
    public Sprite[] OponentSprity = new Sprite[5];
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
        OponentCelkovePenize.text = Penize + "";

        Sazka = SazkaRandom();
        OponentSazka.text = Sazka + "";

        OponentJmeno.text = JmenoRandom();

        Ikonka = IkonkaRandom();
        OponentIkonka.GetComponent<Image>().sprite = OponentSprity[Ikonka];

        Sazky();
    }

    public int IkonkaRandom()
    {      
        return Random.Range(0, 4);;
    }

    public int CelkovePenizeRandom()
    {
        
        return Random.Range(0, 1000);;
    }
    public int SazkaRandom()
    {
        if (!dovysovani)
        {
            return Random.Range(0, Penize);;
        }
        else {
            int i = Random.Range(0, 2);
            if (i == 1)
            {
                OponentSazka.text = Sazka + "";
                return Random.Range(manager.nejvyssiSazka, Penize ); 
            }
            else 
            {
                OponentSazka.text = Sazka + "";
                return 0; 
            }
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
                //Debug.Log(manager.sazky[i]);
                i = manager.sazky.Length;
            }
        }                
    }
}
