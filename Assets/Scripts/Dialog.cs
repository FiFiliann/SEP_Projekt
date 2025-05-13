using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    //KLASICKE ODKAZY
    public manager manager;
    public LizaniKaret LizKaret;
    //OPONENT
    public GameObject OponentDialog;
    public Image OponentDialogIkonka;
    public TextMeshProUGUI OponentDialogText;

    //HRAC
    public GameObject HracDialog;
    public Image HracDialogIkonka;
    public TextMeshProUGUI HracDialogText;

    //VARI
    bool pohyb = true;
    int rychlost = 10;
    private string[] textHrac = { "coze", "nene", "silene","humorne" };
    private string[] textOponent = { "Ale opravdu", "anoano", "smysluplne", "nepobavujici" };
    public string TohleJe;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        LizKaret = GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>();
        int i = Random.Range(0, textHrac.Length);
        HracDialogText.text = textHrac[i];
    }
    public void HracuvText()
    {

    }
    private void Update()
    {
        if(pohyb)
        {
            gameObject.transform.position += Vector3.left * Time.deltaTime * rychlost;

            if (gameObject.transform.position.x <= 5) 
            {
                rychlost = 0;
                StartCoroutine(Pockat());
            }
            if (gameObject.transform.position.x >= 14)
            {
                if(TohleJe == "Hrac") { pohyb = false; StartCoroutine(OdpovediOponenti()); }
                else if(TohleJe == "Oponent") {Destroy(gameObject); }
            }
        }

    }
    private IEnumerator Pockat()
    {
        yield return new WaitForSeconds(2);
        rychlost = -10;

        //ODEBRANI PODEZRENI
        if (LizKaret.aktualniPokusyPodvadeni != 0 && TohleJe == "Hrac")
        {
            LizKaret.aktualniPokusyPodvadeni--;
            float zvyseniPodezreni = LizKaret.PodezreniSlider.maxValue / 3f;
            LizKaret.PodezreniSlider.value = zvyseniPodezreni;//Mathf.Min(LizKaret.PodezreniSlider.maxValue, LizKaret.PodezreniSlider.value - zvyseniPodezreni);
        }
    }
    public IEnumerator OdpovediOponenti()
    {
        LizKaret.KecaniSpustene = false;
        for (int a = 0; a < manager.OponentiUStolu.Length; a++)
        {
            if (manager.OponentiUStolu[a] != null)
            {
                int i = Random.Range(0, textHrac.Length);
                manager.OponentiUStolu[a].GetComponent<OponentUStolu>().DialogOponent(textOponent[i]);
                yield return new WaitForSeconds(3f);
            }
        }
        GameObject.Find("KecaniButton").GetComponent<Button>().interactable = true;
        
        Destroy(gameObject);
    }
}
