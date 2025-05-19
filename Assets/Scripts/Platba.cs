using TMPro;
using UnityEngine;

public class Platba : MonoBehaviour
{
    public manager manager;
    public TextMeshProUGUI cena;
    public TextMeshProUGUI duvod;
    public TextMeshProUGUI cas;
    public int _cena;
    public int _cas;
    public string _duvod;
    string[] duvodString = {"potrava","najemne","dluh","podil" };
    int[] cenaInt = {50,100,150,200};
    
    private void Odkazy()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        cena = transform.Find("Cena").GetComponent<TextMeshProUGUI>();
        duvod = transform.Find("Duvod").GetComponent<TextMeshProUGUI>();
        cas = transform.Find("Cas").GetComponent<TextMeshProUGUI>();
    }
    public void VedlejsiDluh()
    {
        Odkazy();

        _cena = UrceniCeny();
        _duvod = UrceniDuvodu();
        _cas = UrceniCasu();

        cena.text = _cena + "";
        duvod.text = _duvod;
        cas.text = _cas + "";
    }
    public void PribehovyDluh()
    {
        Odkazy();

        _cena = 3000;
        _duvod = "DLUH// MAFIA";
        _cas = 13;

        cena.text = _cena + "";
        duvod.text = _duvod;
        cas.text = _cas + "";
    }

    public void PriteluvDluh(int a, int b)
    {
        Odkazy();

        _cena = a;
        _duvod = "PRITELUV DLUH";
        _cas = b;

        cena.text = _cena + "";
        duvod.text = _duvod;
        cas.text = _cas + ""; ;
    }
    public void Zaplaceni()
    {
        if (manager.penize >= _cena)
        {
            manager.penize -= _cena;
            if(GameObject.Find("NzDh") != null )
            { 
                Destroy(manager.PlatbyDohromady[GameObject.Find("NzDh").GetComponent<NezaplacenyDluh>().cisloDluhu]);
                Destroy(GameObject.Find("NzDh"));             
            }            
            if(gameObject == manager.PlatbyDohromady[0])
            {
                manager.WinScreen.SetActive(true);
            }
            Destroy(gameObject);
            manager.BytMenuPromene();
        }
    }    
    public void KonecHry()
    {
        cas.text = _cas + "";
        if (_cas == 0)
        { manager.GameOverScreen.SetActive(true); }
    }
    string UrceniDuvodu()
    {
        int i = Random.Range(0, duvodString.Length);
        return duvodString[i];
    }
    int UrceniCasu()
    {
        return Random.Range(1, 4);
    }
    int UrceniCeny()
    {
        int i = Random.Range(0, cenaInt.Length);
        return cenaInt[i];
    }

}
