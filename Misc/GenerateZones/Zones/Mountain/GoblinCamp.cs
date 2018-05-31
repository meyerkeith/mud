﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Objects.Die;
using Objects.Effect;
using Objects.Effect.Interface;
using Objects.Global;
using Objects.Item.Interface;
using Objects.Item.Items;
using Objects.Item.Items.Interface;
using Objects.Language;
using Objects.Language.Interface;
using Objects.Magic.Enchantment;
using Objects.Magic.Interface;
using Objects.Material.Interface;
using Objects.Material.Materials;
using Objects.Mob;
using Objects.Mob.Interface;
using Objects.Personality.Personalities;
using Objects.Room;
using Objects.Room.Interface;
using Objects.Zone;
using Objects.Zone.Interface;
using static Objects.Global.Direction.Directions;
using static Objects.Guild.Guild;
using static Objects.Item.Item;
using static Objects.Item.Items.Equipment;
using static Shared.TagWrapper.TagWrapper;

namespace GenerateZones.Zones.Mountain
{
    public class GoblinCamp : IZoneCode
    {
        IZone zone;
        private int zoneId = 16;
        private int roomId = 1;
        private int itemId = 1;
        private int npcId = 1;

        public IZone Generate()
        {
            zone = new Zone();
            zone.Id = zoneId;
            zone.InGameDaysTillReset = 1;
            zone.Name = nameof(GoblinCamp);

            int methodCount = this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Count();
            for (int i = 1; i <= methodCount; i++)
            {
                string methodName = "GenerateRoom" + i;
                MethodInfo method = this.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
                if (method != null)
                {
                    IRoom room = (Room)method.Invoke(this, null);
                    room.Zone = zone.Id;
                    ZoneHelper.AddRoom(zone, room);
                }
            }

            ConnectRooms();

            return zone;
        }


        #region Rooms
        private IRoom GenerateRoom1()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "A hastily made wooden gate prevents attackers from riding into camp.";
            room.LongDescription = "Long tree limbs have been sharpened and lashed into a form of gate that prevents outsiders from getting into camp.";

            return room;
        }

        private IRoom CampOutSide()
        {
            IRoom room = GenerateRoom();
            room.MovementCost = 1;
            room.Attributes.Add(Room.RoomAttribute.Outdoor);
            room.Attributes.Add(Room.RoomAttribute.Weather);


            room.ShortDescription = "Goblin Camp";
            return room;
        }

        private IRoom CampInside()
        {
            IRoom room = GenerateRoom();
            room.MovementCost = 1;
            room.Attributes.Add(Room.RoomAttribute.Indoor);

            return room;
        }

        private IRoom GenerateRoom()
        {
            IRoom room = new Room();
            room.Id = roomId++;
            return room;
        }

        private IRoom GenerateRoom2()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "The camp walls rise up each side to the north and south.";
            room.LongDescription = "The camp appears to be well used with lots of tracks in the dirt.";

