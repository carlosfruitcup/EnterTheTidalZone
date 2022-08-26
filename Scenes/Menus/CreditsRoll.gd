extends Node2D

var moveup = 0

onready var credits = get_node("RichTextLabel")

func _ready():
	var file = File.new() #creates a new file instance
	var content #what will store the text from the file
	file.open("res://CREDITS.md", File.READ)
	content = file.get_as_text(false) #disables return characters whatever that is
	file.close()
	credits.text = content

func _process(delta):
	moveup -= 1.2
	self.set_position(Vector2(0,moveup))
	print(self.position)
