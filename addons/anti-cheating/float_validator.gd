extends ac_validator

var _0: int = 0
var _1: int = 0
var _2: int = 0
var _3: float = 0

func with(value: float) -> ac_validator:
	_2 = Time.get_unix_time_from_system()
	_0 = int(value) - _2
	_1 = int(value) + _2
	_3 = _2 - value
	return self
	
func check(value: float) -> bool:
	return (_0 + _2) == int(value) and (_1 - _2) == int(value)
	
func source():
	return _2 - _3
	
