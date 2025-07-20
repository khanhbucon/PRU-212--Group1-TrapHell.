
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTrapReset : MonoBehaviour
{
    private Tilemap tilemap;
    private Dictionary<Vector3Int, TileBase> originalTiles = new();

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                originalTiles[pos] = tile;
            }
        }
    }

    private void OnEnable()
    {
        PlayerControllerr.OnPlayerDeath += ResetTilemap;
    }

    private void OnDisable()
    {
        PlayerControllerr.OnPlayerDeath -= ResetTilemap;
    }

    private void ResetTilemap()
    {
        tilemap.ClearAllTiles();

        foreach (var pair in originalTiles)
        {
            tilemap.SetTile(pair.Key, pair.Value);
        }

        Debug.Log("Tilemap Trap đã được reset!");
    }
}
