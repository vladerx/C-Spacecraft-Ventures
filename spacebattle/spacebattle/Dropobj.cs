using System;
using System.Collections.Generic;
using System.Linq;

namespace spacebattle
{
    static class Dropobj
    {
        public static Dictionary<int, int> droptypes = new Dictionary<int, int>();
        public static Dictionary<int, int[]> dropcords = new Dictionary<int, int[]>();

        private static readonly int dropSpeed = 2;
        public static int[] amounts = { 30, 50, 20, 30, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        public static int[] dropEnable = { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };
        public static int[] dropRates = {30, 30, 15, 20, 20, 20, 20, 20, 20, 15, 15, 15, 15, 10, 10, 10, 20, 20, 20, 300, 6, 6, 6, 50 };
        public static int[] skillDropRates = { 1, 1, 1, 1, 1, 1, 1, 1 };
        public static string[] dropnames = {" energy", " energy" , " fuel", " durability", " ammo up", " Laser Gun", " Scattered Pulse Gun", " Bouncing Laser Gun", " Pulse Gun", " Canon Gun", " Particle Gun", " Missile Lancher", " Proton Gun", " Death Drone Launcher", " Guided Missile Launcher", " Replicating Bullet Gun", " Crit.Chance", " Crit.Damage", " Fire Rate", " coins", " parts", "Bronze Bar", "Iron Bar", "Skill Drop" };

        public static void CreateDrop(int cordx, int cordy, int droptype, int dropNumber)
        {
            droptypes.Add(dropNumber, droptype);
            dropcords.Add(dropNumber, new int[] { cordx, cordy });

        }
        public static void MoveDrop()
        {
            List<int> keysRemove = new List<int>();
            int cord;
            foreach (int dropkey in dropcords.Keys) {
                cord = dropcords[dropkey][0] + dropSpeed;
                if (cord > 1390)                            //remove drop when reached frame boundries
                {
                    keysRemove.Add(dropkey);
                }
                dropcords[dropkey][0] = cord + dropSpeed;
            }
            if (keysRemove.Count != 0) {
                foreach (int key in keysRemove) {
                    dropcords.Remove(key);
                    droptypes.Remove(key);
                }
                keysRemove.Clear();
            }

        }

        public static int CalcDrop(Random randint)
        {
            int rateSum = 0;
            for (int j = 0; j < dropRates.Length; j++) {
                rateSum += dropRates[j] * dropEnable[j];
            }
            int ratenum = randint.Next(0, rateSum);
            int sum = 0;
            for (int i = 0; i < dropRates.Length; i++)
            {
                sum += (dropRates[i]* dropEnable[i]);
                if (ratenum < sum) {
                    if (i != 23)
                    {
                        return i;
                    }
                    else
                    {
                        rateSum = skillDropRates.Sum();
                        ratenum = randint.Next(0, rateSum);
                        sum = 0;
                        for (int n = 0; n < skillDropRates.Length; n++)
                        {
                            sum += skillDropRates[n];
                            if (ratenum < sum)
                            {
                                return 100 + n;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        public static void ResetDropObj() {
            droptypes.Clear();
            dropcords.Clear();
        }
    }
}
