using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace spacebattle
{
    static class Skillobj
    {
        public static Dictionary<int, int[]> enemySkills = new Dictionary<int, int[]>();// {enemykey ,{ skillid, durationcount, cooldowncount, frame, img enable, duration, cooldown}
        public static Dictionary<int, int[]> enemyskilldata = new Dictionary<int, int[]>() {  // {id ,{ duration, cooldown}}
        {1004 ,new int[]{ 4, 10} },// every 30  * interval update duration and cooldown
        {1005 ,new int[]{ 1, 7} },
        {1006 ,new int[]{ 10, 20 } },
        {1007 ,new int[]{ 5, 15 } },
        {1008 ,new int[]{ 1, 10 } },
        {1009 ,new int[]{ 5, 15 } },
        {1010 ,new int[]{ 6, 10 } },
        {1011 ,new int[]{ 4, 8 } },
        {1012 ,new int[]{ 1, 6 } },
        {1013 ,new int[]{ 1, 7 } },
        {1014 ,new int[]{ 1, 10 } },
        {1015 ,new int[]{ 1, 6 } },
        {1016 ,new int[]{ 1, 6 } },
        {1017 ,new int[]{ 1, 6 } },
        {1018 ,new int[]{ 3, 5 } },
        {1019 ,new int[]{ 4, 6 } },
        {1020 ,new int[]{ 5, 10 } },
        {1021 ,new int[]{ 4, 10 } },
        {1 ,new int[]{ 4, 10 } }, // hp recovery
        {2 ,new int[]{ 4, 10 } },// speed up
        {3 ,new int[]{ 4, 10 } },// defense up
        {4 ,new int[]{ 4, 10 } }, // change direction
        {5 ,new int[]{ 2, 10 } },// another bullet
        {6 ,new int[]{ 2, 7 } }, // another bullet flying upwards
        {7 ,new int[]{ 3, 8 } },// damage immnue
        {8 ,new int[]{ 2, 7 } }, // another bullet flying downwards
        {9 ,new int[]{ 1, 8 } },// bio support
        {10 ,new int[]{ 2, 7 } },// another bullet flying SE
        {11 ,new int[]{ 2, 8 } },// lava ghost support
        {12 ,new int[]{ 2, 10 } },// beam
        {13 ,new int[]{ 2, 10 } }// double strike
        };

        public static Dictionary<int, int[]> playerskilldata = new Dictionary<int, int[]> {  // {id ,{ duration, cooldown, energycon}}
            {0 ,new int[]{ 5, 20, 2} },// durability up
            {1 ,new int[]{ 5, 20, 2} },// fuel up
            {2 ,new int[]{ 10, 20, 4} },// speed up
            {3 ,new int[]{ 5, 40, 6} },// coin up
            {4 ,new int[]{ 5, 50, 8} },// damage immunity
            {5 ,new int[]{ 7, 20, 4} },// armour up
            {6 ,new int[]{ 7, 20, 5} },// damage up
            {7 ,new int[]{ 10, 25, 6} },// skyball
            {8 ,new int[]{ 20, 70, 10} },// max dur up
            {9 ,new int[]{ 8, 30, 8} },// fire rate up
            {10 ,new int[]{ 10, 22, 4} },// criticals up 
            {11 ,new int[]{ 12, 21, 6} },// fire ball
            {12 ,new int[]{ 13, 25, 6} },// energy ball
            {13 ,new int[]{ 6, 40, 10} },// time stop
            {14 ,new int[]{ 30, 60, 8} },// max energy up
            {15 ,new int[]{ 1, 70, 4} },//  lucky gift
            {16 ,new int[]{ 12, 21, 6} },// ice ball
            {17 ,new int[]{ 13, 25, 6} },// stream
            {18 ,new int[]{ 5, 25, 6} },// adv.durability up
            {19 ,new int[]{ 5, 25, 6} },// adv.fuel up
            {20 ,new int[]{ 10, 35, 8} },//  adv.speed up
            {21 ,new int[]{ 5, 45, 12} },// adv.coin up
            {22 ,new int[]{ 13, 25, 7} },// poison
            {23 ,new int[]{ 12, 25, 8} },// adv.armour up
            {24 ,new int[]{ 10, 25, 8} },// adv.criticals up
            {25 ,new int[]{ 8, 35, 12} },// adv.damage up
            {26 ,new int[]{ 7, 45, 14} },// Durability Vampire
            {27 ,new int[]{ 10, 25, 7} },// ironball
            {28 ,new int[]{ 20, 35, 8} },// adv.max durability up
            {29 ,new int[]{ 10, 25, 8} },// adv.damage immunity up
            {30 ,new int[]{ 8, 20, 12} },//  adv.fire rate up
            {31 ,new int[]{ 8, 20, 10} },// another bullet
            {32 ,new int[]{ 10, 20, 7} },// firewall
            {33 ,new int[]{ 50, 35, 8} },// Adv.Max Energy Up 
            {34 ,new int[]{ 12, 25, 12} },// Adv.Another Bullet
            {35 ,new int[]{ 10, 50, 16} },// Adv.Durability Vampire
            {36 ,new int[]{ 9, 50, 12} },// Adv.Time Stop
            {37 ,new int[]{ 15, 30, 9} }// Ultimate Beam
        };

        public static Dictionary<int, String> playerskillnames = new Dictionary<int, String>
        {
            {0, "Durability Up" },
            {1, "Fuel Up"  },
            {2, "Speed Up" },
            {3, "Coin Up"  },
            {4, "Damage Immunity" },
            {5, "Armour Up"  },
            {6, "Damage Up" },
            {7, "Skyball"  },
            {8, "Max Durability Up" },
            {9, "Fire Rate Up"  },
            {10, "Criticals Up" },
            {11, "Fireball"  },
            {12, "Energyball" },
            {13, "Time Stop"  },
            {14, "Max Energy Up" },
            {15, "Lucky Gift"  },
            {16, "Iceball" },
            {17, "Water Stream"  },
            {18, "Adv.Durability Up" },
            {19, "Adv.Fuel Up"  },
            {20, "Adv.Speed Up" },
            {21, "Adv.Coin Up"  },
            {22, "Poison" },
            {23, "Adv.Armour Up"  },
            {24, "Adv.Criticals Up" },
            {25, "Adv.Damage Up"  },
            {26, "Durability Vampire" },
            {27, "Ironball"  },
            {28, "Adv.Max Durability Up" },
            {29, "Adv.Damage Immunity"  },
            {30, "Adv.Fire Rate Up" },
            {31, "Another Bullet"  },
            {32, "Firewall" },
            {33, "Adv.Max Energy Up"  },
            {34, "Adv.Another Bullet" },
            {35, "Adv.Durability Vampire"  },
            {36, "Adv.Time Stop" },
            {37, "Ultimate Beam"  },
        };

        public static Dictionary<int, int[]> playerSkills = new Dictionary<int, int[]> {// {id, {duration, cooldown, energycon}}
            {0 ,new int[]{ 5, 20, 2 } },
            {1 ,new int[]{ 5, 20, 2 } },
            {2 ,new int[]{ 10, 20, 4 } }
        };

        public static Dictionary<int, Image> skilldropimgs = new Dictionary<int, Image>();
        public static Dictionary<int, Image> skillbarimgs = new Dictionary<int, Image>();
        public static Dictionary<int, List<Image>> skillframes = new Dictionary<int, List<Image>>();
        public static Dictionary<int, List<Image>> enemyskillframes = new Dictionary<int, List<Image>>();

        private static readonly Random random = new Random();
        public static bool timestop = false, canceladv = false, anotherbullet = false;

        public static void AddEnemySkill(int enemykey, int skillid) {
            enemySkills.Add(enemykey, new int[] { skillid, enemyskilldata[skillid][0], enemyskilldata[skillid][1], 0, 0, enemyskilldata[skillid][0], enemyskilldata[skillid][1] });
        }
        public static void RemoveEnemySkill(int enemykey)
        {
            enemySkills.Remove(enemykey);
        }

        public static void UpdateEnemySkillCount(int framewidth, Imgs img, int level) {
            if (enemySkills.Count != 0) {
                foreach (int enemykey in enemySkills.Keys)
                {
                    if (enemySkills[enemykey][1] > 0) {  // if not on cooldown countdown skill duration
                        if (random.Next(0, 3) > 1) {
                            enemySkills[enemykey][4] = 1;
                        }
                        if (enemySkills[enemykey][4] != 0) {
                            UseEnemySkill(enemykey, framewidth, img, level);
                            enemySkills[enemykey][1] -= 1;
                        }
                        
                    }
                    if (enemySkills[enemykey][2] > 0) { // reduce cooldown counter
                        enemySkills[enemykey][2] -= 1;
                    }
                    if (enemySkills[enemykey][1] == 0 && enemySkills[enemykey][2] == 0)
                    {
                        enemySkills[enemykey][1] = enemySkills[enemykey][5]; // if cd is 0 and skill effect has ended reset skill
                        enemySkills[enemykey][2] = enemySkills[enemykey][6];
                        enemySkills[enemykey][4] = 1;
                    } else if (enemySkills[enemykey][1] == 0) { // if skill still on cd dont draw img
                        enemySkills[enemykey][4] = 0;
                    }
                }
            }
        }

        public static void UpdatePlayerSkillCount(int index, Imgs img)
        {
            int skill = playerSkills.ElementAt(index).Key;
            if (playerSkills[skill][0] > 0)
            {  
                UsePlayerSkill(skill, playerskilldata[skill][2], img);
                playerSkills[skill][0] -= 1;
            }
            if (playerSkills[skill][1] > 0)
            { // reduce cooldown counter
                playerSkills[skill][1] -= 1;
            }
            if (playerSkills[skill][0] == 0) {
                Game.skillframecounters[index] = -1;
            }
            if (playerSkills[skill][0] == 0 && playerSkills[skill][1] == 0) // reset skill
            {
                playerSkills[skill][0] = playerskilldata[skill][0];
                playerSkills[skill][1] = playerskilldata[skill][1];
                Game.playerskillscounters[index] = -2;
            }
        }

        public static void UseEnemySkill(int enemykey,int framewidth, Imgs img, int level) {
            int[] bosscords, cords;
            Size bossize;
            List<int> bullettypes;
            List<int[]> bossbulletlist;
            int enemytype, dirc, ystart, distancex, distancey, bullettype;
            switch (enemySkills[enemykey][0]) {
                case 1004:
                    if (enemySkills[enemykey][1] == 4)
                    {
                        Enemyobj.enemystats[enemykey][6] = 5;
                    }
                    if (enemySkills[enemykey][1] == 1)
                    {
                        Enemyobj.enemystats[enemykey][6] = 0;
                    }
                    break;
                case 1005:
                    bosscords = Enemyobj.enemycords[Enemyobj.GetBossKey()];
                    bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][0].Size;
                    if (random.Next(0, 2) == 1) {
                        bullettypes = new List<int> { 2, 4, 6, 0};
                        bossbulletlist = new List<int[]> { new int[] { bosscords[0] + bossize.Width / 2, bosscords[1] }, new int[] { bosscords[0] + bossize.Width, bosscords[1] + bossize.Height / 2 }, new int[] { bosscords[0] + bossize.Width / 2, bosscords[1] + bossize.Height }, new int[] { bosscords[0], bosscords[1] + bossize.Height / 2 } };
                    } else {
                        bossbulletlist = new List<int[]>() { new int[] { bosscords[0], bosscords[1] }, new int[] { bosscords[0] + bossize.Width, bosscords[1] }, new int[] { bosscords[0], bosscords[1] + bossize.Height }, new int[] { bosscords[0] + bossize.Width, bosscords[1] + bossize.Height } };
                        bullettypes = new List<int> { 1, 3, 7, 5 };
                    }
                    for (int k = 0; k < bossbulletlist.Count(); k++)
                    {
                        Game.enemybossbulletcounter += 1;
                        Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], Game.enemybossbulletcounter, 10, 1005, 20);
                    }
                    break;
                case 1006:
                    if (enemySkills[enemykey][1] == 10) {
                        Enemyobj.enemystats[enemykey][7] = 1;
                    }
                    if (enemySkills[enemykey][1] == 1) {
                        Enemyobj.enemystats[enemykey][7] = 0;
                    }
                    break;
                case 1007:
                    distancex = Enemyobj.enemycords[enemykey][0] - Playerobj.cords[0];
                    distancey = Enemyobj.enemycords[enemykey][1] - Playerobj.cords[1];
                    for (int i = 0; i < 4; i++) {
                        Game.enemycounter += 1;
                        if (distancex >= 0)
                        {
                            if (distancey >= 0)
                            {
                                Enemyobj.CreateEnemy(random.Next(Playerobj.cords[0] + 20, Playerobj.cords[0] + distancex + 10), random.Next(Playerobj.cords[1] + 20, Enemyobj.enemycords[enemykey][1] + 20), Game.enemycounter, 35, new int[] { 0, 0 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(random.Next(Playerobj.cords[0] + 20, Playerobj.cords[0] + distancex + 10), random.Next(Enemyobj.enemycords[enemykey][1] - 20, Playerobj.cords[1] - 20), Game.enemycounter, 35, new int[] { 0, 0 });
                            }
                        }
                        else {
                            if (distancey >= 0)
                            {
                                Enemyobj.CreateEnemy(random.Next(Enemyobj.enemycords[enemykey][0] - 20, Playerobj.cords[0] - 20), random.Next(Playerobj.cords[1] + 20, Enemyobj.enemycords[enemykey][1] + 20), Game.enemycounter, 35, new int[] { 0, 0 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(random.Next(Enemyobj.enemycords[enemykey][0] - 20, Playerobj.cords[0] - 20), random.Next(Enemyobj.enemycords[enemykey][1] - 20, Playerobj.cords[1] - 20), Game.enemycounter, 35, new int[] { 0, 0 });
                            }
                        }
                    }
                    break;
                case 1008:
                    bosscords = Enemyobj.enemycords[Enemyobj.GetBossKey()];
                    bossize = Enemyobj.enemyimgs[Enemyobj.enemystats[Enemyobj.GetBossKey()][1]][0].Size;
                    bullettypes = new List<int> { 2, 4, 6, 0, 1, 3, 7, 5 };
                    bossbulletlist = new List<int[]> { new int[] { bosscords[0] + bossize.Width / 2, bosscords[1] }, new int[] { bosscords[0] + bossize.Width, bosscords[1] + bossize.Height / 2 }, new int[] { bosscords[0] + bossize.Width / 2, bosscords[1] + bossize.Height }, new int[] { bosscords[0], bosscords[1] + bossize.Height / 2 }, new int[] { bosscords[0], bosscords[1] }, new int[] { bosscords[0] + bossize.Width, bosscords[1] }, new int[] { bosscords[0], bosscords[1] + bossize.Height }, new int[] { bosscords[0] + bossize.Width, bosscords[1] + bossize.Height } };
                    for (int k = 0; k < bossbulletlist.Count(); k++)
                    {
                        Game.enemybossbulletcounter += 1;
                        Bulletobj.CreateEnemyBossBullet(bossbulletlist[k][0], bossbulletlist[k][1], bullettypes[k], Game.enemybossbulletcounter, 8, 1008, 50);
                    }
                    break;
                case 1009:
                    if (enemySkills[enemykey][1] == 5)
                    {
                        Game.playerSpeedLimit -= 4;
                        Playerobj.playerstats[2] = 1;
                        Playerobj.playerImg = img.playerimg[3];
                    }
                    if (enemySkills[enemykey][1] == 1)
                    {
                        bool found = false;
                        Game.playerSpeedLimit += 4;
                        Playerobj.playerstats[2] = 0;
                        for (int i = 0; i < Playerobj.playerstats.Length;i++) {
                            if (Playerobj.playerstats[i] == 1) {
                                Playerobj.playerImg = img.playerimg[i+1];
                                found = true;
                                break;
                            }
                        }
                        if (!found) {
                            Playerobj.playerImg = img.playerimg[0];
                        }
                    }
                    break;
                case 1010:
                    if (enemySkills[enemykey][1] == 6)
                        foreach (int bulletkey in Bulletobj.enemybossbulletcords.Keys) {
                        Bulletobj.enemybossbullet[bulletkey][2] += 10;
                    }
                    if (enemySkills[enemykey][1] == 1) { 
                        foreach (int bulletkey in Bulletobj.enemybossbulletcords.Keys)
                        {
                            Bulletobj.enemybossbullet[bulletkey][2] -= 10;
                        }
                    }
                    break;
                case 1011:
                    Game.enemycounter += 1;
                    Enemyobj.CreateEnemy(1, random.Next(30, 820), Game.enemycounter, 56, new int[] { 1, 0 });
                    Game.enemycounter += 1;
                    Enemyobj.CreateEnemy(1, random.Next(30, 820), Game.enemycounter, 56, new int[] { 2, 0 });
                    Game.enemycounter += 1;
                    Enemyobj.CreateEnemy(1, random.Next(30, 820), Game.enemycounter, 56, new int[] { 1, 0 });
                    break;
                case 1012:
                    cords = new int[] { 0, 200, 500, 700, 1000, 1200, 1500};
                    foreach (int cord in cords) {
                        Game.enemybossbulletcounter += 1;
                        Bulletobj.CreateEnemyBossBullet(cord, 0, 6, Game.enemybossbulletcounter, 10, 1012, 100);
                    }
                    break;
                case 1013:
                    cords = new int[] { 70, 270, 470, 670};
                    foreach (int cord in cords)
                    {
                        Game.enemybossbulletcounter += 1;
                        Bulletobj.CreateEnemyBossBullet(0, cord + random.Next(0,100), 4, Game.enemybossbulletcounter, 12, 1013, 120);
                    }
                    break;
                case 1014:
                    cords = new int[] { 0, 153 };
                    Game.enemybossbulletcounter += 1;
                    Bulletobj.CreateEnemyBossBullet(0, cords[random.Next(0, 2)], 4, Game.enemybossbulletcounter, 8, 1014, 200);
                    break;
                case 1015:
                    cords = new int[] { 0, 400, 800, 1200 };
                    if (random.Next(0, 2) == 0) {
                        dirc = 2;
                        ystart = 830;
                    } else {
                        dirc = 6;
                        ystart = 0;
                    }
                    foreach (int cord in cords)
                    {
                        Game.enemybossbulletcounter += 1;
                        Bulletobj.CreateEnemyBossBullet(cord + random.Next(0, 200), ystart, dirc, Game.enemybossbulletcounter, 10, 1015, 150);
                    }
                    break;
                case 1016:
                    Game.enemybossbulletcounter += 1;
                    if (random.Next(0, 2) == 0)
                    {
                        Bulletobj.CreateEnemyBossBullet(0, 0, 6, Game.enemybossbulletcounter, 8, 1016, 20);
                    }
                    else
                    {
                        Bulletobj.CreateEnemyBossBullet(0, 760, 2, Game.enemybossbulletcounter, 8, 1016, 20);
                    }
                    break;
                case 1017:
                    distancex = Enemyobj.enemycords[enemykey][0] - Playerobj.cords[0];
                    distancey = Enemyobj.enemycords[enemykey][1] - Playerobj.cords[1];
                    for (int i = 0; i < 6; i++)
                    {
                        Game.enemycounter += 1;
                        if (distancex >= 0)
                        {
                            if (distancey >= 0)
                            {
                                Enemyobj.CreateEnemy(random.Next(Playerobj.cords[0] + 20, Playerobj.cords[0] + distancex + 10), random.Next(Playerobj.cords[1] + 20, Enemyobj.enemycords[enemykey][1] + 20), Game.enemycounter, 89, new int[] { 1, 0 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(random.Next(Playerobj.cords[0] + 20, Playerobj.cords[0] + distancex + 10), random.Next(Enemyobj.enemycords[enemykey][1] - 20, Playerobj.cords[1] - 20), Game.enemycounter, 89, new int[] { 1, 0 });
                            }
                        }
                        else
                        {
                            if (distancey >= 0)
                            {
                                Enemyobj.CreateEnemy(random.Next(Enemyobj.enemycords[enemykey][0] - 20, Playerobj.cords[0] - 20), random.Next(Playerobj.cords[1] + 20, Enemyobj.enemycords[enemykey][1] + 20), Game.enemycounter, 89, new int[] { 1, 0 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(random.Next(Enemyobj.enemycords[enemykey][0] - 20, Playerobj.cords[0] - 20), random.Next(Enemyobj.enemycords[enemykey][1] - 20, Playerobj.cords[1] - 20), Game.enemycounter, 89, new int[] { 1, 0 });
                            }
                        }
                    }
                    break;
                case 1018:
                    if (enemySkills[enemykey][1] == 3)
                    {
                        Enemyobj.enemystats[enemykey][6] += 5;
                    }
                    if (enemySkills[enemykey][1] == 1)
                    {
                        Enemyobj.enemystats[enemykey][6] -= 5;
                    }
                    break;
                case 1019:
                    Game.enemybossbulletcounter += 1;
                    if (random.Next(0, 2) == 0)
                    {
                        dirc = 6;
                        distancex = Enemyobj.enemycords[enemykey][0];
                        distancey = Enemyobj.enemycords[enemykey][1] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height + 10;
                    }
                    else
                    {
                        dirc = 4;
                        distancex = Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10;
                        distancey = Enemyobj.enemycords[enemykey][1];

                    }
                    Bulletobj.CreateEnemyBossBullet(distancex, distancey, dirc, Game.enemybossbulletcounter, 10, 1019, 150);
                    break;
                case 1020:
                    if (enemySkills[enemykey][1] > 1)
                    {
                        distancex = Enemyobj.enemycords[enemykey][0] - Playerobj.cords[0];
                        distancey = Enemyobj.enemycords[enemykey][1] - Playerobj.cords[1];
                        Game.enemycounter += 1;
                        if (distancex >= 0)
                        {
                            if (distancey >= 0)
                            {
                                Enemyobj.CreateEnemy(random.Next(Playerobj.cords[0] + 20, Playerobj.cords[0] + distancex + 10), random.Next(Playerobj.cords[1] + 20, Enemyobj.enemycords[enemykey][1] + 20), Game.enemycounter, 105, new int[] { 0, 0 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(random.Next(Playerobj.cords[0] + 20, Playerobj.cords[0] + distancex + 10), random.Next(Enemyobj.enemycords[enemykey][1] - 20, Playerobj.cords[1] - 20), Game.enemycounter, 105, new int[] { 0, 0 });
                            }
                        }
                        else
                        {
                            if (distancey >= 0)
                            {
                                Enemyobj.CreateEnemy(random.Next(Enemyobj.enemycords[enemykey][0] - 20, Playerobj.cords[0] - 20), random.Next(Playerobj.cords[1] + 20, Enemyobj.enemycords[enemykey][1] + 20), Game.enemycounter, 105, new int[] { 0, 0 });
                            }
                            else
                            {
                                Enemyobj.CreateEnemy(random.Next(Enemyobj.enemycords[enemykey][0] - 20, Playerobj.cords[0] - 20), random.Next(Enemyobj.enemycords[enemykey][1] - 20, Playerobj.cords[1] - 20), Game.enemycounter, 105, new int[] { 0, 0 });
                            }
                        }
                    }
                    else {
                        List<int> keysRemove = new List<int>();
                        foreach (int ekey in Enemyobj.enemycords.Keys) {
                            if (Enemyobj.enemystats[ekey][1] == 105) {
                                keysRemove.Add(ekey);
                            }
                        }
                        if (keysRemove.Count != 0)
                        {
                            foreach (int key in keysRemove)
                            {
                                Enemyobj.enemycords.Remove(key);
                                Enemyobj.enemystats.Remove(key);
                            }
                            keysRemove.Clear();
                        }
                    }
                    break;
                case 1021:
                    if (enemySkills[enemykey][1] == 4)
                    {
                        canceladv = true;
                        Playerobj.playerstats[3] = 1;
                        Playerobj.playerImg = img.playerimg[4];
                    }
                    if (enemySkills[enemykey][1] == 1)
                    {
                        canceladv = false;
                        Playerobj.playerstats[3] = 0;
                        bool found = false;
                        for (int i = 0; i < Playerobj.playerstats.Length; i++)
                        {
                            if (Playerobj.playerstats[i] == 1)
                            {
                                Playerobj.playerImg = img.playerimg[i + 1];
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            Playerobj.playerImg = img.playerimg[0];
                        }
                    }
                    break;
                case 1:
                    Enemyobj.enemystats[enemykey][3] += 5;
                    break;
                case 2:
                    if (enemySkills[enemykey][1] == 4)
                    {
                        Enemyobj.enemystats[enemykey][4] += 5;
                    }
                    if (enemySkills[enemykey][1] == 1)
                    {
                        Enemyobj.enemystats[enemykey][4] -= 5;
                    }
                    break;
                case 3:
                    if (enemySkills[enemykey][1] == 4)
                    {
                        Enemyobj.enemystats[enemykey][6] += 5;
                    }
                    if (enemySkills[enemykey][1] == 1)
                    {
                        Enemyobj.enemystats[enemykey][6] -= 5;
                    }
                    break;
                case 4:
                    if (enemySkills[enemykey][1] == 4)
                    {
                        int signint = random.Next(0, 2);
                        if (signint == 1)
                        {
                            signint = -1;
                        }
                        else
                        {
                            signint = 1;
                        }
                        Enemyobj.enemydir[enemykey][1] = signint;
                    }
                    break;
                case 5:
                    if (enemySkills[enemykey][1] == 2) {
                        enemytype = Enemyobj.enemystats[enemykey][1];
                        Game.enemybulletcounter += 1;
                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2) - (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2), 0, Game.enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 10 + (level * 2));
                    }
                    break;
                case 6:
                    if (enemySkills[enemykey][1] == 2)
                    {
                        enemytype = Enemyobj.enemystats[enemykey][1];
                        Game.enemybulletcounter += 1;
                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width / 2) - (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width / 2), Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height) + 10, 1, Game.enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 10 + (level * 2));
                    }
                    break;
                case 7:
                    if (enemySkills[enemykey][1] == 3)
                    {
                        Enemyobj.enemystats[enemykey][7] = 1;
                    }
                    if (enemySkills[enemykey][1] == 1)
                    {
                        Enemyobj.enemystats[enemykey][7] = 0;
                    }
                    break;
                case 8:
                    if (enemySkills[enemykey][1] == 2)
                    {
                        enemytype = Enemyobj.enemystats[enemykey][1];
                        bullettype = Enemyobj.enemystats[enemykey][8];
                        Game.enemybulletcounter += 1;
                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width / 2) - (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width / 2), Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height) + 10, 2, Game.enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 10 + (level * 2));
                    }
                    break;
                case 9:
                    enemytype = Enemyobj.enemystats[enemykey][1];
                    Game.enemycounter += 1;
                    Enemyobj.CreateEnemy(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[enemytype][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1], Game.enemycounter, 72 , new int[] { 1, 0 });
                    break;
                case 10:
                    if (enemySkills[enemykey][1] == 2) {
                        enemytype = Enemyobj.enemystats[enemykey][1];
                        Game.enemybulletcounter += 1;
                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height), 3, Game.enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 10 + (level * 2));
                    }
                    break;
                case 11:
                    if (enemySkills[enemykey][1] == 2)
                    {
                        Game.enemycounter += 1;
                        Enemyobj.CreateEnemy(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1], Game.enemycounter, 88, new int[] { 1, 0 });
                    }
                    break;
                case 12:
                    if (enemySkills[enemykey][1] == 2)
                    {
                        enemytype = Enemyobj.enemystats[enemykey][1];
                        Game.enemybulletcounter += 1;
                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2) - (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2), 0, Game.enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 30);
                    }
                    break;
                case 13:
                    if (enemySkills[enemykey][1] == 2)
                    {
                        enemytype = Enemyobj.enemystats[enemykey][1];
                        Game.enemybulletcounter += 1;
                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 10, Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2) - (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2), 0, Game.enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 30);
                        Game.enemybulletcounter += 1;
                        Bulletobj.CreateEnemyBullet(Enemyobj.enemycords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width + 20, Enemyobj.enemycords[enemykey][1] + (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2) - (Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2) + 10, 0, Game.enemybulletcounter, Enemyobj.enemystats[enemykey][4] + 5, Enemyobj.enemystats[enemykey][8], 30);
                    }
                    break;
            }
        }

        public static void UsePlayerSkill(int skillid, int energycon, Imgs img)
        {
            string labeltext = "";
            Label droppickup = new Label();
            if (Playerobj.skillcolides != null && Playerobj.skillcolides.Parent != null) {
                Playerobj.skillcolides.Parent.Controls.Remove(Playerobj.skillcolides);
            }
            Playerobj.skilllabelcounter = 50;
            Playerobj.skillcolides = droppickup;
            Font font = new Font("Cooper Black", 12);
            droppickup.Font = font;
            droppickup.Location = new Point(920, 790);
            droppickup.BackColor = Color.Transparent;
            droppickup.ForeColor = Color.FromArgb(9, 255, 0);
            droppickup.AutoSize = true;
            switch (skillid)
            {
                case 0:
                    Playerobj.HP += (Playerobj.maxHP/50);
                    if (Playerobj.HP > Playerobj.maxHP) {
                        Playerobj.HP = Playerobj.maxHP;
                    }
                    Game.gameboxlist[4].Width = (Playerobj.HP / Game.playerHPRatio);
                    Game.gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                    labeltext = "+" + (Playerobj.maxHP / 50).ToString() +" Durability";
                    break;
                case 1:
                    labeltext = "+5  Fuel";
                    Playerobj.fuel += 5;
                    if (Playerobj.fuel > 100)
                    {
                        Playerobj.fuel = 100;
                    }
                    Game.gameboxlist[3].Width = Playerobj.fuel;
                    break;
                case 2:
                    if (playerSkills[skillid][0] == 10)
                    {
                        Game.playerSpeedLimit += 4;
                        Game.accelerate = 2;
                        labeltext = "+4  Speed";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Speed Boost is Back to Normal!";
                        Game.playerSpeedLimit -= 4;
                        Game.accelerate = 1;
                    }
                    break;
                case 3:
                    labeltext = "+2  Coins";
                    Playerobj.coins += 2;
                    Playerobj.inverntory[0] += 2;
                    break;
                case 4:
                    if (playerSkills[skillid][0] == 5)
                    {
                        labeltext = "Damage Immunity is Active";
                        Playerobj.playerstats[1] = 1;
                        Playerobj.playerImg = img.playerimg[2];
                    }
                    else if (playerSkills[skillid][0] == 1)
                    { 
                        labeltext = "Damage Immunity is Inactive";
                        Playerobj.playerstats[1] = 0;
                        Playerobj.playerImg = img.playerimg[0];
                    }
                    break;
                case 5:
                    if (playerSkills[skillid][0] == 7)
                    {
                        Playerobj.dmgreduc = 0.8;
                        labeltext = "Damage Reduction is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.dmgreduc = 1;
                        labeltext = "Damage Reduction is Inactive";
                    }
                    break;
                case 6:
                    if (playerSkills[skillid][0] == 7)
                    {
                        Playerobj.dmginc = 1.2 ;
                        labeltext = "Damage Up is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.dmginc = 1;
                        labeltext = "Damage Up is Inactive";
                    }
                    break;
                case 7:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 20, 50, 7, 1);
                    if (playerSkills[skillid][0] == 10)
                    {
                        labeltext = "Skyball Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Skyball Skill is Inactive";
                    }
                    break;
                case 8:
                    if (playerSkills[skillid][0] == 20)
                    {
                        Playerobj.maxHP += 100;
                        Playerobj.HP += 100;
                        Game.playerHPRatio = Playerobj.maxHP / 100;
                        Game.gameboxlist[4].Width = (Playerobj.HP / Game.playerHPRatio);
                        Game.gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                        labeltext = "Durability Increase by 100";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.maxHP -= 100;
                        if (Playerobj.HP > Playerobj.maxHP)
                        {
                            Playerobj.HP = Playerobj.maxHP;
                        }
                        Game.playerHPRatio = Playerobj.maxHP / 100;
                        Game.gameboxlist[4].Width = (Playerobj.HP / Game.playerHPRatio);
                        Game.gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                        labeltext = "Durability Decrease by 100";
                    }
                    break;
                case 9:
                    if (playerSkills[skillid][0] == 8)
                    {
                        labeltext = "Fire rate Increased";
                        Playerobj.fireRate += 2;
                        Playerobj.maxFireRate += 2;
                        Game.gamelabellist[6].Text = Playerobj.fireRate.ToString();
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Fire rate Decreased";
                        Playerobj.fireRate -= 2;
                        Playerobj.maxFireRate -= 2;
                        Game.gamelabellist[6].Text = Playerobj.fireRate.ToString();
                    }
                    break;
                case 10:
                    if (playerSkills[skillid][0] == 10)
                    {
                        labeltext = "Criticals Increased";
                        Playerobj.critChance += 20;
                        Playerobj.critDamage += 20;
                        Game.gamelabellist[2].Text = Playerobj.critChance.ToString() + "%";
                        Game.gamelabellist[1].Text = Playerobj.critDamage.ToString() + "%";

                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Criticals Decreased";
                        Playerobj.critChance -= 20;
                        Playerobj.critDamage -= 20;
                        Game.gamelabellist[2].Text = Playerobj.critChance.ToString() + "%";
                        Game.gamelabellist[1].Text = Playerobj.critDamage.ToString() + "%";
                    }
                    break;
                case 11:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 15, 70, 11, 1);
                    if (playerSkills[skillid][0] == 12)
                    {
                        labeltext = "FireBall Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "FireBall Skill is Inactive";
                    }
                    break;
                case 12:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 15, 80, 12, 1);
                    if (playerSkills[skillid][0] == 13)
                    {
                        labeltext = "Energyball Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Energyball Skill is Inactive";
                    }
                    break;
                case 13:
                    if (playerSkills[skillid][0] == 6)
                    {
                        timestop = true;
                        labeltext = "Time Stop Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        timestop = false;
                        Game.enemySpawnCount = 0;
                        labeltext = "Time Stop Skill is Inactive";
                    }
                    break;
                case 14:
                    if (playerSkills[skillid][0] == 30)
                    {
                        Playerobj.maxEnergy += 100;
                        Game.gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy)/ ((double)Playerobj.maxEnergy)));
                        labeltext = "Max Energy Up is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.maxEnergy -= 100;
                        Game.gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                        labeltext = "Max Energy Up is Inactive";
                    }
                    break;
                case 15:
                    if (playerSkills[skillid][0] == 1)
                    {
                        int chooseprize = random.Next(0,4);
                        switch (chooseprize) {
                            case 0:
                                chooseprize = random.Next(5, 21);
                                Playerobj.coins += chooseprize;
                                Playerobj.inverntory[0] += chooseprize;
                                labeltext = "You Got "+ chooseprize.ToString() + " Coins From the gift Skill";
                                break;
                            case 1:
                                chooseprize = random.Next(1, 4);
                                Playerobj.inverntory[1] += chooseprize;
                                labeltext = "You Got " + chooseprize.ToString() + " Parts From the gift Skill";
                                break;
                            case 2:
                                chooseprize = random.Next(1, 4);
                                Playerobj.coins += chooseprize;
                                Playerobj.inverntory[2] += chooseprize;
                                labeltext = "You Got " + chooseprize.ToString() + " Bronze Bar From the gift Skill";
                                break;
                            case 3:
                                chooseprize = random.Next(1, 4);
                                Playerobj.inverntory[3] += chooseprize;
                                labeltext = "You Got " + chooseprize.ToString() + " Iron Bar From the gift Skill";
                                break;
                        }
                        
                    }
                    break;
                case 16:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 10, 100, 16, 50);
                    if (playerSkills[skillid][0] == 12)
                    {
                        labeltext = "Iceball Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Iceball Skill is Inactive";
                    }
                    break;
                case 17:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 13, 120, 17, 60);
                    if (playerSkills[skillid][0] == 12)
                    {
                        labeltext = "Water Stream Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Water Stream is Inactive";
                    }
                    break;
                case 18:
                    Playerobj.HP += (Playerobj.maxHP/25);
                    if (Playerobj.HP > Playerobj.maxHP)
                    {
                        Playerobj.HP = Playerobj.maxHP;
                    }
                    Game.gameboxlist[4].Width = (Playerobj.HP / Game.playerHPRatio);
                    Game.gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                    labeltext = "+" + (Playerobj.maxHP / 25).ToString()+ " Durability";
                    break;
                case 19:
                    labeltext = "+10  Fuel";
                    Playerobj.fuel += 10;
                    if (Playerobj.fuel > 100)
                    {
                        Playerobj.fuel = 100;
                    }
                    Game.gameboxlist[3].Width = Playerobj.fuel;
                    break;
                case 20:
                    if (playerSkills[skillid][0] == 10)
                    {
                        Game.playerSpeedLimit += 8;
                        Game.accelerate = 2;
                        labeltext = "+8  Speed";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Speed Boost is Back to Normal!";
                        Game.playerSpeedLimit -= 8;
                        Game.accelerate = 1;
                    }
                    break;
                case 21:
                    labeltext = "+5  Coins";
                    Playerobj.coins += 5;
                    Playerobj.inverntory[0] += 5;
                    break;
                case 22:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 5, 150, 22, 150);
                    if (playerSkills[skillid][0] == 13)
                    {
                        labeltext = "Poison Mist Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Poison Mist is Inactive";
                    }
                    break;
                case 23:
                    if (playerSkills[skillid][0] == 12)
                    {
                        Playerobj.dmgreduc = 0.6;
                        labeltext = "Adv.Damage Reduction is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.dmgreduc = 1;
                        labeltext = "Adv.Damage Reduction is Inactive";
                    }
                    break;
                case 24:
                    if (playerSkills[skillid][0] == 10)
                    {
                        labeltext = "Adv.Criticals Increased";
                        Playerobj.critChanceTemp = Playerobj.critChance;
                        Playerobj.critDamageTemp = Playerobj.critDamage;
                        Playerobj.critChance += 40;
                        if (Playerobj.critChance > 80) {
                            Playerobj.critChance = 80;
                        }
                        Playerobj.critDamage += 40;
                        Game.gamelabellist[2].Text = Playerobj.critChance.ToString() + "%";
                        Game.gamelabellist[1].Text = Playerobj.critDamage.ToString() + "%";

                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Adv.Criticals Decreased";
                        Playerobj.critChance = Playerobj.critChanceTemp;
                        Playerobj.critDamage = Playerobj.critDamageTemp;
                        Game.gamelabellist[2].Text = Playerobj.critChance.ToString() + "%";
                        Game.gamelabellist[1].Text = Playerobj.critDamage.ToString() + "%";
                    }
                    break;
                case 25:
                    if (playerSkills[skillid][0] == 8)
                    {
                        Playerobj.dmginc = 1.4;
                        labeltext = "Adv.Damage Up is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.dmginc = 1;
                        labeltext = "Adv.Damage Up is Inactive";
                    }
                    break;
                case 26:
                    if (playerSkills[skillid][0] == 7)
                    {
                        Playerobj.skillvamp = 0.2;
                        labeltext = "Durability Vampire is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.skillvamp = 0;
                        labeltext = "Durability Vampire is Inactive";
                    }
                    break;
                case 27:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 8, 200, 27, 500);
                    if (playerSkills[skillid][0] == 10)
                    {
                        labeltext = "Ironball Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Ironball Skill is Inactive";
                    }
                    break;
                case 28:
                    if (playerSkills[skillid][0] == 20)
                    {
                        Playerobj.maxHP += 200;
                        Playerobj.HP += 200;
                        Game.playerHPRatio = Playerobj.maxHP / 100;
                        Game.gameboxlist[4].Width = (Playerobj.HP / Game.playerHPRatio);
                        Game.gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                        labeltext = "Durability Increase by 200";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.maxHP -= 200;
                        if (Playerobj.HP > Playerobj.maxHP) {
                            Playerobj.HP = Playerobj.maxHP;
                        }
                        Game.playerHPRatio = Playerobj.maxHP / 100;
                        Game.gameboxlist[4].Width = (Playerobj.HP / Game.playerHPRatio);
                        Game.gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                        labeltext = "Durability Decrease by 200";
                    }
                    break;
                case 29:
                    if (playerSkills[skillid][0] == 10)
                    {
                        labeltext = "Damage Immunity is Active";
                        Playerobj.playerstats[1] = 1;
                        Playerobj.playerImg = img.playerimg[2];
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Damage Immunity is Inactive";
                        Playerobj.playerstats[1] = 0;
                        Playerobj.playerImg = img.playerimg[0];
                    }
                    break;
                case 30:
                    if (playerSkills[skillid][0] == 8)
                    {
                        labeltext = "Fire rate Increased";
                        Playerobj.fireRate += 4;
                        Playerobj.maxFireRate += 4;
                        Game.gamelabellist[6].Text = Playerobj.fireRate.ToString();
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Fire rate Decreased";
                        Playerobj.fireRate -= 4;
                        Playerobj.maxFireRate -= 4;
                        Game.gamelabellist[6].Text = Playerobj.fireRate.ToString();
                    }
                    break;
                case 31:
                    if (playerSkills[skillid][0] == 8)
                    {
                        anotherbullet = true;
                        labeltext = "Fires Additional Bullet";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        anotherbullet = false;
                        labeltext = "Bullet Amount Back to Normal";
                    }
                    break;
                case 32:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 8, 500, 32, 2000);
                    if (playerSkills[skillid][0] == 10)
                    {
                        labeltext = "Firewall Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Firewall Skill is Inactive";
                    }
                    break;
                case 33:
                    if (playerSkills[skillid][0] == 50)
                    {
                        Playerobj.maxEnergy += 200;
                        Game.gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                        labeltext = "Max Energy Up is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.maxEnergy -= 200;
                        Game.gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                        labeltext = "Max Energy Up is Inactive";
                    }
                    break;
                case 34:
                    if (playerSkills[skillid][0] == 12)
                    {
                        anotherbullet = true;
                        labeltext = "Fires Additional Bullet";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        anotherbullet = false;
                        labeltext = "Bullet Amount Back to Normal";
                    }
                    break;
                case 35:
                    if (playerSkills[skillid][0] == 10)
                    {
                        Playerobj.skillvamp = 0.4;
                        labeltext = "Durability Vampire is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        Playerobj.skillvamp = 0;
                        labeltext = "Durability Vampire is Inactive";
                    }
                    break;
                case 36:
                    if (playerSkills[skillid][0] == 9)
                    {
                        timestop = true;
                        labeltext = "Time Stop Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        timestop = false;
                        Game.enemySpawnCount = 0;
                        labeltext = "Time Stop Skill is Inactive";
                    }
                    break;
                case 37:
                    Game.bulletcounter += 1;
                    Bulletobj.CreateSkillBullet(Playerobj.cords[0] - 35, Playerobj.cords[1] + 17, 0, Game.bulletcounter, 10, 750, 37, 3000);
                    if (playerSkills[skillid][0] == 15)
                    {
                        labeltext = "Ultimate Beam Skill is Active";
                    }
                    else if (playerSkills[skillid][0] == 1)
                    {
                        labeltext = "Ultimate Beam Skill is Inactive";
                    }
                    break;
            }
            droppickup.Text = labeltext;
            droppickup.Parent = Game.ActiveForm.GetChildAtPoint(new Point(droppickup.Location.X, droppickup.Location.Y));
        }

        public static void ChangeSkillImg() {
            if (enemySkills.Count != 0)
            {
                foreach (int enemykey in enemySkills.Keys)
                {
                    if (enemySkills[enemykey][3] % 2 == 0)
                    {
                        enemySkills[enemykey][3] += 1;
                    }
                    else
                    {
                        enemySkills[enemykey][3] -= 1;
                    }
                }
            }
        }

        public static void RemoveSkillBuff(int skillid, Imgs img)
        {
            switch (skillid)
            {
                case 2:
                    Game.playerSpeedLimit -= 4;
                    Game.accelerate = 1;
                    break;
                case 4:
                    Playerobj.playerstats[1] = 0;
                    Playerobj.playerImg = img.playerimg[0];
                    break;
                case 5:
                    Playerobj.dmgreduc = 1;
                    break;
                case 6:
                    Playerobj.dmginc = 1;
                    break;
                case 8:
                    Playerobj.maxHP -= 100;
                    if (Playerobj.HP > Playerobj.maxHP)
                    {
                        Playerobj.HP = Playerobj.maxHP;
                    }
                    Game.playerHPRatio = Playerobj.maxHP / 100;
                    Game.gameboxlist[4].Width = (Playerobj.HP / Game.playerHPRatio);
                    Game.gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                    break;
                case 9:
                    Playerobj.fireRate -= 2;
                    Playerobj.maxFireRate -= 2;
                    Game.gamelabellist[6].Text = Playerobj.fireRate.ToString();
                    break;
                case 10:
                    Playerobj.critChance -= 20;
                    Playerobj.critDamage -= 20;
                    Game.gamelabellist[2].Text = Playerobj.critChance.ToString() + "%";
                    Game.gamelabellist[1].Text = Playerobj.critDamage.ToString() + "%";
                    break;
                case 13:
                    timestop = false;
                    Game.enemySpawnCount = 0;
                    break;
                case 14:
                    Playerobj.maxEnergy -= 100;
                    Game.gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                    break;
                case 20:
                    Game.playerSpeedLimit -= 8;
                    Game.accelerate = 1;
                    break;
                case 23:
                    Playerobj.dmgreduc = 1;
                    break;
                case 24:
                    Playerobj.critChance = Playerobj.critChanceTemp;
                    Playerobj.critDamage = Playerobj.critDamageTemp;
                    Game.gamelabellist[2].Text = Playerobj.critChance.ToString() + "%";
                    Game.gamelabellist[1].Text = Playerobj.critDamage.ToString() + "%";
                    break;
                case 25:
                    Playerobj.dmginc = 1;
                    break;
                case 26:
                    Playerobj.skillvamp = 0;
                    break;
                case 28:
                    Playerobj.maxHP -= 200;
                    if (Playerobj.HP > Playerobj.maxHP)
                    {
                        Playerobj.HP = Playerobj.maxHP;
                    }
                    Game.playerHPRatio = Playerobj.maxHP / 100;
                    Game.gameboxlist[4].Width = (Playerobj.HP / Game.playerHPRatio);
                    Game.gamelabellist[3].Text = Playerobj.HP.ToString() + "/" + Playerobj.maxHP.ToString();
                    break;
                case 29:
                    Playerobj.playerstats[1] = 0;
                    Playerobj.playerImg = img.playerimg[0];
                    break;
                case 30:
                    Playerobj.fireRate -= 4;
                    Playerobj.maxFireRate -= 4;
                    Game.gamelabellist[6].Text = Playerobj.fireRate.ToString();
                    break;
                case 31:
                    anotherbullet = false;
                    break;
                case 33:
                    Playerobj.maxEnergy -= 200;
                    Game.gameboxlist[2].Width = (int)(100 * (((double)Playerobj.energy) / ((double)Playerobj.maxEnergy)));
                    break;
                case 34:
                    anotherbullet = false;
                    break;
                case 35:
                    Playerobj.skillvamp = 0;
                    break;
                case 36:
                    timestop = false;
                    Game.enemySpawnCount = 0;
                    break;
            }
        }

        public static void LoadPlayerSkillImgs(int level)
        {
            int indxs = 3 + (((level - 1) / 3) * 5);
            skilldropimgs.Add(0,Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skilldrops\0.png"));
            skilldropimgs.Add(1, Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skilldrops\1.png"));
            skilldropimgs.Add(2, Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skilldrops\2.png"));

            skillbarimgs.Add(0, Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skills\playerskills\0.png"));
            skillbarimgs.Add(1, Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skills\playerskills\1.png"));
            skillbarimgs.Add(2, Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skills\playerskills\2.png"));

            skillframes.Add(0, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\0.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\0a.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\0b.png")});

            skillframes.Add(1, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\1.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\1a.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\1b.png")});

            skillframes.Add(2, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\2.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\2a.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\2b.png")});

            for (int i = indxs; i < (indxs + 5); i++)
            {
                skilldropimgs.Add(i, Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skilldrops\" + i.ToString() + ".png"));
                skillframes.Add(i, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\" + i.ToString() + ".png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\" + i.ToString() + "a.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skillframe\" + i.ToString() + "b.png")});
                skillbarimgs.Add(i, Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skills\playerskills\" + i.ToString() + ".png"));
                if (i == 7 || i == 11 || i == 12 || i == 16 || i == 17 || i == 22 || i == 27 || i == 32 || i == 37) {
                    Bulletobj.LoadPlayerSkillBulletImgs(i);
                }
            }
        }

        public static void LoadEnemySkillImgs(int skillId) {
            bool keyfound = false;
            foreach (int skillkey in enemyskillframes.Keys) {
                if (skillkey == skillId) {
                    keyfound = true; 
                    break;
                }
            }
            if (!keyfound)
            {
                enemyskillframes.Add(skillId, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skills\enemyskills\" + skillId.ToString() + ".png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\skills\enemyskills\" + skillId.ToString() + "chng.png"),
                });
            }
        }

        public static void ResetSkillobj() {
            skilldropimgs.Clear();
            skillframes.Clear();
            enemySkills.Clear();
            enemyskillframes.Clear();
            skillbarimgs.Clear();
            timestop = false;
            anotherbullet = false;
            canceladv = false;
            playerSkills = new Dictionary<int, int[]> {
            {0 ,new int[]{ 5, 20, 2 } },
            {1 ,new int[]{ 5, 20, 2 } },
            {2 ,new int[]{ 10, 20, 4 } }
            };
        }
    }
}
