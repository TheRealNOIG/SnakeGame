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
    enum State { Game, GameOver, Pause}

    public partial class Form1 : Form
    {


        private List<Point> snake = new List<Point>();
        private Brush snakeColor = new SolidBrush(Color.Green);
        private Brush snakeHeadColor = new SolidBrush(Color.DarkRed);
        private Pen snakeOutLine = new Pen(Color.Black, 1);

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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawRectangle(snakeOutLine, new Rectangle(blockSize, blockSize, this.ClientSize.Width - (blockSize * 2), this.ClientSize.Height - (blockSize * 2)));

            for (int i = 0; i < snake.Count; i++) {
                if(i == 0)
                    e.Graphics.FillRectangle(snakeHeadColor, grid[snake[i].X, snake[i].Y]);
                else
                    e.Graphics.FillRectangle(snakeColor, grid[snake[i].X, snake[i].Y]);
                e.Graphics.DrawRectangle(snakeOutLine, grid[snake[i].X, snake[i].Y]);
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
