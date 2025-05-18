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
    private string[] textHrac = { "Tak jak jste se dostali do hazardu vy?", "Verili byste tomu, ze nam zabili Ferdinanda?", "Jak znacku cigaret kurite vy, panove","Taky mate dluhy u mafie?" };
    private string[] textOponent = { "Tohle je ovsem velice humorne", "Nekecej! Sam se musim soustredit", "Ja mam dluhy u mafia 2", "Coze?" };
    public string TohleJe;

    //CEKANI BAR
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
        LizKaret = GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>();

    }
    public void TextProHrace()
    {
        int i = Random.Range(0, textHrac.Length);
        HracDialogText.text = textHrac[i];
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
                if(TohleJe == "konec") { pohyb = false; StartCoroutine(OdpovediOponenti()); }
                else if(TohleJe == "Oponent") {Destroy(gameObject); }
            }
        }

    }
    private IEnumerator Pockat()
    {
        yield return new WaitForSeconds(2);
        rychlost = -10;

        //ODEBRANI PODEZRENI
        if (TohleJe == "Hrac")
        {
            int randomPodezreni = UnityEngine.Random.Range(1, 3);
            LizKaret.PodezreniSlider.value -= 1;
            TohleJe = "konec";

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
        Destroy(gameObject);
    }
}
