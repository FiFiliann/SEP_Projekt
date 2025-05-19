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
    public double vratitKolik;
    public int vratitZa;
    public double pujcitSI;
    private bool spravnaPujcka = false;

    public void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
    }
    public void odejit()
    {
        gameObject.SetActive(false);
    }
    public void VlozeniPujcky() // hráè potvrdí sázku
    {
        switch(PujckaInput.text)
        {
            case "pen": manager.penize += 10000; Debug.Log("penize"); manager.BytMenuPromene(); break;
            case "rep": manager.reputace += 10000; Debug.Log("reputace"); manager.BytMenuPromene(); break;
            default: Pujcka(); break;
        }
    }

    public void Pujcka()
    {
        if (double.TryParse(PujckaInput.text, out pujcitSI))
        {       
            if(!manager.ZapujcenoOdPritele)
            {
                Cas();
                if(vratitZa != 0 && vratitKolik != 0)
                {
                    PodtextText.text = "JO, TO BY SLO.";
                    vratitKolik += pujcitSI;
                    spravnaPujcka = true;
                    manager.DalsiPujckaOdPriteleZa = vratitZa;   
                    vratitKolikText.text = "" + vratitKolik.ToString();
                    vratitZaText.text = vratitZa.ToString();
                }
                else if (pujcitSI <= 0)
                {
                    PodtextText.text = "VELICE HUMORNE.";
                }
                else if(vratitZa == 0 && vratitKolik == 0) 
                { 
                    PodtextText.text = "TOLIK NEMAM, KAMARADE.\n PUJCUJI DO 1400"; 
                }
            }
            else { PodtextText.text = "VRAT SE ZA " + manager.DalsiPujckaOdPriteleZa + " DNY, PAK ZASE PUJCIM."; }
        }
        else { PodtextText.text = "COZE?"; }
    }
    public void PotvrzeniPujcky()
    {
        if(spravnaPujcka)
        {
            manager.penize += Mathf.FloorToInt((float)pujcitSI);
            for (int i = 0; i < manager.PlatbyDohromady.Length; i++)
            {
                if (manager.PlatbyDohromady[i] == null)
                {
                    manager.PlatbyDohromady[i] = Instantiate(manager.novaplatba, manager.platbyContent);
                    manager.PlatbyDohromady[i].GetComponent<Platba>().PriteluvDluh(Mathf.FloorToInt((float)vratitKolik), vratitZa);
                    manager.PlatbyDohromady[i].name = "platba" + i;
                    i = manager.PlatbyDohromady.Length;
                    manager.BytMenuPromene();
                    PodtextText.text = "SUPER. VRAT SE ZA " + manager.DalsiPujckaOdPriteleZa + " DNY, PAK ZASE PUJCIM.";
                    manager.ZapujcenoOdPritele = true;
                    spravnaPujcka = false;
                }
            }
        }
        else { PodtextText.text = "NEVIM CO CHCES, KAMARADE"; }
    }
    public void Cas()
    {
        vratitKolik = Mathf.FloorToInt((float)((pujcitSI / 100) * 21));
        if(pujcitSI > 0 && pujcitSI < 199) { vratitZa = 1;}
        else if (pujcitSI >= 200 && pujcitSI < 449) { vratitZa = 2;}
        else if (pujcitSI >= 450 && pujcitSI < 799) { vratitZa = 3;}
        else if (pujcitSI >= 800 && pujcitSI < 1401) { vratitZa = 4;}
        else { vratitZa = 0; vratitKolik = 0; }
    }
    public void PodTextPriPrichodu()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();

        if (manager.DalsiPujckaOdPriteleZa == 0)
        { PodtextText.text = "JE LIBO PUJCKA?"; }
        else
        { PodtextText.text = "VRAT SE ZA " + manager.DalsiPujckaOdPriteleZa + " DNY, PAK ZASE PUJCIM."; }
    }
}
