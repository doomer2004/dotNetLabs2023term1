using System.Threading.Channels;
using DynamicList;

void OutputCollection(IEnumerable<int> collection)
{
    foreach (var item in collection)
    {
        Console.WriteLine(item);
    }

    Console.WriteLine();
}
void ItemRemoved() => Console.WriteLine("Item removed");
void CollectionCleared() => Console.WriteLine("Collection Cleared");
void CollectionResized() => Console.WriteLine("Collection Resized");

var lst = new DynamicList<int>(4);
lst.ItemRemoved += ItemRemoved;
lst.CollectionCleared += CollectionCleared;
lst.CollectionResized += CollectionResized;
lst.Add(1);
lst.Add(2);
lst.Add(3);
lst.Add(4);
lst.Add(5);

Console.WriteLine($"Contains check: {lst.Contains(3)}");

OutputCollection(lst);