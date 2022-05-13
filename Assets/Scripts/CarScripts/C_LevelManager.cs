using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_LevelManager : MonoBehaviour
{
    private List<GameObject> activeTiles = new List<GameObject>();
    private float zSpawn = 0f;

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject[] lastTilePrefab;
    [SerializeField] private float tileLenght = 30f;
    [SerializeField] private int numberOfTiles = 12;
    [SerializeField] private Transform player;


    void Start()
    {
      for(int i = 0; i < numberOfTiles; i++) 
      {
            if (i == 0) 
            {
                SpawnTile(0);
            }
            if (i == 1)
            {
                SpawnTile(1);
            }
            if (i == 2)
            {
                SpawnTile(1);
            }
            else
            {
                SpawnTile(Random.Range(1, tilePrefabs.Length));
            }
        }
    }

    void Update()
    {
        if(player.position.z-35>zSpawn-(numberOfTiles * tileLenght)) 
        {
            SpawnTile(Random.Range(1, tilePrefabs.Length));
            DeleteTile();
        }
    }
    
    public void SpawnTile(int tileIndex) 
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLenght;
    }

    private void DeleteTile() 
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
