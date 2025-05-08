using NUnit.Framework;
using UnityEngine;

public class NezaplacenyDluh : MonoBehaviour
{
    public GameObject NezaplacenyDluhPoloha;
    public int cisloDluhu = 1;
    public manager manager;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<manager>();
    }

    public void KonecHry()
    {
        manager.GameOverScreen.SetActive(true);        
        Destroy(gameObject);
    }
}
