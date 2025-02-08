using Godot;

public readonly struct Caliber
{
    public readonly string caliber;

    public Caliber(string caliber) => this.caliber = caliber;
}

public abstract class InventoryItem
{
    public Texture2D texture;
    protected string name;
    internal string description;
    protected bool isStackable;
    protected uint? maxStackSize;
    public uint? quantity;
    public byte? endurance;
    public byte positionX;
    public byte positionY;
    public byte width;
    public byte height;
    public float weight;

    public bool IsStackable
    {
        get => isStackable; 
    }
    public string Name
    {
        get => name;
    }
    public uint? Quantity
    {
        get => quantity;
        set
        {
            if (quantity > maxStackSize) quantity = maxStackSize;
            quantity = value;
        }
    }
    public byte PositionX
    {
        get => positionX;
    }
    public byte PositionY
    {
        get => positionY;
    }
}

public abstract class InventoryWeapon : InventoryItem
{
    public Caliber caliber;
    public bool chamber;
    
    protected void Init(Texture2D texture, string name, string description, byte positionY, byte positionX, byte width, byte height, float weight, byte? endurance, Caliber caliber, bool chamber)
    {
        isStackable = false;
        maxStackSize = null;
        Quantity = null;
        base.endurance = endurance;
        base.positionX = positionX;
        base.positionY = positionY;
        base.name = name;
        base.description = description;
        base.texture = texture;
        base.width = width;
        base.height = height;
        base.weight = weight;
        this.caliber = caliber;
        this.chamber = chamber;
    }

    public void EmptyTheChamber() => chamber = false;
}

