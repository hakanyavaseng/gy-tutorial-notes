
#region Using Lambda Expression and compiler generated code
Action action = () =>
{
    Console.WriteLine("Function with lambda!");
};

action();

/*
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue | DebuggableAttribute.DebuggingModes.DisableOptimizations)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
[module: RefSafetyRules(11)]

public class Program
{
    [Serializable]
    [CompilerGenerated]
    private sealed class <>c
    {
        public static readonly <>c <>9 = new <>c();

        public static Action <>9__0_0;

        internal void <Main>b__0_0()
        {
            Console.WriteLine("Function with lambda!");
        }
    }

    [NullableContext(1)]
    private static void Main(string[] args)
    {
        Action action = <>c.<>9__0_0 ?? (<>c.<>9__0_0 = new Action(<>c.<>9.<Main>b__0_0));
        action();
    }
}

*/

#endregion

#region Using Static Anonymous Functions to Avoid Memory Allocation
int a = 5;
Action action1 = static () =>
{
    Console.WriteLine(a); // A static anonymous funciton cannot contain a reference to 'a'
};

action1();

#endregion

