public class TileRegion {
    public string name;
    public int x, y;
    public string className;

    public TileRegion(string name, Point2D position, string className) {
        this.name = name;
        this.className = className;
        x = position.X;
        y = position.Y;
    }
}