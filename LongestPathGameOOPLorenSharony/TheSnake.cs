using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPathGameOOPLorenSharony
{
    public class TheSnake : Shapes
    {
        private static Random rnd = new();
        public (int x, int y) Head => points[^1];

        bool firstDot = true;
     
        public TheSnake(char filler, int boardHeight, int boardWidth) : base(filler, ConsoleColor.Black)
        {
            points.Add((
                rnd.Next(1, boardWidth),
                rnd.Next(1, boardHeight)
            ));
        }

        public void DrawHead()
        {
            if (firstDot)
            {
              Console.ForegroundColor = ConsoleColor.White;
            } else
            {
               Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.SetCursorPosition(Head.x, Head.y);
            Console.Write(filler);
            firstDot=false; // בשביל הצבע הכחול בנקודות הבאות
        }

        private (int x, int y) getNewHead(Directions direction)
        {
            (int x, int y) newHead = Head;
            switch (direction)
            {
                case Directions.Left:
                    newHead.x -= 1;
                    break;
                case Directions.Right:
                    newHead.x += 1;
                    break;
                case Directions.Up:
                    newHead.y -= 1;
                    break;
                case Directions.Down:
                    newHead.y += 1;
                    break;
            }
            return newHead;
        }
        
        public (int x, int y) CanMove(Directions direction)
        {
            return getNewHead(direction);
        }

        public void Move(Directions direction)
        {
            var newHead = getNewHead(direction);
            points.Add(newHead);
            DrawHead();
        }
    }
}
