﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAudioEditor.PeachStudio {
    public partial class ProjectControl : UserControl {
        // «Размер» объекта для мыши над целями.
        private const int object_radius = 6;
        // Мы над объектом, если квадрат расстояния
        // между мышью и объектом меньше этого.
        private const int over_dist_squared = object_radius * object_radius;    

        Project project;
        ProjectPlayer projectPlayer;

        Point startPos, endPos;
        Point markerPoint;
        bool markerMoving;
        int OffsetX;
        TimeSpan currentTime;

        TimeSpan outputFileTime = TimeSpan.FromMinutes(5);

        public Project CurrentProject
        {
            set { project = value; }
            get { return project; }
        }

        private int TimePerPoint(TimeSpan time)
        {
            return 0;
        }

        private int PlayerLineWidth {
            get { return pbWaveViewer.Width; }
        }

        public ProjectControl() {
            InitializeComponent();
            //project = new Project();
            //markerPoint = new Point(startPos.X, startPos.Y - object_radius * 2);
            UpdatePointPos();
            markerPoint = new Point(Mathf.TimeToPos(
                Mathf.Clamp(Mathf.PosToTime(startPos.X, PlayerLineWidth, outputFileTime), new TimeSpan(), outputFileTime), currentTime, PlayerLineWidth), startPos.Y - object_radius * 2);

            pbWaveViewer.Invalidate();
        }

        public ProjectControl(Project project) {
            InitializeComponent();
            this.project = project;
            UpdatePointPos();
            markerPoint = new Point(Mathf.TimeToPos(
                Mathf.Clamp(Mathf.PosToTime(startPos.X, PlayerLineWidth, outputFileTime), new TimeSpan(), outputFileTime), currentTime, PlayerLineWidth), startPos.Y - object_radius * 2);

            pbWaveViewer.Invalidate();
        }

        private void UpdatePointPos() {
            startPos = new Point(0, pbWaveViewer.Height / 2);
            endPos = new Point(pbWaveViewer.Width, pbWaveViewer.Height / 2);
            
            if (projectPlayer != null)
                markerPoint = new Point(Mathf.TimeToPos(currentTime, outputFileTime, PlayerLineWidth), markerPoint.Y);

        }

        public void SplitAll() { }

        private void pbWaveViewer_Paint(object sender, PaintEventArgs e) {            
            int penSize = 3;
            Pen grayPen = new Pen(Color.Gray, penSize);

            

            Graphics canvas = e.Graphics;

            TimeSpan timea = new TimeSpan(0, 0, 0);

            if (project.GetSampleList().Count == 0) return;
            for (int i = 0; i < project.GetSampleList().Count; i++)
            {

                var s = project.GetSampleList()[i];


                TimeSpan splitTime = s.SplitEndTime - s.SplitStartTime;
                int timePerPixel = (int)((s.TotalTime.TotalSeconds / outputFileTime.TotalSeconds) * (double)PlayerLineWidth);

                if (i != MovingSegment)
                {
                    Bitmap bmp = Mathf.DrawWave(s.OptimizedArray, s.DrawSource, new Pen(Color.Gray, 1), timePerPixel
                   , pbWaveViewer.Height, Mathf.TimeToPos(s.SplitStartTime, s.TotalTime, timePerPixel), Mathf.TimeToPos(s.SplitEndTime, s.TotalTime, timePerPixel));
                    bmp = Mathf.CropImage(bmp, new Rectangle(new Point(Mathf.TimeToPos(s.SplitStartTime, s.TotalTime, timePerPixel) + 1, 0), new Size(Mathf.TimeToPos(s.SplitEndTime, s.TotalTime, timePerPixel) - Mathf.TimeToPos(s.SplitStartTime, s.TotalTime, timePerPixel), pbWaveViewer.Height)));
                    canvas.DrawImage(bmp, new Point(Mathf.TimeToPos(timea, outputFileTime, PlayerLineWidth), 0));

                }
                else
                {
                    Bitmap bmp = Mathf.DrawWave(s.OptimizedArray, s.DrawSource, new Pen(Color.DarkOrange, 1), timePerPixel
                   , pbWaveViewer.Height, Mathf.TimeToPos(s.SplitStartTime, s.TotalTime, timePerPixel), Mathf.TimeToPos(s.SplitEndTime, s.TotalTime, timePerPixel));
                    bmp = Mathf.CropImage(bmp, new Rectangle(new Point(Mathf.TimeToPos(s.SplitStartTime, s.TotalTime, timePerPixel) + 1, 0), new Size(Mathf.TimeToPos(s.SplitEndTime, s.TotalTime, timePerPixel) - Mathf.TimeToPos(s.SplitStartTime, s.TotalTime, timePerPixel), pbWaveViewer.Height)));
                    canvas.DrawImage(bmp, new Point(Mathf.TimeToPos(timea, outputFileTime, PlayerLineWidth), 0));
                }


                // canvas.DrawLine(new Pen(Color.Magenta, 3),
                //    new Point(Mathf.TimeToPos(timea, outputFileTime, PlayerLineWidth), startPos.Y),
                //    new Point(Mathf.TimeToPos(timea + splitTime, outputFileTime, PlayerLineWidth), startPos.Y));

                canvas.DrawLine(new Pen(Color.Magenta, 1),
               new Point(Mathf.TimeToPos(timea + splitTime, outputFileTime, PlayerLineWidth), startPos.Y + 10),
               new Point(Mathf.TimeToPos(timea + splitTime, outputFileTime, PlayerLineWidth), startPos.Y - 10));
                timea += splitTime;
            }
            canvas.DrawLine(grayPen, startPos, endPos);

            Pen cursorPen = new Pen(Color.Black, penSize);
            //Рисуем маркер
            cursorPen.Color = Color.Black;
            canvas.DrawLine(cursorPen, startPos, new Point(markerPoint.X, startPos.Y));

            canvas.DrawPolygon(cursorPen, new Point[] {
                new Point( markerPoint.X - object_radius, markerPoint.Y - object_radius),
                new Point( markerPoint.X + object_radius, markerPoint.Y - object_radius),
                new Point(markerPoint.X,startPos.Y)});

            
        }

        private void ProjectControl_Load(object sender, EventArgs e)
        {
            currentTime = new TimeSpan(0, 0, 0);
            pbWaveViewer.MouseMove += pbWaveViewer_MouseMove_NotDown;
            pbWaveViewer.MouseDown += pbWaveViewer_MouseDown;
            pbWaveViewer.DragEnter += pbWaveViewer_DragEnter;
            pbWaveViewer.DragDrop += pbWaveViewer_DragDrop;

            UpdatePointPos();
            (pbWaveViewer as Control).AllowDrop = true;
            markerPoint = new Point(Mathf.TimeToPos(Mathf.Clamp(Mathf.PosToTime(startPos.X, PlayerLineWidth, outputFileTime), new TimeSpan(), outputFileTime), currentTime, PlayerLineWidth), startPos.Y - object_radius * 2);
            
            pbWaveViewer.Invalidate();
        }

        #region "Moving Marker"
        // Мы двигаем маркер точку.
        private void pbWaveViewer_MouseMove_MovingMarker(object sender, MouseEventArgs e) {

            
            markerPoint = new Point(Mathf.TimeToPos(
                Mathf.Clamp(Mathf.PosToTime(e.X + OffsetX, PlayerLineWidth, outputFileTime), new TimeSpan(), GetAllTotalTime()), outputFileTime, PlayerLineWidth), startPos.Y - object_radius * 2);
            //UpdateMaskedTimeValue();
            // Перерисовать.
            pbWaveViewer.Invalidate();
        }

        // Остановить перемещение конечной точки.
        protected void pbWaveViewer_MouseUp_MovingMarker(object sender, MouseEventArgs e) {
            // Сброс обработчиков событий.
            pbWaveViewer.MouseMove += pbWaveViewer_MouseMove_NotDown;
            pbWaveViewer.MouseMove -= pbWaveViewer_MouseMove_MovingMarker;
            pbWaveViewer.MouseUp -= pbWaveViewer_MouseUp_MovingMarker;
            currentTime = Mathf.PosToTime(markerPoint.X, PlayerLineWidth, outputFileTime);
            maskedTextBoxCurrentTime.Text = currentTime.ToString(@"hh\:mm\:ss\.FF");

            markerMoving = false;
            // Перерисовать.
            pbWaveViewer.Invalidate();
        }

        private void pbWaveViewer_MouseMove_NotDown(object sender, MouseEventArgs e) {
            Cursor new_cursor = Cursors.Arrow;
            int segment_number;
            Point hit_point;
            if (MouseIsOverMarker(e.Location, out hit_point))
                new_cursor = Cursors.VSplit;
            else if (MouseIsOverSegment(e.Location, out segment_number))
                new_cursor = Cursors.Hand;

            // Установим новый курсор.
            if (pbWaveViewer.Cursor != new_cursor)
                pbWaveViewer.Cursor = new_cursor;
        }

        private void pbWaveViewer_MouseDown(object sender, MouseEventArgs e) {

            Point hit_point;
            int segment_number;
            if (MouseIsOverMarker(e.Location, out hit_point)) {
                // Начните перемещать эту конечную точку.
                pbWaveViewer.MouseMove -= pbWaveViewer_MouseMove_NotDown;
                pbWaveViewer.MouseMove += pbWaveViewer_MouseMove_MovingMarker;
                pbWaveViewer.MouseUp += pbWaveViewer_MouseUp_MovingMarker;
                markerMoving = true;
                //UpdateMaskedTimeValue();
                // Запомните смещение от мыши до точки.
                OffsetX = hit_point.X - e.X;
            }
            else
            if (MouseIsOverSegment(e.Location, out segment_number))
            {

                // Начните перемещение этого сегмента.
                pbWaveViewer.MouseMove -= pbWaveViewer_MouseMove_NotDown;
                pbWaveViewer.MouseMove += pbWaveViewer_MouseMove_MovingSegment;
                pbWaveViewer.MouseUp += pbWaveViewer_MouseUp_MovingSegment;

                // Запомните номер сегмента.
                MovingSegment = segment_number;
            }
        }

        protected bool MouseIsOverMarker(Point mouse_pt, out Point hit_pt) {

            if (FindDistanceToPointSquared(mouse_pt, markerPoint) < over_dist_squared * 2) {
                hit_pt = markerPoint;
                return true;
            } else {
                hit_pt = new Point(-1, -1);
                return false;
            }
        }

        protected int FindDistanceToPointSquared(Point pt1, Point pt2) {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;
            return dx * dx + dy * dy;
        }

        protected int FindDistanceToSplitPointSquared(Point pt1, Point pt2) {
            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt1.Y;
            return dx * dx + dy * dy;
        }
        #endregion // Перемещение конечной точки


        protected void pbWaveViewer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        protected void pbWaveViewer_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Sample)) as Sample != null)
            {
                Sample s = e.Data.GetData(typeof(Sample)) as Sample;
                project.GetSampleList().Insert(GetIndexQueue(pbWaveViewer.PointToClient(new Point(e.X, e.Y))), new Sample(s.SoundPath,s.OptimizedArray,s.DrawSource,s.SplitStartTime,s.SplitEndTime,s.TotalTime));
            }
            pbWaveViewer.Invalidate();
        }


        protected int GetIndexUpderMouse(Point pt1)
        {
            TimeSpan mouseTime = Mathf.PosToTime(pt1.X, PlayerLineWidth, outputFileTime);
            if (project.GetSampleList().Count > 0)
            {
                TimeSpan time = new TimeSpan();

                for (int i = 0; i < project.GetSampleList().Count; i++)
                {
                    TimeSpan splitTime2 = (
                    TimeSpan.FromSeconds((project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime).TotalSeconds));
                    if (mouseTime < time + splitTime2)
                        return i;
                    time += (project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime);
                }
                return -1;
            }
            else
                return -1;

            /*
            TimeSpan mouseTime = Mathf.PosToTime(pt1.X, PlayerLineWidth, outputFileTime);
            if (project.GetSampleList().Count > 0)
            {
                TimeSpan sumtime = new TimeSpan();

                for (int i = 0; i < project.GetSampleList().Count; i++)
                {
                    
                    if (sumtime <= mouseTime && mouseTime <= sumtime + (project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime))
                        return i;
                    else
                    sumtime += (project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime);
                }
                return -1;
            }
            else
                return -1;*/
        }


        protected int GetNewIndex(Point pt1)
        {
            TimeSpan mouseTime = Mathf.PosToTime(pt1.X, PlayerLineWidth, outputFileTime);
            if (project.GetSampleList().Count > 0)
            {
                TimeSpan sumtime = GetAllTotalTime();
                int index = project.GetSampleList().Count - 1;
                
                for (int i = project.GetSampleList().Count-1; i >=0; i--)
                {
                    sumtime -= (project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime);

                    if (mouseTime <= sumtime+ TimeSpan.FromSeconds(((project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime).TotalSeconds)/2))
                    {
                        index = Mathf.Clamp(i, 0, project.GetSampleList().Count);
                    }
                    else
                    {
                        break;
                    }
                }
                return index;
                //if (mouseTime > sumtime + TimeSpan.FromSeconds(
                //    (project.GetSampleList()[index].SplitEndTime - project.GetSampleList()[index].SplitStartTime).TotalSeconds / 2))
                //{
                //    return Mathf.Clamp(index + 1, 0, project.GetSampleList().Count);

                //}
                //else
                //{
                //    return Mathf.Clamp(index, 0, project.GetSampleList().Count);

                //}
                /*
                sumtime = new TimeSpan();
                for (int i = 0; i <= index; i++)
                {
                        sumtime += (project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime);
                }
                
                if (mouseTime+ TimeSpan.FromSeconds(((project.GetSampleList()[MovingSegment].SplitEndTime - project.GetSampleList()[MovingSegment].SplitStartTime).TotalSeconds) )
                    > sumtime)
                    return index;
                else*/
                return index;
            }
            else
                return 0;
        }


        protected int GetIndexQueue(Point pt1)
        {
            TimeSpan mouseTime = Mathf.PosToTime(pt1.X, PlayerLineWidth, outputFileTime);
            if (project.GetSampleList().Count > 0)
            {
                TimeSpan time = new TimeSpan();

                for (int i = 0; i < project.GetSampleList().Count; i++)
                {
                    TimeSpan splitTime2 = (
                    TimeSpan.FromSeconds((project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime).TotalSeconds / 2));

                    if (mouseTime < time + splitTime2)
                        return i;
                    time += (project.GetSampleList()[i].SplitEndTime - project.GetSampleList()[i].SplitStartTime);
                }
                return project.GetSampleList().Count;
            } else
                return 0;
        }

        private void pbWaveViewer_Layout(object sender, LayoutEventArgs e)
        {
            UpdatePointPos();
            markerPoint = new Point(Mathf.Clamp(Mathf.TimeToPos(currentTime, outputFileTime, PlayerLineWidth), startPos.X, endPos.X), markerPoint.Y);
            maskedTextBoxCurrentTime.Text = currentTime.ToString(@"hh\:mm\:ss\.FF");

            pbWaveViewer.Invalidate();
            //    UpdatePointPos();
            //    markerPoint = new Point(Mathf.TimeToPos(currentTime, outputFileTime, PlayerLineWidth),markerPoint.Y);
            //    maskedTextBoxCurrentTime.Text = currentTime.ToString(@"hh\:mm\:ss\.FF");

            //    pbWaveViewer.Invalidate();
        }

        private void bPlayPause_Click(object sender, EventArgs e) {

                projectPlayer = new ProjectPlayer(project.GetSampleList());
                projectPlayer.Timer.Tick += Timer_Tick;

            {
//                projectPlayer.SetListSample = project.GetSampleList();
            }
            projectPlayer.CurrentTime = currentTime;
            projectPlayer.Play();

        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTime = projectPlayer.CurrentTime;
            markerPoint = new Point(Mathf.Clamp(Mathf.TimeToPos(currentTime, outputFileTime, PlayerLineWidth), startPos.X, endPos.X), markerPoint.Y);
            maskedTextBoxCurrentTime.Text = currentTime.ToString();
            pbWaveViewer.Invalidate();
        }
        private void ProjectControl_Layout(object sender, LayoutEventArgs e)
        {
        //    UpdatePointPos();
        //    markerPoint = new Point(Mathf.Clamp( Mathf.TimeToPos(currentTime, outputFileTime, PlayerLineWidth),startPos.X,endPos.X), markerPoint.Y);
        //    maskedTextBoxCurrentTime.Text = currentTime.ToString(@"hh\:mm\:ss\.FF");

        //    pbWaveViewer.Invalidate();
        }

        private int MovingSegment = -1;

       
        // We're moving a segment.
        private void pbWaveViewer_MouseMove_MovingSegment(object sender, MouseEventArgs e)
        {

            // See how far the first point will move.
            Point pt1 = new System.Drawing.Point(e.X, e.Y);
            

            int newIndex = GetNewIndex((pt1));
            if (MovingSegment != newIndex&&project.GetSampleList().Count>0) { 
            //            int movingSegment = MovingSegment;
            maskedTextBoxCurrentTime.Mask = "";
            maskedTextBoxCurrentTime.Text = ""+MovingSegment+" "+newIndex;
            //newIndex = Mathf.Clamp(newIndex, 0, project.GetSampleList().Count);
            // MessageBox.Show(""+newIndex);
            Sample s = project.GetSampleList()[MovingSegment];
           project.GetSampleList().Remove(s);
                project.GetSampleList().Insert(newIndex, new Sample(s));
                MovingSegment = newIndex;
                //zx     if (project.GetSampleList()[i].IndexQueue == indexQueue)
            }
            // Redraw.
            pbWaveViewer.Invalidate();
        }
        // Stop moving the segment.
        private void pbWaveViewer_MouseUp_MovingSegment(object sender, MouseEventArgs e)
        {
            // Reset the event handlers.
            pbWaveViewer.MouseMove += pbWaveViewer_MouseMove_NotDown;
            pbWaveViewer.MouseMove -= pbWaveViewer_MouseMove_MovingSegment;
            pbWaveViewer.MouseUp -= pbWaveViewer_MouseUp_MovingSegment;
            MovingSegment = -1;
            // Redraw.
            pbWaveViewer.Invalidate();
        }

        private bool MouseIsOverSegment(Point mouse_pt, out int segment_number)
        {

             int i = GetIndexUpderMouse((new Point(mouse_pt.X, mouse_pt.Y)));
            if (i ==-1)
            {
                segment_number = -1;
                return false;
            }
            else
            {
                segment_number = i;
                return true;

            }
        }



        private TimeSpan GetAllTotalTime() {
            TimeSpan totalTime = new TimeSpan();
            foreach(var i in project.GetSampleList()) {
                totalTime += i.SplitEndTime-i.SplitStartTime;
            }
            return totalTime;
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            project.Save();
        }
    }
}
