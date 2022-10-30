namespace ALDCodeCriticStuff.Solutions.LIFOFIFO;

internal class Item<T>
{
    private readonly T _data;
    private Item<T>? _next;

    public Item<T>? GetNext()
    {
        return this._next;
    }
    
    public void SetNext(Item<T>? next)
    {
        this._next = next;
    }

    public T GetData()
    {
        return this._data;
    }
    
    public Item(T data)
    {
        this._data = data;
        this._next = null;
    }
}