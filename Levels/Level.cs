using Godot;
using System.Linq;

public partial class Level : Node2D
{
	[Signal]
	public delegate void GameOverEventHandler();

	[Export] private PackedScene _enemyScene;
	[Export] private int _destroyedBarricadesToLose = 2;

	private Lane[] _lanes;
	private int _destroyedBarricades;
	private bool _isGameOver;

	public override void _Ready()
	{
		_lanes = GetNode("Lanes").GetChildren().OfType<Lane>().ToArray();

		foreach (Lane lane in _lanes)
		{
			lane.BarricadeDestroyed += OnBarricadeDestroyed;
			lane.SpawnEnemy(_enemyScene);
		}
	}

	private void OnBarricadeDestroyed(Lane lane)
	{
		_destroyedBarricades++;

		if (_isGameOver || _destroyedBarricades < _destroyedBarricadesToLose)
			return;

		_isGameOver = true;
		GD.Print("Game over");
		EmitSignal(SignalName.GameOver);
	}
}