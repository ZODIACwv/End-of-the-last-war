using Godot;
using System.ComponentModel;

namespace System.Collections.Generic
{
	public class MultiDictionary<TValue, TKey> : IEnumerable
	{
		readonly Dictionary<TValue, List<TKey>> multiDictionary = new();

		[Description("Getting the lines quantity in the MultiDictionary")]
		public int Length
		{
			get => multiDictionary.Count;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		
		public IEnumerator GetEnumerator() => multiDictionary.GetEnumerator();

		[Description("Checking for a key exist")]
		public bool IsKeyExist(TKey key)
		{
			foreach (KeyValuePair<TValue, List<TKey>> multiDictionaryElement in multiDictionary)
				foreach (TKey listElement in multiDictionaryElement.Value)
					if (listElement.Equals(key))
						return true;
			return false;
		}

		[Description("Adding value without checking if key already exist")]
		public void Add(TValue value, IEnumerable<TKey> keys)
		{
			List<TKey> list = new();
			foreach (TKey key in keys)
				list.Add(key);
			multiDictionary.Add(value, list);
		}

		[Description("Adding value with checking if key already exist")]
		public void SafeAdding(TValue value, IEnumerable<TKey> keys)
		{
			List<TKey> list = new();
			foreach (TKey key in keys)
				if (!IsKeyExist(key))
					list.Add(key);
				else
					GD.PrintErr($"MultiDictionary: error while adding the dictionary line: key {key.ToString} already exist");
			multiDictionary.Add(value, list);
		}

		[Description("Removes the key from line of MultiDictionary")]
		public void RemoveKey(TKey key)
		{
			foreach (var multiDictionaryElement in multiDictionary)
				for (int i = 0; i < multiDictionaryElement.Value.Count; i++)
					if (multiDictionaryElement.Value[i].Equals(key))
					{
						multiDictionaryElement.Value.RemoveAt(i);
						return;
					}
		}

		[Description("Removes the line of MultiDictionary by key")]
		public void Remove(TKey key)
		{
			foreach (var multiDictionaryElement in multiDictionary)
				for (int i = 0; i < multiDictionaryElement.Value.Count; i++)
					if (multiDictionaryElement.Value[i].Equals(key))
					{
						multiDictionary.Remove(multiDictionaryElement.Key);
						return;
					}
		}

		[Description("Adding key to line by other key")]
		public void AddKey(TKey key, TKey addedKey)
		{
			foreach (KeyValuePair<TValue, List<TKey>> multiDictionaryElement in multiDictionary)
				foreach (TKey listElement in multiDictionaryElement.Value)
					if (listElement.Equals(addedKey))
					{
						GD.PrintErr($"MultiDictionary: error while adding the key: key {addedKey.ToString} already exist");
						break;
					}
			foreach (KeyValuePair<TValue, List<TKey>> multiDictionaryElement in multiDictionary)
				for (int i = 0; i < multiDictionaryElement.Value.Count; i++)
					if (multiDictionaryElement.Value[i].Equals(key))
						multiDictionaryElement.Value.Add(addedKey);

		}

		[Description("Getting value with checking if key already exist")]
		public TValue TryGetValue(TKey key)
		{
			foreach (KeyValuePair<TValue, List<TKey>> multiDictionaryElement in multiDictionary)
				foreach (TKey listElement in multiDictionaryElement.Value)
					if (listElement.Equals(key))
						return multiDictionaryElement.Key;
			GD.PrintErr($"MultiDictionary: the element with key {key.ToString()} not found");
			return default;
		}

		[Description("Getting list of all of keys in MultiDictionary")]
		public IEnumerable<TKey> GetAllKeys()
		{
			foreach (KeyValuePair<TValue, List<TKey>> multiDictionaryElement in multiDictionary)
				foreach (TKey listElement in multiDictionaryElement.Value)
					yield return listElement;
		}

		[Description("Printing all values with all keys of them")]
		public void PrintAll()
		{
			foreach (KeyValuePair<TValue, List<TKey>> multiDictionaryElement in multiDictionary)
			{
				GD.Print($"value: {multiDictionaryElement.Key}");
				foreach (TKey listElement in multiDictionaryElement.Value)
					GD.Print($"\t-key: {listElement}");
			}
		}
	}
}
