using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace spacebattle
{
    static class Bulletobj
    {
        public static Dictionary<int, int[]> bulletcords = new Dictionary<int, int[]>();
        public static Dictionary<int, int> bulletypes = new Dictionary<int, int>();
        public static Dictionary<int, int> bulletbounces = new Dictionary<int, int>();

        public static Dictionary<int, int[]> enemybulletcords = new Dictionary<int, int[]>();
        public static Dictionary<int, int[]> enemybullet = new Dictionary<int, int[]>(); // {key, {type, speed, dmg, frame, img index}}
        public static Dictionary<int, List<Image>> enemybulletimgs = new Dictionary<int, List<Image>>();

        public static Dictionary<int, int[]> enemybossbulletcords = new Dictionary<int, int[]>();
        public static Dictionary<int, int[]> enemybossbullet = new Dictionary<int, int[]>();   // {key, {type, speed, dmg, frame, img index}}
        public static Dictionary<int, List<Image>> enemybossbulletimgs = new Dictionary<int, List<Image>>();

        public static Dictionary<int, int[]> skillbulletcords = new Dictionary<int, int[]>();
        public static Dictionary<int, int[]> skillbullet = new Dictionary<int, int[]>(); // {key, {type, speed, dmg, frame, hp, img indx}}
        public static Dictionary<int, List<Image>> playerskillbulletimgs = new Dictionary<int, List<Image>>();

        private static readonly int[] missileYcords = {20, 109, 198, 287, 376, 475, 564, 653, 750}, bosswithskillbullets = { 1005, 1008, 1012, 1013, 1014, 1015, 1016, 1019};

        public static int bulletSpeed = 20;
        public static int bulletDamage = 20 ;
        public static int bulletLevel = 1;
        public static int guntype = 0; // (0) bulllet, (3) bounce bullets, (1) pulse, (2) scattered pulse, (4) canonball, (5) missile, (6) guided missile, (7) particle gun, (8) death drone, (9) replicating bullet gun (10) proton gun
        private static readonly Random random = new Random();

        public static void CreateBullet(int cordx, int cordy, int bulletype, int bulletNumber)
        {
            int bulletgun = bulletype + guntype;
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletgun);
            if (bulletgun == 4 || bulletgun == 5)
            {
                bulletbounces.Add(bulletNumber, 1);
            }

        }

            public static void MoveBullet()
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletcords.Keys)
            {
                if (bulletypes[bulletkey] == (1 + guntype))  //flipped bullets will fly vertically in negative direction 
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == (2 + guntype))  //flipped bullets will fly vertically in positive direction 
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < -50 || bulletcords[bulletkey][1] > 810)                            //remove bullet when reached frame boundries
                {
                    if (guntype == 3 && bulletbounces.Count != 0 && (bulletypes[bulletkey] == (1 + guntype) || bulletypes[bulletkey] == (2 + guntype)))                             // checking for bounce gun
                    {
                        if (bulletbounces[bulletkey] == 1)
                        {
                            bulletbounces[bulletkey] -= 1;
                            if (bulletypes[bulletkey] == (1 + guntype))      // if bullet reached frame border flip it once
                            {
                                bulletypes[bulletkey] = (2 + guntype);
                            }
                            else
                            {
                                bulletypes[bulletkey] = (1 + guntype);
                            }
                        }
                        else
                        {
                            keysRemove.Add(bulletkey);
                        }
                    }
                    else
                    {
                        keysRemove.Add(bulletkey);
                    }
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                    bulletbounces.Remove(key);
                }
                keysRemove.Clear();
            }
        }
        public static void CreateEnemyBossBullet(int cordx, int cordy, int bulletype, int bulletNumber, int speed,int bulletimg, int damage)
        {
            enemybossbulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            enemybossbullet.Add(bulletNumber, new int[] { bulletype, speed, damage, 0 , bulletimg});
        }

        public static void MoveEnemyBossBullet()
        {
            List<int> keysRemove = new List<int>();
            if (enemybossbulletcords.Count != 0) {
                foreach (int enemybossbulletkey in enemybossbulletcords.Keys)
                {
                    switch (enemybossbullet[enemybossbulletkey][0])
                    {
                        case 0:
                            enemybossbulletcords[enemybossbulletkey][0] -= enemybossbullet[enemybossbulletkey][1];
                            break;
                        case 1:
                            enemybossbulletcords[enemybossbulletkey][0] -= enemybossbullet[enemybossbulletkey][1];
                            enemybossbulletcords[enemybossbulletkey][1] -= enemybossbullet[enemybossbulletkey][1];
                            break;
                        case 2:
                            enemybossbulletcords[enemybossbulletkey][1] -= enemybossbullet[enemybossbulletkey][1];
                            break;
                        case 3:
                            enemybossbulletcords[enemybossbulletkey][0] += enemybossbullet[enemybossbulletkey][1];
                            enemybossbulletcords[enemybossbulletkey][1] -= enemybossbullet[enemybossbulletkey][1];
                            break;
                        case 4:
                            enemybossbulletcords[enemybossbulletkey][0] += enemybossbullet[enemybossbulletkey][1];
                            break;
                        case 5:
                            enemybossbulletcords[enemybossbulletkey][0] += enemybossbullet[enemybossbulletkey][1];
                            enemybossbulletcords[enemybossbulletkey][1] += enemybossbullet[enemybossbulletkey][1];
                            break;
                        case 6:
                            enemybossbulletcords[enemybossbulletkey][1] += enemybossbullet[enemybossbulletkey][1];
                            break;
                        case 7:
                            enemybossbulletcords[enemybossbulletkey][0] -= enemybossbullet[enemybossbulletkey][1];
                            enemybossbulletcords[enemybossbulletkey][1] += enemybossbullet[enemybossbulletkey][1];
                            break;
                    }
                    if (enemybossbulletcords[enemybossbulletkey][0] < 0 || enemybossbulletcords[enemybossbulletkey][1] < 0 || enemybossbulletcords[enemybossbulletkey][1] > 780 || enemybossbulletcords[enemybossbulletkey][0] > 1400 || ((enemybossbullet[enemybossbulletkey][3] == 46 || enemybossbullet[enemybossbulletkey][3] == 47) && enemybossbulletcords[enemybossbulletkey][1] < 420) || ((enemybossbullet[enemybossbulletkey][3] == 48 || enemybossbullet[enemybossbulletkey][3] == 49) && enemybossbulletcords[enemybossbulletkey][1] > 420))                            //remove pulse when reached frame boundries
                    {
                        keysRemove.Add(enemybossbulletkey);
                    }
                }
                if (keysRemove.Count != 0)
                {
                    foreach (int key in keysRemove)
                    {
                        enemybossbulletcords.Remove(key);
                        enemybossbullet.Remove(key);
                    }
                    keysRemove.Clear();
                }
            }
        }

        public static void CreateEnemyBullet(int cordx, int cordy, int bulletype, int bulletNumber, int speed, int enemybtype, int damage)
        {
            enemybulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            enemybullet.Add(bulletNumber, new int[] { bulletype, speed, damage, 0 , enemybtype });
        }

        public static void MoveEnemyBullet()
        {
            List<int> keysRemove = new List<int>();
            if (enemybulletcords.Count != 0)
            {
                foreach (int enemybulletkey in enemybulletcords.Keys)
                {
                    switch (enemybullet[enemybulletkey][0])
                    {
                        case 1:
                            enemybulletcords[enemybulletkey][1] -= enemybullet[enemybulletkey][1];
                            break;
                        case 2:
                            enemybulletcords[enemybulletkey][1] += enemybullet[enemybulletkey][1];
                            break;
                        case 3:
                            enemybulletcords[enemybulletkey][1] += enemybullet[enemybulletkey][1];
                            enemybulletcords[enemybulletkey][0] += enemybullet[enemybulletkey][1];
                            break;
                        default:
                            enemybulletcords[enemybulletkey][0] += enemybullet[enemybulletkey][1];
                            break;
                    }
                    if (enemybulletcords[enemybulletkey][0] < 0 || enemybulletcords[enemybulletkey][1] < 0 || enemybulletcords[enemybulletkey][1] > 780 || enemybulletcords[enemybulletkey][0] > 1400)                            //remove pulse when reached frame boundries
                    {
                        keysRemove.Add(enemybulletkey);
                    }
                }
                if (keysRemove.Count != 0)
                {
                    foreach (int key in keysRemove)
                    {
                        enemybulletcords.Remove(key);
                        enemybullet.Remove(key);
                    }
                    keysRemove.Clear();
                }
            }
        }

        public static void CreateSkillBullet(int cordx, int cordy, int bulletype, int bulletNumber, int speed, int damage, int bulletimg, int hp) {
            skillbulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            skillbullet.Add(bulletNumber,new int[] { bulletype, speed, damage, 0, hp, bulletimg});
        }

        public static void MoveSkillBullet()
        {
            if (skillbulletcords.Count != 0) {
                List<int> keysRemove = new List<int>();
                foreach (int skillbulletkey in skillbulletcords.Keys)
                {
                    switch (skillbullet[skillbulletkey][0])
                    {
                        case 0:
                            skillbulletcords[skillbulletkey][0] -= skillbullet[skillbulletkey][1];
                            break;

                    }
                    if (skillbulletcords[skillbulletkey][0] < 0 || skillbulletcords[skillbulletkey][1] < 0 || skillbulletcords[skillbulletkey][1] > 780)                            //remove pulse when reached frame boundries
                    {
                        keysRemove.Add(skillbulletkey);
                    }
                }
                if (keysRemove.Count != 0)
                {
                    foreach (int key in keysRemove)
                    {
                        skillbulletcords.Remove(key);
                        skillbullet.Remove(key);
                    }
                    keysRemove.Clear();
                }
            }
        }

        public static void CreatePulse(int cordx, int cordy, int bulletNumber, int pulsetype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            if (guntype == 2) {
                bulletypes.Add(bulletNumber, pulsetype);
            }
        }

        public static void MovePulse()
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletcords.Keys)
            {
                if (guntype == 2) {
                    if (bulletypes[bulletkey] == (guntype))   //flipped pulse will fly vertically in negative direction 
                    {
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                    }
                    else if (bulletypes[bulletkey] == (guntype+1)) //flipped pulse will fly vertically in negative direction 
                    {
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                    }
                }
                bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 780)                            //remove pulse when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    if (guntype == 2)
                    {
                        bulletypes.Remove(key);
                    }
                }
                keysRemove.Clear();
            }
        }

        public static void CreateCanonBall(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
            bulletbounces.Add(bulletNumber, 3);
        }

        public static void MoveCanonBall(Boolean isCanonBall)// move canonball or protron
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletypes.Keys)
            {
                if (bulletypes[bulletkey] == 0)  // different types different directions
                {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 1)
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 2)
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 10) {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 20)
                {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed ;
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810)         //remove bullet when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                    if (isCanonBall) {
                        bulletbounces.Remove(key);
                    }
                }
                keysRemove.Clear();
            }
        }

        public static void CreateMissile(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
        }

        public static void MoveMissile()
        {
            List<int> keysRemove = new List<int>();
            double angle;
            Point bulletPoint = new Point();
            Point targetPoint = new Point();
            foreach (int bulletkey in bulletcords.Keys)
            {
                bulletPoint.X = bulletcords[bulletkey][0];
                bulletPoint.Y = bulletcords[bulletkey][1];
                targetPoint.X = 0;
                targetPoint.Y = missileYcords[(bulletypes[bulletkey] - 10) / 10];
                if (bulletypes[bulletkey] % 10 == 0)    // type 0 missiles get fliped to 45 or-45 type according to distantion point 
                {
                    angle = CalcAngle(bulletPoint, targetPoint); // calc angle base on difference between missile point and target point
                    if (angle >= 40 && angle <= 50)
                    {
                        bulletypes[bulletkey] += 1;
                    }
                    else if (angle <= -40 && angle >= -50)
                    {
                        bulletypes[bulletkey] += 2;
                    }
                }
                else if (bulletypes[bulletkey] % 10 == 1)   // type 1 missiles get fliped to 0 type according to distantion point 
                {
                    angle = CalcAngle(bulletPoint, targetPoint); // calc angle base on difference between missile point and target point
                    if (angle < -5)
                    {
                        bulletypes[bulletkey] += 1;
                    } else if (angle >= -5 && angle <= 5) {
                        bulletypes[bulletkey] -= 1;
                    }
                }
                else if (bulletypes[bulletkey] % 10 == 2)   // type 2 missiles get fliped to 0 type according to distantion point 
                {
                    angle = CalcAngle(bulletPoint, targetPoint); // calc angle base on difference between missile point and target point
                    if (angle > 0)
                    {
                        bulletypes[bulletkey] -= 1;
                    }
                    else if ((angle <= 0 && angle >= -10))
                    {
                        bulletypes[bulletkey] -= 2;
                    }

                }
                if (bulletypes[bulletkey] % 10 == 0) {                     // flying directions according to missile type
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                } else if (bulletypes[bulletkey] % 10 == 1) {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                } else if (bulletypes[bulletkey] % 10 == 2) {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810 || bulletcords[bulletkey][0] > 1400)                            //remove bullet when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }
        private static double CalcDistance(Point bulletPoint, Point targetPoint)
        {
            double ydistance = bulletPoint.Y - targetPoint.Y;
            return Math.Sqrt(Math.Pow(bulletPoint.X - targetPoint.X, 2) + Math.Pow(ydistance, 2)); // calc distance from given points on screen
        }

        private static double CalcAngle(Point bulletPoint, Point targetPoint) {
            double ydistance = bulletPoint.Y - targetPoint.Y;
            double dist = CalcDistance(bulletPoint, targetPoint); // calc distance from given points on screen
            return Math.Round((180 / Math.PI) * Math.Asin(ydistance / dist), 1);
        }

        public static void CreateGuidedMissile(int cordx, int cordy, int bulletNumber, int bulletype)
        {
            bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
            bulletypes.Add(bulletNumber, bulletype);
        }

        public static void MoveGuidedMissile(Dictionary<int, int[]> enemyCords, Size bulletSize, Dictionary<int, int[]> enemyType, Imgs img) //move guided missle
        {
            List<int> keysRemove = new List<int>();
            Point enemyPoint;
            Point bulletPoint = new Point();
            double angle;
            foreach (int bulletkey in bulletcords.Keys)
            {
                enemyPoint = GetClosestEnemy(enemyCords, bulletkey, bulletSize, enemyType, img);
                bulletPoint.X = bulletcords[bulletkey][0] + bulletSize.Width / 2;
                bulletPoint.Y = bulletcords[bulletkey][1] + bulletSize.Height / 2;
                angle = CalcAngle(bulletPoint, enemyPoint); // check angle between bullet and closest enemy
                if (angle >= -5 && angle <= 5)
                {
                    if (bulletPoint.X > enemyPoint.X)
                    {
                        bulletypes[bulletkey] = 0;
                    }
                    else
                    {
                        bulletypes[bulletkey] = 7;
                    }
                }
                else if (angle > 40 && angle <= 50)
                {
                    bulletypes[bulletkey] = 1;
                }
                else if (angle > 85 && angle <= 95)
                {
                    bulletypes[bulletkey] = 3;
                }
                else if (angle > 130 && angle <= 140)
                {
                    bulletypes[bulletkey] = 5;
                }

                else if (angle < -130 && angle >= -140)
                {
                    bulletypes[bulletkey] = 6;
                }
                else if (angle < -85 && angle >= -95)
                {
                    bulletypes[bulletkey] = 4;
                }
                else if (angle < -40 && angle >= -50)
                {
                    bulletypes[bulletkey] = 2;
                }
                switch (bulletypes[bulletkey]) // choose fly direction based on enemy angle (enemy img type)
                {
                    case 0:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        break;
                    case 1:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 2:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 3:
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 4:
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 5:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 6:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 7:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        break;
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810 || bulletcords[bulletkey][0] > 1400)                            //remove bullet when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }
        private static Point GetClosestEnemy(Dictionary<int, int[]> enemyCords, int bulletkey, Size bulletSize, Dictionary<int, int[]> enemyType, Imgs img) {
            List<int> keysRemove = new List<int>();
            Dictionary<int, int> distances = new Dictionary<int, int>();
            double distance;
            double xdistancePow;
            double ydistancePow;
            foreach (int enemykey in enemyCords.Keys) {                    // calc distance from a bullet to each enemy
                if (enemyType[enemykey][0] != 3) {
                    xdistancePow = Math.Pow((enemyCords[enemykey][0] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Width / 2) - (bulletcords[bulletkey][0] + bulletSize.Width / 2), 2);
                    ydistancePow = Math.Pow((enemyCords[enemykey][1] + Enemyobj.enemyimgs[Enemyobj.enemystats[enemykey][1]][Enemyobj.enemystats[enemykey][0]].Height / 2) - (bulletcords[bulletkey][1] + bulletSize.Height / 2), 2);
                    distance = Math.Sqrt(xdistancePow + ydistancePow);       // calc distance from the middle of the img 
                    if (distances.Count == 0)
                    {
                        distances.Add(enemykey, (int)distance); // just adding at the beginning
                    }
                    else {
                        foreach (int distkey in distances.Keys) {
                            if (distances[distkey] > distance)   // if lesser distance found remove the current and add the new one
                            {
                                distances.Add(enemykey, (int)distance);
                                keysRemove.Add(distkey);
                                break;
                            }
                        }
                        if (keysRemove.Count != 0)
                        {
                            foreach (int key in keysRemove)
                            {
                                distances.Remove(key);
                            }
                            keysRemove.Clear();
                        }
                    }
                }
            }
            if (distances.Count != 0)
            {
                Point closestPoint = new Point(enemyCords[distances.ElementAt(0).Key][0]+ (Enemyobj.enemyimgs[Enemyobj.enemystats[distances.ElementAt(0).Key][1]][Enemyobj.enemystats[distances.ElementAt(0).Key][0]].Width / 2), enemyCords[distances.ElementAt(0).Key][1]+(Enemyobj.enemyimgs[Enemyobj.enemystats[distances.ElementAt(0).Key][1]][Enemyobj.enemystats[distances.ElementAt(0).Key][0]].Height / 2)); // return the closest point that left
                return closestPoint;
            }
            else
            {
                return new Point(0, bulletcords[bulletkey][1]);
            }
        }
        public static int CreateParticle(int cordx, int cordy, int bulletNumber, int bulletype, Boolean isBullet)
        {
            if (isBullet)
            {
                bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
                bulletypes.Add(bulletNumber, bulletype);
            }
            else {
                for (int i = 1; i < 9; i++)
                {
                    bulletNumber += 1;
                    bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
                    bulletypes.Add(bulletNumber, i + 10);
                }
            }
            return bulletNumber;
        }

        public static void MoveParticle()
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletypes.Keys)
            {
                if (bulletypes[bulletkey] == 0)  // different types different directions
                {
                    bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 1)
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                }
                else if (bulletypes[bulletkey] == 2)
                {
                    bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                }
                if (bulletypes[bulletkey] > 10) {
                    switch (bulletypes[bulletkey] - 10) // choose fly direction based on img angle
                    {
                        case 1:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                            break;
                        case 2:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                            break;
                        case 3:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                            break;
                        case 4:
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                            break;
                        case 5:
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                            break;
                        case 6:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                            break;
                        case 7:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                            bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                            break;
                        case 8:
                            bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                            break;
                    }
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810 || bulletcords[bulletkey][0] > 1400)                            //remove bullet when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }

        public static void CreateDeathDrone(int cordx, int cordy, int bulletNumber, int type, int bulletAmount, Random rand) // deathdrone or replicating bullet or proton
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                bulletNumber += 1;
                if (type == 0)
                {
                    bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
                    bulletypes.Add(bulletNumber, rand.Next(0, 8));
                } else if (type == 10) {
                    bulletcords.Add(bulletNumber, new int[] { rand.Next(cordx-50, cordx+50), rand.Next(cordy-50, cordy+50) });
                    bulletypes.Add(bulletNumber, rand.Next(1,3)*10);
                }
                else {
                    bulletcords.Add(bulletNumber, new int[] { cordx, cordy });
                    bulletypes.Add(bulletNumber, type-1);
                }
            }
        }

        public static void MoveDeathDrone() // deathdrone or replicating bullet
        {
            List<int> keysRemove = new List<int>();
            foreach (int bulletkey in bulletypes.Keys)
            {
                switch (bulletypes[bulletkey]) // choose fly direction based on img angle
                {
                    case 0:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        break;
                    case 1:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 2:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] - bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 3:
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 4:
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 5:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] - bulletSpeed;
                        break;
                    case 6:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        bulletcords[bulletkey][1] = bulletcords[bulletkey][1] + bulletSpeed;
                        break;
                    case 7:
                        bulletcords[bulletkey][0] = bulletcords[bulletkey][0] + bulletSpeed;
                        break;
                }
                if (bulletcords[bulletkey][0] < 0 || bulletcords[bulletkey][1] < 0 || bulletcords[bulletkey][1] > 810 || bulletcords[bulletkey][0] > 1400)                            //remove bullet when reached frame boundries
                {
                    keysRemove.Add(bulletkey);
                }
            }
            if (keysRemove.Count != 0)
            {
                foreach (int key in keysRemove)
                {
                    bulletcords.Remove(key);
                    bulletypes.Remove(key);
                }
                keysRemove.Clear();
            }
        }

        public static void ChangeBulletsFrame(Dictionary<int, int[]> bulletframes)
        {
            Dictionary<int, int[]> bulframes = new Dictionary<int, int[]>(bulletframes);
            foreach (int bulletkey in bulframes.Keys) {
                if (bulletframes[bulletkey][3] != -1) {
                    if (bulletframes[bulletkey][3] % 2 == 0)
                    {
                        bulletframes[bulletkey][3] += 1;
                    }
                    else {
                        bulletframes[bulletkey][3] -= 1;
                    }
                }
            }
            bulframes.Clear();
        }

        public static void LoadEnemyBulletImgs(int enemyId, int bulletindx)
        {
            if (enemyId < 1000) {
                enemybulletimgs.Add(bulletindx, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\enemybullets\" + bulletindx.ToString() + ".png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\enemybullets\" + bulletindx.ToString() + "chng.png"),
                });
            } else
            {
                enemybossbulletimgs.Add(bulletindx, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bossbullets\" + bulletindx.ToString() + ".png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bossbullets\" + bulletindx.ToString() + "chng.png"),
                });
                foreach (int bossbullet in bosswithskillbullets) {
                    if (bossbullet == enemyId)
                    {
                        enemybossbulletimgs.Add(enemyId, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bossbullets\" + enemyId.ToString() + ".png"),
                        Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\bossbullets\" + enemyId.ToString() + "chng.png"),
                        });
                    }
                }
            }
        }

        public static void LoadBeamSkillBulletImgs()
        {
            enemybulletimgs.Add(92, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\enemybullets\92.png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\enemybullets\92chng.png"),
                });
        }

        public static void LoadPlayerSkillBulletImgs(int skillId) {
            playerskillbulletimgs.Add(skillId, new List<Image> { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\skillbullets\" + skillId.ToString() + ".png"),
                    Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\bullets\skillbullets\" + skillId.ToString() + "chng.png"),
                });
        }

        public static void ResetBulletsObj() {
            bulletcords.Clear();
            bulletypes.Clear();
            bulletbounces.Clear();
            enemybossbulletcords.Clear();
            enemybossbullet.Clear();
            enemybulletcords.Clear();
            enemybullet.Clear();
            skillbulletcords.Clear();
            skillbullet.Clear();
            enemybulletimgs.Clear();
            enemybossbulletimgs.Clear();
            playerskillbulletimgs.Clear();
            guntype = 0;
            bulletSpeed = 20;
            bulletDamage = 24;
            bulletLevel = 1;
        }
    }
}
