using System;
using System.Collections.Generic;
using System.Drawing;

namespace spacebattle
{
    static class Enemyobj
    {
        public static Dictionary<int, int[]> enemycords = new Dictionary<int, int[]>();    // 2 deleted everything move by -1
        public static Dictionary<int, int[]> enemystats = new Dictionary<int, int[]>(); // {key, {frame, type, dmg img frames, hp, speed, bodydmg, defense, damage immnue, bullet indx}}
        public static Dictionary<int, int[]> enemydir = new Dictionary<int, int[]>();
        public static Dictionary<int, List<Image>> enemyimgs = new Dictionary<int, List<Image>>();
        public static List<int> keysRemove = new List<int>();

        private static readonly Random random = new Random();
        public static int[] bossdirc = new int[] { 1, 0 },
            pointer = new int[] { 0, 5, 10, 15, 20, 25, 30, 36, 41, 46, 51, 57, 62, 67, 73, 78, 83, 90, 95, 100, 106 };
        public static int enemyFrame = 0;
        private static bool xmid = false, ymid = false;

        public static Dictionary<int, int[]> enemydata = new Dictionary<int, int[]>(){  // {enemy ID, {speed, hp, bodyDMG, bullet index, skill, defense, damage immnue}}
            { 0, new int []{ 4, 50, 20, -1, 0, 0, 0 } },//blue plate
            { 1, new int []{ 8, 20, 15, -1, 0, 0, 0 } },//lethal plate
            { 2, new int []{ 4, 50, 20, -1, 0 , 0 , 0 } },//alien shuttle
            { 3, new int []{ 8, 20, 15, -1, 0 , 0 , 0 } },//space bee
            { 4, new int []{ 12, 10, 10 , -1, 0 , 0, 0 } },//small plate
            { 5, new int []{ 4, 55, 22, 0, 0 , 0, 0 } },//alien figher
            { 6, new int []{ 6, 25, 17, 1, 0 , 0, 0 } }, //alien destroyer
            { 7, new int []{ 4, 55, 22, 2, 0 , 0, 0 } },//alien carrier
            { 8, new int []{ 8, 25, 17, 3, 0 , 0, 0 } }, //space bike
            { 9, new int []{ 12, 15, 12, -1, 0 , 0, 0 } }, //ball plate
            { 10, new int []{ 4, 60, 24, 4, 0 , 0, 0 } }, //alien searecher
            { 11, new int []{ 6, 30, 19, 5, 0 , 0 , 0 }}, //alien butterfly
            { 12, new int []{ 4, 60, 24, 6, 0 , 0 , 0 } }, //space disk
            { 13, new int []{ 8, 30, 19, 7, 0 , 0 , 0 }}, //space star
            { 14, new int []{ 12, 20, 14, -1, 0 , 0, 0 } }, //alient cookie
            { 15, new int []{ 4, 65, 26, 8, 0 , 0 , 0 } }, //grey tank
            { 16, new int []{ 8, 35, 21, 9, 0 , 0, 0 } }, // grey heli
            { 17, new int []{ 4, 65, 26, 10, 0 , 0, 0 } }, //ghost ship
            { 18, new int []{ 6, 35, 21, 11, 0 , 0, 0 } }, // grey chariot
            { 19, new int []{ 4, 25, 16, -1, 0 , 0, 0 } }, //grey intercepter
            { 20, new int []{ 4, 70, 28, 12, 0 , 0, 0 } }, //flying sub
            { 21, new int []{ 8, 40, 23, 13, 0 , 0, 0 } }, //war wagon
            { 22, new int []{ 4, 70, 28, 14, 0 , 0, 0 } }, //grey jet
            { 23, new int []{ 8, 40, 23, 15, 0 , 0, 0 } }, //grey mob
            { 24, new int []{ 4, 30, 18, -1, 0 , 0, 0 } }, //space worm
            { 25, new int []{ 4, 75, 30, 16, 0 , 0, 0 } }, //grey cracker
            { 26, new int []{ 8, 45, 25, 17, 0 , 0, 0 } }, //grey scout
            { 27, new int []{ 4, 75, 30, 18, 0 , 0, 0 } }, //grey eye
            { 28, new int []{ 8, 45, 25, 19, 0 , 0, 0 } }, //grey cross
            { 29, new int []{ 12, 35, 20, -1, 0 , 0, 0 } }, // space fly
            { 30, new int []{ 4, 80, 32, 20, 1 , 0, 0 } }, //lazer ship
            { 31, new int []{ 6, 50, 27, 21, 1 , 0, 0 } }, //submarine
            { 32, new int []{ 4, 80, 32, 22, 0 , 0, 0 } }, //small clam
            { 33, new int []{ 6, 50, 27, 23, 0 , 0, 0 } }, //canon snail
            { 34, new int []{ 12, 40, 22, -1, 0 , 0, 0 } }, // pirana
            { 35, new int []{ 0, 20, 11, -1, 0 , 0, 0 } },  // sea urchine
            { 36, new int []{ 4, 85, 34, 24, 2 , 0, 0 } }, // sea egg
            { 37, new int []{ 6, 55, 29, 25, 3 , 0 , 0 } }, // poison frog
            { 38, new int []{ 4, 85, 34, 26, 2 , 0, 0 } }, // ali
            { 39, new int []{ 6, 35, 29, 27, 3 , 0, 0 } }, // sea ghost
            { 40, new int []{ 4, 45, 24, -1, 0 , 0, 0 } },  // sea snake
            { 41, new int []{ 8, 90, 36, 28, 1 , 0, 0 } }, // sea warper
            { 42, new int []{ 6, 60, 31, 29, 2 , 0, 0 } }, // sea whale
            { 43, new int []{ 6, 90, 36, 30, 3 , 0, 0 } }, // battle turtle
            { 44, new int []{ 4, 60, 31, 31, 2 , 0, 0 } }, // sea star
            { 45, new int []{ 4, 50, 26, -1, 0 , 0, 0 } },  // sea jellyfish
            { 46, new int []{ 6, 95, 38, 32, 5 , 0, 0 } }, // poison beaker
            { 47, new int []{ 6, 65, 33, 33, 4 , 0, 0 } }, // poison flask
            { 48, new int []{ 6, 95, 38, 34, 5 , 0, 0 } }, // cyclic chem
            { 49, new int []{ 4, 65, 33, 35, 3 , 0, 0 } }, // pill
            { 50, new int []{ 4, 55, 28, 36, 0 , 0, 0 } },  // poison tube
            { 51, new int []{ 6, 100, 40, 37, 3 , 0, 0 } }, // atom
            { 52, new int []{ 6, 70, 35, 38, 4 , 0, 0 } }, // cubestruct
            { 53, new int []{ 6, 100, 40, 39, 2 , 0, 0 } }, // fire
            { 54, new int []{ 4, 70, 35, 40, 5 , 0, 0 } }, // proton
            { 55, new int []{ 4, 60, 30, 41, 0 , 0, 0 } },  // weight
            { 56, new int []{ 6, 120, 50, -1, 0 , 0, 0 } },  // boiling water
            { 57, new int []{ 6, 105, 42, 42, 3 , 0, 0 } }, // chlorine
            { 58, new int []{ 6, 75, 37, 43, 6 , 0, 0 } }, // labcoat
            { 59, new int []{ 6, 105, 42, 44, 7 , 0, 0 } }, // chembook
            { 60, new int []{ 4, 75, 37, 45, 6 , 0, 0 } }, // reactsurface
            { 61, new int []{ 4, 65, 32, 46, 0 , 0, 0 } },  // chemplate
            { 62, new int []{ 6, 115, 44, 47, 2 , 0, 0 } }, // membrane
            { 63, new int []{ 6, 85, 39, 48, 5 , 0, 0 } }, // neuron
            { 64, new int []{ 6, 115, 44, 49, 7 , 0, 0 } }, // small dna
            { 65, new int []{ 4, 85, 39, 50, 6 , 0, 0 } }, // motor protein
            { 66, new int []{ 4, 75, 34, 51, 1 , 0, 0 } },  // antigen
            { 67, new int []{ 4, 125, 50, 52, 5 , 0, 0 } },  // nematode
            { 68, new int []{ 6, 95, 45, 53, 3 , 0, 0 } }, // bloodcell
            { 69, new int []{ 6, 125, 50, 54, 8 , 0, 0 } }, // arechaea
            { 70, new int []{ 6, 95, 45, 55, 9 , 0, 0 } }, // coronavirus
            { 71, new int []{ 4, 85, 40, 56, 1 , 0, 0 } }, // phage
            { 72, new int []{ 6, 30, 15, -1, 0 , 0, 0 } },  // biosupport
            { 73, new int []{ 6, 135, 60, 57, 5 , 0, 0 } }, // ATP
            { 74, new int []{ 6, 105, 55, 58, 8 , 0, 0 } }, // desmosome
            { 75, new int []{ 6, 135, 60, 59, 9 , 0, 0 } }, // muscle
            { 76, new int []{ 4, 105, 55, 60, 5 , 0, 0 } }, // epithelium
            { 77, new int []{ 6, 95, 50, 61, 7 , 0, 0 } },  // white blood cell
            { 78, new int []{ 6, 50, 5, 62, 3 , 0, 0 } }, // red guard
            { 79, new int []{ 6, 40, 3, 63, 4 , 0, 0 } }, // smoggy
            { 80, new int []{ 6, 50, 5, 64, 2 , 0, 0 } }, // lava rock
            { 81, new int []{ 4, 40, 3, 65, 1 , 0, 0 } }, // lava ball
            { 82, new int []{ 6, 30, 2, 66, 7 , 0, 0 } },  // rect eye
            { 83, new int []{ 6, 80, 10, 67, 11 , 0, 0 } },  // double eye
            { 84, new int []{ 6, 70, 8, 68, 10 , 0, 0 } }, // lava spaceship
            { 85, new int []{ 6, 80, 10, 69, 11 , 0, 0 } }, // lava disk
            { 86, new int []{ 6, 70, 8, 70, 10 , 0, 0 } }, // lava snake
            { 87, new int []{ 4, 50, 5, 71, 2 , 0, 0 } }, // lava vert
            { 88, new int []{ 9, 20, 3, -1, 0 , 0, 0 } },  // lava ghost
            { 89, new int []{ 8, 100, 60, -1, 0 , 0, 0 } },  // meteor
            { 90, new int []{ 6, 90, 25, 72, 3 , 0, 0 } },  // lava star
            { 91, new int []{ 6, 80, 15, 73, 10 , 0, 0 } }, // lava mosq
            { 92, new int []{ 6, 90, 25, 74, 4 , 0, 0 } }, // lava chopper
            { 93, new int []{ 6, 80, 15, 75, 10 , 0, 0 } }, // lava doser
            { 94, new int []{ 7, 60, 10, 76, 2 , 0, 0 } }, // lava capsule
            { 95, new int []{ 6, 90, 60, 77, 9 , 0, 0 } },  // mud ghost
            { 96, new int []{ 6, 80, 40, 78, 10 , 0, 0 } }, // trash
            { 97, new int []{ 6, 90, 60, 79, 9 , 0, 0 } }, // mush box
            { 98, new int []{ 6, 80, 40, 80, 10 , 0, 0 } }, // quad eye
            { 99, new int []{ 7, 60, 35, 81, 3 , 0, 0 } }, // flat worm
            { 100, new int []{ 12, 95, 70, 82, 12 , 0, 0 } }, // mud chopper
            { 101, new int []{ 13, 95, 70, 83, 13 , 0, 0 } },  // rusty can
            { 102, new int []{ 12, 95, 70, 84, 12 , 0, 0 } }, // rusty cannon
            { 103, new int []{ 13, 95, 70, 85, 13 , 0, 0 } }, // microbium
            { 104, new int []{ 10, 70, 70, 86, 10 , 0, 0 } }, // mud ballon
            { 105, new int []{ 0, 1000000, 50, -1, 0 , 0, 0 } }, // frog block
            { 106, new int []{ 8, 100, 80, 87, 12 , 0, 0 } }, // mud pile
            { 107, new int []{ 8, 95, 80, 88, 13 , 0, 0 } },  // marsh bubble
            { 108, new int []{ 8, 100, 80, 89, 12 , 0, 0 } }, // mud tank
            { 109, new int []{ 8, 95, 80, 90, 13 , 0, 0 } }, // rusty flower
            { 110, new int []{ 8, 80, 80, 91, 12 , 0, 0 } }, // rusty jet
            { 1000, new int []{ 10, 1000, 50, 0, 0 , 0, 0 } }, //plate boss
            { 1001, new int []{ 8, 2000, 60, 1, 0 , 0, 0 } }, //multilayer boss
            { 1002, new int []{ 6, 1500, 70 , 2, 0 , 0, 0 } }, // first layer boss
            { 1003, new int []{ 6, 2000, 90 , 3, 0 , 0, 0 } },// second layer boss
            { 1004, new int []{ 12, 5000, 110 , 4, 1004, 0, 0 } }, // grey teleporter
            { 1005, new int []{ 8, 7000, 120 , 5, 1005, 0, 0 } }, //grey balls
            { 1006, new int []{ 6, 9000, 130 , 6, 1006, 0, 0 } }, // grey ghost
            { 1007, new int []{ 6, 11500, 140 , 7, 1007, 0, 0 } }, //sea jar
            { 1008, new int []{ 7, 14000, 160 , 8, 1008, 0, 0 } }, // green fish
            { 1009, new int []{ 8, 17000, 180 , 9, 1009, 0, 0 } }, // boss ship
            { 1010, new int []{ 9, 19000, 220 , 10, 1010, 0, 0 } }, // acid boss
            { 1011, new int []{ 8, 23000, 260 , 11, 1011, 0, 0 } }, // base boss
            { 1012, new int []{ 8, 27500, 310 , 12, 1012, 0, 0 } }, // diswater boss
            { 1013, new int []{ 7, 40000, 360 , 13, 1013, 0, 0 } }, // microbe boss
            { 1014, new int []{ 6, 70000, 410 , 14, 1014, 0, 0 } }, // sponge boss
            { 1015, new int []{ 5, 100000, 470 , 15, 1015, 0, 0 } }, // stem cell boss
            { 1016, new int []{ 5, 1000, 10 , 16, 1016, 0, 0 } }, // lava boss
            { 1017, new int []{ 6, 50000, 50 , 17, 1017, 0, 0 } }, // lava devil
            { 1018, new int []{ 10, 60000, 60 , 18, 1018, 0, 0 } }, // lava duck
            { 1019, new int []{ 8, 70000, 60 , 19, 1019, 0, 0 } }, // mud ship
            { 1020, new int []{ 8, 80000, 70 , 20, 1020, 0, 0 } }, // crazy frog
            { 1021, new int []{ 8, 10000000, 50 , 21, 1021, 0, 0 } } // mud car
        };

