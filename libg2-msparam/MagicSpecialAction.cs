using System;
using System.IO;

namespace libg2_msparam
{
    public class MagicSpecialAction
    {
        private Byte id;
        public Byte Id { get { return id; } }
        public Byte Icon { get; set; }

        public const int LENGTH_NAME = 18;
        private String _name;
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length > LENGTH_NAME)
                {
                    _name = value.Remove(LENGTH_NAME);
                }
                else
                {
                    _name = value;
                }
            }
        }
        public String PaddedName
        {
            get
            {
                return Name.PadLeft(LENGTH_NAME);
            }
        }

        public UInt16 PointCost { get; set; }
        public Byte TargetEffect { get; set; }
        public Byte TargetType { get; set; }
        public UInt16 AttackStatBonus { get; set; }
        public UInt16 PowerMultiplier { get; set; }
        public UInt16 AreaSize { get; set; }
        public UInt16 ChargeTimeAtLevel1 { get; set; }
        public UInt16 ChargeTimeAtLevel5 { get; set; }
        public UInt16 RecoveryTime { get; set; }
        public UInt16 Animation { get; set; }
        public Int16 Unknown { get; set; }
        public Int16 IpDamage { get; set; }
        public Int16 CancelDamage { get; set; }
        public Int16 Knockback { get; set; }
        public Byte Element { get; set; }
        public Byte ElementModifier { get; set; }
        public Byte Ailments { get; set; }
        public Byte AilmentChance { get; set; }
        public SByte PowModifier { get; set; }
        public SByte DefModifier { get; set; }
        public SByte ActModifier { get; set; }
        public SByte MovModifier { get; set; }
        public UInt16 SpecialEffect { get; set; }
        public UInt16[] CoinCost { get; set; }
        public UInt16 PowerPerLevel { get; set; }

        public const int LENGTH_DESCRIPTION = 40;
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value.Length > LENGTH_DESCRIPTION)
                {
                    _description = value.Remove(LENGTH_DESCRIPTION);
                }
                else
                {
                    _description = value;
                }
            }
        }
        public String PaddedDescription
        {
            get
            {
                return Description.PadRight(LENGTH_DESCRIPTION);
            }
        }

        public enum IconValue : Byte
        {
            None, Fire, Wind, Earth, Lightning, Blizzard, Water,
            Explosion, Forest, Light, Darkness, Sword, Staff, Crossbow, Dagger, Poleaxe, Chakram, Shoe
        }
        public enum TargetEffectValue : Byte { RestoreHP, RestoreMP, RestoreSP, StatModifierOnly, Damage, Damage2 }
        public enum TargetTypeValue : Byte
        {
            OneFriend, SomeFriends, AllFriends, OneEnemy, SomeEnemies, AllEnemies,
            EnemyLine, SelfOnly, SpecialOneEnemy, SpecialSelfCenteredArea1, SpecialSelfOnly, SpecialSelfCenteredArea2,
            SpecialSelfCenteredArea3, SpecialOneEnemyInvisible, SpecialEveryone
        }
        public enum ElementValue : Byte { Fire, Wind, Earth, Lightning, Blizzard, Unknown, Shatter }
        public enum AilmentFlags : Byte
        {
            Poison = 1, Sleep = 2, Paralysis = 4, Confusion = 8, Plague = 16,
            MagicBlock = 32, MoveBlock = 64, Death = 128
        }
        public enum SpecialEffectValue : UInt16
        {
            Gravity = 0x0C, RemoveDebuffs = 0x0D, SpellbindingEye = 0x0E, RemoveBuffs = 0x16,
            RandomDebuff = 0x17, Lifesteal = 0x28, DamageMP = 0x32
        }

        private MagicSpecialAction(FileStream stream)
        {
            LoadFromBinary(stream);
        }

        public void LoadFromBinary(FileStream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                id = reader.ReadByte();
                Icon = reader.ReadByte();
                Name = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(LENGTH_NAME));
                PointCost = reader.ReadUInt16();
                TargetEffect = reader.ReadByte();
                TargetType = reader.ReadByte();
                AttackStatBonus = reader.ReadUInt16();
                PowerMultiplier = reader.ReadUInt16();
                AreaSize = reader.ReadUInt16();
                ChargeTimeAtLevel1 = reader.ReadUInt16();
                ChargeTimeAtLevel5 = reader.ReadUInt16();
                RecoveryTime = reader.ReadUInt16();
                Animation = reader.ReadUInt16();
                Unknown = reader.ReadInt16();
                IpDamage = reader.ReadInt16();
                CancelDamage = reader.ReadInt16();
                Knockback = reader.ReadInt16();
                Element = reader.ReadByte();
                ElementModifier = reader.ReadByte();
                Ailments = reader.ReadByte();
                AilmentChance = reader.ReadByte();
                PowModifier = reader.ReadSByte();
                DefModifier = reader.ReadSByte();
                ActModifier = reader.ReadSByte();
                MovModifier = reader.ReadSByte();
                SpecialEffect = reader.ReadUInt16();
                for(int i = 0; i < 5; i++)
                {
                    CoinCost[i] = reader.ReadUInt16();
                }
                PowerPerLevel = reader.ReadUInt16();
                Description = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(LENGTH_DESCRIPTION));
            }
        }

        public void WriteToBinary(FileStream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(id);
                writer.Write(Icon);
                writer.Write(System.Text.Encoding.ASCII.GetBytes(PaddedName));
                writer.Write(PointCost);
                writer.Write(TargetEffect);
                writer.Write(TargetType);
                writer.Write(AttackStatBonus);
                writer.Write(PowerMultiplier);
                writer.Write(AreaSize);
                writer.Write(ChargeTimeAtLevel1);
                writer.Write(ChargeTimeAtLevel5);
                writer.Write(RecoveryTime);
                writer.Write(Animation);
                writer.Write(Unknown);
                writer.Write(IpDamage);
                writer.Write(CancelDamage);
                writer.Write(Knockback);
                writer.Write(Element);
                writer.Write(ElementModifier);
                writer.Write(Ailments);
                writer.Write(AilmentChance);
                writer.Write(PowModifier);
                writer.Write(DefModifier);
                writer.Write(ActModifier);
                writer.Write(MovModifier);
                writer.Write(SpecialEffect);
                for(int i = 0; i < 5; i++)
                {
                    writer.Write(CoinCost[i]);
                }
                writer.Write(PowerPerLevel);
                writer.Write(System.Text.Encoding.ASCII.GetBytes(PaddedDescription));
            }
        }
    }
}
