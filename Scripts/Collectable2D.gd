extends Area2D

var collectors = {}

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_body_entered(body):
	if body.is_in_group("Squitos"):
		if not collectors.get(body.id,false):
			collectors[body.id] = true
			print(str(body.id, "  collected ",name))
			# TODO: give squito energy
