Console.WriteLine();

#region Practice 1

MyObservable observable = new();

using var sub1 = observable.Subscribe(new MyObserver("1"));
using var sub2 = observable.Subscribe(new MyObserver("2"));
using var sub3 = observable.Subscribe(new MyObserver("3"));

observable.NotifyObservers(10);
observable.NotifyObservers(20);
observable.NotifyObservers(30);



class MyObservable : IObservable<int>
{
    List<IObserver<int>> observers = new();
    public IDisposable Subscribe(IObserver<int> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);

        return new Unsubscription(() =>
        {
            observers.Remove(observer);
            observer.OnCompleted();
        });
    }

    public void NotifyObservers(int value) => observers.ForEach(observer => observer.OnNext(value));
}

class Unsubscription(Action unSubscription) : IDisposable
{
    public void Dispose()
    {
        unSubscription.Invoke();
        unSubscription = null;
    }
}

class MyObserver(string observerName) : IObserver<int>
{
    public void OnCompleted()
    {
        Console.WriteLine($"{observerName} Completed");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"{observerName} Error: {error.Message}");
    }

    public void OnNext(int value)
    {
        Console.WriteLine($"{observerName} Value: {value}");
    }
}

#endregion