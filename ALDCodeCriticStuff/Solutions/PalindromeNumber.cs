namespace ALDCodeCriticStuff.Solutions;

public static class PalindromeNumber
{
    public static void GetResult()
    {
        while (true)
        {
            var numString = Console.ReadLine() ?? string.Empty;
            if (numString is "-1") return;

            var numCharArray = numString.ToArray();
            var num = new int[numString.Length];
            
            for(var digitI = 0; digitI < numCharArray.Length; digitI++)           
            {
                num[digitI] = int.Parse(numCharArray[digitI].ToString());
            }
            GetPalindrome(num);
        }
    }

    private static void GetPalindrome(IList<int> num)
    {
        if(num.All(digit => digit == 9))
        {
            var palindrome = "1";
            for (var i = 0; i < num.Count - 1; i++)
            {
                palindrome += "0";
            }

            palindrome += "1";
            Console.WriteLine(palindrome);
            return;
        }
        GetNextPalindrome(num);
        Console.WriteLine(num.Aggregate("", (current, digit) => current + digit));
    }

    private static void GetNextPalindrome(IList<int> num)
    {
        var mid = num.Count / 2; 
        var leftSideIndex = mid - 1; // starts at the end of left side
        var rightSideIndex = (num.Count % 2 == 0) ? mid : mid + 1; // starts at the start of right side
        
        // getting to the part where the number stops being symmetric (goes from the middle)
        while (leftSideIndex >= 0 && num[leftSideIndex] == num[rightSideIndex])
        {
            leftSideIndex--;
            rightSideIndex++;
        }

        var leftIsSmaller = leftSideIndex < 0 || num[leftSideIndex] < num[rightSideIndex];
        
        while (leftSideIndex >= 0)
        {
            num[rightSideIndex++] = num[leftSideIndex--];
        }
         
        
        if (!leftIsSmaller) return;
        var carry = 1;
        
        if ((num.Count & 1) != 0)
        {
            num[mid] += 1;
            carry = num[mid] / 10;
            num[mid] %= 10;
        }
        leftSideIndex = mid - 1;
        rightSideIndex = (num.Count & 1) == 0 ? mid : mid + 1;
             
        
        while (leftSideIndex >= 0)
        {
            num[leftSideIndex] += carry;
            carry = num[leftSideIndex] / 10;
            num[leftSideIndex] %= 10;
            num[rightSideIndex] = num[leftSideIndex];
            leftSideIndex--;
            rightSideIndex++;
        }
    }
}