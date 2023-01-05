using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestPathGameOOPLorenSharony
{
    public class Wall : Shapes
    {
        readonly bool isHorizontal;
        readonly bool startPoint;

        public Wall(bool isHorizontal, bool startPoint) 
        {
            this.isHorizontal = isHorizontal;    
            this.startPoint = startPoint;
        }

        public void WallBoundaries(int height, int width)
        {
            if (isHorizontal && startPoint)
            {
                for (int i = 0; i <= width; i++)
                {
                    points.Add((i, 0));
                }
            }
            else if (isHorizontal)
            {
                for (int i = 0; i <= width; i++)
                {
                    points.Add((i, height));
                }
            } else if (!isHorizontal && startPoint)
            {
                for (int i = 0; i <= height -1; i++)
                {
                    points.Add((0, i));
                }
            } else
            {
                for (int i = 0; i <= height-1; i++)
                {
                    points.Add((width, i));
                }
            }

            WallBoundaries();

        }

    }
}
