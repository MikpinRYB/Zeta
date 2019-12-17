using System;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Zeta.Worldgen
{
    internal enum ConversionType
    {
        THUNDER,
    }

    class ConversionHandler
    {
        public static int startTfX = 1;
        public static int startTfY = 1;
        public static int genTfWidth = 1;
        public static void ConvertDown(int centerX, int y, int width, ConversionType convertType)
        {
            int worldSize = GetWorldSize();
            int biomeRadius = worldSize == 3 ? 350 : worldSize == 2 ? 280 : 250;
            biomeRadius /= 2;
            switch (convertType)
            {
                case ConversionType.THUNDER:
                    {
                        startTfX = centerX;
                        startTfY = y;
                        genTfWidth = width;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ConvertDownTfCallback), null);
                        break;
                    }
            }
        }

        public static int GetWorldSize()
        {
            switch (Main.maxTilesX)
            {
                case 4200:
                    return 1;

                case 6400:
                    return 2;

                case 8400:
                    return 3;

                default:
                    return 1;
            }
        }

        #region Thread Callback Stuff
        public static void ConvertDownTfCallback(object threadContext)
        {
            try
            {
                Do_ConvertDownTf(threadContext);
            }
            catch (Exception)
            {
            }
        }

        public static void Do_ConvertDownTf(object threadContext)
        {
            Dodo_ConvertDown(startTfX, startTfY, genTfWidth, ConversionType.THUNDER);
        }

        public static void Dodo_ConvertDown(int startX, int startY, int genWidth, ConversionType conversionType)
        {
            Mod mod = Zeta.instance;
            int tileGrass = 0, wallGrass = 0, tileStone = 0, wallStone = 0, tileSand = 0, tileSandHard = 0, wallSandHard = 0, tileSandstone = 0, wallSandstone = 0, tileIce = 0, tileThorn = 0;

            switch (conversionType)
            {
                case ConversionType.THUNDER:
                    {
                        tileGrass = mod.TileType("ThunderGrass");
                        wallGrass = mod.WallType("ThunderleafWall");
                        tileStone = mod.TileType("Sparkstone");
                        wallStone = mod.WallType("SparkstoneWall");
                        tileSand = mod.TileType("Sparksand");
                        tileSandHard = mod.TileType("SparksandHardened");
                        wallSandHard = mod.WallType("SparksandHardenedWall");
                        tileSandstone = mod.TileType("Sparksandstone");
                        wallSandstone = mod.WallType("SparksandstoneWall");
                        tileIce = mod.TileType("EnergeticIce");
                        break;
                    }

                default:
                    break;
            }

            int centerX = startX, y = startY;
            for (int x1 = 0; x1 < genWidth; x1++)
            {
                while (y < (Main.maxTilesY - 350))
                {
                    Convert(centerX + x1, y, genWidth, tileGrass, wallGrass, tileStone, wallStone, tileSand, tileSandHard, wallSandHard, tileSandstone, wallSandstone, tileIce, tileThorn);
                    y += (int)((double)genWidth * 1.5);
                }
            }
        }

        public static void Convert(int i, int j, int size, int tileGrass, int wallGrass, int tileStone, int wallStone, int tileSand, int tileSandHard, int wallSandHard, int tileSandstone, int wallSandstone, int tileIce, int tileThorn)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1))
                    {
                        int type = Main.tile[k, l].type;
                        int wall = Main.tile[k, l].wall;

                        if (wallGrass != 0 && WallID.Sets.Conversion.Grass[wall] && wall != wallGrass)
                        {
                            Main.tile[k, l].wall = (ushort)wallGrass;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (wallStone != 0 && WallID.Sets.Conversion.Stone[wall] && wall != wallStone)
                        {
                            Main.tile[k, l].wall = (ushort)wallStone;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (wallSandHard != 0 && WallID.Sets.Conversion.HardenedSand[wall] && wall != wallSandHard)
                        {
                            Main.tile[k, l].wall = (ushort)wallSandHard;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (wallSandstone != 0 && WallID.Sets.Conversion.Sandstone[wall] && wall != wallSandstone)
                        {
                            Main.tile[k, l].wall = (ushort)wallSandstone;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }

                        if (tileStone != 0 && (Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != tileStone)
                        {
                            Main.tile[k, l].type = (ushort)tileStone;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileGrass != 0 && TileID.Sets.Conversion.Grass[type] && type != tileGrass)
                        {
                            Main.tile[k, l].type = (ushort)tileGrass;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileIce != 0 && TileID.Sets.Conversion.Ice[type] && type != tileIce)
                        {
                            Main.tile[k, l].type = (ushort)tileIce;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileSand != 0 && TileID.Sets.Conversion.Sand[type] && type != tileSand)
                        {
                            Main.tile[k, l].type = (ushort)tileSand;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileSandHard != 0 && TileID.Sets.Conversion.HardenedSand[type] && type != tileSandHard)
                        {
                            Main.tile[k, l].type = (ushort)tileSandHard;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileSandstone != 0 && TileID.Sets.Conversion.Sandstone[type] && type != tileSandstone)
                        {
                            Main.tile[k, l].type = (ushort)tileSandstone;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                        else if (tileThorn != 0 && TileID.Sets.Conversion.Thorn[type] && type != tileThorn)
                        {
                            Main.tile[k, l].type = (ushort)tileThorn;
                            NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                        }
                    }
                }
            }
        }
    }
    #endregion
}