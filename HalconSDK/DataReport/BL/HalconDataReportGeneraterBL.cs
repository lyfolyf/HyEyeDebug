using Autofac;
using GL.Kit.Log;
using HalconDotNet;
using HalconSDK.DataReport.UI;
using HyVision;
using HyVision.Models;
using HyVision.Tools;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconSDK.DataReport.BL
{
    [Serializable]
    public class HalconDataReportGeneraterBL : BaseHyUserTool
    {
        public const string INPUT_NAME_ALGORITHM_PRO_START_TIME = "AlgorithmProStartTime";
        public const string INPUT_NAME_MASK = "Mask";
        public const string INPUT_NAME_GRADX = "GradX";
        public const string INPUT_NAME_GRADY = "GradY";
        public const string INPUT_NAME_CLOUDZ = "CloudZ";
        public const string INPUT_NAME_ROW_SLOPE = "RowSlope";
        public const string INPUT_NAME_COL_SLOPE = "ColSlope";
        public const string INPUT_NAME_COLORZ = "ColorZ";
        public const string INPUT_NAME_CURVATURE_COL = "CurvatureCol";
        public const string INPUT_NAME_CURVATURE_ROW = "CurvatureRow";
        public const string INPUT_NAME_CURVATURE_MAX = "CurvatureMax";
        public const string INPUT_NAME_CURVATURE_MIN = "CurvatureMin";
        public const string INPUT_NAME_SCORE_TXT = "ScoreTxt";
        public const string INPUT_NAME_SCORE_HEAD = "ScoreHead";

        private bool isGeneratScoreTxt;
        public bool IsGeneratScoreTxt { get => isGeneratScoreTxt; set => isGeneratScoreTxt = value; }

        private bool isGeneratExcel;
        public bool IsGeneratExcel { get => isGeneratExcel; set => isGeneratExcel = value; }

        private bool isGeneratFeatureMap;
        public bool IsGeneratFeatureMap { get => isGeneratFeatureMap; set => isGeneratFeatureMap = value; }

        private bool isGeneratMidImage;
        public bool IsGeneratMidImage { get => isGeneratMidImage; set => isGeneratMidImage = value; }

        private string reportFolderPath;
        public string ReportFolderPath { get => reportFolderPath; set => reportFolderPath = value; }

        public override Type ToolEditType => typeof(HalconDataReportGeneraterUI);

        LogPublisher log;

        private void GeneratExcelReport(List<Bitmap> FeatureMapList, string ScoreHead, string ScoreTxt, string FolderPath, 
            string AlgorithmProStartTime, string AlgorithmProEndTime, string AlgorithmProDurationTime, string ProductName)
        {
            if (!Directory.Exists(FolderPath)) // 如果父亲文件夹不存在则创建
                Directory.CreateDirectory(FolderPath);
            string filePath = FolderPath = $@"{FolderPath}\final_result.major";

            if (!File.Exists(filePath))
            {
                try
                {
                    string[] FeatureMapNames = { "SlopeColorRow", "SlopeColorCol", "ColorZ", "CurvatureCol", "CurvatureRow", "CurvatureMax", "CurvatureMin" };
                    string[] HeadAndScore = { ScoreHead, ScoreTxt };

                    List<string> line1 = HeadAndScore[0].Split(',').ToList();
                    line1.Insert(0, "ProStart");
                    line1.Insert(1, "ProEnd");
                    line1.Insert(2, "ProDuration(ms)");
                    HeadAndScore[0] = string.Join(",", line1);

                    List<string> line2 = HeadAndScore[1].Split(',').ToList();
                    line2.Insert(0, AlgorithmProStartTime);
                    line2.Insert(1, AlgorithmProEndTime);
                    line2.Insert(2, AlgorithmProDurationTime);
                    HeadAndScore[1] = string.Join(",", line2);
                    GenMultiMaterialFinalResultExcel(FeatureMapList, HeadAndScore, ProductName, FeatureMapNames, filePath);
                }
                catch (Exception ex)
                {
                    OnException($"运行[{Name}]时发生异常：{ex.Message}", new HyVisionException(ex.Message));
                }
            }
            else
            {
                try
                {
                    List<string> line2 = ScoreTxt.Split(',').ToList();
                    line2.Insert(0, AlgorithmProStartTime);
                    line2.Insert(1, AlgorithmProEndTime);
                    line2.Insert(2, AlgorithmProDurationTime);
                    ScoreTxt = string.Join(",", line2);

                    GenMultiMaterialFinalResultExcel2(FeatureMapList, ScoreTxt, ProductName, filePath);
                }
                catch (Exception ex)
                {
                    OnException($"运行[{Name}]时发生异常：{ex.Message}", new HyVisionException(ex.Message));
                }

            }
        }

        private void GeneratTXTReport(string ScoreHead, string ScoreTxt, string FolderPath, string ProductName)
        {
            if (!Directory.Exists(FolderPath)) // 如果父亲文件夹不存在则创建
                Directory.CreateDirectory(FolderPath);
            string filePath = FolderPath = $@"{FolderPath}\Scores.csv";

            if (!File.Exists(filePath))
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine(ScoreHead);
                        sw.WriteLine(ScoreTxt);
                    }
                }
                catch (Exception ex)
                {
                    OnException($"运行[{Name}]时发生异常：{ex.Message}", new HyVisionException(ex.Message));
                }
            }
            else
            {
                try
                {
                    // append模式
                    using (StreamWriter sw = new StreamWriter(filePath, true))
                    {
                        sw.WriteLine(ScoreTxt);
                    }
                }
                catch (Exception ex)
                {
                    OnException($"运行[{Name}]时发生异常：{ex.Message}", new HyVisionException(ex.Message));
                }

            }
        }

        private int GenMultiMaterialFinalResultExcel(List<Bitmap> FeatureMapList, string[] HeadAndScore, string SubDirName, string[] FeatureMapNames, string filePath)
        {
            /***
             * 1.建表
             * ***/
            HSSFWorkbook wk = new HSSFWorkbook();
            ISheet image_sheet = wk.CreateSheet("Images");
            ISheet confidence_sheet = wk.CreateSheet("Confidence");


            /***
             * 2.定义单元格格式
             * ***/

            ICellStyle style_1 = wk.CreateCellStyle();
            style_1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style_1.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;


            ICellStyle style_2 = wk.CreateCellStyle();
            style_2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style_2.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            HSSFPalette palette = wk.GetCustomPalette(); //调色板实例
            palette.SetColorAtIndex((short)8, 141, 180, 226);  //第一个参数：设置调色板新增颜色的编号，自已设置即可；取值范围8-64
            HSSFColor hssFColor = palette.FindColor(141, 180, 226);
            style_2.FillForegroundColor = hssFColor.Indexed;
            style_2.FillPattern = FillPattern.SolidForeground;

            ICellStyle style_3 = wk.CreateCellStyle();
            style_3.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style_3.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            palette.SetColorAtIndex((short)9, 197, 217, 241);  //第一个参数：设置调色板新增颜色的编号，自已设置即可；取值范围8-64
            HSSFColor hssFColor2 = palette.FindColor(197, 217, 241);
            style_3.FillForegroundColor = hssFColor2.Indexed;
            style_3.FillPattern = FillPattern.SolidForeground;


            /***
             * 2.1  定义Image表的格式
             * ***/
            // 设置默认行高
            image_sheet.DefaultRowHeight = 230 * 20;
            // 设置列宽
            for (int i = 0; i < 100; i++)
            {
                image_sheet.SetColumnWidth(i + 1, 23 * 256);
            }


            /***
             * 2.2  定义Confidence表的格式
             * ***/

            for (int i = 0; i < 100; i++)
            {
                confidence_sheet.SetColumnWidth(i + 1, 23 * 256);
            }


            /***
            * 3.插入内容
            ***/
            IRow dataRow;
            ICell cell;

            /***
            * 3.1 Image表插入内容
            ***/
            List<byte[]> FeatureMapBytesList = new List<byte[]>();

            Bitmap SlopeColorRowBitmap = FeatureMapList[0];
            Bitmap SlopeColorColBitmap = FeatureMapList[1];
            Bitmap ColorZBitmap = FeatureMapList[2];
            Bitmap CurvatureColBitmap = FeatureMapList[3];
            Bitmap CurvatureRowBitmap = FeatureMapList[4];
            Bitmap CurvatureMaxBitmap = FeatureMapList[5];
            Bitmap CurvatureMinBitmap = FeatureMapList[6];


            FeatureMapBytesList.Add(Bitmap2Byte(SlopeColorRowBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(SlopeColorColBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(ColorZBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(CurvatureColBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(CurvatureRowBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(CurvatureMaxBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(CurvatureMinBitmap));


            //插入行名
            dataRow = image_sheet.CreateRow(0);
            cell = dataRow.CreateCell(0);
            cell.SetCellValue("Part1#");
            cell.CellStyle = style_2;

            cell = dataRow.CreateCell(1);
            cell.SetCellValue("Part2#");
            cell.CellStyle = style_2;

            //插入列名（序列号）
            dataRow = image_sheet.CreateRow(1);
            dataRow.Height = 250 * 20;

            cell = dataRow.CreateCell(0);
            cell.SetCellValue(0001);
            cell.CellStyle = style_3;

            cell = dataRow.CreateCell(1);
            cell.SetCellValue(SubDirName);
            cell.CellStyle = style_3;

            int k = 0;
            for (int i = 0; i < FeatureMapList.Count; i++)
            {

                // 第二步：确定图片索引
                int pictureIdx = wk.AddPicture(FeatureMapBytesList[k], PictureType.PNG); // 注意图片格式
                                                                                         // 第三步：创建画部
                IDrawing patriarch = image_sheet.CreateDrawingPatriarch();
                // 第四步：设置锚点
                int rowline = 0; // y方向
                                 // 参数说明：（在起始单元格的X坐标0-1023，Y的坐标0-255，在终止单元格的X坐标0-1023，Y的坐标0-255，起始单元格列数，行数，终止单元格列数，行数）
                int colline = 1; // x方向

                IClientAnchor anchor = patriarch.CreateAnchor(0, 0, 0, 0, colline + 1 + i, rowline + 1, colline + 1 + 1 + i, rowline + 1 + 1);
                // 第五步：把图片插到相应的位置
                IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);

                k++;
            }


            /**
             * 
             * 插入列名
             * 
             * **/
            dataRow = image_sheet.GetRow(0);
            dataRow.Height = 200 * 2;
            for (int j = 0; j < FeatureMapNames.Length; j++)
            {
                cell = dataRow.CreateCell(j + 2); // 确定单元格
                cell.SetCellValue(FeatureMapNames[j]);
                cell.CellStyle = style_1;

            }

            /***
            * 3.2 Confidence表插入内容
            ***/

            // 插入行名
            dataRow = confidence_sheet.CreateRow(0);
            cell = dataRow.CreateCell(0);
            cell.SetCellValue("Part1#");
            cell.CellStyle = style_2;

            cell = dataRow.CreateCell(1);
            cell.SetCellValue("Part2#");
            cell.CellStyle = style_2;

            // 将txt文本中的一行数据用','分割
            string[] line = HeadAndScore[0].Split(',');
            // 创建行
            //dataRow = confidence_sheet.GetRow(0);
            // 插入每行内容
            for (int j = 0; j < line.Length; j++)
            {
                //创建列，并写入值
                cell = dataRow.CreateCell(j + 2);
                cell.SetCellValue(line[j]);
                cell.CellStyle = style_1;
            }

            string[] line2 = HeadAndScore[1].Split(',');
            // 创建行
            dataRow = confidence_sheet.CreateRow(1);
            cell = dataRow.CreateCell(0);
            cell.SetCellValue(0001);
            cell.CellStyle = style_3;

            cell = dataRow.CreateCell(1);
            cell.SetCellValue(SubDirName);
            cell.CellStyle = style_3;


            // 插入每行内容

            for (int j = 0; j < 2; j++)
            {
                //创建列，并写入值
                cell = dataRow.CreateCell(j + 2);
                cell.SetCellValue(line2[j]);
                cell.CellStyle = style_1;
            }

            for (int j = 2; j < line2.Length; j++)
            {
                //创建列，并写入值
                cell = dataRow.CreateCell(j + 2);
                double doubV = 0;
                double.TryParse(line2[j], out doubV);
                cell.SetCellValue(doubV);
                cell.CellStyle = style_1;
            }



            // 获取行
            dataRow = confidence_sheet.GetRow(1);
            cell = dataRow.CreateCell(1);
            cell.SetCellValue(SubDirName);
            cell.CellStyle = style_3;


            /***
             * 4.保存Excel
             * ***/
            using (FileStream fs = File.OpenWrite(filePath))
            {
                wk.Write(fs);
            }


            //MessageBox.Show("多料生成完毕！");

            return 1;

        }

        private int GenMultiMaterialFinalResultExcel2(List<Bitmap> FeatureMapList, string ScoreTxt, string SubDirName, string filePath)
        {


            /***
             * 1.读已存在的Excel
             * ***/
            FileStream file;
            file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            HSSFWorkbook wk;
            wk = new HSSFWorkbook(file);
            file.Close();
            ISheet image_sheet = wk.GetSheet("Images");
            ISheet confidence_sheet = wk.GetSheet("Confidence");


            int rowsCount = image_sheet.PhysicalNumberOfRows; //取行Excel的最大行数
            int colsCount = image_sheet.GetRow(0).PhysicalNumberOfCells;//取得Excel的列数

            int confidence_sheet_rowsCount = confidence_sheet.PhysicalNumberOfRows; //取行Excel的最大行数
            int confidence_sheet_colsCount = confidence_sheet.GetRow(0).PhysicalNumberOfCells;//取得Excel的列数


            // MessageBox.Show("rowsCount:"+ rowsCount.ToString()+ "---colsCount:"+ colsCount.ToString());

            /***
             * 2.定义单元格格式
             * ***/

            ICellStyle style_1 = wk.CreateCellStyle();
            style_1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style_1.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;


            ICellStyle style_2 = wk.CreateCellStyle();
            style_2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style_2.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            HSSFPalette palette = wk.GetCustomPalette(); //调色板实例
            palette.SetColorAtIndex((short)8, 141, 180, 226);  //第一个参数：设置调色板新增颜色的编号，自已设置即可；取值范围8-64
            HSSFColor hssFColor = palette.FindColor(141, 180, 226);
            style_2.FillForegroundColor = hssFColor.Indexed;
            style_2.FillPattern = FillPattern.SolidForeground;

            ICellStyle style_3 = wk.CreateCellStyle();
            style_3.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style_3.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            palette.SetColorAtIndex((short)9, 197, 217, 241);  //第一个参数：设置调色板新增颜色的编号，自已设置即可；取值范围8-64
            HSSFColor hssFColor2 = palette.FindColor(197, 217, 241);
            style_3.FillForegroundColor = hssFColor2.Indexed;
            style_3.FillPattern = FillPattern.SolidForeground;


            /***
             * 2.1  定义Image表的格式
             * ***/


            /***
            * 3.插入内容
            ***/
            IRow dataRow;
            ICell cell;

            /***
            * 3.1 Image表插入内容
            ***/


            List<byte[]> FeatureMapBytesList = new List<byte[]>();

            Bitmap SlopeColorRowBitmap = FeatureMapList[0];
            Bitmap SlopeColorColBitmap = FeatureMapList[1];
            Bitmap ColorZBitmap = FeatureMapList[2];
            Bitmap CurvatureColBitmap = FeatureMapList[3];
            Bitmap CurvatureRowBitmap = FeatureMapList[4];
            Bitmap CurvatureMaxBitmap = FeatureMapList[5];
            Bitmap CurvatureMinBitmap = FeatureMapList[6];


            FeatureMapBytesList.Add(Bitmap2Byte(SlopeColorRowBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(SlopeColorColBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(ColorZBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(CurvatureColBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(CurvatureRowBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(CurvatureMaxBitmap));
            FeatureMapBytesList.Add(Bitmap2Byte(CurvatureMinBitmap));

            /**
            * 
            * 插入行名
            * 
            * **/
            dataRow = image_sheet.CreateRow(rowsCount);


            cell = dataRow.CreateCell(0);
            cell.SetCellValue(0001);
            cell.CellStyle = style_3;


            cell = dataRow.CreateCell(1);
            dataRow.Height = 230 * 20;
            cell.SetCellValue(SubDirName);
            cell.CellStyle = style_3;

            int k = 0;
            for (int i = 0; i < FeatureMapList.Count; i++)
            {

                // 第二步：确定图片索引
                int pictureIdx = wk.AddPicture(FeatureMapBytesList[k], PictureType.PNG); // 注意图片格式
                                                                                         // 第三步：创建画部
                IDrawing patriarch = image_sheet.CreateDrawingPatriarch();
                // 第四步：设置锚点
                int rowline = rowsCount - 1; // y方向
                                             // 参数说明：（在起始单元格的X坐标0-1023，Y的坐标0-255，在终止单元格的X坐标0-1023，Y的坐标0-255，起始单元格列数，行数，终止单元格列数，行数）
                int colline = 1;  //控制x方向

                IClientAnchor anchor = patriarch.CreateAnchor(0, 0, 0, 0, colline + i + 1, rowline + 1, colline + 1 + i + 1, rowline + 1 + 1);
                // 第五步：把图片插到相应的位置
                IPicture pict = patriarch.CreatePicture(anchor, pictureIdx);

                k++;

            }





            /***
            * 3.2 Confidence表插入内容
            ***/

            string[] line = ScoreTxt.Split(',');
            // 创建行
            dataRow = confidence_sheet.CreateRow(confidence_sheet_rowsCount);
            // 插入每行内容
            for (int j = 0; j < 2; j++)
            {
                //创建列，并写入值
                cell = dataRow.CreateCell(j + 2);
                cell.SetCellValue(line[j]);
                cell.CellStyle = style_1;
            }

            for (int j = 2; j < line.Length; j++)
            {
                //创建列，并写入值
                cell = dataRow.CreateCell(j + 2);
                double doubV = 0;
                double.TryParse(line[j], out doubV);
                cell.SetCellValue(doubV);
                cell.CellStyle = style_1;
            }


            // 获取行
            dataRow = confidence_sheet.GetRow(confidence_sheet_rowsCount);

            cell = dataRow.CreateCell(0);  // 获得单元格
            cell.SetCellValue(0001);
            cell.CellStyle = style_3;

            cell = dataRow.CreateCell(1);  // 获得单元格
            cell.SetCellValue(SubDirName);
            cell.CellStyle = style_3;



            /***
             * 4.保存Excel
             * ***/
            using (FileStream fs = File.OpenWrite(filePath))
            {
                wk.Write(fs);
            }
            wk.Close();

            //MessageBox.Show("多料生成完毕！");

            return 1;

        }

        private void GeneratMidImage(HObject ShowMask, HObject ShowGradX, HObject ShowGradY, HObject ShowCloudZ, string FolderPath)
        {
            try
            {
                DateTime MiddleImages_ProStart = DateTime.Now;
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    if (!Directory.Exists(FolderPath)) // 如果父亲文件夹不存在则创建
                        Directory.CreateDirectory(FolderPath);
                    HOperatorSet.WriteImage(ShowMask, "bmp", 0, $@"{FolderPath}\Mask.bmp");
                    HOperatorSet.WriteImage(ShowGradX, "tiff", 0, $@"{FolderPath}\GradX.tif");
                    HOperatorSet.WriteImage(ShowGradY, "tiff", 0, $@"{FolderPath}\GradY.tif");
                    HOperatorSet.WriteImage(ShowCloudZ, "tiff", 0, $@"{FolderPath}\CloudPtZ.tif");
                }
            }
            catch (Exception ex)
            {
                OnException($"运行[{Name}]时发生异常：{ex.Message}", new HyVisionException(ex.Message));
            }
        }

        private void GeneratFeatureMap(Bitmap RowSlope, Bitmap ColSlope, Bitmap ColorZ, Bitmap CurvatureCol, Bitmap CurvatureRow,
            Bitmap CurvatureMax, Bitmap CurvatureMin, string FolderPath)
        {
            try
            {
                // 特征图的保存
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    if (!Directory.Exists(FolderPath)) // 如果父亲文件夹不存在则创建
                        Directory.CreateDirectory(FolderPath);
                    RowSlope.Save($@"{FolderPath}\SlopeColorRow.bmp", ImageFormat.Bmp);
                    ColSlope.Save($@"{FolderPath}\SlopeColorCol.bmp", ImageFormat.Bmp);
                    ColorZ.Save($@"{FolderPath}\ColorZ.bmp", ImageFormat.Bmp);
                    CurvatureCol.Save($@"{FolderPath}\CurvatureCol.bmp", ImageFormat.Bmp);
                    CurvatureRow.Save($@"{FolderPath}\CurvatureRow.bmp", ImageFormat.Bmp);
                    CurvatureMax.Save($@"{FolderPath}\CurvatureMax.bmp", ImageFormat.Bmp);
                    CurvatureMin.Save($@"{FolderPath}\CurvatureMin.bmp", ImageFormat.Bmp);
                }
            }
            catch (Exception ex)
            {
                OnException($"运行[{Name}]时发生异常：{ex.Message}", new HyVisionException(ex.Message));
            }
        }

        /***
         * 
         * Bitmap2Byte():Bitmap 转 byte[]
         * 传入参数：
         * 返回参数：
         * 
         * ***/
        private byte[] Bitmap2Byte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

        protected override void Run2(string subName)
        {
            try
            {
                //log.Info($"开始执行[{Name}]");
                DateTime AlgorithmProStartTime = DateTime.Now;
                if(isGeneratExcel)
                {
                    if (Inputs.Contains(INPUT_NAME_ALGORITHM_PRO_START_TIME) && Inputs[INPUT_NAME_ALGORITHM_PRO_START_TIME] != null &&
                    Inputs[INPUT_NAME_ALGORITHM_PRO_START_TIME].Value != null && !string.IsNullOrEmpty(Inputs[INPUT_NAME_ALGORITHM_PRO_START_TIME].Value.ToString()))
                        AlgorithmProStartTime = Convert.ToDateTime(Inputs[INPUT_NAME_ALGORITHM_PRO_START_TIME].Value.ToString());
                }

                DateTime TaskStartTime = DateTime.Now;
                if (HyEye.API.GlobalParams.BatchRunStartTime != DateTime.MaxValue)
                    TaskStartTime = HyEye.API.GlobalParams.BatchRunStartTime;

                string now_time = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                // 算法处理结束时间
                DateTime AlgorithmProEndTime = DateTime.Now;
                // 计算的处理时间，包含了中间结果和特征图的生成
                string AlgorithmProDuration = (AlgorithmProEndTime - AlgorithmProStartTime).ToString("ssfff");
                string AlgorithmProStart = AlgorithmProStartTime.ToString("yyyy:MM:dd:HH:mm:ss:fff");
                string AlgorithmProEnd = AlgorithmProEndTime.ToString("yyyy:MM:dd:HH:mm:ss:fff");

                HalconDataConvert dataConvert = new HalconDataConvert();
                
                if(log == null)
                    log = AutoFacContainer.Resolve<LogPublisher>();

                if (isGeneratMidImage)
                {
                    log.Info($"【生成报告】开始生成中间结果图！");
                    if (!Inputs.Contains(INPUT_NAME_MASK) || Inputs[INPUT_NAME_MASK] == null || Inputs[INPUT_NAME_MASK].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_MASK} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_MASK} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_GRADX) || Inputs[INPUT_NAME_GRADX] == null || Inputs[INPUT_NAME_GRADX].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_GRADX} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_GRADX} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_GRADY) || Inputs[INPUT_NAME_GRADY] == null || Inputs[INPUT_NAME_GRADY].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_GRADY} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_GRADY} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CLOUDZ) || Inputs[INPUT_NAME_CLOUDZ] == null || Inputs[INPUT_NAME_CLOUDZ].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CLOUDZ} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CLOUDZ} 的值！"));
                    if(string.IsNullOrEmpty(reportFolderPath) || !Directory.Exists(reportFolderPath))
                        OnException($"输出结果文件夹 {reportFolderPath} 不存在！", new HyVisionException($"输出结果文件夹 {reportFolderPath} 不存在！"));

                    if (Inputs[INPUT_NAME_MASK].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_MASK} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_MASK].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_MASK} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_MASK].ValueType}"));
                    if (Inputs[INPUT_NAME_GRADX].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_GRADX} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_GRADX].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_GRADX} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_GRADX].ValueType}"));
                    if (Inputs[INPUT_NAME_GRADY].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_GRADY} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_GRADY].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_GRADY} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_GRADY].ValueType}"));
                    if (Inputs[INPUT_NAME_CLOUDZ].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CLOUDZ} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CLOUDZ].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CLOUDZ} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CLOUDZ].ValueType}"));

                    HObject ShowMask = dataConvert.ConvertToHObject(Inputs[INPUT_NAME_MASK]);
                    HObject ShowGradX = dataConvert.ConvertToHObject(Inputs[INPUT_NAME_GRADX]);
                    HObject ShowGradY = dataConvert.ConvertToHObject(Inputs[INPUT_NAME_GRADY]);
                    HObject ShowCloudZ = dataConvert.ConvertToHObject(Inputs[INPUT_NAME_CLOUDZ]);
                    string strFolderPath = $@"{reportFolderPath}\Result\result_{TaskStartTime.ToString("yyyyMMdd_hhmmss")}\{HyEye.API.GlobalParams.ProductName}\MiddleImages";
                    GeneratMidImage(ShowMask, ShowGradX, ShowGradY, ShowCloudZ, strFolderPath);
                    log.Info($"【生成报告】中间结果图生成完毕，保存在：{strFolderPath}！");
                }

                if(isGeneratFeatureMap)
                {
                    log.Info($"【生成报告】开始生成特征图！");
                    if (!Inputs.Contains(INPUT_NAME_ROW_SLOPE) || Inputs[INPUT_NAME_ROW_SLOPE] == null || Inputs[INPUT_NAME_ROW_SLOPE].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_ROW_SLOPE} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_ROW_SLOPE} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_COL_SLOPE) || Inputs[INPUT_NAME_COL_SLOPE] == null || Inputs[INPUT_NAME_COL_SLOPE].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_COL_SLOPE} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_COL_SLOPE} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_COLORZ) || Inputs[INPUT_NAME_COLORZ] == null || Inputs[INPUT_NAME_COLORZ].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_COLORZ} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_COLORZ} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CURVATURE_COL) || Inputs[INPUT_NAME_CURVATURE_COL] == null || Inputs[INPUT_NAME_CURVATURE_COL].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_COL} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_COL} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CURVATURE_ROW) || Inputs[INPUT_NAME_CURVATURE_ROW] == null || Inputs[INPUT_NAME_CURVATURE_ROW].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_ROW} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_ROW} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CURVATURE_MAX) || Inputs[INPUT_NAME_CURVATURE_MAX] == null || Inputs[INPUT_NAME_CURVATURE_MAX].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_MAX} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_MAX} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CURVATURE_MIN) || Inputs[INPUT_NAME_CURVATURE_MIN] == null || Inputs[INPUT_NAME_CURVATURE_MIN].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_MIN} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_MIN} 的值！"));
                    if (string.IsNullOrEmpty(reportFolderPath) || !Directory.Exists(reportFolderPath))
                        OnException($"输出结果文件夹 {reportFolderPath} 不存在！", new HyVisionException($"输出结果文件夹 {reportFolderPath} 不存在！"));

                    if (Inputs[INPUT_NAME_ROW_SLOPE].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_ROW_SLOPE} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_ROW_SLOPE].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_ROW_SLOPE} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_ROW_SLOPE].ValueType}"));
                    if (Inputs[INPUT_NAME_COL_SLOPE].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_COL_SLOPE} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_COL_SLOPE].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_COL_SLOPE} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_COL_SLOPE].ValueType}"));
                    if (Inputs[INPUT_NAME_COLORZ].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_COLORZ} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_COLORZ].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_COLORZ} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_COLORZ].ValueType}"));
                    if (Inputs[INPUT_NAME_CURVATURE_COL].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CURVATURE_COL} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_COL].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CURVATURE_COL} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_COL].ValueType}"));
                    if (Inputs[INPUT_NAME_CURVATURE_ROW].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CURVATURE_ROW} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_ROW].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CURVATURE_ROW} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_ROW].ValueType}"));
                    if (Inputs[INPUT_NAME_CURVATURE_MAX].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CURVATURE_MAX} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_MAX].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CURVATURE_MAX} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_MAX].ValueType}"));
                    if (Inputs[INPUT_NAME_CURVATURE_MIN].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CURVATURE_MIN} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_MIN].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CURVATURE_MIN} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_MIN].ValueType}"));

                    Bitmap RowSlope = ((HyImage)Inputs[INPUT_NAME_ROW_SLOPE].Value).Image;
                    Bitmap ColSlope = ((HyImage)Inputs[INPUT_NAME_COL_SLOPE].Value).Image;
                    Bitmap ColorZ = ((HyImage)Inputs[INPUT_NAME_COLORZ].Value).Image;
                    Bitmap CurvatureCol = ((HyImage)Inputs[INPUT_NAME_CURVATURE_COL].Value).Image;
                    Bitmap CurvatureRow = ((HyImage)Inputs[INPUT_NAME_CURVATURE_ROW].Value).Image;
                    Bitmap CurvatureMax = ((HyImage)Inputs[INPUT_NAME_CURVATURE_MAX].Value).Image;
                    Bitmap CurvatureMin = ((HyImage)Inputs[INPUT_NAME_CURVATURE_MIN].Value).Image;
                    string strFolderPath = $@"{reportFolderPath}\Result\result_{TaskStartTime.ToString("yyyyMMdd_hhmmss")}\{HyEye.API.GlobalParams.ProductName}\FeatureMap";
                    GeneratFeatureMap(RowSlope, ColSlope, ColorZ, CurvatureCol, CurvatureRow, CurvatureMax, CurvatureMin, strFolderPath);
                    log.Info($"【生成报告】特征图生成完毕，保存在：{strFolderPath}！");
                }

                if(isGeneratScoreTxt)
                {
                    log.Info($"【生成报告】开始生成txt文本数据！");
                    if (!Inputs.Contains(INPUT_NAME_SCORE_TXT) || Inputs[INPUT_NAME_SCORE_TXT] == null || 
                        Inputs[INPUT_NAME_SCORE_TXT].Value == null || string.IsNullOrEmpty(Inputs[INPUT_NAME_SCORE_TXT].Value.ToString()))
                        OnException($"输入参数中缺少参数 {INPUT_NAME_SCORE_TXT} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_SCORE_TXT} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_SCORE_HEAD) || Inputs[INPUT_NAME_SCORE_HEAD] == null || 
                        Inputs[INPUT_NAME_SCORE_HEAD].Value == null || string.IsNullOrEmpty(Inputs[INPUT_NAME_SCORE_HEAD].Value.ToString()))
                        OnException($"输入参数中缺少参数 {INPUT_NAME_SCORE_HEAD} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_SCORE_HEAD} 的值！"));

                    if (Inputs[INPUT_NAME_SCORE_TXT].ValueType != typeof(string))
                        OnException($"输入参数中参数 {INPUT_NAME_SCORE_TXT} 的类型不对！要求类型为 {typeof(string)} , 当前类型为 {Inputs[INPUT_NAME_SCORE_TXT].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_SCORE_TXT} 的类型不对！要求类型为 {typeof(string)} , 当前类型为 {Inputs[INPUT_NAME_SCORE_TXT].ValueType}"));
                    if (Inputs[INPUT_NAME_SCORE_HEAD].ValueType != typeof(string))
                        OnException($"输入参数中参数 {INPUT_NAME_SCORE_HEAD} 的类型不对！要求类型为 {typeof(string)} , 当前类型为 {Inputs[INPUT_NAME_SCORE_HEAD].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_SCORE_HEAD} 的类型不对！要求类型为 {typeof(string)} , 当前类型为 {Inputs[INPUT_NAME_SCORE_HEAD].ValueType}"));

                    string strFolderPath = $@"{reportFolderPath}\Result\result_{TaskStartTime.ToString("yyyyMMdd_hhmmss")}";
                    string strScoreHead = Inputs[INPUT_NAME_SCORE_HEAD].Value.ToString();
                    string strScoreTxt = Inputs[INPUT_NAME_SCORE_TXT].Value.ToString();
                    GeneratTXTReport(strScoreHead, strScoreTxt, strFolderPath, HyEye.API.GlobalParams.ProductName);
                    log.Info($"【生成报告】txt文本数据生成完毕，保存在：{strFolderPath}！");
                }

                if(isGeneratExcel)
                {
                    log.Info($"【生成报告】开始生成Excel报告！");
                    if (!Inputs.Contains(INPUT_NAME_ROW_SLOPE) || Inputs[INPUT_NAME_ROW_SLOPE] == null || Inputs[INPUT_NAME_ROW_SLOPE].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_ROW_SLOPE} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_ROW_SLOPE} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_COL_SLOPE) || Inputs[INPUT_NAME_COL_SLOPE] == null || Inputs[INPUT_NAME_COL_SLOPE].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_COL_SLOPE} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_COL_SLOPE} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_COLORZ) || Inputs[INPUT_NAME_COLORZ] == null || Inputs[INPUT_NAME_COLORZ].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_COLORZ} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_COLORZ} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CURVATURE_COL) || Inputs[INPUT_NAME_CURVATURE_COL] == null || Inputs[INPUT_NAME_CURVATURE_COL].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_COL} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_COL} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CURVATURE_ROW) || Inputs[INPUT_NAME_CURVATURE_ROW] == null || Inputs[INPUT_NAME_CURVATURE_ROW].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_ROW} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_ROW} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CURVATURE_MAX) || Inputs[INPUT_NAME_CURVATURE_MAX] == null || Inputs[INPUT_NAME_CURVATURE_MAX].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_MAX} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_MAX} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_CURVATURE_MIN) || Inputs[INPUT_NAME_CURVATURE_MIN] == null || Inputs[INPUT_NAME_CURVATURE_MIN].Value == null)
                        OnException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_MIN} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_CURVATURE_MIN} 的值！"));
                    if (string.IsNullOrEmpty(reportFolderPath) || !Directory.Exists(reportFolderPath))
                        OnException($"输出结果文件夹 {reportFolderPath} 不存在！", new HyVisionException($"输出结果文件夹 {reportFolderPath} 不存在！"));

                    if (Inputs[INPUT_NAME_ROW_SLOPE].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_ROW_SLOPE} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_ROW_SLOPE].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_ROW_SLOPE} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_ROW_SLOPE].ValueType}"));
                    if (Inputs[INPUT_NAME_COL_SLOPE].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_COL_SLOPE} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_COL_SLOPE].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_COL_SLOPE} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_COL_SLOPE].ValueType}"));
                    if (Inputs[INPUT_NAME_COLORZ].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_COLORZ} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_COLORZ].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_COLORZ} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_COLORZ].ValueType}"));
                    if (Inputs[INPUT_NAME_CURVATURE_COL].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CURVATURE_COL} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_COL].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CURVATURE_COL} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_COL].ValueType}"));
                    if (Inputs[INPUT_NAME_CURVATURE_ROW].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CURVATURE_ROW} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_ROW].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CURVATURE_ROW} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_ROW].ValueType}"));
                    if (Inputs[INPUT_NAME_CURVATURE_MAX].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CURVATURE_MAX} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_MAX].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CURVATURE_MAX} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_MAX].ValueType}"));
                    if (Inputs[INPUT_NAME_CURVATURE_MIN].ValueType != typeof(HyImage))
                        OnException($"输入参数中参数 {INPUT_NAME_CURVATURE_MIN} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_MIN].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_CURVATURE_MIN} 的类型不对！要求类型为 {typeof(HyImage)} , 当前类型为 {Inputs[INPUT_NAME_CURVATURE_MIN].ValueType}"));

                    if (!Inputs.Contains(INPUT_NAME_SCORE_TXT) || Inputs[INPUT_NAME_SCORE_TXT] == null ||
                        Inputs[INPUT_NAME_SCORE_TXT].Value == null || string.IsNullOrEmpty(Inputs[INPUT_NAME_SCORE_TXT].Value.ToString()))
                        OnException($"输入参数中缺少参数 {INPUT_NAME_SCORE_TXT} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_SCORE_TXT} 的值！"));
                    if (!Inputs.Contains(INPUT_NAME_SCORE_HEAD) || Inputs[INPUT_NAME_SCORE_HEAD] == null ||
                        Inputs[INPUT_NAME_SCORE_HEAD].Value == null || string.IsNullOrEmpty(Inputs[INPUT_NAME_SCORE_HEAD].Value.ToString()))
                        OnException($"输入参数中缺少参数 {INPUT_NAME_SCORE_HEAD} 的值！", new HyVisionException($"输入参数中缺少参数 {INPUT_NAME_SCORE_HEAD} 的值！"));

                    if (Inputs[INPUT_NAME_SCORE_TXT].ValueType != typeof(string))
                        OnException($"输入参数中参数 {INPUT_NAME_SCORE_TXT} 的类型不对！要求类型为 {typeof(string)} , 当前类型为 {Inputs[INPUT_NAME_SCORE_TXT].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_SCORE_TXT} 的类型不对！要求类型为 {typeof(string)} , 当前类型为 {Inputs[INPUT_NAME_SCORE_TXT].ValueType}"));
                    if (Inputs[INPUT_NAME_SCORE_HEAD].ValueType != typeof(string))
                        OnException($"输入参数中参数 {INPUT_NAME_SCORE_HEAD} 的类型不对！要求类型为 {typeof(string)} , 当前类型为 {Inputs[INPUT_NAME_SCORE_HEAD].ValueType}", new HyVisionException($"输入参数中参数 {INPUT_NAME_SCORE_HEAD} 的类型不对！要求类型为 {typeof(string)} , 当前类型为 {Inputs[INPUT_NAME_SCORE_HEAD].ValueType}"));

                    Bitmap RowSlope = ((HyImage)Inputs[INPUT_NAME_ROW_SLOPE].Value).Image;
                    Bitmap ColSlope = ((HyImage)Inputs[INPUT_NAME_COL_SLOPE].Value).Image;
                    Bitmap ColorZ = ((HyImage)Inputs[INPUT_NAME_COLORZ].Value).Image;
                    Bitmap CurvatureCol = ((HyImage)Inputs[INPUT_NAME_CURVATURE_COL].Value).Image;
                    Bitmap CurvatureRow = ((HyImage)Inputs[INPUT_NAME_CURVATURE_ROW].Value).Image;
                    Bitmap CurvatureMax = ((HyImage)Inputs[INPUT_NAME_CURVATURE_MAX].Value).Image;
                    Bitmap CurvatureMin = ((HyImage)Inputs[INPUT_NAME_CURVATURE_MIN].Value).Image;
                    string strFolderPath = $@"{reportFolderPath}\Result\result_{TaskStartTime.ToString("yyyyMMdd_hhmmss")}\{HyEye.API.GlobalParams.ProductName}\FeatureMap";

                    List<Bitmap> FeatureMapList = new List<Bitmap>();
                    FeatureMapList.Add(RowSlope);
                    FeatureMapList.Add(ColSlope);
                    FeatureMapList.Add(ColorZ);
                    FeatureMapList.Add(CurvatureCol);
                    FeatureMapList.Add(CurvatureRow);
                    FeatureMapList.Add(CurvatureMax);
                    FeatureMapList.Add(CurvatureMin);

                    string strScoreHead = Inputs[INPUT_NAME_SCORE_HEAD].Value.ToString();
                    string strScoreTxt = Inputs[INPUT_NAME_SCORE_TXT].Value.ToString();
                    GeneratExcelReport(FeatureMapList, strScoreHead, strScoreTxt, strFolderPath, AlgorithmProStart, AlgorithmProEnd, AlgorithmProDuration, HyEye.API.GlobalParams.ProductName);
                    log.Info($"【生成报告】Excel报告生成完毕，保存在：{strFolderPath}！");
                }
                log.Info($"[{Name}]执行完成！");
            }
            catch(Exception ex)
            {
                OnException($"运行[{Name}]时发生异常", new HyVisionException(ex.Message));
            }
        }

        public override object Clone(bool containsData)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 工具的初始化
        /// add by LuoDian @ 20220116
        /// </summary>
        public override bool Initialize()
        {
            return true;
        }

        /// <summary>
        /// 工具的保存接口，有的工具在保存参数之后，需要重新初始化，可以在这个保存接口里面复位初始化的状态
        /// add by LuoDian @ 20220116
        /// </summary>
        public override void Save()
        {
            
        }
    }
}
