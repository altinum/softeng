# Using SQLite in .NET Projects: A Short Guide

SQLite is a small, serverless SQL database that lives in a single file. There is nothing to install, no server to run, and no passwords to manage. That makes it a great fit for coursework, prototypes, experiment tracking, and small-to-medium datasets.

This guide shows how to use SQLite from C#, starting with a graphical editor and then moving through three code approaches, from low-level to high-level:

0. **Letos**: a free visual editor for creating, inspecting, and querying your `.db` files
1. **Microsoft.Data.Sqlite**: plain ADO.NET, full control
2. **Dapper**: a thin helper that maps rows to objects
3. **Entity Framework Core**: a full ORM, used code-first _or_ database-first (`Scaffold-DbContext` + LINQ)

It finishes with how to feed query results into data analysis and ML code.

---

## 1. Why SQLite for Data Science?

|Benefit|What it means for you|
|---|---|
|Zero setup|One NuGet package, one `.db` file|
|Portable|Email or commit the file; it works anywhere|
|SQL support|Filter, join, and aggregate before loading data into memory|
|Fast for reads|Great for datasets up to a few GB|
|In-memory mode|Perfect for unit tests and throwaway experiments|

**Limits to know about:** SQLite allows only one writer at a time, has no user accounts, and is not designed for many concurrent clients. For multi-user production systems, consider PostgreSQL or SQL Server.

---

## 2. Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) 8.0 or newer
- A code editor (VS Code, Visual Studio, or Rider)
- [Letos](https://letos.org/) to create and inspect your `.db` file visually (see the next section)

## 3. Meet Letos: A Visual Editor for SQLite

[Letos](https://letos.org/) (formerly known as SQLiteStudio) is a free, open-source SQLite manager for Windows, macOS, and Linux. It is portable, so you can run it without installing it or needing administrator rights. Think of it as the "spreadsheet view" of your database: you can look at tables, edit rows, and try out SQL before you put it into C# code.

![[Pasted image 20261123222448.png]]
### What you can do with it

- **Browse and edit data** in a grid, with each table's structure shown alongside.
- **Design tables visually**: add columns, indexes, views, and triggers without writing DDL by hand.
- **Run SQL** in a built-in editor with syntax highlighting and code completion.
- **Import CSV files** into a new table, which is handy for turning a downloaded dataset into a `.db` file in a few clicks.
- **Export** results to formats such as CSV, JSON, SQL, XML, or HTML.
- **Copy tables between databases** by dragging and dropping.

### Suggested workflow

1. Download and start Letos from [letos.org](https://letos.org/).
2. Add your database file (for example `experiments.db`, or a new empty one) to the database list and connect to it.
3. Create tables or import a CSV, then open the table's data view to check that the values look right.
4. Prototype your queries in the SQL editor (for example, the `GROUP BY` query in section 8) and confirm the results.
5. Copy the working SQL into Dapper, or use the schema to scaffold EF Core classes (section 7).

> 💡 **Tip:** Letos and your .NET app both open the same file. Close or disconnect it in Letos before your app writes heavily, and avoid leaving an edit transaction open. Otherwise you may see `database is locked` errors.

### Step 1: Install NuGet packages

EF Core lets you work with C# classes and LINQ instead of SQL strings. In

```bash
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Tools
```

### Step 2: Database-First with Scaffold-DbContext and LINQ

Using the Package Manager Console in Visual Studio (**Tools → NuGet Package Manager → Package Manager Console**), the equivalent command is `Scaffold-DbContext`:

```powershell
Scaffold-DbContext "Data Source=c:\temp\experiments.db" Microsoft.EntityFrameworkCore.Sqlite `
    -OutputDir Models `
    -Context ExperimentsContext `
    -NoOnConfiguring `
    -Force
```

What the options mean:

|Option|Purpose|
|---|---|
|connection string|Points at the existing database file|
|provider|`Microsoft.EntityFrameworkCore.Sqlite`|
|`--output-dir` / `-OutputDir`|Folder for the generated classes|
|`--context` / `-Context`|Name of the generated `DbContext`|
|`--no-onconfiguring` / `-NoOnConfiguring`|Don't hard-code the connection string in the generated class (you supply it yourself)|
|`--force` / `-Force`|Overwrite files from a previous scaffold|
|`--table` / `-Tables`|(Optional) scaffold only the tables you name|

The path in the connection string is relative to where you run the command, so run it from the project folder that contains the `.db` file, or use an absolute path.

### Step 3: Configure the connection and use the context

Because we used `--no-onconfiguring`, pass the options when you create the context:

```csharp
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<ExperimentsContext>()
    .UseSqlite("Data Source=experiments.db")
    .Options;

using var db = new ExperimentsContext(options);
```

### Step 4: Query with LINQ

As usual 😃
## Design Note: Thinking in SQLite's Type System

SQLite has only five storage classes (`NULL`, `INTEGER`, `REAL`, `TEXT`, and `BLOB`), so many types you take for granted in C# have no native counterpart, and your schema has to encode them deliberately. 

There is no `DateTime` or `DateTimeOffset`: choose one representation up front, either ISO-8601 text in UTC (`2026-11-23T10:00:00Z`, human-readable and sorts correctly as text) or a Unix timestamp stored as `INTEGER` (compact and easy to do arithmetic on), and never mix the two in one column. 

There is no `bool` either, so use `INTEGER` with 0 and 1; 

there is no `decimal`, so store money as integer minor units (cents) rather than `REAL`, which is binary floating point and rounds badly; and 

there is no `Guid`, `TimeSpan`, or `enum`, so store them as `TEXT` (or `BLOB` for compact GUIDs), as integer ticks or seconds, and as `INTEGER` codes or `TEXT` names respectively. 

Column types in SQLite are only _affinities_, i.e. hints, so by default the database will happily accept the string `"abc"` in an `INTEGER` column. 

Protect yourself by declaring tables as `STRICT`, adding `CHECK` constraints (for example `CHECK (is_active IN (0, 1))`), and doing the conversions in exactly one place in your code, such as EF Core value converters or a small mapping layer, so the rest of your analysis code works with proper .NET types. 

Finally, write the type conventions down (a short comment in the schema or in your README is enough), because the `.db` file itself will not tell the next person whether a number is seconds or milliseconds, or whether a timestamp is UTC.




