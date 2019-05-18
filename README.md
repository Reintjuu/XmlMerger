# XmlMerger
An XML file merger, merges multiple files to one.

## Usage
```bash
$ XmlMerger.exe . output.xml
```

or

```
$ XmlMerger.exe “C:\Long Path\To\XML-files” superoutput.xml 
```

## Example
fileA.xml:
```xml
<?xml version="1.0"?>
<root>
  <entry>
    <Description>Test</Description>
    <Date>2019-05-17</Date>
  </entry>
</root>
```

fileB.xml:
```xml
<?xml version="1.0"?>
<root>
  <entry>
    <Description>Test</Description>
    <Date>2019-05-18</Date>
    <ExtraEntry>Content</ExtraEntry>
  </entry>
</root>
```

Merge 'em together.
```bash
$ XmlMerger.exe . output.xml
```

Creates output.xml:
```xml
<?xml version="1.0"?>
<root>
  <entry>
    <Description>Test</Description>
    <Date>2019-05-17</Date>
  </entry>
  <entry>
    <Description>Test</Description>
    <Date>2019-05-18</Date>
    <ExtraEntry>Content</ExtraEntry>
  </entry>
</root>
```
