namespace LeetCode;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        NextPermutation([1, 2, 3]);
        NextPermutation([3, 2, 1]);
        NextPermutation([1, 1, 5]);
    }

    static int[] NextPermutation(int[] nums)
    {
        var random = new Random();
        random.Shuffle(nums);
        Console.WriteLine(string.Join(", ", nums));
        return nums;
    }
}
