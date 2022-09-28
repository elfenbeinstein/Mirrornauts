using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHit()
    {
        Debug.Log("spaceship touched border");
    }

    public void PlayerOutOfBounds()
    {
        Debug.Log("spaceship out of bounds");
    }
}
