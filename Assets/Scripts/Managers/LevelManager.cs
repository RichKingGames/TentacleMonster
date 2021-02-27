using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level CurrentLevel;
    public static Vector3 StartPoint = new Vector3(-3,0,-3);
    public TentacleController Tentacle;
    void Start()
    {
        GameObject level = Instantiate(Resources.Load<GameObject>(ProgressManager.Json.GetLevelPrefabName()), new Vector3(), new Quaternion());
        CurrentLevel = level.GetComponent<Level>();
       
        Tentacle.Init(this, CurrentLevel.ActiveHumans, CurrentLevel.StaticHumans, CurrentLevel.RoomEnvironment.StartPoint.transform.position);
        
        
        //Utils.MakeLevel(1, Path.Combine(Application.persistentDataPath, "level1.json"));
        //CurrentLevel = Level.Read(Path.Combine(Application.persistentDataPath, "level1.json"));

        //for(int i = 0; i< CurrentLevel.ActiveHumans.Length; i++)
        //{
        //    Instantiate(Resources.Load<GameObject>(CurrentLevel.ActiveHumans[i].PrefabPath), CurrentLevel.ActiveHumans[i]._startPosition, new Quaternion());
        //}
    }
    public void DestroyHumans(GameObject human)
    {
        for (int i = 0; i < CurrentLevel.ActiveHumans.Count; i++)
        {
            if (CurrentLevel.ActiveHumans[i].gameObject == human)
            {
                CurrentLevel.ActiveHumans.Remove(CurrentLevel.ActiveHumans[i]);
            }
        }
        for (int i = 0; i < CurrentLevel.StaticHumans.Count; i++)
        {
            if (CurrentLevel.StaticHumans[i].gameObject == human)
            {
                CurrentLevel.StaticHumans.Remove(CurrentLevel.StaticHumans[i]);
            }
        }
        Tentacle.SetHumans(CurrentLevel.ActiveHumans, CurrentLevel.StaticHumans);
    }
    
}
