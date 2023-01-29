using System.Drawing;
using System.Windows.Forms;

namespace spacebattle
{
    static class Playerobj
    {
        public static Label colides = new Label(), skillcolides = new Label(), consumecolides = new Label();
        public static int labelcounter = 0, skilllabelcounter = 0, consumelabelcounter = 0;
        public static int[] cords = { 1200, 400 };
        public static Size size = new Size(80, 44);
        public static int[] inverntory = { 0, 0, 0, 0 };
        public static int[] playerSpeedDirc = {6, 6, 6, 6};
        public static int maxFireRate, critChance = 0, critChanceTemp, maxCritChance, critDamage = 100, critDamageTemp, maxCritDamage, HP, maxHP, bulletMaxLevel, fuelCons, energyCons, maxEnergy;
        public static int fuel = 100, energy = 100, coins = 0, fireRate = 5;
        public static int dmgcdcounter = -1;
        public static Image playerImg = Image.FromFile(@"C:\Users\fives\source\repos\spacebattle\spacebattle\player\player.png");
        public static int pocketskill = -1;
        public static double dmgreduc = 1, dmginc = 1, skillvamp = 0;
        public static int[] playerstats = new int[] {0,0,0,0}; // damged,dmg immune,slowed, freeze

        public static void MovePlayer(int[] direc)
        {

            cords[0] = cords[0] +  ((direc[2] * playerSpeedDirc[2]) - (direc[3] * playerSpeedDirc[3]));
            cords[1] = cords[1] + ((direc[1] * playerSpeedDirc[1]) - (direc[0]*playerSpeedDirc[0]));
            if ((cords[0] < 0) || (cords[0] > 1360) || Skillobj.canceladv)                //frame boundries stops player's advance
            {
                cords[0] = cords[0] - ((direc[2] * playerSpeedDirc[2]) - (direc[3] * playerSpeedDirc[3]));
            }
            if ((cords[1] < 50) || (cords[1] > 710)) 
            {
                cords[1] = cords[1] - ((direc[1] * playerSpeedDirc[1]) - (direc[0] * playerSpeedDirc[0]));
            };
        }

        public static void ResetPlayerObj() {
            labelcounter = 0;
            skilllabelcounter = 0;
            consumelabelcounter = 0;
            playerstats = new int[] { 0, 0, 0, 0 };
            playerSpeedDirc = new int[] { 6, 6, 6, 6};
            fuel = 100;
            critDamage = 100;
            critChance = 0;
            energy = 100;
            coins = 0;
            fireRate = 5;
            maxEnergy = 100;
            dmgcdcounter = -1;
            cords = new int[]{ 1200, 400 };
            fuel = 100;
            HP = maxHP;
            pocketskill = -1;
            dmgreduc = 1;
            dmginc = 1;
            skillvamp = 0;
        }
    }
}
