# ExpenseTrackerCase

## Kullanılan Yöntem ve Teknolojiler
 * ***.NET 6.0 Web API***
 * ***Microsoft SQL***
 * ***Entity Framework***
 * ***Automapper***
 * ***XUnit***
 * ***Fluent Validation***
 * ***Moq***
 * ***Identity & JWT Bearer Tokens
 * ***N-tier Architecture***
 * ***Repository Pattern***
 * ***SOLID Principles***
 * ******


___

## Projeyi Geliştirici Ortamında Çalıştırmak İçin

### Ön Gereksinimler
* Visual Studio 2020+
* Microsoft SQL Server 2019+
* .Net 6.0

### Çalıştırılması
Local klasöre projeyi klonlamak için :
```
 git clone https://github.com/sercaniyili/ExpenseTrackerCase.git
```

Veritababı bağlantısı için "appsettings.json" dosyasına aşağıdakinu kopyalayın:
```
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-UKFGN49;Database=ExpenseTrackerCaseDb; Trusted_Connection=True;"
  },
```

Veritabanını ve Tabloları oluşturmak için  "Package Manager Console" açıp aşağıdakini yazın :
```
 Update-Database
```
<br/>


- API için gerekli önyüz tasarımı

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/project.png" />


- Veritabanı tasarım

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/databaseDiagram.png" />


- API endpoints

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/endpoints.png" />


- Kullanıcı oluşturma

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/register.png" />


- Kullanıcı girişi

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/login.png" />


- Hesap oluşturma

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/account_create.png" />


- Yetkisiz kullanıcı hesaplara erişemez

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/accounsUnAuth.png" />


- Kullanıcı girişi sırasında oluşturulan JWT kullanılarak hesaplara erişim sağlanıyor

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/accountsGet.png" />


- Hesap güncelleniyor

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/accountUpdate.png" />


- Hesap siliniyor

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/accountDelete.png" />


- Hesaplara göre işlem oluşturuyor (girdi yada çıktı işlem tutarlarına göre hesap bakiyeleri güncelleniyor)

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/transaction_create.png" />


- İşlemler listeleniyor

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/transactionsGet.png" />


- İşlem siliniyor (işlem tutarı hesap bakiyesinden eksiliyor)

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/transaction_delete.png" />


- Hesaplara göre işlemler

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/transactionsbyAccountID.png" />


- Veritabanında hesaplar

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/accounts.png" />


- Veritabanında işlemler

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/transactions.png" />


- Proje için yazılmış unit tests

<img src="https://github.com/sercaniyili/ExpenseTrackerCase/blob/main/images/tests.png" />

