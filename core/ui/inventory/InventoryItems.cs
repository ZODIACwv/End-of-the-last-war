using Godot;

public sealed class AK74 : InventoryWeapon
{
    public AK74(byte positionY, byte positionX, byte endurance) =>
        Init(
            texture = GD.Load<Texture2D>("res://core/ui/inventory/AK74.jpg"),
            name = "AK74",
            description = "The child of legendary AK47",
            positionY,
            positionX,
            width = 6,
            height = 2,
            weight = 3.63f,
            endurance,
            caliber = new("5.45x39"),
            chamber = false
        );
}
