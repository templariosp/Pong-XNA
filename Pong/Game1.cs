using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Pong : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player player1;
        Player player2;
        Ball ball;

        Texture2D goalTexture;
        Point centerScreen;

        public Pong()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Lógica de inicialização do jogo
            centerScreen = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Carregamento de conteúdo do jogo
            goalTexture = Content.Load<Texture2D>("assets/goal");

            Texture2D barTexture = Content.Load<Texture2D>("assets/bar");
            Texture2D ballTexture = Content.Load<Texture2D>("assets/ball");

            player1 = new Player(this, new Vector2(10, 100), barTexture, Keys.W, Keys.S);
            player2 = new Player(this, new Vector2(760, 100), barTexture, Keys.Up, Keys.Down);
            ball = new Ball(this, ballTexture);
            ball.SetInStartPosition();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Atualização da lógica do jogo, como movimentação dos jogadores, colisões, etc.
            if (ball.OutOfBounds)
            { 
                if(Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    ball.SetInStartPosition();
                }
            }

            player1.Update();
            player2.Update();
            ball.Update();

            player1.HasColided(ball);
            player2.HasColided(ball);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MonoGameOrange);

            // Desenho dos elementos do jogo na tela
            _spriteBatch.Begin();

            player1.Draw(_spriteBatch);
            player2.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);

            if(ball.OutOfBounds)
            {
                _spriteBatch.Draw(goalTexture, new Vector2(centerScreen.X - (goalTexture.Width / 2), centerScreen.Y - (goalTexture.Height / 2)), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
