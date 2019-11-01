using System;
using System.Runtime.InteropServices;
using System.Threading;


namespace Accelerator
{
    public class Program
    {

               

        static void Main(string[] args)
        {
            Timer timer = new Timer(timerCallback, state, 500, 500);
            Start();
        }

        private static void timerCallback(object state)
        {
            if (j_x == stop_position && j_y == stop_position) { rateOfChange = 0; }
            else if (!increaseRateOfChange) { rateOfChange = 0; }
            else if (increaseRateOfChange) { rateOfChange += rateOfChangeStep; };
        }

        public static void Start()
        {
            char input = ' ';
            while (input != 'z')
            {
                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo info = Console.ReadKey();
                    input = info.KeyChar;

                    Console.SetCursorPosition(5, 0);
                    if (input == 'a')
                    {
                        increaseRateOfChange = true;
                        j_x = j_x - 500;
                        Console.WriteLine("left");
                    }
                    else if (input == 'd')
                    {
                        increaseRateOfChange = true;
                        j_x = j_x + 500;
                        Console.WriteLine("right");
                    }
                    else if (input == 'w')
                    {
                        increaseRateOfChange = true;
                        j_y = j_y + 500;
                        Console.WriteLine("up");
                    }
                    else if (input == 'x')
                    {
                        increaseRateOfChange = true;
                        j_y = j_y - 500;
                        Console.WriteLine("down");
                    }


                    if (j_y > max_joystick_position) { j_y = max_joystick_position; }
                    if (j_y < min_joystick_position) { j_y = min_joystick_position; }

                    if (j_x > max_joystick_position) { j_x = max_joystick_position; }
                    if (j_x < min_joystick_position) { j_x = min_joystick_position; }
                }

                string y_dir = "";
                string x_dir = "";

                if (c_y < j_y)
                { y_dir = "asc"; }

                if (c_y > j_y)
                { y_dir = "desc"; }

                if (c_x < j_x)
                { x_dir = "asc"; }

                if (c_x > j_x)
                { x_dir = "desc"; }

                if (y_dir == "desc")
                {
                    c_y = c_y - rateOfChange;
                    if (c_y < j_y) { c_y = j_y; rateOfChange = 0; }
                }
                else if (y_dir == "asc")
                {
                    c_y = c_y + rateOfChange;
                    if (c_y > j_y) { c_y = j_y; rateOfChange = 0; }
                }

                if (x_dir == "desc")
                {
                    c_x = c_x - rateOfChange;
                    if (c_x < j_x) { c_x = j_x; rateOfChange = 0; }
                }
                else if (x_dir == "asc")
                {
                    c_x = c_x + rateOfChange;
                    if (c_x > j_x) { c_x = j_x; rateOfChange = 0; }
                }

                if (c_y == j_y && c_x == j_x)
                { increaseRateOfChange = false; }

                
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("j_x " + j_x.ToString("00000") + ", j_y " + j_y.ToString("00000"));
                Console.WriteLine("c_x " + c_x.ToString("00000") + ", c_y " + c_y.ToString("00000"));
                Console.WriteLine("rateOfChange " + rateOfChange.ToString("0000"));
            }
        }


    }



}
