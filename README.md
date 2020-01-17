# Zombie-solution-using-C-Sharp
**Question:** Given a 2D grid, each cell is either a zombie 1 or a human 0. Zombies can turn adjacent (up/down/left/right) human beings into zombies every hour. Find out how many hours does it take to infect all humans?

Example:

**Input:**
<br/>[[0, 1, 1, 0, 1],
 <br/>[0, 1, 0, 1, 0],
 <br/>[0, 0, 0, 0, 1],
 <br/>[0, 1, 0, 0, 0]]

Output: 2

**Explanation:**
At the end of the 1st hour, the status of the grid:
<br/>[[1, 1, 1, 1, 1],
<br/> [1, 1, 1, 1, 1],
<br/> [0, 1, 0, 1, 1],
<br/> [1, 1, 1, 0, 1]]

At the end of the 2nd hour, the status of the grid:
<br/>[[1, 1, 1, 1, 1],
<br/> [1, 1, 1, 1, 1],
<br/> [1, 1, 1, 1, 1],
<br/> [1, 1, 1, 1, 1]]

Solution C# Sample Code: 

<pre>
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
</pre>
