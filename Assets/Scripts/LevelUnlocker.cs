using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    public void UnlockLevel(int levelUnlock)
    {
        PersistingMenuScript.Instance.levelData.unlockedLevels[levelUnlock] = true;
    }
}
