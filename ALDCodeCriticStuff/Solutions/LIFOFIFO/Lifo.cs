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
        newItem.SetNext(this._top);
        this._top = newItem;
    }

    public T? Pop()
    {
        if (this._top is null) return default;
        var poppedItem = this._top;
        this._top = this._top.GetNext();
        return poppedItem.GetData();
    }
}