            return room;
        }

        private IRoom GenerateRoom3()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "In the dim light to the north you can see the frail figures of several human prisoners.";
            room.LongDescription = "A prison has been carved out of the hillside to the north.";

            room.Enchantments.Add(PrisonerEnter(zoneId, room.Id));
            return room;
        }

        private IEnchantment PrisonerEnter(int zoneId, int roomId)
        {
            IEnchantment enchantment = new EnterRoomEnchantment();
            enchantment.ActivationPercent = 100;

            IEffect effect = new Message();
            enchantment.Effect = effect;

            IEffectParameter effectParameter = new EffectParameter();
            effectParameter.Message = new TranslationMessage("A prisoner shouts \"Let us out of here.\"");
            effectParameter.RoomId = new RoomId(zoneId, roomId);
            enchantment.Parameter = effectParameter;

            return enchantment;
        }

        private IRoom GenerateRoom4()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "The pen is crudely constructed and like it would fall over if the animals wanted to get out.";
            room.LongDescription = "A pen to the south is where the goblins hold their war pigs.";

            return room;
        }

        private IRoom GenerateRoom5()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "The pen smells of foul animal waste.";
            room.LongDescription = "It smells like the goblins do not clean the pens regularly.";

            for (int i = 0; i < 5; i++)
            {
                room.AddMobileObjectToRoom(WarPig());
            }

            return room;
        }

        private IRoom GenerateRoom6()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "The slightly larger hut lies to the north while the smaller to the south.  The size of the huts would indicate that someone important live there.";
            room.LongDescription = "Two massive huts engulf your field of view and dwarfs the other huts to the east.";

            return room;
        }

        private IRoom GenerateRoom7()
        {
            IRoom room = CampInside();
            room.ShortDescription = "Goblin Chief Hut";

            room.ExamineDescription = "A small table for eating sits to the west while a smaller room for sleeping is to the north.  Several swords and shields spaced evenly apart decorate the walls.";
            room.LongDescription = "The large room is has animal hides for a floor with several torches for lighting the area nicely.";

            room.AddMobileObjectToRoom(GoblinChief());

            room.Items.Add(new RecallBeacon());
            return room;
        }

        private IRoom GenerateRoom8()
        {
            IRoom room = CampInside();
            room.ShortDescription = "Goblin Shaman Hut";

            room.ExamineDescription = "Small totems of different animal spirits sit around the fire.";
            room.LongDescription = "The hut is mostly empty save for a small fire in the middle of the hut.";

            room.AddMobileObjectToRoom(Shaman());
            return room;
        }

        private IRoom GenerateRoom9()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "A small cooking fire burns near each of the huts.";
            room.LongDescription = "Two small huts flank the path through the camp.";

            return room;
        }

        private IRoom GenerateRoom10()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "A small cooking fire burns near each of the huts.";
            room.LongDescription = "Two small huts flank the path through the camp.";

            return room;
        }

        private IRoom GenerateRoom11()
        {
            IRoom room = CampInside();
            room.ExamineDescription = "A the desk has several papers on it but they are so poorly written that it makes reading impossible.";
            room.LongDescription = "The hut contains a small desk for writing as well a place to sleep.";
            room.ShortDescription = "A goblin hut";

            return room;
        }

        private IRoom GenerateRoom12()
        {
            IRoom room = CampInside();
            room.ExamineDescription = "Several strips of meat hang from the hut and are slowly becoming jerky in the smoke.";
            room.LongDescription = "The hut is filled with smoke making it hard to see.";
            room.ShortDescription = "A goblin hut";

            return room;
        }

        private IRoom GenerateRoom13()
        {
            IRoom room = CampInside();
            room.ExamineDescription = "Five sets of bunks extend out from the table.  Who ever was the 3rd player would won the hand with a royal flush.";
            room.LongDescription = "Several small bunks extend out past a table with cards on it.";
            room.ShortDescription = "A goblin hut";

            Container container = Chest();
            container.Items.Add(Arms());
            container.Items.Add(Head());
            room.Items.Add(container);

            container = Chest();
            container.Items.Add(Body());
            container.Items.Add(Legs());
            room.Items.Add(container);

            container = Chest();
            container.Items.Add(Feet());
            container.Items.Add(Neck());
            room.Items.Add(container);

            container = Chest();
            container.Items.Add(Finger());
            container.Items.Add(Waist());
            room.Items.Add(container);

            container = Chest();
            container.Items.Add(Hand());
            room.Items.Add(container);
            return room;
        }

        private IRoom GenerateRoom14()
        {
            IRoom room = CampInside();
            room.ExamineDescription = "There is a carving in one of the tables.  TJ + CJ";
            room.LongDescription = "Several rows of tables are in line.  A small cooking area behind a counter is in the back.";
            room.ShortDescription = "A goblin hut";

            return room;
        }

        private IRoom GenerateRoom15()
        {
            IRoom room = CampOutSide();
            room.ExamineDescription = "The gate has a few broken pieces where it looks like has held off some attacks.";
            room.LongDescription = "A large gate to the east separates the camp from the outside world.";

            return room;
        }
        #endregion Rooms

        #region NPC
        private INonPlayerCharacter WarPig()
        {
            INonPlayerCharacter npc = new NonPlayerCharacter();
            npc.Id = npcId++;
            npc.Level = 16;

            npc.ExamineDescription = "The pigs fur is matted with mud and manure.";
            npc.LongDescription = "A war pig snorts around looking for something to eat.";
            npc.ShortDescription = "A goblin war pig.";
            npc.SentenceDescription = "goblin";
            npc.KeyWords.Add("pig");
            npc.KeyWords.Add("war");

            npc.Personalities.Add(new Wanderer(100));

            return npc;
        }

        private INonPlayerCharacter Shaman()
        {
            INonPlayerCharacter npc = new NonPlayerCharacter();
            npc.Id = npcId++;
            npc.Level = 40;

            npc.ExamineDescription = "Wearing a pair of deer antlers and the pelts of a bear the shaman would stand out from any member of the goblin camp.";
            npc.LongDescription = "The shaman sways gently as he communes with spirits.";
            npc.ShortDescription = "The camps shaman.";
            npc.SentenceDescription = "goblin";
            npc.KeyWords.Add("goblin");
            npc.KeyWords.Add("shaman");

            npc.Personalities.Add(new GuildMaster(Guilds.Shaman));

            IEnchantment enchantment = new EnterRoomEnchantment();
            IEffect say = new Message();
            IEffectParameter effectParameter = new EffectParameter();
            enchantment.ActivationPercent = 100;
            enchantment.Effect = say;
            TranslationPair translationPair = new TranslationPair(Objects.Global.Language.Translator.Languages.Goblin, "The spirits said you would come.");
            List<ITranslationPair> translationPairs = new List<ITranslationPair>() { translationPair };
            effectParameter.Message = new TranslationMessage("The Shaman says \"{0}\"", TagType.Communication, translationPairs);
            effectParameter.RoomId = new RoomId(zoneId, 8);
            enchantment.Parameter = effectParameter;

            npc.Enchantments.Add(enchantment);

            return npc;
        }

        private INonPlayerCharacter GoblinChief()
        {
            INonPlayerCharacter npc = new NonPlayerCharacter();
            npc.Id = npcId++;
            npc.Level = 25;

            npc.ExamineDescription = "Goblin Cheif";
            npc.LongDescription = "Goblin Cheif";
            npc.ShortDescription = "The camps chief.";
            npc.SentenceDescription = "goblin";
            npc.KeyWords.Add("goblin");
            npc.KeyWords.Add("chief");

            return npc;
        }
        #endregion NPC

        #region Items
        private Container Chest()
        {
            Container chest = new Container();
            chest.Id = itemId++;
            chest.ExamineDescription = "The chest is a standard issue goblin warrior chest.";
            chest.LongDescription = "The chest is made of wood and reinforced with steel bands.";
            chest.ShortDescription = "A small chest for storing equipment";
            chest.KeyWords.Add("chest");
            chest.Attributes.Add(ItemAttribute.NoGet);

            return chest;

        }

        private IArmor Armor()
        {
            IArmor armor = new Armor();
            armor.Level = 17;
            armor.Id = itemId++;
            armor.Dice = GlobalReference.GlobalValues.DefaultValues.DiceForArmorLevel(armor.Level);
            return armor;
        }

        private IArmor Arms()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Arms;
            armor.Material = new Leather();
            armor.ExamineDescription = "The lamb skin is flawless.  Maybe the original owner thought it would grant the wearer extra protection.";
            armor.LongDescription = "The sleeves are made in such a way as to have soft wool on the inside and out.";
            armor.ShortDescription = "A pair of lamb skin sleeves.";
            armor.KeyWords.Add("lamb");
            armor.KeyWords.Add("skin");
            armor.KeyWords.Add("sleeve");

            return armor;
        }

        private IArmor Body()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Body;
            armor.Material = new Leather();
            armor.ExamineDescription = "The bear skin is made in such a way as to have a hood of sorts that can be flipped up.";
            armor.LongDescription = "The bear skin is made from a hodgepodge of several different bears.";
            armor.ShortDescription = "A bearskin.";
            armor.KeyWords.Add("bear");
            armor.KeyWords.Add("skin");

            return armor;
        }

        private IArmor Feet()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Feet;
            armor.Material = new Leather();
            armor.ExamineDescription = "The boots are have been dyed to a dark black color.";
            armor.LongDescription = "The boots are made of a type of leather sew together.";
            armor.ShortDescription = "A dark black pair of boats.";
            armor.KeyWords.Add("black");
            armor.KeyWords.Add("leather");
            armor.KeyWords.Add("boots");

            return armor;
        }

        private IArmor Finger()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Finger;
            armor.Material = new Gold();
            armor.ExamineDescription = "The gem is made of a red stone with white veins that look like a swirl frozen in time.";
            armor.LongDescription = "The ring is made of a thick gold band with a red stone in the center.";
            armor.ShortDescription = "Gold Ring.";
            armor.KeyWords.Add("red");
            armor.KeyWords.Add("stone");
            armor.KeyWords.Add("gold");
            armor.KeyWords.Add("ring");

            IEnchantment enchantment = new HeartbeatBigTickEnchantment();
            enchantment.Effect = new RecoverStamina();
            IEffectParameter effectParameter = new EffectParameter();
            effectParameter.Dice = new Dice(1, 1);
            enchantment.Parameter = effectParameter;

            return armor;
        }

        private IArmor Hand()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Hand;
            armor.Material = new Cloth();
            armor.ExamineDescription = "The gloves are made of a delicate silver lace that sparkles in the light.";
            armor.LongDescription = "A pair of silver lace gloves that would make a grand statement at any ball.";
            armor.ShortDescription = "A silver pair of ballroom lace gloves.";
            armor.KeyWords.Add("silver");
            armor.KeyWords.Add("lace");
            armor.KeyWords.Add("gloves");

            return armor;
        }

        private IArmor Head()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Head;
            armor.Material = new Leather();
            armor.ExamineDescription = "The leather skull cap has two holes for a goblins ears to stick through.";
            armor.LongDescription = "The leather skull cap is padded to help protect the wearer from blows.";
            armor.ShortDescription = "A leather skull cap.";
            armor.KeyWords.Add("skull");
            armor.KeyWords.Add("cap");
            armor.KeyWords.Add("leather");

            return armor;
        }

        private IArmor Legs()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Legs;
            armor.Material = new Steel();
            armor.ExamineDescription = "Delicately carved gold inlays decorate this piece of museum quality piece of armor.";
            armor.LongDescription = "The steel leggings look to be more decorative than protective but will do the job when needed.";
            armor.ShortDescription = "A decorative pair of leggings.";
            armor.KeyWords.Add("leggings");
            armor.KeyWords.Add("decorative");

            return armor;
        }

        private IArmor Neck()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Neck;
            armor.Material = new Bone();
            armor.ExamineDescription = "The necklace bones appear to be of small animals, birds, squirls and mice.";
            armor.LongDescription = "The necklace looks like it once belonged to a shaman and has several animal bones strung on it.";
            armor.ShortDescription = "A bone necklace.";
            armor.KeyWords.Add("necklace");
            armor.KeyWords.Add("bone");

            return armor;
        }

        private IArmor Waist()
        {
            IArmor armor = Armor();
            armor.ItemPosition = AvalableItemPosition.Waist;
            armor.Material = new Cloth();
            armor.ExamineDescription = "The piece of rope looks unremarkable in every way.";
            armor.LongDescription = "A simple piece of rope for holding your trousers.";
            armor.ShortDescription = "A short piece of rope.";
            armor.KeyWords.Add("rope");

            return armor;
        }
        #endregion Items

        private void ConnectRooms()
        {
            zone.RecursivelySetZone();

            ZoneHelper.ConnectRoom(zone.Rooms[1], Direction.East, zone.Rooms[2], new DoorInfo("gate", "The gate drags across the dirt as it opens.", true, "The gate is made of sturdy wooden tree trunks and looks to be able to take a beating."));
            ZoneHelper.ConnectRoom(zone.Rooms[2], Direction.East, zone.Rooms[3]);
            ZoneHelper.ConnectRoom(zone.Rooms[3], Direction.East, zone.Rooms[4]);
            ZoneHelper.ConnectRoom(zone.Rooms[4], Direction.South, zone.Rooms[5], new DoorInfo("gate", "The gate drags across the dirt as it opens.", true, "The gate is made of flimsy sticks and acts more of a mental barrier than a physical one."));
            ZoneHelper.ConnectRoom(zone.Rooms[4], Direction.East, zone.Rooms[6]);
            ZoneHelper.ConnectRoom(zone.Rooms[6], Direction.North, zone.Rooms[7]);
            ZoneHelper.ConnectRoom(zone.Rooms[6], Direction.South, zone.Rooms[8]);
            ZoneHelper.ConnectRoom(zone.Rooms[6], Direction.East, zone.Rooms[9]);
            ZoneHelper.ConnectRoom(zone.Rooms[9], Direction.East, zone.Rooms[10]);
            ZoneHelper.ConnectRoom(zone.Rooms[9], Direction.North, zone.Rooms[11]);
            ZoneHelper.ConnectRoom(zone.Rooms[9], Direction.South, zone.Rooms[12]);
            ZoneHelper.ConnectRoom(zone.Rooms[10], Direction.North, zone.Rooms[13]);
            ZoneHelper.ConnectRoom(zone.Rooms[10], Direction.South, zone.Rooms[14]);
            ZoneHelper.ConnectRoom(zone.Rooms[10], Direction.East, zone.Rooms[15]);
        }
    }
}
