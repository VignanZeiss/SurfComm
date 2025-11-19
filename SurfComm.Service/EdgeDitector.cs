public class EdgeDetector
{
    private bool _previous;

    public bool RisingEdge(bool current)
    {
        bool rising = (!_previous && current);
        _previous = current;
        return rising;
    }

    public bool FallingEdge(bool current)
    {
        bool falling = (_previous && !current);
        _previous = current;
        return falling;
    }
}
