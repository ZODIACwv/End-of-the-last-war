using Godot;

public sealed class Item_AK74 : InventoryWeapon
{
    public Item_AK74(byte positionX, byte positionY, byte endurance) =>
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

public sealed class Item_Flask : InventoryLiquidContainer
{
    public Item_Flask(byte positionX, byte positionY, byte endurance) =>
        Init(
            texture = GD.Load<Texture2D>("res://sprites/player/items/flask.png"),
            name = "Flask",
            description = "just a simply flask, I can place there some liquid",
            positionX,
            positionY,
            width = 2,
            height = 2,
            weight = 0.3f,
            endurance
        );
}