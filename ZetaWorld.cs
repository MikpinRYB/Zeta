using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Zeta.Tiles;
using Terraria.Utilities;
using Terraria.Localization;
using Zeta.Walls;

namespace Zeta
{
    internal partial class ZetaWorld : ModWorld
    {
		public static int zetaTiles = 0;
        internal void ThunderflowGen(GenerationProgress progress)
        {
            
            progress.Message = "Zeta: Thunderflow";
            //actual world gen
            if (WorldGen.gen)
            {
                for (int i = 0; i < 10; i++)
                {
                    TerrainTop(progress);
                }
            }
            //Debug stick gen
            else
            {
                TerrainTop(progress);
            }
           
            SpreadingGrass(progress);
            
            
            
            
        }
		public override TagCompound Save()
		{
			var downed = new List<string>();
		return new TagCompound {
				{"downed", downed}
			};
	
        }
		     internal void TerrainTop(GenerationProgress progress)
        {
            //Creates the Grass/Biome area for the variant.
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = Main.maxTilesY - 200; j < Main.maxTilesY - 195; j++)
                {
                    if (WorldGen.genRand.Next(20) == 0)
                    {
                        WorldGen.TileRunner(i, j, (int)WorldGen.genRand.Next(4, 6), 50, (2), true);
                    }
                }
            }

            //Replaces area with respective variants.
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = Main.maxTilesY - 200; j < Main.maxTilesY; j++)
                {
                    if (Main.tile[i, j].type != (2))
                    {
                        Main.tile[i, j].active(false);
                        Main.tile[i, j].liquid = 0;
                        Main.tile[i, j].wall = 0;
                        Main.tile[i, j].slope(0);
                    }
                }
            }
        }
		 public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
			  var index = tasks.FindIndex(x => x.Name == "Planting Trees");
			
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
		

            if (genIndex == -1)
            {
                return;
            }
           tasks.Insert(genIndex + 2, new PassLegacy("Thunderflow", delegate (GenerationProgress progress)
            { 
                progress.Message = "Zeta: Thunderflow";
                ushort grassType = (ushort) mod.TileType("ThunderflowGrass");
				int i = WorldGen.genRand.Next(0, Main.maxTilesX);
            for (i = 300; i < 500; i++)
            {

                for (int j = (int)WorldGen.worldSurfaceLow; j < (int)WorldGen.worldSurfaceLow+500; j++)
                {
                    if (Main.tile[i, j].type == 0)
                    {
                        Main.tile[i, j].wall = 0;
                        WorldGen.SpreadGrass(i, j, 0, grassType, true);
                    }
                }
			}
		}));
		}
		 public override void TileCountsAvailable(int[] tileCounts)
        {
            zetaTiles = tileCounts[mod.TileType("ThunderflowGrass")];       //Makes the public static int "customBiome" count as "customtileblock".
        }
        private void SpreadingGrass(GenerationProgress progress)
        {
            ushort dirtType = (ushort) (2);
            ushort grassType = (ushort) mod.TileType("ThunderflowGrass");
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                float percent = (float)(i / Main.maxTilesX);
                progress.Set(percent);
                for (int j = Main.maxTilesY - 200; j < Main.maxTilesY; j++)
                {
                    if (Main.tile[i, j].type == dirtType)
                    {
                        Main.tile[i, j].wall = 0;
                        WorldGen.SpreadGrass(i, j, dirtType, grassType, true);
                    }
                }
            }

            for (int i = 0; i < Main.maxTilesX; i++)
            {
                float percent = (float)(i / Main.maxTilesX);
                progress.Set(percent);
                for (int j = Main.maxTilesY - 200; j < Main.maxTilesY; j++)
                {
                    if (Main.tile[i, j].type == grassType)
                    {
                        Main.tile[i, j].wall = 0;
                    }
                }
            }
        }

       
        }

      


    }

