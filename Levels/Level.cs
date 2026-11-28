using Godot;
using System.Linq;

public partial class Level : Node2D
{
	[Signal]
	public delegate void GameOverEventHandler();

	[Export] private PackedScene _enemyScene;
	[Export] private int _destroyedBarracksToLose = 2;

	private Lane[] _lanes;
	private int _destroyedBarracks;
	private bool _isGameOver;

	public override void _Ready()
	{
		_lanes = GetNode("Lanes").GetChildren().OfType<Lane>().ToArray();

		foreach (Lane lane in _lanes)
		{
			lane.BarrackDestroyed += OnBarrackDestroyed;
			lane.SpawnEnemy(_enemyScene);
		}
	}

	private void OnBarrackDestroyed(Lane lane)
	{
		_destroyedBarracks++;

		if (_isGameOver || _destroyedBarracks < _destroyedBarracksToLose)
			return;

		_isGameOver = true;
		GD.Print("Game over");
		EmitSignal(SignalName.GameOver);
	}
}