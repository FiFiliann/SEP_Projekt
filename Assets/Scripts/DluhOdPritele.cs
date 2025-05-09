using System;
using TMPro;
using UnityEngine;

public class DluhOdPritele : MonoBehaviour
{
    public manager manager;

    public TMP_InputField PujckaInput;
    public TextMeshProUGUI vratitKolikText;
    public TextMeshProUGUI vratitZaText;
    public TextMeshProUGUI PodtextText;
    public GameObject DluhOdKamaradaPopUp;
    public int dalsiPujckaZa;
    public int vratitKolik;
    public int vratitZa;
    public int pujcitSI;
    private bool spravnaPujcka = false;

    public void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        manager = GameObject.Find("GameManager").GetComponent<manager>();
    }
    public void odejit()
    {
        gameObject.SetActive(false);
    }
    public void VlozeniPujcky() // hráè potvrdí sázku
    {
        if (Int32.TryParse(PujckaInput.text, out pujcitSI))
        {
            Cas();
            vratitKolikText.text = vratitKolik.ToString();
            vratitZaText.text = vratitZa.ToString();

            if(vratitZa != 0 && vratitKolik != 0)
            {
                PodtextText.text = "JO, TO BY SLO.";
                vratitKolik += pujcitSI;
                spravnaPujcka = true;
                dalsiPujckaZa = vratitZa + 2;            
            }
            else if (pujcitSI < 0)
            {
                PodtextText.text = "VELICE HUMORNE.";
            }
            else if(vratitZa == 0 && vratitKolik == 0) { PodtextText.text = "TOLIK NEMAM, KAMARADE.\n PUJCUJI DO 1400"; }
        }
        else { PodtextText.text = "COZE?"; }
    }
    public void PotvrzeniPujcky()
    {
        if(spravnaPujcka)
        {
            manager.penize += pujcitSI;
            for (int i = 0; i < manager.PlatbyDohromady.Length; i++)
            {
                if (manager.PlatbyDohromady[i] == null)
                {
                    manager.PlatbyDohromady[i] = Instantiate(manager.novaplatba, manager.platbyContent);
                    manager.PlatbyDohromady[i].GetComponent<Platba>().PriteluvDluh(vratitKolik, vratitZa);
                    manager.PlatbyDohromady[i].name = "platba" + i;
                    i = manager.PlatbyDohromady.Length;
                    manager.BytMenuPromene();
                    spravnaPujcka = false;
                    PodtextText.text = "SUPER. VRAT SE ZA " + dalsiPujckaZa + " DNY, PAK ZASE PUJCIM.";
                }
            }
        }
        else { PodtextText.text = "NEVIM CO CHCES, KAMARADE"; }
    }
    public void Cas()
    {
        if(pujcitSI > 0 && pujcitSI < 199) { vratitZa = 1; vratitKolik = 100; }
        else if (pujcitSI >= 200 && pujcitSI < 449) { vratitZa = 2; vratitKolik = 150; }
        else if (pujcitSI >= 450 && pujcitSI < 799) { vratitZa = 3; vratitKolik = 250; }
        else if (pujcitSI >= 800 && pujcitSI < 1401) { vratitZa = 4; vratitKolik = 400; }
        else { vratitZa = 0; vratitKolik = 0; }
    }
    public void PodTextPriPrichodu()
    {
        if (dalsiPujckaZa == 0)
        { PodtextText.text = "JE LIBO PUJCKA?"; }
        else
        { PodtextText.text = "VRAT SE ZA " + dalsiPujckaZa + " DNY, PAK ZASE PUJCIM."; }
    }
    public void PujckaOdKamarada()
    {
        DluhOdKamaradaPopUp.SetActive(true);
    }
}
