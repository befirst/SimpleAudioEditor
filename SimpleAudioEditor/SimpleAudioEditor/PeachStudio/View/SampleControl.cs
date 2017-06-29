﻿using System;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;
using TagLib;
using TagLib.Mpeg;

namespace SimpleAudioEditor.PeachStudio
{
    public partial class SampleControl : UserControl
    {
        const int SCROLL_STEP = 20;
        const int VALUES_START_AT_ONE = 1;

        Sample sample;
        SamplePlayer samplePlayer;
        int indent = 10;//Отступ от края
        Point startPos, endPos;

        Point markerPoint;
        bool markerMoving;
        int mousePositionX;

        int startedPictureBoxWidth;
        int visiblePictureBoxWidth;
        int oldScrollValue;
        int newScrollValue;

        public Sample Sample
        {
            //    set{sample = value;}
            get { return sample; }
        }

        public SampleControl(Sample _sample, Control _parent, Point _location, int _height = -1)
        {
            InitializeComponent();

            this.Parent = _parent;
            if (_height == -1) {
                this.Size = new Size(this.Parent.Width - indent, MinimumSize.Height);
            } else {
                this.Size = new Size(this.Parent.Width - indent, _height);
            }

            this.Location = _location;
            this.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            sample = _sample;
            markerPoint = new Point(startPos.X, startPos.Y - object_radius * 2);
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseDown += pictureBox_MouseDown;
            samplePlayer = new SamplePlayer(sample.SoundPath);
            samplePlayer.GetTimer.Tick += samplePlayerTimer_Tick;
            markerMoving = false;
            UpdatePointPos();
            UpdateMaskedTimeValue();

            label1.ForeColor = Color.OrangeRed;
            //label1.BackColor = Color.DarkGray;
            label1.Text = "sdfsdfsfsfds";
            AudioFile fileWithTags = new AudioFile(_sample.SoundPath, ReadStyle.Average);
            if (fileWithTags.Tag.Performers != null && fileWithTags.Tag.Title != null)
            {
                label1.Text = fileWithTags.Tag.Performers[0] + " - " + fileWithTags.Tag.Title;
            }
            else
            {
                string[] s = _sample.SoundPath.Split('\\');
                label1.Text = s[s.Length - 1];
            }
        }

        public void UpdateMaskedTimeValue()
        {
            maskedTextBoxCurrentTime.Text = samplePlayer.CurrentTime.ToString(@"hh\:mm\:ss\.FF");
            maskedTextBoxSplitEndTime.Text = sample.SplitEndTime.ToString(@"hh\:mm\:ss\.FF");
            maskedTextBoxSplitStartTime.Text = sample.SplitStartTime.ToString(@"hh\:mm\:ss\.FF");
            maskedTextBoxResultTime.Text = (sample.SplitEndTime - sample.SplitStartTime).ToString(@"hh\:mm\:ss\.FF");
        }

        public SamplePlayer GetSamplePlayer
        {
            get { return samplePlayer; }
        }        

        // «Размер» объекта для мыши над целями.
        private const int object_radius = 6;
        // Мы над объектом, если квадрат расстояния между мышью и объектом меньше этого.       
        private const int over_dist_squared = object_radius * object_radius;

        public int OffsetX;

        private int PlayerLineWidth
        {
            get { return pictureBox.Width - indent * 2; }
        }
        public Point MarkerPoint
        {
            set { markerPoint = value; }
            get { return markerPoint; }
        }

        public int Indent
        {
            set { indent = value; }
        }

        public int SetIndent
        {
            set { indent = value; }
            get { return indent; }
        }
        public SampleControl()
        {
            InitializeComponent();
            markerPoint = new Point(startPos.X, startPos.Y - object_radius * 2);
            markerMoving = false;
            UpdatePointPos();
        }

