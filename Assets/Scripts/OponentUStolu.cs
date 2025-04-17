using TMPro;
using UnityEngine;

public class OponentUStolu : MonoBehaviour
{
    public TextMeshProUGUI PocetKaret;

    private void Start()
    {
        if (gameObject.name == "Oponent3" || gameObject.name == "Oponent4")
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1); 
            PocetKaret.transform.localScale = new Vector3(-1, 1, 1);
        }
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