using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes Sure Spaceship Moves Once Animation Is Over
/// Called From Animation Event
/// </summary>

public class SpaceshipAnim : MonoBehaviour
{
    [SerializeField] private SpaceshipBehaviour spaceship;

    public void MoveShip()
    {
        spaceship.MoveAfterAnim();
    }
}
