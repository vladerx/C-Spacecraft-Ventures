using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace spacebattle
{
    public partial class Game : Form
    {
        private static Imgs imgs = new Imgs();
        public static int dropcounter = 0, miscounter = 0, bulletcounter = 0, enemybulletcounter = 0, enemybossbulletcounter = 0, enemycounter = 0, planetcounter = 0, main = 0,
            mTimerId, sVolume, mVolume, currWorlds = 1, bonusWorlds = 0, mainmenu = 0, controlbut = 0, resumebut = 0, settingsbut = 0, shopbut = 0, startbut = 0, gameOn = 0, enemyflick = 0,
            Distance = 0, mobindx = 0, level = 1, duration = 20000, bulletCounter = 0, playerSpeedLimit = 10, accelerate = 1, playerHPRatio = 0, bossSpawnedState = 0, holecounter = 0, enemySpawnInterval = 30, fullscreen = 0, enemySpawnCount = 0, musicounter = 0;
        private TimerEventHandler mHandler;  
        private int mTestTick;
        private DateTime mTestStart;
        private static readonly int[,] buttoncords = new int[8, 2] { { 437, 314 }, { 437, 380 }, { 437, 454 }, { 437, 527 }, { 190, 200 }, { 190, 350 }, { 190, 500 }, { 190, 650 } };
        private static readonly int[] buttonindxs = { 2, 4, 4, 4, 1, 3, 5, 7 },
            purchasedWeapons = { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, upgradedLevels = { 0, 0, 0, 0, 0, 0, 0, 0 }, direc = { 0, 0, 0, 0 }
            ;
        private static readonly string[] buttonsnames = { "fullscreen", "controls", "quit", "resume", "start", "shop", "settings", "quit", "defgun", "pulsegunscatter", "bounguns", "pulsegun", "canongun", "particlegun", "missile", "proton", "deathdronelauncher", "guidedmissile", "replicatingbulletgun", "crit.damage upgrade", "crit.chance upgrade", "durability upgrade", "energycon upgrade", "fire rate upgrade", "fuelcon upgrade", "weapon level upgrade", "unknown" }, 
            volbutsnames = { "mvol", "svol", "mvol+", "svol+", "mvol-", "svol-" },
            interfaceNames = { "mainbox", "frame", "energybar", "fuelbar", "hpbar", "distancebar", "distancepointer", "weaponbox", "menubox", "currweaplabel", "critdmglab", "critchanlab", "durlab", "fuelconlab", "energyconlab", "fireratelab", "maxlvlweaplab", "coinsLabel" }
            ;
        private static readonly Point[] volbuttoncords = new Point[] { new Point (365,176), new Point(365,240) , new Point(493, 177 ), new Point(493, 240), new Point(335,183) , new Point(335, 248) };
        private static readonly Color[] bgcolors = { Color.FromArgb(38, 38, 38), Color.FromArgb(235, 235, 235), Color.FromArgb(0, 168, 243), Color.FromArgb(228, 255, 147), Color.FromArgb(205, 255, 253), Color.FromArgb(255, 225, 225), Color.FromArgb(160, 169, 148) },
            interfaceColors = { Color.FromArgb(38, 38, 38), Color.FromArgb(38, 38, 38), Color.FromArgb(255, 255, 192), Color.FromArgb(255, 95, 101), Color.FromArgb(224, 224, 224), Color.Yellow, Color.Red, Color.FromArgb(123, 22, 255) }
            ;
        public static int[] playerskillscounters = new int[] { -1, -1, -1 }, skillframecounters = new int[] { -1, -1, -1 };

        private static readonly Dictionary<string, int> labelWeapNamesValue = new Dictionary<string, int>() {
            { "Laser Gun", 0},
            { "Scattered Pulse Gun", 0},
            { "Bouncing Laser Gun",  500},
            { "Pulse Gun", 800},
            { "Canon Gun",  1200},
            { "Particle Gun",  1500},
            { "Missile Lancher",  2000},
            { "Proton Gun",  3000},
            { "Death Drone Launcher", 4000},
            { "Guided Missile Launcher", 6000},
            { "Replicating Bullet Gun", 7500},
        };

        private static readonly Dictionary<string, int[]> labelUPGNamesValue = new Dictionary<string, int[]>() {
            { "Critical Damage Upgrade", new int []{ 400, 4, 2, 2 } },
            { "Critical Chance Upgrade", new int []{ 350, 4, 2, 2 } },
            { "Durability Upgrade",  new int []{ 300, 4, 4 , 4 } },
            { "Energy Consumption Upgrade", new int []{ 250, 2, 4, 2 } },
            { "Fire Rate Upgrade",  new int []{ 500, 4, 4, 4 } },
            { "Fuel Consumption Upgrade",  new int []{ 200, 2, 2, 4 } },
            { "weapon level upgrade",  new int []{ 400, 6, 4, 4 } },
            { "Unknown",  new int []{ 20000 } },
        };

        private static readonly int[,] interfaceCords = new int[18, 2] { { 0, 0 }, { 1449, 2 }, { 1511, 75 }, { 1511, 121 }, { 1511, 167 }, { 1514, 217 }, { 1514, 212 }, { 1484, 275 }, { 1491, 795 }, { 1520, 279 }, { 1529, 347 }, { 1529, 394 }, { 1523, 440 }, { 1529, 488 }, { 1529, 535 }, { 1529, 582 }, { 1530, 628 }, { 1520, 30 } };
        private static readonly int[,] interfaceSizes = new int[18, 2] { { 1443, 758 }, { 185, 870 }, { 100, 23 }, { 100, 23 }, { 100, 23 }, { 100, 8 }, { 5, 20 }, { 30, 30 }, { 100, 30 }, { 64, 25 }, { 58, 25 }, { 39, 25 }, { 80, 25 }, { 42, 25 }, { 23, 25 }, { 23, 25 }, { 23, 25 }, { 0, 20 } };
        private static readonly string[] interfaceLabTxt = { "level 1", "100%", "0%", "100/100", "100", "10", "5", "9", "0" };

        public static readonly List<PictureBox> gameboxlist = new List<PictureBox> { new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox(), new PictureBox() },
            shopWeapButlist = new List<PictureBox>(), shopUpgradeButlist = new List<PictureBox>(), buttonslist = new List<PictureBox>(), volbutlist = new List<PictureBox>()
            ;
        public static readonly List<Label> gamelabellist = new List<Label> { new Label(), new Label(), new Label(), new Label(), new Label(), new Label(), new Label(), new Label(), new Label() },
            barlabellist = new List<Label>(), labellist = new List<Label>()
            ;
        private static readonly List<PictureBox> barboxlist = new List<PictureBox>();
        private static PictureBox skillinterfacebox = new PictureBox(), labelbox;
        private static Label shoplabel, outrolabel;
        private static double height, width;

        private static readonly Random random = new Random();
        private static readonly Bitmap bitmap = new Bitmap(1464, 873), mainbitmap = new Bitmap(700, 785);
        private static bool keylockSpace = true, specialConWin = false, specialConLose = false, outro = false, spawnanotherboss = false, isgamemusic = false;
        private static readonly Graphics g = Graphics.FromImage(bitmap);
        private static readonly System.Windows.Media.MediaPlayer m_mediaPlayer = new System.Windows.Media.MediaPlayer(), m_mediaPlayer1 = new System.Windows.Media.MediaPlayer();
        private static List<Image> menuimgs, bulletimgs;

        public Game()
        {
            InitializeComponent();
            mainbox.Parent = this;
            mainbox.Name = "map";
        }

        private void Game_Start(object sender, EventArgs e)
        {
            height = System.Windows.SystemParameters.FullPrimaryScreenHeight;
            width = System.Windows.SystemParameters.FullPrimaryScreenWidth;
            timeBeginPeriod(1);
            mHandler = new TimerEventHandler(TimerCallback);
            mTimerId = timeSetEvent(1, 0, mHandler, IntPtr.Zero, EVENT_TYPE); // can play with mTestTick and delay (first parameter)
            mTestStart = DateTime.Now;
            mTestTick = 0;
            string fileline = "";
            string asmbstring = "";
            try
            {
                StreamReader sr = File.OpenText(@"C:\Users\\source\repos\spacebattle\spacebattle\enn.vla");
                fileline = sr.ReadLine();
                sr.Close();
            }
            catch {
                string text = "";
                StreamWriter sw = File.CreateText(@"C:\Users\\source\repos\spacebattle\spacebattle\enn.vla"); // if file not found Create one
                for (int i = 0; i < (purchasedWeapons.Length + upgradedLevels.Length + Playerobj.inverntory.Length); i++) { //write purchasedWeapons first then upgradedLevels.Length then Playerobj.inverntory 
                    if (i < purchasedWeapons.Length) {
                        text += purchasedWeapons[i].ToString() + ",";
                    } else if (i >= purchasedWeapons.Length && i < (upgradedLevels.Length + purchasedWeapons.Length)) {
                        text += upgradedLevels[i - purchasedWeapons.Length].ToString() + ",";
                    } else if (i >= (upgradedLevels.Length + purchasedWeapons.Length) && i < (Playerobj.inverntory.Length + upgradedLevels.Length + purchasedWeapons.Length)) {
                        text += Playerobj.inverntory[i - purchasedWeapons.Length - upgradedLevels.Length].ToString() + ",";
                    }
                }
                text += "1$0";
                sw.WriteLine(text);
                sw.Flush();
                sw.Close();
                StreamReader sr = File.OpenText(@"C:\Users\\source\repos\spacebattle\spacebattle\enn.vla");
                fileline = sr.ReadLine();
                sr.Close();
            }
            int j = 0;
            int parsedint = 0;
            for (int i = 0; i < (purchasedWeapons.Length + upgradedLevels.Length + Playerobj.inverntory.Length); i++) // decode text line back to Playerobj stats and inventory
            {
                if (i < purchasedWeapons.Length)
                {
                    for (; j < fileline.Length; j++) {
                        if (fileline[j] != ',') {
                            asmbstring += fileline[j];
                        } else {
                            purchasedWeapons[i] = int.Parse(asmbstring);
                            if (int.Parse(asmbstring) == 1) {
                                Dropobj.dropEnable[i + 5] = 1;
                            }
                            asmbstring = "";
                            j++;
                            break;
                        }
                    }
                }
                else if (i >= purchasedWeapons.Length && i < (upgradedLevels.Length + purchasedWeapons.Length))
                {
                    for (; j < fileline.Length; j++)
                    {
                        if (fileline[j] != ',')
                        {
                            asmbstring += fileline[j];
                        }
                        else
                        {
                            parsedint = int.Parse(asmbstring);
                            upgradedLevels[i - purchasedWeapons.Length] = parsedint;
                            switch (i - purchasedWeapons.Length) {
                                case 0:
                                    Playerobj.maxCritDamage = 100 + (parsedint * 10);
                                    if (parsedint != 0) {
                                        Dropobj.dropEnable[17] = 1;
                                    }
                                    break;
                                case 1:
                                    Playerobj.maxCritChance = parsedint * 10;
                                    if (parsedint != 0)
                                    {
                                        Dropobj.dropEnable[16] = 1;
                                    }
                                    break;
                                case 2:
                                    Playerobj.maxHP = 100 + (parsedint * 100);
                                    Playerobj.HP = Playerobj.maxHP;
                                    interfaceLabTxt[3] = Playerobj.maxHP.ToString() + "/" + Playerobj.maxHP.ToString();
                                    playerHPRatio = Playerobj.HP / 100;
                                    break;
                                case 3:
                                    Playerobj.energyCons = 10 - parsedint;
                                    break;
                                case 4:
                                    Playerobj.maxFireRate = 5 + parsedint;
                                    if (parsedint != 0)
                                    {
                                        Dropobj.dropEnable[18] = 1;
                                    }
                                    break;
                                case 5:
                                    Playerobj.fuelCons = 10 - parsedint;
                                    interfaceLabTxt[4] = Playerobj.fuelCons.ToString();
                                    break;
                                case 6:
                                    Playerobj.bulletMaxLevel = parsedint;
                                    interfaceLabTxt[7] = (Playerobj.bulletMaxLevel + 1).ToString();
                                    if (parsedint != 0)
                                    {
                                        Dropobj.dropEnable[4] = 1;
                                    }
                                    break;
                            }
                            asmbstring = "";
                            j++;
                            break;
                        }
                    }

                }
                else if (i >= (upgradedLevels.Length + purchasedWeapons.Length) && i < (Playerobj.inverntory.Length + upgradedLevels.Length + purchasedWeapons.Length))
                {
                    for (; j < fileline.Length; j++)
                    {
                        if (fileline[j] != ',')
                        {
                            asmbstring += fileline[j];
                        }
                        else
                        {
                            Playerobj.inverntory[i - purchasedWeapons.Length - upgradedLevels.Length] = int.Parse(asmbstring);
                            asmbstring = "";
                            j++;
                            break;
                        }
                    }
                }
            }
            for (; j < fileline.Length; j++)
            {
                if (fileline[j] != '$')
                {
                    asmbstring += fileline[j];
                }
                else if (fileline[j] == '$') 
                {
                    currWorlds = int.Parse(asmbstring);
                    break;
                }
            }
            bonusWorlds = (int)(fileline[fileline.Length - 1]);
            ModifySettings(0);
        }

        private void WriteToFile()
        {
            string text = "";
            StreamWriter sw = File.CreateText(@"C:\Users\\source\repos\spacebattle\spacebattle\enn.vla"); // if file not found Create one
            for (int i = 0; i < (purchasedWeapons.Length + upgradedLevels.Length + Playerobj.inverntory.Length); i++)
            { //write purchasedWeapons first then upgradedLevels.Length then Playerobj.inverntory 
                if (i < purchasedWeapons.Length)
                {
                    text += purchasedWeapons[i].ToString() + ",";
                }
                else if (i >= purchasedWeapons.Length && i < (upgradedLevels.Length + purchasedWeapons.Length))
                {
                    text += upgradedLevels[i - purchasedWeapons.Length].ToString() + ",";
                }
                else if (i >= (upgradedLevels.Length + purchasedWeapons.Length) && i < (Playerobj.inverntory.Length + upgradedLevels.Length + purchasedWeapons.Length))
                {
                    text += Playerobj.inverntory[i - purchasedWeapons.Length - upgradedLevels.Length].ToString() + ",";
                }
            }
            text += currWorlds.ToString() +'$'+ (char)bonusWorlds;
            sw.WriteLine(text);
            sw.Flush();
            sw.Close();
        }

        private void DrawGameimg()
        {
            int index = 0, posx, posy;
            g.Clear(bgcolors[(level - 1) / 3]);
            /*if (level < 7)
            {
                g.Clear(bgcolors[(level - 1) / 3]);
            }
            else
            {
                g.DrawImage(imgs.bgimgs[(level-7) / 3], 0, 0);
            }*/
            foreach (int misckey in Miscobj.miscords.Keys)
            {
                g.DrawImage(Miscobj.miscimgs[Miscobj.misctypes[misckey]], Miscobj.miscords[misckey][0], Miscobj.miscords[misckey][1]);
            }
            foreach (int planetkey in Planetobj.planetcords.Keys)
            {
                g.DrawImage(Planetobj.planetimgs[Planetobj.planetypes[planetkey]], Planetobj.planetcords[planetkey][0], Planetobj.planetcords[planetkey][1]);
            }
            if (Bulletobj.bulletcords.Count != 0)
            {
                foreach (int bulletkey in Bulletobj.bulletcords.Keys)
                {
                    if (Bulletobj.guntype == 3 || Bulletobj.guntype == 0)
                    {
                        g.DrawImage(bulletimgs[Bulletobj.bulletypes[bulletkey]], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (Bulletobj.guntype == 1)
                    {
                        g.DrawImage(bulletimgs[0], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (Bulletobj.guntype == 2)
                    {
                        g.DrawImage(bulletimgs[Bulletobj.bulletypes[bulletkey]], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (Bulletobj.guntype == 4)
                    {
                        g.DrawImage(bulletimgs[3 - Bulletobj.bulletbounces[bulletkey]], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (Bulletobj.guntype == 5)
                    {
                        g.DrawImage(bulletimgs[Bulletobj.bulletypes[bulletkey] % 10], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (Bulletobj.guntype == 6)
                    {
                        g.DrawImage(bulletimgs[Bulletobj.bulletypes[bulletkey]], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (Bulletobj.guntype == 7)
                    {
                        if (Bulletobj.bulletypes[bulletkey] > 10)
                        { // check if bullet or particle
                            index = Bulletobj.bulletypes[bulletkey] - 10;
                        }
                        g.DrawImage(bulletimgs[index], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                        index = 0;
                    }
                    else if (Bulletobj.guntype == 8)
                    {
                        g.DrawImage(bulletimgs[Bulletobj.bulletypes[bulletkey]], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (Bulletobj.guntype == 9)
                    {
                        g.DrawImage(bulletimgs[0], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                    else if (Bulletobj.guntype == 10)
                    {
                        int imgindx;
                        if (Bulletobj.bulletypes[bulletkey] < 10)
                        {
                            imgindx = Bulletobj.bulletypes[bulletkey];
                            if (imgindx == 2)
                                imgindx = 1;
                        }
                        else
                        {
                            imgindx = (Bulletobj.bulletypes[bulletkey] / 10) + 1;
                        }
                        g.DrawImage(bulletimgs[imgindx], Bulletobj.bulletcords[bulletkey][0], Bulletobj.bulletcords[bulletkey][1]);
                    }
                }
            }
            if (Enemyobj.enemycords.Count != 0)
            {
                foreach (int enemykey in Enemyobj.enemycords.Keys)
                {
                    g.DrawImage(Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]], Enemyobj.enemycords[enemykey][0], Enemyobj.enemycords[enemykey][1]);
                }
            }
            if (Bulletobj.enemybulletcords.Count != 0)
            {
                foreach (int enemybulletkey in Bulletobj.enemybulletcords.Keys)
                {
                    g.DrawImage(Bulletobj.enemybulletimgs[Bulletobj.enemybullet[enemybulletkey][4]][Bulletobj.enemybullet[enemybulletkey][3]], Bulletobj.enemybulletcords[enemybulletkey][0], Bulletobj.enemybulletcords[enemybulletkey][1]);
                }
            }
            if (Bulletobj.enemybossbulletcords.Count != 0)
            {
                foreach (int enemybossbulletkey in Bulletobj.enemybossbulletcords.Keys)
                {
                    g.DrawImage(Bulletobj.enemybossbulletimgs[Bulletobj.enemybossbullet[enemybossbulletkey][4]][Bulletobj.enemybossbullet[enemybossbulletkey][3]], Bulletobj.enemybossbulletcords[enemybossbulletkey][0], Bulletobj.enemybossbulletcords[enemybossbulletkey][1]);
                }
            }
            if (Bulletobj.skillbulletcords.Count != 0)
            {
                foreach (int skillbulletkey in Bulletobj.skillbulletcords.Keys)
                {
                    g.DrawImage(Bulletobj.playerskillbulletimgs[Bulletobj.skillbullet[skillbulletkey][5]][Bulletobj.skillbullet[skillbulletkey][3]], Bulletobj.skillbulletcords[skillbulletkey][0], Bulletobj.skillbulletcords[skillbulletkey][1]);
                }
            }
            if (Dropobj.dropcords.Count != 0)
            {
                foreach (int dropkey in Dropobj.dropcords.Keys)
                {
                    int droptype = Dropobj.droptypes[dropkey];
                    if (droptype < 100)
                    {
                        g.DrawImage(imgs.dropimgs[droptype], Dropobj.dropcords[dropkey][0], Dropobj.dropcords[dropkey][1]);
                    }
                    else {
                        if (droptype < 103)
                        {
                            g.DrawImage(Skillobj.skilldropimgs[droptype - 100], Dropobj.dropcords[dropkey][0], Dropobj.dropcords[dropkey][1]);
                        }
                        else
                        {
                            g.DrawImage(Skillobj.skilldropimgs[((((level-1) / 3) * 5) + (droptype - 100))], Dropobj.dropcords[dropkey][0], Dropobj.dropcords[dropkey][1]);
                        }
                    }
                }
            }
            if (Skillobj.enemySkills.Count != 0) //draw skill img above enemy
            {
                foreach (int enemykey in Skillobj.enemySkills.Keys)
                {
                    if (Skillobj.enemySkills[enemykey][4] != 0) {
                        if (Enemyobj.enemystats[enemykey][1] >= 1000)
                        {
                            posx = Enemyobj.enemycords[enemykey][0] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width / 2) - (Skillobj.enemyskillframes[Skillobj.enemySkills[enemykey][0]][Skillobj.enemySkills[enemykey][3]].Width / 2);
                        }
                        else {
                            posx = Enemyobj.enemycords[enemykey][0] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width / 2) - (Skillobj.enemyskillframes[Skillobj.enemySkills[enemykey][0]][Skillobj.enemySkills[enemykey][3]].Width / 2);
                        }
                        posy = Enemyobj.enemycords[enemykey][1] - 30;
                        g.DrawImage(Skillobj.enemyskillframes[Skillobj.enemySkills[enemykey][0]][Skillobj.enemySkills[enemykey][3]], posx, posy);
                    }
                }
            }
            g.DrawImage(Playerobj.playerImg, Playerobj.cords[0], Playerobj.cords[1]);

            //draw skill bar and skill imgs

            g.DrawImage(imgs.menuimgs[5], 600, 760);
            g.DrawImage(imgs.menuimgs[6], 0, 0);
            g.DrawImage(imgs.menuimgs[6], 0, 870);
            g.DrawImage(imgs.menuimgs[7], 0, 0);
            int greendim, duratio;
            int cordx, cordy;
            for (int i = 0; i < 3; i++) {

                duratio = (int)(50 * (double)Skillobj.playerSkills.ElementAt(i).Value[0] / (double)Skillobj.playerskilldata[Skillobj.playerSkills.ElementAt(i).Key][0]);//duration ratio
                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), 634 + (90 * i), 837, 52, 5); //skill duration bar rect fills
                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 255, 255)), 635 + (90 * i), 838, duratio, 3); //skill duration bar rect fills

                greendim = 50 - (int)(50 * (double)Skillobj.playerSkills.ElementAt(i).Value[1] / (double)Skillobj.playerskilldata[Skillobj.playerSkills.ElementAt(i).Key][1]);//cooldown ratios
                if (playerskillscounters[i] == -1)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(196, 255, 14)), 635 + (90 * i), 785, 50, 50); //skills rect fills
                }
                else {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(255, 100, 100)), 635 + (90 * i), 785, 50, 50);  //skills rect fills
                    g.FillRectangle(new SolidBrush(Color.FromArgb(196, 255, 14)), 635 + (90 * i), 785, 50, greendim);
                }
                g.DrawImage(Skillobj.skillbarimgs[Skillobj.playerSkills.ElementAt(i).Key], 635 + (90 * i), 785);
                int skillid = Skillobj.playerSkills.ElementAt(i).Key;
                if (playerskillscounters[i] != -1 && Skillobj.playerSkills[skillid][0] != 0) {          // draw skill animation
                    if (skillid == 2 || skillid == 20)
                    {
                        cordx = Playerobj.cords[0] + 78;
                        cordy = Playerobj.cords[1] + 25;
                    }
                    else {
                        cordx = Playerobj.cords[0] + 13 + (i * 22);
                        cordy = Playerobj.cords[1] - 22;
                    }
                    g.DrawImage(Skillobj.skillframes[skillid][skillframecounters[i]], cordx, cordy);
                }
            }
            if (outro) {
                Color[] colors = new Color[] { Color.Black, Color.White, Color.Red };
                int colorindx = 0;
                if (holecounter % 13 == 0) {
                    if (colorindx == 0)
                    {
                        colorindx = 1;
                    }
                    else {
                        colorindx = 0;
                    }
                }
                outrolabel.ForeColor = colors[colorindx + 1];
                g.FillRectangle(new SolidBrush(colors[colorindx]), 700 - (7 * holecounter), 400 - (4 * holecounter), 16 * holecounter, 8 * holecounter);
                holecounter += 1;
            }
            mainbox.BackColor = bgcolors[level / 4];
            mainbox.Image = bitmap;
            //mainbox.BringToFront();
        }

        private void DrawGameInterface(int xscale, int yscale) {
            Font font = new Font("Script MT Bold", 16);
            for (int i = 1; i < interfaceNames.Length; i++) {
                if (i < 9) {
                    PictureBox inerfacebox = new PictureBox();
                    if (i > 7)
                    {
                        inerfacebox.BackColor = interfaceColors[7];
                    }
                    else
                    {
                        inerfacebox.BackColor = interfaceColors[i];
                    }
                    if (i == 1) {
                        inerfacebox.Image = imgs.menuimgs[2];
                    } else if (i == 7) {
                        if (main == 1 || main == 2) {
                            inerfacebox.Image = imgs.dropimgs[5+ DropImgIndex()];
                        } else {
                            inerfacebox.Image = imgs.dropimgs[5];
                        }
                    }
                    else if (i == 8)
                    {
                        inerfacebox.Image = imgs.menuimgs[0];
                        if (main == 1 || main == 2) {
                            inerfacebox.Image = imgs.menuimgs[1];
                        }
                        inerfacebox.Click += new EventHandler(Button_Click);
                        inerfacebox.MouseHover += new EventHandler(Mouse_Hover);
                        inerfacebox.MouseLeave += new EventHandler(Mouse_Leave);

                    }
                    if (i != 0) {
                        if (main == 1 || main == 2) {
                            switch (i)
                            {
                                case 2:
                                    inerfacebox.Size = new Size((int)(100*((double)Playerobj.energy/(double)Playerobj.maxEnergy)), 23);
                                    break;
                                case 3:
                                    inerfacebox.Size = new Size(Playerobj.fuel, 23);
                                    break;
                                case 4:
                                    inerfacebox.Size = new Size((int)(100 * ((double)Playerobj.HP / (double)Playerobj.maxHP)), 23);
                                    break;
                                default:
                                    inerfacebox.Size = new Size(interfaceSizes[i, 0], interfaceSizes[i, 1]);
                                    break;
                            }
                        } else {
                            inerfacebox.Size = new Size(interfaceSizes[i, 0], interfaceSizes[i, 1]);
                        }
                    }
                    if (i == 6 && (main == 1 || main == 2)) {
                        inerfacebox.Location = new Point(interfaceCords[i, 0] + xscale + (Distance / (duration / 100)), interfaceCords[i, 1] + yscale);
                    } else
                    {
                        inerfacebox.Location = new Point(interfaceCords[i, 0] + xscale, interfaceCords[i, 1] + yscale);
                    }
                    inerfacebox.Parent = this;//this.GetChildAtPoint(inerfacebox.Location);
                    inerfacebox.Name = interfaceNames[i];
                    inerfacebox.BringToFront();
                    gameboxlist[i] = inerfacebox;
                } else {
                    Label inerfacelabel = new Label
                    {
                        Font = font,
                        Location = new Point(interfaceCords[i, 0] + xscale, interfaceCords[i, 1] + yscale),
                        ForeColor = Color.White
                    };
                    if (i > 7) {
                        inerfacelabel.BackColor = interfaceColors[interfaceColors.Length - 1];
                    }
                    inerfacelabel.AutoSize = true;
                    inerfacelabel.Parent = this;//this.GetChildAtPoint(inerfacelabel.Location);
                    inerfacelabel.Name = interfaceNames[i];
                    if (main == 1 || main == 2) {
                        switch (i) {
                            case 9:
                                inerfacelabel.Text = "level "+ Bulletobj.bulletLevel.ToString();
                                break;
                            case 10:
                                inerfacelabel.Text = Playerobj.critDamage.ToString();
                                break;
                            case 11:
                                inerfacelabel.Text = Playerobj.critChance.ToString();
                                break;
                            case 12:
                                inerfacelabel.Text = Playerobj.HP.ToString() +"/"+ Playerobj.maxHP.ToString();
                                break;
                            case 13:
                                inerfacelabel.Text = Playerobj.fuelCons.ToString();
                                break;
                            case 14:
                                inerfacelabel.Text = Playerobj.energyCons.ToString();
                                break;
                            case 15:
                                inerfacelabel.Text = Playerobj.fireRate.ToString();
                                break;
                            case 16:
                                inerfacelabel.Text = (Playerobj.bulletMaxLevel+1).ToString();
                                break;
                            case 17:
                                inerfacelabel.Text = Playerobj.coins.ToString();
                                break;
                        }
                    } else {
                        inerfacelabel.Text = interfaceLabTxt[i - 9];
                    }
                    inerfacelabel.BringToFront();
                    gamelabellist[i - 9] = inerfacelabel;
                }
            }
            PictureBox skillbox = new PictureBox();
            skillinterfacebox = skillbox;
            skillinterfacebox.BackColor = Color.FromArgb(123, 22, 255);
            skillinterfacebox.Size = new Size(50, 50);
            skillinterfacebox.Location = new Point(1515 + xscale, 720 + yscale);
            if ((main == 1 || main == 2) && Playerobj.pocketskill != -1) {
                skillinterfacebox.Image = Skillobj.skillbarimgs[Playerobj.pocketskill];
                skillinterfacebox.BackColor = Color.FromArgb(0, 255, 255);

            }
            skillinterfacebox.Parent = this;//this.GetChildAtPoint(inerfacebox.Location);
            skillinterfacebox.BringToFront();

        }

        private void DrawSettingsimg()
        {
            RemoveMenuButtons();
            if (Playerobj.colides != null && Playerobj.colides.Parent != null)
            {
                Playerobj.colides.Parent.Controls.Remove(Playerobj.colides);
            }
            if (Playerobj.skillcolides != null && Playerobj.skillcolides.Parent != null) {
                Playerobj.skillcolides.Parent.Controls.Remove(Playerobj.skillcolides);
            }
            if (Playerobj.consumecolides != null && Playerobj.consumecolides.Parent != null)
            {
                Playerobj.consumecolides.Parent.Controls.Remove(Playerobj.consumecolides);
            }
            Playerobj.labelcounter = 0;
            Playerobj.skilllabelcounter = 0;
            Playerobj.consumelabelcounter = 0;
            for (int i = 0; i < (buttonindxs.Length - 4); i++)
            {
                PictureBox button = new PictureBox
                {
                    BackColor = Color.Transparent
                };
                if (mainmenu == 1)
                {
                    button.Location = new Point(430, 310 + (i*72));
                }
                else {
                    button.Location = new Point(400 + buttoncords[i, 0], buttoncords[i, 1] - 5);
                }
                if (i == 0)
                {
                    button.Image = imgs.settingsimgs[buttonindxs[i] + fullscreen];
                }
                else
                {
                    button.Image = imgs.settingsimgs[buttonindxs[i]];
                }
                button.SizeMode = PictureBoxSizeMode.AutoSize;
                button.Parent = mainbox;
                button.Name = buttonsnames[i];
                button.BringToFront();
                buttonslist.Add(button);
                button.Click += new EventHandler(Button_Click);
            }
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            if (mainmenu == 1) {
                g.DrawImage(imgs.settingsimgs[0], 150, 130);  // draw settings
            } else
            {
                g.DrawImage(imgs.settingsimgs[0], 550, 125);  // draw ingame settings
            }
            for (int j = 0; j < 6; j++)
            {
                PictureBox volbutton = new PictureBox
                {
                    BackColor = Color.Transparent,
                    Name = volbutsnames[j]
                };
                if (mainmenu == 1) {
                    if (j == 0)
                    {
                        volbutton.Location = new Point(volbuttoncords[j].X + mVolume, volbuttoncords[j].Y) ;
                    }
                    else if (j == 1)
                    {
                        volbutton.Location = new Point(volbuttoncords[j].X + sVolume, volbuttoncords[j].Y);
                    }
                    else {
                        volbutton.Location = volbuttoncords[j];
                    }
                } else {
                    if (j == 0)
                    {
                        volbutton.Location = new Point(volbuttoncords[j].X + 400 + mVolume, volbuttoncords[j].Y - 6);
                    }
                    else if (j == 1)
                    {
                        volbutton.Location = new Point(volbuttoncords[j].X + 400 + sVolume, volbuttoncords[j].Y - 6);
                    }
                    else
                    {
                        volbutton.Location = new Point(volbuttoncords[j].X + 400, volbuttoncords[j].Y - 6);
                    }
                }
                if (j < 2)
                {
                    volbutton.Image = imgs.settingsimgs[5];
                }
                else if (j >= 2 && j < 4)
                {
                    volbutton.Image = imgs.settingsimgs[6];
                }
                else {
                    volbutton.Image = imgs.settingsimgs[7];
                }
                volbutton.SizeMode = PictureBoxSizeMode.AutoSize;
                volbutton.Parent = mainbox;
                volbutton.Click += new EventHandler(Button_Click);
                volbutton.MouseHover += new EventHandler(Mouse_Hover);
                volbutton.MouseLeave += new EventHandler(Mouse_Leave);
                volbutlist.Add(volbutton);
            }
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Image = bitmap;
            mainbox.Parent = this;
            mainbox.Name = "settings";
        }

        private void DrawPreGameMenuimg()
        {
            RemoveMenuButtons();
            Font font = new Font("Cooper Black", 14);
            Graphics g = Graphics.FromImage(mainbitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            g.DrawImage(imgs.preGameMenuimgs[0], 0, 0);
            int k = -150;
            int j = 0;
            for (int i = 0; i < currWorlds; i++)
            {
                PictureBox startbutbox = new PictureBox
                {
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Image = imgs.preGameMenuimgs[1]
                }; // Create world buttons
                if (i % 8 == 0)
                {
                    k += 170;
                    j = 0;
                }
                startbutbox.Location = new Point(80 + k, 130 + (j * 55));
                startbutbox.Parent = mainbox;
                startbutbox.Name = "world " + (i + 1).ToString();
                startbutbox.BringToFront();
                buttonslist.Add(startbutbox);
                startbutbox.Click += new EventHandler(Button_Click);
                startbutbox.MouseHover += new EventHandler(Mouse_Hover);
                startbutbox.MouseLeave += new EventHandler(Mouse_Leave);
                Label preGameLabel = new Label
                {
                    Font = font,
                    Location = new Point(110 + k, 143 + (j * 55)),
                    ForeColor = Color.Black,
                    BackColor = Color.FromArgb(255, 255, 255),
                    AutoSize = true
                };  // Create 30 labels
                preGameLabel.Parent = mainbox;
                preGameLabel.Name = "world " + (i + 1).ToString();
                preGameLabel.Text = "world " + (i + 1).ToString();
                preGameLabel.BringToFront();
                labellist.Add(preGameLabel);
                preGameLabel.MouseHover += new EventHandler(Mouse_Hover);
                preGameLabel.MouseLeave += new EventHandler(Mouse_Leave);
                preGameLabel.Click += new EventHandler(Button_Click);
                j++;
            }
            j = 0;
            if (bonusWorlds != 0) {
                for (int i = 97; i < (bonusWorlds + 1); i++)
                {
                    PictureBox startbutbox = new PictureBox
                    {
                        BackColor = Color.Transparent,
                        SizeMode = PictureBoxSizeMode.AutoSize,
                        Image = imgs.preGameMenuimgs[1]
                    }; // Create bonus buttons
                    startbutbox.Location = new Point(440, 130 + (j * 55));
                    startbutbox.Parent = mainbox;
                    startbutbox.Name = "world " + (char)i;
                    startbutbox.BringToFront();
                    buttonslist.Add(startbutbox);
                    startbutbox.Click += new EventHandler(Button_Click);
                    startbutbox.MouseHover += new EventHandler(Mouse_Hover);
                    startbutbox.MouseLeave += new EventHandler(Mouse_Leave);
                    Label preGameLabel = new Label
                    {
                        Font = font,
                        Location = new Point(470, 143 + (j * 55)),
                        ForeColor = Color.Black,
                        BackColor = Color.FromArgb(255, 255, 255),
                        AutoSize = true
                    };  // Create labels
                    preGameLabel.Parent = mainbox;
                    preGameLabel.Name = "world " + (char)i;
                    preGameLabel.Text = "world " + (char)i;
                    preGameLabel.BringToFront();
                    labellist.Add(preGameLabel);
                    preGameLabel.MouseHover += new EventHandler(Mouse_Hover);
                    preGameLabel.MouseLeave += new EventHandler(Mouse_Leave);
                    preGameLabel.Click += new EventHandler(Button_Click);
                    j++;
                }
            }
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Image = mainbitmap;
            mainbox.Parent = this;
            mainbox.Name = "controls";
        }

        private void DrawControlsimg()
        {
            RemoveMenuButtons();
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            if (mainmenu == 1)
            {
                g.DrawImage(imgs.settingsimgs[1], 150, 130);  // draw settings
            }
            else {
                g.DrawImage(imgs.settingsimgs[1], 550, 125);  // draw ingame settings
            }
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Image = bitmap;
            mainbox.Parent = this;
            mainbox.Name = "controls";
        }

        private void DrawShopimg()
        {
            RemoveMenuButtons();
            menuimgs = new List<Image>{
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\shop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\sbut.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\xbut.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\smallbut.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\smallxbut.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\defgunshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\pulsegunscattershop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\boungunshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\pulsegunshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\canongunshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\particlegunshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\missileshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\protongunshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\deathdronelaunchershop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\guidedmissileshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\replicatingbulletgunshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\defgunshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\pulsegunscattershophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\boungunshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\pulsegunshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\canongunshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\particlegunshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\missileshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\protongunshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\deathdronelaunchershophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\guidedmissileshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\replicatingbulletgunshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\critdamageshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\critchanceshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\durabilityupshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\energyconshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\fireRateUpshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\fuelconshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\weaponupshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\unknownshop.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\critdamageshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\critchanceshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\durabilityupshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\energyconshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\fireRateUpshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\fuelconshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\weaponupshophov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\shop\unknownshophov.png")
            };
            Font font = new Font("Cooper Black", 10);
            Graphics g = Graphics.FromImage(mainbitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            g.DrawImage(menuimgs[0], 10, 0);
            for (int m = 0; m < 4; m++)
            {
                Label shopInvLabel = new Label
                {
                    Font = font,
                    Location = new Point(375 + (78 * m), 32),
                    ForeColor = Color.FromArgb(54, 0, 94),
                    BackColor = Color.Transparent,
                    AutoSize = true
                }; //Create inventory labels
                shopInvLabel.Parent = mainbox;
                shopInvLabel.Text = Playerobj.inverntory[3 - m].ToString();
                labellist.Add(shopInvLabel);
            }
            int j = 0;
            int k = 0;
            for (int i = 1; i < 12; i++) {
                PictureBox shoppur = new PictureBox
                {
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Image = menuimgs[i + 4],
                    Location = new Point(90 + (k * 159), 121 + (j * 116))
                }; // Create 11 weapons relative to shop imgs
                shoppur.Parent = mainbox;
                shoppur.Name = buttonsnames[i + 7];
                shoppur.BringToFront();
                buttonslist.Add(shoppur);
                shoppur.DoubleClick += new EventHandler(Button_DoubleClick);
                shoppur.MouseHover += new EventHandler(Mouse_Hover);
                shoppur.MouseLeave += new EventHandler(Mouse_Leave);
                if (i < 9) { // Create upgrades menu first eight relative to shipimgs
                    PictureBox shopupgrades = new PictureBox
                    {
                        BackColor = Color.Transparent,
                        SizeMode = PictureBoxSizeMode.AutoSize,
                        Image = menuimgs[i + 26],
                        Location = new Point(90 + (k * 159), 551 + (j * 116))
                    };
                    shopupgrades.Parent = mainbox;
                    shopupgrades.Name = buttonsnames[i + 18];
                    shopupgrades.BringToFront();
                    buttonslist.Add(shopupgrades);
                    shopupgrades.DoubleClick += new EventHandler(Button_DoubleClick);
                    shopupgrades.MouseHover += new EventHandler(Mouse_Hover);
                    shopupgrades.MouseLeave += new EventHandler(Mouse_Leave);
                    int l = 0;
                    int p = 0;
                    if (i != 8)
                    {
                        for (int n = 0; n < 8; n++)
                        {
                            PictureBox shopupgradesbox = new PictureBox
                            {
                                BackColor = Color.Transparent,
                                SizeMode = PictureBoxSizeMode.AutoSize,
                                Location = new Point(shopupgrades.Location.X + (p * 10), (shopupgrades.Location.Y + 42) + (l * 8))
                            }; // draw purchase upgrade status button
                            shopupgradesbox.Parent = mainbox;
                            shopupgradesbox.BringToFront();
                            shopUpgradeButlist.Add(shopupgradesbox);
                            if (n < upgradedLevels[i - 1])  //un-upgraded 
                            {
                                shopupgradesbox.Image = menuimgs[4];
                            }
                            else //upgraded already
                            {
                                shopupgradesbox.Image = menuimgs[3];
                            }
                            if ((n + 1) % 4 == 0)
                            {
                                l += 1;
                                p = -1;
                            }
                            p += 1;
                        }
                    }
                    else {
                        PictureBox shopupgradesbox = new PictureBox
                        {
                            BackColor = Color.Transparent,
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(shopupgrades.Location.X + 15, shopupgrades.Location.Y + 42)
                        }; // draw last purchase upgrade status button
                        shopupgradesbox.Parent = mainbox;
                        shopupgradesbox.BringToFront();
                        shopUpgradeButlist.Add(shopupgradesbox);
                        if (0 < upgradedLevels[7])
                        {
                            shopupgradesbox.Image = menuimgs[4];

                        }
                        else {
                            shopupgradesbox.Image = menuimgs[3];
                        }
                    }
                }
                if (purchasedWeapons[i - 1] == 0)
                {
                    PictureBox shopupgradesbox = new PictureBox
                    {
                        BackColor = Color.Transparent,
                        Image = menuimgs[1],
                        SizeMode = PictureBoxSizeMode.AutoSize,
                        Location = new Point(102 + (k * 160), 162 + (j * 116))
                    }; // draw weapon purchase status button
                    shopupgradesbox.Parent = mainbox;
                    shopupgradesbox.BringToFront();
                    shopWeapButlist.Add(shopupgradesbox);
                }
                else {
                    PictureBox shopupgradesbox = new PictureBox
                    {
                        BackColor = Color.Transparent,
                        Image = menuimgs[2],
                        SizeMode = PictureBoxSizeMode.AutoSize,
                        Location = new Point(102 + (k * 160), 162 + (j * 116))
                    }; // draw weapon already purchased status button
                    shopupgradesbox.Parent = mainbox;
                    shopupgradesbox.BringToFront();
                    shopWeapButlist.Add(shopupgradesbox);
                }
                if (i % 4 == 0 && i > 0) {
                    j += 1;
                    k = -1;
                }
                k += 1;
            }
            mainbox.BackColor = Color.FromArgb(38, 38, 38);
            mainbox.Image = mainbitmap;
            mainbox.Parent = this;
            mainbox.Name = "controls";
        }

        private void DrawMainMenuimg() //form game size = 1652, 815
        {
            if (menuimgs != null)
            {
                menuimgs.Clear();
            }
            menuimgs = new List<Image>
            {
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\mainmenu.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\menustartbut.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\menustartbutpres.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\menushopbut.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\menushopbutpres.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\menusettingsbut.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\menusettingsbutpres.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\menuquitbut.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\mainmenu\menuquitbutpres.png"),
            };
            this.Size = new Size(715, 815);
            double scalex = 0, scaley = 0;
            if (fullscreen == 1) {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                mainbox.BackColor = Color.FromArgb(38, 38, 38);
                if (width > 715) {
                    scalex = (width - 715) / 2;
                }
                if (height > 815) {
                    scaley = (height - 815) / 2;
                }
            }
            mainbox.Location = new Point((int)scalex, (int)scaley);
            mainbox.Image = mainbitmap;
            mainbox.Parent = this;
            mainbox.Name = "mainMenu";
            Graphics g = Graphics.FromImage(mainbitmap);
            g.Clear(Color.FromArgb(38, 38, 38));
            g.DrawImage(menuimgs[0], 0, 0);
            for (int i = 0; i < 4; i++) {
                PictureBox startbut = new PictureBox
                {
                    BackColor = Color.Transparent,
                    Location = new Point(200, 210 + (i * 150)),
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Image = menuimgs[buttonindxs[i + 4]],
                    Name = buttonsnames[i + 4]
                };
                startbut.Parent = mainbox;
                buttonslist.Add(startbut);
                startbut.MouseHover += new EventHandler(Mouse_Hover);
                startbut.MouseLeave += new EventHandler(Mouse_Leave);
                startbut.Click += new EventHandler(Button_Click);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox picBox)
            {
                if (gameOn == 2)
                {
                    if (picBox.Name == "menubox")
                    {
                        picBox.Image = imgs.menuimgs[1];
                        if (main == 2)  //return to the game from any sub menus
                        {
                            RemoveMenuButtons();
                            main = 0;
                            controlbut = 0;
                            resumebut = 0;
                        }
                        else
                        {
                            main = 1;
                        }
                    }
                }
                if (picBox.Name == "fullscreen")
                {
                    double scalex = 0, scaley = 0;
                    if (gameOn != 2)
                    {
                        if (width > 715)
                        {
                            scalex = (width - 715) / 2;
                        }
                        if (height > 815)
                        {
                            scaley = (height - 815) / 2;
                        }
                        if (fullscreen == 0)
                        {
                            fullscreen = 1;
                            RemoveMenuButtons();
                            height = System.Windows.SystemParameters.FullPrimaryScreenHeight;
                            width = System.Windows.SystemParameters.FullPrimaryScreenWidth;
                            this.FormBorderStyle = FormBorderStyle.None;
                            this.WindowState = FormWindowState.Maximized;
                            mainbox.Location = new Point((int)scalex, (int)scaley);
                            DrawSettingsimg();
                            fullscreen = 1;
                            picBox.Image = imgs.settingsimgs[2];
                        }
                        else
                        {
                            RemoveMenuButtons();
                            mainbox.Location = new Point(0, 0);
                            this.FormBorderStyle = FormBorderStyle.FixedSingle;
                            this.WindowState = FormWindowState.Normal;
                            fullscreen = 0;
                            DrawSettingsimg();
                        }
                    }
                    else {
                        if (width > mainbox.Width)
                        {
                            scalex = (width - mainbox.Width - 185) / 2;
                        }
                        if (height > mainbox.Height)
                        {
                            scaley = (height - mainbox.Height) / 2;
                        }
                        if (fullscreen == 0)
                        {
                            RemoveMenuButtons();
                            fullscreen = 1;
                            this.FormBorderStyle = FormBorderStyle.None;
                            this.WindowState = FormWindowState.Maximized;
                            RemoveGameInterface();
                            mainbox.Location = new Point((int)scalex, (int)scaley);
                            DrawGameInterface((int)scalex, (int)scaley);
                            DrawSettingsimg();
                        }
                        else {
                            RemoveMenuButtons();
                            //this.Size = new Size(1652, 900);
                            fullscreen = 0;
                            this.FormBorderStyle = FormBorderStyle.FixedSingle;
                            this.WindowState = FormWindowState.Normal;
                            RemoveGameInterface();
                            mainbox.Location = new Point(0, 0);
                            DrawGameInterface(0, 0);
                            DrawSettingsimg();
                        }
                    }
                    ModifySettings(1);
                }
                else if (picBox.Name == "controls")
                {
                    controlbut = 1;
                    return;
                }
                else if (picBox.Name == "resume")
                {
                    resumebut = 1;
                    gameboxlist[8].Image = imgs.menuimgs[0];
                    return;
                }
                else if (picBox.Name == "settings")
                {
                    settingsbut = 1;
                    return;
                }
                else if (picBox.Name == "shop")
                {
                    shopbut = 1;
                    return;
                }
                else if (picBox.Name == "start")
                {
                    startbut = 1;
                    return;
                }
                else if (picBox.Name == "quit")
                {
                    if (mainmenu == 1)
                    {
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        RemoveGameInterface();
                        RemoveMenuButtons();
                        gameOn = 0;
                        controlbut = 0;
                        resumebut = 0;
                        settingsbut = 0;
                    }
                } else if (picBox.Name == "mvol+" && volbutlist[0].Location.X < picBox.Location.X - 28) {
                    volbutlist[0].Location = new Point(volbutlist[0].Location.X + 10, volbutlist[0].Location.Y);
                    mVolume = 100 - ((picBox.Location.X - 28) - volbutlist[0].Location.X);
                    m_mediaPlayer1.Volume = mVolume / 100.0f;
                    ModifySettings(1);
                }
                else if (picBox.Name == "svol+" && volbutlist[1].Location.X < picBox.Location.X - 28)
                {
                    volbutlist[1].Location = new Point(volbutlist[1].Location.X + 10, volbutlist[1].Location.Y);
                    sVolume = 100 - ((picBox.Location.X - 28) - volbutlist[1].Location.X);
                    m_mediaPlayer.Volume = sVolume / 100.0f;
                    ModifySettings(1);
                }
                else if (picBox.Name == "mvol-" && volbutlist[0].Location.X > picBox.Location.X + 30)
                {
                    volbutlist[0].Location = new Point(volbutlist[0].Location.X - 10, volbutlist[0].Location.Y);
                    mVolume =  volbutlist[0].Location.X - (picBox.Location.X + 30);
                    m_mediaPlayer1.Volume = mVolume / 100.0f;
                    ModifySettings(1);
                }
                else if (picBox.Name == "svol-" && volbutlist[1].Location.X > picBox.Location.X + 30)
                {
                    volbutlist[1].Location = new Point(volbutlist[1].Location.X - 10, volbutlist[1].Location.Y);
                    sVolume = volbutlist[1].Location.X - (picBox.Location.X + 30);
                    m_mediaPlayer.Volume = sVolume / 100.0f;
                    ModifySettings(1);
                }
                else
                {
                    for (int i = 0; i < currWorlds; i++)
                    {
                        if (picBox.Name == "world " + (i + 1).ToString())
                        {
                            level = (i + 1);
                            gameOn = 1;
                            mainmenu = 0;
                            main = 0;
                            return;
                        }
                    }
                    for (int i = 97; i < (bonusWorlds + 1); i++)
                    {
                        if (picBox.Name == "world " + (char)i)
                        {
                            level = (i - 97) + 16;
                            gameOn = 1;
                            mainmenu = 0;
                            main = 0;
                            return;
                        }
                    }
                }
            } 
            else if (sender is Label lab) {
                for (int i = 0; i < currWorlds; i++)
                {
                    if (lab.Name == "world " + (i + 1).ToString())
                    {
                        level = (i + 1);
                        gameOn = 1;
                        mainmenu = 0;
                        main = 0;
                        return;
                    }
                }
                for (int i = 97; i < (bonusWorlds + 1); i++)
                {
                    if (lab.Name == "world " + (char)i)
                    {
                        level = (i - 97) + 16;
                        gameOn = 1;
                        mainmenu = 0;
                        main = 0;
                        return;
                    }
                }
            }
        }
        private void Button_DoubleClick(object sender, EventArgs e)
        {
            if (sender is PictureBox picBox)
            {
                int item;
                Boolean canPurchase;
                for (int i = 8; i < buttonsnames.Length; i++)
                {
                    canPurchase = true;
                    if (picBox.Name == buttonsnames[i])
                    {
                        if (i < buttonsnames.Length - 8)
                        {
                            if (purchasedWeapons[i - 8] == 0)
                            {
                                if (labelWeapNamesValue.ElementAt(i - 8).Value <= Playerobj.inverntory[0])
                                {
                                    Playerobj.inverntory[0] -= labelWeapNamesValue.ElementAt(i - 8).Value;
                                    labellist[3].Text = Playerobj.inverntory[0].ToString();
                                    purchasedWeapons[i - 8] = 1;
                                    shopWeapButlist[i - 8].Image = menuimgs[2];
                                    Dropobj.dropEnable[i - 3] = 1; //enable newly purchased drop
                                }
                                else
                                {
                                    labellist[3].ForeColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            if (upgradedLevels[i - 19] < 8)
                            {
                                if ((i - 19) < 7)
                                {
                                    for (int j = 0; j < 4; j++)
                                    {
                                        if (j == 0)
                                        {
                                            if ((labelUPGNamesValue.ElementAt(i - 19).Value[j] + (100 * upgradedLevels[i - 19])) > Playerobj.inverntory[j])
                                            {
                                                canPurchase = false;
                                                labellist[3 - j].ForeColor = Color.Red;
                                            }
                                        }
                                        else {
                                            if ((labelUPGNamesValue.ElementAt(i - 19).Value[j] + upgradedLevels[i - 19]) > Playerobj.inverntory[j])
                                            {
                                                canPurchase = false;
                                                labellist[3 - j].ForeColor = Color.Red;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (labelUPGNamesValue.ElementAt(i - 19).Value[0] > Playerobj.inverntory[0])
                                    {
                                        canPurchase = false;
                                        labellist[3].ForeColor = Color.Red;
                                    }
                                }
                                if (canPurchase)
                                {
                                    for (int k = 0; k < 4; k++)
                                    {
                                        if (k == 0)
                                        {
                                            Playerobj.inverntory[k] -= (labelUPGNamesValue.ElementAt(i - 19).Value[k] + (100 * upgradedLevels[i - 19]));
                                        }
                                        else
                                        {
                                            Playerobj.inverntory[k] -= (labelUPGNamesValue.ElementAt(i - 19).Value[k] + upgradedLevels[i - 19]);
                                        }
                                        labellist[3 - k].Text = Playerobj.inverntory[k].ToString();

                                    }
                                    upgradedLevels[i - 19] += 1;
                                    item = upgradedLevels[i - 19];
                                    shopUpgradeButlist[((i - 19) * 8) + upgradedLevels[i - 19] - 1].Image = menuimgs[4];
                                    switch (i - 19)
                                    {
                                        case 0:
                                            Playerobj.maxCritDamage = 100 + (item * 10);
                                            if (item == 0)
                                            {
                                                Dropobj.dropEnable[17] = 0;
                                            }
                                            else
                                            {
                                                Dropobj.dropEnable[17] = 1;
                                            }
                                            break;
                                        case 1:
                                            Playerobj.maxCritChance = item * 10;
                                            if (item == 0)
                                            {
                                                Dropobj.dropEnable[16] = 0;
                                            }
                                            else
                                            {
                                                Dropobj.dropEnable[16] = 1;
                                            }
                                            break;
                                        case 2:
                                            Playerobj.maxHP = 100 + (item * 100);
                                            Playerobj.HP = Playerobj.maxHP;
                                            playerHPRatio = Playerobj.HP / 100;
                                            interfaceLabTxt[3] = Playerobj.maxHP.ToString() + "/" + Playerobj.maxHP.ToString();
                                            break;
                                        case 3:
                                            Playerobj.energyCons = 10 - item;
                                            break;
                                        case 4:
                                            Playerobj.maxFireRate = 5 + item;
                                            if (item == 0)
                                            {
                                                Dropobj.dropEnable[18] = 0;
                                            }
                                            else
                                            {
                                                Dropobj.dropEnable[18] = 1;
                                            }
                                            break;
                                        case 5:
                                            Playerobj.fuelCons = 10 - item;
                                            interfaceLabTxt[4] = Playerobj.fuelCons.ToString();
                                            break;
                                        case 6:
                                            Playerobj.bulletMaxLevel = item;
                                            interfaceLabTxt[7] = (Playerobj.bulletMaxLevel + 1).ToString();
                                            if (item == 0)
                                            {
                                                Dropobj.dropEnable[4] = 0;
                                            }
                                            else
                                            {
                                                Dropobj.dropEnable[4] = 1;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                return;
            }
        }

        private void Mouse_Hover(object sender, EventArgs e) {
            Font font = new Font("Cooper Black", 10);
            if (sender is PictureBox picBox)
            {
                if (gameOn == 2)
                {
                    if (main == 0)
                    {
                        if (picBox.Name == "menubox")
                        {
                            picBox.Image = imgs.menuimgs[1];
                            return;
                        }
                    }
                }
                if (picBox.Name == "start")
                {
                    picBox.Image = menuimgs[2];
                    return;
                }
                else if (picBox.Name == "shop")
                {
                    picBox.Image = menuimgs[4];
                    return;
                }
                else if (picBox.Name == "settings")
                {
                    picBox.Image = menuimgs[6];
                    return;
                }
                else if (picBox.Name == "quit")
                {
                    picBox.Image = menuimgs[8];
                    return;
                } else if (picBox.Name == "mvol+" || picBox.Name == "svol+") {
                    picBox.Image= imgs.settingsimgs[8];
                } else if (picBox.Name == "mvol-" || picBox.Name == "svol-") {
                    picBox.Image = imgs.settingsimgs[9];
                }
                for (int z = 0; z < currWorlds; z++)
                {
                    if (picBox.Name == "world " + (z + 1).ToString())
                    {
                        picBox.Image = imgs.preGameMenuimgs[2];
                        labellist[buttonslist.IndexOf(picBox)].ForeColor = Color.White;
                        labellist[buttonslist.IndexOf(picBox)].BackColor = Color.FromArgb(195,195,195);
                        return;
                    }
                }
                for (int z = 97; z < (bonusWorlds + 1); z++)
                {
                    if (picBox.Name == "world " + (char)z)
                    {
                        picBox.Image = imgs.preGameMenuimgs[2];
                        labellist[buttonslist.IndexOf(picBox)].ForeColor = Color.White;
                        labellist[buttonslist.IndexOf(picBox)].BackColor = Color.FromArgb(195, 195, 195);
                        return;
                    }
                }
                for (int i = 8; i < buttonsnames.Length; i++)
                {

                    if (picBox.Name == buttonsnames[i])
                    {
                        if (i < buttonsnames.Length - 8) // weapon img relative to weapon names index
                        {
                            picBox.Image = menuimgs[i + 8];
                            Label desclabal = new Label();
                            shoplabel = desclabal;
                            shoplabel.Font = font;
                            shoplabel.ForeColor = Color.FromArgb(54, 0, 94);
                            shoplabel.BackColor = Color.Transparent;
                            shoplabel.AutoSize = true;
                            shoplabel.Parent = mainbox;
                            if (purchasedWeapons[i - 8] == 0)
                            {
                                shoplabel.Text = labelWeapNamesValue.ElementAt(i - 8).Key + " Price:  " + labelWeapNamesValue.ElementAt(i - 8).Value.ToString();
                                shoplabel.Location = new Point(((350 - shoplabel.Width) / 2), 32);
                                PictureBox boxlabel = new PictureBox();
                                labelbox = boxlabel;
                                labelbox.Location = new Point(shoplabel.Location.X + shoplabel.Width, shoplabel.Location.Y + 1);
                                labelbox.BackColor = Color.Transparent;
                                labelbox.SizeMode = PictureBoxSizeMode.AutoSize;
                                labelbox.Image = imgs.dropimgs[19];
                                labelbox.Parent = mainbox;
                            }
                            else
                            {
                                shoplabel.Text = labelWeapNamesValue.ElementAt(i - 8).Key + " Already Purchased!";
                                shoplabel.Location = new Point((350 - shoplabel.Width) / 2, 32);
                            }
                            return;
                        }
                        else
                        {
                            Font font1 = new Font("Cooper Black", 12);
                            picBox.Image = menuimgs[i + 16]; // upgrade img relative to upgrade names index
                            if (upgradedLevels[i - 19] < 8)
                            {
                                for (int k = 0; k < 4; k++)
                                {
                                    Label desclabal = new Label
                                    {
                                        Font = font1,
                                        ForeColor = Color.FromArgb(54, 0, 94),
                                        BackColor = Color.Transparent,
                                        AutoSize = true
                                    };
                                    desclabal.Parent = mainbox;
                                    barlabellist.Add(desclabal);
                                    if (k == 0)
                                    {
                                        desclabal.Text = labelUPGNamesValue.ElementAt(i - 19).Key + " Price:  " + (labelUPGNamesValue.ElementAt(i - 19).Value[k] + (100 * upgradedLevels[i - 19])).ToString();
                                    }
                                    else
                                    {
                                        desclabal.Text = " ," + (labelUPGNamesValue.ElementAt(i - 19).Value[k] + upgradedLevels[i - 19]).ToString() + " X ";
                                    }
                                    if (barboxlist.Count() != 0)
                                    {
                                        desclabal.Location = new Point((barboxlist[k - 1].Location.X + barboxlist[k - 1].Width) + 5, 477);
                                    }
                                    else
                                    {
                                        desclabal.Location = new Point(40, 477);
                                    }
                                    PictureBox boxlabel = new PictureBox();
                                    if (k == 1)
                                    {
                                        boxlabel.Location = new Point(desclabal.Location.X + desclabal.Width + 5, desclabal.Location.Y - 5);
                                    }
                                    else
                                    {
                                        boxlabel.Location = new Point(desclabal.Location.X + desclabal.Width + 5, desclabal.Location.Y + 5);
                                    }
                                    boxlabel.BackColor = Color.Transparent;
                                    boxlabel.SizeMode = PictureBoxSizeMode.AutoSize;
                                    boxlabel.Image = imgs.dropimgs[19 + k];
                                    boxlabel.Parent = mainbox;
                                    barboxlist.Add(boxlabel);
                                    if ((i - 19) == 7)
                                    {
                                        desclabal.Text = labelUPGNamesValue.ElementAt(i - 19).Key + ", Price:  " + labelUPGNamesValue.ElementAt(i - 19).Value[0].ToString();
                                        desclabal.Location = new Point(250, desclabal.Location.Y);
                                        boxlabel.Location = new Point(desclabal.Location.X + desclabal.Width, desclabal.Location.Y);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Label desclabal = new Label();
                                shoplabel = desclabal;
                                shoplabel.Font = font1;
                                shoplabel.ForeColor = Color.FromArgb(54, 0, 94);
                                shoplabel.BackColor = Color.Transparent;
                                shoplabel.AutoSize = true;
                                shoplabel.Parent = mainbox;
                                shoplabel.Text = labelUPGNamesValue.ElementAt(i - 19).Key + " is reached max level!";
                                shoplabel.Location = new Point((mainbox.Width - shoplabel.Width) / 5, 477);
                                barlabellist.Add(shoplabel);
                            }
                            return;
                        }
                    }
                }
            }
            else if (sender is Label label)
            {
                label.ForeColor = Color.White;
                label.BackColor = Color.FromArgb(195, 195, 195); ;
                buttonslist[labellist.IndexOf(label)].Image = imgs.preGameMenuimgs[2];
                return;
            }
        }

        private void Mouse_Leave(object sender, EventArgs e)
        {
            for (int j = 0; j < labellist.Count; j++) {
                if (shopbut == 2) {
                    labellist[j].ForeColor = Color.FromArgb(54, 0, 94);
                }
            }
            if (sender is PictureBox picBox)
            {
                if (gameOn == 2)
                {
                    if (main == 0)
                    {
                        if (picBox.Name == "menubox")
                        {
                            gameboxlist[8].Image = imgs.menuimgs[0];
                            return;
                        }
                    }
                }
                if (picBox.Name == "start")
                {
                    picBox.Image = menuimgs[1];
                    return;
                }
                else if (picBox.Name == "shop")
                {
                    picBox.Image = menuimgs[3];
                    return;
                }
                else if (picBox.Name == "settings")
                {
                    picBox.Image = menuimgs[5];
                    return;
                }
                else if (picBox.Name == "quit")
                {
                    picBox.Image = menuimgs[7];
                    return;
                }
                else if (picBox.Name == "mvol+" || picBox.Name == "svol+")
                {
                    picBox.Image = imgs.settingsimgs[6];
                }
                else if (picBox.Name == "mvol-" || picBox.Name == "svol-")
                {
                    picBox.Image = imgs.settingsimgs[7];
                }
                for (int z = 0; z < currWorlds; z++)
                {
                    if (picBox.Name == "world " + (z + 1).ToString())
                    {
                        picBox.Image = imgs.preGameMenuimgs[1];
                        labellist[buttonslist.IndexOf(picBox)].ForeColor = Color.Black;
                        labellist[buttonslist.IndexOf(picBox)].BackColor = Color.White;
                        return;
                    }
                }
                for (int z = 97; z < (bonusWorlds + 1); z++)
                {
                    if (picBox.Name == "world " + (char)z)
                    {
                        picBox.Image = imgs.preGameMenuimgs[1];
                        labellist[buttonslist.IndexOf(picBox)].ForeColor = Color.Black;
                        labellist[buttonslist.IndexOf(picBox)].BackColor = Color.White;
                        return;
                    }
                }
                for (int i = 8; i < buttonsnames.Length; i++)
                {
                    if (picBox.Name == buttonsnames[i])
                    {
                        if (i < buttonsnames.Length - 8)
                        {
                            picBox.Image = menuimgs[i - 3];
                            if (shoplabel != null && shoplabel.Parent != null)
                            {
                                shoplabel.Parent.Controls.Remove(shoplabel);
                                if (labelbox != null && labelbox.Parent != null)
                                {
                                    labelbox.Parent.Controls.Remove(labelbox);
                                }
                            }
                            return;
                        }
                        else
                        {
                            picBox.Image = menuimgs[i + 8];
                            if (barboxlist.Count != 0)
                            {
                                foreach (PictureBox box in barboxlist)
                                {
                                    if (box.Parent != null)
                                    {
                                        box.Parent.Controls.Remove(box);
                                    }
                                }
                                barboxlist.Clear();
                            }
                            if (barlabellist.Count != 0)
                            {
                                foreach (Label labe in barlabellist)
                                {
                                    if (labe.Parent != null)
                                    {
                                        labe.Parent.Controls.Remove(labe);
                                    }
                                }
                                barlabellist.Clear();
                            }
                            return;
                        }

                    }
                }
            }
            /*else if (sender is Label label)
            {
                label.ForeColor = Color.Black;
                label.BackColor = Color.White;
                return;
            }*/
        }

        private void RemoveGameInterface() {
            if (gameboxlist.Count != 0)
            {
                gameboxlist[8].Click -= Button_Click;
                gameboxlist[8].MouseHover -= Mouse_Hover;
                gameboxlist[8].MouseLeave -= Mouse_Leave;
                foreach (PictureBox butt in gameboxlist)
                {
                    if (butt != null && butt.Parent != null)
                    {
                        butt.Parent.Controls.Remove(butt);
                    }
                }
            }
            if (gamelabellist.Count != 0)
            {
                foreach (Label labe in gamelabellist)
                {
                    if (labe != null && labe.Parent != null)
                    {
                        labe.Parent.Controls.Remove(labe);
                    }
                }
            }
            if (outrolabel != null && outrolabel.Parent != null) {
                outrolabel.Parent.Controls.Remove(outrolabel);
            }
            if (shoplabel != null && shoplabel.Parent != null)
            {
                shoplabel.Parent.Controls.Remove(shoplabel);
            }
            if (labelbox != null && labelbox.Parent != null)
            {
                labelbox.Parent.Controls.Remove(labelbox);
            }
            if (skillinterfacebox != null && skillinterfacebox.Parent != null)
            {
                skillinterfacebox.Parent.Controls.Remove(skillinterfacebox);
            }
            if (Playerobj.colides.Parent != null)
            {
                Playerobj.colides.Parent.Controls.Remove(Playerobj.colides);
            }
            if (Playerobj.skillcolides.Parent != null)
            {
                Playerobj.skillcolides.Parent.Controls.Remove(Playerobj.skillcolides);
            }
            if (Playerobj.consumecolides.Parent != null)
            {
                Playerobj.consumecolides.Parent.Controls.Remove(Playerobj.consumecolides);
            }
            WriteToFile();
        }


        private void RemoveMenuButtons()
        {
            if (buttonslist.Count != 0) {
                foreach (PictureBox butt in buttonslist)
                {
                    butt.Click -= Button_Click;
                    if (settingsbut == 2 || startbut == 2 || mainmenu == 1)
                    {
                        butt.MouseHover -= Mouse_Hover;
                        butt.MouseLeave -= Mouse_Leave;
                    }
                    butt.Parent.Controls.Remove(butt);
                }
                buttonslist.Clear();
            }
            if (labellist.Count != 0)
            {
                foreach (Label labe in labellist)
                {
                    if (startbut == 2)
                    {
                        labe.MouseHover -= Mouse_Hover;
                        labe.MouseLeave -= Mouse_Leave;
                        labe.Click -= Button_Click;
                    }
                    labe.Parent.Controls.Remove(labe);
                }
                labellist.Clear();
            }
            if (shopWeapButlist.Count != 0)
            {
                foreach (PictureBox butt in shopWeapButlist)
                {
                    butt.Parent.Controls.Remove(butt);
                }
            }
            shopWeapButlist.Clear();
            if (shopUpgradeButlist.Count != 0)
            {
                foreach (PictureBox butt in shopUpgradeButlist)
                {
                    butt.Parent.Controls.Remove(butt);
                }
            }
            shopUpgradeButlist.Clear();
            if (shoplabel != null && shoplabel.Parent != null) {
                shoplabel.Parent.Controls.Remove(shoplabel);
            }
            if (labelbox != null && labelbox.Parent != null)
            {
                labelbox.Parent.Controls.Remove(labelbox);
            }
            if (barboxlist.Count != 0)
            {
                foreach (PictureBox barbox in barboxlist)
                {
                    barbox.Parent.Controls.Remove(barbox);
                }
                barboxlist.Clear();
            }
            if (barlabellist.Count != 0)
            {
                foreach (Label labe in barlabellist)
                {
                    if (labe.Parent != null) {
                        labe.Parent.Controls.Remove(labe);
                    }

                }
                barlabellist.Clear();
            }
            if (volbutlist.Count != 0) 
            {
                foreach (PictureBox volbox in volbutlist)
                {
                    volbox.Click -= Button_Click;
                    volbox.MouseHover -= Mouse_Hover;
                    volbox.MouseLeave -= Mouse_Leave;
                    volbox.Parent.Controls.Remove(volbox);
                }
                volbutlist.Clear();
            }
            if (menuimgs != null)
            {
                menuimgs.Clear();
            }
        }

        private void IsKeyDown(object sender, KeyEventArgs e) //upon keydown changing movement in a direction
        {
            if (gameOn == 2 &&  main == 0) {
                if (e.KeyCode == Keys.Q && playerskillscounters[0] == -1)
                {
                    Font font = new Font("Cooper Black", 12);
                    Label energyconlab = new Label();
                    if (Playerobj.consumecolides != null && Playerobj.consumecolides.Parent != null) {
                        Playerobj.consumecolides.Parent.Controls.Remove(Playerobj.consumecolides);
                    }
                    Playerobj.consumelabelcounter = 100;
                    Playerobj.consumecolides = energyconlab;
                    energyconlab.Font = font;
                    energyconlab.Location = new Point(920, 810);
                    energyconlab.BackColor = Color.Transparent;
                    energyconlab.ForeColor = Color.FromArgb(255, 43, 0);
                    energyconlab.AutoSize = true;
                    if (Playerobj.energy >= (Skillobj.playerSkills.ElementAt(0).Value[2] * Playerobj.energyCons))
                    {
                        playerskillscounters[0] = 0;
                        skillframecounters[0] = 0;
                        Playerobj.energy -= (Skillobj.playerSkills.ElementAt(0).Value[2] * Playerobj.energyCons);
                        gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                        energyconlab.Text = "-" + (Skillobj.playerSkills.ElementAt(0).Value[2] * Playerobj.energyCons) + " Energy";

                    } else {
                        energyconlab.Text = "Not Enough Energy"; ;
                    }
                    energyconlab.Parent = this.GetChildAtPoint(new Point(energyconlab.Location.X, energyconlab.Location.Y));
                }
                if (e.KeyCode == Keys.W && playerskillscounters[1] == -1)
                {
                    Font font = new Font("Cooper Black", 12);
                    Label energyconlab = new Label();
                    if (Playerobj.consumecolides != null && Playerobj.consumecolides.Parent != null)
                    {
                        Playerobj.consumecolides.Parent.Controls.Remove(Playerobj.consumecolides);
                    }
                    Playerobj.consumelabelcounter = 100;
                    Playerobj.consumecolides = energyconlab;
                    energyconlab.Font = font;
                    energyconlab.Location = new Point(920, 810);
                    energyconlab.BackColor = Color.Transparent;
                    energyconlab.ForeColor = Color.FromArgb(255, 43, 0);
                    energyconlab.AutoSize = true;
                    if (Playerobj.energy >= (Skillobj.playerSkills.ElementAt(1).Value[2] * Playerobj.energyCons))
                    {
                        playerskillscounters[1] = 0;
                        skillframecounters[1] = 0;
                        Playerobj.energy -= (Skillobj.playerSkills.ElementAt(1).Value[2] * Playerobj.energyCons);
                        gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                        energyconlab.Text = "-" + (Skillobj.playerSkills.ElementAt(1).Value[2] * Playerobj.energyCons) + " Energy";
                    }
                    else {
                        energyconlab.Text = "Not Enough Energy"; ;
                    }
                    energyconlab.Parent = this.GetChildAtPoint(new Point(energyconlab.Location.X, energyconlab.Location.Y));
                }
                if (e.KeyCode == Keys.E && playerskillscounters[2] == -1)
                {
                    Font font = new Font("Cooper Black", 12);
                    Label energyconlab = new Label();
                    if (Playerobj.consumecolides != null && Playerobj.consumecolides.Parent != null)
                    {
                        Playerobj.consumecolides.Parent.Controls.Remove(Playerobj.consumecolides);
                    }
                    Playerobj.consumelabelcounter = 100;
                    Playerobj.consumecolides = energyconlab;
                    energyconlab.Font = font;
                    energyconlab.Location = new Point(920, 810);
                    energyconlab.BackColor = Color.Transparent;
                    energyconlab.ForeColor = Color.FromArgb(255, 43, 0);
                    energyconlab.AutoSize = true;
                    if (Playerobj.energy >= (Skillobj.playerSkills.ElementAt(2).Value[2] * Playerobj.energyCons))
                    {
                        playerskillscounters[2] = 0;
                        skillframecounters[2] = 0;
                        Playerobj.energy -= (Skillobj.playerSkills.ElementAt(2).Value[2] * Playerobj.energyCons);
                        gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                        energyconlab.Text = "-" + (Skillobj.playerSkills.ElementAt(2).Value[2] * Playerobj.energyCons) + " Energy";
                    }
                    else {

                        energyconlab.Text = "Not Enough Energy";
                    }
                    energyconlab.Parent = this.GetChildAtPoint(new Point(energyconlab.Location.X, energyconlab.Location.Y));
                }
                if (e.KeyCode == Keys.D1 && Playerobj.pocketskill != -1)
                {
                    bool isDup = false;
                    int newskill = Playerobj.pocketskill;
                    foreach (int skill in Skillobj.playerSkills.Keys)
                    {
                        if (skill == newskill) {
                            isDup = true;
                        }
                    }
                    if (!isDup) {
                        int[] skilldata = Skillobj.playerskilldata[newskill];
                        if (skillframecounters[0] != -1)
                        {
                            Skillobj.RemoveSkillBuff(Skillobj.playerSkills.ElementAt(0).Key, imgs);
                        }
                        Dictionary<int, int[]> tempDic = new Dictionary<int, int[]>();
                        tempDic.Add(newskill, new int[] { skilldata[0], skilldata[1], skilldata[2] });
                        tempDic.Add(Skillobj.playerSkills.ElementAt(1).Key, Skillobj.playerSkills.ElementAt(1).Value);
                        tempDic.Add(Skillobj.playerSkills.ElementAt(2).Key, Skillobj.playerSkills.ElementAt(2).Value);
                        Skillobj.playerSkills.Clear();
                        Skillobj.playerSkills = new Dictionary<int, int[]>(tempDic);
                        tempDic.Clear();
                        Playerobj.pocketskill = -1;
                        skillinterfacebox.Image = null;
                        skillinterfacebox.BackColor = Color.FromArgb(123, 22, 255);
                        playerskillscounters[0] = -1;
                        skillframecounters[0] = -1;
                    }
                }
                if (e.KeyCode == Keys.D2 && Playerobj.pocketskill != -1)
                {
                    bool isDup = false;
                    int newskill = Playerobj.pocketskill;
                    foreach (int skill in Skillobj.playerSkills.Keys)
                    {
                        if (skill == newskill)
                        {
                            isDup = true;
                        }
                    }
                    if (!isDup)
                    {
                        int[] skilldata = Skillobj.playerskilldata[newskill];
                        if (skillframecounters[1] != -1) {
                            Skillobj.RemoveSkillBuff(Skillobj.playerSkills.ElementAt(1).Key, imgs);
                        }
                        Dictionary<int, int[]> tempDic = new Dictionary<int, int[]>();
                        tempDic.Add(Skillobj.playerSkills.ElementAt(0).Key, Skillobj.playerSkills.ElementAt(0).Value);
                        tempDic.Add(newskill, new int[] { skilldata[0], skilldata[1], skilldata[2] });
                        tempDic.Add(Skillobj.playerSkills.ElementAt(2).Key, Skillobj.playerSkills.ElementAt(2).Value);
                        Skillobj.playerSkills.Clear();
                        Skillobj.playerSkills = new Dictionary<int, int[]>(tempDic);
                        tempDic.Clear();
                        Playerobj.pocketskill = -1;
                        skillinterfacebox.Image = null;
                        skillinterfacebox.BackColor = Color.FromArgb(123, 22, 255);
                        playerskillscounters[1] = -1;
                        skillframecounters[1] = -1;
                    }
                }
                if (e.KeyCode == Keys.D3 && Playerobj.pocketskill != -1)
                {
                    bool isDup = false;
                    int newskill = Playerobj.pocketskill;
                    foreach (int skill in Skillobj.playerSkills.Keys)
                    {
                        if (skill == newskill)
                        {
                            isDup = true;
                        }
                    }
                    if (!isDup)
                    {
                        int[] skilldata = Skillobj.playerskilldata[newskill];
                        if (skillframecounters[2] != -1)
                        {
                            Skillobj.RemoveSkillBuff(Skillobj.playerSkills.ElementAt(2).Key, imgs);
                        }
                        Dictionary<int, int[]> tempDic = new Dictionary<int, int[]>();
                        tempDic.Add(Skillobj.playerSkills.ElementAt(0).Key, Skillobj.playerSkills.ElementAt(0).Value);
                        tempDic.Add(Skillobj.playerSkills.ElementAt(1).Key, Skillobj.playerSkills.ElementAt(1).Value);
                        tempDic.Add(newskill, new int[] { skilldata[0], skilldata[1], skilldata[2] });
                        Skillobj.playerSkills.Clear();
                        Skillobj.playerSkills = new Dictionary<int, int[]>(tempDic);
                        tempDic.Clear();
                        Playerobj.pocketskill = -1;
                        skillinterfacebox.Image = null;
                        skillinterfacebox.BackColor = Color.FromArgb(123, 22, 255);
                        playerskillscounters[2] = -1;
                        skillframecounters[2] = -1;
                    }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (level != 18)
                    {
                        direc[0] = 1;
                        if (Playerobj.playerSpeedDirc[0] < playerSpeedLimit)
                        {
                            Playerobj.playerSpeedDirc[0] += (1 * accelerate);
                        }
                    } else
                    {
                        direc[1] = 1;
                        if (Playerobj.playerSpeedDirc[1] < playerSpeedLimit)
                        {
                            Playerobj.playerSpeedDirc[1] += (1 * accelerate);
                        }
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (level != 18)
                    {
                        direc[1] = 1;
                        if (Playerobj.playerSpeedDirc[1] < playerSpeedLimit)
                        {
                            Playerobj.playerSpeedDirc[1] += (1 * accelerate);
                        }
                    }
                    else {
                        direc[0] = 1;
                        if (Playerobj.playerSpeedDirc[0] < playerSpeedLimit)
                        {
                            Playerobj.playerSpeedDirc[0] += (1 * accelerate);
                        }
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    if (level != 18)
                    {
                        direc[2] = 1;
                        if (Playerobj.playerSpeedDirc[2] < playerSpeedLimit)
                        {
                            Playerobj.playerSpeedDirc[2] += (1 * accelerate);
                        }
                    }
                    else {
                        direc[3] = 1;
                        if (Playerobj.playerSpeedDirc[3] < playerSpeedLimit)
                        {
                            Playerobj.playerSpeedDirc[3] += (1 * accelerate);
                        }
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    if (level != 18)
                    {
                        direc[3] = 1;
                        if (Playerobj.playerSpeedDirc[3] < playerSpeedLimit)
                        {
                            Playerobj.playerSpeedDirc[3] += (1 * accelerate);
                        }
                    }
                    else {
                        direc[2] = 1;
                        if (Playerobj.playerSpeedDirc[2] < playerSpeedLimit)
                        {
                            Playerobj.playerSpeedDirc[2] += (1 * accelerate);
                        }
                    }
                }
                if (e.KeyCode == Keys.Space)          //creating bullet when spacebar is pressed
                {
                    keylockSpace = false;                //timing constant fire
                }
            }
            if (e.KeyCode == Keys.Escape && !outro)
            {
                if (main == 0)
                {
                    main = 1;
                    gameboxlist[8].Image = imgs.menuimgs[1];
                }
                else if (main == 2)
                {
                    if (controlbut == 2) // if on menu draw settings back
                    {
                        controlbut = 0;
                        DrawSettingsimg();
                    }
                    else   // remove menu
                    {
                        resumebut = 1;
                    }
                }
                if (mainmenu != 0) {
                    if (controlbut == 2) // if on menu draw settings back
                    {
                        controlbut = 0;
                        DrawSettingsimg();

                    }
                    else if (settingsbut == 2)
                    {
                        resumebut = 1;
                    }
                    else if (shopbut == 2)
                    {
                        shopbut = 0;
                        RemoveMenuButtons();// remove buttons
                        WriteToFile();
                        mainmenu = 0;
                    }
                    else if (startbut == 2)
                    {
                        RemoveMenuButtons();
                        startbut = 0;
                        mainmenu = 0;
                    }
                }
            }
        }

        private void IsKeyUp(object sender, KeyEventArgs e) //upon keyup reseting movement in a direction
        {
            if (e.KeyCode == Keys.Up)
            {
                if (level != 18)
                {
                    direc[0] = 0;
                    Playerobj.playerSpeedDirc[0] = 5;
                } else
                {
                    direc[1] = 0;
                    Playerobj.playerSpeedDirc[1] = 5;
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (level != 18)
                {
                    direc[1] = 0;
                    Playerobj.playerSpeedDirc[1] = 5;
                }
                else
                {
                    direc[0] = 0;
                    Playerobj.playerSpeedDirc[0] = 5;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (level != 18)
                {
                    direc[2] = 0;
                    Playerobj.playerSpeedDirc[2] = 5;
                }
                else
                {
                    direc[3] = 0;
                    Playerobj.playerSpeedDirc[3] = 5;
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (level != 18)
                {
                    direc[3] = 0;
                    Playerobj.playerSpeedDirc[3] = 5;
                }
                else {
                    direc[2] = 0;
                    Playerobj.playerSpeedDirc[2] = 5;
                }
            }
            if (e.KeyCode == Keys.Space)          //cancel bullet fire on spacebar key release
            {
                keylockSpace = true;
            }

        }

        private void TimerEvent(int tick, TimeSpan span)
        {
            if (gameOn == 0)
            {
                if (isgamemusic)
                {
                    isgamemusic = false;
                    m_mediaPlayer1.Close();
                }
                if (musicounter % 9600 == 0) {
                    m_mediaPlayer1.Open(new Uri(@"C:\Users\\source\repos\spacebattle\spacebattle\sounds\gamebegin.wav"));
                    m_mediaPlayer1.Volume = mVolume / 100.0f;
                    m_mediaPlayer1.Play();
                }
                if (musicounter % 910000 == 0) {
                    musicounter = 0;
                }
                if (mainmenu == 0)
                {
                    DrawMainMenuimg();
                    mainmenu = 1;
                }
                else if (mainmenu == 1)
                {
                    if (settingsbut == 1)
                    {
                        settingsbut = 2;
                        DrawSettingsimg();
                    }
                    if (controlbut == 1)
                    {
                        controlbut = 2;
                        DrawControlsimg();
                    }
                    if (resumebut == 1)
                    {
                        settingsbut = 0;
                        resumebut = 0;
                        RemoveMenuButtons();
                        DrawMainMenuimg();
                    }
                    if (shopbut == 1)
                    {
                        shopbut = 2;
                        DrawShopimg();
                    }
                    if (startbut == 1)
                    {
                        startbut = 2;
                        DrawPreGameMenuimg();
                        shopbut = 0;
                        controlbut = 0;
                        settingsbut = 0;
                        resumebut = 0;
                    }
                }
                musicounter += 1;
            }
            else if (gameOn == 1)
            { //form game size = 1652, 900
                RemoveMenuButtons();
                this.Width = 1652;
                this.Height = 900;
                double scalex = 0, scaley = 0;
                if (fullscreen == 1) {
                    if (width > mainbox.Width)
                    {
                        scalex = (width - mainbox.Width - 185) / 2;
                    }
                    if (height > mainbox.Height)
                    {
                        scaley = (height - mainbox.Height) / 2;
                    }
                    mainbox.Location = new Point((int)scalex, (int)scaley);
                }
                DrawGameInterface((int)scalex, (int)scaley);

                Miscobj.ResetMiscObj();
                Bulletobj.ResetBulletsObj();
                Dropobj.ResetDropObj();
                Playerobj.ResetPlayerObj();
                Enemyobj.ResetEnemyObj();
                Planetobj.ResetPlanetObj();
                Skillobj.ResetSkillobj();

                enemySpawnCount = 0; //reset game parameters 
                enemySpawnInterval = 40;
                Distance = 0;
                duration = 30000; 
                bulletCounter = 0;
                bossSpawnedState = 0;
                holecounter = 0;
                outro = false;
                playerSpeedLimit = 10;
                Playerobj.playerImg = imgs.playerimg[0];

                dropcounter = 0;
                miscounter = 0;
                bulletcounter = 0;
                enemybulletcounter = 0;
                enemybossbulletcounter = 0;
                enemycounter = 0;
                planetcounter = 0;
                musicounter = 0;
                playerskillscounters = new int[] { -1, -1, -1 };
                skillframecounters = new int[] { -1, -1, -1 };
                main = 0;
                mobindx = 0;
                specialConLose = false;
                specialConWin = false;
                int startindx = Miscobj.miscimgsindxs[(level-1) / 3][0];
                CreatemiscObj(5, 0 + startindx); // creating misc objects at the start
                CreatemiscObj(10, 1 + startindx);
                for (int i = (2 + startindx); i <= Miscobj.miscimgsindxs[(level - 1) / 3][1]; i++)
                {
                    CreatemiscObj(5, i);
                }
                CreatEnemyObj(3, 10, 700, 0, false);
                int plantnum = random.Next(Planetobj.planetimgsindxs[(level - 1) / 3][0], Planetobj.planetimgsindxs[(level - 1) / 3][1] + 1);
                int yplace = random.Next(0, 300);
                CreateplanetObj(plantnum, -200, yplace);      // spawns planets on different intervals
                Miscobj.LoadMiscImgs(level);
                Planetobj.LoadPlanetImgs(level);
                Skillobj.LoadPlayerSkillImgs(level);
                Enemyobj.LoadEnemyImgs(level);
                gameOn = 2;
                if (menuimgs != null)
                {
                    menuimgs.Clear();
                }
                if (bulletimgs != null)
                {
                    bulletimgs.Clear();
                }
                bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgun.png") ,       //defgun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgunflip45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgunflip-45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungun.png") ,       //bounce gun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungunflip45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungunflip-45.png")
                };
            }
            else
            {
                if (main == 0) // if esc or menu button is not pressed
                {
                    if (Distance % 5900 == 0) {
                        isgamemusic = true;
                        m_mediaPlayer1.Open(new Uri(@"C:\Users\\source\repos\spacebattle\spacebattle\sounds\neongaming.wav"));
                        m_mediaPlayer1.Volume = mVolume / 100.0f;
                        m_mediaPlayer1.Play();
                    }
                    Boolean flag;
                    DrawGameimg();
                    Distance += 1;
                    gamelabellist[8].Text = Playerobj.coins.ToString();
                    CollusionDetection();
                    Playerobj.MovePlayer(direc);
                    CreateBullet();
                    if (Bulletobj.bulletcords.Count != 0)
                    {
                        if (Bulletobj.guntype == 3 || Bulletobj.guntype == 0)
                        {
                            Bulletobj.MoveBullet();  //move bullets  
                        }
                        else if (Bulletobj.guntype == 1 || Bulletobj.guntype == 2)
                        {
                            Bulletobj.MovePulse();
                        }
                        else if (Bulletobj.guntype == 4)
                        {
                            Bulletobj.MoveCanonBall(true);
                        }
                        else if (Bulletobj.guntype == 5)
                        {
                            Bulletobj.MoveMissile();
                        }
                        else if (Bulletobj.guntype == 6)
                        {
                            Bulletobj.MoveGuidedMissile(Enemyobj.enemycords, bulletimgs[0].Size, Enemyobj.enemystats, imgs);
                        }
                        else if (Bulletobj.guntype == 7)
                        {
                            Bulletobj.MoveParticle();
                        }
                        else if (Bulletobj.guntype == 8)
                        {
                            Bulletobj.MoveDeathDrone();
                        }
                        else if (Bulletobj.guntype == 9)
                        {
                            if (Distance % 100 == 0)
                            {  // replicate every 100 intervals
                                Dictionary<int, int[]> repbullets = new Dictionary<int, int[]>(Bulletobj.bulletcords);
                                foreach (int bulletkey in repbullets.Keys)
                                {
                                    Bulletobj.CreateDeathDrone(repbullets[bulletkey][0], repbullets[bulletkey][1], bulletcounter, 0, 3, random);
                                    bulletcounter += 3;
                                    Bulletobj.bulletcords.Remove(bulletkey);
                                    Bulletobj.bulletypes.Remove(bulletkey);
                                }
                            }
                            Bulletobj.MoveDeathDrone();
                        }
                        else if (Bulletobj.guntype == 10)
                        {
                            Bulletobj.MoveCanonBall(false);
                        }
                    }
                    Bulletobj.MoveSkillBullet();
                    if (!Skillobj.timestop) {
                        ModifyMiscObj();           //misc movement typeofobj = 0 : flying dust, typeofobj = 1 : star
                        ModifyEnemyObj();
                        ModifyPlanetObj();
                        ModifyDropObj();
                        Bulletobj.MoveEnemyBossBullet();
                        Bulletobj.MoveEnemyBullet();

                        if (Distance % 1500 == 0)
                        {
                            int plantnum = random.Next(Planetobj.planetimgsindxs[(level-1) / 3][0], Planetobj.planetimgsindxs[(level - 1) / 3][1] + 1);
                            int yplace = random.Next(0, 300);
                            CreateplanetObj(plantnum, Planetobj.planetimgs[plantnum].Width, yplace);      // spawns planets on different intervals
                        }
                        if (Distance % 30 == 0)
                        {
                            Skillobj.UpdateEnemySkillCount(mainbox.Width, imgs, level);
                        }
                        if (Distance % 20 == 0)
                        {            //change enemy bullet/skill img
                            Bulletobj.ChangeBulletsFrame(Bulletobj.enemybullet);
                            Bulletobj.ChangeBulletsFrame(Bulletobj.enemybossbullet);
                            Bulletobj.ChangeBulletsFrame(Bulletobj.skillbullet);
                            Skillobj.ChangeSkillImg();
                        }
                        if (Distance % 5 == 0)
                        {
                            enemyflick += 1;
                            foreach (int ekey in Enemyobj.enemystats.Keys)
                            {
                                if (enemyflick == 1)
                                {
                                    Enemyobj.enemycords[ekey][1] += 1;
                                }
                                else if (enemyflick == 2)
                                {
                                    Enemyobj.enemycords[ekey][1] -= 1;

                                }
                                else if (enemyflick == 3)
                                {
                                    Enemyobj.enemycords[ekey][1] -= 1;
                                }
                                else
                                {
                                    Enemyobj.enemycords[ekey][1] += 1;
                                    enemyflick = 0;
                                }
                            }
                        }
                    }
                    if (Distance % 50 == 0)
                    {
                        flag = true;
                        if (Playerobj.energy < 100)
                        {
                            Playerobj.energy += 1;
                            gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy))); ;
                        }
                        for (int m = 0; m < Playerobj.playerstats.Length; m++) { // change player frame
                            if (Playerobj.playerstats[m] == 1) {
                                Playerobj.playerstats[m] += 1;
                                Playerobj.playerImg = imgs.playerimg[m + 6];
                                flag = false;
                                break;
                            } if (Playerobj.playerstats[m] == 2) {
                                Playerobj.playerstats[m] -= 1;
                                Playerobj.playerImg = imgs.playerimg[m + 1]; 
                                flag = false;
                                break;
                            }
                        }
                        if (flag) {
                            if (Playerobj.playerImg == imgs.playerimg[0]) {
                                Playerobj.playerImg = imgs.playerimg[5];
                            } else {
                                Playerobj.playerImg = imgs.playerimg[0];
                            }
                        }
                    }
                    ResetCounters();
                    if (Distance < duration && !Skillobj.timestop)
                    { // spawn enemy untill {duration} intervals
                        if (enemySpawnCount == enemySpawnInterval)
                        {
                            CreatEnemyObj(1, 1, 1, mobindx, false);
                            enemySpawnCount = 0;
                            if (Distance > (duration / 3))
                            {
                                CreatEnemyObj(1, 1, 1, mobindx + 1, false);
                            }
                            if (Distance > ((duration * 1) / 5) && mobindx == 0)
                            {
                                mobindx += 1;
                            }
                            if (Distance > ((duration * 2) / 3))
                            {
                                CreatEnemyObj(1, 1, 1, mobindx + 2 , false);
                            }
                            if (Distance > ((duration * 3) / 5) && mobindx == 1)
                            {
                                mobindx += 1;
                            }
                        }
                        if (Distance % (300 - ((level - 1) * 5)) == 0)
                        {                //Create enemy-non boss bullets
                            int enemytype;
                            foreach (int enemykey in Enemyobj.enemystats.Keys)
                            {
                                enemytype = Enemyobj.enemystats[enemykey][1];
                                if (enemytype < 1000 && Enemyobj.enemystats[enemykey][8] != -1)
                                { //only enemies that set to fire can create bullet
                                    enemybulletcounter += 1;
                                    if (level == 16)
                                    {
                                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2), 0, enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 5);
                                    } else if (level == 17) {
                                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2), 0, enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 10);
                                    }
                                    else
                                    {
                                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2), 0, enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 10 + (level * 2));
                                    }
                                }
                            }
                        }
                    }
                    else if (Distance == duration)
                    {
                        CreatEnemyObj(1, 10, 200, level + 999, true);

                    }
                    else if (Distance > duration && Enemyobj.enemycords.Count != 0 && !Skillobj.timestop)
                    {
                        if (Distance % 100 == 0) // Create boss bullets 
                        {
                            CreateBossBullet();
                        }
                    }
                    if (Distance % (duration / 100) == 0)
                    {                               //every 110 distace units move pointer by one pixel
                        gameboxlist[6].Location = new Point(gameboxlist[6].Location.X + 1, gameboxlist[6].Location.Y);
                    }
                    if (Distance % (1000 / Playerobj.fuelCons) == 0)       //every 1100 distace units decrease fuel amount
                    {
                        Playerobj.fuel -= 1;
                        gameboxlist[3].Width = Playerobj.fuel;
                    }
                    if (Playerobj.labelcounter != 0)
                    {        //label that Created has a countdown until it removed
                        Playerobj.labelcounter -= 1;
                    }
                    else {
                        if (Playerobj.colides != null && Playerobj.colides.Parent != null) {
                            Playerobj.colides.Parent.Controls.Remove(Playerobj.colides);
                        }
                        Playerobj.labelcounter = 0;
                    }
                    if (Playerobj.skilllabelcounter != 0)
                    {        //label that Created has a countdown until it removed
                        Playerobj.skilllabelcounter -= 1;
                    }
                    else
                    {
                        if (Playerobj.skillcolides != null && Playerobj.skillcolides.Parent != null)
                        {
                            Playerobj.skillcolides.Parent.Controls.Remove(Playerobj.skillcolides);
                        }
                        Playerobj.skilllabelcounter = 0;
                    }
                    if (Playerobj.consumelabelcounter != 0)
                    {        //label that Created has a countdown until it removed
                        Playerobj.consumelabelcounter -= 1;
                    }
                    else
                    {
                        if (Playerobj.consumecolides != null && Playerobj.consumecolides.Parent != null)
                        {
                            Playerobj.consumecolides.Parent.Controls.Remove(Playerobj.consumecolides);
                        }
                        Playerobj.consumelabelcounter = 0;
                    }
                    enemySpawnCount += 1;
                    bulletCounter += 1;
                    if (Playerobj.dmgcdcounter != -1)
                    {
                        Playerobj.dmgcdcounter += 1;
                        if (Playerobj.dmgcdcounter > 100)
                        {
                            Playerobj.dmgcdcounter = -1;
                            if (Playerobj.playerstats[0] > 0)
                            {
                                Playerobj.playerImg = imgs.playerimg[0];
                                Playerobj.playerstats[0] = 0;
                            }
                        }
                    }
                    if (bossSpawnedState == 2 && Distance % 50 == 0)
                    {
                        if (labelbox.Image == imgs.menuimgs[3])
                        {
                            labelbox.Image = imgs.menuimgs[4];
                        }
                        else
                        {
                            labelbox.Image = imgs.menuimgs[3];
                        }
                    }
                    for (int n = 0; n < playerskillscounters.Length; n++) // check which player skills used
                    {
                        if (playerskillscounters[n] > -1)
                        {
                            if (playerskillscounters[n] % 100 == 0)
                            {
                                Skillobj.UpdatePlayerSkillCount(n, imgs);
                            }
                            playerskillscounters[n] += 1;
                        }
                        if (skillframecounters[n] > -1)
                        {
                            if (playerskillscounters[n] % 15 == 0)
                            {
                                if (skillframecounters[n] < 2)
                                {
                                    skillframecounters[n] += 1;
                                }
                                else
                                {
                                    skillframecounters[n] = 0;
                                }
                            }
                        }
                    }
                    if (holecounter == 100)
                    {
                        RemoveGameInterface();
                        gameOn = 0;
                        controlbut = 0;
                        resumebut = 0;
                        settingsbut = 0;
                        holecounter = 0;
                        outro = false;
                    }
                    else
                    {
                        if (((Playerobj.fuel <= 0 || Playerobj.HP <= 0) && !outro && level != 16) || specialConLose)
                        {
                            outro = true;
                            specialConLose = false;
                            outrolabel = new Label
                            {
                                Font = new Font("Cooper Black", 30),
                                ForeColor = Color.FromArgb(75, 0, 130),
                                BackColor = Color.Transparent,
                                AutoSize = true,
                                Text = "Mission Failed!",
                                Location = new Point((mainbox.Width / 2) - 200, (mainbox.Height / 2) - 50)
                            };
                            outrolabel.Parent = mainbox;
                            outrolabel.BringToFront();

                        } else if (Playerobj.HP <= 0 && !outro && level == 16) {
                            bossSpawnedState = 2;
                            specialConWin = true;
                        }
                    }
                    if (spawnanotherboss)
                    {
                        int bosskey = Enemyobj.GetBossKey();
                        CreatEnemyObj(1, Enemyobj.enemycords[bosskey][0] + (Enemyobj.enemyimgs[Enemyobj.enemystats[bosskey][1]][Enemyobj.enemystats[bosskey][0]].Width / 2), Enemyobj.enemycords[bosskey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[bosskey][1]][Enemyobj.enemystats[bosskey][0]].Height / 2), 1003, true);
                        spawnanotherboss = false;
                    }
                }
                else
                {         // if esc or menu button is pressed drawing setting and buttons
                    if (main == 1)
                    {
                        DrawSettingsimg();
                        if (shoplabel != null && shoplabel.Parent != null)
                        {
                            shoplabel.Parent.Controls.Remove(shoplabel);
                        }
                        if (labelbox != null && labelbox.Parent != null)
                        {
                            labelbox.Parent.Controls.Remove(labelbox);
                        }
                        main = 2;
                    }
                    if (controlbut == 1) // drawing controls
                    {
                        DrawControlsimg();
                        controlbut = 2;
                    }
                    if (resumebut == 1) // removing settings
                    {
                        RemoveMenuButtons();
                        main = 0;
                        controlbut = 0;
                        resumebut = 0;
                        if (bossSpawnedState == 1)
                        {
                            PictureBox bossHPbox = new PictureBox();
                            labelbox = bossHPbox;
                            int enemykey = Enemyobj.GetBossKey();
                            if (Enemyobj.enemystats[enemykey][1] == 1003)
                            {
                                labelbox.BackColor = Color.FromArgb(255, 0, 0);
                            }
                            else
                            {
                                labelbox.BackColor = Color.FromArgb(14, 209, 69);
                            }
                            labelbox.SizeMode = PictureBoxSizeMode.AutoSize;
                            double bossHPratio = (double)1300 / Enemyobj.enemydata[Enemyobj.enemystats[enemykey][1]][1];
                            labelbox.Size = new Size((int)(bossHPratio * (double)Enemyobj.enemystats[enemykey][3]), 30);
                            labelbox.Location = new Point(140, 10);
                            labelbox.Parent = mainbox;
                            labelbox.BringToFront();
                            Label desclabal = new Label();
                            shoplabel = desclabal;
                            shoplabel.Font = new Font("Cooper Black", 20);
                            shoplabel.ForeColor = Color.FromArgb(255, 255, 255);
                            shoplabel.BackColor = Color.Transparent;
                            shoplabel.AutoSize = true;
                            shoplabel.Text = "HP:";
                            shoplabel.Location = new Point(45, 10);
                            shoplabel.Parent = mainbox;
                        }

                    }
                }
            }
        }

        private void CreatemiscObj(int amount, int objtype)
        {
            int i = 0;
            int startSpeed;
            if (objtype % 8 == 0)
            {
                startSpeed = 15;
            }
            else
            {
                startSpeed = 1;
            }
            while (i < amount)
            {
                miscounter += 1;
                Miscobj.CreateMisc(random.Next(1, 1400), random.Next(1, 850), startSpeed, objtype, miscounter);
                i += 1;
            }
        }

        private void ModifyMiscObj()
        {
            int startSpeed;
            int objtype;
            int renderMisc;
            if (Miscobj.miscords.Count != 0)
            {
                renderMisc = Miscobj.MoveMisc(level);
                if (renderMisc != -1)
                {
                    objtype = renderMisc;
                    if (objtype % 8 == 0)
                    {
                        startSpeed = 15;
                    }
                    else
                    {
                        startSpeed = 1;
                    }
                    miscounter += 1;
                    if (level > 6 && level < 16 && (objtype != 16 && objtype != 24 && objtype != 32))
                    {
                        Miscobj.CreateMisc(random.Next(1, 1600), 810, startSpeed, objtype, miscounter);    //creating stardust/star each time one is removed
                    }
                    else {
                        Miscobj.CreateMisc(1, random.Next(50, 870), startSpeed, objtype, miscounter);    //creating stardust/star each time one is removed
                    }
                }
            }
        }

        private void CreateplanetObj(int objtype, int xcord, int ycord)
        {
            planetcounter += 1;
            Planetobj.Createplanet(-xcord, ycord, objtype, planetcounter);
        }

        private void ModifyPlanetObj()
        {
            if (Planetobj.planetcords.Count != 0)
            {
                Planetobj.Moveplanet();
            }
        }

        private void CreatEnemyObj(int amount, int xStart, int xEnd, int offset, Boolean boss)
        {
            int i = 0;
            int xcord;

            while (i < amount)
            {
                enemycounter += 1;
                if (!boss) {
                    xcord = 10;
                    switch (level)
                    { // set starting cords for non-boss enemy
                        case 1:
                            Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 0 + offset, new int[] { 1, 0 });
                            break;
                        case 2:
                            Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 5 + offset, new int[] { 1, 0 });
                            break;
                        case 3:
                            Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 10 + offset, new int[] { 1, 0 });
                            break;
                        case 4:
                            if (15 + offset == 19)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 19, new int[] { 0, 1 });
                            }
                            else {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 15 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 5:
                            if (20 + offset == 24)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 24, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 20 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 6:
                            if (25 + offset == 29)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 29, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 25 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 7:
                            Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 30 + offset, new int[] { 1, 0 });
                            break;
                        case 8:
                            if (36 + offset == 40)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 40, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 36 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 9:
                            if (41 + offset == 45)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 45, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 41 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 10:
                            if (46 + offset == 50)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 50, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 46 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 11:
                            if (51 + offset == 55)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 55, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 51 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 12:
                            if (57 + offset == 61)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 61, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 57 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 13:
                            if (62 + offset == 66)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 66, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 62 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 14:
                            if (67 + offset == 71)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 71, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 67 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 15:
                            if (73 + offset == 77)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 77, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 73 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 16:
                            if (78 + offset == 82)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 82, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 78 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 17:
                            if (83 + offset == 87)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 87, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 83 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 18:
                            Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 90 + offset, new int[] { 1, 0 });
                            break;
                        case 19:
                            if (95 + offset == 99)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 99, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 95 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 20:
                            if (100 + offset == 104)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 104, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 100 + offset, new int[] { 1, 0 });
                            }
                            break;
                        case 21:
                            if (106 + offset == 110)
                            {
                                Enemyobj.CreateEnemy(random.Next(10, 1550), 10, enemycounter, 110, new int[] { 0, 1 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(xcord, random.Next(50, 730), enemycounter, 106 + offset, new int[] { 1, 0 });
                            }
                            break;
                    }
                }
                if (boss)
                {
                    if (level > 3) {
                        offset += 1;
                    }
                    bossSpawnedState = 1;
                    Enemyobj.CreateEnemy(xStart, xEnd, enemycounter, offset, new int[] { });
                    PictureBox bossHP = new PictureBox();
                    labelbox = bossHP;
                    if (offset == 1003)
                    {
                        labelbox.BackColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        labelbox.BackColor = Color.FromArgb(14, 209, 69);
                    }
                    labelbox.SizeMode = PictureBoxSizeMode.AutoSize;
                    labelbox.Size = new Size(1300, 30);
                    labelbox.Location = new Point(140, 10);
                    labelbox.Parent = mainbox;
                    labelbox.BringToFront();
                    Label desclabal = new Label();
                    shoplabel = desclabal;
                    shoplabel.Font = new Font("Cooper Black", 20);
                    shoplabel.ForeColor = Color.FromArgb(255, 255, 255);
                    shoplabel.BackColor = Color.Transparent;
                    shoplabel.AutoSize = true;
                    shoplabel.Text = "HP:";
                    shoplabel.Location = new Point(45, 10);
                    shoplabel.Parent = mainbox;

                }
                i += 1;
            }
        }

        private void ModifyEnemyObj()
        {
            if (Enemyobj.enemycords.Count != 0)
            {
                Enemyobj.MoveEnemy(mainbox.Width, mainbox.Height, level, imgs);
            }
        }

        private void CreatDropObj(int[] cords)
        {
            dropcounter += 1;
            int droptype = Dropobj.CalcDrop(random);
            Dropobj.CreateDrop(cords[0], cords[1], droptype, dropcounter);
        }

        private void ModifyDropObj()
        {
            if (Dropobj.dropcords.Count != 0)
            {
                Dropobj.MoveDrop();
            }
        }

        private Boolean ColideRects(int[] startCords, Size size, int[] startCords1, Size size1)
        {       // checking if two rectangles intersect 
            Rectangle rect1 = new Rectangle(startCords[0], startCords[1], size.Width, size.Height);
            Rectangle rect2 = new Rectangle(startCords1[0], startCords1[1], size1.Width, size1.Height);
            if (rect1.IntersectsWith(rect2))
            {
                return true;
            }
            return false;
        }

        private void CollusionDetection()
        {
            List<int> keysRemove = new List<int>();
            double damage, critDamage;
            Boolean isCrit;
            int result;
            double bossHPratio;
            Size enemysize;
            foreach (int enemykey in Enemyobj.enemycords.Keys)
            {
                if (Enemyobj.enemystats[enemykey][0] != 3)                  //ignore dead enemies on screen
                {
                    foreach (int bulletkey in Bulletobj.bulletcords.Keys)
                    {
                        isCrit = false;
                        damage = Bulletobj.bulletDamage * Playerobj.dmginc;
                        if (Bulletobj.guntype == 7 && Bulletobj.bulletypes[bulletkey] > 10) // devide damage by 2 if a particle
                        {
                            damage /= 2;
                        } else if (Bulletobj.guntype == 10 && Bulletobj.bulletypes[bulletkey] < 10) {// devide damage by 5 if a proton
                            damage /= 5;
                        }
                        result = random.Next(0, 10);                              // calc chance to crit
                        if (result >= 0 && result < (Playerobj.critChance / 10) && (Playerobj.critChance / 10) > 0 && Playerobj.critDamage > 100) {
                            critDamage = (Playerobj.critDamage - 100) / 100;        // add % damage to bullet damage
                            damage *= (1.0 + critDamage);
                            isCrit = true;
                        }
                        enemysize = Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Size;
                                                                                         // collusion Detection between player bullets and enemies
                        if (ColideRects(Enemyobj.enemycords[enemykey], enemysize, Bulletobj.bulletcords[bulletkey], bulletimgs[GetBulletIndx(bulletkey)].Size))    
                        {
                            if (Enemyobj.enemystats[enemykey][7] == 0)
                            {
                                Enemyobj.enemystats[enemykey][3] -= ((int)damage - Enemyobj.enemystats[enemykey][6]); //damage to enemy
                                if (Playerobj.skillvamp != 0)                           //spell vamp
                                {                
                                    Playerobj.HP += (int)(damage * Playerobj.skillvamp);
                                    if (Playerobj.HP > Playerobj.maxHP)
                                    {
                                        Playerobj.HP = Playerobj.maxHP;
                                    }
                                    gameboxlist[4].Width = (Playerobj.HP / playerHPRatio);
                                    gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                                }
                                if (Enemyobj.enemystats[enemykey][1] >= 1000)
                                {
                                    bossHPratio = (double)1300 / Enemyobj.enemydata[Enemyobj.enemystats[enemykey][1]][1];
                                    labelbox.Width = (int)(bossHPratio * (double)Enemyobj.enemystats[enemykey][3]);
                                }
                            }
                            if (Enemyobj.enemystats[enemykey][1] >= 1000)
                            {
                                bossHPratio = (double)1300 / Enemyobj.enemydata[Enemyobj.enemystats[enemykey][1]][1];
                                labelbox.Width = (int)(bossHPratio * (double)Enemyobj.enemystats[enemykey][3]);
                            }
                            if (Bulletobj.guntype != 4)
                            {
                                keysRemove.Add(bulletkey);
                            }
                            else {
                                Bulletobj.bulletbounces[bulletkey] -= 1;
                                if (Bulletobj.bulletbounces[bulletkey] == 0) {
                                    keysRemove.Add(bulletkey);
                                }
                            }
                            if (Enemyobj.enemystats[enemykey][3] <= 0)            //choose img for enemy based on condition
                            {
                                Enemyobj.SetEnemyFrame(3, enemykey);
                                if (Enemyobj.enemystats[enemykey][1] != 35) {
                                    CreatDropObj(Enemyobj.enemycords[enemykey]);
                                }
                                if (Enemyobj.enemystats[enemykey][1] >= 1000) {
                                    shoplabel.Parent.Controls.Remove(shoplabel);
                                    labelbox.Parent.Controls.Remove(labelbox);
                                    if (Enemyobj.enemystats[enemykey][1] == 1002)
                                    {
                                        spawnanotherboss = true;
                                    }
                                    else if (level != 16)
                                    {
                                        PictureBox portalbox = new PictureBox();
                                        labelbox = portalbox;
                                        labelbox.BackColor = Color.Transparent;
                                        labelbox.Image = imgs.menuimgs[3];
                                        labelbox.Size = new Size(100, 200);
                                        labelbox.Location = new Point(Enemyobj.enemycords[enemykey][0], Enemyobj.enemycords[enemykey][1]);
                                        labelbox.Parent = mainbox;
                                        labelbox.BringToFront();
                                        bossSpawnedState = 2;
                                    }
                                    else {
                                        specialConLose = true;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                if (isCrit)               // set enemy crit damaged img
                                {
                                    Enemyobj.SetEnemyFrame(2, enemykey);
                                }
                                else                     // set enemy damaged img
                                {
                                    Enemyobj.SetEnemyFrame(1, enemykey);
                                }
                            }
                        }

                    }
                    if (keysRemove.Count != 0)
                    {
                        foreach (int key in keysRemove)
                        {
                            if (Bulletobj.guntype == 7 && Bulletobj.bulletypes[key] < 10)  // add particle before removing bullet
                            {
                                bulletcounter = Bulletobj.CreateParticle(Bulletobj.bulletcords[key][0], Bulletobj.bulletcords[key][1], bulletcounter, 0, false);
                            } else if (Bulletobj.guntype == 10 && Bulletobj.bulletypes[key] < 10) {
                                Bulletobj.CreateDeathDrone(Bulletobj.bulletcords[key][0], Bulletobj.bulletcords[key][1], bulletcounter, 10, 5, random);// proton particle type
                                bulletcounter += 5;
                            }
                            Bulletobj.bulletcords.Remove(key);
                            Bulletobj.bulletypes.Remove(key);
                            if (Bulletobj.bulletbounces.Count != 0) {
                                Bulletobj.bulletbounces.Remove(key);
                            }
                        }
                        keysRemove.Clear();
                    }

                    enemysize = Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Size;
                    foreach (int skillbulletkey in Bulletobj.skillbulletcords.Keys)
                    {                                                                               // colusion detection between skill bullet and enemy
                        if (ColideRects(Enemyobj.enemycords[enemykey], enemysize, Bulletobj.skillbulletcords[skillbulletkey], Bulletobj.playerskillbulletimgs[Bulletobj.skillbullet[skillbulletkey][5]][Bulletobj.skillbullet[skillbulletkey][3]].Size)) {
                            isCrit = false;
                            damage = Bulletobj.skillbullet[skillbulletkey][2] * Playerobj.dmginc;
                            result = random.Next(0, 10);                              // calc chance to crit
                            if (result >= 0 && result < (Playerobj.critChance / 10) && (Playerobj.critChance / 10) > 0 && Playerobj.critDamage > 100)
                            {
                                critDamage = (Playerobj.critDamage - 100) / 100;        // add % damage to bullet damage
                                damage *= (1.0 + critDamage);
                                isCrit = true;
                            }
                            Bulletobj.skillbullet[skillbulletkey][4] -= Enemyobj.enemystats[enemykey][5];
                            if (Bulletobj.skillbullet[skillbulletkey][4] <= 0) {
                                keysRemove.Add(skillbulletkey);
                            }
                            if (Enemyobj.enemystats[enemykey][7] == 0)
                            {
                                Enemyobj.enemystats[enemykey][3] -= ((int)damage - Enemyobj.enemystats[enemykey][6]); //damage to enemy
                                if (Playerobj.skillvamp != 0) {                //spell vamp
                                    Playerobj.HP += (int)(damage * Playerobj.skillvamp);
                                    if (Playerobj.HP > Playerobj.maxHP)
                                    {
                                        Playerobj.HP = Playerobj.maxHP;
                                    }
                                    gameboxlist[4].Width = (Playerobj.HP / playerHPRatio);
                                    gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                                }
                                if (Enemyobj.enemystats[enemykey][1] >= 1000)
                                {
                                    bossHPratio = (double)1300 / Enemyobj.enemydata[Enemyobj.enemystats[enemykey][1]][1];
                                    labelbox.Width = (int)(bossHPratio * (double)Enemyobj.enemystats[enemykey][3]);
                                }
                            }
                            if (Enemyobj.enemystats[enemykey][3] <= 0)            //choose img for enemy based on condition
                            {
                                Enemyobj.SetEnemyFrame(3, enemykey);
                                CreatDropObj(Enemyobj.enemycords[enemykey]);
                                if (Enemyobj.enemystats[enemykey][1] >= 1000)
                                {
                                    shoplabel.Parent.Controls.Remove(shoplabel);
                                    labelbox.Parent.Controls.Remove(labelbox);
                                    if (Enemyobj.enemystats[enemykey][1] == 1002)
                                    {
                                        spawnanotherboss = true;
                                    }
                                    else if (level != 16)
                                    {
                                        PictureBox portalbox = new PictureBox();
                                        labelbox = portalbox;
                                        labelbox.BackColor = Color.Transparent;
                                        labelbox.Image = imgs.menuimgs[3];
                                        labelbox.Size = new Size(100, 200);
                                        labelbox.Location = new Point(Enemyobj.enemycords[enemykey][0], Enemyobj.enemycords[enemykey][1]);
                                        labelbox.Parent = mainbox;
                                        labelbox.BringToFront();
                                        bossSpawnedState = 2;
                                    }
                                    else {
                                        specialConLose = true;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                if (isCrit)               // set enemy crit damaged img
                                {
                                    Enemyobj.SetEnemyFrame(2, enemykey);
                                }
                                else                     // set enemy damaged img
                                {
                                    Enemyobj.SetEnemyFrame(1, enemykey);
                                }
                            }

                        }
                    }
                    if (keysRemove.Count != 0)
                    {
                        foreach (int key in keysRemove)
                        {

                            Bulletobj.skillbulletcords.Remove(key);
                            Bulletobj.skillbullet.Remove(key);
                        }
                        keysRemove.Clear();
                    }
                    // collusion Detection between enemy and Player
                    if (ColideRects(Enemyobj.enemycords[enemykey], enemysize, Playerobj.cords, Playerobj.size) && Playerobj.dmgcdcounter < 0 && Playerobj.playerstats[1] == 0)
                    {
                        Playerobj.dmgcdcounter = 0;
                        Playerobj.playerImg = imgs.playerimg[1];
                        Playerobj.playerstats[0] = 1;
                        damage = ((double)Enemyobj.enemystats[enemykey][5] * Playerobj.dmgreduc);
                        Playerobj.HP -= (int)damage;
                        gameboxlist[4].Width = Playerobj.HP / playerHPRatio;
                        gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                        if (Enemyobj.enemystats[enemykey][1] < 1000) {
                            Enemyobj.SetEnemyFrame(3, enemykey);
                        }
                        Label duradamage = new Label();    // creating a label above the Player on collusion with enemy to show durability decrease
                        if (Playerobj.colides != null && Playerobj.colides.Parent != null)
                        {
                            Playerobj.colides.Parent.Controls.Remove(Playerobj.colides);
                        }
                        Playerobj.labelcounter = 100;
                        Playerobj.colides = duradamage;
                        Font font = new Font("Cooper Black", 12);
                        duradamage.Font = font;
                        duradamage.Location = new Point(Playerobj.cords[0], Playerobj.cords[1] - 40);
                        duradamage.Parent = this.GetChildAtPoint(new Point(duradamage.Location.X, duradamage.Location.Y));
                        duradamage.BackColor = Color.Transparent;
                        duradamage.ForeColor = Color.FromArgb(255, 43, 0);
                        duradamage.AutoSize = true;
                        duradamage.Text = "-" + ((int)damage).ToString() + " Durability";
                    }
                }
            }
            foreach (int enemybulletkey in Bulletobj.enemybulletcords.Keys)   // colusion detection between enemy's bullet and Player
            {
                if (ColideRects(Bulletobj.enemybulletcords[enemybulletkey], Bulletobj.enemybulletimgs[Bulletobj.enemybullet[enemybulletkey][4]][Bulletobj.enemybullet[enemybulletkey][3]].Size, Playerobj.cords, Playerobj.size) && Playerobj.dmgcdcounter < 0 && Playerobj.playerstats[1] == 0) {
                    Playerobj.dmgcdcounter = 0;
                    Playerobj.playerImg = imgs.playerimg[1];
                    Playerobj.playerstats[0] = 1;
                    damage = ((double)Bulletobj.enemybullet[enemybulletkey][2] * Playerobj.dmgreduc);
                    Playerobj.HP -= (int)damage;
                    gameboxlist[4].Width = (Playerobj.HP / playerHPRatio);
                    gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                    Label duradamage1 = new Label();    // creating a label above the Playerobj on collusion with enemy to show durability decrease
                    if (Playerobj.colides != null && Playerobj.colides.Parent != null)
                    {
                        Playerobj.colides.Parent.Controls.Remove(Playerobj.colides);
                    }
                    Playerobj.labelcounter = 100;
                    Playerobj.colides = duradamage1;
                    Font font = new Font("Cooper Black", 12);
                    duradamage1.Font = font;
                    duradamage1.Location = new Point(Playerobj.cords[0], Playerobj.cords[1] - 40);
                    duradamage1.Parent = this.GetChildAtPoint(new Point(duradamage1.Location.X, duradamage1.Location.Y));
                    duradamage1.BackColor = Color.Transparent;
                    duradamage1.ForeColor = Color.FromArgb(255, 43, 0);
                    duradamage1.AutoSize = true;
                    duradamage1.Text = "-" + ((int)damage).ToString() + " Durability";
                    keysRemove.Add(enemybulletkey);
                }
            }
            foreach (int enemybossbulletkey in Bulletobj.enemybossbulletcords.Keys)   // colusion detection between enemy boss's bullet and Player
            {
                if (ColideRects(Bulletobj.enemybossbulletcords[enemybossbulletkey], Bulletobj.enemybossbulletimgs[Bulletobj.enemybossbullet[enemybossbulletkey][4]][Bulletobj.enemybossbullet[enemybossbulletkey][3]].Size, Playerobj.cords, Playerobj.size) && Playerobj.dmgcdcounter < 0 && Playerobj.playerstats[1] == 0)
                {
                    Playerobj.dmgcdcounter = 0;
                    Playerobj.playerImg = imgs.playerimg[1];
                    Playerobj.playerstats[0] = 1;
                    damage = ((double)Bulletobj.enemybossbullet[enemybossbulletkey][2] * Playerobj.dmgreduc);
                    Playerobj.HP -= (int)damage;
                    gameboxlist[4].Width = (Playerobj.HP / playerHPRatio);
                    gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                    Label duradamage1 = new Label();    // creating a label above the Playerobj on collusion with enemy to show durability decrease
                    if (Playerobj.colides != null && Playerobj.colides.Parent != null)
                    {
                        Playerobj.colides.Parent.Controls.Remove(Playerobj.colides);
                    }
                    Playerobj.labelcounter = 100;
                    Playerobj.colides = duradamage1;
                    Font font = new Font("Cooper Black", 12);
                    duradamage1.Font = font;
                    duradamage1.Location = new Point(Playerobj.cords[0], Playerobj.cords[1] - 40);
                    duradamage1.Parent = this.GetChildAtPoint(new Point(duradamage1.Location.X, duradamage1.Location.Y));
                    duradamage1.BackColor = Color.Transparent;
                    duradamage1.ForeColor = Color.FromArgb(255, 43, 0);
                    duradamage1.AutoSize = true;
                    duradamage1.Text = "-" + ((int)damage).ToString() + " Durability";
                    keysRemove.Add(enemybossbulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    Bulletobj.enemybulletcords.Remove(key);
                    Bulletobj.enemybullet.Remove(key);
                }
                keysRemove.Clear();
            }
            if (bossSpawnedState == 2) {  // collusion detection portal and player
                if (ColideRects(new int[] { labelbox.Location.X, labelbox.Location.Y }, labelbox.Size, Playerobj.cords, Playerobj.size) || specialConWin) {
                    specialConWin = false;
                    labelbox.Parent.Controls.Remove(labelbox);
                    outro = true;
                    bossSpawnedState = 0;
                    outrolabel = new Label
                    {
                        Font = new Font("Cooper Black", 20),
                        ForeColor = Color.FromArgb(75, 0, 130),
                        BackColor = Color.Transparent,
                        AutoSize = true,
                        Text = "Mission Succeeded!",
                        Location = new Point((mainbox.Width / 2) - 200, (mainbox.Height / 2) - 50),
                    };
                    outrolabel.Parent = mainbox;
                    outrolabel.BringToFront();
                    if (level < 15 && currWorlds == level)
                    {
                        currWorlds += 1;

                    }
                    else if (bonusWorlds > 96 && bonusWorlds < 102 && bonusWorlds == (level+81))
                    {
                        bonusWorlds++;
                    }
                    WriteToFile();
                }
            }
            int dropValue;
            int droptype;
            int addcoins;
            int dropstats = 0;
            foreach (int dropkey in Dropobj.dropcords.Keys)
            {
                Size dropsize = new Size();
                if (Dropobj.droptypes[dropkey] >= 100)
                {
                    dropsize.Width = 20;
                    dropsize.Height = 20;
                }
                else {
                    dropsize = imgs.dropimgs[Dropobj.droptypes[dropkey]].Size;
                }
                if (ColideRects(Dropobj.dropcords[dropkey], dropsize, Playerobj.cords, Playerobj.size))  // collusion Detection between drops and Playerobj 
                {
                    Label droppickup = new Label();          // creating a label above the Playerobj on drop collusion to mark the item gain     
                    if (Playerobj.colides != null && Playerobj.colides.Parent != null)
                    {
                        Playerobj.colides.Parent.Controls.Remove(Playerobj.colides);
                    }
                    Playerobj.labelcounter = 100;
                    Playerobj.colides = droppickup;
                    Font font = new Font("Cooper Black", 12);
                    droppickup.Font = font;
                    droppickup.Location = new Point(Playerobj.cords[0], Playerobj.cords[1] - 40);
                    droppickup.BackColor = Color.Transparent;
                    droppickup.ForeColor = Color.FromArgb(9, 255, 0);
                    droppickup.Parent = this.GetChildAtPoint(new Point(droppickup.Location.X, droppickup.Location.Y));
                    keysRemove.Add(dropkey);
                    droptype = Dropobj.droptypes[dropkey];
                    droppickup.AutoSize = true;
                    if (droptype == 16 || droptype == 17)
                    {
                        droppickup.Text = "+10%" + Dropobj.dropnames[droptype];
                    }
                    else if (droptype < 100)
                    {
                        droppickup.Text = "+" + Dropobj.amounts[droptype].ToString() + Dropobj.dropnames[droptype];
                    }
                    else
                    {
                        if (droptype < 103)
                        {
                            droppickup.Text = Skillobj.playerskillnames[droptype - 100];
                        }
                        else {
                            droppickup.Text = Skillobj.playerskillnames[((level / 4) * 5) + droptype - 100];
                        }
                    }
                    if (droptype == Dropobj.dropnames.Length - 5)
                    {

                        dropValue = Dropobj.amounts[droptype];
                        addcoins = random.Next(dropValue, level);
                        if (level != 19)
                        {
                            m_mediaPlayer.Open(new Uri(@"C:\Users\\source\repos\spacebattle\spacebattle\sounds\noti.wav"));
                            m_mediaPlayer.Volume = sVolume / 100.0f;
                            m_mediaPlayer.Play();
                            Playerobj.coins += addcoins;
                            Playerobj.inverntory[0] += addcoins;
                            droppickup.Text = "+" + addcoins.ToString() + " " + Dropobj.dropnames[droptype];
                        }
                        else {
                            if (Playerobj.inverntory[0] >= addcoins) {
                                Playerobj.inverntory[0] -= addcoins;
                                droppickup.Text = "-" + addcoins.ToString() + " " + Dropobj.dropnames[droptype];
                                droppickup.ForeColor = Color.FromArgb(255, 43, 0);
                            }
                        }
                    }
                    else if (droptype == 0 || droptype == 1)      // adding drops based on values
                    {
                        dropValue = Dropobj.amounts[droptype];
                        Playerobj.energy += dropValue;
                        if (Playerobj.energy > 100)
                        {
                            Playerobj.energy = 100;
                        }

                        gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                    } else if (droptype == 2)
                    {
                        dropValue = Dropobj.amounts[droptype];
                        Playerobj.fuel += dropValue;
                        if (Playerobj.fuel > 100) {
                            Playerobj.fuel = 100;
                        }
                        gameboxlist[3].Width = Playerobj.fuel;
                    }
                    else if (droptype == 3)
                    {
                        dropValue = Dropobj.amounts[droptype];
                        Playerobj.HP += dropValue;
                        if (Playerobj.HP > Playerobj.maxHP)
                        {
                            Playerobj.HP = Playerobj.maxHP;
                        }
                        gameboxlist[4].Width = (Playerobj.HP / playerHPRatio);
                        gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                    }
                    else if (droptype == 4) // bullet lvl up
                    {
                        if (Bulletobj.bulletLevel <= Playerobj.bulletMaxLevel)
                        {
                            Bulletobj.bulletLevel += 1;
                            gamelabellist[0].Text = "level " + Bulletobj.bulletLevel.ToString();
                        }
                        else {
                            Playerobj.coins += 5;
                            Playerobj.inverntory[0] += 5;
                            droppickup.Text = "+5 coins";
                        }
                    }
                    else if (droptype == 5) //Laser
                    {
                        Bulletobj.bulletSpeed = 20;
                        Bulletobj.bulletDamage = 24;
                        if (Bulletobj.guntype == 3)
                        {
                            droppickup.Text = Dropobj.dropnames[droptype];
                        }
                        else if (Bulletobj.guntype == 0)
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgun.png") ,       //defgun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgunflip45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgunflip-45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungun.png") ,       //bounce gun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungunflip45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungunflip-45.png")
                            };
                        }
                        Bulletobj.guntype = 0;
                        gameboxlist[7].Image = imgs.dropimgs[5];
                        gamelabellist[0].Text = "level " + Bulletobj.bulletLevel.ToString();
                    }
                    else if (droptype == 7) // Bouncing Laser
                    {
                        Bulletobj.bulletSpeed = 20;
                        Bulletobj.bulletDamage = 26;
                        if (Bulletobj.guntype == 0)
                        {
                            droppickup.Text = Dropobj.dropnames[droptype];
                        }
                        else if (Bulletobj.guntype == 3)
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgun.png") ,       //defgun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgunflip45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\defgun\defgunflip-45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungun.png") ,       //bounce gun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungunflip45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bouncegun\boungunflip-45.png")
                            };
                        }
                        Bulletobj.guntype = 3;
                        gameboxlist[7].Image = imgs.dropimgs[7];
                        gamelabellist[0].Text = "level " + Bulletobj.bulletLevel.ToString();

                    }
                    else if (droptype == 8) //Pulse Gun
                    {
                        Bulletobj.bulletDamage = 27;
                        Bulletobj.bulletSpeed = 15;
                        if (Bulletobj.guntype == 2) // 
                        {
                            Bulletobj.bulletcords.Clear();
                            Bulletobj.bulletypes.Clear();
                            droppickup.Text = Dropobj.dropnames[droptype];
                        }
                        else if (Bulletobj.guntype == 1) //Pulse Gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\pulse\pulse.png"),       //pulse gun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter.png"),      //scattered pulse gun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter45flip.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter-45flip.png")
                            };
                        }
                        Bulletobj.guntype = 1;
                        gameboxlist[7].Image = imgs.dropimgs[8];
                        gamelabellist[0].Text = "level " + Bulletobj.bulletLevel.ToString();

                    }
                    else if (droptype == 6) // Scattered Pulse Gun
                    {
                        Bulletobj.bulletDamage = 25;
                        Bulletobj.bulletSpeed = 18;
                        if (Bulletobj.guntype == 1) //Pulse Gun
                        {
                            Bulletobj.bulletcords.Clear();
                            Bulletobj.bulletypes.Clear();
                            droppickup.Text = Dropobj.dropnames[droptype];
                        }
                        else if (Bulletobj.guntype == 2) // Scattered Pulse Gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\pulse\pulse.png"),       //pulse gun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter.png"),      //scattered pulse gun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter45flip.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\pulse\pulsescatter-45flip.png")
                            };
                        }
                        Bulletobj.guntype = 2;
                        gameboxlist[7].Image = imgs.dropimgs[6];
                        gamelabellist[0].Text = "level " + Bulletobj.bulletLevel.ToString();

                    }
                    else if (droptype == 9) // canon gun
                    {
                        Bulletobj.bulletDamage = 20;
                        Bulletobj.bulletSpeed = 10;
                        if (Bulletobj.guntype == 4) // 
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\canonball\canonball.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\canonball\canonballdmg.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\canonball\canonballcrutial.png")
                            };
                        }
                        Bulletobj.guntype = 4;
                        gameboxlist[7].Image = imgs.dropimgs[9];
                    }
                    else if (droptype == 11) // missile
                    {
                        Bulletobj.bulletDamage = 28;
                        Bulletobj.bulletSpeed = 15;
                        if (Bulletobj.guntype == 5) // missile
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\missile\missile.png"),      //missiles
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\missile\missileflip45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\missile\missileflip-45.png")
                            };
                        }
                        Bulletobj.guntype = 5;
                        gameboxlist[7].Image = imgs.dropimgs[11];
                    }
                    else if (droptype == 14) // guided missile
                    {
                        Bulletobj.bulletDamage = 29;
                        Bulletobj.bulletSpeed = 16;
                        if (Bulletobj.guntype == 6) // guided missile
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissile.png"),      //missiles
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip-45.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip90.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip-90.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip135.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip-135.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\guidedmissile\guidedmissileflip180.png")
                            };
                        }
                        Bulletobj.guntype = 6;
                        gameboxlist[7].Image = imgs.dropimgs[14];
                    }
                    else if (droptype == 10) // particle gun
                    {
                        Bulletobj.bulletDamage = 24;
                        Bulletobj.bulletSpeed = 14;
                        if (Bulletobj.guntype == 7) // particle gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particlebullet.png"),       //particle gun
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particle.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particleflip45.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particleflip-45.png"),
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particleflip90.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particleflip-90.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particleflip135.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particleflip-135.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\particlegun\particleflip180.png")
                            };
                        }
                        Bulletobj.guntype = 7;
                        gameboxlist[7].Image = imgs.dropimgs[10];
                    }
                    else if (droptype == 13) // death drone
                    {
                        Bulletobj.bulletDamage = 22;
                        Bulletobj.bulletSpeed = 17;
                        if (Bulletobj.guntype == 8) // death drone
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\deathdrone\deathdrone.png"),       //death drone
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\deathdrone\deathdroneflip45.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\deathdrone\deathdroneflip-45.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\deathdrone\deathdroneflip90.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\deathdrone\deathdroneflip-90.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\deathdrone\deathdroneflip135.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\deathdrone\deathdroneflip-135.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\deathdrone\deathdroneflip180.png")
                            };
                        }
                        Bulletobj.guntype = 8;
                        gameboxlist[7].Image = imgs.dropimgs[13];
                    }
                    else if (droptype == 15) // replicating bullet gun
                    {
                        Bulletobj.bulletDamage = 26;
                        Bulletobj.bulletSpeed = 13;
                        if (Bulletobj.guntype == 9) // replicating bullet gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\replicatingun\replicatingbullet.png")
                            };

                        }
                        Bulletobj.guntype = 9;
                        gameboxlist[7].Image = imgs.dropimgs[15];
                    }
                    else if (droptype == 12) // proton gun
                    {
                        Bulletobj.bulletDamage = 21;
                        Bulletobj.bulletSpeed = 15;
                        if (Bulletobj.guntype == 10) // proton gun
                        {
                            dropstats = 1;
                        }
                        else
                        {
                            dropstats = 2;
                            if (bulletimgs != null)
                            {
                                bulletimgs.Clear();
                            }
                            bulletimgs = new List<Image> {
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\protongun\protonwave.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\protongun\protonwaveflip.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\protongun\pinkproton.png"), 
                            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\protongun\greenproton.png")
                            };
                        }
                        Bulletobj.guntype = 10;
                        gameboxlist[7].Image = imgs.dropimgs[12];
                    }
                    else if (droptype == 16) // crit.chance
                    {
                        if (Playerobj.critChance != Playerobj.maxCritChance)
                        {
                            Playerobj.critChance += 10;
                            gamelabellist[2].Text = Playerobj.critChance.ToString() + "%";
                            if (Playerobj.critChance == Playerobj.maxCritChance)
                            {
                                gamelabellist[2].ForeColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else {
                            Playerobj.coins += 5;
                            Playerobj.inverntory[0] += 5;
                            droppickup.Text = "+5 coins";
                        }
                    }
                    else if (droptype == 17) // crit.damage
                    {
                        if (Playerobj.critDamage != Playerobj.maxCritDamage)
                        {
                            Playerobj.critDamage += 10;
                            gamelabellist[1].Text = Playerobj.critDamage.ToString() + "%";
                            if (Playerobj.critDamage == Playerobj.maxCritDamage)
                            {
                                gamelabellist[1].ForeColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            Playerobj.coins += 5;
                            Playerobj.inverntory[0] += 5;
                            droppickup.Text = "+5 coins";
                        }
                    }
                    else if (droptype == 18) // fire rate 
                    {
                        if (Playerobj.fireRate < Playerobj.maxFireRate)
                        {
                            Playerobj.fireRate += 1;
                            gamelabellist[6].Text = Playerobj.fireRate.ToString();
                            if (Playerobj.fireRate == Playerobj.maxFireRate) {
                                gamelabellist[6].ForeColor = Color.FromArgb(255, 0, 0);
                            }
                        }
                        else
                        {
                            Playerobj.coins += 5;
                            Playerobj.inverntory[0] += 5;
                            droppickup.Text = "+5 coins";
                        }
                    }
                    else if (droptype >= 100) {
                        skillinterfacebox.BackColor = Color.FromArgb(0, 255, 255);
                        if (droptype < 103)
                        {
                            skillinterfacebox.Image = Skillobj.skillbarimgs[droptype - 100];
                            Playerobj.pocketskill = droptype - 100;
                        }
                        else
                        {
                            skillinterfacebox.Image = Skillobj.skillbarimgs[(((level - 1) / 3) * 5) + droptype - 100];
                            Playerobj.pocketskill = (((level-1) / 3) * 5) + droptype - 100;

                        }

                    }
                    else {
                        Playerobj.inverntory[droptype - (Dropobj.amounts.Count() - 5)] += 1;
                    }
                    if (dropstats == 1)
                    {
                        Playerobj.coins += 5;
                        Playerobj.inverntory[0] += 5;
                        droppickup.Text = "+5 coins";
                        gamelabellist[0].Text = "level " + Bulletobj.bulletLevel.ToString();
                    }
                    else if (dropstats == 2) {
                        droppickup.Text = Dropobj.dropnames[droptype];
                        Bulletobj.bulletcords.Clear();
                        Bulletobj.bulletypes.Clear();
                        Bulletobj.bulletbounces.Clear();
                        gamelabellist[0].Text = "level " + Bulletobj.bulletLevel.ToString();
                    }
                    if (level == 17) {
                        Bulletobj.guntype = 0;
                        Bulletobj.bulletSpeed = 20;
                        Bulletobj.bulletDamage = 24;
                        Bulletobj.bulletLevel = 1;
                        gamelabellist[0].Text = "level 1";
                        gameboxlist[7].Image = imgs.dropimgs[5];
                    }
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    Dropobj.dropcords.Remove(key);
                    Dropobj.droptypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }

        private void CreateBullet() {
            if (!keylockSpace)  // only when spacebar is pressed generating bullets in constant fire rate.
            {
                if (bulletCounter >= (100 / Playerobj.fireRate))
                {
                    int bulletguntype = Bulletobj.guntype;
                    int xcord = 0;
                    int ycord = 0;
                    int type = 0;
                    if (bulletguntype == 0 || bulletguntype == 3)
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            int[] playercords = Playerobj.cords;
                            if (Bulletobj.bulletLevel == 1)
                            {
                                xcord = playercords[0] - 35;
                                ycord = playercords[1] + 17;
                                type = 0;
                            }
                            else if (Bulletobj.bulletLevel == 2)
                            {
                                if (i == 0)
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + 7;
                                    type = 0;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + 27;
                                    type = 0;
                                }
                            }
                            else if (Bulletobj.bulletLevel == 3)
                            {
                                xcord = playercords[0] - 35;
                                ycord = playercords[1] + (-3 + (i * 20));
                                type = 0;
                            }
                            else if (Bulletobj.bulletLevel == 4)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-13 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (Bulletobj.bulletLevel == 5)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (Bulletobj.bulletLevel == 6)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 5)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (Bulletobj.bulletLevel == 7)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 5)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else if (i == 6)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (Bulletobj.bulletLevel == 8)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 5)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else if (i == 6)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 7)
                                {
                                    xcord = playercords[0] + 40;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }

                            }
                            else if (Bulletobj.bulletLevel == 9)
                            {
                                if (i == 3)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] - 43;
                                    type = 1;

                                }
                                else if (i == 4)
                                {
                                    xcord = playercords[0] - 30;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 5)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else if (i == 6)
                                {
                                    xcord = playercords[0] + 5;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else if (i == 7)
                                {
                                    xcord = playercords[0] + 40;
                                    ycord = playercords[1] - 43;
                                    type = 1;
                                }
                                else if (i == 8)
                                {
                                    xcord = playercords[0] + 40;
                                    ycord = playercords[1] + 67;
                                    type = 2;
                                }
                                else
                                {
                                    xcord = playercords[0] - 35;
                                    ycord = playercords[1] + (-3 + (i * 20));
                                    type = 0;
                                }
                            }
                            Bulletobj.CreateBullet(xcord, ycord, type, bulletcounter);
                            if (i == (Bulletobj.bulletLevel-1) && Skillobj.anotherbullet) {
                                bulletcounter += 1;
                                Bulletobj.CreateBullet(playercords[0] - 55, playercords[1] + 17, 0, bulletcounter);
                            }
                        }

                    }
                    else if (bulletguntype == 1)  //Create pulse type bullet
                    {
                        xcord = Playerobj.cords[0] - 30;
                        ycord = Playerobj.cords[1] - 8;
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 3 || i == 6)
                            {     // 4th and 7th pulse will start more indented
                                xcord -= 45;
                                ycord = Playerobj.cords[1] - 8;
                            }
                            else if (i == 1 || i == 4 || i == 7)
                            {    // 2th and 5th and 8th pulse start higher
                                ycord = Playerobj.cords[1] - 78;
                            }
                            else if (i == 2 || i == 5 || i == 8)    // 3th and 6th and 9th pulse start lower
                            {
                                ycord = Playerobj.cords[1] + 62;
                            }
                            Bulletobj.CreatePulse(xcord, ycord, bulletcounter, 0);
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                bulletcounter += 1;
                                Bulletobj.CreatePulse(Playerobj.cords[0] - 50, Playerobj.cords[1] - 8, bulletcounter, 0);
                            }
                        }
                    }
                    else if (bulletguntype == 2)  //Create pulse type bullet
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 8;
                                type = 1;
                            }
                            else if (i == 1)
                            {
                                xcord = Playerobj.cords[0] + 10;
                                ycord = Playerobj.cords[1] - 40;
                                type = 2;
                            }
                            else if (i == 2)
                            {
                                xcord = Playerobj.cords[0] + 10;
                                ycord = Playerobj.cords[1] + 40;
                                type = 3;
                            }
                            else if (i == 3)
                            {
                                xcord = Playerobj.cords[0] - 75;
                                ycord = Playerobj.cords[1] - 8;
                                type = 1;
                            }
                            else if (i == 4)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 80;
                                type = 2;
                            }
                            else if (i == 5)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 80;
                                type = 3;
                            }
                            else if (i == 6)
                            {
                                xcord = Playerobj.cords[0] - 120;
                                ycord = Playerobj.cords[1] - 8;
                                type = 1;
                            }
                            else if (i == 7)
                            {
                                xcord = Playerobj.cords[0] - 70;
                                ycord = Playerobj.cords[1] - 120;
                                type = 2;
                            }
                            else if (i == 8)
                            {
                                xcord = Playerobj.cords[0] - 70;
                                ycord = Playerobj.cords[1] + 120;
                                type = 3;
                            }
                            Bulletobj.CreatePulse(xcord, ycord, bulletcounter, type);
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                bulletcounter += 1;
                                Bulletobj.CreatePulse(Playerobj.cords[0] - 50, Playerobj.cords[1] - 8, bulletcounter, 1);
                            }
                        }
                    }
                    else if (bulletguntype == 4)  //Create canon ball type bullet
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 10;
                                type = 0;
                            }
                            else if (i == 1)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 2)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 3)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 40;
                                type = 0;
                            }
                            else if (i == 4)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 5)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 6)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 20;
                                type = 0;
                            }
                            else if (i == 7)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 8)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;
                            }
                            Bulletobj.CreateCanonBall(xcord, ycord, bulletcounter, type);
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                bulletcounter += 1;
                                Bulletobj.CreateCanonBall(Playerobj.cords[0] - 50, Playerobj.cords[1] + 10, bulletcounter, 0);
                            }
                        }

                    }
                    else if (bulletguntype == 5)  //Create missile type bullet
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 10;
                                type = 10;
                            }
                            else if (i == 1)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] - 34;
                                type = 21;
                            }
                            else if (i == 2)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] + 54;
                                type = 32;
                            }
                            else if (i == 3)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 40;
                                type = 40;
                            }
                            else if (i == 4)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] - 34;
                                type = 51;
                            }
                            else if (i == 5)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] + 54;
                                type = 62;
                            }
                            else if (i == 6)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 20;
                                type = 70;
                            }
                            else if (i == 7)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] - 34;
                                type = 81;
                            }
                            else if (i == 8)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] + 54;
                                type = 92;
                            }
                            Bulletobj.CreateMissile(xcord, ycord, bulletcounter, type);
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                bulletcounter += 1;
                                Bulletobj.CreateMissile(Playerobj.cords[0] - 50, Playerobj.cords[1] + 10, bulletcounter, 10);
                            }
                        }

                    }
                    else if (bulletguntype == 6)  //Create guided missile type bullet
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 10;
                                type = 0;
                            }
                            else if (i == 1)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 2)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 3)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 40;
                                type = 0;
                            }
                            else if (i == 4)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 5)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 6)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 20;
                                type = 0;
                            }
                            else if (i == 7)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 8)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;

                            }
                            Bulletobj.CreateGuidedMissile(xcord, ycord, bulletcounter, type);
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                bulletcounter += 1;
                                Bulletobj.CreateGuidedMissile(Playerobj.cords[0] - 50, Playerobj.cords[1] + 10, bulletcounter, 0);
                            }
                        }

                    }
                    else if (bulletguntype == 7)  //Create particle type
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            bulletcounter += 1;
                            if (i == 0)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 10;
                                type = 0;
                            }
                            else if (i == 1)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 2)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 3)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 40;
                                type = 0;
                            }
                            else if (i == 4)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 5)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;
                            }
                            else if (i == 6)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 20;
                                type = 0;
                            }
                            else if (i == 7)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] - 34;
                                type = 1;
                            }
                            else if (i == 8)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] + 54;
                                type = 2;
                            }
                            bulletcounter = Bulletobj.CreateParticle(xcord, ycord, bulletcounter, type, true);
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                bulletcounter += 1;
                                bulletcounter = Bulletobj.CreateParticle(Playerobj.cords[0] - 50, Playerobj.cords[1] + 10, bulletcounter, 0, true);
                            }
                        }

                    }
                    else if (bulletguntype == 8)  //Create death drone
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            if (i == 0)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 10;
                            }
                            else if (i == 1)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] - 34;
                            }
                            else if (i == 2)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] + 54;
                            }
                            else if (i == 3)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 40;
                            }
                            else if (i == 4)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] - 34;
                            }
                            else if (i == 5)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] + 54;
                            }
                            else if (i == 6)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 20;
                            }
                            else if (i == 7)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] - 34;
                            }
                            else if (i == 8)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] + 54;
                            }
                            Bulletobj.CreateDeathDrone(xcord, ycord, bulletcounter, 0, 4, random);
                            bulletcounter += 4;
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                Bulletobj.CreateDeathDrone(Playerobj.cords[0] - 50, Playerobj.cords[1] + 10, bulletcounter, 0, 4, random);
                                bulletcounter += 4;
                            }
                        }

                    }
                    else if (bulletguntype == 9)  //Create replicating bullet
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            if (i == 0)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 10;
                                type = 1;
                            }
                            else if (i == 1)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 2)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] + 54;
                                type = 3;
                            }
                            else if (i == 3)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 40;
                                type = 1;
                            }
                            else if (i == 4)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 5)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] + 54;
                                type = 3;
                            }
                            else if (i == 6)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 20;
                                type = 1;
                            }
                            else if (i == 7)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 8)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] + 54;
                                type = 3;
                            }
                            Bulletobj.CreateDeathDrone(xcord, ycord, bulletcounter, type, 1, random);
                            bulletcounter += 1;
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                Bulletobj.CreateDeathDrone(Playerobj.cords[0] - 50, Playerobj.cords[1] + 10, bulletcounter, 1, 1, random);
                                bulletcounter += 1;
                            }
                        }

                    }
                    else if (bulletguntype == 10)  //Create protonwave
                    {
                        for (int i = 0; i < Bulletobj.bulletLevel; i++)
                        {
                            if (i == 0)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 10;
                                type = 1;
                            }
                            else if (i == 1)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 2)
                            {
                                xcord = Playerobj.cords[0] + 54;
                                ycord = Playerobj.cords[1] + 54;
                                type = 3;
                            }
                            else if (i == 3)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] + 40;
                                type = 1;
                            }
                            else if (i == 4)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 5)
                            {
                                xcord = Playerobj.cords[0] + 94;
                                ycord = Playerobj.cords[1] + 54;
                                type = 3;
                            }
                            else if (i == 6)
                            {
                                xcord = Playerobj.cords[0] - 30;
                                ycord = Playerobj.cords[1] - 20;
                                type = 1;
                            }
                            else if (i == 7)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] - 34;
                                type = 2;
                            }
                            else if (i == 8)
                            {
                                xcord = Playerobj.cords[0] + 14;
                                ycord = Playerobj.cords[1] + 54;
                                type = 3;
                            }
                            Bulletobj.CreateDeathDrone(xcord, ycord, bulletcounter, type, 1, random); // proton bullet type
                            bulletcounter += 1;
                            if (i == (Bulletobj.bulletLevel - 1) && Skillobj.anotherbullet)
                            {
                                Bulletobj.CreateDeathDrone(Playerobj.cords[0] - 50, Playerobj.cords[1] + 10, bulletcounter, 1, 1, random);
                                bulletcounter += 1;
                            }
                        }
                    }
                    bulletCounter = 0;
                }
            }
        }

        public void CreateBossBullet() {
            Boolean bossAlive = false;
            int bulletdir;
            int[] enemybosscords = { }, playercords;
            List<int[]> bossbulletlist;
            Size bossize;
            int[] bullettypes = { 2, 4, 6, 0, 1, 3, 7, 5 };
            foreach (int enemykey in Enemyobj.enemystats.Keys)
            {
                if (Enemyobj.enemystats[enemykey][1] >= 1000 && Enemyobj.enemystats[enemykey][0] != 3)
                { // only alive boss enemy can fire bullets
                    enemybosscords = Enemyobj.enemycords[enemykey];
                    bossAlive = true;
                }
            }
            if (bossAlive)
            {
                switch (level) {
                    case 1:
                        bossbulletlist = new List<int[]> { new int[] { -20, 190 }, new int[] { 90, -20 }, new int[] { 200, -40 }, new int[] { 310, -20 }, new int[] { 420, 190 }, new int[] { 310, 320 }, new int[] { 200, 340 }, new int[] { 90, 320 } };
                        for (int k = 0; k < 8; k++)
                        {
                            enemybossbulletcounter += 1;
                            bossbulletlist[k][0] += enemybosscords[0];
                            bossbulletlist[k][1] += enemybosscords[1];
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], k, enemybossbulletcounter, 10, 0, 20);
                        }
                        break;
                    case 2:
                        bossbulletlist = new List<int[]> { new int[] { -20, 190 }, new int[] { 90, -20 }, new int[] { 200, -40 }, new int[] { 310, -20 }, new int[] { 420, 190 }, new int[] { 310, 320 }, new int[] { 200, 340 }, new int[] { 90, 320 } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            bossbulletlist[k][0] += enemybosscords[0];
                            bossbulletlist[k][1] += enemybosscords[1];
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], k, enemybossbulletcounter, 10, 1, 20);
                            if (bossbulletlist[k][1] > 0)
                            {
                                enemybossbulletcounter += 1;
                                Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0] + 30, bossbulletlist[k][1] + 30, k, enemybossbulletcounter, 10, 1, 22);
                                enemybossbulletcounter += 1;
                                Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0] + 60, bossbulletlist[k][1] + 60, k, enemybossbulletcounter, 10, 1, 22);
                            }
                            else {
                                enemybossbulletcounter += 1;
                                Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0] - 30, bossbulletlist[k][1] - 30, k, enemybossbulletcounter, 10, 1, 22);
                                enemybossbulletcounter += 1;
                                Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0] - 60, bossbulletlist[k][1] - 60, k, enemybossbulletcounter, 10, 1, 22);
                            }

                        }
                        break;
                    case 3:
                        int s = 0;
                        int bossbtype = 2;
                        bossbulletlist = new List<int[]> { new int[] { -120, 140 }, new int[] { -10, -20 }, new int[] { 100, -40 }, new int[] { 210, -20 }, new int[] { 320, 140 }, new int[] { 210, 270 }, new int[] { 100, 290 }, new int[] { -10, 270 }, new int[] { 180, 140 }, new int[] { 290, -20 }, new int[] { 400, -40 }, new int[] { 510, -20 }, new int[] { 620, 140 }, new int[] { 510, 270 }, new int[] { 400, 290 }, new int[] { 290, 270 } };
                        int length = bossbulletlist.Count();
                        if (Enemyobj.enemystats[Enemyobj.GetBossKey()][1] == 1003) {
                            length /= 2;
                            bossbtype = 3;
                        }
                        for (int k = 0; k < length; k++)
                        {
                            enemybossbulletcounter += 1;
                            bossbulletlist[k][0] += enemybosscords[0];
                            bossbulletlist[k][1] += enemybosscords[1];
                            if (s > ((length / 2) - 1) && Enemyobj.enemystats[Enemyobj.GetBossKey()][1] != 1003) {
                                s = 0;
                            }
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], s, enemybossbulletcounter, 12, bossbtype ,24);
                            s++;
                        }
                        break;
                    case 4:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        ;
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 10, 4,26);
                        }
                        break;
                    case 5:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 10, 5,28);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0] + 40, bossbulletlist[k][1] + 40, bullettypes[k], enemybossbulletcounter, 10, 5,28);
                        }
                        break;
                    case 6:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 10, 6, 35);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1]+40, bullettypes[k], enemybossbulletcounter, 10, 6, 35);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0]+ 40, bossbulletlist[k][1] + 40, bullettypes[k], enemybossbulletcounter, 10, 6, 35);
                        }
                        break;
                    case 7:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 11, 7 ,38);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0]+40, bossbulletlist[k][1]+40, bullettypes[k], enemybossbulletcounter, 11, 7, 38);
                        }
                        break;
                    case 8:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 11, 8,38);
                        }
                        break;
                    case 9:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 13, 9, 40);
                        }
                        break;
                    case 10:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size; // { 2, 4, 6, 0, 1, 3, 7, 5 };
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        playercords = Playerobj.cords;
                        if (playercords[0] > enemybosscords[0])
                        {
                            if (playercords[1] > enemybosscords[1])
                            {
                                bulletdir = 5;
                            }
                            else if (playercords[1] < enemybosscords[1])
                            {
                                bulletdir = 3;
                            }
                            else
                            {
                                bulletdir = 4;
                            }
                        }
                        else if (playercords[0] < enemybosscords[0])
                        {
                            if (playercords[1] > enemybosscords[1])
                            {
                                bulletdir = 7;
                            }
                            else if (playercords[1] < enemybosscords[1])
                            {
                                bulletdir = 1;
                            }
                            else
                            {
                                bulletdir = 0;
                            }
                        }
                        else {
                            if (playercords[1] > enemybosscords[1])
                            {
                                bulletdir = 6;
                            }
                            else if (playercords[1] < enemybosscords[1])
                            {
                                bulletdir = 2;
                            }
                            else
                            {
                                bulletdir = 4;
                            }
                        }
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bulletdir, enemybossbulletcounter, 9, 10, 45);
                        }
                        break;
                    case 11:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 14, 11, 48);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1]+ 300, bullettypes[k], enemybossbulletcounter, 14, 11, 48);
                        }
                        break;
                    case 12:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 15, 12, 55);
                        }
                        break;
                    case 13:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 15, 13, 75);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0]+ 40, bossbulletlist[k][1]+40, bullettypes[k], enemybossbulletcounter, 15, 13, 75);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1] + 80, bullettypes[k], enemybossbulletcounter, 15, 13, 75);
                        }
                        break;
                    case 14:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 13, 14, 100);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1] + 80, bullettypes[k], enemybossbulletcounter, 13, 14, 100);
                        }
                        break;
                    case 15:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 10, 15, 120);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1] + 150, bullettypes[k], enemybossbulletcounter, 10, 15, 120);
                        }
                        break;
                    case 16:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 10, 16, 20);
                        }
                        break;
                    case 17:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < 4; k++)
                        {
                            int randombullet = random.Next(0,8);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[randombullet][0], bossbulletlist[randombullet][1], bullettypes[randombullet], enemybossbulletcounter, 10, 17, 80);
                        }
                        break;
                    case 18:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size; // { 2, 4, 6, 0, 1, 3, 7, 5 };
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 } };
                        playercords = Playerobj.cords;
                        if (playercords[0] > enemybosscords[0])
                        {
                            if (playercords[1] > enemybosscords[1])
                            {
                                bulletdir = 5;
                            }
                            else if (playercords[1] < enemybosscords[1])
                            {
                                bulletdir = 3;
                            }
                            else
                            {
                                bulletdir = 4;
                            }
                        }
                        else if (playercords[0] < enemybosscords[0])
                        {
                            if (playercords[1] > enemybosscords[1])
                            {
                                bulletdir = 7;
                            }
                            else if (playercords[1] < enemybosscords[1])
                            {
                                bulletdir = 1;
                            }
                            else
                            {
                                bulletdir = 0;
                            }
                        }
                        else
                        {
                            if (playercords[1] > enemybosscords[1])
                            {
                                bulletdir = 6;
                            }
                            else if (playercords[1] < enemybosscords[1])
                            {
                                bulletdir = 2;
                            }
                            else
                            {
                                bulletdir = 4;
                            }
                        }
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bulletdir, enemybossbulletcounter, 13, 18, 100);
                        }
                        break;
                    case 19:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < 4; k++)
                        {
                            int randombullet = random.Next(0, 8);
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[randombullet][0], bossbulletlist[randombullet][1], bullettypes[randombullet], enemybossbulletcounter, 10, 19, 110);
                        }
                        break;
                    case 20:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 10, 20, 20);
                        }
                        break;
                    case 21:
                        bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][Enemyobj.enemystats[Enemyobj.GetBossKey()][0]].Size;
                        bossbulletlist = new List<int[]> { new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0] + bossize.Width / 2, enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height / 2 }, new int[] { enemybosscords[0], enemybosscords[1] }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] }, new int[] { enemybosscords[0], enemybosscords[1] + bossize.Height }, new int[] { enemybosscords[0] + bossize.Width, enemybosscords[1] + bossize.Height } };
                        for (int k = 0; k < bossbulletlist.Count(); k++)
                        {
                            enemybossbulletcounter += 1;
                            Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], enemybossbulletcounter, 15, 21, 30);
                        }
                        break;
                } 
            }
        }

        private void ResetCounters() {
            if (dropcounter > 10000) {
                dropcounter = 0;
            }
            if (miscounter > 10000)
            {
                miscounter = 0;
            }
            if (bulletcounter > 100000) {
                bulletcounter = 0;
            }
            if (enemybulletcounter > 10000)
            {
                enemybulletcounter = 0;
            }
            if (enemybossbulletcounter > 10000)
            {
                enemybossbulletcounter = 0;
            }
            if (enemycounter > 10000) {
                enemycounter = 0;
            }
            if (planetcounter > 10000)
            {
                planetcounter = 0;
            }
    }

        private int DropImgIndex() {
            switch (Bulletobj.guntype)
            {
                case 0:
                    return 0;
                case 1:
                    return 3;
                case 2:
                    return 1;
                case 3:
                    return 2;
                case 4:
                    return 4;
                case 5:
                    return 6;
                case 6:
                    return 9;
                case 7:
                    return 5;
                case 8:
                    return 8;
                case 9:
                    return 10;
                case 10:
                    return 7;
                default:
                    return 0;
            }
        }

        public int GetBulletIndx(int bulletkey)
        {
            int indx;
            switch (Bulletobj.guntype)
            {
                case 0:
                    indx = Bulletobj.bulletypes[bulletkey];
                    return indx;
                case 1:
                    return 0;
                case 2:
                    indx = Bulletobj.bulletypes[bulletkey];
                    return indx;
                case 3:
                    indx = Bulletobj.bulletypes[bulletkey];
                    return indx;
                case 4:
                    indx = Bulletobj.bulletbounces[bulletkey];
                    return 3-indx;
                case 5:
                    indx = Bulletobj.bulletypes[bulletkey];
                    return (indx % 10);
                case 6:
                    indx = Bulletobj.bulletypes[bulletkey];
                    return indx;
                case 7:
                    indx = 0;
                    if (Bulletobj.bulletypes[bulletkey] > 10)
                    { // check if bullet or particle
                        indx = Bulletobj.bulletypes[bulletkey] - 10;
                    }
                    return indx;
                case 8:
                    indx = Bulletobj.bulletypes[bulletkey];
                    return indx;
                case 9:
                    return 0;
                case 10:
                    if (Bulletobj.bulletypes[bulletkey] < 10)
                    {
                        indx = Bulletobj.bulletypes[bulletkey];
                        if (indx == 2)
                            indx = 1;
                    }
                    else
                    {
                        indx = (Bulletobj.bulletypes[bulletkey] / 10) + 1;
                    }
                    return indx;
                default:
                    return 0;
            }
        }

        private void ModifySettings(int mode) {
            string fileline = "", asmbstring = "";
            int[] paramas = new int[3];
            int i = 0;
            if (mode == 0)
            {
                try
                {
                    StreamReader sr = File.OpenText(@"C:\Users\\source\repos\spacebattle\spacebattle\stt.vla");
                    fileline = sr.ReadLine();
                    for (int j = 0; j < fileline.Length; j++)
                    {
                        if (fileline[j] != ',')
                        {
                            asmbstring += fileline[j];
                        }
                        else
                        {
                            paramas[i] = int.Parse(asmbstring);
                            asmbstring = "";
                            i++;
                        }
                    }
                    fullscreen = paramas[0];
                    mVolume = paramas[1]*10;
                    sVolume = paramas[2]*10;
                    sr.Close();
                }
                catch
                {
                    fileline = "0,0,0,";
                    StreamWriter sw = File.CreateText(@"C:\Users\\source\repos\spacebattle\spacebattle\stt.vla"); // if file not found Create one
                    sw.WriteLine(fileline);
                    sw.Flush();
                    sw.Close();
                }
            }
            else {
                fileline = fullscreen.ToString()+","+ (mVolume/10).ToString() + "," + (sVolume / 10).ToString() + ",";
                StreamWriter sw = File.CreateText(@"C:\Users\\source\repos\spacebattle\spacebattle\stt.vla"); // if file not found Create one
                sw.WriteLine(fileline);
                sw.Flush();
                sw.Close();
            }
        }

        private void GameClose(object sender, FormClosedEventArgs e)
        {
            mTimerId = 0;
            int err = timeKillEvent(mTimerId);
            timeEndPeriod(1);
            // Ensure callbacks are drained
            System.Threading.Thread.Sleep(1000);
        }
        private delegate void TestEventHandler(int tick, TimeSpan span);
        private void TimerCallback(int id, int msg, IntPtr user, int dw1, int dw2) 
        {
            mTestTick += 1;
            if ((mTestTick % 17) == 0 && mTimerId != 0)
                this.BeginInvoke(new TestEventHandler(TimerEvent), mTestTick, DateTime.Now - mTestStart);
            if (mTestTick > 17000) {
                mTestTick = 1;
            }
        }
        // P/Invoke declarations
        private delegate void TimerEventHandler(int id, int msg, IntPtr user, int dw1, int dw2);
        private const int TIME_PERIODIC = 1;
        private const int EVENT_TYPE = TIME_PERIODIC;// + 0x100;  // TIME_KILL_SYNCHRONOUS causes a hang ?!
        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay, int resolution, TimerEventHandler handler, IntPtr user, int eventType);
        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);
        [DllImport("winmm.dll")]
        private static extern int timeBeginPeriod(int msec);
        [DllImport("winmm.dll")]
        private static extern int timeEndPeriod(int msec);
    }

}
