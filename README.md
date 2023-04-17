# MYSQL Database Connector for ZTZBX Framework

To build it, run `build.cmd`. To run it, run the following commands to make a symbolic link in your server data directory:

```dos
cd /d [PATH TO THIS RESOURCE]
mklink /d X:\cfx-server-data\resources\[local]\fivem_mysql dist
```

Afterwards, you can use `ensure fivem_mysql` in your server.cfg or server console to start the resource.

## Guide

### Config your DataBase
In `Config.yaml` you will find a series of parameters where you can configure the database where you want to connect

```yaml
hostname: "localhost"
port: 3306
username: "root"
password: ""
database: "test"
```

### How to use it
In your script you need to add the dependency in the `fxmanifest.lua`

```json
dependencies {
    "fivem-mysql"
}
```

#### Raw Query 
This will return a matrix like this, without the headers:

|user_id|name|age|
|--------------|-----------|------------|
|1|Plablo|18|
|2|Emilio|21|

Lets try to get the data and get the first row.`

Sintaxis: 

`IEnumerable<string[]> raw(string query)`
```c#
dynamic result = Exports["fivem-mysql"].raw("SELECT * FROM test");
/*
return
|1|Plablo|18|
|2|Emilio|21|
*/
string[] first_row = result[0]
// return {1,Plablo,18}
string first_row_user_id = result[0][0]
// return 1
````
###

#### Select Query
This is an automatic generator to easy create a select query

Sintaxis:

`IEnumerable<string[]> select(string tableName, List<string> tableAtributes, string condition = "")`
```c#
List<string> atributes = new List<string> {"user_id", "name"};
dynamic result = Exports["fivem-mysql"].select(
    "test",
    atributes);
/*
return
|1|Plablo|
|2|Emilio|
*/
string[] first_row = result[0]
// return {1,Plablo}
string first_row_user_id = result[0][0]
// return 1
```

#### Insert Query
This is an automatic generator to easy create a insert query

Sintaxis:

`void insert(string tableName, List<string> tableAtributes, List<List<string>> rows)`

```c#
List<List<string>> rows_to_add = new List<List<string>> { };
List<string> new_line_1 = new List<string> { "'Fabio'", "30" };
List<string> new_line_2 = new List<string> { "'Alonso'", "29" };
            
rows_to_add.Add(new_line_1);
rows_to_add.Add(new_line_2);

Exports["fivem-mysql"].insert("test", new List<string> { "name", "age" }, rows_to_add);
```
If the query is works fine, will return void.



