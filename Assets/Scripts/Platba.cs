using TMPro;
using UnityEngine;

public class Platba : MonoBehaviour
{
    public manager manager;
    public TextMeshProUGUI cena;
    public TextMeshProUGUI duvod;
    public int _cena;
    public string _duvod;
    string[] duvodString = {"potrava","noviny","nájemné","dluh","podíl" };
    int[] cenaInt = {50,100,150,200,500};
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        cena = transform.Find("Cena").GetComponent<TextMeshProUGUI>();
        duvod = transform.Find("Duvod").GetComponent<TextMeshProUGUI>();
        _cena = UrceniCeny();
        _duvod = UrceniDuvodu();
        cena.text = _cena + " KÈ";
        duvod.text = _duvod;
    }

    public void Zaplaceni()
    {
        _cena = UrceniCeny();
        if (manager.penize >= _cena)
        {
            manager.penize -= _cena;
            Destroy(gameObject);
            manager.BytMenuPromene();
        }
    }
    string UrceniDuvodu()
    {
        int i = Random.Range(0, duvodString.Length);
        return duvodString[i];
    }
    int UrceniCeny()
    {
        int i = Random.Range(0, cenaInt.Length);
        return cenaInt[i];
    }
}
