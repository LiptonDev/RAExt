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
It's very simple. Like this:
```csharp
using RAExt;
...
var db = new RaeDatabase("D:/rae_items.realm");
var item = db.GetItem(11208);
Console.WriteLine(item.Name);
Console.WriteLine(item.ToJson());
db.CloseRealm();
```
# Database sample (rae_items.realm)
Download: https://github.com/Rody66/RAExt/raw/master/rae_items.zip
