using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace ImageStitchLib
{
    [Serializable]
    public class SysConfig
    {
        public static int SURFACE_ROW_COMMON = 3;
        public static int SURFACE_COL_COMMON = 4;
        public static int SURFACE_ROW_CORNER = 4;
        public static int SURFACE_COL_CORNER = 2;
        public static int SURFACE_ROW_SIDE = 6;
        public static int SURFACE_COL_SIDE13 = 3;
        public static int SURFACE_COL_SIDE24 = 2;

        public Dictionary<string, Surface> DictSurf { get; set; } = new Dictionary<string, Surface>()
        {
            {"TC", Surface.TC },
            {"LCM", Surface.LCM },
            {"Corner", Surface.Corner },
            {"Side1", Surface.Side1 },
            {"Side2", Surface.Side2 },
            {"Side3", Surface.Side3 },
            {"Side4", Surface.Side4 },
            { "DH", Surface.DH},
            { "BC", Surface.BC}
        };

        public void SetSurfaceCfg(Surface surface, SurfaceConfig config)
        {
            if (DictSurfaceCfg.ContainsKey(surface))
            {
                DictSurfaceCfg[surface] = config;
            }
        }

        //拼接后大图的子图结构
        public Dictionary<Surface, SurfaceConfig> DictSurfaceCfg { get; set; } = new Dictionary<Surface, SurfaceConfig>()
        {
            {Surface.TC,new SurfaceConfig(SURFACE_ROW_COMMON,SURFACE_COL_COMMON) },
            {Surface.LCM,new SurfaceConfig(SURFACE_ROW_COMMON,SURFACE_COL_COMMON) },
            {Surface.Mandrel,new SurfaceConfig(1,4) },
            {Surface.Logo,new SurfaceConfig(1,1) },
            {Surface.Corner,new SurfaceConfig(SURFACE_ROW_CORNER,SURFACE_COL_CORNER) },
            {Surface.Side1,new SurfaceConfig(SURFACE_ROW_SIDE,SURFACE_COL_SIDE13) },
            {Surface.Side2,new SurfaceConfig(SURFACE_ROW_SIDE,SURFACE_COL_SIDE24) },
            {Surface.Side3,new SurfaceConfig(SURFACE_ROW_SIDE,SURFACE_COL_SIDE13) },
            {Surface.Side4,new SurfaceConfig(SURFACE_ROW_SIDE,SURFACE_COL_SIDE24) },
            {Surface.DH,new SurfaceConfig(SURFACE_ROW_COMMON,SURFACE_COL_COMMON) },
            {Surface.BC,new SurfaceConfig(SURFACE_ROW_COMMON,SURFACE_COL_COMMON) }
        };

        public Dictionary<Surface, int> DictSurfaceImageCount { get; set; } = new Dictionary<Surface, int>()
        {
            {Surface.TC, 12 },
            {Surface.LCM,12 },
            {Surface.Mandrel,4 },
            {Surface.Logo,1 },
            {Surface.Corner,8 },
            {Surface.Side1,18 },
            {Surface.Side2,12 },
            {Surface.Side3,18 },
            {Surface.Side4,12 },
            {Surface.DH,12 },
            {Surface.BC,12 }
        };

    }

    [Serializable]
    public class SurfaceConfig
    {
        public int RowsCount
        {
            get { return rowsCount; }
            set
            {
                rowsCount = value;
            }
        }

        public SurfaceConfig(int rowsCount, int colsCount)
        {
            RowsCount = rowsCount;
            ColsCount = colsCount;
        }

        public int ColsCount
        {
            get { return colsCount; }
            set
            {
                colsCount = value;
            }
        }

        //只对大面有效，side和corner无效
        public Dictionary<Point, (int, int)> DictBlock = new Dictionary<Point, (int, int)>()
        {
            {new Point(1,1),(1, 4) },
            {new Point(1,2),(2, 4) },
            {new Point(1,3),(3, 4) },
            {new Point(2,1),(1, 3) },
            {new Point(2,2),(2, 3) },
            {new Point(2,3),(3, 3) },
            {new Point(3,1),(1, 2) },
            {new Point(3,2),(2, 2) },
            {new Point(3,3),(3, 2) },
            {new Point(4,1),(1, 1) },
            {new Point(4,2),(2, 1) },
            {new Point(4,3),(3, 1) }
        };

        private int rowsCount = 3;
        private int colsCount = 4;
    }
}
