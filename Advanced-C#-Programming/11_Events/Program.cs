#region What is an Event? What purpose does it serve?
// An Event is a structure used to track the occurrence of an event and respond to it.
// They are frequently used in scenarios such as monitoring events, responding to events, recording events, and communicating between different components and objects.
// Events are commonly used in conjunction with delegates.
#endregion

#region Event Declaration and Usage
/*
MyEventPublisher m = new();
MyEventPublisher.XHandler xDelegate = new MyEventPublisher.XHandler(() => { }); // Delegates can normally be accessed through classes, not objects. This situation is handled with events.

m.MyEvent += () => { Console.WriteLine("Event thrown!"); }; // Event subscription
m.RaiseEvent();
Console.WriteLine();

class MyEventPublisher
{
    public delegate void XHandler();
    public event XHandler MyEvent;

    public void RaiseEvent()
    {
        MyEvent();
    }
}
*/
#endregion

#region Add and Remove Blocks in Event Structure
/*
class MyEventPublisher
{
    public delegate void XHandler();
    XHandler xDelegate;

    public event XHandler MyEvent
    {
        add
        {
            xDelegate += value;
            Console.WriteLine("A method is attached to the event!");
        }

        remove
        {
            xDelegate -= value;
            Console.WriteLine("A method is detached from the event!");
        }
    }

    public void RaiseEvent()
    {
        xDelegate();
    }
}
*/
#endregion

#region Example Event Application 
PathControl pc = new();
pc.PathControlEvent += () =>
{
    Console.WriteLine("The size exceeded 50 MB!");
};
await pc.ControlAsync();

class PathControl
{
    public delegate void PathHandler();
    public event PathHandler PathControlEvent;
    string path = @"C:\Users\hakan\OneDrive\Desktop";

    public async Task ControlAsync()
    {
        while (true)
        {
            await Task.Delay(1000);
            DirectoryInfo directoryInfo = new(path);
            var files = directoryInfo.GetFiles();
            var size = await Task.Run(() => directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length));
            var sizeMB = (size / 1024) / 1024;

            if (sizeMB > 50)
                PathControlEvent();
        }
    }
}
#endregion
