using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonLibrary
{
	private const int TotalLevels = 1;
	private const string FileName = "JsonLibrary.json";
	private string _fullFileName;
	public JsonLibrary(string savePath)
	{
		_fullFileName = Path.Combine(savePath, FileName);
	}
	public void Write(Info data)
	{
		InfoSerializer serializer = new InfoSerializer();
		using (StreamWriter sw = new StreamWriter(_fullFileName))
		{
			serializer.SerializeToFile(sw, data);
		}
	}
	public Info Read()
	{
		if (!File.Exists(_fullFileName))
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