        public static void CreateEnemy(int cordx, int cordy, int enemyNumber, int etype, int[] dirc)
        {
            enemycords.Add(enemyNumber, new int[] { cordx, cordy });
            int speed;
            if (etype == 6 || etype == 7 || etype == 8) {
               speed = random.Next(enemydata[etype][0], enemydata[etype][0] * 2);
            } else {
                speed = enemydata[etype][0];
            }
            if (enemydata[etype][4] != 0) { // add enemyskill
                Skillobj.AddEnemySkill(enemyNumber, enemydata[etype][4]);
            }
            enemystats.Add(enemyNumber, new int[] { 0, etype, -1, enemydata[etype][1], speed, enemydata[etype][2], enemydata[etype][5], enemydata[etype][6], enemydata[etype][3] });
            if (etype < 1000) {
                enemydir.Add(enemyNumber, dirc);
            }
        }

        public static void MoveEnemy(int mainwidth, int mainheight, int gamelevel, Imgs img)
        {
            foreach (int enemykey in enemycords.Keys)
            {
                if (enemystats[enemykey][0] == 3)
                {
                    enemycords[enemykey][1] = enemycords[enemykey][1] + 10;
                }
                else
                {
                    if (enemystats[enemykey][1] >= 1000)
                    {
                        EnemyBossMove(enemykey);
                    }
                    else
                    {
                        enemycords[enemykey][0] += enemystats[enemykey][4] * enemydir[enemykey][0];
                        enemycords[enemykey][1] += enemystats[enemykey][4] * enemydir[enemykey][1];
                        ChangeDircByType(gamelevel, enemykey, mainwidth);//enemycords[enemykey][0] = enemycords[enemykey][0] + enemyspeed[enemykey];
                    }
                }
                if ((enemystats[enemykey][1] < 1000) || ((enemystats[enemykey][1] >= 1000) && enemystats[enemykey][0] == 3))
                {
                    if (enemycords[enemykey][0] > 1390 || enemycords[enemykey][1] > 750 || enemycords[enemykey][1] < 0)               //frame boundries removes enemy
                    {
                        keysRemove.Add(enemykey);
                        if ((enemystats[enemykey][1] >= 1000) && enemystats[enemykey][0] == 3)
                        {
                            Bulletobj.enemybossbulletcords.Clear();
                            Bulletobj.enemybossbullet.Clear();
                        }
                    }
                }
                else  // change boss flying direction based on boundries reached
                {
                    EnemyBossDirect(enemykey, gamelevel, mainwidth, mainheight, img);
                }
                if (enemystats[enemykey][2] >= 0)
                {
                    enemystats[enemykey][2] += 1;
                }
                if (enemystats[enemykey][2] == 7)
                {
                    SetEnemyFrame(0, enemykey);
                    enemystats[enemykey][2] = -1;
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    enemycords.Remove(key);
                    enemystats.Remove(key);
                    Skillobj.RemoveEnemySkill(key);
                }
                keysRemove.Clear();
            }
        }

