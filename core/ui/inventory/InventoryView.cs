using Godot;
using System.Collections.Generic;

public partial class InventoryView : Control
{
	public Inventory inventory;
	public InventoryGridView inventoryGridView;
	InventoryRoot inventoryRoot;
	PackedScene InventoryItemPrefab;
	Dictionary<InventoryItemView, ushort> displayedItems;

	public InventoryView Initialize(InventoryRoot inventoryRoot)
	{
		this.inventoryRoot = inventoryRoot;
        inventory = inventoryRoot.inventory;
		displayedItems = new Dictionary<InventoryItemView, ushort>();
		inventoryGridView = GetNode<InventoryGridView>("../Grid").Initialize(inventory);
		InventoryItemPrefab = GD.Load<PackedScene>("res://core/ui/inventory/InventoryItem.tscn");
		MouseFilter = MouseFilterEnum.Ignore;
		return this;
	}
	public void UpdateView() => GD.Print("UpdateView invoked");
	public void UpdateItemAdd(InventoryItem item)
	{
		InventoryItemView itemView = InventoryItemPrefab.Instantiate<InventoryItemView>();
		itemView.item = item;
		itemView.Size = new(item.width * TextureRuntimeSettings.slotSize, item.height * TextureRuntimeSettings.slotSize);
		itemView.GetNode<TextureRect>("ItemTexture").Texture = item.texture;
        ProgressBar progressBar = itemView.GetChild<ProgressBar>(1);
        if (!item.IsStackable)
		{
			progressBar.Visible = true;
			progressBar.Value = (double)item.endurance;
		}
		else 
			progressBar.Visible = false;

		itemView.Position = new(
			item.positionX * TextureRuntimeSettings.slotSize + inventoryRoot.borderRadius, 
			item.positionY * TextureRuntimeSettings.slotSize + inventoryRoot.borderRadius
		);

		displayedItems.Add(itemView, (ushort)GetChildCount());
		AddChild(itemView);
	}
	public void UpdateItemRemove(InventoryItemView itemView)
	{
		itemView.QueueFree();
	}
    public void SnapToGrid(Vector2I? cellPosition, InventoryItemView itemView)
    {
        inventory.Remove(itemView.item);
        UpdateItemRemove(itemView);
        itemView.item.positionX = (byte)cellPosition.Value.X;
        itemView.item.positionY = (byte)cellPosition.Value.Y;
        inventory.Add(itemView.item);
		UpdateItemAdd(itemView.item);
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
		if (item.positionX + item.width > width || item.positionY + item.height > height) return false;
		for (byte y = item.positionY; y < item.positionY + item.height; y++)
			for (byte x = item.positionX; x < item.positionX + item.width; x++)
				if (!grid[x, y].CanPlaceItem())
					return false;
		return true;
	}
	public bool CanPlaceItem(Vector2I? cellPos, InventoryItem item)
	{
		if (cellPos.Value.X + item.width > width || cellPos.Value.Y + item.height > height) return false;
		for (int y = cellPos.Value.Y; y < cellPos.Value.Y + item.height; y++)
			for (int x = cellPos.Value.X; x < cellPos.Value.X + item.width; x++)
				if (!grid[x, y].CanPlaceItem())
					return false;
        return true;
    }
    public void Add(InventoryItem item)
	{
		if (!CanPlaceItem(item)) return;
		grid[item.positionX, item.positionY].isGeneralItemSlot = true;
		for (byte y = item.positionY; y < item.positionY + item.height; y++)
			for (byte x = item.positionX; x < item.positionX + item.width; x++)
				grid[x, y].item = item;
	}
	public void Remove(InventoryItem item) 
	{
		grid[item.positionX, item.positionY].isGeneralItemSlot = false;
        for (byte y = item.positionY; y < item.positionY + item.height; y++)
            for (byte x = item.positionX; x < item.positionX + item.width; x++)
                grid[x, y].item = null;
	}
	
	// TODO
	public void Increase(byte posY, byte posX, uint quantity) { }
	public void Decrease(byte posY, byte posX, uint quantity) { }
}
