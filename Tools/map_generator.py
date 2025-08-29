#!/usr/bin/env python3
# Simple optional map generator to produce a new JSON grid.

import argparse, json, random

def gen(w, h, seed=None):
    rnd = random.Random(seed)
    cells = []
    for y in range(h):
        for x in range(w):
            if x == 0 or y == 0 or x == w-1 or y == h-1:
                t = "wall"
            else:
                t = "floor"
            cells.append({"type": t})
    # place player
    px, py = rnd.randint(1, w-2), rnd.randint(1, h-2)
    cells[py*w + px]["type"] = "player"
    # place doors
    for _ in range(max(2, (w*h)//50)):
        dx, dy = rnd.randint(1, w-2), rnd.randint(1, h-2)
        cells[dy*w + dx]["type"] = "door"
    # place enemies
    for _ in range(max(5, (w*h)//20)):
        ex, ey = rnd.randint(1, w-2), rnd.randint(1, h-2)
        idx = ey*w + ex
        if cells[idx]["type"] == "floor":
            cells[idx]["type"] = "enemy"
    return {"width": w, "height": h, "cells": cells}

def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--width", type=int, default=16)
    ap.add_argument("--height", type=int, default=16)
    ap.add_argument("--seed", type=int, default=42)
    ap.add_argument("--out", type=str, default="Assets/StreamingAssets/Configs/level1.json")
    args = ap.parse_args()
    data = gen(args.width, args.height, args.seed)
    with open(args.out, "w", encoding="utf-8") as f:
        json.dump(data, f, ensure_ascii=False, indent=2)
    print("Wrote", args.out)

if __name__ == "__main__":
    main()
