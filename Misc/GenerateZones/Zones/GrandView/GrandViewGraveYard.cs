﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Objects.Interface;
using Objects.LevelRange;
using Objects.LoadPercentage;
using Objects.Mob.Interface;
using Objects.Personality.Personalities;
using Objects.Room.Interface;
using Objects.Zone.Interface;
using static GenerateZones.RandomZoneGeneration;
using static Objects.Mob.NonPlayerCharacter;

namespace GenerateZones.Zones.GrandView
{
    public class GrandViewGraveYard : BaseZone, IZoneCode
    {
        public GrandViewGraveYard() : base(21)
        {
        }

        public IZone Generate()
        {
            List<string> names = new List<string>() { "Falim Nasha", "Bushem Dinon", "Stavelm Eaglelash", "Giu Thunderbash", "Marif Hlisk", "Fim Grirgav", "Strarcar Marshgem", "Storth Shadowless", "Tohkue-zid Lendikrafk", "Vozif Jikrehd", "Dranrovelm Igenomze", "Zathis Vedergi", "Mieng Chiao", "Thuiy Chim", "Sielbonron Canderger", "Craldu Gacevi",
            "Rumeim Shennud","Nilen Cahrom","Bei Ashspark","Hii Clanbraid","Sodif Vatsk","Por Rorduz","Grorcerth Forestsoar","Gath Distantthorne","Duhvat-keuf Faltrueltrim","Ham-kaoz Juhpafk","Rolvoumvald Gibenira","Rondit Vumregi","Foy Sheiy","Fiop Tei","Fruenrucu Jalbese","Fhanun Guldendal"};

            RandomZoneGeneration randZoneGen = new RandomZoneGeneration(5, 5, Zone.Id);
            RoomDescription description = new RoomDescription();
            description.LongDescription = "The dirt has been freshly disturbed where a body has been recently placed in the ground.";
            description.ExamineDescription = "Some flowers have been placed on the headstone that belongs to {name}.";
            description.ShortDescription = "Graveyard";
            randZoneGen.RoomDescriptions.Add(description);

            description = new RoomDescription();
            description.LongDescription = "The headstone has been here a while and is starting to show its age.";
            description.ExamineDescription = "The headstone name has worn off and is impossible to read.";
            description.ShortDescription = "Graveyard";
            randZoneGen.RoomDescriptions.Add(description);

            description = new RoomDescription();
            description.LongDescription = "A grand tower of marble rises to the sky.  This person must have been important or rich in life.";
            description.ExamineDescription = "The tombstone belongs to {name}.";
            description.ShortDescription = "Graveyard";
            randZoneGen.RoomDescriptions.Add(description);

            description = new RoomDescription();
            description.LongDescription = "A small flat stone marker is all shows where this person is buried.";
            description.ExamineDescription = "The grave marker belongs to {name}.";
            description.ShortDescription = "Graveyard";
            randZoneGen.RoomDescriptions.Add(description);

            description = new RoomDescription();
            description.LongDescription = "There is a small bench for resting as one walks among the tombstones.";
            description.ExamineDescription = "A pair of angles are carved into the sides of the feet on the bench.";
            description.ShortDescription = "Graveyard";
            randZoneGen.RoomDescriptions.Add(description);

            description = new RoomDescription();
            description.LongDescription = "Crosses give hint that the owner might have been religions in life.";
            description.ExamineDescription = "Here lies {name}.";
            description.ShortDescription = "Graveyard";
            randZoneGen.RoomDescriptions.Add(description);

            description = new RoomDescription();
            description.LongDescription = "The statue a weeping angle stands watch over the deceased.";
            description.ExamineDescription = "The grave belongs to {name}.";
            description.ShortDescription = "Graveyard";
            randZoneGen.RoomDescriptions.Add(description);

            FlavorOption option = new FlavorOption();
            option.FlavorValues.Add("{name}", names);
            description.FlavorOption.Add(option);
            randZoneGen.RoomDescriptions.Add(description);

            Zone = randZoneGen.Generate();
            Zone.InGameDaysTillReset = 1;
            Zone.Name = nameof(GrandViewGraveYard);

            int creatueChoices = 0;

            MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (MethodInfo info in methods)
            {
                if (info.ReturnType == typeof(INonPlayerCharacter) && info.Name != "BuildNpc")
                {
                    creatueChoices++;
                }
            }


            int percent = 50 / creatueChoices;
            foreach (IRoom room in Zone.Rooms.Values)
            {
                ILoadableItems loadable = (ILoadableItems)room;
                loadable.LoadableItems.Add(new LoadPercentage() { PercentageLoad = percent, Object = Skeleton() });
                loadable.LoadableItems.Add(new LoadPercentage() { PercentageLoad = percent, Object = Zombie() });
                loadable.LoadableItems.Add(new LoadPercentage() { PercentageLoad = percent, Object = Crow() });

            }

            return Zone;
        }

        private INonPlayerCharacter Skeleton()
        {
            INonPlayerCharacter npc = BuildNpc();
            npc.LevelRange = new LevelRange() { LowerLevel = 17, UpperLevel = 19 };
            npc.Personalities.Add(new Wanderer());
            npc.KeyWords.Add("skeleton");
            npc.SentenceDescription = "skeleton";
            npc.ShortDescription = "A skeleton walks bones clatter as it walks around.";
            npc.LookDescription = "Somewhere the skeleton lost part of its arm.";
            npc.ExamineDescription = "There air takes on a slight chill as the skeleton turns and looks at you.";

            return npc;
        }

        private INonPlayerCharacter Zombie()
        {
            INonPlayerCharacter npc = BuildNpc();
            npc.LevelRange = new LevelRange() { LowerLevel = 17, UpperLevel = 19 };
            npc.KeyWords.Add("zombie");
            npc.SentenceDescription = "zombie";
            npc.ShortDescription = "A zombie stares off into the distance looking at nothing.";
            npc.LookDescription = "The zombie is wearing clothes or at least what used to be clothes.  A small red handkerchief can be seen sticking out of what is left of its suit.";
            npc.ExamineDescription = "The smell of rotting flesh emanates from the zombie as you get close to it.";

            return npc;
        }

        private INonPlayerCharacter Crow()
        {
            INonPlayerCharacter npc = BuildNpc();
            npc.LevelRange = new LevelRange() { LowerLevel = 16, UpperLevel = 17 };
            npc.KeyWords.Add("Crow");
            npc.LookDescription = "As you and the crow stare at each other it starts crowing loudly as trying to win a staring contest by making you look away.";
            npc.SentenceDescription = "a crow";
            npc.ShortDescription = "A black as night crow calls out a warning as you approach.";
            npc.ExamineDescription = "It seems to have been born of the night with black feathers, feet and beak. The small black beady eyes are the only thing to reflect any light.";


            return npc;
        }

        private INonPlayerCharacter BuildNpc()
        {
            INonPlayerCharacter npc = CreateNonplayerCharacter(MobType.Other);
            npc.Personalities.Add(new Wanderer());
            return npc;
        }
    }
}
