using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


/// <summary>
/// The class that spawns levels and initializes objects
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static Vector3 StartPoint = new Vector3(-3, 0, -3); // Start Point of Tentacle destination
    public static Vector3 ExitPoint; // Humans try to escape in this Point 

    public Level CurrentLevel;
    public UiManager Ui;
    public TentacleController Tentacle; // Player
    public NavMeshSurface navMeshSurface;

    void Start()
    {
        InitLevel();
    }

    /// <summary>
    /// The method which initializes current level.
    /// </summary>
    void InitLevel()
    {
        GameObject level = Instantiate(Resources.Load<GameObject>(ProgressManager.Json.GetLevelPrefabName()), new Vector3(), new Quaternion()); // Instantiate prefab of the level.
        CurrentLevel = level.GetComponent<Level>();

        ExitPoint = CurrentLevel.RoomEnvironment.ExitPoint.transform.position;

        navMeshSurface.BuildNavMesh();

        Tentacle.Init(this, CurrentLevel.ActiveHumans, CurrentLevel.StaticHumans, CurrentLevel.RoomEnvironment.StartPoint.transform.position); // Initialize player


        // Pass current LevelManager to all humans
        for(int i =0; i<CurrentLevel.ActiveHumans.Count; i++)
        {
            CurrentLevel.ActiveHumans[i].Init(this);
        }
        for (int i = 0; i < CurrentLevel.StaticHumans.Count; i++)
        {
            CurrentLevel.StaticHumans[i].Init(this);
        }

    }

    /// <summary>
    /// This method invokes if level complete (All humans were eaten)
    /// </summary>
    public void MissionComplete()
    {
        Ui.EnableCompletePanel();
    }

    /// <summary>
    /// This method is invokes if the human has reached the exit
    /// </summary>
    public void GameOver()
    {
        Ui.EnableGameOverPanel();

        Destroy(CurrentLevel.gameObject);

        InitLevel(); // Updating the level
    }

    /// <summary>
    /// This method is invokes if the human were eaten. Updating Lists of Active and Static Humans
    /// </summary>
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
        Tentacle.SetHumans(CurrentLevel.ActiveHumans, CurrentLevel.StaticHumans); // Pass new Lists to player script
    }
    
}
