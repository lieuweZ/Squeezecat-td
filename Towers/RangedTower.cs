using Godot;

public partial class RangedTower : Area2D, IDamageable
{
    [Export] private PackedScene _projectileScene;
    [Export] private int _damage = 1;

    private Area2D _range;
    private Timer _fireTimer;
    private Marker2D _muzzle;
    private Health _health;
    private int _enemiesInRange;

    public override void _Ready()
    {
        _range = GetNode<Area2D>("Range");
        _fireTimer = GetNode<Timer>("FireTimer");
        _muzzle = GetNode<Marker2D>("Muzzle");
        _health = GetNode<Health>("Health");

        _range.AreaEntered += OnEnemyEntered;
        _range.AreaExited += OnEnemyExited;
        _fireTimer.Timeout += Shoot;
        _health.Died += QueueFree;
    }

    public void TakeDamage(int amount) => _health.TakeDamage(amount);

    private void OnEnemyEntered(Area2D area)
    {
        _enemiesInRange++;
        if (_fireTimer.IsStopped())
        {
            CallDeferred(MethodName.Shoot);
            _fireTimer.Start();
        }
    }

    private void OnEnemyExited(Area2D area)
    {
        _enemiesInRange--;
        if (_enemiesInRange <= 0)
            _fireTimer.Stop();
    }

    private void Shoot()
    {
        var projectile = _projectileScene.Instantiate<Projectile>();
        GetParent().AddChild(projectile);   
        projectile.Launch(_muzzle.GlobalPosition, _damage);
    }
}