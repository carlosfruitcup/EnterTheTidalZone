extends Node2D

var moveup = 0

onready var credits = get_node("RichTextLabel")

#func _ready():
#	var file = File.new()
#	var content
#	file.open("res://CREDITS.md", File.READ)
#	content = file.get_as_text(false)
#	file.close()
#	credits.text = content

func _process(delta):
	
	moveup -= 1.2
	self.set_position(Vector2(0,moveup))
	print(self.position)
