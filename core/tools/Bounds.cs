
public class Bounds
{
    public long top;
    public long bottom;
    public long left;
    public long right;
    
    public Bounds(long top, long bottom, long left, long right)
    {
        this.top = top;
        this.bottom = bottom;
        this.left = left;
        this.right = right;
    }

    public void SetBounds(long top, long bottom, long left, long right)
    {
        this.top = top;
        this.bottom = bottom;
        this.left = left;
        this.right = right;
    }
}
