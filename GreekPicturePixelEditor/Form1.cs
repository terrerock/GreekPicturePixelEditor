// The code listed below contains a bare bone example on how to create a pixel image editor
// using C# programming language on MS Visual Studio 2022.

namespace GreekPicturePixelEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int X1 = 0;  // picture box 1 mouse click X position
        private int Y1 = 0;  // picture box 1 mouse click y position
        private bool mousePress = false;  // is left mouse pressed in picture box 2
        private Bitmap statueBitmap;
        private SolidBrush brush;
        private Pen pen;
        private Rectangle rec;
        private Color color;
        private const int pSize = 20;  // pixel size

        // Step 1. Create first picture box 1 at least 512x512 pixel on Form1
        //         Create second picture box 2 at least 640 x 640 pixel grid on Form1
        //         Create a 512x512 pixel bitmap image using MS Paint
        //         Load and display statue bitmap on picture box 1
        private void Form1_Load(object sender, EventArgs e)
        {
            statueBitmap = new Bitmap(@"C:\VC2022\GreekPicturePixelEditor\Pic\Greek512x512.bmp");
            pictureBox1.Image = statueBitmap;
        }

        // Step 2. Save X1,Y1 mouse coordinate when user click on picture box 1
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            X1 = e.X;    // Save X1, Y1 mouse coordinate when user click on picture box 1
            Y1 = e.Y;
            pictureBox2.Refresh();  // force re-draw picture box 2
        }

        // Step 3. Create a 32x32 pixel grid on picture box 2. When user click on 
        // picture box 1, a pixel grid is created based on the X1,
        // Y1 coordinate of picture box 2.  In other words, 32x32 pixel grid bitmap image
        // is cloned from picture box 1 starting at X1,Y1 mouse cliked position (to the
        // X2 = 0, Y2 = 0 position of picture box 2.
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for( int px = 0; px < 32; px++ )
            {
                for( int py = 0; py < 32; py++ )
                {
                    color = ((Bitmap)pictureBox1.Image).GetPixel( X1 + px, Y1 + py );
                    brush = new SolidBrush( color );
                    pen = new Pen( Form1.DefaultBackColor );
                    rec = new Rectangle( px * pSize, py * pSize, pSize, pSize );
                    g.FillRectangle( brush, rec );
                    g.DrawRectangle( pen, rec );
                }
            }
        }

        // Step 4.  Handle mouse events when drawing picture box 2 with a mouse
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            mousePress = false;
        }

        // Step 4.  Handle mouse events when drawing picture box 2 with a mouse
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if( e.Button == MouseButtons.Left )
            {
                mousePress = true;
                int X2 = e.X / 20;  // Normalized the mouse position X and Y because 
                int Y2 = e.Y / 20;  // each square in grid is 20 x 20 pixels. This is the opposite of
                                    // px * pSize, py * pSize in pictureBox2_Paint method
                ((Bitmap)pictureBox1.Image).SetPixel( X1 + X2, Y1 + Y2, System.Drawing.Color.Red );
                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }

        }

        // Step 4.  Handle mouse events when drawing picture box 2 with a mouse
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if( mousePress )
            {
                int X2 = e.X / 20;  // Normalized the mouse position X and Y because 
                int Y2 = e.Y / 20;  // each square in grid is 20 x 20 pixels. This is the opposite of
                                    // px * pSize, py * pSize in pictureBox2_Paint method
                ((Bitmap)pictureBox1.Image).SetPixel( X1 + X2, Y1 + Y2, System.Drawing.Color.Red );
                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }
        }

        // Step 5. Deallocate memory to the system
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            statueBitmap.Dispose();
            brush.Dispose();
            pen.Dispose() ;
        }
    }
}