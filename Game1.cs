using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogame_topic4
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bombTexture;
        Texture2D exlosionTexture;
        SpriteFont Time;

        MouseState mouseState;

        SoundEffect explode;
        
        float seconds;
        float startTime;

        bool exploded;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            exploded = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            Time = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            exlosionTexture = Content.Load<Texture2D>("explosion.png");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (seconds >= 15 && ! exploded)
            {
                explode.Play();
                exploded = true;
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (!exploded)
            {
                _spriteBatch.Draw(bombTexture, new Rectangle(50, 50, 700, 400), Color.White);
                _spriteBatch.DrawString(Time, (15 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            }
            else if (exploded)
            {
                _spriteBatch.Draw(exlosionTexture, new Vector2(0,0), Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}