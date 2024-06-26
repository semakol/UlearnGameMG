﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UlearnGameMG
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }

    public class GameInterface
    {
        public List<IDrawable> objects = new();
        public List<ITexturable> textureObjs = new();
        public Dictionary<string, Button> buttons = new();

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obj in objects)
            {
                obj.Draw(spriteBatch);
            }
        }

        public void Update()
        {
            foreach (var button in buttons)
            {
                button.Value.ClickButton();
            }
        }

        public void AddButton(Button button)
        {
            buttons.Add(button.Name, button);
            objects.Add(button);
            textureObjs.Add(button);
        }

        public List<ITexturable> GetTexturables() { return textureObjs; }

        public class Button : IDrawable, ITexturable
        {
            public Point point;
            public string Name;
            public Rectangle rect;
            public Texture2D texture { get; set; }
            public Action action;
            public string textureName { get; set; }

            public Button(Point point, string textureName, string name)
            {
                this.point = point;
                this.textureName = textureName;
                Name = name;
            }

            public void TextureLoad(Texture2D texture)
            {
                rect = new Rectangle(point.X, point.Y, texture.Width, texture.Height);
                this.texture = texture;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(
                texture,
                rect,
                new Color(255, 255, 255)
                );
            }

            public void SetAction(Action action)
            {
                this.action = action;
            }

            public bool EnterButton()
            {
                if (rect.Contains(InputManager.mousePos))
                {
                    return true;
                }
                return false;
            }

            public bool ClickButton()
            {
                if (InputManager.JustPressed(MouseInput.LeftButton) && EnterButton())
                {
                    action.Invoke();
                    return true;
                }
                return false;
            }
        }

        static public GameInterface InGame { get
        {
            var result = new GameInterface();
            result.AddButton(new Button(new Point(10, 10), "interface/Next_Turn", "NextTurn"));
            return result;
        } }

        static public GameInterface EndGame
        {
            get
            {
                var result = new GameInterface();
                result.AddButton(new Button(new Point(10, 540), "interface/Next_level", "NextLevel"));
                result.AddButton(new Button(new Point(10, 600), "interface/Restart", "Restart"));
                result.AddButton(new Button(new Point(10, 660), "interface/Main_menu", "MainMenu"));
                return result;
            }
        }

        static public GameInterface InMenu
        {
            get
            {
                var result = new GameInterface();
                result.AddButton(new Button(new Point(20, 400), "interface/Start_game", "StartGame"));
                result.AddButton(new Button(new Point(20, 460), "interface/Exit", "Exit"));
                result.AddButton(new Button(new Point(20, 640), "interface/1920", "S1920"));
                result.AddButton(new Button(new Point(160, 640), "interface/1280", "S1280"));
                return result;
            }
        }
    }
}
