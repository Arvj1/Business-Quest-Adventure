using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
public class LevelsDataSO : ScriptableObject
{
    public List<bool> unlockedLevels;
}