        public static void SetEnemyFrame(int frame, int key)
        {
            if (enemystats[key][0] != 3)
            {
                enemystats[key][0] = frame;
                if (frame == 1 || frame == 2)
                {
                    enemystats[key][2] = 0;
                }
                else if (frame == 3)
                {
                    enemystats[key][2] = -1;
                }
            }
        }

        public static void EnemyBossMove(int enemykey) {
            if (enemystats[enemykey][1] > 1003)//
            {
                if (Skillobj.enemySkills[enemykey][1] == 0)
                {
                    enemycords[enemykey][0] += bossdirc[0] * enemystats[enemykey][4];
                    enemycords[enemykey][1] += bossdirc[1] * enemystats[enemykey][4];
                }
            }
            else {
                enemycords[enemykey][0] += bossdirc[0] * enemystats[enemykey][4];
                enemycords[enemykey][1] += bossdirc[1] * enemystats[enemykey][4];
            }
        }

        public static void EnemyBossDirect(int enemykey, int levelboss, int mainwidth, int mainheight, Imgs img)
        {
            int[] signandnum = GetsignAndNum();
            {
                switch (levelboss)
                {
                    case 1:
                        if (enemycords[enemykey][0] > (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = random.Next(0, 2);

                        }
                        if (enemycords[enemykey][1] > (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = random.Next(0, 2);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = random.Next(0, 2);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = random.Next(0, 2);
                        }
                        break;
                    case 2:
                        if (enemycords[enemykey][0] > (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1];
                        }
                        if (enemycords[enemykey][1] > (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1];
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            xmid = false;
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1];

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            ymid = false;
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1];
                        }
                        if (enemycords[enemykey][0] < (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width) && enemycords[enemykey][0] > (mainwidth / 2) && !xmid) {
                            xmid = true;
                            bossdirc[1] = signandnum[0] * signandnum[1];
                            signandnum = GetsignAndNum();
                            bossdirc[0] = 1 * signandnum[1];
                        }
                        if (enemycords[enemykey][1] < (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height) && enemycords[enemykey][1] > (mainheight / 2) && !ymid)
                        {
                            ymid = true;
                            bossdirc[1] = 1 * signandnum[1];
                            signandnum = GetsignAndNum();
                            bossdirc[0] = signandnum[0] * signandnum[1];
                        }
                        break;
                    case 3:
                        if (enemycords[enemykey][0] > (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                        }
                        if (enemycords[enemykey][1] > (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            xmid = false;
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(1, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            ymid = false;
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                        }
                        if (enemycords[enemykey][0] < (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width) && enemycords[enemykey][0] > (mainwidth / 2) && !xmid)
                        {
                            xmid = true;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                            signandnum = GetsignAndNum();
                            bossdirc[0] = signandnum[1] * random.Next(1, 3);
                        }
                        if (enemycords[enemykey][1] < (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height) && enemycords[enemykey][1] > (mainheight / 2) && !ymid)
                        {
                            ymid = true;
                            bossdirc[1] = signandnum[1] * random.Next(1, 3);
                            signandnum = GetsignAndNum();
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                        }
                        break;
                    case 4:
                        if (enemycords[enemykey][0] > (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = random.Next(0, 2);

                        }
                        if (enemycords[enemykey][1] > (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = random.Next(0, 2);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = random.Next(0, 2);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = random.Next(0, 2);
                        }
                        break;
                    case 5:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = random.Next(0, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = random.Next(0, 3);
                        }
                        break;
                    case 6:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        break;
                    case 7:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        break;
                    case 8:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        break;
                    case 9:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        break;
                    case 10:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                        }
                        break;
                    case 11:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                        }
                        break;
                    case 12:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                        }
                        break;
                    case 13:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                        }
                        break;
                    case 14:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                        }
                        break;
                    case 15:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        break;
                    case 16:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                        }
                        break;
                    case 17:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        break;
                    case 18:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                        }
                        break;
                    case 19:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(0, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(0, 3);
                        }
                        break;
                    case 20:
                        if (enemycords[enemykey][0] > (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                        }
                        if (enemycords[enemykey][1] > (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            xmid = false;
                            bossdirc[0] = 1;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(1, 3);

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            ymid = false;
                            bossdirc[1] = 1;
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                        }
                        if (enemycords[enemykey][0] < (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width) && enemycords[enemykey][0] > (mainwidth / 2) && !xmid)
                        {
                            xmid = true;
                            bossdirc[1] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                            signandnum = GetsignAndNum();
                            bossdirc[0] = signandnum[1] * random.Next(1, 3);
                        }
                        if (enemycords[enemykey][1] < (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height) && enemycords[enemykey][1] > (mainheight / 2) && !ymid)
                        {
                            ymid = true;
                            bossdirc[1] = signandnum[1] * random.Next(1, 3);
                            signandnum = GetsignAndNum();
                            bossdirc[0] = signandnum[0] * signandnum[1] * random.Next(1, 3);
                        }
                        break;
                    case 21:
                        if (enemycords[enemykey][0] >= (mainwidth - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Width))
                        {
                            bossdirc[0] = -1;
                            
                        }
                        if (enemycords[enemykey][1] >= (mainheight - enemyimgs[enemystats[enemykey][1]][enemystats[enemykey][0]].Height))
                        {
                            bossdirc[1] = -1;
                        }
                        if (enemycords[enemykey][0] <= 10)
                        {
                            bossdirc[0] = 1;

                        }
                        if (enemycords[enemykey][1] <= 50)
                        {
                            bossdirc[1] = 1;
                        }
                        break;
                }
            }
        }

