using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace D3
{
    public partial class Main : Form
    {
        public Camera camera;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            camera.DrawPhoto(e.Graphics);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Map map = new Map("map.txt");
            Point3D pos = new Point3D(-900, 0, 150);
            Vector vector = new Vector(1, 0, 0);
            camera = new Camera(map, pos, vector);
            timerRepaint.Start();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: camera.Forward(10); break;
                case Keys.S: camera.Back(10); break;
                case Keys.Right: camera.RotateRight(0.1); break;
                case Keys.Left: camera.RotateLeft(0.1); break;
                case Keys.Q: camera.Up(10); break;
                case Keys.E: camera.Down(10); break;
            }
           
            if (e.KeyCode == Keys.L)
            {
                camera.map.items[0].state = camera.map.items[0].state ^ 1;
            }
        }

        private void timerRepaint_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
