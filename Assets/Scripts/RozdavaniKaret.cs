using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RozdavaniKaret : MonoBehaviour
{
     public List<string> balicek = new List<string>();
    int vyberJedna;
    int vyberDva;

    void Start()
    {
        balicek.Add("♣1-");
        balicek.Add("♣2-");
        balicek.Add("♣3-");
        balicek.Add("♣4-");
        balicek.Add("♣5-");
        balicek.Add("♣6-");
        balicek.Add("♣7-");
        balicek.Add("♣8-"); 
        balicek.Add("♣9-");
        balicek.Add("♣10");
        balicek.Add("♣J-");
        balicek.Add("♣Q-");
        balicek.Add("♣K-");

        balicek.Add("♦1-");
        balicek.Add("♦2-");
        balicek.Add("♦3-");
        balicek.Add("♦4-");
        balicek.Add("♦5-");
        balicek.Add("♦6-");
        balicek.Add("♦7-");
        balicek.Add("♦8-");
        balicek.Add("♦9-");
        balicek.Add("♦10");
        balicek.Add("♦J-");
        balicek.Add("♦Q-");
        balicek.Add("♦K-"); 

        balicek.Add("♥1-");
        balicek.Add("♥2-");
        balicek.Add("♥3-");
        balicek.Add("♥4-");
        balicek.Add("♥5-");
        balicek.Add("♥6-");
        balicek.Add("♥7-");
        balicek.Add("♥8-");
        balicek.Add("♥9-");
        balicek.Add("♥10");
        balicek.Add("♥J-");
        balicek.Add("♥Q-");
        balicek.Add("♥K-"); 

        balicek.Add("♠1-");
        balicek.Add("♠2-");
        balicek.Add("♠3-");
        balicek.Add("♠4-");
        balicek.Add("♠5-");
        balicek.Add("♠6-");
        balicek.Add("♠7-");
        balicek.Add("♠8-");
        balicek.Add("♠9-");
        balicek.Add("♠10");
        balicek.Add("♠J-");
        balicek.Add("♠Q-");
        balicek.Add("♠K-");

        balicek.Add("J");
        balicek.Add("J");
        for (int i = 0; i < balicek.Count; i++)
        {
            Debug.Log(balicek[i]);
        }
        for (int i = 0; i <50;  i++)
        {
            vyberJedna = Random.Range(0, balicek.Count);
            vyberDva = Random.Range(0, balicek.Count);
            string podrz = balicek[vyberJedna];
            balicek[vyberJedna] = balicek[vyberDva];
            balicek[vyberDva] = podrz;
        }

        Debug.Log("Zamichani");
        for (int i = 0; i < balicek.Count; i++)
        {
            Debug.Log(balicek[i]);
        }
    }
    public void PromichaniKaret()
    {


    }
}
