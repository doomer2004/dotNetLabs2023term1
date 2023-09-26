using System.Numerics;
using System.Threading.Channels;
using DynamicList;

var vector = new DynamicList<float>(5)
{
    1f,
    2f,
    3f,
    4f,
    5f
};

foreach (var i in vector)
{
    Console.WriteLine(i);
}