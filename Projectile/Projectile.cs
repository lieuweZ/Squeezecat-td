using Godot;

public partial class Projectile : Area2D
{
    [Export] private float _speed = 500f;
    [Export] private float _maxDistance = 1200f;

    private int _damage;
    private Tween _tween;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public void Launch(Vector2 start, int damage)
    {
        GlobalPosition = start;
        _damage = damage;

        Vector2 end = start + Vector2.Right * _maxDistance;
        _tween = CreateTween();
        _tween.TweenProperty(this, "global_position", end, _maxDistance / _speed);
        _tween.Finished += QueueFree;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is not IDamageable target)
            return;

        target.TakeDamage(_damage);
        _tween?.Kill();
        QueueFree();
    }
}