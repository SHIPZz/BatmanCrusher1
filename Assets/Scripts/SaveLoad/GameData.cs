using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.Serialization;

[Serializable, Preserve]
public class GameData
{
    [field: Preserve]
    public float Volume = 0.1f;
    [field: Preserve]
    public int Level = 1;
    [field: Preserve]
    public int Money;
    [field: Preserve]
    public int ImageId = 1;
    [field: Preserve]
    public int EnemyCount;
    [field: Preserve]
    public int ChosenCharacter = 0;
    [field: Preserve] 
    public string CharactersKey = "CharactersId";

    [field: Preserve] 
    public List<int> CharactersId = new List<int>();
}