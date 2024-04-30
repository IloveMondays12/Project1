using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont timeFont;
        Texture2D bombTexture;
        Rectangle bombRect;
        float seconds;
        MouseState mouseState;
        SoundEffect explosion;
        SoundEffectInstance explosionInstance;
        bool exploded;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            bombRect = new Rectangle(50,50, 700, 400);
            _graphics.ApplyChanges();
            seconds = 0f;
            base.Initialize();
            exploded = false;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("TimeFont");
            explosion = Content.Load<SoundEffect>("explosion");
            explosionInstance = explosion.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                seconds = 0;
            }
            if (seconds >= 10 && !exploded)
            {
                explosionInstance.Play();
                exploded = true;
            }
            if (explosionInstance.State == SoundState.2 && exploded)
            {
                Exit();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(timeFont, (10 - seconds).ToString("0:00"), new Vector2(270,200), Color.DarkGoldenrod);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}