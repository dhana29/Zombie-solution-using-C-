using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_CSharp
{
    class ZombieSolution
    {
        static void Main(string[] args)
        {
            try
            {
               Console.WriteLine("Answer: " + zombie().ToString() + " hours.");
               Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static int zombie()
        {
            int[,] ip = {
                            { 0, 1, 1, 0, 1},
                            { 0, 1, 0, 1, 0},
                            { 0, 0, 0, 0, 1},
                            { 0, 1, 0, 0, 0}
                        };
            int x = ip.GetLength(0);
            int y = ip.GetLength(1);
            List<int> ipList = new List<int>();
            ipList = ip.Cast<int>().ToList();
            Dictionary<int, int> dList = new Dictionary<int, int>();
            for (int i = 1; i <= ipList.Count; i++)
            {
                dList.Add(i, ipList[i - 1]);
            }
            int count = 0;
            do
            {
                //Update Adjacent records
                Dictionary<int, int> adjList = updateAdj(dList, x, y);
                //Update UpDown records
                Dictionary<int, int> upDownList = updateUpDown(dList, x, y);
                var merged = adjList
                     .Concat(upDownList)
                     .GroupBy(i => i.Key)
                     .ToDictionary(
                         group => group.Key,
                         group => group.First().Value).OrderBy(g => g.Key);

                foreach (KeyValuePair<int, int> temp in merged)
                {
                    dList[temp.Key] = temp.Value;
                }
                count++;
            } while (dList.Where(a => a.Value == 0).Select(a => a.Key).Count() > 0);

            return count;
        }
        static Dictionary<int, int> updateAdj(Dictionary<int, int> dList, int x, int y)
        {
            Dictionary<int, int> rList = new Dictionary<int, int>();    //{ 1, 1, 1, 1, 1}
                                                                        //{ 1, 1, 1, 1, 1}
            foreach (var k in dList)                                    //{ 0, 0, 0, 1, 1}
            {                                                           //{ 1, 1, 1, 0, 0}
                if (((k.Key - 1) % y == 0 || k.Key == 1) && k.Value == 1)
                {
                    rList.Add(k.Key + 1, 1);
                    rList[k.Key] = 1;
                }
                else if (k.Key % y == 0 && k.Value == 1)
                {
                    rList[k.Key - 1] = 1;
                    rList[k.Key] = 1;
                }
                else if (k.Value == 1)
                {
                    rList[k.Key - 1] = 1;
                    rList.Add(k.Key + 1, 1);
                    rList[k.Key] = 1;
                }
            }
            return rList;
        }
        static Dictionary<int, int> updateUpDown(Dictionary<int, int> dList, int x, int y)
        {
            Dictionary<int, int> rList = new Dictionary<int, int>();    //{ 0, 1, 1, 1, 1},
                                                                        //{ 0, 1, 1, 1, 1},
            foreach (var k in dList)                                    //{ 0, 1, 0, 0, 1},
            {                                                           //{ 0, 1, 0, 0, 1} 
                if (k.Key <= y && k.Value == 1)
                {
                    rList[k.Key + 5] = 1;
                }
                else if (k.Key > y && k.Key <= dList.Count - y && k.Value == 1)
                {
                    rList[k.Key - 5] = 1;
                    rList[k.Key + 5] = 1;
                }
                else if (k.Value == 1)
                {
                    rList[k.Key - 5] = 1;
                }
            }
            return rList;
        }
    }
}
