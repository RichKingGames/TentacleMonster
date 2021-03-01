using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Class  which contain all saves in JSON.
/// </summary>
public class JsonLibrary
{
	private const int TotalLevels = 1;
	private const string FileName = "JsonLibrary.json";
	private string _fullFileName;
	public JsonLibrary(string savePath)
	{
		_fullFileName = Path.Combine(savePath, FileName);
	}

	/// <summary>
	/// Method that write in JSON file.
	/// </summary>
	public void Write(Info data)
	{
		InfoSerializer serializer = new InfoSerializer();
		using (StreamWriter sw = new StreamWriter(_fullFileName))
		{
			serializer.SerializeToFile(sw, data);
		}
	}

	/// <summary>
	/// Method that Read from JSON file.
	/// </summary>
	public Info Read()
	{
		if (!File.Exists(_fullFileName)) // Checkin if we have json file.
		{
			NewPlayer();
		}
		InfoSerializer serializer = new InfoSerializer();
		Info data = null;
		using (StreamReader sr = new StreamReader(_fullFileName))
		{
			data = serializer.DerializeFromFile(sr);
		}
		return data;
	}


	/// <summary>
	/// Method that create all information about levels, progress etc.
	/// </summary>
	public void NewPlayer()
	{
		List<string> levels = new List<string>();
		List<bool> isDone = new List<bool>();
		for(int i =0; i < TotalLevels; i++)
		{
			levels.Add("Prefabs/Levels/Level" + i.ToString());
			isDone.Add(false);
		}
		
		Info data = new Info(levels,isDone);

		Write(data);
	}

	/// <summary>
	/// Method that check what is current level(no completed)
	/// </summary>
	public string GetLevelPrefabName()
	{
		NewPlayer();
		Info data = Read();
		for(int i = 0; i< data.IsLevelDone.Count; i++)
		{
			if(!data.IsLevelDone[i])
			{
				return data.LevelPrefabPath[i];
			}
			
		}
		return null;
	}

}
