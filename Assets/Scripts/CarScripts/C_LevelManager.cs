using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_LevelManager : MonoBehaviour
{
    private List<GameObject> activeTiles = new List<GameObject>();
    private float zSpawn = 0f;

    public static bool gameOver;

    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject[] lastTilePrefab;
    [SerializeField] private float tileLenght = 30f;
    [SerializeField] private int numberOfTiles = 12;
    [SerializeField] private Transform player;


    void Start()
    {
       //Spawnar alltid en förbestämd första tile, sedan blir det random.
      for(int i = 0; i < numberOfTiles; i++) 
      {
            if (i == 0) 
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(1, tilePrefabs.Length));
            }
      }
    }

    void Update()
    {
        //Spawnar nya tiles inom en visst räckhåll till spelaren och tar bort de gamla.
        if(player.position.z-35>zSpawn-(numberOfTiles * tileLenght)) 
        {
            SpawnTile(Random.Range(1, tilePrefabs.Length));
            if (gameOver == false)
            {
                DeleteTile();
            }
        }
    }
    
    //Spawnar nya tiles framför dem gamla.
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
