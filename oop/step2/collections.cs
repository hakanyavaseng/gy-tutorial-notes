 //Collections are used to store multiple values in a single variable, instead of declaring separate variables for each value.

    //Difference between Arrays and Collections is that Arrays are fixed size while Collections are dynamic in size.
    //Collections has 3 types: Generic, Non-Generic and Specialized.

    //Generic Collections, System.Collections.Generic. Generic collections are strongly typed collections. Because of this, they are type safe and make more sense to use. Has better performance than non-generic collections.

    //Non-Generic Collections, System.Collections. Non-generic collections are not type safe. They store values as objects and you need to cast them to use it. Has poor performance than generic collections.

    //Specialized Collections, System.Collections.Specialized. Specialized collections are used for special purpose. For example, NameValueCollection, StringCollection, StringDictionary, HybridDictionary, ListDictionary, OrderedDictionary

namespace collections
{

    class Collections 
    {
        //*List<T> is a generic collection. It is used to store elements. It is similar to ArrayList but it is type safe. It can store elements of specific type only. It inherits IList<T> interface.

        //*Dictionary<TKey, TValue> is a generic collection. It is used to store key/value pairs. It is similar to Hashtable but it is type safe. It can store key/value pairs of specific types only. It inherits IDictionary<TKey, TValue> interface. Keys must be unique and cannot be null. Values can be null or duplicate.

        //*Queue<T> is a generic collection. It is used to store elements in FIFO (First In First Out) order. It inherits IEnumerable<T> interface. 

        //Stack<T> is a generic collection. It is used to store elements in LIFO (Last In First Out) order. It inherits IEnumerable<T> interface. It is used when you want to access the elements in reverse order.

        //SortedList<TKey, TValue> is a generic collection. It is used to store key/value pairs in sorted order. It is similar to Dictionary<TKey, TValue> but it is sorted. IComperable<T> interface is used to sort the elements.

        //SortedDictionary<TKey, TValue> is a generic collection. It is used to store key/value pairs in sorted order. It is similar to SortedList<TKey, TValue> but it is sorted. It is faster than SortedList<TKey, TValue> because it uses less memory.

        //ObservableCollection<T> is a generic collection. It is used to provide notifications when items get added, removed or refreshed. It is similar to List<T> but it provides notifications when items get added, removed or refreshed.

        //ArrayList is a non-generic collection. It is used to store elements. It can store elements of any type. It holds data as object. It is not type safe. It is slower than generic collections because boxing and unboxing is  required.

        //HybridDictionary is a specialized collection. It is used to store key/value pairs. It changes its internal structure depending on the number of elements in the collection. It uses Hashtable for large number of elements and ListDictionary for small number of elements.

        









    



    }























}
