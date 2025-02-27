using Godot;

public partial class GlobalRoot : Node
{
    [Export] CameraTransform camera;
    [Export] PlayerOnMap playerIcon;
    [Export] MapPointer pointer;

    public override void _Input(InputEvent @event)
    {
#if GODOT_PC
        if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
        {
            Vector2 clickPosition = camera.GetGlobalMousePosition();
            pointer.SetPosition(clickPosition);
            playerIcon.SetTarget(clickPosition);
        }
#else
        else if (@event is InputEventScreenTouch touch && touch.Pressed)
        {
            Vector2 touchPosition = camera.GetGlobalTransform().AffineInverse() * touch.Position;
            pointer.SetPosition(touchPosition);
            playerIcon.SetTarget(touchPosition);
        }
#endif
    }
}