        private void UpdatePointPos()
        {
            startPos = new Point(indent, pictureBox.Height / 2);
            endPos = new Point(pictureBox.Width - indent, pictureBox.Height / 2);
            if (samplePlayer != null)
                markerPoint = new Point(indent + Mathf.TimeToPos(Mathf.Clamp(samplePlayer.CurrentTime, sample.SplitStartTime, sample.SplitEndTime), samplePlayer.TotalTime, PlayerLineWidth), startPos.Y - object_radius * 2);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {

            if (samplePlayer.PlayerState == PlaybackState.Paused || samplePlayer.PlayerState == PlaybackState.Stopped)
            {
                samplePlayer.Play();
                buttonPlay.BackgroundImage = Properties.Resources.pause_icon;
            }
            else
            {
                samplePlayer.Pause();
                buttonPlay.BackgroundImage = Properties.Resources.play_icon;
            }
            UpdateMaskedTimeValue();
            UpdatePointPos();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            samplePlayer.Stop();
            samplePlayer.CurrentTime = sample.SplitStartTime;
            buttonPlay.AccessibleName = "started";
            UpdatePointPos();
        //    markerPoint = new Point(indent + Mathf.TimeToPos(samplePlayer.CurrentTime, samplePlayer.TotalTime, PlayerLineWidth), startPos.Y - object_radius * 2);
            pictureBox.Invalidate();
        }

        private void samplePlayerTimer_Tick(object sender, EventArgs e)
        {
            if (!markerMoving)
                markerPoint = new Point(Mathf.Clamp(indent + Mathf.TimeToPos(samplePlayer.CurrentTime, samplePlayer.TotalTime, PlayerLineWidth), startPos.X, endPos.X), startPos.Y - object_radius * 2);
            maskedTextBoxCurrentTime.Text = samplePlayer.CurrentTime.ToString(@"hh\:mm\:ss\.FF");
            if (samplePlayer.CurrentTime >= sample.SplitEndTime)
            {
                samplePlayer.Stop();
                samplePlayer.CurrentTime = sample.SplitStartTime;
                buttonPlay.AccessibleName = "started";
            }
            if(!markerMoving)
            UpdatePointPos();
            pictureBox.Invalidate();
        }

        #region "Moving Marker"


        // Мы двигаем маркер точку.
        private void pictureBox_MouseMove_MovingMarker(object sender, MouseEventArgs e)
        {
            markerPoint = new Point(Mathf.Clamp(e.X + OffsetX, Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent,
            Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth)+indent),markerPoint.Y);
                UpdateMaskedTimeValue();
            // Перерисовать.
            pictureBox.Invalidate();
        }

        // Остановить перемещение конечной точки.
        protected void pictureBox_MouseUp_MovingMarker(object sender, MouseEventArgs e)
        {
            // Сброс обработчиков событий.
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseMove -= pictureBox_MouseMove_MovingMarker;
            pictureBox.MouseUp -= pictureBox_MouseUp_MovingMarker;
            samplePlayer.CurrentTime = Mathf.PosToTime(markerPoint.X - indent, PlayerLineWidth, samplePlayer.TotalTime);
            maskedTextBoxCurrentTime.Text = samplePlayer.CurrentTime.ToString(@"hh\:mm\:ss\.FF");


            UpdateMaskedTimeValue();
            markerMoving = false;
            // Перерисовать.
            pictureBox.Invalidate();
        }

        private void pictureBox_MouseMove_NotDown(object sender, MouseEventArgs e)
        {
            Cursor new_cursor = Cursors.Arrow;

            Point hit_point;
            if (MouseIsOverMarker(e.Location, out hit_point))
                new_cursor = Cursors.VSplit;
            else
            if (MouseIsOverSplitPoint(e.Location, out hit_point))
                new_cursor = Cursors.SizeWE;
            else
            if (MouseIsOverSegment(e.Location))
                new_cursor = Cursors.Hand;

            // Установим новый курсор.
            if (pictureBox.Cursor != new_cursor)
                pictureBox.Cursor = new_cursor;
        }

        


        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Point hit_point;
           
            if (MouseIsOverMarker(e.Location, out hit_point))
            {
                // Начните перемещать эту конечную точку.
                pictureBox.MouseMove -= pictureBox_MouseMove_NotDown;
                pictureBox.MouseMove += pictureBox_MouseMove_MovingMarker;
                pictureBox.MouseUp += pictureBox_MouseUp_MovingMarker;
                markerMoving = true;

                // Запомните смещение от мыши до точки.
                OffsetX = hit_point.X - e.X;
            }else
            if (e.Button == MouseButtons.Right)
            {
                mousePositionX = e.X - indent;
                contextMenuStrip.Show(MousePosition);
            }
            else
            if (MouseIsOverSplitPoint(e.Location, out hit_point))
            {
                // Начните перемещать эту конечную точку.
                pictureBox.MouseMove -= pictureBox_MouseMove_NotDown;
                pictureBox.MouseMove += pictureBox_MouseMove_MovingSplitPoint;
                pictureBox.MouseUp += pictureBox_MouseUp_MovingSplitPoint;


                // Посмотрим, будем ли мы перемещать начальную конечную точку.
                MovingStartSplitPoint = (new Point(Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent, startPos.Y).Equals(hit_point));

                // Запомните смещение от мыши до точки.
                OffsetX = hit_point.X - e.X;
            }else
            if (MouseIsOverSegment(e.Location))
            {
                //   MessageBox.Show("DpDragDrop");
                pictureBox.DoDragDrop(sample, DragDropEffects.Copy);
            }

            

        }

