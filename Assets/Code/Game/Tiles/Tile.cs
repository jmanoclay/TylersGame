using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float TileMoveTime = 1f;
    public float TileRotateTime = .5f;
    private Coroutine _currMoveRoutine;
    private Coroutine _currRotationRoutine;
    private const float TILE_ROTATION_SNAP = 90f;

    private int _orientation = 0;
    private int minOrientation = 0;
    private int maxOrientation = 3;
    //0 = north, 1 = east, 2 = south, 3 = west

    public bool IsMoving;
    public bool IsRotating;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToSlot(Vector3 slotPos)
    {
        if (_currMoveRoutine != null)
        {
            StopCoroutine( _currMoveRoutine );
        }

        _currMoveRoutine = StartCoroutine(MoveToSlotRoutine(slotPos));
    }

    private IEnumerator MoveToSlotRoutine(Vector3 slotPos)
    {
        IsMoving = true;

        float moveTime = 0f;
        Vector3 toPos = new Vector3(slotPos.x, slotPos.y, slotPos.z);

        while (moveTime < TileMoveTime)
        {
            moveTime += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, toPos, moveTime /  TileMoveTime);

            yield return null;
        }

        transform.position = toPos;

        IsMoving = false;
    }

    public void Rotate(int dir)
    {
        if (_currRotationRoutine != null)
        {
            StopCoroutine(_currRotationRoutine);
        }

        _orientation += dir;
        if(_orientation < minOrientation)
        {
            _orientation = maxOrientation;
        }
        else if(_orientation > maxOrientation)
        {
            _orientation = minOrientation;
        }

        float toRot = TILE_ROTATION_SNAP * _orientation;
        Quaternion toRotQuat = Quaternion.Euler(0, toRot, 0);

        _currMoveRoutine = StartCoroutine(RotateTileRoutine(toRotQuat));
    }

    private IEnumerator RotateTileRoutine(Quaternion toRotQuat)
    {
        IsRotating = true;

        float rotateTime = 0f;

        while (rotateTime < TileRotateTime)
        {
            rotateTime += Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, toRotQuat, rotateTime / TileRotateTime);

            yield return null;
        }

        transform.rotation = toRotQuat;

        IsRotating = false;
    }
}
