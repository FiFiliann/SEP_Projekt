using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Podvod : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public manager manager;
    public GameObject popisOkenko;
    public GameObject ObsahPopisu;
    public GameObject ObsahNazev;
    //--//
    public int cisloButonu;
    public int potrebnaReputace;
    //public string popisText;
    //public string nazevText;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        popisOkenko.SetActive(false);
        //ObsahNazev.GetComponent<TextMeshProUGUI>().text = nazevText;
        //ObsahPopisu.GetComponent<TextMeshProUGUI>().text = popisText;
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
            case 1: manager.Kecanikoupeno = true; Debug.Log("1"); break;
            case 2: manager.Kecanikoupeno = true; Debug.Log("2"); break;
            case 3: manager.Kecanikoupeno = true; Debug.Log("3"); break;
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
