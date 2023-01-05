using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LongestPathGameOOPLorenSharony
{
    public class Shapes
    {
        protected readonly char filler;
        protected List<(int x, int y)> points = new();

        private ConsoleColor color;
        public bool HasPoint((int x, int y)? point)
        {
            if (point is null) { return false; }
            return points.Contains(point.Value);
        }

        public void Draw()
        {
            foreach (var (x, y) in points)
            {
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = color;
                Console.Write(filler);
            }
        }

        public void WallBoundaries()
        {
            foreach (var (x, y) in points)
            {
                Console.SetCursorPosition(x, y);
            }
        }

        public Shapes(IEnumerable<(int x, int y)>? points = default)
        {
            if (points is not null)
            {
                foreach (var point in points)
                {
                    this.points.Add(point);
                }
            }
        }
       
        public Shapes(char filler, ConsoleColor color, IEnumerable<(int x, int y)>? points = default) : this(points)
        {
            this.filler = filler;
            this.color = color;
        }

        public static Shapes CreateRandom(int boardHeight, int boardWidth)
        {
            Random rnd = new Random();
            List<(int x, int y)> points = new();
            char filler = ' ';
            ConsoleColor color = ConsoleColor.White;

            while (true)
            {
                var top = rnd.Next(2, boardHeight - 2);
                var left = rnd.Next(2, boardWidth - 2);

                switch (rnd.Next(0, 4))
                {
                    case 0:
                        // קו
                        filler = '=';
                        color = ConsoleColor.Yellow;

                        var length = rnd.Next(2, 11);
                        if (length + left > boardWidth) { continue; }

                        for (int i = 0; i < length; i++)
                        {
                            points.Add((i + left, top));
                            
                        }
                        break;

                    case 1:
                        // משולש
                        filler = '#';
                        color = ConsoleColor.Red;

                        var height = rnd.Next(2, 10);
                        if (height + top > boardHeight-3 || height + left > boardWidth-2)
                        {
                            continue;
                        }

                        for (int relativeX = 0; relativeX < height; relativeX++)
                        {
                            for (int relativeY = 0; relativeY <= relativeX; relativeY++)
                            {
                                points.Add((relativeX + left, relativeY + top));
                            }
                        }
                        break;

                    case 2:
                        //מלבן
                        filler = 'ם';
                        color = ConsoleColor.Blue;
                        
                        var lengthR = rnd.Next(2, 11);
                        var heightR = rnd.Next(2, 11);

                        if (heightR + top > boardHeight-3 || lengthR + left > boardWidth)
                        {
                            continue;
                        }

                        for (int relativeX = 0; relativeX < lengthR; relativeX++)
                        {

                            for (int relativeY = 0; relativeY <= heightR; relativeY++)
                            {
                                points.Add((relativeX + left, relativeY + top));
                            }
                        }
                        break;

                    case 3:
                        //מרובע 
                        filler = 'X';
                        color = ConsoleColor.Green;

                        var lengthS = rnd.Next(3, 11);

                        if (lengthS + top > boardHeight-2 || lengthS + left > boardWidth)
                        {
                            continue;
                        }

                        for (int relativeX = 0; relativeX < lengthS; relativeX++)
                        {

                            for (int relativeY = 0; relativeY < lengthS; relativeY++)
                            {
                                points.Add((relativeX + left, relativeY + top ));
                            }
                        }
                        break;
                }
                break;
            }
            return new Shapes(filler, color, points);
        }

        public static bool HasCollision(Shapes shape1, Shapes shape2)
        {
            foreach (var (x1, y1) in shape1.points)
            {
                foreach (var (x2, y2) in shape2.points)
                {
                    if (x1 == x2 && y1 == y2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
