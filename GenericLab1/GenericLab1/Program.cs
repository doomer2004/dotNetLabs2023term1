using System.Numerics;
using System.Threading.Channels;
using DynamicList;
using DynamicList.CustomEventArgs;

var lst = new DynamicList<int>(4);
lst.ItemAdded += PrintArrayItemEventArgs!;
lst.ItemRemoved  += PrintArrayItemEventArgs!;
lst.DynamicListCleared += (sender, e) => Console.WriteLine($"Event invoked: {e.Action} {e.ActionTime}");
lst.DynamicListResized += (sender, e) =>
    Console.WriteLine($"Event invoked: {e.Action} {e.ActionTime} Old capacity: {e.OldCapacity}- New capacity: {e.NewCapacity}");
Console.WriteLine("Insert to array");
lst.Insert(0, 1);
lst.Insert(1, 2);
lst.Insert(2, 3);
lst.Insert(3, 4);
OutputCollection(lst);
Console.WriteLine("Remove from array");
lst.RemoveAt(3);
Console.WriteLine($"Contains check: {lst.Contains(1)}");
Console.WriteLine("Copy to array");
var lst2 = new int[]{5, 6, 7, 8, 9, 10, 0, 0, 0}; 
lst.CopyTo(lst2 , 4);
OutputCollection(lst);
OutputCollection(lst2);
Console.WriteLine("Clear array");
lst.Clear();
Console.WriteLine("Length of array: " + lst.Count);
Console.ReadLine();


void PrintArrayItemEventArgs(object sender, ArrayItemEventArgs<int> e)
{
    Console.WriteLine($"Event invoked: {e.Action} {e.ActionTime} [{e.Item} : {e.Index}]");
}

void OutputCollection(IEnumerable<int> collection)
{
    foreach (var item in collection)
    {
        Console.WriteLine(item);
    }
}