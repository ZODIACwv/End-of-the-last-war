using Godot;

public partial class CameraTransform : Camera2D
{
    private Vector2 dragStartPosition;
    private bool isDragging;

    public override void _Input(InputEvent @event)
    {
#if GODOT_PC
        if (@event is InputEventMouseButton mouseButton)
            if (mouseButton.ButtonIndex == MouseButton.Left)
            {
                if (mouseButton.Pressed)
                {
                    isDragging = true;
                    dragStartPosition = GetGlobalMousePosition();
                }
                else
                    isDragging = false;
            }
            else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
                Zoom *= 1.1f;
            else if (mouseButton.ButtonIndex == MouseButton.WheelDown)
                Zoom *= 0.9f;
        else if (@event is InputEventMouseMotion mouseMotion && isDragging)
        {
            Vector2 delta = dragStartPosition - GetGlobalMousePosition();
            Position += delta;
            dragStartPosition = GetGlobalMousePosition();
        }
#else
        if (@event is InputEventScreenTouch touch)
            if (touch.Pressed)
            {
                isDragging = true;
                dragStartPosition = touch.Position;
            }
            else
                isDragging = false;
        else if (@event is InputEventScreenDrag drag)
        {
            Vector2 delta = dragStartPosition - drag.Position;
            Position += delta;
            dragStartPosition = drag.Position;
        }
        else if (@event is InputEventMagnifyGesture magnify)
            Zoom *= magnify.Factor;
#endif
    }
}