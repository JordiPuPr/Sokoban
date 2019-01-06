using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    public LevelData data = new LevelData();

    public int[] tiles;
    public int[] lastPositionPlayer;
    public string fileName;

    public void StoreData()
    {
        data.tiles = tiles;
        data.lastPositionPlayer = lastPositionPlayer;
        data.fileName = fileName;
    }

    public void LoadData()
    {
        fileName = data.fileName;
        lastPositionPlayer = data.lastPositionPlayer;
        tiles = data.tiles;
    }
}

[Serializable]
public class LevelData
{
    public int[] tiles;

    public int[] lastPositionPlayer;

    public string fileName;
}
