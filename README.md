# Toko Sembah Ayam 🍗

My first project using **C# and .NET**.

This project is a simple console-based store inventory and transaction system inspired by a small fried chicken shop.

The main purpose of this project is to practice **C# fundamentals, Object-Oriented Programming (OOP), collections, class relationships, and basic business logic** by building a simple real-world scenario.

> This is a learning project. The current version does not use a database yet, so all data is stored in memory using C# collections.

---

## 📌 Project Overview

Before starting the implementation, I tried to think about the system from a real-world perspective:

**What kind of store is this?**
**Who will use the system?**
**What information does the system need?**
**What can each user do?**

From those questions, I defined two types of users:

### 👨‍💼 Admin

The admin can:

* Add new products
* View available products
* View transactions and transaction details

### 🛒 Customer

The customer can:

* View the available menu
* Select a product to purchase
* Check product availability
* Enter the desired quantity
* Confirm the purchase
* Automatically update the product stock
* Create or update a transaction

---

## 🧠 Starting With the Data Structure

Before writing the program, I designed the data structure that would be needed if this application were connected to a database.

I identified three main entities:

```text
Produk
    │
    │
    ▼
DetailTransaksi
    │
    │
    ▼
Transaksi
```

### Produk

```text
Produk
├── id
├── name
├── price
└── stock
```

### Transaksi

```text
Transaksi
├── id
├── date
├── total
└── detailTransaction
```

### DetailTransaksi

```text
DetailTransaksi
├── id
├── transactionId
├── product
├── qty
└── subtotal
```

The idea is that one transaction can contain multiple transaction details, while each detail represents a product being purchased.

For example:

```text
Transaction #1
│
├── Ayam Goreng × 2
│   └── Subtotal: Rp40.000
│
└── Ayam Bakar × 1
    └── Subtotal: Rp25.000

Total: Rp65.000
```

---

## 🏗️ Mapping the Data Structure to C#

After designing the data structure, I represented each entity as a C# class.

The project currently has three main models:

```text
Models/
├── Produk.cs
├── Transaksi.cs
└── DetailTransaksi.cs
```

### `Produk`

Represents a product available in the store.

```csharp
public class Produk
{
    public int id { get; set; }
    public string name { get; set; }
    public int price { get; set; }
    public int stock { get; set; }
}
```

### `Transaksi`

Represents a transaction and contains a list of transaction details.

```csharp
public class Transaksi
{
    public int id { get; set; }
    public DateTime date { get; set; }
    public int total { get; set; }

    public List<DetailTransaksi> detailTransaction
        { get; set; } = new List<DetailTransaksi>();
}
```

### `DetailTransaksi`

Represents the individual products inside a transaction.

```csharp
public class DetailTransaksi
{
    public int id { get; set; }
    public int transactionId { get; set; }
    public Produk product { get; set; }
    public int qty { get; set; }
    public int subtotal { get; set; }
}
```

---

# ⚙️ Application Flow

## 1. Main Menu

When the application starts, the user can choose:

```text
Who Are You?

1. I am an Admin
2. I am a Customer
3. Exit
```

---

## 2. Admin Flow

The admin menu provides several options:

```text
What would you like to do?

1. Add Product
2. View Products
3. Logout
4. View Transactions
```

### Add Product

The admin can enter:

* Product ID
* Product name
* Product price
* Product stock

The product is then added to the product collection.

### View Products

The admin can see the products currently available in the store together with their price and stock.

### View Transactions

The admin can see:

* Transaction ID
* Transaction date
* Transaction total
* Transaction details
* Product purchased
* Quantity
* Subtotal

---

# 🛒 Customer Flow

The customer menu provides:

```text
What would you like to do?

1. Give me the Menu
2. I want to buy product
3. Logout
```

### View Menu

The customer can see the available products and their prices.

### Purchase Product

The purchasing flow is:

```text
Select Product
      ↓
Check Product Exists
      ↓
Enter Quantity
      ↓
Check Stock
      ↓
Calculate Total Price
      ↓
Confirm Purchase
      ↓
Update Stock
      ↓
Create / Update Transaction
      ↓
Add Transaction Detail
```

For example:

```text
Ayam Goreng
Price: Rp20.000
Quantity: 2

Total Price: Rp40.000

Do you want to buy it? (yes/no)
```

If the customer confirms the purchase, the product stock is reduced and the transaction is recorded in memory.

---

# 💾 Current Data Storage

The current version does **not use a database**.

Instead, the application uses C# collections:

```csharp
List<Produk> products
List<Transaksi> transactions
```

This means the data only exists while the application is running.

For example:

```csharp
List<Produk> products = new List<Produk>()
{
    new Produk
    {
        id = 1,
        name = "Ayam Goreng",
        price = 20000,
        stock = 25
    },

    new Produk
    {
        id = 2,
        name = "Ayam Bakar",
        price = 25000,
        stock = 30
    }
};
```

The database design was created beforehand as a way to understand what data the application would eventually need.

---

# 🧩 What I Learned

This project taught me that building an application does not always start with writing code.

I started by thinking about the system itself:

```text
Business Requirements
        ↓
Identify Users
        ↓
Identify Features
        ↓
Identify Data
        ↓
Design Data Relationships
        ↓
Create C# Models
        ↓
Implement Business Logic
```

Through this project, I practiced:

* C# fundamentals
* Classes and objects
* Object-Oriented Programming
* `List<T>` collections
* Object relationships
* `DateTime`
* Conditional logic
* Loops
* Searching collections
* Basic input validation
* Inventory/stock management logic
* Transaction and transaction-detail relationships

---

# 🚀 Future Improvements

There are several things I would like to improve as I continue learning .NET:

* [ ] Connect the application to a relational database
* [ ] Implement Entity Framework Core
* [ ] Implement proper CRUD operations
* [ ] Improve input validation
* [ ] Prevent duplicate product IDs
* [ ] Improve transaction ID generation
* [ ] Separate business logic from the console UI
* [ ] Improve error handling
* [ ] Add product update/delete functionality
* [ ] Add transaction history filtering
* [ ] Create a web-based version using ASP.NET Core

---

# 🛠️ Technologies

* **C#**
* **.NET**
* **Object-Oriented Programming**
* **Generic Collections (`List<T>`)**

---

## 📚 Project Status

**Learning Project — Version 1**

This project is part of my journey learning **C# and .NET**, starting from console applications and gradually moving toward database-backed and web-based applications.
