using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{

    public int numberMobMax = 8;
    public string spawnerTag;
    public List<GameObject> listPrefab;
    private int mobSpawned = 0;

    void Start()
    {
        var listSpawner = GameObject.FindGameObjectsWithTag(spawnerTag).ToList();
        var shuffleSpawners = listSpawner.OrderBy(a => Guid.NewGuid()).ToList();
        foreach (var spawner in shuffleSpawners)
        {
            if (mobSpawned < numberMobMax)
            {
                int index = Random.Range(0, listPrefab.Count - 1);
                Instantiate(listPrefab[index], spawner.transform.position, spawner.transform.rotation);
                mobSpawned++;
            }
            
        }
    }

}