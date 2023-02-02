using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StrangeWorld
{
    public class Planet
    {
        private int size = 150;
        private float gravity;
        private Texture2D texture;
        private Vector2 position;

        public void setSize(int radius)
        {
            size = radius;
        }
        public Vector2 getCenter()
        {
            return new Vector2(position.X + size/2,position.Y + size/2);
        }
        public void createCircleText(int radius, GraphicsDevice device)
        {
            Texture2D aTexture = new Texture2D(device, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            size = radius;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = Color.White;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }
            aTexture.SetData(colorData);
            texture = aTexture;
        }
        public Texture2D Texture { get => texture;}
 
        public int Size { get => size;}

        public Vector2 Position { get => position; }
        public void setPosition(float x, float y)
        {
            position.X = position.X + x;
            position.Y = position.Y + y;
        }
         public void changePosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

       
    }
}
