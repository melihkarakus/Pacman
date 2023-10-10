using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duraction = 8.0f;

    protected override void Eat()
    {
        FindAnyObjectByType<GameManager>().PowerPelletEaten(this);
    }
}
