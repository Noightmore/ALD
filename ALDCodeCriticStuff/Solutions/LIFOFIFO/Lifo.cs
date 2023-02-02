namespace ALDCodeCriticStuff.Solutions.LIFOFIFO;

public class Lifo<T>
{
    private Item<T>? _top;

    public Lifo()
    {
        _top = null;
    }

    public void Push(T data)
    {
        var newItem = new Item<T>(data);
        newItem.SetNext(_top);
        _top = newItem;
    }

    public T? Pop()
    {
        if (_top is null) return default;
        var poppedItem = _top;
        _top = _top.GetNext();
        return poppedItem.GetData();
    }
}