using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OponentUStolu : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public TextMeshProUGUI PocetKaret;
    public GameObject start;
    public GameObject konec;
    public GameObject Karta;
    public GameObject OdhozenaKarta;
    public GameObject Stul;
    //public Karta kartasc;
    private bool packa = false;
    private float cas = 0;
    private void Start()
    {
        if (gameObject.name == "Oponent3" || gameObject.name == "Oponent4")
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1); 
            PocetKaret.transform.localScale = new Vector3(-1, 1, 1);
        }
        konec = GameObject.Find("OdhozeneKarty");
        Stul = GameObject.Find("Stul");
        GameObject startovniBod = Instantiate(start, gameObject.transform);
        startovniBod.transform.position = this.transform.position;        
        start = startovniBod;
    }
    private void Update()
    {
        if (packa)
        {
            cas += Time.deltaTime;
            OdhozenaKarta.transform.position = Vector3.Lerp(start.transform.position, konec.transform.position, cas);
            OdhozenaKarta.transform.localScale = Vector3.Lerp(start.transform.localScale, konec.transform.localScale, cas);
            if (cas > 1) { packa = false;}
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(OdhozenaKarta != null) { Destroy(OdhozenaKarta); }
        OdhozenaKarta = Instantiate(Karta, Stul.transform);
        OdhozenaKarta.transform.position = this.transform.position;
        packa = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
            gameObject.transform.position += new Vector3(0, 0.5f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

            gameObject.transform.position -= new Vector3(0, 0.5f, 0);
    }
}
/*
void Shuffle<T>(T[] array)
{
    for (int i = array.Length - 1; i > 0; i--)
    {
        int randomIndex = Random.Range(0, i + 1);
        T temp = array[i];
        array[i] = array[randomIndex];
        array[randomIndex] = temp;
    }
}
*/