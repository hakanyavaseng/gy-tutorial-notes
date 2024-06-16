using System.Runtime.InteropServices;
using System.Security;

Console.WriteLine();

#region Usage of SecureString

string bankCardNumber = "1234 5678 9012 3456";
SecureString secureBankCardNumber = new();

foreach (char c in bankCardNumber)
    secureBankCardNumber.AppendChar(c);

secureBankCardNumber.MakeReadOnly();

//secureBankCardNumber.AppendChar('7'); // Throws InvalidOperationException because it is read-only after line 13
#endregion

#region Getting the value of SecureString

//First way
IntPtr bstr = Marshal.SecureStringToBSTR(secureBankCardNumber);
var value = Marshal.PtrToStringUni(bstr);
Console.WriteLine(value);

//Second way
IntPtr bstr2 = Marshal.SecureStringToBSTR(secureBankCardNumber);
var value2 = Marshal.PtrToStringAuto(bstr2);
Console.WriteLine(value2);

//Third way
IntPtr bstr3 = Marshal.SecureStringToBSTR(secureBankCardNumber);
char[] chars = new char[secureBankCardNumber.Length];
Marshal.Copy(bstr3, chars, 0, secureBankCardNumber.Length);
string value3 = string.Join("", chars);
Marshal.ZeroFreeBSTR(bstr3);
Console.WriteLine(value3);
#endregion