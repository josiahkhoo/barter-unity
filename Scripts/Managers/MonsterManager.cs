using SimpleJSON;
using System.Collections.Generic;
using System.Collections;
using Barter.Classes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

namespace Barter.Managers
{
    public static class MonsterManager
    {
        public static Monster GetMonsterFromString(string monsterString)
        {
            var monsterJson = JSON.Parse(monsterString);
            int monsterId = monsterJson["id"];
            string name = monsterJson["name"];
            string appearanceConfig = monsterJson["apperance_config"];
            bool isActive = monsterJson["is_active"];
            int level = monsterJson["level"];
            int duration = monsterJson["duration"];
            int partySize = monsterJson["party_size"];
            Monster monster = new Monster(monsterId, name, appearanceConfig, isActive, level, duration, partySize);
            return monster;
        }
    }
}