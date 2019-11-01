using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Accelerator
{
    public class JoyStick
    {
        private int j_x = 16383;
        private int j_y = 16383;
        private int c_x = 16383;
        private int c_y = 16383;
        private int rateOfChange = 0;
        private bool increaseRateOfChange;
                
        private static object state;

        public int Stop_position { get; set; } = 16383;
        public int Max_joystick_position { get; set; } = 32766;
        public int Min_joystick_position { get; set; } = 0;
        public Point JoystickLocation { get => new Point(j_x, j_y); }
        public Point CurrentLocation { get => new Point(c_x, c_y); }
        public int RateOfChangeStep { get; set; } = 25;

        public delegate void PositionChanged(Point point);

        public event PositionChanged positionChanged;

        //class constructor will use defaults
        //for the joystick starting position
        //as given in the specs
        public JoyStick() {
            Timer timer = new Timer(timerCallback, state, 500, 500);
        }

        //class constructor that allow you
        //to start create the class with a 
        //starting positon
        public JoyStick(Point joystickLocation)
        {
            j_x = joystickLocation.X;
            j_y = joystickLocation.Y;
            c_x = joystickLocation.X;
            c_y = joystickLocation.Y;

            Timer timer = new Timer(timerCallback, state, 500, 500);
        }

        private void timerCallback(object state)
        {
            if (j_x == Stop_position && j_y == Stop_position) { rateOfChange = 0; }
            else if (!increaseRateOfChange) { rateOfChange = 0; }
            else if (increaseRateOfChange) { rateOfChange += RateOfChangeStep; };
        }

        public void UpdateJoyStickLocation(Point point) {

            if (point.X != j_x || point.Y != j_y)
            {
                j_x = point.X;
                j_y = point.Y;

                increaseRateOfChange = true;
            }

            if (j_y > Max_joystick_position) { j_y = Max_joystick_position; }
            if (j_y < Min_joystick_position) { j_y = Min_joystick_position; }

            if (j_x > Max_joystick_position) { j_x = Max_joystick_position; }
            if (j_x < Min_joystick_position) { j_x = Min_joystick_position; }

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
            else
            {
                //raise the event
                positionChanged?.Invoke(new Point(c_x, c_y));
            }
        }

    }
}
