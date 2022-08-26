extends Node

var sHealth = 100
var pHealth = 50

var change

var curr_char = "Spongebob"

func _ready():
	pass

func _process(delta):
	if Input.is_action_just_released("switch_character"):
		if change: #if action equals true
			Global.curr_char = "Spongebob"
			change = false #set the variable to false
			print('Switched to '+Global.curr_char)
		else: #if it doesnt then
			Global.curr_char = "Plankton"
			change = true #set the variable to true
			print('Switched to '+Global.curr_char)
