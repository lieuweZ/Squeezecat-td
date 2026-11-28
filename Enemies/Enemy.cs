using Godot;

public partial class Enemy : Area2D, IDamageable
{
    [Signal] public delegate void ReachedEndEventHandler(Enemy enemy);
    [Signal] public delegate void DiedEventHandler(Enemy enemy);

    [Export] public EnemyStats Stats { get; set; }

    private Sprite2D _sprite;
    private Health _health;
    private Tween _moveTween;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite");
        _health = GetNode<Health>("Health");

        _health.SetMax(Stats.MaxHealth);
        _health.Died += OnDied;

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
        _health.TakeDamage(amount);
    }

    private void OnDied()
    {
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