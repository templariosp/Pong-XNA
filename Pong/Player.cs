using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Player
    {
        public const float PLAYER_VELOCITY = 5F;

        Vector2 position;
        Texture2D texture;
        Keys KeyUp;
        Keys KeyDown;
        Game game;

        public Player(Game game, Vector2 position, Texture2D texture, Keys keyUp, Keys keyDown)
        { 
            this.game = game;
            this.position = position;
            this.texture = texture;
            this.KeyUp = keyUp;
            this.KeyDown = keyDown;
        }

        public void Update()
        {
            // Captura estado do teclado para movimentação do jogador
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(KeyUp))
            {
                position.Y -= PLAYER_VELOCITY; // Move para cima
            }
            else if (keyboard.IsKeyDown(KeyDown))
            {
                position.Y += PLAYER_VELOCITY; // Move para baixo
            }

            var viewport = game.GraphicsDevice.Viewport;

            if(position.Y < 0)
            {
                position.Y = 0; // Previne de sair pela borda superior
            }
            else if (position.Y + texture.Height > viewport.Height)
            {
                position.Y = viewport.Height - texture.Height; // Previne de sair pela borda inferior
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Desenha o jogador na tela usando o SpriteBatch
            spriteBatch.Draw(texture, position, Color.White);
        }

        public Rectangle GetBounds()
        {
            // Retorna um retângulo representando os limites do jogador para detecção de colisões
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void HasColided(Ball ball)
        { 
            var ballBounds = ball.GetBounds();
            var playerBounds = GetBounds();

            if (playerBounds.Intersects(ballBounds))
            {
                if(ball.Velocity.X < 0)
                {
                    ball.SetPosition(position.X + texture.Width); // Reverte a posição do X para evitar que a bola fique presa dentro do jogador
                }
                else
                {
                    ball.SetPosition(position.X - texture.Width); // Reverte a posição do X para evitar que a bola fique presa dentro do jogador
                }

                ball.InvertVelocity(); // Reverte a direção da bola ao colidir com o jogador
            }
        }
    }
}
