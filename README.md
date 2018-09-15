# RAExt
Rody's Angelica Editor Extensions Library

# How to install
After the installation create a file FodyWeavers.xml in your project root directory. For example into:
```
\source\repos\ExampleApp1\FodyWeavers.xml
```
With the next content:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Weavers>
	<RealmWeaver/>
</Weavers>
```

# Example
It's very simple. 
1. Add this to the file header:
```csharp
using RAExt;
```
2. Open a database:
```csharp
var db = new RaeDatabase("D:/rae_items.realm");
```
3. Get items:
```csharp
var item = db.GetItem(11208);
//test:
Console.WriteLine(item.Name);
Console.WriteLine(item.ToJson());
```
4. Don't forget to close realm file when your app closing or you don't need any items anymore:
```csharp
db.CloseRealm();
```

# Database sample (rae_items.realm)
Download: https://github.com/Rody66/RAExt/raw/master/rae_items.zip