        public static void ResetEnemyObj()
        {
            enemycords.Clear();
            enemystats.Clear();
            enemydir.Clear();
            enemyimgs.Clear();
            enemyFrame = 0;
            xmid = false;
            ymid = false;
            bossdirc = new int[] { 1, 0 };
        }

        public static int[] GetsignAndNum()
        {
            int zeroOrOne = random.Next(0, 2);
            int signint = random.Next(0, 2);
            if (signint == 1)
            {
                signint = -1;
            }
            else
            {
                signint = 1;
            }
            return new int[] { zeroOrOne, signint };
        }

        public static void ChangeDircByType(int gamelevel, int enemykey, int mainwidth) {
            switch (gamelevel)
            {
                case 8:
                    if ((enemystats[enemykey][1] == 39 || enemystats[enemykey][1] == 37) && enemycords[enemykey][0] > (mainwidth / 2)) {
                        enemydir[enemykey][1] = 1;
                    }
                    if (enemystats[enemykey][1] == 38 && enemycords[enemykey][0] > (mainwidth / 2))
                    {
                        enemydir[enemykey][1] = -1;
                    }
                    break;
                case 9:
                    if ((enemystats[enemykey][1] == 42 || enemystats[enemykey][1] == 44) && enemycords[enemykey][0] > (mainwidth / 2))
                    {
                        enemydir[enemykey][1] = 1;
                    }
                    if (enemystats[enemykey][1] == 43 && enemycords[enemykey][0] > (mainwidth / 2))
                    {
                        enemydir[enemykey][1] = -1;
                    }
                    break;
                case 11:
                    if (enemystats[enemykey][1] == 53 && enemycords[enemykey][0] > (mainwidth / 2))
                    {
                        enemydir[enemykey][1] = 1;
                    }
                    if (enemystats[enemykey][1] == 54 && enemycords[enemykey][0] > (mainwidth / 2))
                    {
                        enemydir[enemykey][1] = -1;
                    }
                    break;
                case 14:
                    if (enemystats[enemykey][1] == 69 && enemycords[enemykey][0] > (mainwidth / 2))
                    {
                        enemydir[enemykey][1] = 1;
                    }
                    if (enemystats[enemykey][1] == 70 && enemycords[enemykey][0] > (mainwidth / 2))
                    {
                        enemydir[enemykey][1] = -1;
                    }
                    break;
            }

        }

