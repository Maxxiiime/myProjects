using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace StrangeWorld
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private List<Planet> itsPlanet;
        public float itsTime = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
             _spriteBatch = new SpriteBatch(GraphicsDevice);
            itsPlanet = new List<Planet>();
            // crreate first planet
            Planet a = new Planet();
            a.changePosition(1100,200);
            itsPlanet.Add(a);
            itsPlanet[0].createCircleText(itsPlanet[0].Size, GraphicsDevice);

            player = new Player(GraphicsDevice);
            player.setAttracter(itsPlanet[0]);
            player.setPosition(itsPlanet[0].Position.X - player.getWidth(),itsPlanet[0].getCenter().Y);


            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //loadPlanet(itsPlanet);


           
        }

        void playerAttract()
        {
            bool notFound = true;
            //player.setAttracter(null);
            for (int i = 0; i < itsPlanet.Count; i++)
            {
                Planet p = new Planet();
                p.setSize(itsPlanet[i].Size + 2*player.getWidth());
                p.changePosition(itsPlanet[i].Position.X- player.getWidth(),itsPlanet[i].Position.Y - player.getWidth());


                if(player.isCollide(p,player.Position))
                {
                    player.setAttracter(itsPlanet[i]);
                    System.Console.WriteLine(i);
                    notFound = false;
                }
            }
            if(notFound)
            {
                player.setAttracter(null);
                player.setIsGround(false);
            }
        }

        void manageInput()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Space))
            {
                player.jump();
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                player.setMoveDirection(1);
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                player.setMoveDirection(2);
            }
            else
            {
                player.setMoveDirection(0);
            }

        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Random randNumber = new Random();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds/1000;
            
            manageInput();

            updateDisplayPlanet(deltaTime);
            player.move(deltaTime);
            playerAttract();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            foreach(Planet planet in itsPlanet)
            {
                _spriteBatch.Draw(planet.Texture, planet.Position, Color.White);
            }
            _spriteBatch.Draw(player.getTexture(),new Rectangle((int)player.Position.X,(int)player.Position.Y,player.getWidth(),player.getHeight()),null,Color.White,player.Angle,player.getCenterTexture(),SpriteEffects.None,1);
          
            _spriteBatch.End();
        

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        public void loadPlanet(List<Planet> aPlanet)
        {
            Random randomNumber = new Random();
           
            float randomY = 0;

            for (int i = 0; i < aPlanet.Count; i++)
            {
                if (i == 0)
                {
                    aPlanet[i].setPosition(randomNumber.Next(200, 350), randomNumber.Next(50, 350));
                }
                else
                {
                    do
                    {
                        randomY = aPlanet[i - 1].Position.Y + randomNumber.Next(-300, 300);
                    } while (randomY < 0 || randomY > 400);

                    aPlanet[i].setPosition(aPlanet[i -1].Position.X + 200, randomY);

                }

                aPlanet[i].createCircleText(aPlanet[i].Size, GraphicsDevice);
            }
        }

        public void updateDisplayPlanet(float aDelataTime)
        {

            Random randNumber = new Random();
            int futurSize;

           if(itsTime > 2.5)
            {
               
                   
                    Planet a = new Planet();
                    futurSize = randNumber.Next(100, 200);
                    a.setPosition(1300, randNumber.Next(75, 200));

                    itsPlanet.Add(a);
                    a.createCircleText(futurSize, GraphicsDevice);

                
                itsTime = 0;

            }
           else
            {
                itsTime += aDelataTime;
            }
            for (int i = 0; i < itsPlanet.Count; i++)
            {
                itsPlanet[i].setPosition(-180*aDelataTime, 0);
                if(itsPlanet[i].Position.X + itsPlanet[i].Size < 0)
                {
                    itsPlanet.RemoveAt(i);
                    i--;
                }
            }

        }
    }
}
