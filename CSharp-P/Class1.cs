using DataStructures;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization.Formatters;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSharp_P
{
    /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    public class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode first = new ListNode();
            ListNode curr = first;
            int val1 = 0;
            int val2 = 0;
            int add = 0;
            while (l1 != null || l2 != null || add != 0)
            {
                val1 = l1 == null ? 0 : l1.val;
                val2 = l2 == null ? 0 : l2.val;

                curr.next = new ListNode();
                curr = curr.next;

                if (val1 + val2 + add >= 10)
                {
                    curr.val = val1 + val2 + add - 10;
                    add = 1;
                }
                else
                {
                    curr.val = val1 + val2 + add;
                    add = 0;
                }

                if (l1 != null)
                {
                    l1 = l1.next;
                }
                if (l2 != null)
                {

                    l2 = l2.next;
                }
            }
            return first;
        }

        public IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);
            List<IList<int>> list = new List<IList<int>>();
            int left = 0;
            int right = nums.Length - 1;
            for (int i = 0; i < nums.Length - 2; i++)
            {
                int find = 0 - nums[i];
                left = i + 1;
                while (left < right)
                {
                    if ((nums[left] + nums[right] + nums[i]) > 0)
                    {
                        right--;
                    }
                    else if ((nums[left] + nums[right] + nums[i]) < 0)
                    {
                        left++;
                    }
                    else
                    {
                        if (list.Find(p => p[0] == nums[i] && p[1] == nums[left]) == null)
                        {
                            list.Add(new List<int> { nums[i], nums[left], nums[right] });
                        }
                        left++;
                        right--;
                        while (left + 1 < right && nums[left + 1] == nums[left])
                        {
                            left++;
                        }
                        while (left + 1 < right && nums[right - 1] == nums[right])
                        {
                            right--;
                        }
                    }
                }
            }
            return list;
        }

        public int LongestValidParentheses(string s)
        {
            Stack<char> stack = new Stack<char>();
            int max = 0;
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    stack.Push(s[i]);
                }
                else if (s[i] == ')')
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                        count += 2;
                        if (count > max)
                        {
                            max = count;
                        }
                    }
                    else
                    {
                        if (count > max)
                        {
                            max = count;
                        }
                        count = 0;
                    }
                }
            }
            return max;
        }

        public int FindNthDigit(int n)
        {
            int i = 1;
            long left = 0;
            long right = 0;
            while (true)
            {
                right += 9 * (Convert.ToInt64(Math.Pow(10, i - 1))) * i;
                if (right >= n)
                {
                    break;
                }
                left = right;
                i++;
            }
            int start = Convert.ToInt32(Math.Pow(10, i - 1)) - 1;
            long res = start + (n - left) / i;
            int index = Convert.ToInt32((n - left) % i);
            if (index == 0)
            {
                return Convert.ToInt32(res.ToString().Last().ToString());
            }
            else
            {
                return Convert.ToInt32((res + 1).ToString().Substring((index - 1), 1));
            }
        }

        public int FindKthNumber(int n, int k)
        {
            Test(n, k);
            int nIndex = MaxIndex(n, 0);

            if (k <= nIndex)
            {
                int singleSum = SingleSum(n.ToString().Length);
                int first = k / singleSum + 1;
                int surplus = k % singleSum;
                if (surplus == 0)
                {
                    int r = first * Convert.ToInt32(Math.Pow(10, n.ToString().Length - 1)) - 1;
                    Console.WriteLine(r);
                    return r;
                }
                else
                {
                    string s = GetSurplus(surplus, n.ToString().Length - 1, "");
                    int r = Convert.ToInt32((first.ToString() + s));
                    Console.WriteLine(r);
                    return 1;
                }

            }
            else
            {
                int over = k - nIndex;
                n = n / 10;
                int r = MaxIndex(n, 0);
                r = r + over;
                return FindKthNumber(Convert.ToInt32(Math.Pow(10, n.ToString().Length)) - 1, r);
            }
        }
        private string GetSurplus(int n, int len, string str)
        {
            n = n - 1;
            if (n < len)
            {
                for (int i = 0; i < n; i++)
                {
                    str += "0";
                }
                return str;
            }
            if (len <= 0)
            {
                return str;
            }

            int singleSum = SingleSum(len--);
            int curr = n / singleSum;
            int next = n % singleSum;
            if (singleSum == 1)
            {
                curr--;
            }
            str += curr.ToString();
            if (next == 0 && len != 0)
            {
                str = (Convert.ToInt32(str) * Math.Pow(10, len) - 1).ToString();
                len = 0;

            }
            return GetSurplus(next, len, str);
        }

        private int MaxIndex(int n, int sum)
        {
            sum += Sum(n.ToString().Length, Convert.ToInt32(n.ToString().First().ToString()));

            int x = n % Convert.ToInt32(Math.Pow(10, n.ToString().Length - 1));

            int length = n.ToString().Length;

            for (int i = 0; i < length; i++)
            {
                sum += x / Convert.ToInt32(Math.Pow(10, i)) + 1;
            }

            return sum;
        }

        private int Sum(int length, int first)
        {
            return SingleSum(length) * (first - 1);
        }

        private int SingleSum(int length)
        {
            int x = 0;
            for (int i = 0; i < length; i++)
            {
                x += Convert.ToInt32(Math.Pow(10, i));
            }
            return x;
        }

        private void Test(int n, int k)
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= n; i++)
            {
                list.Add(i.ToString());
            }
            list.Sort();
            Console.WriteLine("k=" + k + " =" + list[k - 1]);
            int x = 1;
            for (int i = 0; i < list.Count; i++)
            {
                string s = "";
                if (i >= 2222 && i < 3333)
                {
                    s = x++.ToString();
                }
                Console.WriteLine("No " + (i + 1) + "=" + list[i] + "      " + s);
            }
        }

        public string KthLargestNumber(string[] nums, int k)
        {
            Shuffle(ref nums);
            k = nums.Length - k;
            int lo = 0;
            int hi = nums.Length - 1;
            while (hi > lo)
            {
                int j = Partition(nums, lo, hi);
                if (j < k) lo = j + 1;
                else if (j > k) hi = j - 1;
                else
                {
                    string a = nums[k];
                    return a;
                }
            }
            string s = nums[k];
            return s;
        }
        private void Shuffle(ref string[] pokeArr)
        {
            Random myRandom = new Random();
            for (int i = 0; i < pokeArr.Length; i++)
            {
                string temp = pokeArr[i];
                int randomIndex = myRandom.Next(pokeArr.Length);
                pokeArr[i] = pokeArr[randomIndex];
                pokeArr[randomIndex] = temp;
            }
        }

        int count = 0;

        private int Partition(string[] a, int lo, int hi)
        {
            int i = lo, j = hi + 1;
            while (true)
            {
                while (Compare(a, ++i, lo) < 0)
                    if (i == hi) break;
                while (Compare(a, lo, --j) < 0)
                    if (j == lo) break;

                if (i >= j) break;
                Exch(a, i, j);
            }
            Exch(a, lo, j);
            return j;
        }

        private void Exch(string[] nums, int i, int j)
        {
            string temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        private int Compare(string[] nums, int i, int j)
        {
            count++;
            if (nums[i].Length < nums[j].Length)
            {
                return -1;
            }
            else if (nums[i].Length > nums[j].Length)
            {
                return 1;
            }
            else
            {
                for (int k = 0; k < nums[i].Length; k++)
                {
                    if (nums[i][k] < nums[j][k])
                    {
                        return -1;
                    }
                    else if (nums[i][k] > nums[j][k])
                    {
                        return 1;
                    }
                    else
                    {
                        continue;
                    }
                }
                return 0;
            }
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode last = head;
            ListNode before = head;

            for (int i = 0; i < n - 1; i++)
            {
                last = last.next;
            }

            if (last.next == null)
            {
                return head.next;
            }

            last = last.next;

            while (last.next != null)
            {
                last = last.next;
                before = before.next;
            }

            before.next = before.next.next;
            return head;
        }

        public int[][] Merge(int[][] intervals)
        {
            ArrayComparer ac = new ArrayComparer();
            Array.Sort(intervals, ac);
            Stack<int[]> stack = new Stack<int[]>();
            for (int i = 0; i < intervals.Length; i++)
            {
                int[] curr = intervals[i];
                while (stack.Count != 0)
                {
                    if (needMerge(stack.Peek(), curr))
                    {
                        int[] top = stack.Pop();
                        top[0] = Math.Min(top[0], curr[0]);
                        top[1] = Math.Max(top[1], curr[1]);
                        curr = top;
                    }
                    else
                    {
                        break;
                    }
                }
                stack.Push(curr);
            }
            return stack.ToArray();
        }

        private bool needMerge(int[] nums1, int[] nums2)
        {
            if ((nums1[1] >= nums2[0] && nums1[1] <= nums2[1])
                    || (nums2[1] >= nums1[0] && nums2[1] <= nums1[1]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Divide(int dividend, int divisor)
        {
            if (divisor == int.MinValue)
            {
                if (dividend == int.MinValue)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            bool isMin = false;
            if (dividend == int.MinValue)
            {
                if (divisor == -1)
                {
                    return int.MaxValue;
                }
                else if (divisor == 1)
                {
                    return int.MinValue;
                }
                dividend = dividend + 1;
                isMin = true;
            }
            if (divisor < 0 && dividend > 0)
            {
                return 0 - Divide(dividend, 0 - divisor, 0, isMin);
            }
            if (dividend < 0 && divisor > 0)
            {
                return 0 - Divide(0 - dividend, divisor, 0, isMin);
            }
            if (dividend < 0 && divisor < 0)
            {
                return Divide(0 - dividend, 0 - divisor, 0, isMin);
            }

            return Divide(dividend, divisor, 0);
        }

        private int Divide(int dividend, int divisor, int count, bool isMin = false)
        {
            if (dividend < divisor)
            {
                if (isMin && dividend + 1 == divisor)
                {
                    count = count + 1;
                }
                return count;
            }

            int nextSum = divisor;
            int currSum = nextSum;
            int nextCount = 1;
            int currCount = nextCount;
            while (dividend >= nextSum)
            {
                currSum = nextSum;
                currCount = nextCount;
                if (nextSum >= Math.Pow(2, 30))
                {
                    break;
                }
                nextSum += nextSum;
                nextCount += nextCount;
            }
            dividend -= currSum;

            return Divide(dividend, divisor, count + currCount, isMin);
        }

        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0)
                return null;
            ListNode curr = new ListNode();
            ListNode head = curr;
            ListNodeComparer lc = new ListNodeComparer();
            MinPQ<ListNode> minPQ = new MinPQ<ListNode>(lists.Length, lc);
            for (int i = 0; i < lists.Length; i++)
            {
                minPQ.Insert(lists[i]);
            }
            while (true)
            {
                ListNode node = minPQ.DelMin();
                if (node == null)
                {
                    break;
                }
                curr.next = node;
                curr = curr.next;
                if (node.next != null)
                {
                    minPQ.Insert(node.next);
                }
            }
            return head.next;
        }

        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            if (intervals.Length == 0)
            {
                return new int[][] { newInterval };
            }
            int start = 0;
            int end = 0;
            for (int i = 0; i < intervals.Length; i++)
            {
                if (newInterval[0] <= intervals[i][1])
                {
                    start = i;
                    break;
                }
            }
            while (end < intervals.Length)
            {
                if (newInterval[1] >= intervals[end][1])
                {
                    end++;
                }
            }
            int[][] temp = new int[end - start + 2][];
            temp[0] = newInterval;
            Array.Copy(intervals, start, temp, 1, end - start + 1);
            int[][] mer = Merge(temp);
            int len = start + mer.Length + (intervals.Length - end - 1);
            int[][] res = new int[len][];
            Array.Copy(intervals, 0, res, 0, start);
            Array.Copy(temp, 0, res, start, mer.Length);
            if (end < intervals.Length - 1)
            {
                Array.Copy(intervals, end + 1, res, start + mer.Length, intervals.Length - end - 1);
            }
            return res;
        }

        public IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
        {
            IList<TreeNode> treeNodes = new List<TreeNode>();
            PreDelete(root, to_delete, ref treeNodes);
            if (!to_delete.Contains(root.val))
            {
                treeNodes.Add(root);
            }
            return treeNodes;
        }

        private TreeNode PreDelete(TreeNode root, int[] to_delete, ref IList<TreeNode> treeNodes)
        {
            if (root == null)
            {
                return null;
            }
            if (to_delete.Contains(root.val))
            {
                TreeNode left = PreDelete(root.left, to_delete, ref treeNodes);
                if (left != null)
                {
                    treeNodes.Add(left);
                }
                TreeNode right = PreDelete(root.right, to_delete, ref treeNodes);
                if (right != null)
                {
                    treeNodes.Add(right);
                }
                return null;
            }
            root.left = PreDelete(root.left, to_delete, ref treeNodes);
            root.right = PreDelete(root.right, to_delete, ref treeNodes);
            return root;
        }

        public string LongestPalindrome(string s)
        {
            int max = 1;
            int start = 0;
            int i = 1;
            while (i < s.Length)
            {
                int cmp = i - max;
                int newStart = 0;
                int max1 = 0;
                int max2 = 0;
                if (isInnerCycle(s, cmp, i))
                {
                    max1 = ExtendCycle(s, cmp, i, out newStart);
                }
                if (isInnerCycle(s, cmp - 1, i))
                {
                    max2 = ExtendCycle(s, cmp - 1, i, out newStart);
                }
                if (Math.Max(max1, max2) > max)
                {
                    max = Math.Max(max1, max2);
                    start = newStart;
                    i = newStart + max;
                }
                else
                {
                    i++;
                }
            }
            return s.Substring(start, max);
        }

        private int ExtendCycle(string s, int start, int end, out int nStart)
        {
            while (start >= 0 && end < s.Length && s[start] == s[end])
            {
                start--;
                end++;
            }
            nStart = start + 1;
            return end - start - 1;
        }

        private bool isInnerCycle(string s, int start, int end)
        {
            if ((end - start) % 2 == 0)
            {
                return IsSingleCycle(s, start, end);
            }
            else
            {
                return IsDoubleCycle(s, start, end);
            }
        }

        private bool IsSingleCycle(string s, int start, int end)
        {
            while (start >= 0 && end < s.Length && s[start] == s[end])
            {
                if (start == end)
                {
                    return true;
                }
                start++;
                end--;
            }
            return false;
        }

        private bool IsDoubleCycle(string s, int start, int end)
        {
            while (start >= 0 && end < s.Length && s[start] == s[end])
            {
                if (start == end - 1)
                {
                    return true;
                }
                start++;
                end--;
            }
            return false;
        }

        public IList<int> PreorderTraversal(TreeNode root)
        {
            IList<int> ints = new List<int>();
            PreOrder(root, ints);
            return ints;
        }

        private void PreOrder(TreeNode root, IList<int> ints)
        {
            if (root == null)
            {
                return;
            }
            ints.Add(root.val);
            PreOrder(root.left, ints);
            PreOrder(root.right, ints);
        }

        public string serialize1(TreeNode root)
        {
            List<string> nums = new List<string>();
            Lever(root, ref nums);
            return ConvertArray2Str(nums);
        }

        public TreeNode deserialize1(string data)
        {
            List<string> nums = ConvertStr2Array(data);
            TreeNode treeNode = DeepOrder(nums, nums.Count, 0);
            return treeNode;
        }

        private List<string> ConvertStr2Array(string s)
        {
            List<string> nums = new List<string>();
            string[] strs = s.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                nums.Add(strs[i]);
            }
            return nums;
        }

        private string ConvertArray2Str(List<string> nums)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var num in nums)
            {
                sb.Append($"{num},");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private void Lever(TreeNode root, ref List<string> nums)
        {
            Queue<TreeNode> Q = new Queue<TreeNode>();
            TreeNode T;
            T = root;
            if (T != null)
            {
                Q.Enqueue(T);
            }
            while (Q.Count > 0)
            {
                T = Q.First();
                if (T == null)
                {
                    nums.Add("null");
                    continue;
                }
                else
                {
                    nums.Add(T.val.ToString());
                }
                Q.Dequeue();
                if (T.left != null)
                {
                    Q.Enqueue(T.left);
                }
                else
                {
                    Q.Enqueue(null);
                }
                if (T.right != null)
                {
                    Q.Enqueue(T.right);
                }
                else
                {
                    Q.Enqueue(null);
                }
            }
        }

        private TreeNode DeepOrder(List<string> nums, int len, int i)
        {
            if (i >= len)
            {
                return null;
            }
            if (nums[i] == "null")
            {
                return null;
            }
            TreeNode node = new TreeNode();
            node.val = Convert.ToInt32(nums[i]);
            if (i == 0)
            {
                i = 1;
            }
            else
            {
                i = i * 2 + 1;
            }
            node.left = DeepOrder(nums, len, i);
            node.right = DeepOrder(nums, len, i + 1);
            return node;
        }

        private void Deep(TreeNode root, ref Dictionary<int, string> nums, int i)
        {
            if (root == null)
            {
                nums[i] = "null";
                return;
            }
            nums[i] = root.val.ToString();
            if (i == 0)
            {
                i = 1;
            }
            else
            {
                i = i * 2 + 1;
            }
            Deep(root.left, ref nums, i);
            Deep(root.right, ref nums, i + 1);

        }

        public string serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            sb = DeepList(root);
            return sb.ToString();
        }

        public TreeNode deserialize(string data)
        {
            Queue<string> queue = ConvertStr(data);
            TreeNode root = DeepStr(queue);
            return root;
        }

        private StringBuilder DeepList(TreeNode root)
        {
            if (root == null)
            {
                return new StringBuilder("X");
            }
            //根（左，右）
            string str = root.val + "(" + DeepList(root.left) + "," + DeepList(root.right) + ")";
            return new StringBuilder(str); ;
        }

        private TreeNode DeepStr(Queue<string> dataQueue)
        {
            if (dataQueue.Count == 0 || dataQueue.Peek() == "X")//null
            {
                return null;
            }
            TreeNode root = new TreeNode();
            root.val = Convert.ToInt32(dataQueue.Dequeue());
            if (dataQueue.Peek() == ")")
            {
                dataQueue.Dequeue();
            }
            if (dataQueue.Peek() == "(")
            {
                dataQueue.Dequeue();
                root.left = DeepStr(dataQueue);
            }
            if (dataQueue.Peek() == ",")
            {
                dataQueue.Dequeue();
                root.right = DeepStr(dataQueue);
            }

            return root;
        }

        private Queue<string> ConvertStr(string str)
        {
            Queue<string> queue = new Queue<string>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                {
                    if (i < str.Length - 3 && str[i + 1] == 'X' && str[i + 2] == ',' && str[i + 3] == 'X')
                    {
                        i = i + 4;//去后括号
                    }
                    else
                    {
                        queue.Enqueue(str[i].ToString());
                    }
                }
                else if (str[i] == ',' || str[i] == ')')
                {
                    queue.Enqueue(str[i].ToString());
                }
                else if (str[i] == 'X')
                {
                    queue.Enqueue(str[i].ToString());
                }
                else
                {
                    int end = FetchNum(str, i);
                    queue.Enqueue(str.Substring(i, end - i + 1));
                    i = end;
                }
            }
            return queue;
        }

        private int FetchNum(string str, int index)
        {
            while (true)
            {
                if (str[index] >= '0' && str[index] <= '9')
                {
                    index++;
                }
                else
                {
                    break;
                }
            }
            return index - 1;
        }

        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length==0)
            {
                return 0;
            }   
            Utils.QuickSort(nums);
            int currTimes = 0;
            int currIndex = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i==0)
                {
                    currTimes++;
                    continue;
                }
                if (nums[i] != nums[currIndex])
                {
                    currTimes = 1;
                    currIndex++;
                    if (i != currIndex)
                    {
                        Exch(nums, i, currIndex);
                    }
                }
                else
                {
                    currTimes++;
                    if (currTimes==2)
                    {
                        currIndex++;
                        if (i != currIndex)
                        {
                            Exch(nums, i, currIndex);
                        }
                    }
                }
            }
            return currIndex + 1;
        }
        private void Exch(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        public IList<string> GenerateParenthesis(int n)
        {
            IList<string> strings = new List<string>();
            Stack<int> stack = new Stack<int>(n);
            string s = "";
            GenerateParenthesis(n, stack,  s, strings);
            return strings;
        }

        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null)
            {
                return null;
            }
            ListNode curr = null;
            Stack<ListNode> listNodes = new Stack<ListNode>();
            listNodes.Push(head);
            int var = head.val;
            head =head.next;
            while (head!=null)
            {
                if (var == head.val)
                {
                    while ((listNodes.Count > 0 && listNodes.Peek().val == head.val))
                    {
                        listNodes.Pop();
                    }
                }
                else
                {
                    if (listNodes.Count > 0)
                    {
                        listNodes.Peek().next = head;
                    }
                    listNodes.Push(head);
                    var = head.val;
                }
                head = head.next;
            }
            if (listNodes.Count > 0)
            {
                listNodes.Peek().next = null;
                while (listNodes.Count > 0)
                {
                    curr = listNodes.Pop();
                }
            }
            return curr;
        }

        public bool Exist(char[][] board, string word)
        {
            int wigth = board.Length / board.Rank;
            for (int i = 0; i < board.Rank; i++)
            {
                for (int j = 0; j < wigth; j++)
                {
                    if (FindBorder(board,word,0,i,j,out int nexti,out int nextj))
                    {
                        return true;
                    }
                } 
            }
            return false;
        }

        private bool FindBorder(char[][] board, string word, int wordcurr, int curri, int currj, out int nexti, out int nextj)
        {
            nexti = curri;nextj = currj;
            if (wordcurr == word.Length)
            {
                return true;
            }
            if (curri<0 || curri> board.Length/board.Rank-1 ||currj<0 || currj > board.Rank-1)
            {
                return false;
            }
            if (word[wordcurr] != board[curri][currj])
            {
                return false;
            }
            if (FindBorder(board, word, wordcurr + 1, curri - 1, currj, out nexti, out nextj))//上
            {
                nexti = curri - 1;
                nextj = currj;
                FindBorder(board, word, wordcurr + 1, nexti, nextj, out nexti, out nextj);
            }
            if (FindBorder(board, word, wordcurr + 1, curri + 1, currj, out nexti, out nextj))//下
            {
                nexti = curri + 1;
                nextj = currj;
                FindBorder(board, word, wordcurr + 1, nexti, nextj, out nexti, out nextj);
            }
            if (FindBorder(board, word, wordcurr + 1, curri, currj - 1, out nexti, out nextj))//左
            {
                nexti = curri;
                nextj = currj - 1;
                FindBorder(board, word, wordcurr + 1, nexti, nextj, out nexti, out nextj);
            }
            if (FindBorder(board, word, wordcurr + 1, curri, currj + 1, out nexti, out nextj))//右
            {
                nexti = curri;
                nextj = currj + 1;
                FindBorder(board, word, wordcurr + 1, nexti, nextj, out nexti, out nextj);
            }
            return false;
        }

        private void GenerateParenthesis(int n, Stack<int> stack, string s, IList<string> strings)
        {
            if (stack.Count == 0 && n <=0)
            {
                strings.Add(s);
                return;
            }
            if (stack.Count==0)
            {
                stack.Push(n); 
                s += "(";
                GenerateParenthesis(n-1, stack,  s, strings);
            }
            else
            {
                if (n > 0)
                {
                    stack.Push(n);
                    s += "(";
                    GenerateParenthesis(n - 1, stack, s, strings);
                }
                stack.Pop();
                s += ")";
                GenerateParenthesis(n, stack,  s, strings);
            }
        }

        public int NumIslands(char[][] grid)
        {
            int row = grid.Length;
            int column = grid[0].Length;
            int count = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j   = 0; j < column; j++)
                {
                    if (grid[i][j]=='1')
                    {
                        CleanIsland(grid,i,j);
                        count++;
                    }
                }
            }
            return count;
        }

        private void CleanIsland(char[][] grid, int i, int j)
        {
            int row = grid.Length;
            int column = grid[0].Length;
            if (j - 1 >= 0 && grid[i][j - 1] == '1')//左
            {
                grid[i][j - 1] = '0';
                CleanIsland(grid, i, j - 1);
            }
            if (j+1 < column && grid[i][j+1]=='1')//右
            {
                grid[i][j + 1] = '0';
                CleanIsland(grid, i, j + 1);
            }
            if (i - 1 >= 0 && grid[i - 1][j] == '1')//上
            {
                grid[i - 1][j] = '0';
                CleanIsland(grid, i - 1, j);
            }
            if (i + 1 < row && grid[i+1][j] == '1')//下
            {
                grid[i + 1][j] = '0';
                CleanIsland(grid, i + 1, j);
            }
        }

        public int LargestIsland(int[][] grid)
        {
            int row = grid.Length;
            int column = grid[0].Length;
            int len = row * column;
            int max = 0;
            int[] gridroot = new int[len];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        root = i * column + j; 
                        grid[i][j] = len + root;
                        islandSize = 1;
                        ConnectIsland(grid, i, j);
                        gridroot[root] = islandSize;
                        max=Math.Max(max, islandSize);
                    }
                }
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (grid[i][j] == 0)
                    {
                        max = Math.Max(MergeIsland(grid, i, j, gridroot), max);
                    }
                }
            }
            return max;
        }

        int root = 0;
        int islandSize = 0;
        private void ConnectIsland(int[][] grid, int i, int j)
        {
            int row = grid.Length;
            int column = grid[0].Length;
            int len = row * column;
            if (j - 1 >= 0 && grid[i][j - 1] == 1)//左
            {
                islandSize++;
                grid[i][j - 1] = len+root;
                ConnectIsland(grid, i, j - 1);
            }
            if (j + 1 < column && grid[i][j + 1] == 1)//右
            {
                islandSize++;
                grid[i][j + 1] = len + root;
                ConnectIsland(grid, i, j + 1);
            }
            if (i - 1 >= 0 && grid[i - 1][j] == 1)//上
            {
                islandSize++;
                grid[i - 1][j] = len + root;
                ConnectIsland(grid, i - 1, j);
            }
            if (i + 1 < row && grid[i + 1][j] == 1)//下
            {
                islandSize++;
                grid[i + 1][j] = len + root;
                ConnectIsland(grid, i + 1, j);
            }
        }

        private int MergeIsland(int[][] grid, int i, int j, int[] gridroot)
        {
            int size = 1;
            int row = grid.Length;
            int column = grid[0].Length;
            int len = row * column;
            List<int> roots=new List<int>();
            if (j - 1 >= 0 && grid[i][j - 1] > 0)//左
            {
                int root = grid[i][j - 1] - len;
                roots.Add(root);
                size += gridroot[root];
            }
            if (j + 1 < column && grid[i][j + 1] > 0)//右
            {
                int root = grid[i][j + 1] - len;
                if (!roots.Contains(root))
                {
                    roots.Add(root);
                    size += gridroot[root];
                }
            }
            if (i - 1 >= 0 && grid[i - 1][j] > 0)//上
            {
                int root = grid[i - 1][j] - len;
                if (!roots.Contains(root))
                {
                    roots.Add(root);
                    size += gridroot[root];
                }
            }
            if (i + 1 < row && grid[i + 1][j] > 0)//下
            {
                int root = grid[i + 1][j] - len;
                if (!roots.Contains(root))
                {
                    roots.Add(root);
                    size += gridroot[root];
                }
            }
            return size;
        }

        public int MaxAreaOfIsland(int[][] grid)
        {
            int max = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        islandSize = 1;
                        grid[i][j] = 0;
                        CalculateSize(grid, i, j);
                        max = Math.Max(max, islandSize);
                    }
                }
            }
            return max;
        }

        private void CalculateSize(int[][] grid, int i, int j)
        {
            int row = grid.Length;
            int column = grid[0].Length;
            if (j - 1 >= 0 && grid[i][j - 1] == 1)//左
            {
                islandSize++;
                grid[i][j - 1] = 0;
                CalculateSize(grid, i, j - 1);
            }
            if (j + 1 < column && grid[i][j + 1] == 1)//右
            {
                islandSize++;
                grid[i][j + 1] = 0;
                CalculateSize(grid, i, j + 1);
            }
            if (i - 1 >= 0 && grid[i - 1][j] == 1)//上
            {
                islandSize++;
                grid[i - 1][j] = 0;
                CalculateSize(grid, i - 1, j);
            }
            if (i + 1 < row && grid[i + 1][j] == 1)//下
            {
                islandSize++;
                grid[i + 1][j] = 0;
                CalculateSize(grid, i + 1, j);
            }
        }

        public int IslandPerimeter(int[][] grid)
        {
            int per = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (i==0 && grid[i][j] == 1)//上边缘
                    {
                        per++;
                    }
                    if (grid[i][j]==0 && i < grid.Length - 1 && grid[i+1][j] == 1)//从上向下看
                    {
                        per++;
                    }
                    if (j == 0 && grid[i][j] == 1)//左边缘
                    {
                        per++;
                    }
                    if (grid[i][j] == 0 && j < grid[0].Length - 1 && grid[i][j + 1] == 1)//从左到右看
                    {
                        per++;
                    }
                    if (i == grid.Length - 1 && grid[i][j] == 1)//下边缘
                    {
                        per++;
                    }
                    if (grid[i][j] == 0 && i > 0 && grid[i - 1][j] == 1)//从下向上看
                    {
                        per++;
                    }
                    if (j == grid[0].Length - 1 && grid[i][j] == 1)//右边缘
                    {
                        per++;
                    }
                    if (grid[i][j] == 0 && j > 0 && grid[i][j - 1] == 1)//从右到左看
                    {
                        per++;
                    }
                }
            }
            return per; 
        }

        public int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!map.Keys.Contains(nums[i]))
                {
                    map.Add(nums[i],1);
                }
                else
                {
                    map[nums[i]]++;
                }
            }
            NumsComparer nc = new NumsComparer();
            MaxPQ<Nums> maxPQ = new MaxPQ<Nums>(nums.Length, nc);
            foreach (var item in map.Keys)
            {
                Nums num = new Nums(item, map[item]);
                maxPQ.Insert(num);
            }
            int[] res = new int[k];
            for (int i = 0; i < k; i++)
            {
                res[i] = maxPQ.DelMax().num;
            }
            return res;
        }

        public string MaxValue(string n, int x)
        {
            bool sign = false;
            string s;
            for (int i = 0; i < n.Length; i++)
            {
                if (i==0 && n[i] == '-')
                {
                    sign = true;
                    continue;
                }
                if ((x <= Convert.ToInt32(n[i].ToString())) == sign)
                {
                    s= n.Substring(0, i) + x.ToString() + n.Substring(i, n.Length - i);
                    return s;
                }
            }
            s= n + x.ToString();
            return s;
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    internal class NumsComparer : IComparer<Nums>
    {
        public int Compare(Nums x, Nums y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            else if (x == null)
            {
                return 1;
            }
            else if (y == null)
            {
                return -1;
            }
            else if (x.times < y.times)
            {
                return -1;
            }
            else if (x.times > y.times)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    internal class Nums
    {
        public int num;
        public int times;
        public Nums(int num, int times)
        {
            this.num = num;
            this.times = times;
        }
    }

    internal class ListNodeComparer : IComparer<ListNode>
    {
        public int Compare(ListNode x, ListNode y)
        {
            if (x == null && y==null)
            {
                return 0;
            }
            else if (x == null)
            {
                return 1;
            }
            else if (y == null)
            {
                return -1;
            }
            else if (x.val < y.val)
            {
                return -1;
            }
            else if (x.val > y.val)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    internal class ArrayComparer : IComparer<int[]>
    {
        public int Compare(int[] x, int[] y)
        {
            if (x[0] < y[0])
            {
                return -1;
            }
            else if (x[0] > y[0])
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
