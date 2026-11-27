using Godot;
using System.Linq;

public partial class Level : Node2D
{
	[Export] private PackedScene _enemyScene;

	private Lane[] _lanes;

	public override void _Ready()
	{
		_lanes = GetNode("Lanes").GetChildren().OfType<Lane>().ToArray();

		foreach (Lane lane in _lanes)
			lane.SpawnEnemy(_enemyScene);
	}
}