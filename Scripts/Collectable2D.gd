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
		if not collectors.get(body.name,false):
			collectors[body.name] = true
			print(str(body.name, " collected ",name))
			# TODO: give squito energy
		else:
			print(str(body.name," has already collected this ",name))
