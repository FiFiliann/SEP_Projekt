using UnityEngine;

public class spawn : MonoBehaviour
{
    [SerializeField] public int rychlost = 0;
    private bool coz = true;
    private void Start()
    {
        rychlost = 0;
    }
    public void SpusteniBytMenu()
    {
        if (rychlost == 0)
        {
            rychlost = 5;

            if (coz) { rychlost = 5;  }
            else { rychlost = -5;  }
        }
    }

    void Update()
    {
        gameObject.transform.position += Vector3.right * Time.deltaTime * rychlost;
        if(coz)
        {
            if (gameObject.transform.position.x >= -3.9f) { rychlost = 0;coz = false;  } 
        }
        else
        {
            if (gameObject.transform.position.x <= -14.5f) { rychlost = 0;coz = true; }
        }
    }
}
