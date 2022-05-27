using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_LevelManager : MonoBehaviour
{
    private R_TileData lastTile;
    private R_TileData chosenTile;
    private R_TileData.Direction newStartDir;
    private Vector3 newPos;
    private float startTileCounter, removeTileTimer;
    private System.Random rnd = new System.Random();

    [SerializeField] private R_TileData[] listOfTiles;
    [SerializeField] private List<R_TileData> activeTiles = new List<R_TileData>();
    [SerializeField] private R_TileData startTile;
    [SerializeField] private Vector3 origin;
    [SerializeField] private R_PlayerScript player_script;

    // Generates 10 tiles at start.
    void Start()
    {
        newStartDir = R_TileData.Direction.South;
        lastTile = startTile;
        for (int i = 0; i < 10; i++)
        {
            GenerateTile();
        }
    }

    void Update()
    {
        removeTileTimer += Time.deltaTime;

        if (player_script.SpawnTile)
        {
            GenerateTile();
            player_script.SpawnTile = false;

        }
    }

    /// <summary>
    /// Checks the exit direction of the last tile and matches it with the next tiles entry direction.
    /// </summary>
    /// <returns></returns>
    private R_TileData ChooseTile()
    {
        List<R_TileData> possibleTiles = new List<R_TileData>();

        if (startTileCounter > 4)
        {
            switch (lastTile.exit)
            {
                case R_TileData.Direction.North:
                    newStartDir = R_TileData.Direction.South;

                    break;

                case R_TileData.Direction.East:
                    newStartDir = R_TileData.Direction.West;

                    break;

                case R_TileData.Direction.West:
                    newStartDir = R_TileData.Direction.East;

                    break;
                default:
                    break;
            }
        }
        else // Spawna 5 raka tiles vid start.
        {
            newStartDir = R_TileData.Direction.North;
            newPos += new Vector3(0, 0, lastTile.tileSize.y);

        }

        FindPossibleTiles(newStartDir, possibleTiles);

        return possibleTiles[rnd.Next(0, possibleTiles.Count)];
    }


    private void FindPossibleTiles(R_TileData.Direction newStartDir, List<R_TileData> possibleTiles)
    {
        foreach (R_TileData t in listOfTiles)
        {
            if (startTileCounter <= 4 && t.name == "South-North")
            {
                possibleTiles.Add(t);
                startTileCounter++;
            }
            else if (t.entry == newStartDir && t.tileName != lastTile.tileName)
            {
                if (!(t.gameObject.tag == "Bridge" && lastTile.gameObject.tag == "Turn"))
                {
                    possibleTiles.Add(t);
                }
                if (t.gameObject.tag == "Bridge")
                {

                }
            }
        }
    }
    private void GenerateTile()
    {
        chosenTile = ChooseTile();
        lastTile = chosenTile;
        activeTiles.Add(chosenTile);
        switch (newStartDir)
        {
            case R_TileData.Direction.South:
                newPos += new Vector3(0, 0, lastTile.tileSize.y);
                break;
            case R_TileData.Direction.East:
                newPos += new Vector3(-lastTile.tileSize.x, 0, 0);
                break;
            case R_TileData.Direction.West:
                newPos += new Vector3(lastTile.tileSize.x, 0, 0);
                break;
            default:
                break;
        }
        Instantiate(chosenTile, newPos + origin, chosenTile.transform.rotation);
    }
}

