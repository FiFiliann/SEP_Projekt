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
    public string popisText;
    public string nazevText;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        popisOkenko.SetActive(false);
        ObsahNazev.GetComponent<TextMeshProUGUI>().text = nazevText;
        ObsahPopisu.GetComponent<TextMeshProUGUI>().text = popisText;
    }
    public void Zaplaceni()
    {
        if (manager.reputace >= potrebnaReputace)
        {
            if(cisloButonu != 0) 
            {
                if (manager.koupenaDovednosti[cisloButonu - 1] == true)
                { 
                    gameObject.GetComponent<Button>().interactable = false;
                    manager.koupenaDovednosti[cisloButonu] = true;
                    OtevrenyPodvod();
                } 
            }
            else {gameObject.GetComponent<Button>().interactable = false; manager.koupenaDovednosti[cisloButonu] = true; }
        }
    }
    void OtevrenyPodvod()
    {
        switch(cisloButonu)
        {
            case 0: manager.KartaVRukavuKoupeno = true; break;
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
