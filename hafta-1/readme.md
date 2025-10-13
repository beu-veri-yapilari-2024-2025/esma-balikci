# 📚 Veri Yapıları Ödevi – C# Uygulamaları

## 📌 İçindekiler

- Kod Açıklamaları
  - Dizideki Sayıların Toplamını Bulma
  - Matris Çarpımı
  - İkili Arama (Recursive ve Iterative)
- Big O Notasyonu Açıklamaları
  - Dizi Toplamı: O(n)
  - Matris Çarpımı: O(n³)
  - İkili Arama: O(log n)

---

## 💡 Kod Açıklamaları

### 🧮 Dizideki Sayıların Toplamını Bulma

Bu bölümde, bir tam sayı dizisinin (`dizi2`) içindeki tüm elemanların toplanması işlemi gerçekleştirilir.  
Basit bir `for` döngüsü kullanılarak dizinin her bir elemanı gezilir ve `toplam` değişkenine eklenir.

```csharp
int[] dizi2 = { 2, 5, 6, 8, 11 };
int toplam = 0;
for (int i = 0; i < dizi2.Length; i++)
{
    toplam += dizi2[i];
}
Console.WriteLine($"Dizideki sayıların toplamı: {toplam}");
```

---

### 🔢 Matris Çarpımı

Bu kısımda, **3x3** boyutunda iki matrisin (`matris1` ve `matris2`) çarpımı yapılır ve sonuç `carpim` adlı matrise yazdırılır.  
Matris çarpımı işlemi, iç içe **üç adet for döngüsü** ile gerçekleştirilir.

```csharp
int[,] matris1 = { { 2, 5, 1 }, { 3, 2, 4 }, { 1, 3, 5 } };
int[,] matris2 = { { 2, 5, 3 }, { 4, 2, 4 }, { 3, 4, 1 } };
int[,] carpim = new int[3, 3];

for (int i = 0; i < 3; i++)
{
    for (int j = 0; j < 3; j++)
    {
        for (int k = 0; k < 3; k++)
        {
            carpim[i, j] += matris1[i, k] * matris2[k, j];
        }
    }
}
```

---

### 🔍 İkili Arama (Binary Search)

Projede, sıralı bir dizide belirli bir elemanı bulmak için ikili arama algoritmasının **iki farklı yöntemi** kullanılmıştır:

#### 1. Recursive (Özyineli) Yöntem

`Search` metodu, aranan sayıyı bulana kadar kendini tekrar tekrar çağırır.  
Eğer aranan sayı ortadaki elemandan küçükse, arama dizinin **sol yarısında**, büyükse **sağ yarısında** devam eder.

```csharp
public static int Search(int[] dizi, int aradigimizSayi, int sol, int sag)
{
    if (sag >= sol)
    {
        int orta = sol + (sag - sol) / 2;
        if (dizi[orta] == aradigimizSayi)
            return orta;
        else if (dizi[orta] > aradigimizSayi)
            return Search(dizi, aradigimizSayi, sol, orta - 1);
        else
            return Search(dizi, aradigimizSayi, orta + 1, sag);
    }
    return -1;
}
```

#### 2. Iterative (Döngüsel) Yöntem

`ikiliarama` metodu, aynı işlemi `while` döngüsüyle gerçekleştirir.  
`sol` ve `sag` indislerini güncelleyerek arama alanını daraltır.

```csharp
public static int ikiliarama(int[] A, int n, int sayi)
{
    int sol = 0;
    int sag = n - 1;

    while (sol <= sag)
    {
        int orta = (sol + sag) / 2;
        if (A[orta] == sayi)
            return orta;
        else if (sayi < A[orta])
            sag = orta - 1;
        else
            sol = orta + 1;
    }
    return -1;
}
```

> 🔔 Not: İkili arama algoritmasının çalışabilmesi için dizinin **önceden sıralanmış** olması gerekir.

---

## 📈 Big O Notasyonu Açıklamaları

### 1. Dizi Toplamı – `O(n)`  
**Anlamı:** Lineer (Doğrusal) karmaşıklık  
**Açıklama:**  
Dizideki her eleman yalnızca bir kez işlenir.  
Eğer dizi `n` elemandan oluşuyorsa, algoritma **n adım** çalışır.

---

### 2. Matris Çarpımı – `O(n³)`  
**Anlamı:** Kübik karmaşıklık  
**Açıklama:**  
İç içe **3 for döngüsü** çalıştığı için işlem sayısı `n * n * n` yani `n³` olur.  
Bu, özellikle büyük matrislerde performans açısından **çok maliyetli** olabilir.

---

### 3. İkili Arama – `O(log n)`  
**Anlamı:** Logaritmik karmaşıklık  
**Açıklama:**  
Her adımda arama yapılacak alan ikiye bölünür.  
Örneğin `n=16` elemanlı dizide maksimum 4 adımda sonuç alınır (`log₂(16) = 4`).

---

## 🛠️ Kullanılan Teknolojiler

- 💻 C#  
- 🧩 Visual Studio 2022  
- 📘 Konsol Uygulaması  
- 🧮 Algoritma & Veri Yapıları

---

## ✅ Nasıl Çalıştırılır?

1. Visual Studio 2022 ile projeyi açın  
2. `Program.cs` dosyasını çalıştırın  
3. Konsol ekranında çıktıların görüntülendiğinden emin olun

---

