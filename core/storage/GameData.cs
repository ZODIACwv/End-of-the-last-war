using Godot;
using Newtonsoft.Json;
using System;
using System.IO;

public class GameData
{
	readonly object _lock = new();

	// Settings
	bool RAM_SavingMode = false;

	public GameData() {}

	public void Load()
	{
		lock (_lock)
		{
			try
			{
				var data = JsonConvert.DeserializeObject<GameData>(File.ReadAllText("C:\\Projects\\Godot\\EndOfTheLastWar\\core\\storage\\gamedata.json"));
				if (data != null)
				{
					RAM_SavingMode = data.RAM_SavingMode;
				}
				else
					Save();
			}
			catch (Exception e)
			{
				GD.PushError($"Failed to load data. Error: {e.Message}");
			}
		}
	}

	public void Save()
	{
		lock (_lock)
		{
			try
			{
				var json = JsonConvert.SerializeObject(this, Formatting.Indented);
				File.WriteAllText("C:\\Projects\\Godot\\EndOfTheLastWar\\core\\storage\\gamedata.json", json);
			}
			catch (Exception e)
			{
				GD.PushError($"Failed to save data. Error: {e.Message}");
			}
		}
	}
}

public static class GameDataLoader
{
	public static GameData GamedataInstanse;
	public static void Load() => GamedataInstanse.Load();
	public static void Save() => GamedataInstanse.Save();
}
