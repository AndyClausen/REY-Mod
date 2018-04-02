﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Minimap;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Pipes.LiquidComponents;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.Shared.Items;
using Eco.Gameplay.Pipes;
using Eco.World.Blocks;
using EcoRealism.Utils;

namespace EcoRealism.Utils
{

    public class UtilsInitItem : Item
    {
        static UtilsInitItem()
        {
            SkillUtils.Initialize();
        }
    }


    public static class SkillUtils
    {

        public static List<string> superskillconfirmed;

        public static void Initialize()
        {
            superskillconfirmed = new List<string>(); 
        }

        public static void ShowSuperSkillInfo(Player player)
        {
            player.OpenInfoPanel("Super Skills", "Current amount of Super Skills: <b><color=green>" + SkillUtils.SuperSkillCount(player.User) + "</color></b><br>Max amount of Super Skills: <b><color=green>" + ConfigHandler.maxsuperskills + "</color></b><br><br>Super Skills are Skills that can be leveled all the way up to 10 times.<br><br><color=red>You can only have a limited amount of them.</color><br><br>To confirm that you understood Super Skills and to unlock them please enter <b><color=green>/confirmsuperskill</b></color>");
        }

        public static bool UserHasSkill(User user, Type skilltype, int lvl)
        {
            
            foreach (Skill skill in user.Skillset.Skills)
            {
                if (skill.Type == skilltype)
                {
                    if (skill.Level >= lvl)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static int SuperSkillCount(User user)
        {
            int x = 0;
            foreach (Skill skill in user.Skillset.Skills)
            {
                if (skill.Level > 5) x++;
            }
            return x;
        }
    }

    public static class ChatUtils
    {
        public static void SendMessage(User user)
        { }

        public static void SendMessage(Player player)
        { }


        public static string CustomTags(string text)
        {
            return text;
        }

    }


    public static class IOUtils
    {
        public static string ReadConfig(string filename)
        {
            string content = string.Empty;
            string path = "./configs/" + filename;


            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else return "Error reading file: File does not exist!";


        }


        public static void WriteToLog(string logdata, string desc = "")
        {
            string path = "./Dump/log.txt";
            logdata = Environment.NewLine + System.Environment.NewLine + desc + System.Environment.NewLine + logdata;
            if (!File.Exists(path)) File.Create(path).Close();

            File.AppendAllText(path, logdata);

        }

        public static void WriteToFile(string path,string text)
        {
            string dirpath = path.Substring(0, path.LastIndexOf('/') + 1);
            if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
            if (!File.Exists(path)) File.Create(path).Close();
            File.AppendAllText(path, text);
        }

        public static void ClearFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                File.Create(path).Close();
            }
        }

    }


    public class MyComparer : IComparer<KeyValuePair<string, float>>
    {
        public int Compare(KeyValuePair<string, float> x, KeyValuePair<string, float> y)
        {
            if (x.Value == y.Value) return 0;
            if (x.Value > y.Value) return -1;
            else return 1;
        }

    }

}
