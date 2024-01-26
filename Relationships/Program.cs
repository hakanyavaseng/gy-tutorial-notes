

#region Relationships (İlişkiler) Terimleri

#region Principal Entity (Asıl Varlık)
//Kendi başına var olabilen tabloyu modelleyen entity'dir.
//Departman'ın kendisi bir asıl varlıktır. Çalışanlar ise bağımlı varlıktır.
#endregion

#region Dependent Entity (Bağımlı Varlık)
//Kendi başına var olamayan tabloyu modelleyen entity'dir.
//Bir başka tabloya ilişkisel olarak bağlıdır.
#endregion

#region Foreign Key (Yabancı Anahtar)
//Bağımlı varlığın asıl varlığa bağlanmasını sağlayan anahtardır.
//Çalışanlar tablosunda DepartmanId alanı Foreign Key'dir.
#endregion

#region Principal Key (Asıl Anahtar)
//Asıl varlığın kendi içindeki anahtarıdır.
//Departman tablosunda Id alanı Principal Key'dir.
#endregion

#region Navigation Property (Gezinme Özelliği)
//İlişkisel veritabanlarında bir tablodan diğer tabloya geçiş yapmamızı sağlayan özelliktir.
//Entity classlar üzerinden sağlanır.

//Bir property'nin Navigation Property olabilmesi için Entity class'ı olması gerekir.
#endregion
class Calisan
{
    public int Id { get; set; }
    public string CalisanAdi { get; set; }
    public int DepartmanId { get; set; }
    public Departman Departman { get; set; } // Navigation Property
}

class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }

    public ICollection<Calisan> Calisanlar { get; set; } // Navigation Property
}

#endregion

#region Relationships (İlişkiler) Türleri

#region One-to-One 
//Çalışan ve adres
#endregion

#region One-to-Many
//Departman ve çalışanlar
#endregion

#region Many-to-Many
//Çalışan ve projeler
#endregion
#endregion

