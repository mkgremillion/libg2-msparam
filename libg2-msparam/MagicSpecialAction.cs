using System;
using System.IO;
using System.Text;

namespace libg2_msparam
{
    public class MagicSpecialAction
    {
        private byte id;
        public byte Id { get { return id; } }
        public byte Icon { get; set; }

        public const int LENGTH_NAME = 18;
        private string _name;
        public string Name
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
        public string NameWithPadding
        {
            get
            {
                return Name.PadLeft(LENGTH_NAME);
            }
        }

        public byte[] NameAsBytes
        {
            get
            {
                return Encoding.ASCII.GetBytes(NameWithPadding);
            }
        }

        public ushort PointCost { get; set; }
        public byte TargetEffect { get; set; }
        public byte TargetType { get; set; }
        public ushort AttackStatBonus { get; set; }
        public ushort PowerMultiplier { get; set; }
        public ushort AreaSize { get; set; }
        public ushort ChargeTimeAtLevel1 { get; set; }
        public ushort ChargeTimeAtLevel5 { get; set; }
        public ushort RecoveryTime { get; set; }
        public ushort Animation { get; set; }
        public short Unknown { get; set; }
        public short IpDamage { get; set; }
        public short CancelDamage { get; set; }
        public short Knockback { get; set; }
        public byte Element { get; set; }
        public byte ElementModifier { get; set; }
        public byte Ailments { get; set; }
        public byte AilmentChance { get; set; }
        public sbyte PowModifier { get; set; }
        public sbyte DefModifier { get; set; }
        public sbyte ActModifier { get; set; }
        public sbyte MovModifier { get; set; }
        public ushort SpecialEffect { get; set; }
        public ushort[] CoinCost { get; set; }
        public ushort PowerPerLevel { get; set; }

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
        public string DescriptionWithPadding
        {
            get
            {
                return Description.PadRight(LENGTH_DESCRIPTION);
            }
        }
        public byte[] DescriptionAsBytes
        {
            get
            {
                return Encoding.ASCII.GetBytes(DescriptionWithPadding);
            }
        }

        public enum IconValue : byte
        {
            None, Fire, Wind, Earth, Lightning, Blizzard, Water,
            Explosion, Forest, Light, Darkness, Sword, Staff, Crossbow, Dagger, Poleaxe, Chakram, Shoe
        }
        public enum TargetEffectValue : byte { RestoreHP, RestoreMP, RestoreSP, StatModifierOnly, Damage, Damage2 }
        public enum TargetTypeValue : byte
        {
            OneFriend, SomeFriends, AllFriends, OneEnemy, SomeEnemies, AllEnemies,
            EnemyLine, SelfOnly, SpecialOneEnemy, SpecialSelfCenteredArea1, SpecialSelfOnly, SpecialSelfCenteredArea2,
            SpecialSelfCenteredArea3, SpecialOneEnemyInvisible, SpecialEveryone
        }
        public enum ElementValue : byte { Fire, Wind, Earth, Lightning, Blizzard, Unknown, Shatter }
        public enum AilmentFlags : byte
        {
            Poison = 1, Sleep = 2, Paralysis = 4, Confusion = 8, Plague = 16,
            MagicBlock = 32, MoveBlock = 64, Death = 128
        }
        public enum SpecialEffectValue : ushort
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
                writer.Write(NameAsBytes);
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
                writer.Write(DescriptionAsBytes);
            }
        }
    }
}
