﻿using Minecraft.Math;
using OpenTK;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    public static class Structure
    {
        public static void MakeTree(Vector3i position, ConcurrentQueue<BlockMod> queue, int minHeight, int maxHeight)
        {
            int height = (int)((maxHeight - minHeight) * Noise.Get2DPerlinNoise(new Vector2(position.X, position.Z), 120f, 1f)) + minHeight;

            if (height < minHeight)
                height = minHeight;

                for (int i = 1; i < height; i++)
                    queue.Enqueue(new BlockMod(new Vector3i(position.X, position.Y + i, position.Z), 15));

            //Queue<BlockMod> _mods = new Queue<BlockMod>(queue.ToArray());
            /*List<BlockMod> _mods = new List<BlockMod>();
            for (int i = 0; i < queue.Count; i++)
                _mods.Add(queue.ElementAt(i));*/

            for (int x = -2; x <= 2; x++)
                for (int y = height - 2; y <= height + 1; y++)
                    for (int z = -2; z <= 2; z++)
                        if ((x != 0 || z != 0) || y >= height) {
                            Vector3i pos = new Vector3i(position.X + x, position.Y + y, position.Z + z);

                            if (queue.Where((BlockMod bm) => bm.Pos == pos && bm.Id == 15).Count() == 0)
                                queue.Enqueue(new BlockMod(pos, 16));
                        }
        }
    }
}
