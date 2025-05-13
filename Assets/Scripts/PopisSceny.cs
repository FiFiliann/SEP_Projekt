
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopisSceny : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public manager manager;
    public Image Image;
    //public GameObject[] Vlastnosti = new GameObject[4];
    public Sprite ScenaPNG;
    public GameObject OtevritScenuButton;
    public Slider reputaceBar;
    public double reputaceMax;
    //public Transform vlastnostiContent;
    public GameObject Okno;

    void Start()
    {
        Okno.SetActive(false);
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        Image.GetComponent<Image>().sprite = ScenaPNG;   
    }

    void Reputace()
    {
        double reputacePriprava = manager.reputace / reputaceMax;
        reputaceBar.value = (float)reputacePriprava;
    }
    public void ZmenaSceny(int a)
    {
        manager.novaScena = a; manager.rychlost = 5;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Okno.SetActive(true);
        Reputace();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Okno.SetActive(false);
    }
}
