using UnityEngine;

public class OpponentSpawner : MonoBehaviour
{
    public GameObject[] opponentPrefabs; // Pokud chceš rùzné postavy, jinak jen GameObject opponentPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        Shuffle(spawnPoints);         // Zamíchá pozice
        Shuffle(opponentPrefabs);     // (Volitelné) zamíchá i výbìr postav

        SpawnOpponents();
    }

    void SpawnOpponents()
    {
        int count = Mathf.Min(spawnPoints.Length, opponentPrefabs.Length);

        for (int i = 0; i < count; i++)
        {
            Instantiate(opponentPrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
        }
    }

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
}

