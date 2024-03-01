using System;
using System.Drawing;
using System.Windows.Forms;

namespace SolarBar.UI
{
    public class CustomToolTip : ToolTip
    {
        public CustomToolTip()
        {
            OwnerDraw = true;
            Draw += new DrawToolTipEventHandler(CustomDraw);
            Popup += new PopupEventHandler(CustomPopup);
            IsBalloon = true;
            AutoPopDelay = 5000;
            InitialDelay = 1000;
            ReshowDelay = 500;
            ShowAlways = true;
        }

        private void CustomDraw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);

            // Rysowanie tekstu
            using (Font f = new Font("Arial", 9))
            {
                e.Graphics.DrawString(e.ToolTipText, f, Brushes.Black, new PointF(2, 2));
            }
        }

        private void CustomPopup(object sender, PopupEventArgs e)
        {
            using (Font font = new Font("Arial", 9))
            {
                using (Graphics g = Graphics.FromImage(new Bitmap(1, 1)))
                {
                    Control associatedControl = e.AssociatedControl;
                    string tooltipText = GetToolTip(associatedControl);

                    SizeF size = g.MeasureString(tooltipText, font, Int32.MaxValue, StringFormat.GenericTypographic);

                    int width = (int)size.Width+50 + 4;
                    int height = (int)size.Height + 4;

                    e.ToolTipSize = new Size(width, height);
                }
            }
        }
    }
}
