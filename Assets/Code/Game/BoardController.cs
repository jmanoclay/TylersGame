using System.Collections.Generic;
using System;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public float SlotScale = 2f;
    public float BoardTileHeight = 1f;
    public int BoardLength;
    public int BoardWidth;
    public GameObject BoardSlotObject;

    private List<GameObject> Slot;

    public Tile DebugTile;
    private Tile _activeTile;
    public bool ControllingTile;

    public int CurrTileX;
    public int CurrTileY;

    public bool Debug;
    public Transform BoardStartPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateBoardSlots();

        if (Debug)
        {
            SetNewActiveTile(DebugTile);
        }
    }

    void CreateBoardSlots()
    {
        for(int i = 0; i < BoardLength; i++)
        {
            for (int j = 0; j < BoardWidth; j++)
            {
                GameObject newSlot = Instantiate(BoardSlotObject);
                float slotPosX = BoardStartPoint.transform.position.x + (i * SlotScale);
                float slotPosY = BoardStartPoint.transform.position.y + 0;
                float slotPosZ = BoardStartPoint.transform.position.z + (j * SlotScale);
                Vector3 slotPos = new Vector3(slotPosX, slotPosY, slotPosZ);
                newSlot.transform.position = slotPos;
                newSlot.name = $"Slot {i} _ {j}";
                newSlot.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO I don't like putting the constraints here, but we have to pre-empt any board positioning here
        // if we don't want board position to mismatch tile position... Later we must confirm the tile is always in the 
        // right board slot
        if (!ControllingTile && !_activeTile.IsMoving && !_activeTile.IsRotating)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveTile(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveTile(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveTile(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveTile(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateTile(-1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotateTile(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceTile();
        }
    }

    void MoveTile(int dirX, int dirY)
    {
        CurrTileX += dirX;
        CurrTileY += dirY;

        Vector3 SlotPos = new Vector3(CurrTileX * SlotScale, BoardTileHeight, CurrTileY * SlotScale);
        _activeTile.MoveToSlot(SlotPos);
    }

    void RotateTile(int dir)
    {
        _activeTile.Rotate(dir);
    }

    void PlaceTile()
    {
        //Play Tile Logic Here
        //For now just drop it
        _activeTile = null;

        //For now, just get a new tile
        if (Debug)
        {
            DebugGetNewTile();
        }
    }

    private void DebugGetNewTile()
    {
        Tile newTile = TilePool.Instance.GetTile();
        newTile.gameObject.SetActive(true);
        SetNewActiveTile(newTile);
    }

    public void SetNewActiveTile(Tile tile)
    {
        CurrTileX = 0;
        CurrTileY = 0;

        _activeTile = tile;
        _activeTile.transform.position = BoardStartPoint.position + Vector3.up * BoardTileHeight;
    }
}
