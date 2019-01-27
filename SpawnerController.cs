using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    public string spawnerTag;
    public List<GameObject> listPrefab;

    void Start()
    {
        var listSpawner = GameObject.FindGameObjectsWithTag(spawnerTag);
        foreach (var spawner in listSpawner)
        {
            int index = Random.Range(0, listPrefab.Count - 1);
            Instantiate(listPrefab[index], spawner.transform.position, spawner.transform.rotation);
        }
    }

}