namespace ALDCodeCriticStuff.Solutions.LIFOFIFO;

internal class Item<T>
{
    private readonly T _data;
    private Item<T>? _next;

    public Item(T data)
    {
        _data = data;
        _next = null;
    }

    public Item<T>? GetNext()
    {
        return _next;
    }

    public void SetNext(Item<T>? next)
    {
        _next = next;
    }

    public T GetData()
    {
        return _data;
    }
}