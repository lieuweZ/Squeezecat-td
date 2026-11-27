using Godot;

public partial class Enemy : Area2D
{
	[Signal] public delegate void ReachedEndEventHandler(Enemy enemy);

	[Export] public float Speed { get; set; } = 80f;

	private Tween _moveTween;

	public void StartMoving(Vector2 target)
	{
		float duration = GlobalPosition.DistanceTo(target) / Speed;
		
		_moveTween = CreateTween();
		_moveTween.TweenProperty(this, "global_position", target, duration);
		_moveTween.Finished += OnReachedEnd;
	}

	private void OnReachedEnd()
	{
		EmitSignal(SignalName.ReachedEnd, this);
		QueueFree();
	}
}