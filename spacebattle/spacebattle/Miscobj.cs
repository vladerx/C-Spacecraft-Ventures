using System.Collections.Generic;
using System.Drawing;

namespace spacebattle
{
    static class Miscobj
    {
        public static Dictionary<int, int[]> miscords = new Dictionary<int, int[]>();
        public static Dictionary<int, int> misctypes = new Dictionary<int, int>();
        public static Dictionary<int, int> miscspeeds = new Dictionary<int, int>();
        public static Dictionary<int, int[]> miscimgsindxs = new Dictionary<int, int[]>() {
            { 0, new int []{ 0, 7} },
            { 1, new int []{ 8, 15} },
            { 2, new int []{ 16, 23} },
            { 3, new int []{ 24, 31} },
            { 4, new int []{ 32, 39} },
            { 5, new int []{ 40, 47} },
            { 6, new int []{ 48, 55} }
        };

        public static Dictionary<int, Image> miscimgs = new Dictionary<int, Image>();

        public static void CreateMisc(int cordx, int cordy, int speed, int type, int miscNumber)
        {
            miscords.Add(miscNumber, new int[] { cordx, cordy });
            misctypes.Add(miscNumber, type);
            miscspeeds.Add(miscNumber, speed);
        }
        public static int MoveMisc(int level)
        {
            int misctype;
            foreach (int misckey in miscords.Keys)
            {
                if (level > 6 && level < 16 && (misctypes[misckey] != 16 && misctypes[misckey] != 24 && misctypes[misckey] != 32))
                {
                    miscords[misckey][1] -= miscspeeds[misckey];
                }
                else
                {
                    miscords[misckey][0] += miscspeeds[misckey];
                }
                if (miscords[misckey][0] > 1390 || miscords[misckey][1] < 0)
                {
                    misctype = misctypes[misckey];
                    miscords.Remove(misckey);
                    misctypes.Remove(misckey);
                    miscspeeds.Remove(misckey);
                    return misctype;
                }
            }
            return -1;
        }

        public static void LoadMiscImgs(int level)
        {
            for (int i = miscimgsindxs[(level - 1) / 3][0]; i <= miscimgsindxs[(level - 1) / 3][1]; i++)
            {
                miscimgs.Add(i, Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\misc\" + i.ToString() + ".png"));
            }
        }

        public static void ResetMiscObj() {
            miscimgs.Clear();
            miscords.Clear();
            misctypes.Clear();
            miscspeeds.Clear();
        }
    }
}
