using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerDeck", menuName = "Scriptable Objects/PlayerDeck")]
public class PlayerDeck : ScriptableObject
{
    public int DeckSize;
    public List<Tile> Tiles;

    public void DebugPopulateDeck()
    {
        for(int i = 0; i < DeckSize; i++) 
        {
            Tiles.Add(TilePool.Instance.GetTile());
        }
    }

    public Tile GetTile()
    {
        if (Tiles.Count > 0)
        {
            Tile tileToGet = Tiles[0];
            Tiles.Remove(tileToGet);

            return tileToGet;
        }

        return null;
    }
}