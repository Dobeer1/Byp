using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace never.Class
{
    public class buttonanimated : Button
    {
        Stopwatch sw = new Stopwatch();
        Timer Animation { get; set; } = new Timer();
        Timer AnimationBack { get; set; } = new Timer();
        public int AnimationInterval { get; set; } = 3;
        public string CustomButtonText { get; set; } = "Authorization";
        public Color BackHoverColor { get; set; } = Color.FromArgb(93, 147, 255);
        public int BackgroundSpeed { get; set; } = 25;
        public Color TextHoverColor { get; set; } = Color.Black;
        public double SmoothCorrectionFactor { get; set; } = 3F;
        private int borderSize { get; set; } = 1;
        private int borderRadius { get; set; } = 5;
        private Color borderColor { get; set; } = Color.FromArgb(22, 22, 22);
        public bool UseSmoothSpeedIncrement { get; set; } = true;

        private int incremental_x = 1;

        private bool DrawString = false;

        [Category("RJ Code Advance")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        public buttonanimated()
        {
            //double buffer -> less flickering
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            //Events Section
            Animation.Interval = AnimationInterval;
            AnimationBack.Interval = AnimationInterval;
            Animation.Tick += ButtonAnimation;
            AnimationBack.Tick += ButtonAnimationBack;
            //Standard values
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.FromArgb(18, 18, 18);//ligth grey
            this.ForeColor = Color.FromArgb(175, 174, 175);//dark grey
            this.Text = ""; //we want to override the standard text 
            this.Resize += new EventHandler(Button_Resize);
        }
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }
        private void ButtonAnimationBack(object sender, EventArgs e)
        {
            if (UseSmoothSpeedIncrement)
            {
                incremental_x -= Convert.ToInt32(BackgroundSpeed * sw.Elapsed.TotalSeconds * SmoothCorrectionFactor);
            }
            else
            {
                incremental_x -= BackgroundSpeed;
            }
            if (incremental_x <= 0)
            {
                AnimationBack.Stop();
                incremental_x = 1;
            }
            this.Invalidate();
        }

        private void ButtonAnimation(object sender, EventArgs e)
        {
            if (UseSmoothSpeedIncrement)
            {
                incremental_x += Convert.ToInt32(BackgroundSpeed * sw.Elapsed.TotalSeconds * SmoothCorrectionFactor);
            }
            else
            {
                incremental_x += BackgroundSpeed;
            }
            if (incremental_x > this.Width * 3)
            {
                Animation.Stop();
            }
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Text = "";
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (incremental_x != 1) // 1 is the min value 
            {
                Rectangle r = new Rectangle(new Point(0 - incremental_x / 2, 0 - incremental_x / 2), new Size(incremental_x, this.Height + incremental_x));
                g.FillPie(new SolidBrush(BackHoverColor), r, 0, 360);
            }
            //redraw string
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            SolidBrush TextColor;
            if (DrawString)
            {
                TextColor = new SolidBrush(TextHoverColor);

            }
            else
            {
                TextColor = new SolidBrush(this.ForeColor);
            }
            g.DrawString(CustomButtonText, this.Font, TextColor, new Rectangle(new Point(0, 0), new Size(this.Width, this.Height)), sf);
            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;
            if (borderRadius > 2) //Rounded button
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //Button surface
                    this.Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    e.Graphics.DrawPath(penSurface, pathSurface);
                    //Button border                    
                    if (borderSize >= 1)
                        //Draw control border
                        e.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else //Normal button
            {
                e.Graphics.SmoothingMode = SmoothingMode.None;
                //Button surface
                this.Region = new Region(rectSurface);
                //Button border
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
            //done here!
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            DrawString = true;
            AnimationBack.Stop();
            Animation.Start();
            //Reset stopwatch
            sw.Reset();
            sw.Stop();
            sw.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            DrawString = false;
            Animation.Stop();
            AnimationBack.Start();
            //Reset stopwatch
            sw.Reset();
            sw.Stop();
            sw.Start();
        }
    }
}
