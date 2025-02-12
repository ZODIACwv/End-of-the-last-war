using Godot;

public partial class InventoryView : Control
{
	public Inventory inventory;
	public InventoryGridView inventoryGridView;
	public PackedScene InventoryItemPrefab;
	public InventoryView Initialize(Inventory inventory)
	{
        this.inventory = inventory;
		inventoryGridView = GetNode<InventoryGridView>("../Grid").Initialize(inventory);
		InventoryItemPrefab = GD.Load<PackedScene>("res://core/ui/inventory/InventoryItem.tscn");
		return this;
	}
	public void UpdateView() => GD.Print("UpdateView invoked");
	public void UpdateItem(InventoryItem item)
	{
		InventoryItemView itemView = InventoryItemPrefab.Instantiate<InventoryItemView>();
		itemView.Size = new(item.width * TextureRuntimeSettings.slotSize, item.height * TextureRuntimeSettings.slotSize);
		itemView.GetChild<TextureRect>(0).Texture = item.texture;
        var progressBar = itemView.GetChild<ProgressBar>(1);
        if (!item.IsStackable)
		{
			progressBar.Visible = true;
			progressBar.Value = (double)item.endurance;
		}
		else 
			progressBar.Visible = false;
		itemView.Position = new(item.PositionX * TextureRuntimeSettings.slotSize, item.PositionY * TextureRuntimeSettings.slotSize);
		AddChild(itemView);
	}
}

public class Inventory
{
	public InventorySlot[,] grid;
	public readonly byte width;
	public readonly byte height;

	public Inventory(byte width, byte height)
	{
		this.width = width;
		this.height = height;
		grid = new InventorySlot[width, height];

		for (byte y = 0; y < height; y++)
			for (byte x = 0; x < width; x++)
				grid[x, y] = new();
	}
	public bool CanPlaceItem(InventoryItem item)
	{
		if (item.PositionX + item.width > grid.GetLength(1) || item.PositionY + item.height > grid.GetLength(0)) return false;
		for (byte y = item.PositionY; y < item.PositionY + item.height; y++)
			for (byte x = item.PositionX; x < item.PositionX + item.width; x++)
				if (!grid[y, x].CanPlaceItem())
					return false;
		return true;
	}
	public void Add(InventoryItem item)
	{
		if (!CanPlaceItem(item)) return;
		grid[item.PositionX, item.PositionY].isGeneralItemSlot = true;
		for (byte y = 0; y < item.PositionY + item.height; y++)
			for (byte x = 0; x < item.PositionX + item.width; x++)
				grid[x, y].item = item;
	}
	public void Remove(InventoryItem item) 
	{
		grid[item.positionX, item.positionY].isGeneralItemSlot = false;
		for (byte x = 0; x < item.width; x++)
			for (byte y = 0; y < item.height; y++)
				grid[x, y].item = null;
	}
	
	// TODO
	public void Increase(byte posY, byte posX, uint quantity) { }
	public void Decrease(byte posY, byte posX, uint quantity) { }
}
