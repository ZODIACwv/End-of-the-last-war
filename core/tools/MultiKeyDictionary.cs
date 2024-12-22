using System.Collections.Generic;

public class MultiKeyDictionary<TKey, TValue> {
	public Dictionary<TKey, TValue> keyValuePairs = new Dictionary<TKey, TValue>();

	public void Add(TValue value, params TKey[] keys) {
		foreach(TKey key in keys)
			keyValuePairs.Add(key, value);
	}

	public TValue Get(TKey key) {
		keyValuePairs.TryGetValue(key, out TValue value);
		return value;
	}
}
