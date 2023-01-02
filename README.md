# CommonLibrary
Collection of useful utility classes and methods

## Extensions ##
A static class with several useful extension methods.

### HasProperty ###
A static method for extending Object to return whether the extended class has the specified property.

### TrimAll ###
A static method for extending String to remove all whitespace from the string.

### IsWhiteSpace ###
A static method for extending Char to returns whether it is whitespace.

### ToSqlString ###
A static method for extending String to return the string with any single quotes duplicated so they become literals for SQL use.

### FromSqlString ###
A static method for extending Object to return the string with any duplicated single quotes as a single quote.

### TrimMilliseconds ###
A static method for extending DateTime to return the value with no milliseconds.

### ToList<T> ###
A static method for extending ObservableCollection to return its content as  List<T>.

### Clone ###
A static method for extedning ObservableCollection to support a member-wise clone.

## TextFieldParser ##
A class that immplements the functionality from Microsoft.VisualBasic.FileIO.TextFieldParser which is incompatible with .NET Core 2.0

### SetDelimiters ###
A method to specify the characters to be used as delimiters when identifying the fields in the text.

### GetFields ###
A method that parses a line of text using the specified delimiters and returns a string array.

### ReadFields ###
A method that uses GetFields to read the fields from a Stream.

### HasFieldsEnclosedInQuotes ###
A bool property that specifies/indicates whether the text stream to be read has embedded quotes.

### EndOfData ###
A bool property that returns true when the end of the text stream has been reached.

### Length ###
An int property that returns the count of bytes in the text stream. 

## Setting ##
A class with Name and Value that supports use of the key-value pairs in the .config file of the executable.

## Settings ##
A class derived from IList<Setting> that supports management of the key-value pairs in the .config file of the executable.

## Utility ##
A static class that exposes several useful methods.

### ParseException ###
Interrogates an Exception and drills down to into the inner Exceptions to return them as a formatted string.

### GetCurrentMethod ###
Relies upon StackTrace to return the name of the currently executing method.

### FormatWithComma ###
Format a numeric string with commas.

### StateCodeFromName ###
Return the two-character state abbreviation given the name of the state.

### LoadSettings ###
Load settings stored in the .config file of the executable into a Settings class.

### SaveSettings ###
Write contents of a Settings class to the .config file of the executable.

## BitmapManager ##
A static class for managing bitmaps.

### BitmapToBitmapSource ###
A static method that returns a BitmapSource object from a Bitmap source.

### BitmapSourceToBitmap ###
A static method that returns a Bitmp source from a BitmapSource object.

### BitmapSourceToFile ###
A static method that saves a bitmap as a file.

### BitmapToBytes ###
A static method that converts a bitmap (GIF, PNG, JPEG, or TIFF) to an array of bytes.
