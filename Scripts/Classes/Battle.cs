using UnityEngine;
using System;

namespace Barter.Classes
{

    public class Battle
    {
        public int id;
        public Monster monster;

        public DateTime datetimeCreated;
        //State 0 is ongoing, 1 is complete
        public int state;

        public Equipment equipment;

        public Battle(int id, Monster monster, DateTime datetimeCreated, int state, Equipment equipment)
        {
            this.id = id;
            this.monster = monster;
            this.datetimeCreated = datetimeCreated;
            this.state = state;
            this.equipment = equipment;
        }

    }

}