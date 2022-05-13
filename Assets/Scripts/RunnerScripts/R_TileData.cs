using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class R_TileData : MonoBehaviour
{
    public enum Direction
    {
        North, East, South, West, None
    }
    
    public Vector2 tileSize = new Vector2(20f, 20f);
    public Direction entry;
    public Direction exit;
    public string tileName;  
}
