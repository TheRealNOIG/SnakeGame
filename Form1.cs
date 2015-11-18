using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{

    enum Direction { up, down, left, right}
    enum State { game, gameOver}

    public partial class Form1 : Form
    {


        private List<Point> snake = new List<Point>();
        private Point newPoint = new Point();
        private Brush snakeColor = new SolidBrush(Color.Green);
        private Brush snakeHeadColor = new SolidBrush(Color.DarkRed);
        private Pen pen = new Pen(Color.Black, 1);

        private Direction direction;
        private State state;

        private Size gridSize = new Size(40, 30);
        private int blockSize = 25;
        private Rectangle[,] grid;

        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(gridSize.Width * blockSize, gridSize.Height * blockSize);
            MakeGrid();
            MakeSnake();
        }

        private void MakeSnake() {
            snake.Clear();
            int posX = gridSize.Width / 2, posY = gridSize.Height / 2;
            for (int i = 0; i < 4; i++)
                snake.Add(new Point(posX - 1, posY + i));
        }

        private void MakeGrid() {
            int width = this.ClientSize.Width / blockSize;
            int height = this.ClientSize.Height / blockSize;
            grid = new Rectangle[width, height];
            for (int i = 0; i < width; i++) {
                for (int w = 0; w < height; w++) {
                    grid[i, w] = new Rectangle(blockSize * i, blockSize * w, blockSize, blockSize);
                }
            }
        }


        private void MoveSnake() {
            newPoint = snake[0];

            switch (direction) {
                case Direction.up:
                    newPoint.Y--;
                    break;
                case Direction.down:
                    newPoint.Y++;
                    break;
                case Direction.left:
                    newPoint.X--;
                    break;
                case Direction.right:
                    newPoint.X++;
                    break;
            }

            for (int k = snake.Count - 1; k > 0; k--)
            {
                snake[k] = snake[k - 1];
            }
            snake[0] = newPoint;
        }

        private void collisionTest() {
            if (snake[0].X == 0) {
                state = State.gameOver;
            }
            if (snake[0].X == gridSize.Width - 1)
            {
                state = State.gameOver;
            }
            if (snake[0].Y == 0)
            {
                state = State.gameOver;
            }
            if (snake[0].Y == gridSize.Height - 1)
            {
                state = State.gameOver;
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < snake.Count; i++) {
                if(i == 0)
                    e.Graphics.FillRectangle(snakeHeadColor, grid[snake[i].X, snake[i].Y]);
                else
                    e.Graphics.FillRectangle(snakeColor, grid[snake[i].X, snake[i].Y]);
            }

            for (int i = 0; i < gridSize.Width - 2; i++)
            {
                for (int w = 0; w < gridSize.Height - 2; w++)
                {
                    e.Graphics.DrawRectangle(pen, grid[i + 1, w + 1]);
                }
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.W:
                case Keys.Up:
                    if (direction != Direction.down && direction != Direction.up)
                        direction = Direction.up;
                    break;
                case Keys.D:
                case Keys.Right:
                    if (direction != Direction.left && direction != Direction.right)
                        direction = Direction.right;
                    break;
                case Keys.S:
                case Keys.Down:
                    if (direction != Direction.down && direction != Direction.up)
                        direction = Direction.down;
                    break;
                case Keys.A:
                case Keys.Left:
                    if (direction != Direction.left && direction != Direction.right)
                        direction = Direction.left;
                    break;
                case Keys.Space:
                case Keys.Enter:
                    if (state == State.gameOver) {
                        timer1.Start();

                        state = State.game;
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval == 50)
            {
                //One Time Run Code here!

                timer1.Interval = 100;
            }
            else {
                //Update Code Here
                MoveSnake();
                collisionTest();

                if (state == State.gameOver)
                    timer1.Stop();

                this.Refresh();
            }

        }
    }
}
