using Godot;

public partial class Enemy : Area2D
{
    [Signal] public delegate void ReachedEndEventHandler(Enemy enemy);
    [Signal] public delegate void DiedEventHandler(Enemy enemy);

    [Export] public EnemyStats Stats { get; set; }
    
    private Tween _moveTween;
    private int _currentHealth;
    private Sprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite");
        _currentHealth = Stats.MaxHealth;

        if (Stats.Texture != null)
            _sprite.Texture = Stats.Texture;
    }

    public void StartMoving(Vector2 target)
    {
        float duration = GlobalPosition.DistanceTo(target) / Stats.Speed;

        _moveTween = CreateTween();
        _moveTween.TweenProperty(this, "global_position", target, duration);
        _moveTween.Finished += OnReachedEnd;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth > 0)
            return;

        _moveTween?.Kill();
        EmitSignal(SignalName.Died, this);
        QueueFree();
    }

    private void OnReachedEnd()
    {
        EmitSignal(SignalName.ReachedEnd, this);
        QueueFree();
    }
}