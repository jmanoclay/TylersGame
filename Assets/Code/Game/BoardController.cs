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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateBoardSlots();
    }

    void CreateBoardSlots()
    {
        for(int i = 0; i < BoardLength; i++)
        {
            for (int j = 0; j < BoardWidth; j++)
            {
                GameObject newSlot = Instantiate(BoardSlotObject);
                float slotPosX = i * SlotScale;
                float slotPosY = 0;
                float slotPosZ = j * SlotScale;
                Vector3 slotPos = new Vector3(slotPosX, slotPosY, slotPosZ);
                newSlot.transform.position = slotPos;
                newSlot.name = $"Slot {i} _ {j}";
                newSlot.transform.parent = transform;
            }
        }
    }

    public Tile CurrentTile;
    public bool ControllingTile;

    public int CurrTileX;
    public int CurrTileY;

    // Update is called once per frame
    void Update()
    {
        //TODO I don't like putting the constraints here, but we have to pre-empt any board positioning here
        // if we don't want board position to mismatch tile position... Later we must confirm the tile is always in the 
        // right board slot
        if (!ControllingTile && !CurrentTile.IsMoving && !CurrentTile.IsRotating)
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
    }

    void MoveTile(int dirX, int dirY)
    {
        CurrTileX += dirX;
        CurrTileY += dirY;

        Vector3 SlotPos = new Vector3(CurrTileX * SlotScale, BoardTileHeight, CurrTileY * SlotScale);
        CurrentTile.MoveToSlot(SlotPos);
    }

    void RotateTile(int dir)
    {
        CurrentTile.Rotate(dir);
    }
}
