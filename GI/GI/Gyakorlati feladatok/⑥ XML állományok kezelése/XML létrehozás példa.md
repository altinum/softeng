Az alábbi rövid kódrészlet egy példa arra, hogyan kell XML-dokumentumot létrehozni. 

```c#
XDocument xdoc = new XDocument();

* XDeclaration xdecl = new XDeclaration("1.0", "utf-8", "yes");
* xdoc.Declaration = xdecl;

XElement root = new XElement("gyökér");
xdoc.Add(root);

XAttribute testAttribute = new XAttribute("mintaattribútum", "attribótum értéke");
node.Add(testAttribute);

//Igy is lehet
node.SetAttributeValue("mintaattribútum2", "attribótum értéke";

//10 gyermek hozzáadása
for(int i=0;i<10;i++)
{
	XElement childNode = new XElement("child");
	node.Add(childNode);
}
```

