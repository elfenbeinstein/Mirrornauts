using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAnim : MonoBehaviour
{
    [SerializeField] private SpaceshipBehaviour spaceship;

    public void MoveShip()
    {
        spaceship.MoveAfterAnim();
    }
}
