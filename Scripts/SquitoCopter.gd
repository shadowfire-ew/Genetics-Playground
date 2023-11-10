extends RigidBody2D

## number of times per second an action is taken
@export var frequency : float = 1
## hove far this copter moves
@export var step_size : float
## Speed (in pixels per second) at which copter takes steps
@export var speed : float
## number of directions this can face
@export_range(4,256) var directions : int = 4
## actions this instance will loop over
@export var genome : Array[int]
## Id for interraction for this instance
@export var id : String
var step : int = 0
var actions : Array[Callable] = [
	turn_left,
	turn_right,
	move_forward,
	wait
]
var _action_counter : float = 0
# 0=up,1=left,2=down,3=right
var _facing : int = 0
var _moving : bool = false
var _end_move : Vector2

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if _moving:
		global_position = global_position.move_toward(_end_move,speed)
		if global_position == _end_move:
			_moving = false
	else:
		_action_counter += delta*frequency
		if _action_counter >= 1:
			do_action()
			_action_counter = 0

func do_action():
	var action = genome[step]
	actions[action].call()
	step = (step + 1) % len(genome)

func turn_left():
	_facing = (_facing+1)%directions
	rotation = _facing_to_rotation()

func turn_right():
	_facing = (_facing+3)%4
	rotation = _facing_to_rotation()

func move_forward():
	print("Moving forward")
	_moving = true
	var move_dir = Vector2.UP * step_size
	move_dir = move_dir.rotated(_facing_to_rotation())
	_end_move = global_position+move_dir

func wait():
	print("Waiting")
	pass

func _facing_to_rotation():
	return _facing*PI*-2/directions
