
#region Event nedir? Ne amaca hizmet etmektedir?
//Event bir olayın meydana geldiğini takip etmek ve bu olaya karşın tepki vermek amacıyla kullanılan bir yapılanmadır.

// Olayları izlemek ve tepki vermek, olayları kaydetmek, farklı bileşen ve nesneler arasında iletişim kurma gibi senaryolarda sıklıkla kullanılırlar.

//Eventler delegate'lerle birlikte kullanılmaktadır.
#endregion

#region Event Tanımlama ve Kullanma
/*
MyEventPublisher m = new();
MyEventPublisher.XHandler xDelegate = new MyEventPublisher.XHandler(() => { }); // Delegate'ler normal şartlarda nesneler üzerinden değil sınıf üzerinden erişilebilir. Bu durum eventler ile handle edilmektedir.

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

#region Event Yapılanmasındaki Add ve Remove Blokları
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
            Console.WriteLine("A method is attached to event!");

        }

        remove
        {
            xDelegate -= value;
            Console.WriteLine("A method is detached from event!");
        }
    }

    public void RaiseEvent()
    {
        xDelegate();
    }
}
*/
#endregion

#region Örnek Event Uygulaması 
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
        while(true)
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





