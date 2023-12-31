﻿using DynamicList;
using DynamicList.CustomEventArgs;

var lst = new DynamicList<int>(4);

lst.ArrayChanged += PrintArrayItemEventArgs;
Console.WriteLine("Insert to array");
lst.Insert(0, 1);
lst.Insert(1, 2);
lst.Insert(2, 3);
lst.Insert(3, 4);
lst.Insert(3, 4);
OutputCollection(lst);
Console.WriteLine("Remove from array");
lst.RemoveAt(3);
Console.WriteLine($"Contains check: {lst.Contains(1)}");
Console.WriteLine("Copy to array");
var lst2 = new int[]{5, 6, 7, 8, 9, 10, 0, 0, 0}; 
lst.CopyTo(lst2 , 1);
OutputCollection(lst);
OutputCollection(lst2);
Console.WriteLine("Clear array");
lst.Clear();
Console.WriteLine("Length of array: " + lst.Count);
Console.ReadLine();


void PrintArrayItemEventArgs<T>(NotifyArrayChangedEventArgs<T> e)
{
    var message = e.Action switch
    {
        ArrayAction.Add
            => $"Event invoked: {e.Action} {e.ActionTime} Item: {e.Item} Index: {e.Index}",
        ArrayAction.Clear => $"Event invoked: {e.Action} {e.ActionTime}",
        ArrayAction.Remove => $"Event invoked: {e.Action} {e.ActionTime} Item: {e.Item} Index: {e.Index}",
        ArrayAction.Resize =>
            $"Event invoked: {e.Action} {e.ActionTime} Old Capacity: {e.OldCapacity} New Capacity: {e.NewCapacity}",
        _ => throw new ArgumentOutOfRangeException()
    };
    Console.WriteLine(message);
}

void OutputCollection(IEnumerable<int> collection)
{
    foreach (var item in collection)
    {
        Console.WriteLine(item);
    }
}