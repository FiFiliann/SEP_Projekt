using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Podvod : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public manager manager;
    public GameObject LizKaretObj;
    public LizaniKaret LizKaret;

    public GameObject popisOkenko;
    public GameObject ObsahPopisu;
    public GameObject ObsahNazev;
    //--//
    public int cisloButonu;
    public int potrebnaReputace;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        LizKaret = LizKaretObj.GetComponent<LizaniKaret>();
        popisOkenko.SetActive(false);

    }
    public void Zaplaceni()
    {
        if (manager.reputace >= potrebnaReputace)
        {
            if(cisloButonu != 0) 
            {
                if (manager.koupenaDovednosti[cisloButonu - 1] == true)
                { 
                    Debug.Log("jE TO TADY");
                    gameObject.GetComponent<Button>().interactable = false;
                    manager.koupenaDovednosti[cisloButonu] = true;
                    
                    KoupenyPodvod();
                } 
            }
            else {gameObject.GetComponent<Button>().interactable = false; manager.koupenaDovednosti[cisloButonu] = true; KoupenyPodvod(); }
        }
    }
    public void KoupenyPodvod()
    {
        switch(cisloButonu)
        {
            case 0: manager.KartaVRukavuKoupeno = true; break;
            case 1: manager.Kecanikoupeno = true; break;
            case 2: LizKaret.hodnotaZvetseniPodezreni = 4;  break;
            case 3: LizKaret.CekaniSlider.maxValue = 300;  break;
            default: break;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        popisOkenko.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popisOkenko.SetActive(false);
    }
}
