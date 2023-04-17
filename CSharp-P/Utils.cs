using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_P
{
    public class Utils
    {
        /// <summary>
        /// 二分查找
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int BinarySearch(int[] nums, int lo, int hi, int key)
        {
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (key < nums[mid])
                {
                    hi = mid - 1;
                }
                else if (key > nums[mid])
                {
                    lo = mid + 1;
                }
                else
                {
                    return mid;
                }
            }
            return -1;
        }

        /// <summary>
        /// 希尔排序
        /// </summary>
        /// <param name="nums"></param>
        public static void ShellSort(int[] nums)
        {
            int N = nums.Length;
            int h = 1;
            while (h < N / 3)
            {
                h = 3 * h + 1;
            }
            while (h >= 1)
            {
                for (int i = h; i < N; i++)
                {
                    for (int j = i; j >= h && nums[j] < nums[j - h]; j -= h)
                    {
                        int temp = nums[j];
                        nums[j] = nums[j - h];
                        nums[j - h] = temp;
                    }
                }
                h = h / 3;
            }
        }

        /// <summary>
        /// 二叉树数组转换为链表表示，空用null表示
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode ConvertArray2Link(int[] nums)
        {
            TreeNode treeNode = new TreeNode();
            treeNode = DeepOrder(nums, nums.Length, 0);
            return treeNode;
        }

        private static TreeNode DeepOrder(int[] nums, int len, int i)
        {
            if (i >= len)
            {
                return null;
            }
            if (nums[i]==-1)//-1表示空节点
            {
                return null;
            }
            TreeNode node = new TreeNode();
            node.val = nums[i];
            if (i==0)
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

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="nums"></param>
        public static void QuickSort(int[] nums)
        {
            Quick<int>.Sort(nums);
        }

        /// <summary>
        /// 层序遍历
        /// </summary>
        /// <param name="BT"></param>
        public void LeverOrder(TreeNode BT)
        {
            Queue<TreeNode> Q = new Queue<TreeNode>();
            TreeNode T;
            T = BT;
            if (T != null)
            {
                Q.Enqueue(T);
            }
            while (Q.Count > 0)
            {
                T = Q.First();
                //Data
                Console.WriteLine(T.val);
                Q.Dequeue();
                if (T.left != null)
                {
                    Q.Enqueue(T.left);
                }
                if (T.right != null)
                {
                    Q.Enqueue(T.right);
                }
            }
        }
    }

    public class MinPQ<Key>
    {
        private Key[] pq;
        private int n = 0;
        private IComparer<Key> lc = null;
        public MinPQ(int initCapacity, IComparer<Key> comparer)
        {
            pq = new Key[initCapacity + 1];
            lc = comparer;
        }

        public void Insert(Key v)
        {
            pq[++n] = v;
            Swim(n);
        }

        public Key DelMin()
        {
            Key min = pq[1];
            Each(1, n--);
            pq[n + 1] = default;
            Sink(1);
            return min;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k) < 0)
            {
                Each(k / 2, k);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && Less(j, j + 1) < 0)
                {
                    j++;
                }
                if (Less(k, j) > 0)
                {
                    break;
                }
                Each(k, j);
                k = j;
            }
        }
        private int Less(int i, int j)
        {
            return lc.Compare(pq[j], pq[i]);//pq[j]在前，为最小堆
        }

        private void Each(int i, int j)
        {
            Key temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }
    }

    public class MaxPQ<Key>
    {
        private Key[] pq;
        private int n = 0;
        private IComparer<Key> lc = null;
        public MaxPQ(int initCapacity, IComparer<Key> comparer)
        {
            pq = new Key[initCapacity + 1];
            lc = comparer;
        }

        public void Insert(Key v)
        {
            pq[++n] = v;
            Swim(n);
        }

        public Key DelMax()
        {
            Key min = pq[1];
            Each(1, n--);
            pq[n + 1] = default;
            Sink(1);
            return min;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k) < 0)
            {
                Each(k / 2, k);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && Less(j, j + 1) < 0)
                {
                    j++;
                }
                if (Less(k, j) > 0)
                {
                    break;
                }
                Each(k, j);
                k = j;
            }
        }
        private int Less(int i, int j)
        {
            return lc.Compare(pq[i], pq[j]);//pq[i]在前，为最大堆
        }

        private void Each(int i, int j)
        {
            Key temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }
    }

    public class MaxPQ
    {
        private int[] pq;
        private int n = 0;
        public MaxPQ(int initCapacity)
        {
            pq = new int[initCapacity + 1];
        }

        public void Insert(int v)
        {
            pq[++n] = v;
            Swim(n);
        }

        public int DelMax()
        {
            int min = pq[1];
            Each(1, n--);
            pq[n + 1] = default;
            Sink(1);
            return min;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k) < 0)
            {
                Each(k / 2, k);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && Less(j, j + 1) < 0)
                {
                    j++;
                }
                if (Less(k, j) > 0)
                {
                    break;
                }
                Each(k, j);
                k = j;
            }
        }
        private int Less(int i, int j)
        {
            if (i > j)
            {
                return 1;
            }
            else if (i < j)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private void Each(int i, int j)
        {
            int temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }
    }

    class Quick<T>
    {
        public static void Sort(T[] a)
        {
            Sort(a, 0, a.Length - 1);
        }

        private static void Sort(T[] a,int lo,int hi)
        {
            if (hi<=lo)
            {
                return;
            }
            int j = Partition(a, lo, hi);
            Sort(a, lo, j - 1);
            Sort(a, j + 1, hi);
        }

        private static int Partition(T[] a, int lo, int hi)
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

        private static void Exch(T[] nums, int i, int j)
        {
            T temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        private static int Compare(T[] nums, int i, int j)
        {
            if (Convert.ToInt32(nums[i]) < Convert.ToInt32(nums[j]))
            {
                return -1;
            }
            if (Convert.ToInt32(nums[i]) > Convert.ToInt32(nums[j]))
            {
                return 1;
            }
            return 0;
        }
    }
}
