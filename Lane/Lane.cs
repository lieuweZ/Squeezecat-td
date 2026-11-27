using Godot;

public partial class Lane : Node2D
{
	[Signal] public delegate void BarricadeDestroyedEventHandler(Lane lane);
	[Export] public int LaneIndex { get; set; }

	public Vector2 SpawnPosition => _spawnPoint.GlobalPosition;
	public Vector2 EndPosition => _endPoint.GlobalPosition;
	

	private Marker2D _spawnPoint;
	private Marker2D _endPoint;
	private Barack _barack;

	public override void _Ready()
	{
		_spawnPoint = GetNode<Marker2D>("SpawnPoint");
		_endPoint = GetNode<Marker2D>("EndPoint");
		_barack = GetNode<Barack>("Barack");

		_barack.Destroyed += OnBarackDestroyed;
	}
	public Enemy SpawnEnemy(PackedScene enemyScene)
	{
		var enemy = enemyScene.Instantiate<Enemy>();
		AddChild(enemy);
		enemy.ReachedEnd += OnEnemyReachedEnd;
		enemy.GlobalPosition = SpawnPosition;
		enemy.StartMoving(EndPosition);
		return enemy;
	}
	private void OnEnemyReachedEnd(Enemy enemy)
	{
		// Temporary number
		_barack.TakeDamage(1);
	}

	private void OnBarackDestroyed(Barack barack)
	{
		EmitSignal(SignalName.BarricadeDestroyed, this);
	}
}
