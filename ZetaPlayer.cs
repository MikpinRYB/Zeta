using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Zeta
{
    public class ZetaPlayer : ModPlayer
    {
        private const int saveVersion = 0;
		public bool geodeMinion;
	public bool ZoneThunder = false;

	public override void UpdateBiomes()
        {
            ZoneThunder = (ZetaWorld.zetaTiles > 1);    
        }
		
		public override Texture2D GetMapBackgroundImage()
		{
			if (ZoneThunder)
			{
				return mod.GetTexture("Backgrounds/ThunderBg");
			}
			return null;
		}

		public override bool CustomBiomesMatch(Player other)
		{
			ZetaPlayer modOther = other.GetModPlayer<ZetaPlayer>();
			return ZoneThunder == modOther.ZoneThunder;
		}

		public override void CopyCustomBiomesTo(Player other)
		{
			ZetaPlayer modOther = other.GetModPlayer<ZetaPlayer>();
			modOther.ZoneThunder = ZoneThunder;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = ZoneThunder;
			writer.Write(flags);
		}

		public override void ReceiveCustomBiomes(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			ZoneThunder = flags[0];
		}
    }
}