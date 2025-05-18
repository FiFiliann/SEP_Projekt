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
using NUnit.Framework;

public class manager : MonoBehaviour
{    
    public GameObject GameMenu;
    public GameObject GameOverScreen;
    public GameObject WinScreen;

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
    public GameObject NezaplacenyDluhPopUp;
    public bool PribehovyDluh = true;

    public GameObject PujckaOdPritelePopUpPrefab;
    public GameObject PujckaOdPritelePopUp;
    public int DalsiPujckaOdPriteleZa = 0;
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
    public int reputace = 50;
    public int penize = 1000;
    public TextMeshProUGUI denText;
    public TextMeshProUGUI reputaceText;
    public TextMeshProUGUI penizeText;

    public GameObject GameOverPopup;
    public Slider PodezreniSlider;

   

    public Button[] lokaceTlacitka;         // Tlačítka pro lokace
    public int[] lokaceReputace;            // Potřebná reputace pro každou lokaci
    public GameObject[] lokaceZamky;        // Ikonky zámků pro každou lokaci

    public Slider[] SeznamReputaceMapa;           // Seznam slideru na mape 

    public float casovac = 1080f; // čas v sekundách
    private bool casovacBezi = false;
    public TextMeshProUGUI casovacText;
    public bool pozdniHodina = false;
    public bool prilisPozde = false;

    public TextMeshProUGUI CelkovaSazkaText;




    //podvody
    public bool KartaVRukavuKoupeno = true;
    public bool Kecanikoupeno = true;
    private void Start()
    {
        opona = GameObject.Find("Stmivacka");
        opona.transform.position = new Vector3(0, 12, 0);
        background[staraScena].SetActive(true);

        for (int i = 0; i < koupenaDovednosti.Length; i++)  {koupenaDovednosti[i] = false;}
        for (int i = 0; i < BytMenuVyber.Length; i++) { BytMenuVyber[i].SetActive(false); }
        BytMenuPromene();
        SpustiCasovac();
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

        if (casovacBezi)
        {
            casovac += Time.deltaTime;
            int minuty = Mathf.FloorToInt(casovac / 60);
            int sekundy = Mathf.FloorToInt(casovac % 60);

            if (casovacText != null)
                casovacText.text = minuty.ToString("00") + ":" + sekundy.ToString("00");
            if (casovac >= 1410f) { pozdniHodina = true; casovacText.color = Color.red; }
            if (casovac >= 1440f) { casovac = 0f; }
            if (casovac >= 60f && pozdniHodina == true) { prilisPozde = true; }

        }
    }

    public void ZmenaSceny(int a)
    {

            novaScena = a; rychlost = 5;
            if(GameObject.Find("KartaVRukavu")  != null) 
            {
                if (GameObject.Find("KartaVRukavu").transform.childCount > 0)
                    DestroyImmediate(GameObject.Find("KartaVRukavu").transform.GetChild(0).gameObject);
            }

    }

