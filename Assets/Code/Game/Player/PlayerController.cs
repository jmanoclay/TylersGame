using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerDeck Deck;
    public PlayerHand Hand;

    public bool DebugPopulateDeck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DebugPopulateDeck)
        {
            Deck.DebugPopulateDeck();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
