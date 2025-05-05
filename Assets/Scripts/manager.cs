using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using System.Linq;

public class manager : MonoBehaviour
{    
    public GameObject GameMenu;
    public spawn spawn;

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
    public GameObject[] OponentiDohromady = new GameObject[7];//for
    public GameObject[] OponentiUStolu = new GameObject[7];

    public int[] sazky = new int[7];
    public int secteni = 0;
    public int nejvyssiSazka = 0;
    public bool zacatekSazeni = true;
    public GameObject sazeciOkenko;    
    public OponentovaIkonka oponentUStolu;

    public List<Sprite> OponentiNePouziteSprity = new List<Sprite>();
    public List<Sprite> OponentiPouziteSprity = new List<Sprite>();
    public bool dovysovani = false;
    // Hrac Sazky//
    public int hracSazka;
    public TMP_InputField SazkaInput;

    //Hra//
    public string VrchniKarta;
    //ZmenaSceny//
    public GameObject[] background;
    public GameObject opona;
    public int staraScena = 0;
    public int novaScena = 0;
    public int rychlost = 0;
    public bool packa = false;

    //Prom�n�//
    public string datum = ".1.1998";
    public int den = 2;
    public int reputace = 1069;
    public int penize = 1000;

    // Zmeny, oponenti
    public int numberOfObjects = 6;

    //Podezreni
    public Slider PodezreniSlider;
    public float PodezreniValue = 0f;
    public int pocetOponentu = 0;

    //Reputace
    public Slider ReputaceSlider;
    public float ReputaceValue = 0f;

