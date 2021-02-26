using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class InfoSerializer
{
	private JsonSerializer _serializer;
	public InfoSerializer()
	{
		_serializer = new JsonSerializer();
		_serializer.NullValueHandling = NullValueHandling.Include;
		_serializer.Formatting = Formatting.Indented;
	}
	public void SerializeToFile(StreamWriter sw, Info data)
	{
		using (JsonWriter writer = new JsonTextWriter(sw))
		{
			_serializer.Serialize(writer, data);
		}
	}
	public Info DerializeFromFile(StreamReader sr)
	{
		Info data = null;
		using (JsonTextReader reader = new JsonTextReader(sr))
		{
			data = _serializer.Deserialize<Info>(reader);
		}
		return data;
	}

}

