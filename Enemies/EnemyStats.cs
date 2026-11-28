using Godot;

[GlobalClass]
public partial class EnemyStats : Resource
{
    [Export] public float Speed { get; set; } = 80f;
    [Export] public int MaxHealth { get; set; } = 1;
    [Export] public Texture2D Texture { get; set; }
}