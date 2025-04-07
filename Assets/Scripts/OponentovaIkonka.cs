using TMPro;
using UnityEngine;

public class OponentovaIkonka : MonoBehaviour
{
    public manager manager;
    public TextMeshProUGUI OponentJmeno;
    public TextMeshProUGUI OponentCelkovePenize;
    public TextMeshProUGUI OponentSazka;
    //public Texture OponentObrazek;
    public string[] jmena = { "Stuart", "Billy", "Jan", "Lukáš", "Kenny", "Petr", };
    public int Penize;
    public int Sazka;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        OponentJmeno = transform.Find("OponentovoJmeno").GetComponent<TextMeshProUGUI>();
        OponentCelkovePenize = transform.Find("OponentoviPenize").GetComponent<TextMeshProUGUI>();
        OponentSazka = transform.Find("OponentovaSazka").GetComponent<TextMeshProUGUI>();
        Penize = CelkovePenizeRandom();
        Sazka = SazkaRandom();
        OponentJmeno.text = JmenoRandom();
        OponentCelkovePenize.text = Penize + "";
        OponentSazka.text = Sazka + "";
        Sazky();
    }
    public int CelkovePenizeRandom()
    {
        int i = Random.Range(0, 1000);
        return i;
    }
    public int SazkaRandom()
    {
        int i = Random.Range(0, Penize);
        return i;
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
                manager.sazky[i] = Penize;
                //Debug.Log(manager.sazky[i]);
                i = manager.sazky.Length;
            }
        }                
    }
}
