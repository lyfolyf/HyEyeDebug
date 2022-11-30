using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;





namespace HyVision.Tools.ImageDisplay
{
    public class HyROIRepository
    {
        public HyROIRepository()
        {

        }


        private List<BaseHyROI> HyROIs = new List<BaseHyROI>();
        private List<BaseHyROI> OverLapHyROIs = new List<BaseHyROI>();
        private int OverLapROIIndex;

        private PointF MouseDownPoint;
        private BaseHyROI ReDrawSelectedHyROI;
        private BaseHyROI CurrentSelectedHyRoi;
        private int NextROIIndex, MaxIndex;



        private int CalculateHyROIIndex()
        {
            if (HyROIs.Count != MaxIndex )
            {
                NextROIIndex = 1;
                foreach (BaseHyROI roi in HyROIs)
                {
                    BaseHyROI roi1 = HyROIs.Find(h => h.Index == NextROIIndex);

                    if (roi1 != null)
                    {
                        NextROIIndex += 1;
                        continue;
                    }
                    else
                    {
                        return NextROIIndex;
                    }
                }
            }
            else
            {
                MaxIndex += 1;
                NextROIIndex = MaxIndex;
            }
            return NextROIIndex;
        }

        public void AddNewHyROI(ROIType RoiType, Color Color, float LineWidth = 0.1f)
        {
            Type type = Type.GetType("HyVision.Tools.ImageDisplay." + "Hy" + RoiType.ToString());

            BaseHyROI NewHyROI = Activator.CreateInstance(type) as BaseHyROI;

            NewHyROI.Index = CalculateHyROIIndex();
            NewHyROI.Color = Color;
            NewHyROI.LineWidth = LineWidth;
            NewHyROI.IsSelected = true;

            HyROIs.Insert(NewHyROI.Index - 1, NewHyROI);
            CurrentSelectedHyRoi = NewHyROI;
        }

        public void DeleteHyROI(int HyRoiIndex)
        {
            BaseHyROI roi = HyROIs.Find(h => h.Index == HyRoiIndex);
            HyROIs.Remove(roi);

            BaseHyROI selectedRoi = HyROIs.Find(h => h.IsSelected == true);
            CurrentSelectedHyRoi = selectedRoi;
        }

        public void DeleteHyROI()
        {
            HyROIs.RemoveAll(h => h.IsSelected == true);

            BaseHyROI selectedRoi = HyROIs.Find(h => h.IsSelected == true);
            CurrentSelectedHyRoi = selectedRoi;
        }

        //add by LuoDian @ 20210804 用于删除ROI的时候，查询删除的是哪些ROI
        public BaseHyROI[] GetAllSelectedHyROI()
        {
            return HyROIs.Where(h => h.IsSelected == true).ToArray();
        }

        public void MoveSelectedROI(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            List<BaseHyROI> SelectedHyROIs = HyROIs.FindAll(h => h.IsSelected == true);

            foreach (BaseHyROI roi in SelectedHyROIs)
            {
                roi.Move(ImgStartPoint, ImgEndPoint);
            }
        }

        public void Draw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            BaseHyROI SelectedHyROI = HyROIs.Find(h => h.IsSelected == true);

            if (SelectedHyROI != null)
            {
                SelectedHyROI.Draw(ImgStartPoint, ImgEndPoint);
                CurrentSelectedHyRoi = SelectedHyROI;
            }
        }

        public void ReDraw(PointF ImgStartPoint, PointF ImgEndPoint)
        {
            if (MouseDownPoint != ImgStartPoint)
            {
                MouseDownPoint = ImgStartPoint;
                ReDrawSelectedHyROI = HyROIs.Find(h => h.IsSelected == true && h.IsInsideHyROI(ImgStartPoint) > 0);
            }

            if (ReDrawSelectedHyROI != null)
            {
                ReDrawSelectedHyROI.ReDraw(ImgStartPoint, ImgEndPoint);
                CurrentSelectedHyRoi = ReDrawSelectedHyROI;
            }
        }

        public Cursor GetMouseType(PointF ImgPoint)
        {
            List<BaseHyROI> SelectedHyROIs = HyROIs.FindAll(h => h.IsSelected == true);

            foreach (BaseHyROI Roi in SelectedHyROIs)
            {
                Cursor cursor = Roi.GetMouseType(ImgPoint);
                if (cursor != Cursors.Default)
                {
                    return cursor;
                }
            }

            return default;
        }