    private void Start()
    {
        opona = GameObject.Find("Stmivacka");
        opona.transform.position = new Vector3(0, 12, 0);
        background[staraScena].SetActive(true);

        for (int i = 0; i < koupenaDovednosti.Length; i++)  {koupenaDovednosti[i] = false;}
        for (int i = 0; i < BytMenuVyber.Length; i++) { BytMenuVyber[i].SetActive(false); }
        BytMenuPromene();

        //Inicializace slideru podezreni
        PodezreniSlider.value = 0f;
        PodezreniSlider.value =+ ZvysitPodezreni();
        PodezreniSlider.minValue = 0f;
        PodezreniSlider.maxValue = 5f;

        // Inicializace slideru reputace
        ReputaceSlider.value = ReputaceValue;
        ReputaceSlider.minValue = 0f;
        ReputaceSlider.maxValue = 5f;
    }
    void Update()
    {
        opona.transform.position += Vector3.down * Time.deltaTime * rychlost;
        if (opona.transform.position.y <= -12) 
        {
            opona.transform.position = new Vector3(0, 12, 0); rychlost = 0; packa = true; 
                if (novaScena != 0)
                { StartCoroutine(VytvoreniOponenta()); }
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

        // Kontrola, jestli podezření dosáhlo maxima
        if (PodezreniSlider.value >= 1f)
        {
            Debug.Log("Chytili tě!");
            OdebratReputaci();
        }
    }

    public float ZvysitPodezreni()
    {
        // Výpočet zvýšení podezření // Počet oponentů (1-5)
        //float zvyseni = (5f * pocetOponentu) / 100f;

        // Zvýšení hodnoty podezření
        //PodezreniValue += zvyseni;
        //PodezreniSlider.value = PodezreniValue;

        for(int i = 0; i < OponentiUStolu.Length; i++)
        {
            if (OponentiUStolu[i] != null)
            {
                pocetOponentu++;
            }
        }

        return (5f * pocetOponentu) / 100f;
    }

    public void SnizitPodezreni()
    {
        // Snížení podezření a zvýšení reputace
        PodezreniSlider.value = Mathf.Max(0f, PodezreniValue - 0.1f);

        BytMenuPromene(); // Aktualizace UI
    }

    private void OdebratReputaci()
    {
        // Odečtení reputace na základě počtu oponentů
        //Zmeny
        ReputaceSlider.value = ReputaceValue;

        PodezreniSlider.value -= (5f * pocetOponentu) / 100f;

        BytMenuPromene();
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
    public void ButtonAkce() // skryt�/odkryt� tla��tek podle sc�ny
    {
        if (novaScena != 0) 
        {
            UI[0].SetActive(false); UI[1].SetActive(true); sazeciOkenko.SetActive(true) /*BUDE SE HODIT DO BUDOUCNA*/;
            GameObject.Find("HracovaSazka").GetComponent<TextMeshProUGUI>().text = penize + "K�";          
            for (int j = 0; j < OponentiDohromady.Length; j++)
            {
                Destroy(OponentiUStolu[j]);
                Destroy(OponentiDohromady[j]);
            }
            nejvyssiSazka = 0;
            secteni = 0;
            MenuButtony(10);
            GameObject.Find("CelkovaSazka").GetComponent<TextMeshProUGUI>().text = "nej: " + nejvyssiSazka + "  celkem: " + secteni;
            SazkaInput.text = "";
        }
        else {  UI[0].SetActive(true); UI[1].SetActive(false); /*spawn.coz = true;*/ VytvoreniPlatby(); }
    }
    public void MenuButtony(int a) // zjist�, kter� tla��tko v bytov�m menu jde stisknout
    {
        for (int i = 0; i < BytMenuVyber.Length; i++)
        {
            if (a == i) { BytMenuVyber[i].SetActive(true); BytButtons[i].GetComponent<Button>().interactable = false; }
            else { BytMenuVyber[i].SetActive(false); BytButtons[i].GetComponent<Button>().interactable = true; }
        }
    }
    public void VytvoreniPlatby() //vytvo�en� nov� platby
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

    IEnumerator VytvoreniOponenta()
    {
        if (PrichodDoNoveSceny)
        {          
            OponentIkonkaReset();
            for (int i = 0; i < OponentiDohromady.Length; i++)
            {
                int random = UnityEngine.Random.Range(0, 2);    
                if (random == 0 || i == 0)
                {
                    for (int j = 0; j < OponentiDohromady.Length; j++)
                    {
                        if (OponentiDohromady[j] == null)
                        {
                            OponentiDohromady[j] = Instantiate(OponentIkonka, OponentIkonkaContent);
                            OponentiDohromady[j].name = "OponentIkonka" + (j + 1);
                            OponentiDohromady[j].GetComponent<OponentovaIkonka>().CisloOponenta = j;
                            OponentiDohromady[j].GetComponent<OponentovaIkonka>().OponentIkonka.GetComponent<Image>().sprite = OponentIkonkaRandom();
                            yield return new WaitForSeconds(0.5f);
                            j = OponentiDohromady.Length;
                        }
                    }
                }
            }
            
            for (int i = 0; i < OponentiDohromady.Length; i++)
            {
                if (OponentiDohromady[i] != null)
                {                    
                    OponentiDohromady[i].GetComponent<OponentovaIkonka>().SazkaRandom(i);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            dovysovani = true;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(porovnanaviSazek());
        }
    }
    
    public IEnumerator NoveKoloPrsi()
    {
        SazkaInput.text = null;
        secteni = 0;
        nejvyssiSazka = 0;
        hracSazka = 0;
        GameObject.Find("CelkovaSazka").GetComponent<TextMeshProUGUI>().text = "nej: " + nejvyssiSazka + "  celkem: " + secteni;

        for (int i = 0; i < OponentiDohromady.Length; i++) // reset původních oponentů
        {
            if (OponentiDohromady[i] != null)
            {
                OponentiUStolu[i].GetComponent<OponentUStolu>().KartyGUI.SetActive(true);
                OponentiUStolu[i].GetComponent<OponentUStolu>().Hraje = false;
                OponentiDohromady[i].GetComponent<OponentovaIkonka>().VypsaniIkonky();
            }
        }       
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < OponentiDohromady.Length-1; i++) // odstranení oponentů
        {
            if (OponentiDohromady[i] != null)
            {
                int random = UnityEngine.Random.Range(0, 2);
                if (random == 0 || i == 0)
                {
                    sazky[i] = 0;
                    Destroy(OponentiDohromady[i]);
                    Destroy(OponentiUStolu[i]);
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        for (int i = 0; i < OponentiDohromady.Length; i++) // přidání oponentů
        {
            if (OponentiDohromady[i] == null)
            {
                int random = UnityEngine.Random.Range(0, 2);
                if (random == 0 || i == 0)
                {
                    OponentiDohromady[i] = Instantiate(OponentIkonka, OponentIkonkaContent);                    
                    OponentiDohromady[i].name = "OponentIkonka" + (i + 1);
                    OponentiDohromady[i].GetComponent<OponentovaIkonka>().CisloOponenta = i;
                    OponentiDohromady[i].GetComponent<OponentovaIkonka>().OponentIkonka.GetComponent<Image>().sprite = OponentIkonkaRandom();

                    yield return new WaitForSeconds(1f);
                    i = OponentiDohromady.Length;
                }
            }
        }
        dovysovani = false;
        for (int i = 0; i < OponentiDohromady.Length; i++) // výpis nových sázek
        {
            if (OponentiDohromady[i] != null)
            {
                OponentiDohromady[i].GetComponent<OponentovaIkonka>().SazkaRandom(i);
                yield return new WaitForSeconds(0.2f);
            }
        }
        dovysovani = true;
        StartCoroutine(porovnanaviSazek());
    }
    public void OponentIkonkaReset()
    {
        for (int j = 0; j < OponentiPouziteSprity.Count; j++) { OponentiNePouziteSprity.Add(OponentiPouziteSprity[0]); OponentiPouziteSprity.RemoveAt(0); } //smazání celého listu
    }
    public Sprite OponentIkonkaRandom()
    {
        int a = UnityEngine.Random.Range(0, OponentiNePouziteSprity.Count);
        OponentiPouziteSprity.Add(OponentiNePouziteSprity[a]); 
        OponentiNePouziteSprity.RemoveAt(a);
        return OponentiPouziteSprity.LastOrDefault();
    }

    public IEnumerator porovnanaviSazek()
    {
        secteni = 0;
        if (zacatekSazeni) {nejvyssiSazka = sazky[0]; zacatekSazeni = false; }
        for (int i = 0; i < sazky.Length; i++) // nalezení největší sázky
        {
            if (nejvyssiSazka < sazky[i])
            { nejvyssiSazka = sazky[i]; }            
        }
                   
        for (int i = 0; i < sazky.Length; i++) // srovnání sázek
        {
            if (OponentiDohromady[i] != null)
                {
                    OponentiDohromady[i].GetComponent<OponentovaIkonka>().SazkaRandom(i);
                    yield return new WaitForSeconds(0.2f);
                }
            //else { i = sazky.Length; }
        }

        secteni += hracSazka;
        GameObject.Find("CelkovaSazka").GetComponent<TextMeshProUGUI>().text = "nej: " + nejvyssiSazka + "  celkem: " + secteni;
        yield return new WaitForSeconds(0.1f);

    }
    public void VlozeniSazky() // hráč potvrdí sázku
    {
        if (Int32.TryParse(SazkaInput.text, out int j))
        {
            if(j > 0 && j >= nejvyssiSazka)
            {
                hracSazka = j;
                if(hracSazka > nejvyssiSazka)
                { nejvyssiSazka = hracSazka; StartCoroutine(porovnanaviSazek()); }
                else {
                    StartCoroutine(GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>().StartKolo()); 
                    sazeciOkenko.SetActive(false); zacatekSazeni = true; 
                }
            }
        }
    }

    public void BytMenuPromene() //vyps�n� zm�ny variabilit v menu
    {
        GameObject.Find("Penize").GetComponent<TextMeshProUGUI>().text = penize.ToString() + " K�";
        GameObject.Find("Datum").GetComponent<TextMeshProUGUI>().text = den.ToString() + datum;
        GameObject.Find("Reputace").GetComponent<TextMeshProUGUI>().text = reputace.ToString();
    }
}
