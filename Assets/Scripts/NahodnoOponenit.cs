using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSpawner : MonoBehaviour
{
    public GameObject[] opponentPrefabs; // Seznam v�ech mo�n�ch oponent�
    public Transform[] spawnPoints; // M�sta, kde se oponenti objev�
    private List<int> usedIndexes = new List<int>(); // Seznam pou�it�ch index�

    void Start()
    {
        if (spawnPoints.Length < 5)
        {
            Debug.LogError("Nedostatek spawn point�! Pot�ebuje� alespo� 5.");
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

        for (int i = 0; i < 5; i++) // Spawn 5 oponent�
        {
            int randomOpponentIndex = availableOpponents[Random.Range(0, availableOpponents.Count)];
            availableOpponents.Remove(randomOpponentIndex); // Zabr�n�me opakov�n� stejn�ho oponenta

            int randomSpawnIndex;
            do
            {
                randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            } while (usedIndexes.Contains(randomSpawnIndex)); // Zabr�n�me opakov�n� stejn�ho m�sta

            usedIndexes.Add(randomSpawnIndex); // Ulo��me pou�itou pozici

            GameObject chosenOpponent = Instantiate(opponentPrefabs[randomOpponentIndex], spawnPoints[randomSpawnIndex].position, Quaternion.identity);
            Debug.Log("Spawnov�n oponent: " + chosenOpponent.name);
        }
    }
}
