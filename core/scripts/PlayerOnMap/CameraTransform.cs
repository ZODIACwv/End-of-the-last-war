using Godot;

public partial class CameraTransform : Camera2D
{
    [Export] public Node2D mapGenerator;
    [Export] public Sprite2D pointer;
    [Export] public PlayerOnMap playerIcon;
    [Export] public Node2D[] zoomIndependentNodes;

    float targetZoom = 1.0f;

    const float MIN_ZOOM = 0.3f;
    const float MAX_ZOOM = 1.0f;
    const float ZOOM_INCREMENT = 0.1f;
    const float ZOOM_RATE = 4.0f;

    public override void _Ready()
    {
        foreach (Node2D node in zoomIndependentNodes)
            node.SetMeta("original_scale", node.Scale);
    }

    public override void _PhysicsProcess(double delta)
    {
        Zoom = Zoom.Lerp(targetZoom * Vector2.One, ZOOM_RATE * (float)delta);
        UpdateMapEntitiesScaling();
        SetPhysicsProcess(!Mathf.IsEqualApprox(Zoom.X, targetZoom));
    }

    public override void _Input(InputEvent @event)
    {
#if GODOT_PC
        if (@event is InputEventMouseButton mouseButton && mouseButton.IsPressed())
        {
            if (mouseButton.ButtonIndex == MouseButton.Left)
                OnClick();
            else if (mouseButton.ButtonIndex == MouseButton.WheelUp)
                OnZoomInc();
            else if (mouseButton.ButtonIndex == MouseButton.WheelDown)
                OnZoomDec();
        }
        else if (@event is InputEventMouseMotion mouseMotion && (mouseMotion.ButtonMask == MouseButtonMask.Middle || mouseMotion.ButtonMask == MouseButtonMask.Right))
            Position -= mouseMotion.Relative / Zoom.X;
#else
        if (@event is InputEventScreenTouch touch && touch.Pressed)
            OnClick();
        else if (@event is InputEventScreenDrag drag)
            Position -= drag.Relative / Zoom.X;
        else if (@event is InputEventMagnifyGesture magnify)
        {
            targetZoom *= magnify.Factor;
            targetZoom = Mathf.Clamp(targetZoom, MIN_ZOOM, MAX_ZOOM);
            SetPhysicsProcess(true);
        }
#endif
    }

    void OnZoomInc()
    {
        targetZoom = Mathf.Min(targetZoom + ZOOM_INCREMENT, MAX_ZOOM);
        SetPhysicsProcess(true);
    }

    void OnZoomDec()
    {
        targetZoom = Mathf.Max(targetZoom - ZOOM_INCREMENT, MIN_ZOOM);
        SetPhysicsProcess(true);
    }

    void OnClick()
    {
        Vector2 clickPosition = GetGlobalMousePosition();
        pointer.SetPosition(clickPosition);
        playerIcon.SetTarget(clickPosition);
    }

    void UpdateMapEntitiesScaling()
    {
        if (Zoom.X > 0)
            foreach (Node2D node in zoomIndependentNodes)
                node.Scale = node.GetMeta("original_scale").AsVector2() / Zoom.X;
    }
}