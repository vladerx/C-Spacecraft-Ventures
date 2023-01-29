using System.Collections.Generic;
using System.Drawing;

namespace spacebattle
{
    class Imgs
    {
        public readonly Image[] playerimg = { Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\player.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerdmg.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerimmune.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerslowed.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerfreeze.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerchng.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerdmgchng.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerimmunechng.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerslowedchng.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\player\playerfreezechng.png")
        };

        public readonly Image[] dropimgs = new Image[] {
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\energy30.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\energy50.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\fuel.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\PlayerHP.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\ammoup.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\defgundrop.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\pulsegunscatter.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\boungundrop.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\pulsegun.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\canongun.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\particlegun.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\missile.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\protongun.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\deathdronelauncher.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\guidedmissile.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\replicatingbulletgun.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\critchancedrop.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\critdmgdrop.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\fireRateUpDrop.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\coin.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\parts.png"), 
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\bronze.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\drops\iron.png")
        };


        public readonly Image[] menuimgs = {
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\ingame interface\menubutton.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\ingame interface\menubuttonHover.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\ingame interface\frame.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\ingame interface\portalb.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\ingame interface\portalw.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\ingame interface\skillbar.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\ingame interface\topbotbar.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\ingame interface\leftbar.png")
        };

        public readonly Image[] settingsimgs = {
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\menu.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\controls.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\redbut.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\greenbut.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\but.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\volumebut.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\plus.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\minus.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\plushov.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\settings\minushov.png")
        };

        public readonly Image[] preGameMenuimgs = {
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\pregamemenu\menuback.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\pregamemenu\worldbut.png"),
            Image.FromFile(@"C:\Users\\source\repos\spacebattle\spacebattle\pregamemenu\worldbuthov.png")
        };

    }
}