        private bool MouseIsOverSegment(Point mouse_pt)
        {
            // Посмотрим, перешли ли мы над сегментом.
            Point splitP1 = new Point(Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent, startPos.Y);
            Point splitP2 = new Point(Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth) + indent, startPos.Y);

            PointF closest;
            if (FindDistanceToSegmentSquared(
                mouse_pt, splitP1, splitP2, out closest)
                    < over_dist_squared)
            {
                return true;
            }
            return false;
        }

        private double FindDistanceToSegmentSquared(Point pt, Point p1, Point p2, out PointF closest)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // Это точка, а не сегмент линии.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return dx * dx + dy * dy;
            }

            // Вычислим t, который минимизирует расстояние.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // Посмотрим, представляет ли это один из сегментов
            // конечные точки или точка в середине.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return dx * dx;
        }

        protected bool MouseIsOverMarker(Point mouse_pt, out Point hit_pt)
        {

            if (FindDistanceToPointSquared(mouse_pt, markerPoint) < over_dist_squared * 2)
            {
                hit_pt = markerPoint;
                return true;
            }
            else
            {
                hit_pt = new Point(-1, -1);
                return false;
            }

        }

        // Посмотрим, находится ли мышь над конечной точкой.
        private bool MouseIsOverSplitPoint(Point mouse_pt, out Point hit_pt)
        {
            Point splitP1 = new Point(Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent, startPos.Y);
            Point splitP2 = new Point(Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth) + indent, startPos.Y);
            // Проверьте начальную точку.
            if (FindDistanceToSplitPointSquared(mouse_pt, splitP1) < Math.Sqrt(over_dist_squared))
            {
                hit_pt = splitP1;
                return true;
            }

            // Проверьте конечную точку.
            if (FindDistanceToSplitPointSquared(mouse_pt, splitP2) < Math.Sqrt(over_dist_squared))

            {

                hit_pt = splitP2;
                return true;
            }

            hit_pt = new Point(-1, -1);
            return false;
        }


        protected int FindDistanceToPointSquared(Point pt1, Point pt2)
        {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        protected int FindDistanceToSplitPointSquared(Point pt1, Point pt2)
        {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt1.Y;
            return dx * dx + dy * dy;
        }
        #endregion // Перемещение конечной точки

        #region "Moving End Point"

        // Конечная точка, с которой мы движемся.
        private bool MovingStartSplitPoint = false;

        // Смещение от мыши до перемещаемого объекта.
        private int OffsetP1X, OffsetP2X;

        // Мы двигаем конечную точку.
        private void pictureBox_MouseMove_MovingSplitPoint(object sender, MouseEventArgs e)
        {
            // Переместите точку в новое место
            if (MovingStartSplitPoint)
            {

                sample.SplitStartTime = Mathf.PosToTime(Mathf.Clamp(e.X + OffsetP1X, startPos.X, endPos.X) - indent, PlayerLineWidth, sample.TotalTime);
                if (sample.SplitEndTime <= sample.SplitStartTime)
                    MovingStartSplitPoint = false;
            }
            else
            {

                sample.SplitEndTime = Mathf.PosToTime(Mathf.Clamp(e.X + OffsetP1X, startPos.X, endPos.X) - indent, PlayerLineWidth, sample.TotalTime);
                if (sample.SplitEndTime <= sample.SplitStartTime)
                    MovingStartSplitPoint = true;

            }
            //samplePlayer.CurrentTime = Mathf.Clamp(samplePlayer.CurrentTime, sample.SplitStartTime, sample.SplitEndTime);

            UpdatePointPos();
            UpdateMaskedTimeValue();
            // Перерисовать.
            pictureBox.Invalidate();
        }

        // Остановить перемещение конечной точки.
        private void pictureBox_MouseUp_MovingSplitPoint(object sender, MouseEventArgs e)
        {
            // Сброс обработчиков событий.
            pictureBox.MouseMove += pictureBox_MouseMove_NotDown;
            pictureBox.MouseMove -= pictureBox_MouseMove_MovingSplitPoint;
            pictureBox.MouseUp -= pictureBox_MouseUp_MovingSplitPoint;

            samplePlayer.CurrentTime = Mathf.Clamp(samplePlayer.CurrentTime, sample.SplitStartTime, sample.SplitEndTime);
            UpdateMaskedTimeValue();
            // Перерисовать.
            pictureBox.Invalidate();
        }

        #endregion // Перемещение конечной точки

        private void SampleControl_Load(object sender, EventArgs e) {           
            startedPictureBoxWidth =  pictureBox.Width;
            visiblePictureBoxWidth = this.Width - buttonPlay.Width;
            hScrollBar.Maximum = startedPictureBoxWidth;
            hScrollBar.SmallChange = startedPictureBoxWidth;
            hScrollBar.LargeChange = startedPictureBoxWidth;
        }

        private void FromBeginingToPointToolStripMenuItem_Click(object sender, EventArgs e) {
            sample.SplitStartTime = new TimeSpan();
            sample.SplitEndTime = Mathf.PosToTime(mousePositionX, PlayerLineWidth, samplePlayer.TotalTime);
            UpdatePointPos();
            UpdateMaskedTimeValue();
            pictureBox.Invalidate();
        }

        private void FromPointToEndingToolStripMenuItem_Click(object sender, EventArgs e) {
            sample.SplitStartTime = Mathf.PosToTime(mousePositionX, PlayerLineWidth, samplePlayer.TotalTime);
            sample.SplitEndTime = samplePlayer.TotalTime;
            UpdatePointPos();
            UpdateMaskedTimeValue();
            pictureBox.Invalidate();
        }
        
        private void pictureBox_MouseClick(object sender, MouseEventArgs e) {
            
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e) {
            if (!pictureBox.Focused) {
                pictureBox.Focus();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            if (pictureBox.Focused) {
                Console.WriteLine("initially: max " + (hScrollBar.Maximum) + " currentValue " + hScrollBar.Value + " " + oldScrollValue + " " + newScrollValue);
                Console.WriteLine("Location: " + pictureBox.Location.X);
                if (e.Delta > 0)
                {
                    pictureBox.Width = (int)(pictureBox.Width) + SCROLL_STEP;
                    hScrollBar.Maximum = hScrollBar.Maximum + SCROLL_STEP;
                } else
                {
                    hScrollBar.Maximum = ((hScrollBar.Maximum - SCROLL_STEP) < startedPictureBoxWidth) ? startedPictureBoxWidth : (hScrollBar.Maximum - SCROLL_STEP);
                    pictureBox.Width = (pictureBox.Width - SCROLL_STEP) < visiblePictureBoxWidth ? visiblePictureBoxWidth : (pictureBox.Width - SCROLL_STEP);
                    if (hScrollBar.Maximum == startedPictureBoxWidth) {
                        callBackScrollEventWithFirstPosition();
                    } else {
                        if (hScrollBar.Value == hScrollBar.Maximum - hScrollBar.LargeChange + SCROLL_STEP + VALUES_START_AT_ONE) {
                            callBackScrollEventWithScrollStep(2);
                        }
                    }
                }
                Console.WriteLine("after: max " + (hScrollBar.Maximum) + " currentValue " + hScrollBar.Value + " " + oldScrollValue + " " + newScrollValue);
                Console.WriteLine("Location: " + pictureBox.Location.X);                
            }
            UpdatePointPos();
            pictureBox.Invalidate();
        }

        private void callBackScrollEventWithFirstPosition() {
            newScrollValue = VALUES_START_AT_ONE;
            hScrollBar_Scroll(new object(), new ScrollEventArgs(ScrollEventType.LargeDecrement, oldScrollValue, newScrollValue));
            oldScrollValue = newScrollValue;
            hScrollBar_Scroll(new object(), new ScrollEventArgs(ScrollEventType.EndScroll, oldScrollValue, newScrollValue));
            hScrollBar.Value = newScrollValue;
        }

        private void callBackScrollEventWithScrollStep(int i) {
            Console.WriteLine(i + ".1: max " + (hScrollBar.Maximum) + " currentValue " + hScrollBar.Value + " " + oldScrollValue + " " + newScrollValue);
            newScrollValue -= SCROLL_STEP;
            hScrollBar_Scroll(new object(), new ScrollEventArgs(ScrollEventType.SmallDecrement, oldScrollValue, newScrollValue));
            Console.WriteLine(i + ".2: max " + (hScrollBar.Maximum) + " currentValue " + hScrollBar.Value + " " + oldScrollValue + " " + newScrollValue);
            oldScrollValue = newScrollValue;
            hScrollBar_Scroll(new object(), new ScrollEventArgs(ScrollEventType.EndScroll, oldScrollValue, newScrollValue));
            Console.WriteLine(i + ".3: max " + (hScrollBar.Maximum) + " currentValue " + hScrollBar.Value + " " + oldScrollValue + " " + newScrollValue);
            hScrollBar.Value = newScrollValue;
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e) {
            oldScrollValue = e.OldValue;
            newScrollValue = e.NewValue;
            int diference =  e.NewValue - e.OldValue;
            pictureBox.Location = new Point((pictureBox.Location.X - (int)(diference)), pictureBox.Location.Y);
            Location = new Point(0, Location.Y);
            Console.WriteLine("Location: " + pictureBox.Location.X + " currentValue " + hScrollBar.Value + " old " + oldScrollValue + " new " + newScrollValue);
            UpdatePointPos();
            pictureBox.Invalidate();
        }

        private void SampleControl_Resize(object sender, EventArgs e) {
            visiblePictureBoxWidth = this.Width - buttonPlay.Width;
            UpdatePointPos();
            pictureBox.Invalidate();
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            
            int penSize = 3;
            Pen grayPen = new Pen(Color.YellowGreen, penSize);

            
            Pen drawWaveSplitPen = new Pen(Color.DarkOrange,0.5f);
            Pen drawWaveBackPen = new Pen(Color.Firebrick, 0.5f);

            Graphics canvas = e.Graphics;

            canvas.DrawImage(
                Mathf.DrawWave(sample.OptimizedArray,
                sample.DrawSource,
                drawWaveSplitPen,
                pictureBox.Width-indent*2,
                pictureBox.Height, 
                Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth),
                Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth), drawWaveBackPen), 
                new PointF(startPos.X,0));

            canvas.DrawLine(grayPen, new Point(
                Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, endPos.Y),
                new Point( Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, endPos.Y));

            Pen segmentPen = new Pen(Color.DarkGreen, penSize);

            //Рисуем маркер
            canvas.DrawLine(segmentPen,
                new Point(Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, startPos.Y), new Point(markerPoint.X, startPos.Y));


            canvas.DrawLine(segmentPen,
                new Point(Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, 0),
                new Point(Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, pictureBox.Height));

            canvas.DrawLine(segmentPen,
                new Point(Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, 0),
                new Point(Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, pictureBox.Height));

            canvas.DrawLine(segmentPen,
                new Point(Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, 0 + penSize / 2),
                new Point(Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, 0 + penSize / 2));

            canvas.DrawLine(segmentPen,
                new Point(Mathf.TimeToPos(sample.SplitStartTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, pictureBox.Height - penSize / 2),
                new Point(Mathf.TimeToPos(sample.SplitEndTime, sample.TotalTime, PlayerLineWidth) + indent + penSize / 2, pictureBox.Height - penSize / 2));


            Pen cursorPen = new Pen(Color.ForestGreen, 1);

            canvas.DrawPolygon(new Pen(Color.ForestGreen,3), new Point[] {
                new Point( markerPoint.X - object_radius, markerPoint.Y - object_radius),
                new Point( markerPoint.X + object_radius, markerPoint.Y - object_radius),
                new Point(markerPoint.X,startPos.Y)});
            
            canvas.DrawPolygon(new Pen(Color.OrangeRed,0.5f), new Point[] {
                new Point( markerPoint.X - object_radius, markerPoint.Y - object_radius),
                new Point( markerPoint.X + object_radius, markerPoint.Y - object_radius),
                new Point(markerPoint.X,startPos.Y)});
                
        }

        private void pictureBox_Layout(object sender, LayoutEventArgs e)
        {
            UpdatePointPos();
            pictureBox.Invalidate();
        }
    }
}
