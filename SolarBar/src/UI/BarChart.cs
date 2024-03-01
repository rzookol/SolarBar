using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SolarBar.UI
{
    public class BarChart : PictureBox
    {
        public List<float> Data { get; set; }
        public Color BarColor { get; set; } = Color.White;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Data == null || Data.Count == 0)
                return;

            float maxValue = Data.Max();
            if (maxValue == 0) // Unikamy dzielenia przez zero
                return;

            this.BackColor = Color.Transparent;             // Ustaw przezroczyste tło

            int numberOfBars = Data.Count;
            float barWidth = (float)Width / numberOfBars;

            // Jeśli jest więcej wartości niż pikseli szerokości, trzeba usrednić dane
            if (barWidth < 1)
            {
                int barsPerPixel = (int)Math.Ceiling((double)numberOfBars / Width);
                List<float> averagedData = new List<float>();
                for (int i = 0; i < numberOfBars; i += barsPerPixel)
                {
                    averagedData.Add(Data.Skip(i).Take(barsPerPixel).Average());
                }
                Data = averagedData;
                barWidth = (float)Width / Data.Count;
            }

            Graphics g = e.Graphics;
            for (int i = 0; i < Data.Count; i++)
            {
                float barHeight = (Data[i] / maxValue) * Height;
                using (var brush = new SolidBrush(BarColor))
                {
                    g.FillRectangle(brush, i * barWidth, Height - barHeight, (numberOfBars>12)? barWidth:(barWidth - 1), barHeight);
                }
            }
        }

        // Metoda do odświeżania danych i rysowania wykresu
        public void RefreshData(List<float> newData)
        {
            Data = newData;
            Invalidate(); // Powoduje przerysowanie kontrolki
        }
    }
}