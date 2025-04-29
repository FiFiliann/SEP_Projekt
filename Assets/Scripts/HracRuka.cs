using System.Collections.Generic;
using UnityEngine;

public class HracRuka : MonoBehaviour
{
    public List<string> HracKarty = new List<string>();
    public LizaniKaret LizKaret;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LizKaret = GameObject.Find("LizaciBalicek").GetComponent<LizaniKaret>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
