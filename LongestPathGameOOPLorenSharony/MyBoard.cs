using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LongestPathGameOOPLorenSharony
{
    public class MyBoard
    {
        public int Height { get; }
        public int Width { get; }
        public int CurrentLevel { get; private set; } = 0;

        int shapeCount;

        int  countDot; 

        bool hasCollision;

        TheSnake snake;

        List<Shapes> shapes = new();

        public Wall[] Walls { get; }

        public MyBoard(int height, int width)
        {
            Height = height;
            Width = width;

            Walls = new Wall[]
                   {
                //אנכי
                new (false,  true),
                new (false,  false),
                //אופקי
                new (true, true),
                new (true, false),
                   };
        }

        public void DrawGameLine()
        {
            Console.SetCursorPosition(0, 26);
            Console.Write($"Current Level: {CurrentLevel}, Number Of Dots: {countDot}, Number Of Shapes: {shapeCount}");
            Console.CursorVisible = false;
        }

        public void StartGame()
        {
            shapeCount = 0;

            shapeCount = new Random().Next(3, 7);

            startLevel();

            while (shapeCount <= 14)
            {
                Directions direction;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = Directions.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        direction = Directions.Down;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = Directions.Right;
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = Directions.Left;
                        break;
                    default:
                        continue;
                }

                var newHead = snake.CanMove(direction);

                bool newLevel = false;

                foreach (var shape in shapes)
                {
                    if (shape.HasPoint(newHead))
                    {
                        hasCollision = true;
                    }
                }

                if (snake.HasPoint(newHead) || hasCollision == true) // תנאי התנגשות
                {
                    newLevel = true;
                }
                else
                {
                    bool continueOuter = false;
                    foreach (var wall in Walls)
                    {
                        if (wall.HasPoint(newHead))
                        {
                            continueOuter = true;
                            break;
                        }
                    }
                    if (continueOuter)
                    {
                        continue;
                    }
                }

                if (newLevel)
                {
                    shapeCount++;
                    startLevel();
                    continue;
                }

                snake.Move(direction);
                countDot++;
                Console.ForegroundColor = ConsoleColor.White;
                DrawGameLine();
            }
                Console.Clear();
                Console.WriteLine("Game Over!");
                Console.Write($"Level: {CurrentLevel-1}, Total Number Of Dots: {countDot}, Number Of Shapes: {shapeCount-1}");
                Console.CursorVisible = false;
        }

        private void startLevel()
        {
            Console.Clear();
            CurrentLevel++;
            
            foreach (var wall in Walls)
            {
                wall.WallBoundaries(Height-1, Width);
            }

            shapes.Clear();

            for (int i = 0; i < shapeCount; i++)
            {
                Shapes newShape1;

                do
                {
                    hasCollision = false;
                    newShape1 = Shapes.CreateRandom(Height-1, Width);

                    foreach (var shape in shapes)
                    {
                        if (Shapes.HasCollision(shape, newShape1))
                        {
                            hasCollision = true;
                            break;
                        }
                    }
                } while (hasCollision);

                shapes.Add(newShape1);  
            }

            foreach (var shape in shapes)
            {
                shape.Draw();
            }

            do
            {
                hasCollision = false;
                snake = new('*', Height-1, Width);
                foreach (var shape in shapes)
                {
                    if (Shapes.HasCollision(shape, snake))
                    {
                        hasCollision = true;
                        break;
                    }
                }
            } while (hasCollision);
            
            snake.DrawHead();
            DrawGameLine();
        }
    }
}