        public static void LoadEnemyImgs(int level) {
            List<Image> eneimgs = new List<Image>();
            for (int  i = pointer[level-1]; i < (pointer[level - 1]+5); i++) {
                enemyimgs.Add(i, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\" + i.ToString() + ".png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\" + i.ToString() + "dmg.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\" + i.ToString() + "critdmg.png"),
                Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\" + i.ToString() + "dead.png")}) ;
                if (enemydata[i][4] != 0) {
                    Skillobj.LoadEnemySkillImgs(enemydata[i][4]);
                }
                if (enemydata[i][3] != -1) {
                    Bulletobj.LoadEnemyBulletImgs(i, enemydata[i][3]);
                }
                if (level == 21) {
                    Bulletobj.LoadBeamSkillBulletImgs();
                }
            }
            switch (level) {
                case 7:
                    enemyimgs.Add(35, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\35.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\35dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\35critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\35dead.png")});
                    break;
                case 11:
                    enemyimgs.Add(56, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\56.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\56dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\56critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\56dead.png")});
                    break;
                case 14:
                    enemyimgs.Add(72, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\72.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\72dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\72critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\72dead.png")});
                    break;
                case 15:
                    enemyimgs.Add(72, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\72.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\72dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\72critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\72dead.png")});
                    break;
                case 17:
                    enemyimgs.Add(88, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\88.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\88dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\88critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\88dead.png")});
                    enemyimgs.Add(89, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\89.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\89dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\89critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\89dead.png")});
                    break;
                case 20:
                    enemyimgs.Add(105, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\105.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\105dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\105critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\105dead.png")});
                    break;
            }
            switch (level)
            {
                case 1:
                    enemyimgs.Add(1000, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1000.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1000dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1000critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1000dead.png")});
                    Bulletobj.LoadEnemyBulletImgs(1000, enemydata[1000][3]);
                    break;
                case 2:
                    enemyimgs.Add(1001, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1001.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1001dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1001critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1001dead.png")});
                    Bulletobj.LoadEnemyBulletImgs(1001, enemydata[1001][3]);
                    break;
                case 3:
                    enemyimgs.Add(1002, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1002.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1002dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1002critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1002dead.png")});
                    Bulletobj.LoadEnemyBulletImgs(1002, enemydata[1002][3]);
                    enemyimgs.Add(1003, new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1003.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1003dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1003critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\1003dead.png")});
                    Bulletobj.LoadEnemyBulletImgs(1003, enemydata[1003][3]);
                    break;
                default:
                    enemyimgs.Add((level + 1000), new List<Image>() { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\" + (level + 1000).ToString() + ".png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\" + (level + 1000).ToString() + "dmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\" + (level + 1000).ToString() + "critdmg.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\enemies\" + (level + 1000).ToString() + "dead.png")});
                    Skillobj.LoadEnemySkillImgs(enemydata[level + 1000][4]);
                    Bulletobj.LoadEnemyBulletImgs((level + 1000), enemydata[(level + 1000)][3]);
                    break;
            }
        }

        public static int GetBossKey()
        {
            foreach (int enemykey in enemystats.Keys) {
                if (enemystats[enemykey][1] >= 1000) {
                    return enemykey;
                }
            }
            return -1;
        }

    }
}
