namespace ALDCodeCriticStuff.Solutions.LIFOFIFO;

public class Fifo<T>
{
    private Item<T>? _front;
    private Item<T>? _rear;

    public Fifo()
    {
        _front = _rear = null;
    }

    public void Enqueue(T data)
    {
        var newItem = new Item<T>(data);

        if (_rear is not null)
        {
            _rear.SetNext(newItem);
            _rear = newItem;
            return;
        }

        _front = _rear = newItem;
    }

    public T? Dequeue()
    {
        if (_front is null) return default;

        var returnValue = _front;
        _front = _front.GetNext();

        if (_front is null) _rear = null;

        return returnValue.GetData();
    }
}