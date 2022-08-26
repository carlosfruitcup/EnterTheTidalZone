extends KinematicBody

enum{
	SPONGEBOB
	PLANKTON
}

export var GRAVITY = -63.0
export var jump_speed = 18 #this controls the jump height
export var jump_counter = 0
export var air_accel = 4.0
export var SPEED = 10
const ACEL = 15.0

# How fast the player moves in meters per second.
export var speed = 14
# The downward acceleration when in the air, in meters per second squared.
export var fall_acceleration = 75

var curr_velocity = Vector3.ZERO

var velocity = Vector3.ZERO

var pause = false

var dir = Vector3.ZERO

onready var camera = get_node("Camera")

#	if get_parent() != null:
#		get_node("AudioStreamPlayer").stop()

func _physics_process(delta):
	if Global.curr_char != "Spongebob":
		camera.current = false
	else:
		camera.current = true
	
	process_movement_inputs()
	
	if Global.sHealth > 0 and pause == false and Global.curr_char == "Spongebob":
		process_jump_input(delta)
		process_movement(delta)


func process_movement_inputs():
		#sets the direction var back to zero like 06
	dir = Vector3.ZERO
	
	if Input.is_action_pressed("foward"):
		dir -= global_transform.basis.z
	if Input.is_action_pressed("back"):
		dir += global_transform.basis.z
	if Input.is_action_pressed("left"):
		dir -= global_transform.basis.x
	if Input.is_action_pressed("right"):
		dir += global_transform.basis.x

func process_jump_input(delta):
	#gravity
	velocity.y += GRAVITY * delta
	
	#sets the jump counter back to zero after the player lands on the floor
	if is_on_floor() or is_on_wall():
		jump_counter = 0
		
	
	#jump
	if Input.is_action_just_pressed("jump") and is_on_floor():
		jump_counter += 1
		#this VVV is the jump
		velocity.y = jump_speed

func process_movement(delta):
	var target_vel = dir * speed
	
	#silky smooth movement
	var accel = ACEL if is_on_floor() else air_accel
	curr_velocity = curr_velocity.linear_interpolate(target_vel, accel * delta)
	
	velocity.x = curr_velocity.x
	velocity.z = 0
	
	velocity = move_and_slide(velocity, Vector3.UP, true, 4, deg2rad(45))

#func _input(event):
#	if event is InputEventMouseMotion and pause == false:
#		rotate_x(deg2rad(event.relative.y * MOUSE_SENS * -1))
#		rotation_degrees.x = clamp(rotation_degrees.x, -75, 75)
#		#make camera move up and down
#
#		#make camera move left & right
#		self.rotate_y(deg2rad(event.relative.x * MOUSE_SENS * -1))
#
#	if Input.CONNECT_ONESHOT and Input.is_action_just_released("pause") and Global.health > 0:
#		if pause:
#			pause = false
#			Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
#			get_node("Control/Pause").visible = false
#		else:
#			pause = true
#			Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
#			get_node("Control/Pause").visible = true
#


#func _process(delta):
#	if Global.health < 0:
#		get_tree().paused = true
#		get_node("Control/Dead").visible = true
#		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
#
#	if Global.health <25:
#		get_node("Control").emit_signal("reset", 1.5)


func _debug():
	if Engine.is_editor_hint() == true:
		return

#func damage():
#	get_node("Control").emit_signal("reset", 1)

func _on_Timer_timeout(): #blinking
	var eyes = get_node("Eyes")
	eyes.visible = false
	yield(get_tree().create_timer(0.2),"timeout")
	eyes.visible = true
