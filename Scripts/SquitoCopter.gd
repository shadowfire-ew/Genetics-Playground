extends RigidBody2D

## number of times per second an action is taken
@export var frequency : float = 1
## force size for movement
@export var move_force : float
## number of directions this can face
@export_range(4,256) var directions : int = 4
## actions this instance will loop over
@export var genome : Array[int]
var step : int = 0
var actions : Array[Callable] = [
	wait,
	move_forward,
	turn_left,
	turn_right,
]
var _action_counter : float = 0
# 0=up,1=left,2=down,3=right
var _facing : int = 0
# the visual, this is rotated to indicate where the next force will push
@onready var sprite = get_node("Sprite")
@onready var moveForceVec = Vector2.UP*move_force
var move:Vector2 = Vector2()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	_action_counter += delta*frequency
	if _action_counter >= 1:
		do_action()
		_action_counter = 0

func _integrate_forces(state):
	state.apply_central_force(move)
	if move.length_squared() != 0:
		print(move)
		move = Vector2()

func do_action():
	var action = genome[step]
	actions[action].call()
	step = (step + 1) % len(genome)

func turn_left():
	_facing = (_facing+1)%directions
	sprite.rotation = _facing_to_rotation()

func turn_right():
	_facing = (_facing+3)%4
	sprite.rotation = _facing_to_rotation()

func move_forward():
	move = moveForceVec.rotated(_facing_to_rotation())

func wait():
	print("Waiting")
	pass

func _facing_to_rotation():
	return _facing*PI*-2/directions
