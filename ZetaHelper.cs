using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace zeta
{
	public class ZetaHelper
	{
		public static int Clamp(int v1, int minValue, int maxValue)
		{
			if (v1 < minValue)
			{
				v1 = minValue;
			}
			if (v1 > maxValue)
			{
				v1 = maxValue;
			}
			return v1;
		}

		public static float ClampFloat(float v1, float minValue, float maxValue)
		{
			if (v1 < minValue)
			{
				v1 = minValue;
			}
			if (v1 > maxValue)
			{
				v1 = maxValue;
			}
			return v1;
		}

		public static double ClampDouble(double v1, double minValue, double maxValue)
		{
			if (v1 < minValue)
			{
				v1 = minValue;
			}
			if (v1 > maxValue)
			{
				v1 = maxValue;
			}
			return v1;
		}

        public static ulong ClampULong(ulong v1, ulong minValue, ulong maxValue)
        {
            if (v1 < minValue)
            {
                v1 = minValue;
            }
            if (v1 > maxValue)
            {
                v1 = maxValue;
            }
            return v1;
        }

		public static int Choose(int num1, int num2)
		{
			if (Main.rand.Next(1,5) == 3)
			{
				return num1;
			}
			return num2;
		}

		public static int Difference(int num1, int num2)
		{
			return (num1 - num2);
		}

        public static float GetDistance(float v2, float v3)
		{
			return (v2 - v3);
		}

		public static float RotateTowards(float v4, float v5)
		{
			return (float)Math.Atan2(v4, v5);
		}

		public static int ShiftChance(bool boss)
		{
			if (boss)
			{
				return Main.rand.Next(1,4);
			}
			return Main.rand.Next(1,25);
		}

        public static int ReverseNegativeInt(int val)
        {
            return (val - val) - val;
        }

        public static float ReverseNegative(float val)
        {
            return (val - val) - val;
        }

		public static Vector2 MoveTowardsPlayer(float speed, float currentX, float currentY, Player player, Vector2 issue, int direction)
		{
			float num1 = speed;
			Vector2 vector3 = new Vector2(issue.X + (float)(direction * 20), issue.Y + 6f);
			float num2 = player.position.X + (float)player.width * 0.5f - vector3.X;
			float num3 = player.Center.Y - vector3.Y;
			float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
			float num5 = num1 / num4;
			num2 *= num5;
			num3 *= num5;
			currentX = (currentX * 58f + num2) / 58.8f;
			currentY = (currentY * 58f + num3) / 58.8f;
			return new Vector2(currentX, currentY);
		}
	}
}