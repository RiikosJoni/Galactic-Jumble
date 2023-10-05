using Godot;
using System;

public partial class Player : Area2D
{	
	public Vector2 ScreenSize;
	
	[Export] public string PlayerType = "Basic";
	[Export] public string PlayerAbility = "None";
	
	[Export] public int AttackSpeed = 10; //Cooldown of bullets
	[Export] public int AttackRate = 1; //Amount of bullets shot
	bool canShoot = true;
	
	[Export] public float MaxXVelocity { get; set; } = 1000;
	[Export] public float MaxYVelocity { get; set; } = 1000;
	
	public Vector2 Velocity;
	
	[Export] public float Acceleration = 4;
	[Export] public float Friction = 0.9f;
	
	private AnimatedSprite2D _rocketFlame;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		
		Velocity = Vector2.Zero;
		
		_rocketFlame = GetNode<AnimatedSprite2D>("Flame");
	}

	public override void _Process(double delta)
	{
		//Defining input
		if (Input.IsActionPressed("move_right") && Velocity.X < MaxXVelocity)
		{
			Velocity.X += Acceleration;
		}

		if (Input.IsActionPressed("move_left") && Velocity.X > -MaxXVelocity)
		{
			Velocity.X -= Acceleration;
		}

		if (Input.IsActionPressed("move_down") && Velocity.Y < MaxYVelocity)
		{
			Velocity.Y += Acceleration;
		}

		if (Input.IsActionPressed("move_up") && Velocity.Y > -MaxYVelocity)
		{
			Velocity.Y -= Acceleration;
		}
		
		if (Input.IsActionPressed("shoot"))
		{
			Shoot();
		}
		
		if (Velocity.Length() > 0.3f)
		{
			Velocity = Velocity * Friction;
			_rocketFlame.Play("Move");
   		}else{
			Velocity = Vector2.Zero;
			_rocketFlame.Play("Idle");
		}
		
		Position += Velocity * (float)delta;
		Position = new Vector2(
		x: Mathf.Clamp(Position.X, 0 + ScreenSize.X / 5, ScreenSize.X - ScreenSize.X / 5),
		y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		
		
	}
	
	public void Shoot() 
	{
		if (!canShoot) { return }
		canShoot = false;
		
	}
}
