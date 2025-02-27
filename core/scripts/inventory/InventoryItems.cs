using Godot;

public sealed class AK74 : InventoryWeapon
{
    public AK74(byte positionX, byte positionY, byte endurance) =>
        Init(
            texture = GD.Load<Texture2D>("res://sprites/player/items/AK74.png"),
            name = "AK74",
            description = "The child of legendary AK47",
            positionX,
            positionY,
            width = 6,
            height = 2,
            weight = 3.63f,
            endurance,
            caliber = new("5.45x39"),
            chamber = false
        );
}
