using System;


namespace LongestPathGameOOPLorenSharony
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.SetWindowSize(81, 27);
            Console.SetBufferSize(81, 27);

            MyBoard board = new MyBoard(26,80);
            board.StartGame();

            Console.ReadLine();
        }
    }
}