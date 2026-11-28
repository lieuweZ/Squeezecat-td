using Godot;

public partial class Health : Node
{
	[Signal] public delegate void DamagedEventHandler(int current, int max);
	[Signal] public delegate void DiedEventHandler();

	[Export] public int MaxHealth { get; set; } = 1;

	public int Current { get; private set; }
	public bool IsDead => Current <= 0;

	public override void _Ready()
	{
		Current = MaxHealth;
	}

	public void SetMax(int max)
	{
		MaxHealth = max;
		Current = max;
	}

	public void TakeDamage(int amount)
	{
		if (IsDead)
			return;

		Current = Mathf.Max(Current - amount, 0);
		EmitSignal(SignalName.Damaged, Current, MaxHealth);

		if (IsDead)
			EmitSignal(SignalName.Died);
	}
}
