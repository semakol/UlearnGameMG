﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UlearnGameMG
{
    public abstract class GameObject : ITexturable
    {
        public Point position;
        public int Hp;
        public bool canUse = false;

        public string textureName { get; set; }
        public Texture2D texture { get; set; }

        public void Move(Point point) => position = point;

        public void TextureLoad(Texture2D texture)
        {
            this.texture = texture;
        }
    }

    public class Character : GameObject
    {
        public string Name;
        public int move = 5;
        public Spell FirstSpell = Spell.Shot();
        public Spell SecondSpell;
        public List<Point> canMove;
        public List<Point> canCast;
        public bool moveDo = false;
        public bool castDo = false;

        public Character(string Name, Point pos, int hp, string textureName)
        {
            Hp = hp;
            this.textureName = textureName;
            position = pos;
            this.Name = Name;
            canUse = true;
        }

        public void Heal() => Hp++;

    }

    public class Enemies : GameObject
    {
        public string Name;
        public int move = 5;
        public Spell FirstSpell = Spell.Shot();
        public Spell SecondSpell;

        public Enemies(string Name, Point pos, int hp)
        {
            Hp = hp;
            this.texture = texture;
            position = pos;
            this.Name = Name;
            canUse = false;
        }

        public void Heal() => Hp++;

    }

    public class Barricade : GameObject
    {
        public Barricade(Point pos, int hp, string textureName)
        {
            position = pos;
            this.textureName = textureName;
            Hp = hp;
        }
    }

    public class Spell
    {
        public string Name;
        public List<Point> atacksPoints;
        public List<(Point, int)> splashPoints;

        public Spell(string name, List<Point> atPoints, List<(Point, int)> spPoints)
        {
            Name = name;
            atacksPoints = atPoints;
            splashPoints = spPoints;
        }

        public static Spell Shot()
        {
            var atPoints = new List<Point>();
            for (int i = -8; i<8; i++)
            {
                if (i == 0) continue;
                atPoints.Add(new Point(0, i));
                atPoints.Add(new Point(i, 0));
            }
            var spPoints = new List<(Point, int)>();
            spPoints.Add((new(0, 1), 1));
            spPoints.Add((new(1, 0), 1));
            spPoints.Add((new(0, -1), 1));
            spPoints.Add((new(-1, 0), 1));
            return new Spell("Shot", atPoints, spPoints);
        }
    }
}
