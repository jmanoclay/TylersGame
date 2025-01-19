using UnityEngine;
using System.Collections.Generic;

public class TilePool : MonoBehaviour
{
    public int PoolSize = 100;
    private List<Tile> _tiles;
    public GameObject TileObject;

    // Prevent external instantiation
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Enforce singleton behavior
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateTilePool();
    }

    void CreateTilePool()
    {
        _tiles = new List<Tile>();
        for(int i = 0; i < PoolSize; i++)
        {
            GameObject newTile = Instantiate(TileObject) as GameObject;
            newTile.transform.parent = transform;
            newTile.transform.localPosition = Vector3.zero;
            newTile.gameObject.SetActive(false);
            _tiles.Add(newTile.GetComponent<Tile>());
        }
    }

    public Tile GetTile()
    {
        if(_tiles.Count > 0)
        {
            Tile tileToGet = _tiles[0];
            _tiles.Remove(tileToGet);

            return tileToGet;
        }
        
        return null;
    }


    private static TilePool _instance;

    // Lock object for thread safety
    private static readonly object _lock = new object();

    public static TilePool Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        // Try to find an existing instance in the scene
                        _instance = FindObjectOfType<TilePool>();

                        // If none exists, create a new one
                        if (_instance == null)
                        {
                            GameObject singletonObject = new GameObject("SingletonExample");
                            _instance = singletonObject.AddComponent<TilePool>();

                            // Ensure the instance persists between scenes
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                }
            }

            return _instance;
        }
    }
}
