
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace StrangeWorld
{
    public class Player
    {


        bool isGround = true;
        public void setIsGround(bool isGround)
        {
            this.isGround = isGround;
        }
        private const float angularVelocity = 2* MathF.PI;
        private const float jumpPower = 500;

        public const int width = 50;
        public int getWidth()
        {
            return width;
        }
        public const int height = 50;
        public int getHeight()
        {
            return height;
        }
        private Vector2 velocity;
        private int moveDirection = 0; // 0 = no move 1 = move on left 2 = move on rigjt
        private Planet attracter;

        private float angle = 0;
        private Vector2 position;
        public void setPosition(float X,float Y)
        {
            position = new Vector2(X,Y);
        }
        private Texture2D texture;

        public Vector2 Position { get => position; }
        public float Angle { get => angle; }

        public Player(GraphicsDevice device)
        {
            createRectangle(width,height,device);
            isGround = false;
        }

        public Vector2 getCenter()
        {
            return new Vector2(position.X + (width/2),position.Y + (height/2));
        }

        public void setAttracter(Planet attracter)
        {
            this.attracter = attracter;
        }
        public Texture2D getTexture()
        {
            return texture;
        }
        

        public void createRectangle(int width,int height, GraphicsDevice device)
        {
            texture = new Texture2D(device, width, height);
            Color[] colorData = new Color[width * height];



            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    int index = x * width + y;
                    if(x == 0)
                    {
                        colorData[index] = Color.Red; 
                    }
                    else
                    {
                        colorData[index] = Color.Blue;  
                    }
                                      
                }
            }
            texture.SetData(colorData);
            
        }

        private Vector2 calculateForce()
        {   
            Vector2 beReturn;
            if(attracter == null)
            {
                beReturn = new Vector2(0,25*9.1f);
            }   
            else
            {
                Vector2 distance = attracter.getCenter() - getCenter();
                beReturn = distance;
            }
            return beReturn;
            
        }

        public Vector2 getCenterTexture()
        {
            return new Vector2(width/2,height/2);
        }

        private float getAngle()
        {
            Vector2 distance = attracter.getCenter() - getCenter();
            distance.Normalize();
            return MathF.Atan2(distance.Y, distance.X);
        }
        private void updateAngle(float gameTime)
        {
            float targetAngle = getAngle()- MathF.PI/2;
            
            if(MathF.Abs(angle - targetAngle) < 1)
            {
                angle = targetAngle;
            }
            if(targetAngle > angle )
            {
                angle += 2*MathF.PI * gameTime;
            }
            else
            {
                angle -= 2*MathF.PI * gameTime;
            }
      
        }
        private void updateVelocity(float gameTime)
        {
            velocity += gameTime * calculateForce() ;
        }

        public void setMoveDirection(int direction)
        {
            moveDirection = direction;
        }
        public bool isCollide(Planet planet,Vector2 nextPosition)
        {
            Vector2 dist = planet.getCenter() - (nextPosition);

            
            if(dist.Length() - planet.Size/2 - (getCenterTexture()/2).Length() <= 0) // is collide
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void jump()
        {
            if(isGround)
            {
                float angleOfJump = angle - MathF.PI/2;
                Vector2 jumpVectorNormal = new Vector2((float)Math.Cos(angleOfJump), (float)Math.Sin(angleOfJump));
                velocity = jumpVectorNormal * jumpPower;
                isGround = false;
            }



        }
        public void move(float gameTime)
        {
            if(!isGround)
            {
                updateVelocity(gameTime);

                
                Vector2 nextPosition = position;

                nextPosition.X += velocity.X * gameTime;
                nextPosition.Y += velocity.Y * gameTime;

                if(attracter != null)
                {
                    updateAngle(gameTime);
                    if(isCollide(attracter,nextPosition))
                    {
                        velocity = new Vector2(0,0);
                        Vector2 temp = attracter.getCenter() - (getCenter() - getCenterTexture());
                        temp.Normalize();
                        float angleBase = MathF.Atan2(temp.X, -temp.Y);
                        angle = angleBase- MathF.PI;
                        isGround = true;
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    position = nextPosition;
                }
                
                
            }
            else
            {
                //this.angle = getAngle()-MathF.PI/2;

                if(moveDirection == 1)
                {
                    angle+= gameTime * angularVelocity;
                    

                }
                else if(moveDirection == 2)
                {
                    angle-= gameTime * angularVelocity;
                }
                else
                {

                }
                float dist = attracter.Size/2 + getCenterTexture().Length()/2;
                

                Vector2 displacedPosition = new Vector2(dist * (float)MathF.Sin(angle), dist * -(float)MathF.Cos((angle)));


                position = attracter.getCenter() + displacedPosition;
            }
           

        }
    }

}