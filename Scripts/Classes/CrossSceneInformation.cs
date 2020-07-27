using CharacterInternal = Barter.Classes.Character;
using UnityEngine;


namespace Barter.Classes
{
    public static class StaticClass
    {
        public static string CrossSceneInformation { get; set; }

        public static User CrossSceneUser { get; set; }

        public static User CrossSceneOtherUser { get; set; }

        public static int CrossSceneConversationInt { get; set; }

        public static int CrossSceneChatInt { get; set; }

        public static Party CrossSceneParty { get; set; }

        public static Battle CrossSceneBattle { get; set; }

        public static Monster CrossSceneMonster { get; set; }

        public static CharacterInternal SelectedCharacter { get; set; }

        public static CharacterInternal CharacterOne { get; set; }

        public static CharacterInternal CharacterTwo { get; set; }

        public static CharacterInternal CharacterThree { get; set; }

        public static int CrossSceneEquipmentType { get; set; }

    }
}