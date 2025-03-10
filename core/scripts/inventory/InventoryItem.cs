using Godot;

public readonly struct Caliber
{
    public readonly string caliber;
    public Caliber(string caliber) => this.caliber = caliber;
}

public abstract class InventoryItem
{
    public Texture2D texture;
    public string name;
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
}

public abstract class InventoryWeapon : InventoryItem
{
    public Caliber caliber;
    public bool chamber;
    
    protected void Init(Texture2D texture, string name, string description, byte positionX, byte positionY, byte width, byte height, float weight, byte endurance, Caliber caliber, bool chamber)
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

public abstract class InventoryLiquidContainer : InventoryItem
{
    protected void Init(Texture2D texture, string name, string description, byte positionX, byte positionY, byte height, byte width, float weight, byte endurance)
    {
        isStackable = false;
        maxStackSize = null;
        quantity = null;
        base.texture = texture;
        base.name = name;
        base.description = description;
        base.endurance = endurance;
        base.positionX = positionX;
        base.positionY = positionY;
        base.width = width;
        base.height = height;
        base.weight = weight;
    }
}