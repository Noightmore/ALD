namespace ALDCodeCriticStuff.Solutions.LIFOFIFO;

public class Fifo<T>
{
    private Item<T>? _front;
    private Item<T>? _rear;

    public Fifo()
    {
        this._front = this._rear = null;
    }

    public void Enqueue(T data)
    {
        var newItem = new Item<T>(data);
        
        if (this._rear is not null)
        {
            this._rear.SetNext(newItem);
            this._rear = newItem;
            return;
        }
        this._front = this._rear = newItem;
    }

    public T? Dequeue()
    {
        if(this._front is null) return default;
        
        var returnValue = this._front;
        this._front = this._front.GetNext();
        
        if(this._front is null) this._rear = null;
        
        return returnValue.GetData();
    }
}