        public int GetSelectedIndex(PointF ImgPoint)
        {
            BaseHyROI selectedHyROI = HyROIs.Find(h => h.IsSelected == true && h.IsInsideHyROI(ImgPoint) >= 0);

            if (selectedHyROI != null)
            {
                return selectedHyROI.Index;
            }

            return -1;
        }

        public void DisplayHyROI(Graphics Canvas)
        {
            foreach (BaseHyROI Roi in HyROIs)
            {
                if (Roi.Visible == true)
                {
                    Roi.Display(Canvas);
                }
            }
        }

        public void DisplayDefects(Graphics Canvas, PointF[] DefectPoints)
        {
            HyDefectRegion hyDefects = new HyDefectRegion
            {
                DefectPoints = DefectPoints.ToList()
            };
            hyDefects.Display(Canvas);
        }

        public void SetSelected(PointF InputPoint)
        {
            OverLapHyROIs.Clear();
            foreach (BaseHyROI Roi in HyROIs)
            {
                if (Roi.IsInsideHyROI(InputPoint) == 0)
                {
                    OverLapHyROIs.Add(Roi);
                }
            }

            int Total = OverLapHyROIs.Count;
            if (Total == 0)
            {
                SetSelected(false);
                CurrentSelectedHyRoi = null;
            }
            else if (Total == 1)
            {
                if (OverLapHyROIs[0].IsSelected == true)
                {
                    OverLapHyROIs[0].IsSelected = false;
                }
                else
                {
                    OverLapHyROIs[0].IsSelected = true;
                    CurrentSelectedHyRoi = OverLapHyROIs[0];
                }
            }
            else
            {
                foreach (BaseHyROI Roi in OverLapHyROIs)
                {
                    if (Roi.IsSelected == true)
                    {
                        Roi.IsSelected = false;
                    }
                }

                if (OverLapROIIndex >= Total)
                {
                    OverLapROIIndex = 0;
                }
                OverLapHyROIs[OverLapROIIndex].IsSelected = true;
                CurrentSelectedHyRoi = OverLapHyROIs[OverLapROIIndex];

                OverLapROIIndex += 1;

               
            }
        }

        public void SetSelected(int HyRoiIndex)
        {
            SetSelected(false);
            BaseHyROI HyRoi = HyROIs.Find(h => h.Index == HyRoiIndex);

            if (HyRoi != null)
            {
                HyRoi.IsSelected = true;
            }
        }

        public void SetSelected(bool SelectedAll = true)
        {
            foreach (BaseHyROI Roi in HyROIs)
            {
                if (SelectedAll == true)
                {
                    Roi.Visible = true;
                    Roi.IsSelected = true;
                }
                else
                {
                    Roi.IsSelected = false;
                }
            }
        }

        public void SetVisible(bool Visible)
        {
            foreach (BaseHyROI roi in HyROIs)
            {
                roi.Visible = Visible;
            }
        }

        public void SetColor(Color color)
        {
            foreach (BaseHyROI roi in HyROIs)
            {
                roi.Color = color;
            }
        }

        public void SetLineWidth(float lineWidth)
        {
            foreach (BaseHyROI roi in HyROIs)
            {
                roi.LineWidth = lineWidth;
            }
        }

        public void SetShapeWidth(float ShapeWidth)
        {
            BaseHyROI.ShapeWidth = ShapeWidth;
            foreach (BaseHyROI roi in HyROIs)
            {
                roi.CalculatePointPosition();
            }
        }

        public void SetFill(bool IsFill)
        {
            foreach (BaseHyROI roi in HyROIs)
            {
                roi.IsFill = IsFill;
            }
        }

        public List<BaseHyROI> GetHyROIs()
        {
            return HyROIs;
        }

        public void SetHyROIs(List<BaseHyROI> InputHyROIs)
        {
            this.HyROIs = InputHyROIs;
        }

        public BaseHyROI GetHyROI(int Index)
        {
            BaseHyROI HyRoi = HyROIs.Find(h => h.Index == Index);
            return HyRoi;
        }

        public BaseHyROI GetCurrentSelectedHyROI()
        {
            return CurrentSelectedHyRoi;
        }

    }

}
