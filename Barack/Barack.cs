using Godot;

public partial class Barack : Node2D
{
	[Signal] public delegate void DestroyedEventHandler(Barack barack);

	[Export] public int MaxHealth { get; set; } = 1;

	public bool IsDestroyed => _currentHealth <= 0;

	private int _currentHealth;

	public override void _Ready()
	{
		_currentHealth = MaxHealth;
	}

	public void TakeDamage(int amount)
	{
		if (IsDestroyed)
			return;

		_currentHealth -= amount;

		if (IsDestroyed)
		{
			QueueFree();
			EmitSignal(SignalName.Destroyed, this);
		}
	}
}
