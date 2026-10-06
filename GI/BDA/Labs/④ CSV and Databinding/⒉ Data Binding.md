In the first part, we just managed to load a CSV file into a list. But for now, the data exists only in memory; there’s no indication in the user interface that the read was successful. We can be glad if we didn’t run into a runtime error. We’ll handle the runtime errors later. 

## Step 4: Displaying data in a `DataGridView`

Place a `DataGridView` control on Form1. 

In Code, it’s very easy to get the content of our `powerUsages` to appear in the data-bound control. 

After the code part that reads the file, insert the following line:

``` csharp
dataGridView1.DataSource = powerUsages;
```

Your form should just look something like this:

![[Pasted image 20261006225244.png]]

## Step 5: Editing data

Please note that you can edit data in the `DataGridView`.  Objects in the underlying list will be updated accordingly. So, if you write another piece of code that saves changes to a CSV file, changes made by the user can be persisted. 

If you replace your `List` with a `BindingList` changes made in the list from code will be updated on the user interface automatically. Using a `BindingList` you can even add new objects to the list. In other words, you can append rows to your table. 

## Step 6: Handling runtime errors

Handling files means potential danger to life. You can never be sure whether a file operation succeeds or not. For example if you open your CSV in Excel, Excel just locks the file and if you try to open it from your code, you won't be able to access the locked file. 

Using try-catch blocks, you can handle runtime errors:

``` c#
try
{
    //Code that reads the file    
}
catch (Exception ex)
{
    MessageBox.Show(
        "Could not read the file.\n\n" + ex.Message,        
    );
}
```

When an error occurs, C# creates an **exception object** containing information about the error.

The variable `ex` refers to this object:

``` c#
catch (Exception ex)
```

You can think of it as: `ex` = information about the error

`Message` is a **property** of the exception object. It contains a human-readable description of what went wrong.

For example, if the file does not exist, `ex.Message` might contain something like: `Could not find file 'C:\temp\power_usage.csv'.`

Please add a try-catch block to your code to handle runtime errors! 

## Step 7: OpenFileDialog

Hardcoding the file path is not the best practice. It’s much more user-friendly to let users browse the file system and select the file they want to open. 

``` c#
private void buttonOpen_Click(object sender, EventArgs e)
{
    OpenFileDialog ofd = new OpenFileDialog();
    

    if (ofd.ShowDialog() != DialogResult.OK) return;
    //If we reach here, the user selected a file and clicked OK
    string[] lines = File.ReadAllLines(ofd.FileName);
    ...
}
```

**Breaking it down**

``` c#
OpenFileDialog ofd = new OpenFileDialog();
```

This creates a new `OpenFileDialog` object. 
`OpenFileDialog` is a Windows Forms class that displays the standard **Open File** dialog.
`ofd` is simply the variable name referring to this object.

Just by creating an instance of the `OpenFileDialog` class, the dialog won't be displayed on the screen. You need to call its a `ShowDialog()` method.

`ShowDialog()` is a blocking method: code execution on the UI thread won’t continue until the user picks a file or closes the dialog. `ShowDialog()` returns a value indicating what the user did.

``` c#
if (ofd.ShowDialog() != DialogResult.OK) return;
```

The return type of the `ShowDialog()` method is `DialogResult`. `DialogResult` Is it an enumeration that hosts all the possible buttons and dialog boxes: `DialogResult.OK`, `DialogResult.Cancel`, `DialogResult.Yes`, `DialogResult.No`, `DialogResult.Abort`, `DialogResult.Retry`, `DialogResult.Ignore`, etc. 

So, in that example, if the `ShowDialog()` returns with something other than OK, then we just terminate the execution of the event handler. The code survived this line we can go on processing the file. `ofd.FileName` holds the name of the file including the full path:

``` c#
string[] lines = File.ReadAllLines(ofd.FileName);
```

You can also set the filter for the open file dialog. AI will be happy to help you write the appropriate string:

``` c#
ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
```

Congratulations! You made it through the end of the second part! 😎

Upload your code to Moodle! 