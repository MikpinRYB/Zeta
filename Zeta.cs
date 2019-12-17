using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Zeta
{
	class Zeta : Mod
	{
		internal static Zeta instance;
		public Zeta()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
			           instance = this;
		}
		public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
		{
			if (ZetaWorld.zetaTiles > 0)
			{
				float zetaStrength = ZetaWorld.zetaTiles / 200f;
				zetaStrength = Math.Min(zetaStrength, 1f);

				int sunR = backgroundColor.R;
				int sunG = backgroundColor.G;
				int sunB = backgroundColor.B;
				// Remove some green and more red.
				sunB -= (int)(180f * zetaStrength * (backgroundColor.B / 255f));
				//sunG -= (int)(90f * zetaStrength * (backgroundColor.G / 255f));
				sunR = Utils.Clamp(sunR, 15, 255);
				sunG = Utils.Clamp(sunG, 15, 255);
				sunB = Utils.Clamp(sunB, 15, 255);
				backgroundColor.R = (byte)sunR;
				backgroundColor.G = (byte)sunG;
				backgroundColor.B = (byte)sunB;
			}
			

			
			}
					
		}
		
		 
		
	}