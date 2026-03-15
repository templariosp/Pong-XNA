using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Ball
    {
        public const float BALL_VELOCITY = 2.5F;

        Texture2D texture;
        Game game;
        Vector2 position;
        Vector2 velocity;
        bool outOfBounds = false;

        public bool OutOfBounds { get => outOfBounds; }
        public Vector2 Velocity { get => velocity; }

        public Ball(Game game, Texture2D texture)
        {
            this.game = game;
            this.texture = texture;
        }

        public void Update()
        {
            // Coleta a posição atual da bola e verifica se ela colidiu com as bordas superior ou inferior da tela, invertendo a direção vertical se necessário
            var viewport = game.GraphicsDevice.Viewport;

            if (position.Y < 0)
            {
                position.Y = 0; // Previne a bola de sair pela borda superior
                velocity.Y *= -1;
            }
            else if (position.Y + texture.Height > viewport.Height)
            {
                position.Y = viewport.Height - texture.Height; // Previne a bola de sair pela borda inferior
                velocity.Y *= -1;
            }

            position += velocity; // Move a bola de acordo com sua velocidade

            if (position.X + texture.Width < 0 || position.X > viewport.Width)
            {
                outOfBounds = true; // Reseta a bola para a posição inicial se ela sair pelos lados esquerdo ou direito da tela
                velocity = Vector2.Zero; // Para a bola para evitar que ela continue se movendo enquanto estiver fora dos limites
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Desenha a bola na tela usando o SpriteBatch
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void SetInStartPosition()
        {
            // Seta a posição inicial da bola no centro da tela
            var viewport = game.GraphicsDevice.Viewport;
            position.Y = (viewport.Height / 2) - (texture.Height / 2);
            position.X = (viewport.Width / 2) - (texture.Width / 2);

            velocity = new Vector2(BALL_VELOCITY); // Velocidade inicial para a direita
            outOfBounds = false; // Reseta o estado de fora dos limites
        }

        public Rectangle GetBounds()
        {
            // Retorna um retângulo representando os limites da bola para detecção de colisões
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void SetPosition(float x)
        {
            // Seta a posição X da bola, mantendo a posição Y inalterada
            position.X = x;
        }

        public void InvertVelocity()
        {
            velocity.X += velocity.X * 0.3F; // Aumenta a velocidade da bola em 30% a cada colisão para tornar o jogo mais desafiador
            velocity.X *= -1; // Reverte a direção da bola no eixo X
        }
    }
}
