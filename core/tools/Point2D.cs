public class Point2D {
	private int x, y;

	public static Point2D Zero = new Point2D(0, 0);

	public Point2D(int x, int y) {
		X = x;
		Y = y;
	}

	public Point2D Get() {
		return this;
	}

	public int X { get => x; set => x = value; }
	public int Y { get => y; set => y = value; }
	public override string ToString() {
		return $"Point2D(X: {X}, Y: {Y})";
	}
	public override bool Equals(object obj)
	{
		if(obj is Point2D point) {
			return point.X == X && point.Y == Y;
		}
		return false;
	}
	public override int GetHashCode()
	{
		return (X, Y).GetHashCode();
	}
}
