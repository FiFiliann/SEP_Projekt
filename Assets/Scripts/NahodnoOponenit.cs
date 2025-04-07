using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSpawner : MonoBehaviour
{
    public GameObject[] opponentPrefabs; // Seznam všech možných oponentù
    public Transform[] spawnPoints; // Místa, kde se oponenti objeví
    private List<int> usedIndexes = new List<int>(); // Seznam použitých indexù

    void Start()
    {
        if (spawnPoints.Length < 5)
        {
            Debug.LogError("Nedostatek spawn pointù! Potøebuješ alespoò 5.");
            return;
        }

        SpawnRandomOpponents();
    }

    void SpawnRandomOpponents()
    {
        List<int> availableOpponents = new List<int>();
        for (int i = 0; i < opponentPrefabs.Length; i++)
        {
            availableOpponents.Add(i);
        }

        for (int i = 0; i < 5; i++) // Spawn 5 oponentù
        {
            int randomOpponentIndex = availableOpponents[Random.Range(0, availableOpponents.Count)];
            availableOpponents.Remove(randomOpponentIndex); // Zabráníme opakování stejného oponenta

            int randomSpawnIndex;
            do
            {
                randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            } while (usedIndexes.Contains(randomSpawnIndex)); // Zabráníme opakování stejného místa

            usedIndexes.Add(randomSpawnIndex); // Uložíme použitou pozici

            GameObject chosenOpponent = Instantiate(opponentPrefabs[randomOpponentIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
            Debug.Log("Spawnován oponent: " + chosenOpponent.name);
        }
    }
}
