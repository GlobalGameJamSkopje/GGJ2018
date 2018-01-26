using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    public Plain PlayerOne { get; private set; }
    public Plain PlayerTwo { get; private set; }

    public World()
    {
        PlayerOne = new Plain();
        PlayerTwo = new Plain();
    }
}
