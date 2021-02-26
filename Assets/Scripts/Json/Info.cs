using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info
{
    public List<string> LevelPrefabPath;
    public List<bool> IsLevelDone;

    public Info(List<string> prefabPath,List<bool> isLevelDone)
    {
        LevelPrefabPath = prefabPath;
        IsLevelDone = isLevelDone;
    }

}
