extends RigidBody2D

var _anim_sprite : AnimatedSprite2D

# Called when the node enters the scene tree for the first time.
func _ready():
	_anim_sprite = $AnimatedSprite2D
	_anim_sprite.play("default")


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
