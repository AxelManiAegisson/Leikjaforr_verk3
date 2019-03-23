using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    // Hvar á Z axisinum á að spawna tiles
    private float spawnZ = 0.0f;
    private float tileLength = 9.08f;
    //Hversu margir tiles eru á skjánum
    private int amntOfTiles = 15;
    private float safeSone = 15.0f;
    private int lastPrefab = 0;

    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < amntOfTiles; i++)
        {
            //Spawnar fyrsta tile
            if (i < 4)
                SpawnTile(0);
            else
                SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Spawnar tile í hvert skipi sem hann fer yfir eitt
        if(playerTransform.position.z - safeSone > (spawnZ - amntOfTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }
    // Spawnar tiles
    void SpawnTile(int prefabIndex = -1)
    {
        // passar að spawna base tile ekki random tile fyrst
        GameObject go;
        if (prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefab;
        while(randomIndex == lastPrefab)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefab = randomIndex;
        return randomIndex;
    }
}
