using NUnit.Framework;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static System.Net.Mime.MediaTypeNames;

public class vysledekPopUp : MonoBehaviour
{
    public LizaniKaret LizKaret;
    public GameObject zacatek;
    public GameObject stred;
    public GameObject konec;
    public UnityEngine.UI.Image jenTakProRadost;
    public TextMeshProUGUI text;
    private bool zacatek_stred = false;
    private bool stred_konec = false;
    private float cas = 0f;

    private void Start()
    {
        LizKaret = GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>();
    }
    // Update is called once per frame
    void Update()
    {
        if (zacatek_stred)//z ruky hráèe do odhazovacího balíèku
        {
            cas += Time.deltaTime * 1f;
            this.transform.position = Vector3.Lerp(zacatek.transform.position, stred.transform.position, cas);
            if (cas > 1) { StartCoroutine(Pauza());  }
        }
        if (stred_konec)//Z lizacího balíèku do ruky hráèe
        {
            cas += Time.deltaTime * 1f;
            this.transform.position = Vector3.Lerp(stred.transform.position, konec.transform.position, cas);
            if (cas > 1) { this.transform.position = zacatek.transform.position; stred_konec = false; cas = 0; }
        }
    }
    private IEnumerator Pauza()
    {
        zacatek_stred = false;
        yield return new WaitForSeconds(1);
        stred_konec = true;  cas = 0;
    }
    public IEnumerator Vysledky(int i, int j)
    {
        zacatek_stred = true;
        switch (i)
        {
            case 0:
                text.text = "VYHRA!";
                jenTakProRadost.color = new Color32(250, 255, 155, 255);
                yield return new WaitForSeconds(3.5f);
                LizKaret.HracVyhra();
                break;
            case 1:
                text.text = "PROHRA!";
                jenTakProRadost.color = new Color32(255, 0, 0, 255);
                yield return new WaitForSeconds(3.5f);
                StartCoroutine(LizKaret.OponentVyhra(j));
                break;
            case 2:
                text.text = "CHYCEN!";
                jenTakProRadost.color = new Color32(255, 0, 0, 255);
                yield return new WaitForSeconds(3.5f);
                LizKaret.RukavChycen();
                
                break;
            default:Debug.Log("chyba"); break;
        }
    }
}
