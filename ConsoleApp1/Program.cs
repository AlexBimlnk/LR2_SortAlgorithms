using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppForTask
{
    class Program
    {
        #region Другие сортировки
        //SelectionSort
        static void SelSort(int[] tb)
        {
            for(int i = 0; i<tb.Length-1; i++)
            {
                int min = tb[i];
                int minIndex = i;
                for(int j = i+1; j<tb.Length; j++)
                {
                    if (min > tb[j])
                    {
                        min = tb[j];
                        minIndex = j;
                    }
                }
                tb[minIndex] = tb[i];
                tb[i] = min;
            }
        }

        //Сортировка Шелла
        static void ShellSort(int[] tb)
        {
            int step = tb.Length / 2;

            while(step > 0)
            {
                for(int i = step; i<tb.Length; i++)
                {
                    int j = i;
                    while((j>=step) && tb[j - step] > tb[j])
                    {
                        int temp = tb[j];
                        tb[j] = tb[j-step];
                        tb[j-step] = temp;
                        j -= step;
                    }
                }
                step /= 2;
            }
        }

        //Сортировка бинарными вставками
        static void BinaryInceptionSort(int[] tb)
        {
            for (int i = 1; i < tb.Length; i++)
            {
                int temp = tb[i];
                int r = i-1;
                int l = 0;
                int mid = (l + r) / 2;
                while (l<=r)
                {
                    if (tb[mid] > temp)
                        r = mid-1;
                    else
                        l = mid+1;
                    mid = (l+r) / 2;
                }
                for (int j = i - 1; j >= l; j--)
                {
                    tb[j + 1] = tb[j];
                }
                tb[l] = temp;
            }
        }

        //Сортировка вставками
        static void InceptionSort(int[] tb)
        {
            for(int i = 1; i<tb.Length; i++)
            {
                int temp = tb[i];
                int j = i;
                while (j > 0 && tb[j - 1] > temp)
                {
                    tb[j] = tb[j - 1];
                    j--;
                }
                tb[j] = temp;
            }
        }

        //Сортировка пузырьком
        static void BubbleSort(int[] tb)
        {
            for(int i = 0; i<tb.Length; i++)
            {
                for(int j = 0; j<tb.Length - i - 1; j++)
                {
                    if(tb[j] > tb[j+1])
                    {
                        int temp = tb[j+1];
                        tb[j+1] = tb[j];
                        tb[j] = temp;
                    }
                }
            }
        }

        #endregion

        //O(n) Сортировка подсчетом
        static string CountigSort(string data)
        {
            int[,] N = new int[2, 58];
            string answer = "";
            for (int i = 65; i < 123; i++)
                N[0, i - 65] = i;

            foreach (var i in data)
                N[1, Convert.ToInt32(i) - 65] += 1;

            for (int i = 0; i < 58; i++)
            {
                if (N[1, i] == 0)
                    continue;
                answer += new string(Convert.ToChar(N[0, i]), N[1,i]);
            }

            return answer;
        }
         

        //O(n^2) Сортировка выбором
        static void SelectionSort(int[,] tb, int rowCount, int columnCount)
        {
            int temp;
            int indexI = 0;
            int indexJ = 0;
            for (int m = 0; m < rowCount * columnCount; m++)
            {
                temp = int.MaxValue;
                int startJ = m % rowCount;
                for (int i = m/rowCount; i < rowCount; i++)
                {
                    for (int j = startJ; j < columnCount; j++)
                    {
                        if(tb[i,j] < temp)
                        {
                            temp = tb[i, j];
                            indexI = i;
                            indexJ = j;
                        }
                    }
                    startJ = 0;
                }
                tb[indexI, indexJ] = tb[m / rowCount, m % rowCount];
                tb[m / rowCount, m % rowCount] = temp;
            }

        }


        //O(nlogn) Быстрая сортировка
        static void QSort(int[] tb, int start, int end)
        {
            if (start >= end)
                return;
            int left = start;
            int right = end;
            int mid = tb[(left + right) / 2];
            int temp = 0;
            while (left <= right)
            {
                while (tb[left] < mid) left++;
                while (tb[right] > mid) right--;
                if (left <= right)
                {
                    temp = tb[left];
                    tb[left] = tb[right];
                    tb[right] = temp;
                    left++;
                    right--;
                }
            }
            QSort(tb, start, right);
            QSort(tb, left, end);
        }


        //Сортировка слиянием
        static int[] Merge(int[] L, int[] R)
        {
            int[] mergeArray = new int[L.Length + R.Length];

            int i = 0; int j = 0; int k = 0;

            while(i<L.Length && j < R.Length)
            {
                if (L[i] < R[j])
                {
                    mergeArray[k] = L[i];
                    i++;
                }
                else
                {
                    mergeArray[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < L.Length)
            {
                mergeArray[k] = L[i];
                i++; k++;
            }
            while (j < R.Length)
            {
                mergeArray[k] = R[j];
                j++; k++;
            }

            return mergeArray;
        }

        static void MergeSort(ref int[] tb)
        {
            if (tb.Length == 1)
                return;

            int mid = tb.Length / 2;

            int[] L = tb.Take(mid).ToArray();
            int[] R = tb.Skip(mid).ToArray();

            MergeSort(ref L);
            MergeSort(ref R);

            tb = Merge(L, R);
        }


        //Внешняя сортировка
        static void ExternalSort()
        {
            int countFile = 0;

            StreamReader fileReader = new StreamReader("input.txt");

            StreamWriter fileWriter;


            int[] sectorData1;


            while (!fileReader.EndOfStream)
            {
                sectorData1 = fileReader.ReadLine().Split(' ').Select((x) => Convert.ToInt32(x)).ToArray();

                MergeSort(ref sectorData1);

                fileWriter = new StreamWriter($@"temp\output{countFile}.txt");
                fileWriter.WriteLine(string.Join(" ", sectorData1));
                fileWriter.Close();
                countFile++;
            }

            fileReader.Close();

            MergeFile("temp", countFile);
        }

        static void MergeFile(string path, int id)
        {
            if (Directory.Exists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                while (fileNames.Length > 1)
                {
                    for(int i = 0; i<fileNames.Length-1; i+=2)
                    {
                        StreamReader file1 = new StreamReader(fileNames[i]);
                        StreamReader file2 = new StreamReader(fileNames[i+1]);

                        StreamWriter fileWrite = new StreamWriter($@"{path}\output{id}.txt");

                        string itemFile1 = "";
                        string itemFile2 = "";

                        while(file1.Peek() != '\r' || file2.Peek() != '\r')
                        {
                            if (itemFile1 == "")
                            {
                                while (file1.Peek() != '\r')
                                {
                                    if((char)file1.Peek() == ' ')
                                    {
                                        file1.Read();
                                        break;
                                    }
                                    itemFile1 += (char)file1.Read();
                                }
                            }
                            if (itemFile2 == "")
                            {
                                while (file2.Peek() != '\r')
                                {
                                    if ((char)file2.Peek() == ' ')
                                    {
                                        file2.Read();
                                        break;
                                    }
                                    itemFile2 += (char)file2.Read();
                                }
                            }


                            if (itemFile1 != "")
                            {
                                if(itemFile2 != "")
                                {
                                    if (Convert.ToInt32(itemFile1) < Convert.ToInt32(itemFile2))
                                    {
                                        fileWrite.Write(itemFile1 + " ");
                                        itemFile1 = "";
                                    }
                                    else
                                    {
                                        fileWrite.Write(itemFile2 + " ");
                                        itemFile2 = "";
                                    }
                                }
                                else
                                {
                                    fileWrite.Write(itemFile1 + " ");
                                    itemFile1 = "";
                                }
                            }
                            else
                            {
                                fileWrite.Write(itemFile2 + " ");
                                itemFile2 = "";
                            }                            
                            
                        }
                        fileWrite.Write('\r');
                        fileWrite.Close();
                        file1.Close();
                        file2.Close();
                        File.Delete(fileNames[i]);
                        File.Delete(fileNames[i+1]);

                        id++;
                    }
                    fileNames = Directory.GetFiles(path);
                }
            }
            else
                throw new Exception();
        }



        static void PrintArr(int[,] tb, int rowCount, int columnCount)
        {
            for(int i = 0; i < rowCount; i++)
            {
                for(int j = 0; j<columnCount; j++)
                    Console.Write($"\t{tb[i, j]}\t");
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {

            #region Внутренние сортировки

            string data1 = "cgAaZdfzBeqeaFgub";
            int[] data2 = { 5, 1, 2, 6, 9, 11, 25, 7, 5 };
            int[,] data3 =
            {
                {2,6, 1},
                {7,9,23},
                {5,8,11}
            };

            Console.WriteLine();

            Console.WriteLine($"O(n) data: {data1}");
            Console.WriteLine($"\tResult: {CountigSort(data1)}\n");
            Console.WriteLine($"O(nlog) data: {string.Join(" ", data2)}");
            QSort(data2, 0, data2.Length - 1);
            Console.WriteLine($"\tResult: {string.Join(" ", data2)}\n");

            Console.WriteLine("O(n^2) data:");
            PrintArr(data3, 3, 3);
            SelectionSort(data3, 3, 3);
            Console.WriteLine("Result:");
            PrintArr(data3, 3, 3);

            #endregion


            ExternalSort();


            Console.ReadKey();
        }
    }
}
