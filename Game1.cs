using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace shootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;

        Texture2D target_Sprite;
        Texture2D crosshairs_Sprite;
        Texture2D background_Sprite;

        SpriteFont gameFont;

        Vector2 targetPosition = new Vector2(300, 300);
        const int TARGET_RADIUS = 45;
        MouseState mState;
        float mouseTargetDist;
        bool mReleased = true;
       
        int score =  0;
        float timer = 10f;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            target_Sprite = Content.Load<Texture2D>("target");
            crosshairs_Sprite = Content.Load<Texture2D>("crosshairs");
            background_Sprite = Content.Load<Texture2D>("sky");

            gameFont = Content.Load<SpriteFont>("galleryFont");

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (timer > 0)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            // TODO: Add your update logic here
            mState = Mouse.GetState();

           

            mouseTargetDist = Vector2.Distance(targetPosition, new Vector2(mState.X, mState.Y));

            if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            {
                if ( mouseTargetDist < TARGET_RADIUS && timer > 0 )
                {
                    score++;

                    Random rand = new Random();
                    targetPosition.X = rand.Next(TARGET_RADIUS,graphics.PreferredBackBufferWidth + 1);
                    targetPosition.Y = rand.Next(TARGET_RADIUS,graphics.PreferredBackBufferHeight + 1);
                }
                mReleased = false;

            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

                    base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(background_Sprite, new Vector2(0, 0), Color.White);

            if (timer > 0)
            {

                _spriteBatch.Draw(target_Sprite, new Vector2(targetPosition.X - TARGET_RADIUS, targetPosition.Y - TARGET_RADIUS), Color.White);
            }

            _spriteBatch.DrawString(gameFont, "Score :  " + score.ToString(), new Vector2(3, 3), Color.White);

            _spriteBatch.DrawString(gameFont, "Time :  " + Math.Ceiling(timer).ToString(), new Vector2(3, 40), Color.White);

            _spriteBatch.Draw(crosshairs_Sprite, new Vector2(mState.X -25, mState.Y -25), Color.White);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
