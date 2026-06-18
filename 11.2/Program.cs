namespace Assignment_11._2
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Assignment 11.2");
            Console.WriteLine();

            RunStockProfitExamples();
            Console.WriteLine();
            RunLinkedListExamples();
        }

        private static void RunStockProfitExamples()
        {
            Console.WriteLine("Problem 1: Best Time to Buy and Sell Stock");

            int[] exampleOne = { 7, 1, 5, 3, 6, 4 };
            int[] exampleTwo = { 7, 6, 4, 3, 1 };

            Console.WriteLine($"Input: [{string.Join(",", exampleOne)}]");
            Console.WriteLine($"Output: {MaxProfit(exampleOne)}");

            Console.WriteLine($"Input: [{string.Join(",", exampleTwo)}]");
            Console.WriteLine($"Output: {MaxProfit(exampleTwo)}");
        }

        public static int MaxProfit(int[] prices)
        {
            int lowestPrice = int.MaxValue;
            int maximumProfit = 0;

            foreach (int price in prices)
            {
                if (price < lowestPrice)
                {
                    lowestPrice = price;
                }

                int profit = price - lowestPrice;
                if (profit > maximumProfit)
                {
                    maximumProfit = profit;
                }
            }

            return maximumProfit;
        }

        private static void RunLinkedListExamples()
        {
            Console.WriteLine("Problem 2: Reverse Singly Linked List");

            ListNode? exampleOne = BuildList(new[] { 1, 2, 3, 4, 5 });
            ListNode? exampleTwo = BuildList(new[] { 1, 2 });
            ListNode? exampleThree = BuildList(Array.Empty<int>());

            PrintReverseExample(exampleOne);
            PrintReverseExample(exampleTwo);
            PrintReverseExample(exampleThree);
        }

        public static ListNode? ReverseList(ListNode? head)
        {
            ListNode? previous = null;
            ListNode? current = head;

            while (current is not null)
            {
                ListNode? nextNode = current.Next;
                current.Next = previous;
                previous = current;
                current = nextNode;
            }

            return previous;
        }

        private static void PrintReverseExample(ListNode? head)
        {
            string input = ListToString(head);
            ListNode? reversed = ReverseList(head);
            string output = ListToString(reversed);

            Console.WriteLine($"Input: {input}");
            Console.WriteLine($"Output: {output}");
        }

        private static ListNode? BuildList(int[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }

            ListNode head = new(values[0]);
            ListNode current = head;

            for (int index = 1; index < values.Length; index++)
            {
                current.Next = new ListNode(values[index]);
                current = current.Next;
            }

            return head;
        }

        private static string ListToString(ListNode? head)
        {
            List<int> values = new();
            ListNode? current = head;

            while (current is not null)
            {
                values.Add(current.Value);
                current = current.Next;
            }

            return $"[{string.Join(",", values)}]";
        }
    }

    public class ListNode
    {
        public int Value { get; set; }
        public ListNode? Next { get; set; }

        public ListNode(int value)
        {
            Value = value;
        }
    }
}
