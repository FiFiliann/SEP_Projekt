using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class manager : MonoBehaviour
{    
    public GameObject GameMenu;
    //Menu//
    public GameObject[] BytMenuVyber;
    public GameObject[] UI;
    public GameObject BytMenu;
    public GameObject[] BytButtons;

    //Podvod//
    public bool[] koupenaDovednosti;

    //Platby//
    public GameObject novaplatba;
    public Transform platbyContent;
    public GameObject[] PlatbyDohromady = new GameObject[5];

    //OponentiIkonka//
    public GameObject OponentIkonka;
    public Transform OponentIkonkaContent;
    public bool PrichodDoNoveSceny = true;
    public GameObject[] OponentiDohromady = new GameObject[5];
    public int[] sazky = new int[4];
    public int secteni = 0;
    public int nejvyssiSazka = 0;
    public Podvod podvod;

    //ZmenaSceny//
    public GameObject[] background;
    public GameObject opona;
    public int staraScena = 0;
    public int novaScena = 0;
    public int rychlost = 0;
    public bool packa = false;

    //Promìné//
    public string datum = ".1.1998";
    public int den = 2;
    public int reputace = 1069;
    public int penize = 100;

    //Karty//
    public Texture[] KartySrdce = new Texture[13];
    public Texture[] KartyKary = new Texture[13];
    public Texture[] KartyPiky = new Texture[13];
    public Texture[] KartyKrize = new Texture[13];

    private void Start()
    {
        opona = GameObject.Find("Stmivacka");
        opona.transform.position = new Vector3(0, 12, 0);
        background[staraScena].SetActive(true);

        for (int i = 0; i < koupenaDovednosti.Length; i++)  {koupenaDovednosti[i] = false;}
        for (int i = 0; i < BytMenuVyber.Length; i++) { BytMenuVyber[i].SetActive(false); }
        BytMenuPromene();
    }
    void Update()
    {
        opona.transform.position += Vector3.down * Time.deltaTime * rychlost;
        if (opona.transform.position.y <= -12) 
        {
            opona.transform.position = new Vector3(0, 12, 0); rychlost = 0; packa = true; 
                if (novaScena != 0)
                {VytvoreniOponentu();}
        }

        if (packa)
        {
            if (opona.transform.position.y <= 0)
            {
                if (novaScena == 0) { BytMenu.SetActive(true); BytMenu.transform.position = new Vector3(-14.5f, 0, 0); }
                else
                {
                    for (int i = 0; i < BytMenuVyber.Length; i++)
                    {
                        BytMenuVyber[i].SetActive(false);
                    }
                    BytMenu.SetActive(false);
                }
                ButtonAkce();

                background[staraScena].SetActive(false);
                background[novaScena].SetActive(true);
                staraScena = novaScena;
                packa = false;
            }
        }
        //porovnanaviSazek();
       //Debug.Log(sazky[0]);
    }
    public void ZmenaSceny(int a)
    {
        novaScena = a; rychlost = 5;
    }
    public void ExitGame()
    {
        Application.Quit();        

    }
    public void ExitMainMenu()
    {
        GameMenu.SetActive(false);
    }
    public void OpenMinMenu()
    {
        GameMenu.SetActive(true);
    }
    public void ButtonAkce() // skrytí/odkrytí tlaèítek podle scény
    {
        if (novaScena != 0) 
        {
            UI[0].SetActive(true); UI[1].SetActive(true); UI[2].SetActive(true); UI[3].SetActive(false); UI[4].SetActive(false);
        }
        else { UI[0].SetActive(false); UI[1].SetActive(false); UI[2].SetActive(false); UI[3].SetActive(true); UI[4].SetActive(true); }
    }
    public void MenuButtony(int a) // zjistí, které tlaèítko v bytovém menu jde stisknout
    {
        for (int i = 0; i < BytMenuVyber.Length; i++)
        {
            if (a == i) { BytMenuVyber[i].SetActive(true); BytButtons[i].GetComponent<Button>().interactable = false; }
            else { BytMenuVyber[i].SetActive(false); BytButtons[i].GetComponent<Button>().interactable = true; }
        }
    }
    public void VytvoreniPlatby() //vytvoøení nové platby
    {
        for (int i = 0; i < PlatbyDohromady.Length; i++)
        {
            if (PlatbyDohromady[i] == null)
            {
                PlatbyDohromady[i] = Instantiate(novaplatba, platbyContent);
                PlatbyDohromady[i].name = "platba" + i;
                i = PlatbyDohromady.Length;
            }
        }
    }
    public void VytvoreniOponentu() //vytvoøení nového Oponenta
    {                
        if(PrichodDoNoveSceny)
        {
            OponentiDohromady[0] = Instantiate(OponentIkonka, OponentIkonkaContent);
            OponentiDohromady[0].name = "OponentIkonka" + 1;
            for (int i = 1; i < OponentiDohromady.Length; i++)
            {
                int random = Random.Range(0, 2);
                if (random == 0) 
                {
                    for (int j = 1; j < OponentiDohromady.Length; j++)
                    {
                        if (OponentiDohromady[j] == null)
                        {
                            OponentiDohromady[j] = Instantiate(OponentIkonka, OponentIkonkaContent);
                            OponentiDohromady[j].name = "OponentIkonka" + (j+1); 
                            Thread.Sleep(2000);
                            j = OponentiDohromady.Length;
                        }
                    }
                }            
            }
            SecteniSazek();
            porovnanaviSazek();
        }
    }
    public void SecteniSazek()
    {
        for(int i = 0; i < sazky.Length;i++)
        {
            secteni += sazky[i];
        }
        Debug.Log(secteni);
    }
    public void porovnanaviSazek()
    {
        nejvyssiSazka = sazky[0];
        for (int i = 0; i < sazky.Length; i++)
        {
            if (nejvyssiSazka < sazky[i])
            { nejvyssiSazka = sazky[i]; }            

        }
        Debug.Log(nejvyssiSazka);
    }
    public void BytMenuPromene() //vypsání zmìny variabilit v menu
    {
        GameObject.Find("Penize").GetComponent<TextMeshProUGUI>().text = penize.ToString() + " KÈ";
        GameObject.Find("Datum").GetComponent<TextMeshProUGUI>().text = den.ToString() + datum;
        GameObject.Find("Reputace").GetComponent<TextMeshProUGUI>().text = reputace.ToString();
    }
}
