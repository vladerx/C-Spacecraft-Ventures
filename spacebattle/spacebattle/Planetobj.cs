using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace spacebattle
{
    static class Planetobj
    {
        public static Dictionary<int, int[]> planetcords = new Dictionary<int, int[]>();
        public static Dictionary<int, int> planetypes = new Dictionary<int, int>();
        public static Dictionary<int, int> planetspeeds = new Dictionary<int, int>();
        public static Dictionary<int, int[]> planetimgsindxs = new Dictionary<int, int[]>() {
            { 0, new int []{ 0, 7} },
            { 1, new int []{ 8, 13} },
            { 2, new int []{ 14, 19} },
            { 3, new int []{ 20, 25} },
            { 4, new int []{ 26, 31} },
            { 5, new int []{ 32, 37} },
            { 6, new int []{ 38, 43} }
        };
        public static Dictionary<int, Image> planetimgs = new Dictionary<int, Image>();


        public static void Createplanet(int cordx, int cordy, int type, int planetNumber)
        {
            planetcords.Add(planetNumber, new int[] { cordx, cordy });
            planetypes.Add(planetNumber, type);
            planetspeeds.Add(planetNumber, 1);
        }
        public static void Moveplanet()
        {
            List<int> keysRemove = new List<int>();
            foreach (int planetkey in planetcords.Keys)
            {
                planetcords[planetkey][0] = planetcords[planetkey][0] + planetspeeds[planetkey];
                if (planetcords[planetkey][0] > 1390)
                {
                    keysRemove.Add(planetkey);
                    
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    planetcords.Remove(key);
                    planetypes.Remove(key);
                    planetspeeds.Remove(key);
                }
                keysRemove.Clear();
            }
        }
        public static void LoadPlanetImgs(int level)
        {
            for (int i = planetimgsindxs[(level - 1) / 3][0]; i <= planetimgsindxs[(level - 1) / 3][1]; i++)
            {
                planetimgs.Add(i ,Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\planets\" + i.ToString() + ".png"));
            }
        }

        public static void ResetPlanetObj() {
            planetcords.Clear();
            planetypes.Clear();
            planetspeeds.Clear();
            planetimgs.Clear();
        }
    }
}
