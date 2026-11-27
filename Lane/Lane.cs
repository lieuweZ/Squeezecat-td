using Godot;

public partial class Lane : Node2D
{
	[Export] public int LaneIndex { get; set; }

	public Vector2 SpawnPosition => _spawnPoint.GlobalPosition;
	public Vector2 EndPosition => _endPoint.GlobalPosition;
	

	private Marker2D _spawnPoint;
	private Marker2D _endPoint;

	public override void _Ready()
	{
		_spawnPoint = GetNode<Marker2D>("SpawnPoint");
		_endPoint = GetNode<Marker2D>("EndPoint");
	}
	public Enemy SpawnEnemy(PackedScene enemyScene)
	{
		var enemy = enemyScene.Instantiate<Enemy>();
		AddChild(enemy);
		enemy.GlobalPosition = SpawnPosition;
		enemy.StartMoving(EndPosition);
		return enemy;
	}
}