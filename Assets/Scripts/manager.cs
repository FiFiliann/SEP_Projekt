using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class manager : MonoBehaviour
{    
    public GameObject GameMenu;
    //BytMenu//
    public GameObject[] BytMenuVyber;
    public GameObject[] Buttons;
    public GameObject BytMenu;
    public GameObject[] BytButtons;
    //Podvod//
    public bool[] koupenaDovednosti;
    //Platby//
    public GameObject novaplatba;
    public Transform platbyContent;
    /* */public GameObject[] PlatbyDohromady = new GameObject[5];
    //ZmenaSceny//
    public GameObject[] background;
    public GameObject opona;
    public int staraScena = 0;
    public int novaScena = 0;
    public bool packa = false;
    public int rychlost = 0;
    //Promìné//
    public string datum = ".1.1998";
    public int den = 2;
    public int reputace = 1069;
    public int penize = 100;
    //-//
    private void Start()
    {
        opona = GameObject.Find("Stmivacka");
        opona.transform.position = new Vector3(0, 12, 0);
        background[staraScena].SetActive(true);
        platbyContent = GameObject.Find("PlatbyContent").GetComponent<Transform>();
        for (int i = 0; i < koupenaDovednosti.Length; i++)  {koupenaDovednosti[i] = false;}
        for (int i = 0; i < BytMenuVyber.Length; i++) { BytMenuVyber[i].SetActive(false); }
        BytMenuPromene();
    }
    void Update()
    {
        opona.transform.position += Vector3.down * Time.deltaTime * rychlost;
        if (opona.transform.position.y <= -12) { opona.transform.position = new Vector3(0, 12, 0); rychlost = 0; packa = true; }
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
    public void ButtonAkce()
    {
        if (novaScena != 0) { Buttons[0].SetActive(true); Buttons[1].SetActive(true); Buttons[2].SetActive(false); }
        else { Buttons[0].SetActive(true); Buttons[1].SetActive(false); Buttons[2].SetActive(true); }
    }
    public void MenuButtony(int a)
    {
        for (int i = 0; i < BytMenuVyber.Length; i++)
        {
            if (a == i) { BytMenuVyber[i].SetActive(true); BytButtons[i].GetComponent<Button>().interactable = false; }
            else { BytMenuVyber[i].SetActive(false); BytButtons[i].GetComponent<Button>().interactable = true; }
        }
    }
    public void VytvoreniPlatby()
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
    public void BytMenuPromene()
    {
        GameObject.Find("Penize").GetComponent<TextMeshProUGUI>().text = penize.ToString() + " KÈ";
        GameObject.Find("Datum").GetComponent<TextMeshProUGUI>().text = den.ToString() + datum;
        GameObject.Find("Reputace").GetComponent<TextMeshProUGUI>().text = reputace.ToString();
    }
}
