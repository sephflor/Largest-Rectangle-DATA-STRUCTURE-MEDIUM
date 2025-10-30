using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'largestRectangle' function below.
     *
     * The function is expected to return a LONG_INTEGER.
     * The function accepts INTEGER_ARRAY h as parameter.
     */

    public static long largestRectangle(List<int> h)
    {
        Stack<int> stack = new Stack<int>();
        long maxArea = 0;
        int n = h.Count;
        
        for (int i = 0; i <= n; i++) {
            int currentHeight = (i == n) ? 0 : h[i];
            
            while (stack.Count > 0 && currentHeight < h[stack.Peek()]) {
                int height = h[stack.Pop()];
                int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
                maxArea = Math.Max(maxArea, (long)height * width);
            }
            
            stack.Push(i);
        }
        
        return maxArea;
    }

    // Alternative approach with explicit left and right boundaries
    public static long largestRectangleAlternative(List<int> h) {
        int n = h.Count;
        int[] left = new int[n];
        int[] right = new int[n];
        Stack<int> stack = new Stack<int>();
        
        // Calculate left boundaries
        for (int i = 0; i < n; i++) {
            while (stack.Count > 0 && h[stack.Peek()] >= h[i]) {
                stack.Pop();
            }
            left[i] = stack.Count == 0 ? -1 : stack.Peek();
            stack.Push(i);
        }
        
        stack.Clear();
        
        // Calculate right boundaries
        for (int i = n - 1; i >= 0; i--) {
            while (stack.Count > 0 && h[stack.Peek()] >= h[i]) {
                stack.Pop();
            }
            right[i] = stack.Count == 0 ? n : stack.Peek();
            stack.Push(i);
        }
        
        // Calculate maximum area
        long maxArea = 0;
        for (int i = 0; i < n; i++) {
            int width = right[i] - left[i] - 1;
            maxArea = Math.Max(maxArea, (long)h[i] * width);
        }
        
        return maxArea;
    }

    }
class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> h = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(hTemp => Convert.ToInt32(hTemp)).ToList();

        long result = Result.largestRectangle(h);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
