extends RefCounted
class_name ac_dict

# The number of times interference data is generated; the higher the value, the lower the performance, but the better the effect.
var disturb: int = 0

# private
var _pool: Dictionary

func set_value(key: String, value: ac_value):
	_pool[key] = value
	# Generate interference data
	_do_disturb.call_deferred(value)
	
func get_value(key: String, default_value: ac_value = ac_value.new()) -> ac_value:
	if _pool.has(key):
		return _pool[key]
	return default_value
	
func has(key: String) -> bool:
	return _pool.has(key)
	
func keys() -> Array[String]:
	return _pool.keys()
	
func erase(key: String) -> bool:
	return _pool.erase(key)
	
func clear():
	_pool.clear()
	
func is_empty() -> bool:
	return _pool.is_empty()
	
func _do_disturb(value: ac_value):
	if not is_instance_valid(self):
		return
	for i in disturb:
		_pool["__ac_disturb:%d__" % i] = value.duplicate()
	