    public void ExitMainMenu()
    {
        GameMenu.SetActive(false);
        if (PribehovyDluh)
        {
            PlatbyDohromady[0] = Instantiate(novaplatba, platbyContent);
            PlatbyDohromady[0].GetComponent<Platba>().PribehovyDluh();
            PlatbyDohromady[0].name = "PribehovyDluh";
            PribehovyDluh = false;
        }
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
            GameObject.Find("HracovaSazka").GetComponent<TextMeshProUGUI>().text = penize + "KC";          
            for (int j = 0; j < OponentiDohromady.Length; j++)
            {
                Destroy(OponentiUStolu[j]);
                Destroy(OponentiDohromady[j]);
            }
            SpustiCasovac();
            nejvyssiSazka = 0;
            secteni = 0;
            MenuButtony(10);
            GameObject.Find("CelkovaSazka").GetComponent<TextMeshProUGUI>().text = "nej: " + nejvyssiSazka + "  celkem: " + secteni;
            SazkaInput.text = "";
        }
        else {  UI[0].SetActive(true); UI[1].SetActive(false);}
    }
    public void MenuButtony(int a) // zjist�, kter� tla��tko v bytov�m menu jde stisknout
    {
        for (int i = 0; i < BytMenuVyber.Length; i++)
        {
            if (a == i) { BytMenuVyber[i].SetActive(true); BytButtons[i].GetComponent<Button>().interactable = false; }
            else { BytMenuVyber[i].SetActive(false); BytButtons[i].GetComponent<Button>().interactable = true; }
        }
    }
    //
    public void NezaplacenyDluhKontrola(int a)
    {
        bool vseVPohode = true;
        for (int i = 0; i < PlatbyDohromady.Length; i++)
        {
            if (PlatbyDohromady[i] != null)
            {
                if(PlatbyDohromady[i].GetComponent<Platba>()._cas == 0)
                {
                    vseVPohode = false;
                    GameObject upominkaPopUp = Instantiate(NezaplacenyDluhPopUp,GameObject.Find("HlavniMenu").transform);
                    upominkaPopUp.transform.position = new Vector3(0,0,9);
                    upominkaPopUp.name = "NzDh";
                    upominkaPopUp.GetComponent<NezaplacenyDluh>().cisloDluhu = i;
                    GameObject dluh = Instantiate(PlatbyDohromady[i], upominkaPopUp.transform);
                    dluh.transform.localScale = new Vector3(0.8f, 0.8f); 
                    dluh.transform.position = upominkaPopUp.GetComponent<NezaplacenyDluh>().NezaplacenyDluhPoloha.transform.position;
                    //dluh.GetComponent<NezaplacenyDluh>().cisloDluhu = 2;  ?
                    i = PlatbyDohromady.Length;
                }
            }
        }
        if(vseVPohode) { ZmenaSceny(a); }
    }
    public void VytvoreniPlatby() //vytvo�en� nov� platby
    {        
        for (int i = 0; i < PlatbyDohromady.Length; i++)
        {
            if (PlatbyDohromady[i] == null)
            {
                PlatbyDohromady[i] = Instantiate(novaplatba, platbyContent);
                PlatbyDohromady[i].GetComponent<Platba>().VedlejsiDluh();
                PlatbyDohromady[i].name = "platba" + i;
                i = PlatbyDohromady.Length;
            }
        }
    }
    public void NovyDen()
    {
        spawn.coz = true;
        den++;
        if (DalsiPujckaOdPriteleZa != 0) 
            {DalsiPujckaOdPriteleZa -= 1; }
        for (int i = 0; i < PlatbyDohromady.Length; i++)
        {
            if (PlatbyDohromady[i] != null)
            {

                PlatbyDohromady[i].GetComponent<Platba>()._cas -= 1;
                PlatbyDohromady[i].GetComponent<Platba>().cas.text = PlatbyDohromady[i].GetComponent<Platba>()._cas + "";
            }
        }
        for(int i = 0;i<2;i++)// VYTVORENI NOVE PLATBY
        {
            if(UnityEngine.Random.Range(0, 8) > 3) 
            {VytvoreniPlatby(); } 
        }

        BytMenuPromene();

    }

    public void OtevritPujckuOdPritele()
    {
        PujckaOdPritelePopUp = Instantiate(PujckaOdPritelePopUpPrefab,GameObject.Find("HlavniMenu").transform);
        PujckaOdPritelePopUp.GetComponent<DluhOdPritele>().PodTextPriPrichodu();
    }

    //OPONENTI
    IEnumerator VytvoreniOponenta()
    {
        GameObject.Find("Vynechat").GetComponent<Button>().interactable = false;
        GameObject.Find("Odejit").GetComponent<Button>().interactable = false;
        GameObject.Find("PotvrditSazku").GetComponent<Button>().interactable = false;
        dovysovani = false;
        SazkaInput.text = null;
        secteni = 0;
        nejvyssiSazka = 0;
        hracSazka = 0;
        for (int i = 0; i < sazky.Length; i++)
        { sazky[i] = 0; }

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
        GameObject.Find("Vynechat").GetComponent<Button>().interactable = false;
        GameObject.Find("Odejit").GetComponent<Button>().interactable = false;
        GameObject.Find("PotvrditSazku").GetComponent<Button>().interactable = false;
        SazkaInput.text = null;
        secteni = 0;
        nejvyssiSazka = 0;
        hracSazka = 0;
        for (int i = 0; i < sazky.Length; i++)
        { sazky[i] = 0;}
        
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
                if(prilisPozde)
                {
                    sazky[i] = 0;
                    Destroy(OponentiDohromady[i]);
                    Destroy(OponentiUStolu[i]);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    int random = UnityEngine.Random.Range(0, 3);
                    if (random == 0 || i == 0)
                    {
                        sazky[i] = 0;
                        Destroy(OponentiDohromady[i]);
                        Destroy(OponentiUStolu[i]);
                        yield return new WaitForSeconds(0.5f);
                    }
                }

            }
        }

        if (!OponentiDohromady.Any() && pozdniHodina == false)  // pridani oponenta pokud vsichni odesli
        {
            OponentiDohromady[0] = Instantiate(OponentIkonka, OponentIkonkaContent);
            OponentiDohromady[0].name = "OponentIkonka" + (1);
            OponentiDohromady[0].GetComponent<OponentovaIkonka>().CisloOponenta = 0;
            OponentiDohromady[0].GetComponent<OponentovaIkonka>().OponentIkonka.GetComponent<Image>().sprite = OponentIkonkaRandom();
            yield return new WaitForSeconds(0.5f);
        }
        dovysovani = false;
        if(pozdniHodina == false)
        {
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
                    }
                }
            }
        }

        for (int i = 0; i < OponentiDohromady.Length; i++) // výpis nových sázek
        {
            if (OponentiDohromady[i] != null)
            {
                OponentiDohromady[i].GetComponent<OponentovaIkonka>().SazkaRandom(i);
                yield return new WaitForSeconds(0.2f);
            }
        }
        dovysovani = true;
        OponentIkonkaReset();
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
        int prazdno = 0;
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
                //GameObject.Find("Vynechat").GetComponent<Button>().interactable = true;
                yield return new WaitForSeconds(0.2f);
                }
            else { prazdno++; }
            //else { i = sazky.Length; }
        }

        secteni += hracSazka;
        GameObject.Find("CelkovaSazka").GetComponent<TextMeshProUGUI>().text = "nej: " + nejvyssiSazka + "  celkem: " + secteni;

        if(prazdno != 5) 
        { 
            GameObject.Find("PotvrditSazku").GetComponent<Button>().interactable = true;
            GameObject.Find("Vynechat").GetComponent<Button>().interactable = true;
        }
        GameObject.Find("Odejit").GetComponent<Button>().interactable = true;

        yield return new WaitForSeconds(0.1f);

    }


    public void VlozeniSazky() // hráč potvrdí sázku
    {
        if (Int32.TryParse(SazkaInput.text, out int j))
        {
            if (j > 0 && j >= nejvyssiSazka)
            {
                hracSazka = j;
                // Aktualizace celkové sázky v UI
                if (CelkovaSazkaText != null)
                    CelkovaSazkaText.text = hracSazka + " KC";

                if (hracSazka > nejvyssiSazka)
                {
                    nejvyssiSazka = hracSazka;
                    StartCoroutine(porovnanaviSazek());
                }
                else
                {
                    for (int i = 0; i < sazky.Length; i++)
                    {
                        if (sazky[i] != 0)
                        {
                            StartCoroutine(GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>().StartKolo());
                            sazeciOkenko.SetActive(false); zacatekSazeni = true;
                            i = sazky.Length;
                        }
                    }
                }
            }
        }
    }


    public void ResetHry()
    {
        GameMenu.SetActive(true);
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);
        KartaVRukavuKoupeno = false;
        reputace = 0;
        penize = 100;
        for (int i = 0; i < PlatbyDohromady.Length; i++)
        {
            if (PlatbyDohromady[i] != null)
            {
                Destroy(PlatbyDohromady[i]);
            }
        }
        PribehovyDluh = true;


        if (CelkovaSazkaText != null)
            CelkovaSazkaText.text = "Celková sázka: 0 KC";


        VynulujPodezreni();

        BytMenuPromene();
    }

    public void KontrolaPodezeni()
    {
        if (PodezreniSlider.value >= PodezreniSlider.maxValue)
        {
            Debug.Log("Hra ukončena! Podezreni dosáhla maxima.");
            UkoncitHru();
        }

        VynulujPodezreni();
    }

    public void UkoncitHru()
    {
        GameOverPopup.SetActive(true);

        StopCasovac(); // Zastaví časovač
        GameOverScreen.SetActive(true);
        Time.timeScale = 0; // Zastaví čas ve hře

        VynulujPodezreni();

    }

    public void OdejitZeHry()
    {
        StopCasovac(); // Zastaví časovač
        Time.timeScale = 1; // Obnoví čas ve hře, pokud byl zastaven
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);
        BytMenu.SetActive(true);
        ZmenaSceny(0); // Přepne scénu na byt (index 0)

        VynulujPodezreni();
    }

    public void NavratDoBytu()
    {
        // Skryje pop-up okno
        GameOverPopup.SetActive(false);

        // Obnoví čas ve hře
        Time.timeScale = 1;

        // Přepne scénu na byt
        ZmenaSceny(0); // Předpokládám, že scéna "byt" má index 0
        VynulujPodezreni();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void AktualizovatLokaceVMenu()
    {
        for (int i = 0; i < lokaceTlacitka.Length; i++)
        {
            SeznamReputaceMapa[i].value = reputace;

            bool dostupna = reputace >= lokaceReputace[i];
            {
                lokaceTlacitka[i].interactable = dostupna;
                lokaceZamky[i].SetActive(!dostupna);
            }
        }

    }
    public void BytMenuPromene() //vyps�n� zm�ny variabilit v menu
    {
        penizeText.text = penize.ToString() + " KC";
        denText.text = den.ToString() + datum;
        reputaceText.text = reputace.ToString() + " REP";
        AktualizovatLokaceVMenu();
    }

    public void SpustiCasovac()
    {
        casovacText.color = Color.white;
        pozdniHodina = false;
        prilisPozde = false;
        casovac = 990f;
        casovacBezi = true;
    }

    public void StopCasovac()
    {
        casovacBezi = false;
    }

    private void VynulujPodezreni()
    {
        if (PodezreniSlider != null)
            PodezreniSlider.value = 0;
    }
}
