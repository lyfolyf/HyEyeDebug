using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalconSDK.Calib
{
    public class Clib_xml

    {

        // Procedures 
        // Chapter: Calibration / Camera Parameters
        // Short Description: Generate a camera parameter tuple for an area scan camera with distortions modeled by the division model. 
        public void gen_cam_par_area_scan_division(HTuple hv_Focus, HTuple hv_Kappa, HTuple hv_Sx,
            HTuple hv_Sy, HTuple hv_Cx, HTuple hv_Cy, HTuple hv_ImageWidth, HTuple hv_ImageHeight,
            out HTuple hv_CameraParam)
        {
            // Local iconic variables 
            // Initialize local and output iconic variables 
            hv_CameraParam = new HTuple();
            //Generate a camera parameter tuple for an area scan camera
            //with distortions modeled by the division model.
            //
            hv_CameraParam.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_CameraParam = new HTuple();
                hv_CameraParam[0] = "area_scan_division";
                hv_CameraParam = hv_CameraParam.TupleConcat(hv_Focus, hv_Kappa, hv_Sx, hv_Sy, hv_Cx, hv_Cy, hv_ImageWidth, hv_ImageHeight);
            }


            return;
        }

        public void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
      HTuple hv_Bold, HTuple hv_Slant)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_OS = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_Style = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_AvailableFonts = new HTuple(), hv_Fdx = new HTuple();
            HTuple hv_Indices = new HTuple();
            HTuple hv_Font_COPY_INP_TMP = new HTuple(hv_Font);
            HTuple hv_Size_COPY_INP_TMP = new HTuple(hv_Size);

            // Initialize local and output iconic variables 
            try
            {
                //This procedure sets the text font of the current window with
                //the specified attributes.
                //
                //Input parameters:
                //WindowHandle: The graphics window for which the font will be set
                //Size: The font size. If Size=-1, the default of 16 is used.
                //Bold: If set to 'true', a bold font is used
                //Slant: If set to 'true', a slanted font is used
                //
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP.Dispose();
                    hv_Size_COPY_INP_TMP = 16;
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    //Restore previous behaviour
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = ((1.13677 * hv_Size_COPY_INP_TMP)).TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = hv_Size_COPY_INP_TMP.TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Courier";
                    hv_Fonts[1] = "Courier 10 Pitch";
                    hv_Fonts[2] = "Courier New";
                    hv_Fonts[3] = "CourierNew";
                    hv_Fonts[4] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Consolas";
                    hv_Fonts[1] = "Menlo";
                    hv_Fonts[2] = "Courier";
                    hv_Fonts[3] = "Courier 10 Pitch";
                    hv_Fonts[4] = "FreeMono";
                    hv_Fonts[5] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Luxi Sans";
                    hv_Fonts[1] = "DejaVu Sans";
                    hv_Fonts[2] = "FreeSans";
                    hv_Fonts[3] = "Arial";
                    hv_Fonts[4] = "Liberation Sans";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Times New Roman";
                    hv_Fonts[1] = "Luxi Serif";
                    hv_Fonts[2] = "DejaVu Serif";
                    hv_Fonts[3] = "FreeSerif";
                    hv_Fonts[4] = "Utopia";
                    hv_Fonts[5] = "Liberation Serif";
                }
                else
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple(hv_Font_COPY_INP_TMP);
                }
                hv_Style.Dispose();
                hv_Style = "";
                if ((int)(new HTuple(hv_Bold.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Bold";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Bold.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Italic";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Slant.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Style.TupleEqual(""))) != 0)
                {
                    hv_Style.Dispose();
                    hv_Style = "Normal";
                }
                hv_AvailableFonts.Dispose();
                HOperatorSet.QueryFont(hv_WindowHandle, out hv_AvailableFonts);
                hv_Font_COPY_INP_TMP.Dispose();
                hv_Font_COPY_INP_TMP = "";
                for (hv_Fdx = 0; (int)hv_Fdx <= (int)((new HTuple(hv_Fonts.TupleLength())) - 1); hv_Fdx = (int)hv_Fdx + 1)
                {
                    hv_Indices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Indices = hv_AvailableFonts.TupleFind(
                            hv_Fonts.TupleSelect(hv_Fdx));
                    }
                    if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleGreater(
                        0))) != 0)
                    {
                        if ((int)(new HTuple(((hv_Indices.TupleSelect(0))).TupleGreaterEqual(0))) != 0)
                        {
                            hv_Font_COPY_INP_TMP.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(
                                    hv_Fdx);
                            }
                            break;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(""))) != 0)
                {
                    throw new HalconException("Wrong value of control parameter Font");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Font = (((hv_Font_COPY_INP_TMP + "-") + hv_Style) + "-") + hv_Size_COPY_INP_TMP;
                        hv_Font_COPY_INP_TMP.Dispose();
                        hv_Font_COPY_INP_TMP = ExpTmpLocalVar_Font;
                    }
                }
                HOperatorSet.SetFont(hv_WindowHandle, hv_Font_COPY_INP_TMP);

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Local procedures 

        //public string source_path;
        //public  HTuple hv_HomMat3DObjInCamera;
        //public  HTuple hv_HomMat3Dscreen;


        public HTuple innerImg_path = new HTuple();
        public HTuple HomMat3DObjInCamera = new HTuple();
        public HTuple HomMat3Dscreen = new HTuple(), open_XmlFile_path = new HTuple();

        public HTuple HomTemp = new HTuple();
        public HTuple pixelSizeX = new HTuple();
        public HTuple pixelSizeY = new HTuple();
        public HTuple hv_value = new HTuple();

        public HTuple save_XmlFile_path = new HTuple();
        public HTuple backup_XmlFile_path = new HTuple();
        public HTuple gamma_path = new HTuple();
        public HTuple ng_img = new HTuple();
        public HTuple Indextxt = new HTuple();





        public HTupleVector Elements = new HTupleVector(2);

        //参数
        public HTuple StdMaxs = new HTuple();
        public HTuple StdMins = new HTuple();
        public HTuple ProductMaxs = new HTuple();
        public HTuple ProductMins = new HTuple();
        public HTuple Wid_Rate = new HTuple();
        public HTuple Height_Rate = new HTuple();
        public HTuple hv_rmse = new HTuple();
        public HTuple hv_gamma = new HTuple();


        //MachineMaster.clibxml.pathStd = text_InnerImgPath_1.Text + "\pathStd";
        //    MachineMaster.clibxml.pathScreen = text_InnerImgPath_1.Text + "\pathScreen";


        // Local procedures 

        public void list_image_files(HTuple hv_ImageDirectory, HTuple hv_Extensions, HTuple hv_Options,
      out HTuple hv_ImageFiles)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ImageDirectoryIndex = new HTuple();
            HTuple hv_ImageFilesTmp = new HTuple(), hv_CurrentImageDirectory = new HTuple();
            HTuple hv_HalconImages = new HTuple(), hv_OS = new HTuple();
            HTuple hv_Directories = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Length = new HTuple(), hv_NetworkDrive = new HTuple();
            HTuple hv_Substring = new HTuple(), hv_FileExists = new HTuple();
            HTuple hv_AllFiles = new HTuple(), hv_i = new HTuple();
            HTuple hv_Selection = new HTuple();
            HTuple hv_Extensions_COPY_INP_TMP = new HTuple(hv_Extensions);

            // Initialize local and output iconic variables 
            hv_ImageFiles = new HTuple();
            //This procedure returns all files in a given directory
            //with one of the suffixes specified in Extensions.
            //
            //Input parameters:
            //ImageDirectory: Directory or a tuple of directories with images.
            //   If a directory is not found locally, the respective directory
            //   is searched under %HALCONIMAGES%/ImageDirectory.
            //   See the Installation Guide for further information
            //   in case %HALCONIMAGES% is not set.
            //Extensions: A string tuple containing the extensions to be found
            //   e.g. ['png','tif',jpg'] or others
            //If Extensions is set to 'default' or the empty string '',
            //   all image suffixes supported by HALCON are used.
            //Options: as in the operator list_files, except that the 'files'
            //   option is always used. Note that the 'directories' option
            //   has no effect but increases runtime, because only files are
            //   returned.
            //
            //Output parameter:
            //ImageFiles: A tuple of all found image file names
            //
            if ((int)((new HTuple((new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
                "default")))) != 0)
            {
                hv_Extensions_COPY_INP_TMP.Dispose();
                hv_Extensions_COPY_INP_TMP = new HTuple();
                hv_Extensions_COPY_INP_TMP[0] = "ima";
                hv_Extensions_COPY_INP_TMP[1] = "tif";
                hv_Extensions_COPY_INP_TMP[2] = "tiff";
                hv_Extensions_COPY_INP_TMP[3] = "gif";
                hv_Extensions_COPY_INP_TMP[4] = "bmp";
                hv_Extensions_COPY_INP_TMP[5] = "jpg";
                hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                hv_Extensions_COPY_INP_TMP[7] = "jp2";
                hv_Extensions_COPY_INP_TMP[8] = "jxr";
                hv_Extensions_COPY_INP_TMP[9] = "png";
                hv_Extensions_COPY_INP_TMP[10] = "pcx";
                hv_Extensions_COPY_INP_TMP[11] = "ras";
                hv_Extensions_COPY_INP_TMP[12] = "xwd";
                hv_Extensions_COPY_INP_TMP[13] = "pbm";
                hv_Extensions_COPY_INP_TMP[14] = "pnm";
                hv_Extensions_COPY_INP_TMP[15] = "pgm";
                hv_Extensions_COPY_INP_TMP[16] = "ppm";
                //
            }
            hv_ImageFiles.Dispose();
            hv_ImageFiles = new HTuple();
            //Loop through all given image directories.
            for (hv_ImageDirectoryIndex = 0; (int)hv_ImageDirectoryIndex <= (int)((new HTuple(hv_ImageDirectory.TupleLength()
                )) - 1); hv_ImageDirectoryIndex = (int)hv_ImageDirectoryIndex + 1)
            {
                hv_ImageFilesTmp.Dispose();
                hv_ImageFilesTmp = new HTuple();
                hv_CurrentImageDirectory.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_CurrentImageDirectory = hv_ImageDirectory.TupleSelect(
                        hv_ImageDirectoryIndex);
                }
                if ((int)(new HTuple(hv_CurrentImageDirectory.TupleEqual(""))) != 0)
                {
                    hv_CurrentImageDirectory.Dispose();
                    hv_CurrentImageDirectory = ".";
                }
                hv_HalconImages.Dispose();
                HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                ";");
                            hv_HalconImages.Dispose();
                            hv_HalconImages = ExpTmpLocalVar_HalconImages;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                ":");
                            hv_HalconImages.Dispose();
                            hv_HalconImages = ExpTmpLocalVar_HalconImages;
                        }
                    }
                }
                hv_Directories.Dispose();
                hv_Directories = new HTuple(hv_CurrentImageDirectory);
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_HalconImages.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Directories = hv_Directories.TupleConcat(
                                ((hv_HalconImages.TupleSelect(hv_Index)) + "/") + hv_CurrentImageDirectory);
                            hv_Directories.Dispose();
                            hv_Directories = ExpTmpLocalVar_Directories;
                        }
                    }
                }
                hv_Length.Dispose();
                HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NetworkDrive.Dispose();
                    HOperatorSet.TupleGenConst(new HTuple(hv_Length.TupleLength()), 0, out hv_NetworkDrive);
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        if ((int)(new HTuple(((((hv_Directories.TupleSelect(hv_Index))).TupleStrlen()
                            )).TupleGreater(1))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Substring.Dispose();
                                HOperatorSet.TupleStrFirstN(hv_Directories.TupleSelect(hv_Index), 1,
                                    out hv_Substring);
                            }
                            if ((int)((new HTuple(hv_Substring.TupleEqual("//"))).TupleOr(new HTuple(hv_Substring.TupleEqual(
                                "\\\\")))) != 0)
                            {
                                if (hv_NetworkDrive == null)
                                    hv_NetworkDrive = new HTuple();
                                hv_NetworkDrive[hv_Index] = 1;
                            }
                        }
                    }
                }
                hv_ImageFilesTmp.Dispose();
                hv_ImageFilesTmp = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Directories.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_FileExists.Dispose();
                        HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
                    }
                    if ((int)(hv_FileExists) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_AllFiles.Dispose();
                            HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HTuple("files")).TupleConcat(
                                hv_Options), out hv_AllFiles);
                        }
                        hv_ImageFilesTmp.Dispose();
                        hv_ImageFilesTmp = new HTuple();
                        for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Extensions_COPY_INP_TMP.TupleLength()
                            )) - 1); hv_i = (int)hv_i + 1)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Selection.Dispose();
                                HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                                    hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ImageFilesTmp = hv_ImageFilesTmp.TupleConcat(
                                        hv_Selection);
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                }
                            }
                        }
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("\\\\")).TupleConcat(
                                "replace_all"), "/", out ExpTmpOutVar_0);
                            hv_ImageFilesTmp.Dispose();
                            hv_ImageFilesTmp = ExpTmpOutVar_0;
                        }
                        if ((int)(hv_NetworkDrive.TupleSelect(hv_Index)) != 0)
                        {
                            {
                                HTuple ExpTmpOutVar_0;
                                HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                    "replace_all"), "/", out ExpTmpOutVar_0);
                                hv_ImageFilesTmp.Dispose();
                                hv_ImageFilesTmp = ExpTmpOutVar_0;
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ImageFilesTmp = "/" + hv_ImageFilesTmp;
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                }
                            }
                        }
                        else
                        {
                            {
                                HTuple ExpTmpOutVar_0;
                                HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                    "replace_all"), "/", out ExpTmpOutVar_0);
                                hv_ImageFilesTmp.Dispose();
                                hv_ImageFilesTmp = ExpTmpOutVar_0;
                            }
                        }
                        break;
                    }
                }
                //Concatenate the output image paths.
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ImageFiles = hv_ImageFiles.TupleConcat(
                            hv_ImageFilesTmp);
                        hv_ImageFiles.Dispose();
                        hv_ImageFiles = ExpTmpLocalVar_ImageFiles;
                    }
                }
            }

            hv_Extensions_COPY_INP_TMP.Dispose();
            hv_ImageDirectoryIndex.Dispose();
            hv_ImageFilesTmp.Dispose();
            hv_CurrentImageDirectory.Dispose();
            hv_HalconImages.Dispose();
            hv_OS.Dispose();
            hv_Directories.Dispose();
            hv_Index.Dispose();
            hv_Length.Dispose();
            hv_NetworkDrive.Dispose();
            hv_Substring.Dispose();
            hv_FileExists.Dispose();
            hv_AllFiles.Dispose();
            hv_i.Dispose();
            hv_Selection.Dispose();

            return;
        }


        public void check_Singleimg(out HObject ho_ImageMirror, out HTuple STDindex, HTuple hv_dir, HTuple hv_index)
        {



            // Local iconic variables 

            HObject ho_Image, ho_Caltab = null;

            // Local control variables 

            HTuple hv_path = new HTuple(), hv_dcpdir = new HTuple(), isNumber = new HTuple();
            HTuple hv_NumImages = new HTuple();
            HTuple hv_Files = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_StartCamPar = new HTuple();
            HTuple hv_CalibDataID = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageMirror);
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Caltab);

            hv_path.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_path = hv_dir + "/inner";
            }


            hv_dcpdir.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_dcpdir = hv_dir + "/HC-105.cpd";
            }

            hv_Files.Dispose();
            HOperatorSet.ListFiles(hv_path, "files", out hv_Files);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Image.Dispose();
                HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(0));
            }
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            hv_NumImages.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_NumImages = new HTuple(hv_Files.TupleLength()
                    );
            }

            //初始赋值参数（焦距，kappa，像元水平尺寸，竖直尺寸，中点列坐标，中点行坐标，图像宽，图像高）
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_StartCamPar.Dispose();
                gen_cam_par_area_scan_division(0.030, 0, 0.00000345, 0.00000345, hv_Width / 2,
                    hv_Height / 2, hv_Width, hv_Height, out hv_StartCamPar);
            }
            //****************标定工作状态的相机内参
            hv_CalibDataID.Dispose();
            HOperatorSet.CreateCalibData("calibration_object", 1, 1, out hv_CalibDataID);
            HOperatorSet.SetCalibDataCamParam(hv_CalibDataID, 0, new HTuple(), hv_StartCamPar);
            HOperatorSet.SetCalibDataCalibObject(hv_CalibDataID, 0, hv_dcpdir);
            //Note, we do not use the image from which the pose of the measurement plane can be derived


            if (hv_index == "std")
            {
                hv_path.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_path = hv_dir + "/std";
                }
                hv_Files.Dispose();
                HOperatorSet.ListFiles(hv_path, "files", out hv_Files);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ImageMirror.Dispose();
                    HOperatorSet.ReadImage(out ho_ImageMirror, hv_Files.TupleSelect(0));
                }

                STDindex = null;
            }
            else
            {
                isNumber.Dispose();
                HOperatorSet.TupleNumber(hv_index, out hv_index);
                HOperatorSet.TupleIsNumber(hv_index, out isNumber);
                if (isNumber && hv_index <= hv_NumImages && hv_index != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_Image.Dispose();
                        HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(hv_index - 1));
                    }

                    ho_ImageMirror.Dispose();
                    HOperatorSet.MirrorImage(ho_Image, out ho_ImageMirror, "row");

                    //HOperatorSet.FindCalibObject(ho_ImageMirror, hv_CalibDataID, 0, 0, 0,
                    //        new HTuple(), new HTuple());

                    //ho_Caltab.Dispose();
                    //HOperatorSet.GetCalibDataObservContours(out ho_Caltab, hv_CalibDataID,
                    //    "caltab", 0, 0, 0);
                    //HOperatorSet.ConcatObj(ho_Caltab, ho_ImageMirror, out ho_ImageMirror);



                    STDindex = null;
                }
                else
                {
                    STDindex = null;
                    STDindex = "Index输入错误";
                }

            }
            ho_Image.Dispose();
            hv_path.Dispose();
            hv_dcpdir.Dispose();
            hv_Files.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            isNumber.Dispose();
            hv_StartCamPar.Dispose();
            hv_CalibDataID.Dispose();
            hv_NumImages.Dispose();

            return;
        }


        public void check_img_caltab(HTuple hv_windows, HTuple hv_dir, out HTuple hv_NGnum)
        {



            // Local iconic variables 

            HObject ho_Image, ho_ImageMirror = null, ho_Caltab = null;

            // Local control variables 

            HTuple hv_path = new HTuple(), hv_dcpdir = new HTuple();
            HTuple hv_stdpath = new HTuple();
            HTuple hv_Number = new HTuple();

            HTuple hv_Files = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_StartCamPar = new HTuple();
            HTuple hv_CalibDataID = new HTuple(), hv_NumImages = new HTuple();
            HTuple hv_NG = new HTuple(), hv_I = new HTuple(), hv_i = new HTuple();
            HTuple hv_Exception = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageMirror);
            HOperatorSet.GenEmptyObj(out ho_Caltab);
            hv_NGnum = new HTuple();
            try
            {

                //HDevWindowStack.SetActive(hv_windows);
                set_display_font(hv_windows, 15, "mono", "true", "false");
                hv_path.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_path = hv_dir + "/inner";
                }
                hv_dcpdir.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_dcpdir = hv_dir + "/HC-105.cpd";
                }
                hv_Files.Dispose();

                hv_stdpath.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_stdpath = hv_dir + "/std";
                }


                HOperatorSet.ListFiles(hv_path, "files", out hv_Files);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(0));
                }
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);


                //初始赋值参数（焦距，kappa，像元水平尺寸，竖直尺寸，中点列坐标，中点行坐标，图像宽，图像高）
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_StartCamPar.Dispose();
                    gen_cam_par_area_scan_division(0.030, 0, 0.00000345, 0.00000345, hv_Width / 2,
                        hv_Height / 2, hv_Width, hv_Height, out hv_StartCamPar);
                }

                //****************标定工作状态的相机内参
                hv_CalibDataID.Dispose();
                HOperatorSet.CreateCalibData("calibration_object", 1, 1, out hv_CalibDataID);
                HOperatorSet.SetCalibDataCamParam(hv_CalibDataID, 0, new HTuple(), hv_StartCamPar);
                HOperatorSet.SetCalibDataCalibObject(hv_CalibDataID, 0, hv_dcpdir);
                hv_NumImages.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumImages = new HTuple(hv_Files.TupleLength()
                        );
                }
                //Note, we do not use the image from which the pose of the measurement plane can be derived
                hv_NG.Dispose();
                hv_NG = new HTuple();
                HTuple end_val20 = hv_NumImages - 1;
                HTuple step_val20 = 1;
                for (hv_I = 0; hv_I.Continue(end_val20, step_val20); hv_I = hv_I.TupleAdd(step_val20))
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_Image.Dispose();
                        HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(hv_I));
                    }
                    ho_ImageMirror.Dispose();
                    HOperatorSet.MirrorImage(ho_Image, out ho_ImageMirror, "row");
                    hv_i.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_i = hv_I + 1;
                    }
                    try
                    {
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispObj(ho_ImageMirror, hv_windows);
                        //}
                        HOperatorSet.FindCalibObject(ho_ImageMirror, hv_CalibDataID, 0, 0, hv_I,
                            new HTuple(), new HTuple());
                        ho_Caltab.Dispose();
                        HOperatorSet.GetCalibDataObservContours(out ho_Caltab, hv_CalibDataID,
                            "caltab", 0, 0, hv_I);
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.SetColor(hv_windows, "green");
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispObj(ho_Caltab, hv_windows);
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            HOperatorSet.DispText(hv_windows, (("第 " + hv_i) + " 幅图像：") + "OK",
                                "window", "top", "left", "green", (new HTuple("box_color")).TupleConcat(
                                "shadow"), (new HTuple("white")).TupleConcat("false"));
                        }
                        //}
                        HOperatorSet.WaitSeconds(0.5);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_NG = hv_NG.TupleConcat(
                                    hv_I + 1);
                                hv_NG.Dispose();
                                hv_NG = ExpTmpLocalVar_NG;
                            }
                        }
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.SetColor(hv_windows, "red");
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            HOperatorSet.DispText(hv_windows, (("第 " + hv_i) + " 幅图像：") + "NG",
                                "window", "top", "left", "red", (new HTuple("box_color")).TupleConcat(
                                "shadow"), (new HTuple("white")).TupleConcat("false"));
                        }
                        //}
                        HOperatorSet.WaitSeconds(1.5);
                        continue;
                    }
                    //if (HDevWindowStack.IsOpen())
                    //{
                    HOperatorSet.ClearWindow(hv_windows);
                    //}
                    //hv_NGnum.Dispose();
                    //HOperatorSet.TupleSort(hv_NG, out hv_NGnum);


                }
                ho_Image.Dispose();
                ho_ImageMirror.Dispose();
                ho_Caltab.Dispose();

                hv_path.Dispose();
                hv_dcpdir.Dispose();
                hv_Files.Dispose();
                //hv_Width.Dispose();
                //hv_Height.Dispose();
                //hv_StartCamPar.Dispose();
                //hv_CalibDataID.Dispose();
                //hv_NumImages.Dispose();
                hv_I.Dispose();
                hv_i.Dispose();
                hv_Exception.Dispose();

                /////////////////////////////////////////////////////////////////////////////////////
                //***********************************
                hv_Files.Dispose();
                HOperatorSet.ListFiles(hv_stdpath, "files", out hv_Files);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(0));
                }
                try
                {


                    HOperatorSet.FindCalibObject(ho_Image, hv_CalibDataID, 0, 0, 0, new HTuple(),
                        new HTuple());
                    ho_Caltab.Dispose();
                    HOperatorSet.GetCalibDataObservContours(out ho_Caltab, hv_CalibDataID, "marks",
                        0, 0, 0);
                    hv_Number.Dispose();
                    HOperatorSet.CountObj(ho_Caltab, out hv_Number);
                    if ((int)(new HTuple(hv_Number.TupleNotEqual(195))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_NG = hv_NG.TupleConcat(
                                    'S');
                                hv_NG.Dispose();
                                hv_NG = ExpTmpLocalVar_NG;
                            }
                        }
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.SetColor(hv_windows, "red");
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispObj(ho_Image, hv_windows);
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispObj(ho_Caltab, hv_windows);
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispText(hv_windows, ("STD 标定图像：") + "NG", "window", "top",
                            "center", "red", (new HTuple("box")).TupleConcat("shadow"), (new HTuple("false")).TupleConcat(
                            "false"));
                        //}
                        HOperatorSet.WaitSeconds(1.5);
                    }
                    else
                    {
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.SetColor(hv_windows, "green");
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispObj(ho_Image, hv_windows);
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispObj(ho_Caltab, hv_windows);
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispText(hv_windows, ("STD 标定图像：") + "OK", "window", "top",
                            "center", "green", (new HTuple("box")).TupleConcat("shadow"), (new HTuple("false")).TupleConcat(
                            "false"));
                        HOperatorSet.WaitSeconds(0.5);
                        //}
                    }
                }
                catch
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {

                            HOperatorSet.DispObj(ho_Image, hv_windows);
                            HOperatorSet.DispText(hv_windows, ("STD 标定图像：") + "NG", "window", "top",
                           "center", "red", (new HTuple("box")).TupleConcat("shadow"), (new HTuple("false")).TupleConcat(
                           "false"));


                            HTuple
                              ExpTmpLocalVar_NG = hv_NG.TupleConcat(
                                "std");
                            hv_NG.Dispose();
                            hv_NG = ExpTmpLocalVar_NG;

                            HOperatorSet.WaitSeconds(1.5);
                        }

                    }
                }
                //if (HDevWindowStack.IsOpen())
                //{
                HOperatorSet.ClearWindow(hv_windows);
                //}
                //*********************************

                hv_NGnum.Dispose();
                HOperatorSet.TupleSort(hv_NG, out hv_NGnum);

                ho_Image.Dispose();
                ho_Caltab.Dispose();

                hv_dir.Dispose();
                hv_path.Dispose();
                hv_dcpdir.Dispose();
                hv_Files.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_StartCamPar.Dispose();
                hv_CalibDataID.Dispose();
                hv_NG.Dispose();
                hv_Number.Dispose();


                /////////////////////////////////////////////////////

                return;
            }


            catch (HalconException HDevExpDefaultException)
            {
                ho_Image.Dispose();
                ho_ImageMirror.Dispose();
                ho_Caltab.Dispose();

                hv_path.Dispose();
                hv_dcpdir.Dispose();
                hv_Files.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_StartCamPar.Dispose();
                hv_CalibDataID.Dispose();
                hv_NumImages.Dispose();
                hv_NG.Dispose();
                hv_I.Dispose();
                hv_i.Dispose();
                hv_Exception.Dispose();


                //hv_windows.Dispose();
                //if (HDevWindowStack.IsOpen())
                //{
                HOperatorSet.ClearWindow(hv_windows);
                //}
                throw HDevExpDefaultException;
            }
        }

        public void check_img_marks(HTuple hv_windows, HTuple hv_dir, out HTuple hv_NGnum)
        {



            // Local iconic variables 

            HObject ho_Image, ho_ImageMirror = null, ho_Caltab = null;

            // Local control variables 

            HTuple hv_path = new HTuple(), hv_dcpdir = new HTuple();
            HTuple hv_Files = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_StartCamPar = new HTuple();
            HTuple hv_CalibDataID = new HTuple(), hv_NumImages = new HTuple();
            HTuple hv_NG = new HTuple(), hv_I = new HTuple(), hv_Number = new HTuple();
            HTuple hv_i = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageMirror);
            HOperatorSet.GenEmptyObj(out ho_Caltab);
            hv_NGnum = new HTuple();
            try
            {

                //HDevWindowStack.SetActive(hv_windows);
                set_display_font(hv_windows, 20, "mono", "true", "false");
                hv_path.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_path = hv_dir + "/inner";
                }
                hv_dcpdir.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_dcpdir = hv_dir + "/HC-105.cpd";
                }
                hv_Files.Dispose();
                HOperatorSet.ListFiles(hv_path, "files", out hv_Files);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(0));
                }
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);


                //初始赋值参数（焦距，kappa，像元水平尺寸，竖直尺寸，中点列坐标，中点行坐标，图像宽，图像高）
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_StartCamPar.Dispose();
                    gen_cam_par_area_scan_division(0.030, 0, 0.00000345, 0.00000345, hv_Width / 2,
                        hv_Height / 2, hv_Width, hv_Height, out hv_StartCamPar);
                }

                //****************标定工作状态的相机内参
                hv_CalibDataID.Dispose();
                HOperatorSet.CreateCalibData("calibration_object", 1, 1, out hv_CalibDataID);
                HOperatorSet.SetCalibDataCamParam(hv_CalibDataID, 0, new HTuple(), hv_StartCamPar);
                HOperatorSet.SetCalibDataCalibObject(hv_CalibDataID, 0, hv_dcpdir);
                hv_NumImages.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumImages = new HTuple(hv_Files.TupleLength()
                        );
                }
                //Note, we do not use the image from which the pose of the measurement plane can be derived
                hv_NG.Dispose();
                hv_NG = new HTuple();
                HTuple end_val20 = hv_NumImages - 1;
                HTuple step_val20 = 1;
                for (hv_I = 0; hv_I.Continue(end_val20, step_val20); hv_I = hv_I.TupleAdd(step_val20))
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_Image.Dispose();
                        HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(hv_I));
                    }
                    ho_ImageMirror.Dispose();
                    HOperatorSet.MirrorImage(ho_Image, out ho_ImageMirror, "row");
                    //if (HDevWindowStack.IsOpen())
                    //{
                    //    //dev_display (ImageMirror)
                    //}
                    //find_calib_object (ImageMirror, CalibDataID, 0, 0, I, [], [])

                    try
                    {
                        HOperatorSet.FindCalibObject(ho_ImageMirror, hv_CalibDataID, 0, 0, hv_I,
                        new HTuple(), new HTuple());
                        ho_Caltab.Dispose();
                        HOperatorSet.GetCalibDataObservContours(out ho_Caltab, hv_CalibDataID, "marks",
                            0, 0, hv_I);
                        hv_Number.Dispose();
                        HOperatorSet.CountObj(ho_Caltab, out hv_Number);
                        hv_i.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_i = hv_I + 1;
                        }
                        if ((int)(new HTuple(hv_Number.TupleNotEqual(195))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_NG = hv_NG.TupleConcat(
                                        hv_I + 1);
                                    hv_NG.Dispose();
                                    hv_NG = ExpTmpLocalVar_NG;
                                }
                            }
                            //if (HDevWindowStack.IsOpen())
                            //{
                            HOperatorSet.SetColor(hv_windows, "red");
                            //}
                            //if (HDevWindowStack.IsOpen())
                            //{
                            HOperatorSet.DispObj(ho_ImageMirror, hv_windows);
                            //}
                            //if (HDevWindowStack.IsOpen())
                            //{
                            HOperatorSet.DispObj(ho_Caltab, hv_windows);
                            //}
                            //if (HDevWindowStack.IsOpen())
                            //{
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.DispText(hv_windows, (("第 " + hv_i) + " 幅图像：") + "NG",
                                    "window", "top", "left", "red", (new HTuple("box_color")).TupleConcat(
                                    "shadow"), (new HTuple("white")).TupleConcat("false"));
                            }
                            //}
                            HOperatorSet.WaitSeconds(1);
                        }
                        else
                        {
                            //if (HDevWindowStack.IsOpen())
                            //{
                            HOperatorSet.SetColor(hv_windows, "green");
                            //}
                            //if (HDevWindowStack.IsOpen())
                            //{
                            HOperatorSet.DispObj(ho_ImageMirror, hv_windows);
                            //}
                            //if (HDevWindowStack.IsOpen())
                            //{
                            HOperatorSet.DispObj(ho_Caltab, hv_windows);
                            //}
                            //if (HDevWindowStack.IsOpen())
                            //{
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.DispText(hv_windows, (("第 " + hv_i) + " 幅图像：") + "OK",
                                    "window", "top", "left", "green", (new HTuple("box_color")).TupleConcat(
                                    "shadow"), (new HTuple("white")).TupleConcat("false"));
                            }
                            //}
                            HOperatorSet.WaitSeconds(0.05);
                        }
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.SetColor(hv_windows, "green");
                        //}
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.ClearWindow(hv_windows);
                        //}
                    }
                    catch
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            HTuple
                              ExpTmpLocalVar_NG = hv_NG.TupleConcat(
                                hv_I + 1);
                            hv_NG.Dispose();
                            hv_NG = ExpTmpLocalVar_NG;
                        }
                        //if (HDevWindowStack.IsOpen())
                        //{
                        HOperatorSet.DispObj(ho_ImageMirror, hv_windows);
                        //}
                        continue;
                    }




                    hv_NGnum.Dispose();
                    HOperatorSet.TupleSort(hv_NG, out hv_NGnum);
                }
                ho_Image.Dispose();
                ho_ImageMirror.Dispose();
                ho_Caltab.Dispose();

                hv_path.Dispose();
                hv_dcpdir.Dispose();
                hv_Files.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_StartCamPar.Dispose();
                hv_CalibDataID.Dispose();
                hv_NumImages.Dispose();
                hv_NG.Dispose();
                hv_I.Dispose();
                hv_Number.Dispose();
                hv_i.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Image.Dispose();
                ho_ImageMirror.Dispose();
                ho_Caltab.Dispose();

                hv_path.Dispose();
                hv_dcpdir.Dispose();
                hv_Files.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_StartCamPar.Dispose();
                hv_CalibDataID.Dispose();
                hv_NumImages.Dispose();
                hv_NG.Dispose();
                hv_I.Dispose();
                hv_Number.Dispose();
                hv_i.Dispose();

                throw HDevExpDefaultException;
            }
        }



        public void adjust_Gamma(HTuple hv_input_images_path, out HTuple hv_Rmse, out HTuple hv_Gamma)
        {



            // Local iconic variables 

            HObject ho_Image1, ho_Regions, ho_RegionFillUp;
            HObject ho_ConnectedRegions, ho_SelectedRegions, ho_DilationRegion;
            HObject ho_Image = null, ho_Rectangle = null;

            // Local control variables 

            HTuple hv_output_path = new HTuple(), hv_Gray = new HTuple();
            HTuple hv_FileExists = new HTuple(), hv_FileHandle = new HTuple();
            HTuple hv_ImageFiles = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple(), hv_I = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_Mean = new HTuple(), hv_Deviation = new HTuple();
            HTuple hv_Light = new HTuple(), hv_Normalize = new HTuple();
            HTuple hv_RMSE = new HTuple(), hv_interval = new HTuple();
            HTuple hv_gamma = new HTuple(), hv_start = new HTuple();
            HTuple hv_end = new HTuple(), hv_total = new HTuple();
            HTuple hv_YY = new HTuple(), hv_error = new HTuple(), hv_Prod = new HTuple();
            HTuple hv_Sum = new HTuple(), hv_temp = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_DilationRegion);
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            hv_Rmse = new HTuple();
            hv_Gamma = new HTuple();
            hv_output_path.Dispose();
            hv_output_path = new HTuple(hv_input_images_path);
            //自动创建存数据文件
            hv_Gray.Dispose();
            hv_Gray = new HTuple();

            hv_FileExists.Dispose();
            HOperatorSet.FileExists(hv_output_path, out hv_FileExists);
            if ((int)(new HTuple(hv_FileExists.TupleNotEqual(1))) != 0)
            {
                HOperatorSet.MakeDir(hv_output_path);
            }
            //*新建输出text
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_FileHandle.Dispose();
                HOperatorSet.OpenFile(hv_output_path + "/data.txt", "output", out hv_FileHandle);
            }

            //*文件夹内容
            hv_ImageFiles.Dispose();
            list_image_files(hv_input_images_path, "default", new HTuple(), out hv_ImageFiles);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Image1.Dispose();
                HOperatorSet.ReadImage(out ho_Image1, hv_ImageFiles.TupleSelect(30));
            }
            ho_Regions.Dispose();
            HOperatorSet.Threshold(ho_Image1, out ho_Regions, 83, 255);
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_Regions, out ho_RegionFillUp);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_RegionFillUp, out ho_ConnectedRegions);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                "and", 200000, 9999999999);

            ho_DilationRegion.Dispose();
            HOperatorSet.DilationCircle(ho_SelectedRegions, out ho_DilationRegion, 35);
            hv_Row1.Dispose(); hv_Column1.Dispose(); hv_Row2.Dispose(); hv_Column2.Dispose();
            HOperatorSet.SmallestRectangle1(ho_DilationRegion, out hv_Row1, out hv_Column1,
                out hv_Row2, out hv_Column2);

            for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_ImageFiles.TupleLength())) - 1); hv_I = (int)hv_I + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_I));
                }
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Row1, hv_Column1, hv_Row2,
                    hv_Column2);
                hv_Mean.Dispose(); hv_Deviation.Dispose();
                HOperatorSet.Intensity(ho_Rectangle, ho_Image, out hv_Mean, out hv_Deviation);
                //**写入txt
                HOperatorSet.FwriteString(hv_FileHandle, hv_Mean);
                HOperatorSet.FwriteString(hv_FileHandle, "\n");
                //**完成写入
                if (hv_Gray == null)
                    hv_Gray = new HTuple();
                hv_Gray[new HTuple(hv_Gray.TupleLength())] = hv_Mean;
            }
            //*关闭txt
            HOperatorSet.CloseFile(hv_FileHandle);

            //****计算gamma值
            //Gray := [0.710907,0.821125,0.983414,1.209,1.85945,2.44198,3.21782,4.26579,5.37043,6.81326,8.33399,9.91452,11.6618,13.7304,18.3525,20.9718,23.9476,27.2443,31.0464,34.7516,39.0203,43.1714,47.9314,51.9555,56.8137,61.7788,67.1308,72.3367,78.183,83.8506,89.4473,95.493,102.371,109.335,116.871,124.251,132.15,139.247,147.57,155.649,164.647,173.392,183.02,192.303,200.954,210.23,220.283,230.062,240.186,248.542,253.328]
            //归一化
            hv_Light.Dispose();
            HOperatorSet.TupleGenSequence(0, 1, 0.02, out hv_Light);
            hv_Normalize.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Normalize = hv_Gray / ((hv_Gray.TupleMax()
                    ) - (hv_Gray.TupleMin()));
            }
            //根据最小均方根误差，找最佳拟合gamma值
            hv_RMSE.Dispose();
            hv_RMSE = 1;
            hv_interval.Dispose();
            hv_interval = 0.01;
            //gamma := [0.02,5]
            hv_start.Dispose();
            hv_start = 0.02;
            hv_end.Dispose();
            hv_end = 5;
            hv_total.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_total = (((hv_end - hv_start) / hv_interval)).TupleFloor()
                    ;
            }
            HTuple end_val48 = hv_total;
            HTuple step_val48 = 1;
            for (hv_I = 1; hv_I.Continue(end_val48, step_val48); hv_I = hv_I.TupleAdd(step_val48))
            {
                //I := 99
                //    YY:=Light^(start+I*interval-interval)
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_YY.Dispose();
                    HOperatorSet.TuplePow(hv_Light, (hv_start + (hv_I * hv_interval)) - hv_interval,
                        out hv_YY);
                }
                hv_error.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_error = hv_Normalize - hv_YY;
                }
                hv_Prod.Dispose();
                HOperatorSet.TupleMult(hv_error, hv_error, out hv_Prod);
                hv_Sum.Dispose();
                HOperatorSet.TupleSum(hv_Prod, out hv_Sum);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_temp.Dispose();
                    HOperatorSet.TupleSqrt(hv_Sum / (new HTuple(hv_Prod.TupleLength())), out hv_temp);
                }
                if ((int)(new HTuple(hv_temp.TupleLess(hv_RMSE))) != 0)
                {
                    hv_RMSE.Dispose();
                    hv_RMSE = new HTuple(hv_temp);
                    hv_gamma.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_gamma = (hv_start + (hv_I * hv_interval)) - hv_interval;
                    }
                }
            }

            hv_Rmse.Dispose();
            hv_Rmse = new HTuple(hv_RMSE);
            hv_Gamma.Dispose();
            hv_Gamma = new HTuple(hv_gamma);
            ho_Image1.Dispose();
            ho_Regions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            ho_DilationRegion.Dispose();
            ho_Image.Dispose();
            ho_Rectangle.Dispose();

            hv_output_path.Dispose();
            hv_Gray.Dispose();
            hv_FileExists.Dispose();
            hv_FileHandle.Dispose();
            hv_ImageFiles.Dispose();
            hv_Row1.Dispose();
            hv_Column1.Dispose();
            hv_Row2.Dispose();
            hv_Column2.Dispose();
            hv_I.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_Mean.Dispose();
            hv_Deviation.Dispose();
            hv_Light.Dispose();
            hv_Normalize.Dispose();
            hv_RMSE.Dispose();
            hv_interval.Dispose();
            hv_gamma.Dispose();
            hv_start.Dispose();
            hv_end.Dispose();
            hv_total.Dispose();
            hv_YY.Dispose();
            hv_error.Dispose();
            hv_Prod.Dispose();
            hv_Sum.Dispose();
            hv_temp.Dispose();

            return;
        }



        public void stdscreen_path(HTuple hv_path, out HTuple hv_stdpath, out HTuple hv_screen)
        {



            // Local iconic variables 
            // Initialize local and output iconic variables 
            hv_stdpath = new HTuple();
            hv_screen = new HTuple();
            hv_stdpath.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_stdpath = hv_path + "/std";
            }
            hv_screen.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_screen = hv_path + "/screen";
            }


            return;
        }


        public void Count_Num(HTuple hv_stdpath, out HTuple hv_STDFiles, out HTuple hv_Num)
        {
            // Local iconic variables 
            // Initialize local and output iconic variables 
            hv_STDFiles = new HTuple();
            hv_Num = new HTuple();
            hv_STDFiles.Dispose();
            HOperatorSet.ListFiles(hv_stdpath, "files", out hv_STDFiles);
            hv_Num.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Num = new HTuple(hv_STDFiles.TupleLength()
                    );
            }


            return;
        }

        public void read_XMLfiles(HTuple hv_File_path, out HTupleVector hvec_Elements)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_FileName = new HTuple(), hv_FileExists = new HTuple();
            HTuple hv_TextSE = new HTuple(), hv_S = new HTuple(), hv_NameStrt = new HTuple();
            HTuple hv_NameChar = new HTuple(), hv_Name = new HTuple();
            HTuple hv_ElementTagRE = new HTuple(), hv_EndTagSE = new HTuple();
            HTuple hv_EndTagRE = new HTuple(), hv_ElementValueRE = new HTuple();
            HTuple hv_FileHandle = new HTuple(), hv_IsEof = new HTuple();
            HTuple hv_XmlElement = new HTuple(), hv_Matches = new HTuple();
            HTuple hv_Length = new HTuple(), hv_ElementTag = new HTuple();

            HTupleVector hvec_vector = new HTupleVector(1);
            // Initialize local and output iconic variables 
            hvec_Elements = new HTupleVector(2);
            //# 读取所有参数↓
            hv_FileName.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_FileName = hv_File_path + "/Para.xml";
            }
            hv_FileExists.Dispose();
            HOperatorSet.FileExists(hv_FileName, out hv_FileExists);
            if ((int)(hv_FileExists) != 0)
            {
                hv_TextSE.Dispose();
                hv_TextSE = "[^<>]+";
                hv_S.Dispose();
                hv_S = "[ \\n\\t\\r]?";
                hv_NameStrt.Dispose();
                hv_NameStrt = "[A-Za-z]";
                hv_NameChar.Dispose();
                hv_NameChar = "[A-Za-z0-9_:.-]";
                hv_Name.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Name = ((("(?:" + hv_NameStrt) + ")(?:") + hv_NameChar) + ")*";
                }
                hv_ElementTagRE.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ElementTagRE = ("<" + hv_Name) + ">";
                }
                hv_EndTagSE.Dispose();
                hv_EndTagSE = "</";
                hv_EndTagRE.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_EndTagRE = ((hv_EndTagSE + hv_Name) + hv_S) + ">+";
                }
                hv_ElementValueRE.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ElementValueRE = ">(.*?)" + hv_EndTagSE;
                }
                hv_FileHandle.Dispose();
                HOperatorSet.OpenFile(hv_FileName, "input", out hv_FileHandle);
                hv_IsEof.Dispose();
                hv_IsEof = 0;
                hvec_Elements.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_Elements = dh.Take(dh.Add(new HTupleVector(2)));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_Elements.Clear();
                }
                while ((int)(hv_IsEof.TupleNot()) != 0)
                {
                    hv_XmlElement.Dispose(); hv_IsEof.Dispose();
                    HOperatorSet.FreadLine(hv_FileHandle, out hv_XmlElement, out hv_IsEof);
                    if ((int)(hv_IsEof) != 0)
                    {
                        break;
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Matches.Dispose();
                        HOperatorSet.TupleRegexpMatch(hv_XmlElement, (((((((((("(?:(" + hv_ElementTagRE) + ")(.*)") + "(") + hv_EndTagRE) + "))|") + "(") + hv_ElementTagRE) + ")|") + "(") + hv_EndTagRE) + ")",
                            out hv_Matches);
                    }
                    hv_Length.Dispose();
                    HOperatorSet.TupleStrlen(hv_Matches, out hv_Length);
                    if ((int)(new HTuple(((hv_Length.TupleSelect(3))).TupleGreater(0))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_ElementTag.Dispose();
                            HOperatorSet.TupleRegexpMatch(hv_Matches.TupleSelect(3), hv_TextSE, out hv_ElementTag);
                        }
                        hvec_vector.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hvec_vector = dh.Take(((
                                dh.Add(new HTupleVector(1)).Insert(0, dh.Add(new HTupleVector(hv_ElementTag)))).Insert(
                                1, dh.Add(new HTupleVector(new HTuple(hv_Matches.TupleSelect(1)))))));
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hvec_Elements.Insert(new HTuple(hvec_Elements.Length), hvec_vector);
                        }
                    }
                    else if ((int)(new HTuple(((hv_Length.TupleSelect(4))).TupleGreater(
                        0))) != 0)
                    {
                    }
                    else if ((int)(new HTuple(((hv_Length.TupleSelect(0))).TupleGreater(
                        0))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_ElementTag.Dispose();
                            HOperatorSet.TupleRegexpMatch(hv_Matches.TupleSelect(0), hv_TextSE, out hv_ElementTag);
                        }
                        hvec_vector.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hvec_vector = dh.Take(((
                                dh.Add(new HTupleVector(1)).Insert(0, dh.Add(new HTupleVector(hv_ElementTag)))).Insert(
                                1, dh.Add(new HTupleVector(new HTuple(hv_Matches.TupleSelect(1)))))));
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hvec_Elements.Insert(new HTuple(hvec_Elements.Length), hvec_vector);
                        }
                    }
                }
                HOperatorSet.CloseFile(hv_FileHandle);
            }
            else
            {
            }

            hv_FileName.Dispose();
            hv_FileExists.Dispose();
            hv_TextSE.Dispose();
            hv_S.Dispose();
            hv_NameStrt.Dispose();
            hv_NameChar.Dispose();
            hv_Name.Dispose();
            hv_ElementTagRE.Dispose();
            hv_EndTagSE.Dispose();
            hv_EndTagRE.Dispose();
            hv_ElementValueRE.Dispose();
            hv_FileHandle.Dispose();
            hv_IsEof.Dispose();
            hv_XmlElement.Dispose();
            hv_Matches.Dispose();
            hv_Length.Dispose();
            hv_ElementTag.Dispose();
            hvec_vector.Dispose();

            return;
        }

        #region
        //  public void clib_Matrix_generated(HTuple hv_path, HTuple hv_widRate, HTuple hv_heightRate,
        //out HTuple hv_HomMat3DObjInCamera, out HTuple hv_HomMat3Dscreen)
        //  {



        //      // Local iconic variables 

        //      HObject ho_Image, ho_ImageMirror = null, ho_Caltab = null;
        //      HObject ho_ImageStd, ho_Image1, ho_Image2, ho_Image3, ho_Image4;
        //      HObject ho_img12, ho_RegionFillUp, ho_img13, ho_img14, ho_img15;
        //      HObject ho_img32, ho_RegionFillUp1, ho_img33, ho_img34;
        //      HObject ho_img35, ho_img55, ho_img22, ho_RegionFillUp3;
        //      HObject ho_img23, ho_img24, ho_img25, ho_img44, ho_RegionFillUp2;
        //      HObject ho_img43, ho_img45, ho_img66, ho_img124, ho_img125;
        //      HObject ho_img126, ho_img127, ho_img128, ho_img129, ho_imgout1;
        //      HObject ho_img130, ho_imgout2, ho_imgout2a, ho_SortedRegions;
        //      HObject ho_imgout3, ho_imgout4, ho_imgout5, ho_SortedRegions2;

        //      // Local control variables 

        //      HTuple hv_dcpdir = new HTuple(), hv_pathinner = new HTuple();
        //      HTuple hv_pathStd = new HTuple(), hv_pathScreen = new HTuple();
        //      HTuple hv_Files = new HTuple(), hv_Width = new HTuple();
        //      HTuple hv_Height = new HTuple(), hv_StartCamPar = new HTuple();
        //      HTuple hv_CalibDataID = new HTuple(), hv_NumImages = new HTuple();
        //      HTuple hv_I = new HTuple(), hv_Error = new HTuple(), hv_CamParam = new HTuple();
        //      HTuple hv_Files2 = new HTuple(), hv_TmpCtrl_MarkRows = new HTuple();
        //      HTuple hv_TmpCtrl_MarkColumns = new HTuple(), hv_TmpCtrl_Ind = new HTuple();
        //      HTuple hv_ObjInCameraPose = new HTuple(), hv_X = new HTuple();
        //      HTuple hv_Y = new HTuple(), hv_Z = new HTuple(), hv_XSelect = new HTuple();
        //      HTuple hv_YSelect = new HTuple(), hv_ZSelect = new HTuple();
        //      HTuple hv_num = new HTuple(), hv_Index = new HTuple();
        //      HTuple hv_HomMat2D1 = new HTuple(), hv_Covariance = new HTuple();
        //      HTuple hv_HomMat2D = new HTuple(), hv_ObjInCameraPose1 = new HTuple();
        //      HTuple hv_Quality = new HTuple(), hv_Files1 = new HTuple();
        //      HTuple hv_GrayThreshold = new HTuple(), hv_RowStart = new HTuple();
        //      HTuple hv_Rshift = new HTuple(), hv_ColStart = new HTuple();
        //      HTuple hv_Cshift = new HTuple(), hv_ScaleRate = new HTuple();
        //      HTuple hv_Rows1 = new HTuple(), hv_Cols1 = new HTuple();
        //      HTuple hv_Rows20 = new HTuple(), hv_Cols20 = new HTuple();
        //      HTuple hv_indx = new HTuple(), hv_Rows2 = new HTuple();
        //      HTuple hv_Cols2 = new HTuple(), hv_Rows3 = new HTuple();
        //      HTuple hv_Cols3 = new HTuple(), hv_Hommat2D = new HTuple();
        //      HTuple hv_RowsTmp = new HTuple(), hv_ColsTmp = new HTuple();
        //      HTuple hv_Hommat2D_A = new HTuple(), hv_Rows4 = new HTuple();
        //      HTuple hv_Cols4 = new HTuple(), hv_Cols5 = new HTuple();
        //      HTuple hv_Rows5 = new HTuple(), hv_PointNum = new HTuple();
        //      HTuple hv_Rows6 = new HTuple(), hv_Cols6 = new HTuple();
        //      HTuple hv_Newtuple = new HTuple(), hv_Pose = new HTuple();
        //      // Initialize local and output iconic variables 
        //      HOperatorSet.GenEmptyObj(out ho_Image);
        //      HOperatorSet.GenEmptyObj(out ho_ImageMirror);
        //      HOperatorSet.GenEmptyObj(out ho_Caltab);
        //      HOperatorSet.GenEmptyObj(out ho_ImageStd);
        //      HOperatorSet.GenEmptyObj(out ho_Image1);
        //      HOperatorSet.GenEmptyObj(out ho_Image2);
        //      HOperatorSet.GenEmptyObj(out ho_Image3);
        //      HOperatorSet.GenEmptyObj(out ho_Image4);
        //      HOperatorSet.GenEmptyObj(out ho_img12);
        //      HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
        //      HOperatorSet.GenEmptyObj(out ho_img13);
        //      HOperatorSet.GenEmptyObj(out ho_img14);
        //      HOperatorSet.GenEmptyObj(out ho_img15);
        //      HOperatorSet.GenEmptyObj(out ho_img32);
        //      HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
        //      HOperatorSet.GenEmptyObj(out ho_img33);
        //      HOperatorSet.GenEmptyObj(out ho_img34);
        //      HOperatorSet.GenEmptyObj(out ho_img35);
        //      HOperatorSet.GenEmptyObj(out ho_img55);
        //      HOperatorSet.GenEmptyObj(out ho_img22);
        //      HOperatorSet.GenEmptyObj(out ho_RegionFillUp3);
        //      HOperatorSet.GenEmptyObj(out ho_img23);
        //      HOperatorSet.GenEmptyObj(out ho_img24);
        //      HOperatorSet.GenEmptyObj(out ho_img25);
        //      HOperatorSet.GenEmptyObj(out ho_img44);
        //      HOperatorSet.GenEmptyObj(out ho_RegionFillUp2);
        //      HOperatorSet.GenEmptyObj(out ho_img43);
        //      HOperatorSet.GenEmptyObj(out ho_img45);
        //      HOperatorSet.GenEmptyObj(out ho_img66);
        //      HOperatorSet.GenEmptyObj(out ho_img124);
        //      HOperatorSet.GenEmptyObj(out ho_img125);
        //      HOperatorSet.GenEmptyObj(out ho_img126);
        //      HOperatorSet.GenEmptyObj(out ho_img127);
        //      HOperatorSet.GenEmptyObj(out ho_img128);
        //      HOperatorSet.GenEmptyObj(out ho_img129);
        //      HOperatorSet.GenEmptyObj(out ho_imgout1);
        //      HOperatorSet.GenEmptyObj(out ho_img130);
        //      HOperatorSet.GenEmptyObj(out ho_imgout2);
        //      HOperatorSet.GenEmptyObj(out ho_imgout2a);
        //      HOperatorSet.GenEmptyObj(out ho_SortedRegions);
        //      HOperatorSet.GenEmptyObj(out ho_imgout3);
        //      HOperatorSet.GenEmptyObj(out ho_imgout4);
        //      HOperatorSet.GenEmptyObj(out ho_imgout5);
        //      HOperatorSet.GenEmptyObj(out ho_SortedRegions2);
        //      hv_HomMat3DObjInCamera = new HTuple();
        //      hv_HomMat3Dscreen = new HTuple();

        //      //标定文件
        //      hv_dcpdir.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_dcpdir = hv_path + "/HC-105.cpd";
        //      }
        //      //内参图像路径
        //      hv_pathinner.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_pathinner = hv_path + "/inner";
        //      }
        //      //基准平面标定路径
        //      hv_pathStd.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_pathStd = hv_path + "/std";
        //      }
        //      //屏幕标定路径
        //      hv_pathScreen.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_pathScreen = hv_path + "/screen";
        //      }

        //      hv_Files.Dispose();
        //      HOperatorSet.ListFiles(hv_pathinner, "files", out hv_Files);
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          ho_Image.Dispose();
        //          HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(0));
        //      }
        //      hv_Width.Dispose(); hv_Height.Dispose();
        //      HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        //      //初始赋值参数（焦距，kappa，像元水平尺寸，竖直尺寸，中点列坐标，中点行坐标，图像宽，图像高）
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_StartCamPar.Dispose();
        //          gen_cam_par_area_scan_division(0.030, 0, 0.00000345, 0.00000345, hv_Width / 2,
        //              hv_Height / 2, hv_Width, hv_Height, out hv_StartCamPar);
        //      }

        //      //****************标定工作状态的相机内参
        //      hv_CalibDataID.Dispose();
        //      HOperatorSet.CreateCalibData("calibration_object", 1, 1, out hv_CalibDataID);
        //      HOperatorSet.SetCalibDataCamParam(hv_CalibDataID, 0, new HTuple(), hv_StartCamPar);
        //      HOperatorSet.SetCalibDataCalibObject(hv_CalibDataID, 0, hv_dcpdir);
        //      hv_NumImages.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_NumImages = new HTuple(hv_Files.TupleLength()
        //              );
        //      }
        //      //Note, we do not use the image from which the pose of the measurement plane can be derived

        //      HTuple end_val23 = hv_NumImages - 1;
        //      HTuple step_val23 = 1;
        //      for (hv_I = 0; hv_I.Continue(end_val23, step_val23); hv_I = hv_I.TupleAdd(step_val23))
        //      {
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              ho_Image.Dispose();
        //              HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(hv_I));
        //          }
        //          ho_ImageMirror.Dispose();
        //          HOperatorSet.MirrorImage(ho_Image, out ho_ImageMirror, "row");
        //          HOperatorSet.FindCalibObject(ho_ImageMirror, hv_CalibDataID, 0, 0, hv_I, new HTuple(),
        //              new HTuple());
        //          ho_Caltab.Dispose();
        //          HOperatorSet.GetCalibDataObservContours(out ho_Caltab, hv_CalibDataID, "caltab",
        //              0, 0, hv_I);
        //          if (HDevWindowStack.IsOpen())
        //          {
        //              //dev_set_color ('green')
        //          }
        //          if (HDevWindowStack.IsOpen())
        //          {
        //              //dev_display (Caltab)
        //          }
        //      }
        //      hv_Error.Dispose();
        //      HOperatorSet.CalibrateCameras(hv_CalibDataID, out hv_Error);
        //      //内参参数：CamParam
        //      hv_CamParam.Dispose();
        //      HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 0, "params", out hv_CamParam);

        //      //************************工作状态的相机内参结束
        //      //************************标准平面标定开始
        //      hv_Files2.Dispose();
        //      HOperatorSet.ListFiles(hv_pathStd, "files", out hv_Files2);
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          ho_ImageStd.Dispose();
        //          HOperatorSet.ReadImage(out ho_ImageStd, hv_Files2.TupleSelect(0));
        //      }
        //      //Calibration 01: Create calibration model for managing calibration data

        //      HOperatorSet.FindCalibObject(ho_ImageStd, hv_CalibDataID, 0, 0, hv_NumImages,
        //          "sigma", 0.5);
        //      hv_TmpCtrl_MarkRows.Dispose(); hv_TmpCtrl_MarkColumns.Dispose(); hv_TmpCtrl_Ind.Dispose(); hv_ObjInCameraPose.Dispose();
        //      HOperatorSet.GetCalibDataObservPoints(hv_CalibDataID, 0, 0, hv_NumImages, out hv_TmpCtrl_MarkRows,
        //          out hv_TmpCtrl_MarkColumns, out hv_TmpCtrl_Ind, out hv_ObjInCameraPose);

        //      hv_X.Dispose();
        //      HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj", 0, "x", out hv_X);
        //      hv_Y.Dispose();
        //      HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj", 0, "y", out hv_Y);
        //      hv_Z.Dispose();
        //      HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj", 0, "z", out hv_Z);

        //      hv_XSelect.Dispose();
        //      HOperatorSet.TupleSelect(hv_X, hv_TmpCtrl_Ind, out hv_XSelect);
        //      hv_YSelect.Dispose();
        //      HOperatorSet.TupleSelect(hv_Y, hv_TmpCtrl_Ind, out hv_YSelect);
        //      hv_ZSelect.Dispose();
        //      HOperatorSet.TupleSelect(hv_Z, hv_TmpCtrl_Ind, out hv_ZSelect);

        //      hv_num.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_num = new HTuple(hv_XSelect.TupleLength()
        //              );
        //      }
        //      HTuple end_val53 = hv_num - 1;
        //      HTuple step_val53 = 1;
        //      for (hv_Index = 0; hv_Index.Continue(end_val53, step_val53); hv_Index = hv_Index.TupleAdd(step_val53))
        //      {
        //          if (hv_XSelect == null)
        //              hv_XSelect = new HTuple();
        //          hv_XSelect[hv_Index] = (hv_XSelect.TupleSelect(hv_Index)) * 1000;
        //          if (hv_YSelect == null)
        //              hv_YSelect = new HTuple();
        //          hv_YSelect[hv_Index] = (hv_YSelect.TupleSelect(hv_Index)) * 1000;

        //      }

        //      hv_HomMat2D1.Dispose(); hv_Covariance.Dispose();
        //      HOperatorSet.VectorToProjHomMat2d(hv_TmpCtrl_MarkColumns, hv_TmpCtrl_MarkRows,
        //          hv_XSelect, hv_YSelect, "gold_standard", new HTuple(), new HTuple(), new HTuple(),
        //          new HTuple(), new HTuple(), new HTuple(), out hv_HomMat2D1, out hv_Covariance);
        //      hv_HomMat2D.Dispose();
        //      HOperatorSet.VectorToHomMat2d(hv_TmpCtrl_MarkColumns, hv_TmpCtrl_MarkRows, hv_XSelect,
        //          hv_YSelect, out hv_HomMat2D);
        //      hv_ObjInCameraPose1.Dispose(); hv_Quality.Dispose();
        //      HOperatorSet.VectorToPose(hv_XSelect, hv_YSelect, hv_ZSelect, hv_TmpCtrl_MarkRows,
        //          hv_TmpCtrl_MarkColumns, hv_CamParam, "iterative", "error", out hv_ObjInCameraPose1,
        //          out hv_Quality);
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.SetOriginPose(hv_ObjInCameraPose1, 0.0, 0.0, 0.001, out ExpTmpOutVar_0);
        //          hv_ObjInCameraPose1.Dispose();
        //          hv_ObjInCameraPose1 = ExpTmpOutVar_0;
        //      }

        //      //*************目标输出对象：单一性矩阵 HomMat3DObjInCamera
        //      hv_HomMat3DObjInCamera.Dispose();
        //      HOperatorSet.PoseToHomMat3d(hv_ObjInCameraPose1, out hv_HomMat3DObjInCamera);

        //      //***********************获得屏幕的标定矩阵开始
        //      //选用照片的光源条纹宽度及相移
        //      //1024 1024 0
        //      //64 64 32
        //      //1024 1024 512
        //      //64 64 32

        //      hv_Files1.Dispose();
        //      HOperatorSet.ListFiles(hv_pathScreen, "files", out hv_Files1);
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          ho_Image1.Dispose();
        //          HOperatorSet.ReadImage(out ho_Image1, hv_Files1.TupleSelect(0));
        //      }
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          ho_Image2.Dispose();
        //          HOperatorSet.ReadImage(out ho_Image2, hv_Files1.TupleSelect(1));
        //      }
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          ho_Image3.Dispose();
        //          HOperatorSet.ReadImage(out ho_Image3, hv_Files1.TupleSelect(2));
        //      }
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          ho_Image4.Dispose();
        //          HOperatorSet.ReadImage(out ho_Image4, hv_Files1.TupleSelect(3));
        //      }
        //      //条纹分割阈值
        //      hv_GrayThreshold.Dispose();
        //      hv_GrayThreshold = 70;
        //      //条纹有一个方向出现的相移，1024/2=512
        //      //RowStart := 768
        //      hv_RowStart.Dispose();
        //      hv_RowStart = 1024;
        //      //32是条纹宽度相对于中心点位置的距离，实际是64条纹宽度
        //      hv_Rshift.Dispose();
        //      hv_Rshift = 32;

        //      hv_ColStart.Dispose();
        //      hv_ColStart = 512;
        //      hv_Cshift.Dispose();
        //      hv_Cshift = -32;


        //      hv_ScaleRate.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_ScaleRate = (hv_widRate + hv_heightRate) / 2;
        //      }

        //      ho_img12.Dispose();
        //      HOperatorSet.Threshold(ho_Image1, out ho_img12, hv_GrayThreshold, 255);
        //      //*
        //      ho_RegionFillUp.Dispose();
        //      HOperatorSet.FillUp(ho_img12, out ho_RegionFillUp);
        //      ho_img13.Dispose();
        //      HOperatorSet.OpeningRectangle1(ho_RegionFillUp, out ho_img13, 1, 16);
        //      //*
        //      ho_img14.Dispose();
        //      HOperatorSet.ErosionRectangle1(ho_img13, out ho_img14, 3, 3);
        //      ho_img15.Dispose();
        //      HOperatorSet.Difference(ho_img13, ho_img14, out ho_img15);
        //      ho_img32.Dispose();
        //      HOperatorSet.Threshold(ho_Image3, out ho_img32, hv_GrayThreshold, 255);

        //      ho_RegionFillUp1.Dispose();
        //      HOperatorSet.FillUp(ho_img32, out ho_RegionFillUp1);
        //      ho_img33.Dispose();
        //      HOperatorSet.OpeningRectangle1(ho_RegionFillUp1, out ho_img33, 16, 1);
        //      //*
        //      ho_img34.Dispose();
        //      HOperatorSet.ErosionRectangle1(ho_img33, out ho_img34, 3, 3);
        //      ho_img35.Dispose();
        //      HOperatorSet.Difference(ho_img33, ho_img34, out ho_img35);
        //      ho_img55.Dispose();
        //      HOperatorSet.Intersection(ho_img15, ho_img35, out ho_img55);
        //      //***************************************
        //      ho_img22.Dispose();
        //      HOperatorSet.Threshold(ho_Image2, out ho_img22, hv_GrayThreshold, 255);
        //      ho_RegionFillUp3.Dispose();
        //      HOperatorSet.FillUp(ho_img22, out ho_RegionFillUp3);
        //      ho_img23.Dispose();
        //      HOperatorSet.OpeningRectangle1(ho_RegionFillUp3, out ho_img23, 1, 16);
        //      //*
        //      ho_img24.Dispose();
        //      HOperatorSet.ErosionRectangle1(ho_img23, out ho_img24, 3, 3);
        //      ho_img25.Dispose();
        //      HOperatorSet.Difference(ho_img23, ho_img24, out ho_img25);
        //      ho_img44.Dispose();
        //      HOperatorSet.Threshold(ho_Image4, out ho_img44, hv_GrayThreshold, 255);
        //      ho_RegionFillUp2.Dispose();
        //      HOperatorSet.FillUp(ho_img44, out ho_RegionFillUp2);
        //      ho_img43.Dispose();
        //      HOperatorSet.OpeningRectangle1(ho_RegionFillUp2, out ho_img43, 16, 2);
        //      //*
        //      ho_img44.Dispose();
        //      HOperatorSet.ErosionRectangle1(ho_img43, out ho_img44, 3, 3);
        //      ho_img45.Dispose();
        //      HOperatorSet.Difference(ho_img43, ho_img44, out ho_img45);
        //      ho_img66.Dispose();
        //      HOperatorSet.Intersection(ho_img25, ho_img45, out ho_img66);

        //      ho_img124.Dispose();
        //      HOperatorSet.AddImage(ho_Image2, ho_Image4, out ho_img124, 1, 0);
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          ho_img125.Dispose();
        //          HOperatorSet.Threshold(ho_img124, out ho_img125, hv_GrayThreshold + 30, 255);
        //      }
        //      ho_img126.Dispose();
        //      HOperatorSet.FillUp(ho_img125, out ho_img126);
        //      ho_img127.Dispose();
        //      HOperatorSet.OpeningRectangle1(ho_img126, out ho_img127, 256, 256);
        //      ho_img128.Dispose();
        //      HOperatorSet.ErosionRectangle1(ho_img127, out ho_img128, 128, 256);
        //      ho_img129.Dispose();
        //      HOperatorSet.OpeningRectangle1(ho_img128, out ho_img129, 1024, 512);

        //      ho_imgout1.Dispose();
        //      HOperatorSet.Intersection(ho_img55, ho_img129, out ho_imgout1);
        //      hv_Rows1.Dispose();
        //      HOperatorSet.RegionFeatures(ho_imgout1, "row1", out hv_Rows1);
        //      hv_Cols1.Dispose();
        //      HOperatorSet.RegionFeatures(ho_imgout1, "column1", out hv_Cols1);
        //      ho_img130.Dispose();
        //      HOperatorSet.DilationRectangle1(ho_imgout1, out ho_img130, 200, 200);
        //      ho_imgout2.Dispose();
        //      HOperatorSet.Intersection(ho_img66, ho_img130, out ho_imgout2);
        //      ho_imgout2a.Dispose();
        //      HOperatorSet.Connection(ho_imgout2, out ho_imgout2a);
        //      ho_SortedRegions.Dispose();
        //      HOperatorSet.SortRegion(ho_imgout2a, out ho_SortedRegions, "first_point", "true",
        //          "row");
        //      hv_Rows20.Dispose();
        //      HOperatorSet.RegionFeatures(ho_SortedRegions, "row1", out hv_Rows20);
        //      hv_Cols20.Dispose();
        //      HOperatorSet.RegionFeatures(ho_SortedRegions, "column1", out hv_Cols20);

        //      //手动排序
        //      for (hv_indx = 0; (int)hv_indx <= 3; hv_indx = (int)hv_indx + 1)
        //      {
        //          if ((int)(new HTuple(((hv_Rows20.TupleSelect(hv_indx))).TupleLess(hv_Rows1.TupleSelect(
        //              0)))) != 0)
        //          {
        //              if ((int)(new HTuple(((hv_Cols20.TupleSelect(hv_indx))).TupleLess(hv_Cols1.TupleSelect(
        //                  0)))) != 0)
        //              {
        //                  if (hv_Rows2 == null)
        //                      hv_Rows2 = new HTuple();
        //                  hv_Rows2[0] = hv_Rows20.TupleSelect(hv_indx);
        //                  if (hv_Cols2 == null)
        //                      hv_Cols2 = new HTuple();
        //                  hv_Cols2[0] = hv_Cols20.TupleSelect(hv_indx);
        //              }
        //              if ((int)(new HTuple(((hv_Cols20.TupleSelect(hv_indx))).TupleGreater(hv_Cols1.TupleSelect(
        //                  0)))) != 0)
        //              {
        //                  if (hv_Rows2 == null)
        //                      hv_Rows2 = new HTuple();
        //                  hv_Rows2[1] = hv_Rows20.TupleSelect(hv_indx);
        //                  if (hv_Cols2 == null)
        //                      hv_Cols2 = new HTuple();
        //                  hv_Cols2[1] = hv_Cols20.TupleSelect(hv_indx);
        //              }
        //          }

        //          if ((int)(new HTuple(((hv_Rows20.TupleSelect(hv_indx))).TupleGreater(hv_Rows1.TupleSelect(
        //              0)))) != 0)
        //          {
        //              if ((int)(new HTuple(((hv_Cols20.TupleSelect(hv_indx))).TupleLess(hv_Cols1.TupleSelect(
        //                  0)))) != 0)
        //              {
        //                  if (hv_Rows2 == null)
        //                      hv_Rows2 = new HTuple();
        //                  hv_Rows2[2] = hv_Rows20.TupleSelect(hv_indx);
        //                  if (hv_Cols2 == null)
        //                      hv_Cols2 = new HTuple();
        //                  hv_Cols2[2] = hv_Cols20.TupleSelect(hv_indx);
        //              }
        //              if ((int)(new HTuple(((hv_Cols20.TupleSelect(hv_indx))).TupleGreater(hv_Cols1.TupleSelect(
        //                  0)))) != 0)
        //              {
        //                  if (hv_Rows2 == null)
        //                      hv_Rows2 = new HTuple();
        //                  hv_Rows2[3] = hv_Rows20.TupleSelect(hv_indx);
        //                  if (hv_Cols2 == null)
        //                      hv_Cols2 = new HTuple();
        //                  hv_Cols2[3] = hv_Cols20.TupleSelect(hv_indx);
        //              }
        //          }
        //      }

        //      if (hv_Rows3 == null)
        //          hv_Rows3 = new HTuple();
        //      hv_Rows3[0] = hv_RowStart + hv_Rshift;
        //      if (hv_Cols3 == null)
        //          hv_Cols3 = new HTuple();
        //      hv_Cols3[0] = hv_ColStart + hv_Cshift;
        //      if (hv_Rows3 == null)
        //          hv_Rows3 = new HTuple();
        //      hv_Rows3[1] = hv_RowStart + hv_Rshift;
        //      if (hv_Cols3 == null)
        //          hv_Cols3 = new HTuple();
        //      hv_Cols3[1] = hv_ColStart - hv_Cshift;
        //      if (hv_Rows3 == null)
        //          hv_Rows3 = new HTuple();
        //      hv_Rows3[2] = hv_RowStart - hv_Rshift;
        //      if (hv_Cols3 == null)
        //          hv_Cols3 = new HTuple();
        //      hv_Cols3[2] = hv_ColStart + hv_Cshift;
        //      if (hv_Rows3 == null)
        //          hv_Rows3 = new HTuple();
        //      hv_Rows3[3] = hv_RowStart - hv_Rshift;
        //      if (hv_Cols3 == null)
        //          hv_Cols3 = new HTuple();
        //      hv_Cols3[3] = hv_ColStart - hv_Cshift;

        //      hv_Hommat2D.Dispose();
        //      HOperatorSet.VectorToHomMat2d(hv_Cols2, hv_Rows2, hv_Cols3, hv_Rows3, out hv_Hommat2D);

        //      for (hv_indx = 0; (int)hv_indx <= 3; hv_indx = (int)hv_indx + 1)
        //      {
        //          if (hv_RowsTmp == null)
        //              hv_RowsTmp = new HTuple();
        //          hv_RowsTmp[hv_indx] = (hv_Rows3.TupleSelect(hv_indx)) * 0.1736;
        //          if (hv_ColsTmp == null)
        //              hv_ColsTmp = new HTuple();
        //          hv_ColsTmp[hv_indx] = (hv_Cols3.TupleSelect(hv_indx)) * 0.1736;
        //      }

        //      hv_Hommat2D_A.Dispose();
        //      HOperatorSet.VectorToHomMat2d(hv_Cols2, hv_Rows2, hv_ColsTmp, hv_RowsTmp, out hv_Hommat2D_A);

        //      ho_imgout3.Dispose();
        //      HOperatorSet.Intersection(ho_img66, ho_img129, out ho_imgout3);
        //      ho_imgout4.Dispose();
        //      HOperatorSet.Connection(ho_imgout3, out ho_imgout4);
        //      ho_imgout5.Dispose();
        //      HOperatorSet.SelectShape(ho_imgout4, out ho_imgout5, "area", "and", 1, 6);
        //      ho_SortedRegions2.Dispose();
        //      HOperatorSet.SortRegion(ho_imgout5, out ho_SortedRegions2, "first_point", "true",
        //          "row");
        //      hv_Rows4.Dispose();
        //      HOperatorSet.RegionFeatures(ho_SortedRegions2, "row", out hv_Rows4);
        //      hv_Cols4.Dispose();
        //      HOperatorSet.RegionFeatures(ho_SortedRegions2, "column", out hv_Cols4);

        //      hv_Cols5.Dispose(); hv_Rows5.Dispose();
        //      HOperatorSet.AffineTransPoint2d(hv_Hommat2D, hv_Cols4, hv_Rows4, out hv_Cols5,
        //          out hv_Rows5);
        //      hv_PointNum.Dispose();
        //      HOperatorSet.CountObj(ho_SortedRegions2, out hv_PointNum);
        //      HTuple end_val192 = hv_PointNum - 1;
        //      HTuple step_val192 = 1;
        //      for (hv_indx = 0; hv_indx.Continue(end_val192, step_val192); hv_indx = hv_indx.TupleAdd(step_val192))
        //      {
        //          if (hv_Rows6 == null)
        //              hv_Rows6 = new HTuple();
        //          hv_Rows6[hv_indx] = ((((hv_Rows5.TupleSelect(hv_indx)) / 64)).TupleRound()) * 64;
        //          if (hv_Rows6 == null)
        //              hv_Rows6 = new HTuple();
        //          hv_Rows6[hv_indx] = ((hv_Rows6.TupleSelect(hv_indx)) - 0.5) * hv_ScaleRate;
        //          if (hv_Cols6 == null)
        //              hv_Cols6 = new HTuple();
        //          hv_Cols6[hv_indx] = ((((hv_Cols5.TupleSelect(hv_indx)) / 64)).TupleRound()) * 64;
        //          if (hv_Cols6 == null)
        //              hv_Cols6 = new HTuple();
        //          hv_Cols6[hv_indx] = ((hv_Cols6.TupleSelect(hv_indx)) - 0.5) * hv_ScaleRate;
        //      }
        //      hv_Newtuple.Dispose();
        //      HOperatorSet.TupleGenConst(hv_PointNum, 0, out hv_Newtuple);
        //      hv_Pose.Dispose(); hv_Quality.Dispose();
        //      HOperatorSet.VectorToPose(hv_Cols6, hv_Rows6, hv_Newtuple, hv_Rows4, hv_Cols4,
        //          hv_CamParam, "iterative", "error", out hv_Pose, out hv_Quality);

        //      //*************目标输出对象：单一性矩阵 HomMat3Dscreen
        //      hv_HomMat3Dscreen.Dispose();
        //      HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_HomMat3Dscreen);
        //      ho_Image.Dispose();
        //      ho_ImageMirror.Dispose();
        //      ho_Caltab.Dispose();
        //      ho_ImageStd.Dispose();
        //      ho_Image1.Dispose();
        //      ho_Image2.Dispose();
        //      ho_Image3.Dispose();
        //      ho_Image4.Dispose();
        //      ho_img12.Dispose();
        //      ho_RegionFillUp.Dispose();
        //      ho_img13.Dispose();
        //      ho_img14.Dispose();
        //      ho_img15.Dispose();
        //      ho_img32.Dispose();
        //      ho_RegionFillUp1.Dispose();
        //      ho_img33.Dispose();
        //      ho_img34.Dispose();
        //      ho_img35.Dispose();
        //      ho_img55.Dispose();
        //      ho_img22.Dispose();
        //      ho_RegionFillUp3.Dispose();
        //      ho_img23.Dispose();
        //      ho_img24.Dispose();
        //      ho_img25.Dispose();
        //      ho_img44.Dispose();
        //      ho_RegionFillUp2.Dispose();
        //      ho_img43.Dispose();
        //      ho_img45.Dispose();
        //      ho_img66.Dispose();
        //      ho_img124.Dispose();
        //      ho_img125.Dispose();
        //      ho_img126.Dispose();
        //      ho_img127.Dispose();
        //      ho_img128.Dispose();
        //      ho_img129.Dispose();
        //      ho_imgout1.Dispose();
        //      ho_img130.Dispose();
        //      ho_imgout2.Dispose();
        //      ho_imgout2a.Dispose();
        //      ho_SortedRegions.Dispose();
        //      ho_imgout3.Dispose();
        //      ho_imgout4.Dispose();
        //      ho_imgout5.Dispose();
        //      ho_SortedRegions2.Dispose();

        //      hv_dcpdir.Dispose();
        //      hv_pathinner.Dispose();
        //      hv_pathStd.Dispose();
        //      hv_pathScreen.Dispose();
        //      hv_Files.Dispose();
        //      hv_Width.Dispose();
        //      hv_Height.Dispose();
        //      hv_StartCamPar.Dispose();
        //      hv_CalibDataID.Dispose();
        //      hv_NumImages.Dispose();
        //      hv_I.Dispose();
        //      hv_Error.Dispose();
        //      hv_CamParam.Dispose();
        //      hv_Files2.Dispose();
        //      hv_TmpCtrl_MarkRows.Dispose();
        //      hv_TmpCtrl_MarkColumns.Dispose();
        //      hv_TmpCtrl_Ind.Dispose();
        //      hv_ObjInCameraPose.Dispose();
        //      hv_X.Dispose();
        //      hv_Y.Dispose();
        //      hv_Z.Dispose();
        //      hv_XSelect.Dispose();
        //      hv_YSelect.Dispose();
        //      hv_ZSelect.Dispose();
        //      hv_num.Dispose();
        //      hv_Index.Dispose();
        //      hv_HomMat2D1.Dispose();
        //      hv_Covariance.Dispose();
        //      hv_HomMat2D.Dispose();
        //      hv_ObjInCameraPose1.Dispose();
        //      hv_Quality.Dispose();
        //      hv_Files1.Dispose();
        //      hv_GrayThreshold.Dispose();
        //      hv_RowStart.Dispose();
        //      hv_Rshift.Dispose();
        //      hv_ColStart.Dispose();
        //      hv_Cshift.Dispose();
        //      hv_ScaleRate.Dispose();
        //      hv_Rows1.Dispose();
        //      hv_Cols1.Dispose();
        //      hv_Rows20.Dispose();
        //      hv_Cols20.Dispose();
        //      hv_indx.Dispose();
        //      hv_Rows2.Dispose();
        //      hv_Cols2.Dispose();
        //      hv_Rows3.Dispose();
        //      hv_Cols3.Dispose();
        //      hv_Hommat2D.Dispose();
        //      hv_RowsTmp.Dispose();
        //      hv_ColsTmp.Dispose();
        //      hv_Hommat2D_A.Dispose();
        //      hv_Rows4.Dispose();
        //      hv_Cols4.Dispose();
        //      hv_Cols5.Dispose();
        //      hv_Rows5.Dispose();
        //      hv_PointNum.Dispose();
        //      hv_Rows6.Dispose();
        //      hv_Cols6.Dispose();
        //      hv_Newtuple.Dispose();
        //      hv_Pose.Dispose();

        //      return;
        //  }

        #endregion


        //  public void save_XMLfiles(HTuple hv_FileName, HTuple hv_HomMat3DObjInCamera, HTuple hv_HomMat3Dscreen,
        //HTupleVector hvec_Elements, HTuple hv_StdMaxs, HTuple hv_StdMins,
        //HTuple hv_ProductMaxs, HTuple hv_ProductMins, HTuple hv_widRate, HTuple hv_heightRate)
        //  {



        //      // Local iconic variables 

        //      // Local control variables 

        //      HTuple hv_Filepath = new HTuple(), hv_stdparam = new HTuple();
        //      HTuple hv_Index1 = new HTuple(), hv_screenparam = new HTuple();
        //      HTuple hv_Names = new HTuple(), hv_i = new HTuple(), hv_value = new HTuple();
        //      HTuple hv_StdData = new HTuple(), hv_index = new HTuple();
        //      HTuple hv_valueCount = new HTuple(), hv_foundValue = new HTuple();
        //      HTuple hv_ScreenData = new HTuple(), hv_ThreshStdMax = new HTuple();
        //      HTuple hv_ThreshStdMin = new HTuple(), hv_ThreshProductMax = new HTuple();
        //      HTuple hv_ThreshProductMin = new HTuple(), hv_ScrPixSize = new HTuple();
        //      HTuple hv_writeFileHandle = new HTuple();

        //      HTupleVector hvec_ElementsOut = new HTupleVector(2);
        //      HTupleVector hvec_ElementsOUT = new HTupleVector(2);
        //      // Initialize local and output iconic variables 
        //      hvec_ElementsOut.Dispose();
        //      hvec_ElementsOut = new HTupleVector(hvec_Elements);
        //      hv_Filepath.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_Filepath = hv_FileName + "/Para.xml";
        //      }
        //      //矩阵写入Xml
        //      hv_stdparam.Dispose();
        //      hv_stdparam = new HTuple();
        //      for (hv_Index1 = 0; (int)hv_Index1 <= (int)((new HTuple(hv_HomMat3DObjInCamera.TupleLength()
        //          )) - 1); hv_Index1 = (int)hv_Index1 + 1)
        //      {
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              HTuple ExpTmpOutVar_0;
        //              HOperatorSet.TupleConcat(hv_stdparam, hv_HomMat3DObjInCamera.TupleSelect(hv_Index1),
        //                  out ExpTmpOutVar_0);
        //              hv_stdparam.Dispose();
        //              hv_stdparam = ExpTmpOutVar_0;
        //          }
        //      }
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.TupleConcat(hv_stdparam, 0, out ExpTmpOutVar_0);
        //          hv_stdparam.Dispose();
        //          hv_stdparam = ExpTmpOutVar_0;
        //      }
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.TupleConcat(hv_stdparam, 0, out ExpTmpOutVar_0);
        //          hv_stdparam.Dispose();
        //          hv_stdparam = ExpTmpOutVar_0;
        //      }
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.TupleConcat(hv_stdparam, 0, out ExpTmpOutVar_0);
        //          hv_stdparam.Dispose();
        //          hv_stdparam = ExpTmpOutVar_0;
        //      }
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.TupleConcat(hv_stdparam, 1, out ExpTmpOutVar_0);
        //          hv_stdparam.Dispose();
        //          hv_stdparam = ExpTmpOutVar_0;
        //      }
        //      hv_screenparam.Dispose();
        //      hv_screenparam = new HTuple();
        //      for (hv_Index1 = 0; (int)hv_Index1 <= (int)((new HTuple(hv_HomMat3Dscreen.TupleLength()
        //          )) - 1); hv_Index1 = (int)hv_Index1 + 1)
        //      {
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              HTuple ExpTmpOutVar_0;
        //              HOperatorSet.TupleConcat(hv_screenparam, hv_HomMat3Dscreen.TupleSelect(hv_Index1),
        //                  out ExpTmpOutVar_0);
        //              hv_screenparam.Dispose();
        //              hv_screenparam = ExpTmpOutVar_0;
        //          }
        //      }
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.TupleConcat(hv_screenparam, 0, out ExpTmpOutVar_0);
        //          hv_screenparam.Dispose();
        //          hv_screenparam = ExpTmpOutVar_0;
        //      }
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.TupleConcat(hv_screenparam, 0, out ExpTmpOutVar_0);
        //          hv_screenparam.Dispose();
        //          hv_screenparam = ExpTmpOutVar_0;
        //      }
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.TupleConcat(hv_screenparam, 0, out ExpTmpOutVar_0);
        //          hv_screenparam.Dispose();
        //          hv_screenparam = ExpTmpOutVar_0;
        //      }
        //      {
        //          HTuple ExpTmpOutVar_0;
        //          HOperatorSet.TupleConcat(hv_screenparam, 1, out ExpTmpOutVar_0);
        //          hv_screenparam.Dispose();
        //          hv_screenparam = ExpTmpOutVar_0;
        //      }

        //      //# 根据参数名查找数值↓
        //      hv_Names.Dispose();
        //      hv_Names = new HTuple();
        //      HTuple end_val22 = new HTuple(hvec_ElementsOut.Length) - 1;
        //      HTuple step_val22 = 1;
        //      for (hv_i = 0; hv_i.Continue(end_val22, step_val22); hv_i = hv_i.TupleAdd(step_val22))
        //      {
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              {
        //                  HTuple
        //                    ExpTmpLocalVar_Names = hv_Names.TupleConcat(
        //                      hvec_ElementsOut[hv_i][0].T);
        //                  hv_Names.Dispose();
        //                  hv_Names = ExpTmpLocalVar_Names;
        //              }
        //          }
        //      }
        //      hv_value.Dispose();
        //      hv_value = new HTuple();

        //      //std
        //      hv_StdData.Dispose();
        //      hv_StdData = "HomStdData";
        //      hv_index.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_index = hv_Names.TupleFind(
        //              hv_StdData);
        //      }
        //      if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
        //      {
        //          hv_value.Dispose();
        //          hv_value = -1;

        //          hv_Filepath.Dispose();
        //          hv_stdparam.Dispose();
        //          hv_Index1.Dispose();
        //          hv_screenparam.Dispose();
        //          hv_Names.Dispose();
        //          hv_i.Dispose();
        //          hv_value.Dispose();
        //          hv_StdData.Dispose();
        //          hv_index.Dispose();
        //          hv_valueCount.Dispose();
        //          hv_foundValue.Dispose();
        //          hv_ScreenData.Dispose();
        //          hv_ThreshStdMax.Dispose();
        //          hv_ThreshStdMin.Dispose();
        //          hv_ThreshProductMax.Dispose();
        //          hv_ThreshProductMin.Dispose();
        //          hv_ScrPixSize.Dispose();
        //          hv_writeFileHandle.Dispose();
        //          hvec_ElementsOut.Dispose();
        //          hvec_ElementsOUT.Dispose();

        //          return;
        //      }
        //      for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
        //          )) - 1); hv_valueCount = (int)hv_valueCount + 1)
        //      {
        //          hv_foundValue.Dispose();
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
        //                  hv_valueCount)][1].T);
        //          }
        //          if ((int)(hv_foundValue.TupleIsNumber()) != 0)
        //          {
        //              //tuple_number (foundValue1Out, foundValue1Out)
        //              using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //              {
        //                  hvec_ElementsOut[hv_index.TupleSelect(
        //                      hv_valueCount)][1] = dh.Add(new HTupleVector(hv_stdparam.TupleSelect(
        //                      hv_valueCount)));
        //              }
        //          }
        //          //value1 := [value1, foundValue1Out]
        //      }

        //      //screen
        //      hv_ScreenData.Dispose();
        //      hv_ScreenData = "HomScreenData";
        //      hv_index.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_index = hv_Names.TupleFind(
        //              hv_ScreenData);
        //      }
        //      if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
        //      {
        //          hv_value.Dispose();
        //          hv_value = -1;

        //          hv_Filepath.Dispose();
        //          hv_stdparam.Dispose();
        //          hv_Index1.Dispose();
        //          hv_screenparam.Dispose();
        //          hv_Names.Dispose();
        //          hv_i.Dispose();
        //          hv_value.Dispose();
        //          hv_StdData.Dispose();
        //          hv_index.Dispose();
        //          hv_valueCount.Dispose();
        //          hv_foundValue.Dispose();
        //          hv_ScreenData.Dispose();
        //          hv_ThreshStdMax.Dispose();
        //          hv_ThreshStdMin.Dispose();
        //          hv_ThreshProductMax.Dispose();
        //          hv_ThreshProductMin.Dispose();
        //          hv_ScrPixSize.Dispose();
        //          hv_writeFileHandle.Dispose();
        //          hvec_ElementsOut.Dispose();
        //          hvec_ElementsOUT.Dispose();

        //          return;
        //      }
        //      for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
        //          )) - 1); hv_valueCount = (int)hv_valueCount + 1)
        //      {
        //          hv_foundValue.Dispose();
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
        //                  hv_valueCount)][1].T);
        //          }
        //          if ((int)(hv_foundValue.TupleIsNumber()) != 0)
        //          {
        //              //tuple_number (foundValue, foundValue)
        //              using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //              {
        //                  hvec_ElementsOut[hv_index.TupleSelect(
        //                      hv_valueCount)][1] = dh.Add(new HTupleVector(hv_screenparam.TupleSelect(
        //                      hv_valueCount)));
        //              }
        //          }
        //      }
        //      //其他参数
        //      //'ThreshStdMaxs'
        //      hv_ThreshStdMax.Dispose();
        //      hv_ThreshStdMax = "ThreshStdMaxs";
        //      hv_index.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_index = hv_Names.TupleFind(
        //              hv_ThreshStdMax);
        //      }
        //      if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
        //      {
        //          hv_value.Dispose();
        //          hv_value = -1;

        //          hv_Filepath.Dispose();
        //          hv_stdparam.Dispose();
        //          hv_Index1.Dispose();
        //          hv_screenparam.Dispose();
        //          hv_Names.Dispose();
        //          hv_i.Dispose();
        //          hv_value.Dispose();
        //          hv_StdData.Dispose();
        //          hv_index.Dispose();
        //          hv_valueCount.Dispose();
        //          hv_foundValue.Dispose();
        //          hv_ScreenData.Dispose();
        //          hv_ThreshStdMax.Dispose();
        //          hv_ThreshStdMin.Dispose();
        //          hv_ThreshProductMax.Dispose();
        //          hv_ThreshProductMin.Dispose();
        //          hv_ScrPixSize.Dispose();
        //          hv_writeFileHandle.Dispose();
        //          hvec_ElementsOut.Dispose();
        //          hvec_ElementsOUT.Dispose();

        //          return;
        //      }
        //      for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
        //          )) - 1); hv_valueCount = (int)hv_valueCount + 1)
        //      {
        //          hv_foundValue.Dispose();
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
        //                  hv_valueCount)][1].T);
        //          }
        //          if ((int)(hv_foundValue.TupleIsNumber()) != 0)
        //          {
        //              //tuple_number (foundValue, foundValue)
        //              using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //              {
        //                  hvec_ElementsOut[hv_index.TupleSelect(
        //                      hv_valueCount)][1] = dh.Add(new HTupleVector(hv_StdMaxs.TupleSelect(hv_valueCount)));
        //              }
        //          }
        //      }

        //      //参数ThreshStdMins
        //      hv_ThreshStdMin.Dispose();
        //      hv_ThreshStdMin = "ThreshStdMins";
        //      hv_index.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_index = hv_Names.TupleFind(
        //              hv_ThreshStdMin);
        //      }
        //      if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
        //      {
        //          hv_value.Dispose();
        //          hv_value = -1;

        //          hv_Filepath.Dispose();
        //          hv_stdparam.Dispose();
        //          hv_Index1.Dispose();
        //          hv_screenparam.Dispose();
        //          hv_Names.Dispose();
        //          hv_i.Dispose();
        //          hv_value.Dispose();
        //          hv_StdData.Dispose();
        //          hv_index.Dispose();
        //          hv_valueCount.Dispose();
        //          hv_foundValue.Dispose();
        //          hv_ScreenData.Dispose();
        //          hv_ThreshStdMax.Dispose();
        //          hv_ThreshStdMin.Dispose();
        //          hv_ThreshProductMax.Dispose();
        //          hv_ThreshProductMin.Dispose();
        //          hv_ScrPixSize.Dispose();
        //          hv_writeFileHandle.Dispose();
        //          hvec_ElementsOut.Dispose();
        //          hvec_ElementsOUT.Dispose();

        //          return;
        //      }
        //      for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
        //          )) - 1); hv_valueCount = (int)hv_valueCount + 1)
        //      {
        //          hv_foundValue.Dispose();
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
        //                  hv_valueCount)][1].T);
        //          }
        //          if ((int)(hv_foundValue.TupleIsNumber()) != 0)
        //          {
        //              //tuple_number (foundValue, foundValue)
        //              using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //              {
        //                  hvec_ElementsOut[hv_index.TupleSelect(
        //                      hv_valueCount)][1] = dh.Add(new HTupleVector(hv_StdMins.TupleSelect(hv_valueCount)));
        //              }
        //          }
        //      }

        //      //ThreshProductMaxs
        //      hv_ThreshProductMax.Dispose();
        //      hv_ThreshProductMax = "ThreshProductMaxs";
        //      hv_index.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_index = hv_Names.TupleFind(
        //              hv_ThreshProductMax);
        //      }
        //      if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
        //      {
        //          hv_value.Dispose();
        //          hv_value = -1;

        //          hv_Filepath.Dispose();
        //          hv_stdparam.Dispose();
        //          hv_Index1.Dispose();
        //          hv_screenparam.Dispose();
        //          hv_Names.Dispose();
        //          hv_i.Dispose();
        //          hv_value.Dispose();
        //          hv_StdData.Dispose();
        //          hv_index.Dispose();
        //          hv_valueCount.Dispose();
        //          hv_foundValue.Dispose();
        //          hv_ScreenData.Dispose();
        //          hv_ThreshStdMax.Dispose();
        //          hv_ThreshStdMin.Dispose();
        //          hv_ThreshProductMax.Dispose();
        //          hv_ThreshProductMin.Dispose();
        //          hv_ScrPixSize.Dispose();
        //          hv_writeFileHandle.Dispose();
        //          hvec_ElementsOut.Dispose();
        //          hvec_ElementsOUT.Dispose();

        //          return;
        //      }
        //      for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
        //          )) - 1); hv_valueCount = (int)hv_valueCount + 1)
        //      {
        //          hv_foundValue.Dispose();
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
        //                  hv_valueCount)][1].T);
        //          }
        //          if ((int)(hv_foundValue.TupleIsNumber()) != 0)
        //          {
        //              //tuple_number (foundValue, foundValue)
        //              using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //              {
        //                  hvec_ElementsOut[hv_index.TupleSelect(
        //                      hv_valueCount)][1] = dh.Add(new HTupleVector(hv_ProductMaxs.TupleSelect(
        //                      hv_valueCount)));
        //              }
        //          }
        //      }

        //      //ThreshProductMins
        //      hv_ThreshProductMin.Dispose();
        //      hv_ThreshProductMin = "ThreshProductMins";
        //      hv_index.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_index = hv_Names.TupleFind(
        //              hv_ThreshProductMin);
        //      }
        //      if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
        //      {
        //          hv_value.Dispose();
        //          hv_value = -1;

        //          hv_Filepath.Dispose();
        //          hv_stdparam.Dispose();
        //          hv_Index1.Dispose();
        //          hv_screenparam.Dispose();
        //          hv_Names.Dispose();
        //          hv_i.Dispose();
        //          hv_value.Dispose();
        //          hv_StdData.Dispose();
        //          hv_index.Dispose();
        //          hv_valueCount.Dispose();
        //          hv_foundValue.Dispose();
        //          hv_ScreenData.Dispose();
        //          hv_ThreshStdMax.Dispose();
        //          hv_ThreshStdMin.Dispose();
        //          hv_ThreshProductMax.Dispose();
        //          hv_ThreshProductMin.Dispose();
        //          hv_ScrPixSize.Dispose();
        //          hv_writeFileHandle.Dispose();
        //          hvec_ElementsOut.Dispose();
        //          hvec_ElementsOUT.Dispose();

        //          return;
        //      }
        //      for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
        //          )) - 1); hv_valueCount = (int)hv_valueCount + 1)
        //      {
        //          hv_foundValue.Dispose();
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
        //                  hv_valueCount)][1].T);
        //          }
        //          if ((int)(hv_foundValue.TupleIsNumber()) != 0)
        //          {
        //              //tuple_number (foundValue, foundValue)
        //              using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //              {
        //                  hvec_ElementsOut[hv_index.TupleSelect(
        //                      hv_valueCount)][1] = dh.Add(new HTupleVector(hv_ProductMins.TupleSelect(
        //                      hv_valueCount)));
        //              }
        //          }
        //      }

        //      hv_ScrPixSize.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_ScrPixSize = (hv_widRate + hv_heightRate) / 2;
        //      }
        //      hv_ThreshProductMin.Dispose();
        //      hv_ThreshProductMin = "ScreenPixSize";
        //      hv_index.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_index = hv_Names.TupleFind(
        //              hv_ThreshProductMin);
        //      }

        //      hv_foundValue.Dispose();
        //      using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //      {
        //          hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
        //              0)][1].T);
        //      }
        //      if ((int)(hv_foundValue.TupleIsNumber()) != 0)
        //      {
        //          //tuple_number (foundValue, foundValue)
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              hvec_ElementsOut[hv_index.TupleSelect(
        //                  0)][1] = dh.Add(new HTupleVector(hv_ScrPixSize));
        //          }
        //      }

        //      //# 写入XML文件

        //      hv_writeFileHandle.Dispose();
        //      HOperatorSet.OpenFile(hv_Filepath, "output", out hv_writeFileHandle);
        //      HOperatorSet.FwriteString(hv_writeFileHandle, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\n");
        //      HOperatorSet.FwriteString(hv_writeFileHandle, "<XD_PhaseDeflectionPara xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\n");
        //      HTuple end_val133 = new HTuple(hvec_ElementsOut.Length) - 1;
        //      HTuple step_val133 = 1;
        //      for (hv_valueCount = 0; hv_valueCount.Continue(end_val133, step_val133); hv_valueCount = hv_valueCount.TupleAdd(step_val133))
        //      {
        //          using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //          {
        //              HOperatorSet.FwriteString(hv_writeFileHandle, ((((("\t<" + hvec_ElementsOut[hv_valueCount][0].T) + ">") + hvec_ElementsOut[hv_valueCount][1].T) + "</") + hvec_ElementsOut[hv_valueCount][0].T) + ">\n");
        //          }
        //      }
        //      HOperatorSet.FwriteString(hv_writeFileHandle, "</XD_PhaseDeflectionPara>\n");
        //      hvec_ElementsOUT.Dispose();
        //      hvec_ElementsOUT = new HTupleVector(hvec_ElementsOut);
        //      HOperatorSet.CloseFile(hv_writeFileHandle);

        //      hv_Filepath.Dispose();
        //      hv_stdparam.Dispose();
        //      hv_Index1.Dispose();
        //      hv_screenparam.Dispose();
        //      hv_Names.Dispose();
        //      hv_i.Dispose();
        //      hv_value.Dispose();
        //      hv_StdData.Dispose();
        //      hv_index.Dispose();
        //      hv_valueCount.Dispose();
        //      hv_foundValue.Dispose();
        //      hv_ScreenData.Dispose();
        //      hv_ThreshStdMax.Dispose();
        //      hv_ThreshStdMin.Dispose();
        //      hv_ThreshProductMax.Dispose();
        //      hv_ThreshProductMin.Dispose();
        //      hv_ScrPixSize.Dispose();
        //      hv_writeFileHandle.Dispose();
        //      hvec_ElementsOut.Dispose();
        //      hvec_ElementsOUT.Dispose();

        //      return;
        //  }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///代码更新
        public void clib_Matrix_generated(HTuple hv_path, HTuple hv_widRate, HTuple hv_heightRate,
    out HTuple hv_HomMat3DObjInCamera, out HTuple hv_HomMat3Dscreen, out HTuple hv_HomTemp,
    out HTuple hv_pixelSizeX, out HTuple hv_pixelSizeY)
        {



            // Local iconic variables 

            HObject ho_Image, ho_ImageMirror = null, ho_Caltab = null;
            HObject ho_ImageStd, ho_Image1, ho_Image2, ho_Image3, ho_Image4;
            HObject ho_img12, ho_RegionFillUp, ho_img13, ho_img14, ho_img15;
            HObject ho_img32, ho_RegionFillUp1, ho_img33, ho_img34;
            HObject ho_img35, ho_img55, ho_img22, ho_RegionFillUp3;
            HObject ho_img23, ho_img24, ho_img25, ho_img44, ho_RegionFillUp2;
            HObject ho_img43, ho_img45, ho_img66, ho_img124, ho_img125;
            HObject ho_img126, ho_img127, ho_img128, ho_img129, ho_imgout1;
            HObject ho_img130, ho_imgout2, ho_imgout2a, ho_SortedRegions;
            HObject ho_imgout3, ho_imgout4, ho_imgout5, ho_SortedRegions2;

            // Local control variables 

            HTuple hv_NumRows = new HTuple(), hv_MarksPerRow = new HTuple();
            HTuple hv_Diameter = new HTuple(), hv_FinderRow = new HTuple();
            HTuple hv_FinderColumn = new HTuple(), hv_dcpdir = new HTuple();
            HTuple hv_pathinner = new HTuple(), hv_pathStd = new HTuple();
            HTuple hv_pathScreen = new HTuple(), hv_Files = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_StartCamPar = new HTuple(), hv_CalibDataID = new HTuple();
            HTuple hv_NumImages = new HTuple(), hv_I = new HTuple();
            HTuple hv_Error = new HTuple(), hv_CamParam = new HTuple();
            HTuple hv_Files2 = new HTuple(), hv_TmpCtrl_MarkRows = new HTuple();
            HTuple hv_TmpCtrl_MarkColumns = new HTuple(), hv_TmpCtrl_Ind = new HTuple();
            HTuple hv_ObjInCameraPose = new HTuple(), hv_X = new HTuple();
            HTuple hv_Y = new HTuple(), hv_Z = new HTuple(), hv_XSelect = new HTuple();
            HTuple hv_YSelect = new HTuple(), hv_ZSelect = new HTuple();
            HTuple hv_num = new HTuple(), hv_XSelect1 = new HTuple();
            HTuple hv_YSelect1 = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Covariance = new HTuple(), hv_ObjInCameraPose1 = new HTuple();
            HTuple hv_Quality = new HTuple(), hv_numMarks = new HTuple();
            HTuple hv_marksNum = new HTuple(), hv_patternIndex = new HTuple();
            HTuple hv_index = new HTuple(), hv_centerNum = new HTuple();
            HTuple hv_marksPt3dX = new HTuple(), hv_marksPt3dY = new HTuple();
            HTuple hv_marksPt3dZ = new HTuple(), hv_index1 = new HTuple();
            HTuple hv_centerPt3dX = new HTuple(), hv_centerPt3dY = new HTuple();
            HTuple hv_centerPt3dZ = new HTuple(), hv_marksPt2dR = new HTuple();
            HTuple hv_marksPt2dC = new HTuple(), hv_centerC = new HTuple();
            HTuple hv_centerR = new HTuple(), hv_HomMat2D3 = new HTuple();
            HTuple hv_zValue = new HTuple(), hv_len0_1 = new HTuple();
            HTuple hv_len1_3 = new HTuple(), hv_len1 = new HTuple();
            HTuple hv_len7 = new HTuple(), hv_ratio = new HTuple();
            HTuple hv_len5 = new HTuple(), hv_len6 = new HTuple();
            HTuple hv_inputPtInWorldX = new HTuple(), hv_inputPtInWorldY = new HTuple();
            HTuple hv_pixel2_3 = new HTuple(), hv_pixel0_2 = new HTuple();
            HTuple hv_Files1 = new HTuple(), hv_GrayThreshold = new HTuple();
            HTuple hv_RowStart = new HTuple(), hv_Rshift = new HTuple();
            HTuple hv_ColStart = new HTuple(), hv_Cshift = new HTuple();
            HTuple hv_ScaleRate = new HTuple(), hv_Rows1 = new HTuple();
            HTuple hv_Cols1 = new HTuple(), hv_Rows20 = new HTuple();
            HTuple hv_Cols20 = new HTuple(), hv_indx = new HTuple();
            HTuple hv_Rows2 = new HTuple(), hv_Cols2 = new HTuple();
            HTuple hv_Rows3 = new HTuple(), hv_Cols3 = new HTuple();
            HTuple hv_Hommat2D = new HTuple(), hv_RowsTmp = new HTuple();
            HTuple hv_ColsTmp = new HTuple(), hv_Hommat2D_A = new HTuple();
            HTuple hv_Rows4 = new HTuple(), hv_Cols4 = new HTuple();
            HTuple hv_Cols5 = new HTuple(), hv_Rows5 = new HTuple();
            HTuple hv_PointNum = new HTuple(), hv_Rows6 = new HTuple();
            HTuple hv_Cols6 = new HTuple(), hv_Newtuple = new HTuple();
            HTuple hv_Pose = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageMirror);
            HOperatorSet.GenEmptyObj(out ho_Caltab);
            HOperatorSet.GenEmptyObj(out ho_ImageStd);
            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_Image2);
            HOperatorSet.GenEmptyObj(out ho_Image3);
            HOperatorSet.GenEmptyObj(out ho_Image4);
            HOperatorSet.GenEmptyObj(out ho_img12);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_img13);
            HOperatorSet.GenEmptyObj(out ho_img14);
            HOperatorSet.GenEmptyObj(out ho_img15);
            HOperatorSet.GenEmptyObj(out ho_img32);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
            HOperatorSet.GenEmptyObj(out ho_img33);
            HOperatorSet.GenEmptyObj(out ho_img34);
            HOperatorSet.GenEmptyObj(out ho_img35);
            HOperatorSet.GenEmptyObj(out ho_img55);
            HOperatorSet.GenEmptyObj(out ho_img22);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp3);
            HOperatorSet.GenEmptyObj(out ho_img23);
            HOperatorSet.GenEmptyObj(out ho_img24);
            HOperatorSet.GenEmptyObj(out ho_img25);
            HOperatorSet.GenEmptyObj(out ho_img44);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp2);
            HOperatorSet.GenEmptyObj(out ho_img43);
            HOperatorSet.GenEmptyObj(out ho_img45);
            HOperatorSet.GenEmptyObj(out ho_img66);
            HOperatorSet.GenEmptyObj(out ho_img124);
            HOperatorSet.GenEmptyObj(out ho_img125);
            HOperatorSet.GenEmptyObj(out ho_img126);
            HOperatorSet.GenEmptyObj(out ho_img127);
            HOperatorSet.GenEmptyObj(out ho_img128);
            HOperatorSet.GenEmptyObj(out ho_img129);
            HOperatorSet.GenEmptyObj(out ho_imgout1);
            HOperatorSet.GenEmptyObj(out ho_img130);
            HOperatorSet.GenEmptyObj(out ho_imgout2);
            HOperatorSet.GenEmptyObj(out ho_imgout2a);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_imgout3);
            HOperatorSet.GenEmptyObj(out ho_imgout4);
            HOperatorSet.GenEmptyObj(out ho_imgout5);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions2);
            hv_HomMat3DObjInCamera = new HTuple();
            hv_HomMat3Dscreen = new HTuple();
            hv_HomTemp = new HTuple();
            hv_pixelSizeX = new HTuple();
            hv_pixelSizeY = new HTuple();
            //标定板参数  与标定板参数一一对应
            //利用算子助手，保存标定文件,并引用标定文件
            //*****************************
            //****改动 添加标定板参数后续备用
            hv_NumRows.Dispose();
            hv_NumRows = 13;
            hv_MarksPerRow.Dispose();
            hv_MarksPerRow = 15;
            hv_Diameter.Dispose();
            hv_Diameter = 0.0035;
            hv_FinderRow.Dispose();
            hv_FinderRow = new HTuple();
            hv_FinderRow[0] = 6;
            hv_FinderRow[1] = 2;
            hv_FinderRow[2] = 2;
            hv_FinderRow[3] = 10;
            hv_FinderRow[4] = 10;
            hv_FinderColumn.Dispose();
            hv_FinderColumn = new HTuple();
            hv_FinderColumn[0] = 7;
            hv_FinderColumn[1] = 2;
            hv_FinderColumn[2] = 12;
            hv_FinderColumn[3] = 2;
            hv_FinderColumn[4] = 12;
            //******************************
            //create_caltab (13, 15, 0.0035, [6,2,2,10,10], [7,2,12,2,12], 'dark_on_light', 'E:/程序/标定文件/HC-105-1.cpd', 'E:/程序/标定文件/HC-105-1.ps')
            //相机参数：焦距30

            //*****文件路径
            //标定文件
            hv_dcpdir.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_dcpdir = hv_path + "/HC-105.cpd";
            }
            //内参图像路径
            hv_pathinner.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_pathinner = hv_path + "/inner";
            }
            //基准平面标定路径
            hv_pathStd.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_pathStd = hv_path + "/std";
            }
            //屏幕标定路径
            hv_pathScreen.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_pathScreen = hv_path + "/screen";
            }

            hv_Files.Dispose();
            HOperatorSet.ListFiles(hv_pathinner, "files", out hv_Files);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Image.Dispose();
                HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(0));
            }
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            //初始赋值参数（焦距，kappa，像元水平尺寸，竖直尺寸，中点列坐标，中点行坐标，图像宽，图像高）
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_StartCamPar.Dispose();
                gen_cam_par_area_scan_division(0.030, 0, 0.00000345, 0.00000345, hv_Width / 2,
                    hv_Height / 2, hv_Width, hv_Height, out hv_StartCamPar);
            }

            //****************标定工作状态的相机内参
            hv_CalibDataID.Dispose();
            HOperatorSet.CreateCalibData("calibration_object", 1, 1, out hv_CalibDataID);
            HOperatorSet.SetCalibDataCamParam(hv_CalibDataID, 0, new HTuple(), hv_StartCamPar);
            HOperatorSet.SetCalibDataCalibObject(hv_CalibDataID, 0, hv_dcpdir);
            hv_NumImages.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_NumImages = new HTuple(hv_Files.TupleLength()
                    );
            }
            //Note, we do not use the image from which the pose of the measurement plane can be derived

            HTuple end_val36 = hv_NumImages - 1;
            HTuple step_val36 = 1;
            for (hv_I = 0; hv_I.Continue(end_val36, step_val36); hv_I = hv_I.TupleAdd(step_val36))
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, hv_Files.TupleSelect(hv_I));
                }
                ho_ImageMirror.Dispose();
                HOperatorSet.MirrorImage(ho_Image, out ho_ImageMirror, "row");
                HOperatorSet.FindCalibObject(ho_ImageMirror, hv_CalibDataID, 0, 0, hv_I, new HTuple(),
                    new HTuple());
                ho_Caltab.Dispose();
                HOperatorSet.GetCalibDataObservContours(out ho_Caltab, hv_CalibDataID, "caltab",
                    0, 0, hv_I);
                if (HDevWindowStack.IsOpen())
                {
                    //dev_set_color ('green')
                }
                if (HDevWindowStack.IsOpen())
                {
                    //dev_display (Caltab)
                }
            }
            hv_Error.Dispose();
            HOperatorSet.CalibrateCameras(hv_CalibDataID, out hv_Error);
            //内参参数：CamParam
            hv_CamParam.Dispose();
            HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 0, "params", out hv_CamParam);

            //************************工作状态的相机内参结束
            //************************标准平面标定开始
            hv_Files2.Dispose();
            HOperatorSet.ListFiles(hv_pathStd, "files", out hv_Files2);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_ImageStd.Dispose();
                HOperatorSet.ReadImage(out ho_ImageStd, hv_Files2.TupleSelect(0));
            }
            //Calibration 01: Create calibration model for managing calibration data
            //*****改动 改变标定板识别sigma参数
            HOperatorSet.FindCalibObject(ho_ImageStd, hv_CalibDataID, 0, 0, hv_NumImages,
                "sigma", 1.5);
            hv_TmpCtrl_MarkRows.Dispose(); hv_TmpCtrl_MarkColumns.Dispose(); hv_TmpCtrl_Ind.Dispose(); hv_ObjInCameraPose.Dispose();
            HOperatorSet.GetCalibDataObservPoints(hv_CalibDataID, 0, 0, hv_NumImages, out hv_TmpCtrl_MarkRows,
                out hv_TmpCtrl_MarkColumns, out hv_TmpCtrl_Ind, out hv_ObjInCameraPose);

            hv_X.Dispose();
            HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj", 0, "x", out hv_X);
            hv_Y.Dispose();
            HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj", 0, "y", out hv_Y);
            hv_Z.Dispose();
            HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj", 0, "z", out hv_Z);

            hv_XSelect.Dispose();
            HOperatorSet.TupleSelect(hv_X, hv_TmpCtrl_Ind, out hv_XSelect);
            hv_YSelect.Dispose();
            HOperatorSet.TupleSelect(hv_Y, hv_TmpCtrl_Ind, out hv_YSelect);
            hv_ZSelect.Dispose();
            HOperatorSet.TupleSelect(hv_Z, hv_TmpCtrl_Ind, out hv_ZSelect);

            hv_num.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_num = new HTuple(hv_XSelect.TupleLength()
                    );
            }

            //***改动 添加XSelect1 YSelect1 后面会使用XSelect YSelect
            hv_XSelect1.Dispose();
            HOperatorSet.TupleGenConst(hv_num, 0, out hv_XSelect1);
            hv_YSelect1.Dispose();
            HOperatorSet.TupleGenConst(hv_num, 0, out hv_YSelect1);
            HTuple end_val70 = hv_num - 1;
            HTuple step_val70 = 1;
            for (hv_Index = 0; hv_Index.Continue(end_val70, step_val70); hv_Index = hv_Index.TupleAdd(step_val70))
            {
                if (hv_XSelect1 == null)
                    hv_XSelect1 = new HTuple();
                hv_XSelect1[hv_Index] = (hv_XSelect.TupleSelect(hv_Index)) * 1000;
                if (hv_YSelect1 == null)
                    hv_YSelect1 = new HTuple();
                hv_YSelect1[hv_Index] = (hv_YSelect.TupleSelect(hv_Index)) * 1000;

            }
            //****改动 去掉后面两句
            //vector_to_proj_hom_mat2d (TmpCtrl_MarkColumns, TmpCtrl_MarkRows, XSelect, YSelect, 'gold_standard', [], [], [], [], [], [], HomMat2D1, Covariance)
            //vector_to_hom_mat2d (TmpCtrl_MarkColumns, TmpCtrl_MarkRows, XSelect, YSelect, HomMat2D)
            hv_ObjInCameraPose1.Dispose(); hv_Quality.Dispose();
            HOperatorSet.VectorToPose(hv_XSelect1, hv_YSelect1, hv_ZSelect, hv_TmpCtrl_MarkRows,
                hv_TmpCtrl_MarkColumns, hv_CamParam, "iterative", "error", out hv_ObjInCameraPose1,
                out hv_Quality);
            //***改动  0.001改为1
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.SetOriginPose(hv_ObjInCameraPose1, 0.0, 0.0, 1, out ExpTmpOutVar_0);
                hv_ObjInCameraPose1.Dispose();
                hv_ObjInCameraPose1 = ExpTmpOutVar_0;
            }

            //*************目标输出对象：单一性矩阵 HomMat3DObjInCamera
            hv_HomMat3DObjInCamera.Dispose();
            HOperatorSet.PoseToHomMat3d(hv_ObjInCameraPose1, out hv_HomMat3DObjInCamera);


            //******改动  获取projecthom
            hv_numMarks.Dispose();
            HOperatorSet.GetCalibData(hv_CalibDataID, "calib_obj", 0, "num_marks", out hv_numMarks);
            hv_marksNum.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_marksNum = new HTuple(hv_ZSelect.TupleLength()
                    );
            }

            hv_patternIndex.Dispose();
            hv_patternIndex = new HTuple();
            hv_patternIndex[0] = 0;
            hv_patternIndex[1] = 0;
            hv_patternIndex[2] = 0;
            hv_patternIndex[3] = 0;
            for (hv_index = 0; (int)hv_index <= 3; hv_index = (int)hv_index + 1)
            {
                if (hv_patternIndex == null)
                    hv_patternIndex = new HTuple();
                hv_patternIndex[hv_index] = ((hv_FinderRow.TupleSelect(hv_index + 1)) * hv_MarksPerRow) + (hv_FinderColumn.TupleSelect(
                    hv_index + 1));
            }

            hv_centerNum.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_centerNum = ((hv_FinderRow.TupleSelect(
                    0)) * hv_MarksPerRow) + (hv_FinderColumn.TupleSelect(0));
            }

            hv_marksPt3dX.Dispose();
            hv_marksPt3dX = new HTuple();
            hv_marksPt3dX[0] = 0;
            hv_marksPt3dX[1] = 0;
            hv_marksPt3dX[2] = 0;
            hv_marksPt3dX[3] = 0;
            hv_marksPt3dY.Dispose();
            hv_marksPt3dY = new HTuple();
            hv_marksPt3dY[0] = 0;
            hv_marksPt3dY[1] = 0;
            hv_marksPt3dY[2] = 0;
            hv_marksPt3dY[3] = 0;
            hv_marksPt3dZ.Dispose();
            hv_marksPt3dZ = new HTuple();
            hv_marksPt3dZ[0] = 0;
            hv_marksPt3dZ[1] = 0;
            hv_marksPt3dZ[2] = 0;
            hv_marksPt3dZ[3] = 0;
            for (hv_index = 0; (int)hv_index <= 3; hv_index = (int)hv_index + 1)
            {
                hv_index1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_index1 = hv_patternIndex.TupleSelect(
                        hv_index);
                }
                if (hv_marksPt3dX == null)
                    hv_marksPt3dX = new HTuple();
                hv_marksPt3dX[hv_index] = hv_X.TupleSelect(hv_index1);
                if (hv_marksPt3dY == null)
                    hv_marksPt3dY = new HTuple();
                hv_marksPt3dY[hv_index] = hv_Y.TupleSelect(hv_index1);
                if (hv_marksPt3dZ == null)
                    hv_marksPt3dZ = new HTuple();
                hv_marksPt3dZ[hv_index] = hv_Z.TupleSelect(hv_index1);
            }
            hv_centerPt3dX.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_centerPt3dX = hv_X.TupleSelect(
                    hv_centerNum);
            }
            hv_centerPt3dY.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_centerPt3dY = hv_Y.TupleSelect(
                    hv_centerNum);
            }
            hv_centerPt3dZ.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_centerPt3dZ = hv_Z.TupleSelect(
                    hv_centerNum);
            }


            hv_marksPt2dR.Dispose();
            hv_marksPt2dR = new HTuple();
            hv_marksPt2dR[0] = 0;
            hv_marksPt2dR[1] = 0;
            hv_marksPt2dR[2] = 0;
            hv_marksPt2dR[3] = 0;
            hv_marksPt2dC.Dispose();
            hv_marksPt2dC = new HTuple();
            hv_marksPt2dC[0] = 0;
            hv_marksPt2dC[1] = 0;
            hv_marksPt2dC[2] = 0;
            hv_marksPt2dC[3] = 0;
            hv_centerC.Dispose();
            hv_centerC = 0;
            hv_centerR.Dispose();
            hv_centerR = 0;
            if ((int)(new HTuple(hv_marksNum.TupleEqual(hv_numMarks))) != 0)
            {
                hv_centerC.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_centerC = hv_TmpCtrl_MarkColumns.TupleSelect(
                        hv_centerNum);
                }
                hv_centerR.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_centerR = hv_TmpCtrl_MarkRows.TupleSelect(
                        hv_centerNum);
                }

                for (hv_index = 0; (int)hv_index <= 3; hv_index = (int)hv_index + 1)
                {
                    hv_index1.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_index1 = hv_patternIndex.TupleSelect(
                            hv_index);
                    }
                    if (hv_marksPt2dC == null)
                        hv_marksPt2dC = new HTuple();
                    hv_marksPt2dC[hv_index] = hv_TmpCtrl_MarkColumns.TupleSelect(hv_index1);
                    if (hv_marksPt2dR == null)
                        hv_marksPt2dR = new HTuple();
                    hv_marksPt2dR[hv_index] = hv_TmpCtrl_MarkRows.TupleSelect(hv_index1);
                }
            }
            else
            {
                hv_HomMat2D3.Dispose(); hv_Covariance.Dispose();
                HOperatorSet.VectorToProjHomMat2d(hv_XSelect, hv_YSelect, hv_TmpCtrl_MarkColumns,
                    hv_TmpCtrl_MarkRows, "normalized_dlt", new HTuple(), new HTuple(), new HTuple(),
                    new HTuple(), new HTuple(), new HTuple(), out hv_HomMat2D3, out hv_Covariance);
                hv_zValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_zValue = (((hv_HomMat2D3.TupleSelect(
                        6)) * hv_centerPt3dX) + ((hv_HomMat2D3.TupleSelect(7)) * hv_centerPt3dY)) + (hv_HomMat2D3.TupleSelect(
                        8));
                }
                hv_centerC.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_centerC = ((((hv_HomMat2D3.TupleSelect(
                        0)) * hv_centerPt3dX) + ((hv_HomMat2D3.TupleSelect(1)) * hv_centerPt3dY)) + (hv_HomMat2D3.TupleSelect(
                        2))) / hv_zValue;
                }
                hv_centerR.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_centerR = ((((hv_HomMat2D3.TupleSelect(
                        3)) * hv_centerPt3dX) + ((hv_HomMat2D3.TupleSelect(4)) * hv_centerPt3dY)) + (hv_HomMat2D3.TupleSelect(
                        5))) / hv_zValue;
                }
                for (hv_index = 0; (int)hv_index <= 3; hv_index = (int)hv_index + 1)
                {
                    hv_zValue.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_zValue = (((hv_HomMat2D3.TupleSelect(
                            6)) * (hv_marksPt3dX.TupleSelect(hv_index))) + ((hv_HomMat2D3.TupleSelect(
                            7)) * (hv_marksPt3dY.TupleSelect(hv_index)))) + (hv_HomMat2D3.TupleSelect(
                            8));
                    }
                    if (hv_marksPt2dC == null)
                        hv_marksPt2dC = new HTuple();
                    hv_marksPt2dC[hv_index] = ((((hv_HomMat2D3.TupleSelect(0)) * (hv_marksPt3dX.TupleSelect(
                        hv_index))) + ((hv_HomMat2D3.TupleSelect(1)) * (hv_marksPt3dY.TupleSelect(
                        hv_index)))) + (hv_HomMat2D3.TupleSelect(2))) / hv_zValue;
                    if (hv_marksPt2dR == null)
                        hv_marksPt2dR = new HTuple();
                    hv_marksPt2dR[hv_index] = ((((hv_HomMat2D3.TupleSelect(3)) * (hv_marksPt3dX.TupleSelect(
                        hv_index))) + ((hv_HomMat2D3.TupleSelect(4)) * (hv_marksPt3dY.TupleSelect(
                        hv_index)))) + (hv_HomMat2D3.TupleSelect(5))) / hv_zValue;
                }
            }

            //实际长度0-1
            hv_len0_1.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_len0_1 = (((((hv_marksPt3dX.TupleSelect(
                    0)) - (hv_marksPt3dX.TupleSelect(1))) * ((hv_marksPt3dX.TupleSelect(0)) - (hv_marksPt3dX.TupleSelect(
                    1)))) + (((hv_marksPt3dY.TupleSelect(0)) - (hv_marksPt3dY.TupleSelect(1))) * ((hv_marksPt3dY.TupleSelect(
                    0)) - (hv_marksPt3dY.TupleSelect(1)))))).TupleSqrt();
            }
            //实际长度1-3
            hv_len1_3.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_len1_3 = (((((hv_marksPt3dX.TupleSelect(
                    3)) - (hv_marksPt3dX.TupleSelect(1))) * ((hv_marksPt3dX.TupleSelect(3)) - (hv_marksPt3dX.TupleSelect(
                    1)))) + (((hv_marksPt3dY.TupleSelect(3)) - (hv_marksPt3dY.TupleSelect(1))) * ((hv_marksPt3dY.TupleSelect(
                    3)) - (hv_marksPt3dY.TupleSelect(1)))))).TupleSqrt();
            }
            //获取四个角点到中心点的距离
            hv_len1.Dispose();
            hv_len1 = new HTuple();
            hv_len1[0] = 0;
            hv_len1[1] = 0;
            hv_len1[2] = 0;
            hv_len1[3] = 0;
            for (hv_index = 0; (int)hv_index <= 3; hv_index = (int)hv_index + 1)
            {
                if (hv_len1 == null)
                    hv_len1 = new HTuple();
                hv_len1[hv_index] = (((((hv_marksPt2dC.TupleSelect(hv_index)) - hv_centerC) * ((hv_marksPt2dC.TupleSelect(
                    hv_index)) - hv_centerC)) + (((hv_marksPt2dR.TupleSelect(hv_index)) - hv_centerR) * ((hv_marksPt2dR.TupleSelect(
                    hv_index)) - hv_centerR)))).TupleSqrt();
            }
            hv_len7.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_len7 = ((((hv_len1.TupleSelect(
                    0)) + (hv_len1.TupleSelect(1))) + (hv_len1.TupleSelect(2))) + (hv_len1.TupleSelect(
                    3))) * 0.25;
            }
            hv_ratio.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ratio = hv_len1_3 / hv_len0_1;
            }
            hv_len5.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_len5 = (2 * hv_len7) / (((1 + (hv_ratio * hv_ratio))).TupleSqrt()
                    );
            }
            hv_len6.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_len6 = hv_ratio * hv_len5;
            }

            hv_inputPtInWorldX.Dispose();
            hv_inputPtInWorldX = new HTuple();
            hv_inputPtInWorldX[0] = 0;
            hv_inputPtInWorldX[1] = 0;
            hv_inputPtInWorldX[2] = 0;
            hv_inputPtInWorldX[3] = 0;
            hv_inputPtInWorldY.Dispose();
            hv_inputPtInWorldY = new HTuple();
            hv_inputPtInWorldY[0] = 0;
            hv_inputPtInWorldY[1] = 0;
            hv_inputPtInWorldY[2] = 0;
            hv_inputPtInWorldY[3] = 0;

            //新的代码
            for (hv_index = 0; (int)hv_index <= 3; hv_index = (int)hv_index + 1)
            {
                if ((int)(new HTuple(((hv_marksPt2dC.TupleSelect(hv_index))).TupleGreater(hv_centerC))) != 0)
                {
                    if (hv_inputPtInWorldX == null)
                        hv_inputPtInWorldX = new HTuple();
                    hv_inputPtInWorldX[hv_index] = hv_centerC + (hv_len5 * 0.5);
                }
                else
                {
                    if (hv_inputPtInWorldX == null)
                        hv_inputPtInWorldX = new HTuple();
                    hv_inputPtInWorldX[hv_index] = hv_centerC - (hv_len5 * 0.5);
                }

                if ((int)(new HTuple(((hv_marksPt2dR.TupleSelect(hv_index))).TupleGreater(hv_centerR))) != 0)
                {
                    if (hv_inputPtInWorldY == null)
                        hv_inputPtInWorldY = new HTuple();
                    hv_inputPtInWorldY[hv_index] = hv_centerR + (hv_len6 * 0.5);
                }
                else
                {
                    if (hv_inputPtInWorldY == null)
                        hv_inputPtInWorldY = new HTuple();
                    hv_inputPtInWorldY[hv_index] = hv_centerR - (hv_len6 * 0.5);
                }
            }

            //原来有异常的代码

            //if (hv_inputPtInWorldX == null)
            //    hv_inputPtInWorldX = new HTuple();
            //hv_inputPtInWorldX[0] = hv_centerC - (hv_len5 * 0.5);
            //if (hv_inputPtInWorldY == null)
            //    hv_inputPtInWorldY = new HTuple();
            //hv_inputPtInWorldY[0] = hv_centerR - (hv_len6 * 0.5);

            //if (hv_inputPtInWorldX == null)
            //    hv_inputPtInWorldX = new HTuple();
            //hv_inputPtInWorldX[1] = hv_centerC + (hv_len5 * 0.5);
            //if (hv_inputPtInWorldY == null)
            //    hv_inputPtInWorldY = new HTuple();
            //hv_inputPtInWorldY[1] = hv_centerR - (hv_len6 * 0.5);

            //if (hv_inputPtInWorldX == null)
            //    hv_inputPtInWorldX = new HTuple();
            //hv_inputPtInWorldX[2] = hv_centerC - (hv_len5 * 0.5);
            //if (hv_inputPtInWorldY == null)
            //    hv_inputPtInWorldY = new HTuple();
            //hv_inputPtInWorldY[2] = hv_centerR + (hv_len6 * 0.5);

            //if (hv_inputPtInWorldX == null)
            //    hv_inputPtInWorldX = new HTuple();
            //hv_inputPtInWorldX[3] = hv_centerC + (hv_len5 * 0.5);
            //if (hv_inputPtInWorldY == null)
            //    hv_inputPtInWorldY = new HTuple();
            //hv_inputPtInWorldY[3] = hv_centerR + (hv_len6 * 0.5);

            hv_HomTemp.Dispose(); hv_Covariance.Dispose();
            HOperatorSet.VectorToProjHomMat2d(hv_inputPtInWorldX, hv_inputPtInWorldY, hv_marksPt2dC,
                hv_marksPt2dR, "normalized_dlt", new HTuple(), new HTuple(), new HTuple(),
                new HTuple(), new HTuple(), new HTuple(), out hv_HomTemp, out hv_Covariance);

            for (hv_index = 0; (int)hv_index <= 8; hv_index = (int)hv_index + 1)
            {
                if (hv_HomTemp == null)
                    hv_HomTemp = new HTuple();
                hv_HomTemp[hv_index] = (hv_HomTemp.TupleSelect(hv_index)) / (hv_HomTemp.TupleSelect(
                    8));
            }

            hv_pixel2_3.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_pixel2_3 = ((((((hv_inputPtInWorldX.TupleSelect(
                    2)) - (hv_inputPtInWorldX.TupleSelect(3)))).TuplePow(2)) + ((((hv_inputPtInWorldY.TupleSelect(
                    2)) - (hv_inputPtInWorldY.TupleSelect(3)))).TuplePow(2)))).TupleSqrt();
            }
            hv_pixelSizeX.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_pixelSizeX = hv_len0_1 / hv_pixel2_3;
            }
            hv_pixel0_2.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_pixel0_2 = ((((((hv_inputPtInWorldX.TupleSelect(
                    2)) - (hv_inputPtInWorldX.TupleSelect(0)))).TuplePow(2)) + ((((hv_inputPtInWorldY.TupleSelect(
                    2)) - (hv_inputPtInWorldY.TupleSelect(0)))).TuplePow(2)))).TupleSqrt();
            }
            hv_pixelSizeY.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_pixelSizeY = hv_len1_3 / hv_pixel0_2;
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                {
                    HTuple
                      ExpTmpLocalVar_pixelSizeX = (0.5 * (hv_pixelSizeX + hv_pixelSizeY)) * 1000;
                    hv_pixelSizeX.Dispose();
                    hv_pixelSizeX = ExpTmpLocalVar_pixelSizeX;
                }
            }
            hv_pixelSizeY.Dispose();
            hv_pixelSizeY = new HTuple(hv_pixelSizeX);

            //***********************获得屏幕的标定矩阵开始
            //选用照片的光源条纹宽度及相移
            //1024 1024 0
            //64 64 32
            //1024 1024 512
            //64 64 32

            hv_Files1.Dispose();
            HOperatorSet.ListFiles(hv_pathScreen, "files", out hv_Files1);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Image1.Dispose();
                HOperatorSet.ReadImage(out ho_Image1, hv_Files1.TupleSelect(0));
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Image2.Dispose();
                HOperatorSet.ReadImage(out ho_Image2, hv_Files1.TupleSelect(1));
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Image3.Dispose();
                HOperatorSet.ReadImage(out ho_Image3, hv_Files1.TupleSelect(2));
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Image4.Dispose();
                HOperatorSet.ReadImage(out ho_Image4, hv_Files1.TupleSelect(3));
            }
            //条纹分割阈值
            hv_GrayThreshold.Dispose();
            hv_GrayThreshold = 70;
            //条纹有一个方向出现的相移，1024/2=512
            //RowStart := 768
            hv_RowStart.Dispose();
            hv_RowStart = 1024;
            //32是条纹宽度相对于中心点位置的距离，实际是64条纹宽度
            hv_Rshift.Dispose();
            hv_Rshift = 32;

            hv_ColStart.Dispose();
            hv_ColStart = 512;
            hv_Cshift.Dispose();
            hv_Cshift = -32;

            //*****************************
            //屏幕分辨率
            hv_ScaleRate.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ScaleRate = (hv_widRate + hv_heightRate) / 2;
            }

            ho_img12.Dispose();
            HOperatorSet.Threshold(ho_Image1, out ho_img12, hv_GrayThreshold, 255);
            //*
            ho_RegionFillUp.Dispose();
            HOperatorSet.FillUp(ho_img12, out ho_RegionFillUp);
            ho_img13.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionFillUp, out ho_img13, 1, 16);
            //*
            ho_img14.Dispose();
            HOperatorSet.ErosionRectangle1(ho_img13, out ho_img14, 3, 3);
            ho_img15.Dispose();
            HOperatorSet.Difference(ho_img13, ho_img14, out ho_img15);
            ho_img32.Dispose();
            HOperatorSet.Threshold(ho_Image3, out ho_img32, hv_GrayThreshold, 255);

            ho_RegionFillUp1.Dispose();
            HOperatorSet.FillUp(ho_img32, out ho_RegionFillUp1);
            ho_img33.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionFillUp1, out ho_img33, 16, 1);
            //*
            ho_img34.Dispose();
            HOperatorSet.ErosionRectangle1(ho_img33, out ho_img34, 3, 3);
            ho_img35.Dispose();
            HOperatorSet.Difference(ho_img33, ho_img34, out ho_img35);
            ho_img55.Dispose();
            HOperatorSet.Intersection(ho_img15, ho_img35, out ho_img55);
            //***************************************
            ho_img22.Dispose();
            HOperatorSet.Threshold(ho_Image2, out ho_img22, hv_GrayThreshold, 255);
            ho_RegionFillUp3.Dispose();
            HOperatorSet.FillUp(ho_img22, out ho_RegionFillUp3);
            ho_img23.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionFillUp3, out ho_img23, 1, 16);
            //*
            ho_img24.Dispose();
            HOperatorSet.ErosionRectangle1(ho_img23, out ho_img24, 3, 3);
            ho_img25.Dispose();
            HOperatorSet.Difference(ho_img23, ho_img24, out ho_img25);
            ho_img44.Dispose();
            HOperatorSet.Threshold(ho_Image4, out ho_img44, hv_GrayThreshold, 255);
            ho_RegionFillUp2.Dispose();
            HOperatorSet.FillUp(ho_img44, out ho_RegionFillUp2);
            ho_img43.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionFillUp2, out ho_img43, 16, 2);
            //*
            ho_img44.Dispose();
            HOperatorSet.ErosionRectangle1(ho_img43, out ho_img44, 3, 3);
            ho_img45.Dispose();
            HOperatorSet.Difference(ho_img43, ho_img44, out ho_img45);
            ho_img66.Dispose();
            HOperatorSet.Intersection(ho_img25, ho_img45, out ho_img66);

            ho_img124.Dispose();
            HOperatorSet.AddImage(ho_Image2, ho_Image4, out ho_img124, 1, 0);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_img125.Dispose();
                HOperatorSet.Threshold(ho_img124, out ho_img125, hv_GrayThreshold + 30, 255);
            }
            ho_img126.Dispose();
            HOperatorSet.FillUp(ho_img125, out ho_img126);
            ho_img127.Dispose();
            HOperatorSet.OpeningRectangle1(ho_img126, out ho_img127, 256, 256);
            ho_img128.Dispose();
            HOperatorSet.ErosionRectangle1(ho_img127, out ho_img128, 128, 256);
            ho_img129.Dispose();
            HOperatorSet.OpeningRectangle1(ho_img128, out ho_img129, 1024, 512);

            ho_imgout1.Dispose();
            HOperatorSet.Intersection(ho_img55, ho_img129, out ho_imgout1);
            hv_Rows1.Dispose();
            HOperatorSet.RegionFeatures(ho_imgout1, "row1", out hv_Rows1);
            hv_Cols1.Dispose();
            HOperatorSet.RegionFeatures(ho_imgout1, "column1", out hv_Cols1);
            ho_img130.Dispose();
            HOperatorSet.DilationRectangle1(ho_imgout1, out ho_img130, 200, 200);
            ho_imgout2.Dispose();
            HOperatorSet.Intersection(ho_img66, ho_img130, out ho_imgout2);
            ho_imgout2a.Dispose();
            HOperatorSet.Connection(ho_imgout2, out ho_imgout2a);
            ho_SortedRegions.Dispose();
            HOperatorSet.SortRegion(ho_imgout2a, out ho_SortedRegions, "first_point", "true",
                "row");
            hv_Rows20.Dispose();
            HOperatorSet.RegionFeatures(ho_SortedRegions, "row1", out hv_Rows20);
            hv_Cols20.Dispose();
            HOperatorSet.RegionFeatures(ho_SortedRegions, "column1", out hv_Cols20);

            //手动排序
            for (hv_indx = 0; (int)hv_indx <= 3; hv_indx = (int)hv_indx + 1)
            {
                if ((int)(new HTuple(((hv_Rows20.TupleSelect(hv_indx))).TupleLess(hv_Rows1.TupleSelect(
                    0)))) != 0)
                {
                    if ((int)(new HTuple(((hv_Cols20.TupleSelect(hv_indx))).TupleLess(hv_Cols1.TupleSelect(
                        0)))) != 0)
                    {
                        if (hv_Rows2 == null)
                            hv_Rows2 = new HTuple();
                        hv_Rows2[0] = hv_Rows20.TupleSelect(hv_indx);
                        if (hv_Cols2 == null)
                            hv_Cols2 = new HTuple();
                        hv_Cols2[0] = hv_Cols20.TupleSelect(hv_indx);
                    }
                    if ((int)(new HTuple(((hv_Cols20.TupleSelect(hv_indx))).TupleGreater(hv_Cols1.TupleSelect(
                        0)))) != 0)
                    {
                        if (hv_Rows2 == null)
                            hv_Rows2 = new HTuple();
                        hv_Rows2[1] = hv_Rows20.TupleSelect(hv_indx);
                        if (hv_Cols2 == null)
                            hv_Cols2 = new HTuple();
                        hv_Cols2[1] = hv_Cols20.TupleSelect(hv_indx);
                    }
                }

                if ((int)(new HTuple(((hv_Rows20.TupleSelect(hv_indx))).TupleGreater(hv_Rows1.TupleSelect(
                    0)))) != 0)
                {
                    if ((int)(new HTuple(((hv_Cols20.TupleSelect(hv_indx))).TupleLess(hv_Cols1.TupleSelect(
                        0)))) != 0)
                    {
                        if (hv_Rows2 == null)
                            hv_Rows2 = new HTuple();
                        hv_Rows2[2] = hv_Rows20.TupleSelect(hv_indx);
                        if (hv_Cols2 == null)
                            hv_Cols2 = new HTuple();
                        hv_Cols2[2] = hv_Cols20.TupleSelect(hv_indx);
                    }
                    if ((int)(new HTuple(((hv_Cols20.TupleSelect(hv_indx))).TupleGreater(hv_Cols1.TupleSelect(
                        0)))) != 0)
                    {
                        if (hv_Rows2 == null)
                            hv_Rows2 = new HTuple();
                        hv_Rows2[3] = hv_Rows20.TupleSelect(hv_indx);
                        if (hv_Cols2 == null)
                            hv_Cols2 = new HTuple();
                        hv_Cols2[3] = hv_Cols20.TupleSelect(hv_indx);
                    }
                }
            }

            if (hv_Rows3 == null)
                hv_Rows3 = new HTuple();
            hv_Rows3[0] = hv_RowStart + hv_Rshift;
            if (hv_Cols3 == null)
                hv_Cols3 = new HTuple();
            hv_Cols3[0] = hv_ColStart + hv_Cshift;
            if (hv_Rows3 == null)
                hv_Rows3 = new HTuple();
            hv_Rows3[1] = hv_RowStart + hv_Rshift;
            if (hv_Cols3 == null)
                hv_Cols3 = new HTuple();
            hv_Cols3[1] = hv_ColStart - hv_Cshift;
            if (hv_Rows3 == null)
                hv_Rows3 = new HTuple();
            hv_Rows3[2] = hv_RowStart - hv_Rshift;
            if (hv_Cols3 == null)
                hv_Cols3 = new HTuple();
            hv_Cols3[2] = hv_ColStart + hv_Cshift;
            if (hv_Rows3 == null)
                hv_Rows3 = new HTuple();
            hv_Rows3[3] = hv_RowStart - hv_Rshift;
            if (hv_Cols3 == null)
                hv_Cols3 = new HTuple();
            hv_Cols3[3] = hv_ColStart - hv_Cshift;

            hv_Hommat2D.Dispose();
            HOperatorSet.VectorToHomMat2d(hv_Cols2, hv_Rows2, hv_Cols3, hv_Rows3, out hv_Hommat2D);

            for (hv_indx = 0; (int)hv_indx <= 3; hv_indx = (int)hv_indx + 1)
            {
                if (hv_RowsTmp == null)
                    hv_RowsTmp = new HTuple();
                hv_RowsTmp[hv_indx] = (hv_Rows3.TupleSelect(hv_indx)) * 0.1736;
                if (hv_ColsTmp == null)
                    hv_ColsTmp = new HTuple();
                hv_ColsTmp[hv_indx] = (hv_Cols3.TupleSelect(hv_indx)) * 0.1736;
            }

            hv_Hommat2D_A.Dispose();
            HOperatorSet.VectorToHomMat2d(hv_Cols2, hv_Rows2, hv_ColsTmp, hv_RowsTmp, out hv_Hommat2D_A);

            ho_imgout3.Dispose();
            HOperatorSet.Intersection(ho_img66, ho_img129, out ho_imgout3);
            ho_imgout4.Dispose();
            HOperatorSet.Connection(ho_imgout3, out ho_imgout4);
            ho_imgout5.Dispose();
            HOperatorSet.SelectShape(ho_imgout4, out ho_imgout5, "area", "and", 1, 6);
            ho_SortedRegions2.Dispose();
            HOperatorSet.SortRegion(ho_imgout5, out ho_SortedRegions2, "first_point", "true",
                "row");
            hv_Rows4.Dispose();
            HOperatorSet.RegionFeatures(ho_SortedRegions2, "row", out hv_Rows4);
            hv_Cols4.Dispose();
            HOperatorSet.RegionFeatures(ho_SortedRegions2, "column", out hv_Cols4);

            hv_Cols5.Dispose(); hv_Rows5.Dispose();
            HOperatorSet.AffineTransPoint2d(hv_Hommat2D, hv_Cols4, hv_Rows4, out hv_Cols5,
                out hv_Rows5);
            hv_PointNum.Dispose();
            HOperatorSet.CountObj(ho_SortedRegions2, out hv_PointNum);
            HTuple end_val303 = hv_PointNum - 1;
            HTuple step_val303 = 1;
            for (hv_indx = 0; hv_indx.Continue(end_val303, step_val303); hv_indx = hv_indx.TupleAdd(step_val303))
            {
                if (hv_Rows6 == null)
                    hv_Rows6 = new HTuple();
                hv_Rows6[hv_indx] = ((((hv_Rows5.TupleSelect(hv_indx)) / 64)).TupleRound()) * 64;
                if (hv_Rows6 == null)
                    hv_Rows6 = new HTuple();
                hv_Rows6[hv_indx] = ((hv_Rows6.TupleSelect(hv_indx)) - 0.5) * hv_ScaleRate;
                if (hv_Cols6 == null)
                    hv_Cols6 = new HTuple();
                hv_Cols6[hv_indx] = ((((hv_Cols5.TupleSelect(hv_indx)) / 64)).TupleRound()) * 64;
                if (hv_Cols6 == null)
                    hv_Cols6 = new HTuple();
                hv_Cols6[hv_indx] = ((hv_Cols6.TupleSelect(hv_indx)) - 0.5) * hv_ScaleRate;
            }
            hv_Newtuple.Dispose();
            HOperatorSet.TupleGenConst(hv_PointNum, 0, out hv_Newtuple);
            hv_Pose.Dispose(); hv_Quality.Dispose();
            HOperatorSet.VectorToPose(hv_Cols6, hv_Rows6, hv_Newtuple, hv_Rows4, hv_Cols4,
                hv_CamParam, "iterative", "error", out hv_Pose, out hv_Quality);

            //*************目标输出对象：单一性矩阵 HomMat3Dscreen
            hv_HomMat3Dscreen.Dispose();
            HOperatorSet.PoseToHomMat3d(hv_Pose, out hv_HomMat3Dscreen);
            ho_Image.Dispose();
            ho_ImageMirror.Dispose();
            ho_Caltab.Dispose();
            ho_ImageStd.Dispose();
            ho_Image1.Dispose();
            ho_Image2.Dispose();
            ho_Image3.Dispose();
            ho_Image4.Dispose();
            ho_img12.Dispose();
            ho_RegionFillUp.Dispose();
            ho_img13.Dispose();
            ho_img14.Dispose();
            ho_img15.Dispose();
            ho_img32.Dispose();
            ho_RegionFillUp1.Dispose();
            ho_img33.Dispose();
            ho_img34.Dispose();
            ho_img35.Dispose();
            ho_img55.Dispose();
            ho_img22.Dispose();
            ho_RegionFillUp3.Dispose();
            ho_img23.Dispose();
            ho_img24.Dispose();
            ho_img25.Dispose();
            ho_img44.Dispose();
            ho_RegionFillUp2.Dispose();
            ho_img43.Dispose();
            ho_img45.Dispose();
            ho_img66.Dispose();
            ho_img124.Dispose();
            ho_img125.Dispose();
            ho_img126.Dispose();
            ho_img127.Dispose();
            ho_img128.Dispose();
            ho_img129.Dispose();
            ho_imgout1.Dispose();
            ho_img130.Dispose();
            ho_imgout2.Dispose();
            ho_imgout2a.Dispose();
            ho_SortedRegions.Dispose();
            ho_imgout3.Dispose();
            ho_imgout4.Dispose();
            ho_imgout5.Dispose();
            ho_SortedRegions2.Dispose();

            hv_NumRows.Dispose();
            hv_MarksPerRow.Dispose();
            hv_Diameter.Dispose();
            hv_FinderRow.Dispose();
            hv_FinderColumn.Dispose();
            hv_dcpdir.Dispose();
            hv_pathinner.Dispose();
            hv_pathStd.Dispose();
            hv_pathScreen.Dispose();
            hv_Files.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_StartCamPar.Dispose();
            hv_CalibDataID.Dispose();
            hv_NumImages.Dispose();
            hv_I.Dispose();
            hv_Error.Dispose();
            hv_CamParam.Dispose();
            hv_Files2.Dispose();
            hv_TmpCtrl_MarkRows.Dispose();
            hv_TmpCtrl_MarkColumns.Dispose();
            hv_TmpCtrl_Ind.Dispose();
            hv_ObjInCameraPose.Dispose();
            hv_X.Dispose();
            hv_Y.Dispose();
            hv_Z.Dispose();
            hv_XSelect.Dispose();
            hv_YSelect.Dispose();
            hv_ZSelect.Dispose();
            hv_num.Dispose();
            hv_XSelect1.Dispose();
            hv_YSelect1.Dispose();
            hv_Index.Dispose();
            hv_Covariance.Dispose();
            hv_ObjInCameraPose1.Dispose();
            hv_Quality.Dispose();
            hv_numMarks.Dispose();
            hv_marksNum.Dispose();
            hv_patternIndex.Dispose();
            hv_index.Dispose();
            hv_centerNum.Dispose();
            hv_marksPt3dX.Dispose();
            hv_marksPt3dY.Dispose();
            hv_marksPt3dZ.Dispose();
            hv_index1.Dispose();
            hv_centerPt3dX.Dispose();
            hv_centerPt3dY.Dispose();
            hv_centerPt3dZ.Dispose();
            hv_marksPt2dR.Dispose();
            hv_marksPt2dC.Dispose();
            hv_centerC.Dispose();
            hv_centerR.Dispose();
            hv_HomMat2D3.Dispose();
            hv_zValue.Dispose();
            hv_len0_1.Dispose();
            hv_len1_3.Dispose();
            hv_len1.Dispose();
            hv_len7.Dispose();
            hv_ratio.Dispose();
            hv_len5.Dispose();
            hv_len6.Dispose();
            hv_inputPtInWorldX.Dispose();
            hv_inputPtInWorldY.Dispose();
            hv_pixel2_3.Dispose();
            hv_pixel0_2.Dispose();
            hv_Files1.Dispose();
            hv_GrayThreshold.Dispose();
            hv_RowStart.Dispose();
            hv_Rshift.Dispose();
            hv_ColStart.Dispose();
            hv_Cshift.Dispose();
            hv_ScaleRate.Dispose();
            hv_Rows1.Dispose();
            hv_Cols1.Dispose();
            hv_Rows20.Dispose();
            hv_Cols20.Dispose();
            hv_indx.Dispose();
            hv_Rows2.Dispose();
            hv_Cols2.Dispose();
            hv_Rows3.Dispose();
            hv_Cols3.Dispose();
            hv_Hommat2D.Dispose();
            hv_RowsTmp.Dispose();
            hv_ColsTmp.Dispose();
            hv_Hommat2D_A.Dispose();
            hv_Rows4.Dispose();
            hv_Cols4.Dispose();
            hv_Cols5.Dispose();
            hv_Rows5.Dispose();
            hv_PointNum.Dispose();
            hv_Rows6.Dispose();
            hv_Cols6.Dispose();
            hv_Newtuple.Dispose();
            hv_Pose.Dispose();

            return;
        }

        public void save_XMLfiles(HTuple hv_FileName, HTuple hv_HomMat3DObjInCamera, HTuple hv_HomMat3Dscreen,
            HTuple hv_HomTemp, HTupleVector hvec_Elements, HTuple hv_StdMaxs,
            HTuple hv_StdMins, HTuple hv_ProductMaxs, HTuple hv_ProductMins, HTuple hv_pixelSizeX,
            HTuple hv_pixelSizeY, HTuple hv_widRate, HTuple hv_heightRate, out HTuple hv_value)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Filepath = new HTuple(), hv_stdparam = new HTuple();
            HTuple hv_Index0 = new HTuple(), hv_screenparam = new HTuple();
            HTuple hv_Index1 = new HTuple(), hv_productparam = new HTuple();
            HTuple hv_Index2 = new HTuple(), hv_Names = new HTuple();
            HTuple hv_i = new HTuple(), hv_param2 = new HTuple(), hv_index = new HTuple();
            HTuple hv_valueCount = new HTuple(), hv_foundValue = new HTuple();
            HTuple hv_param = new HTuple(), hv_param3 = new HTuple();
            HTuple hv_index3 = new HTuple(), hv_ThreshStdMax = new HTuple();
            HTuple hv_ThreshStdMin = new HTuple(), hv_ThreshProductMax = new HTuple();
            HTuple hv_ThreshProductMin = new HTuple(), hv_SizePixelX = new HTuple();
            HTuple hv_SizePixelY = new HTuple(), hv_ScaleRate = new HTuple();
            HTuple hv_ScreenPixSize = new HTuple(), hv_writeFileHandle = new HTuple();

            HTupleVector hvec_ElementsOut = new HTupleVector(2);
            // Initialize local and output iconic variables 
            hv_value = new HTuple();
            hvec_ElementsOut.Dispose();
            hvec_ElementsOut = new HTupleVector(hvec_Elements);
            //XML存储路径
            hv_Filepath.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Filepath = hv_FileName + "/Para.xml";
            }
            //矩阵写入Xml
            //std矩阵
            hv_stdparam.Dispose();
            hv_stdparam = new HTuple();
            for (hv_Index0 = 0; (int)hv_Index0 <= (int)((new HTuple(hv_HomMat3DObjInCamera.TupleLength()
                )) - 1); hv_Index0 = (int)hv_Index0 + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleConcat(hv_stdparam, hv_HomMat3DObjInCamera.TupleSelect(hv_Index0),
                        out ExpTmpOutVar_0);
                    hv_stdparam.Dispose();
                    hv_stdparam = ExpTmpOutVar_0;
                }
            }
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleConcat(hv_stdparam, 0, out ExpTmpOutVar_0);
                hv_stdparam.Dispose();
                hv_stdparam = ExpTmpOutVar_0;
            }
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleConcat(hv_stdparam, 0, out ExpTmpOutVar_0);
                hv_stdparam.Dispose();
                hv_stdparam = ExpTmpOutVar_0;
            }
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleConcat(hv_stdparam, 0, out ExpTmpOutVar_0);
                hv_stdparam.Dispose();
                hv_stdparam = ExpTmpOutVar_0;
            }
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleConcat(hv_stdparam, 1, out ExpTmpOutVar_0);
                hv_stdparam.Dispose();
                hv_stdparam = ExpTmpOutVar_0;
            }

            //screen矩阵
            hv_screenparam.Dispose();
            hv_screenparam = new HTuple();
            for (hv_Index1 = 0; (int)hv_Index1 <= (int)((new HTuple(hv_HomMat3Dscreen.TupleLength()
                )) - 1); hv_Index1 = (int)hv_Index1 + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleConcat(hv_screenparam, hv_HomMat3Dscreen.TupleSelect(hv_Index1),
                        out ExpTmpOutVar_0);
                    hv_screenparam.Dispose();
                    hv_screenparam = ExpTmpOutVar_0;
                }
            }
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleConcat(hv_screenparam, 0, out ExpTmpOutVar_0);
                hv_screenparam.Dispose();
                hv_screenparam = ExpTmpOutVar_0;
            }
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleConcat(hv_screenparam, 0, out ExpTmpOutVar_0);
                hv_screenparam.Dispose();
                hv_screenparam = ExpTmpOutVar_0;
            }
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleConcat(hv_screenparam, 0, out ExpTmpOutVar_0);
                hv_screenparam.Dispose();
                hv_screenparam = ExpTmpOutVar_0;
            }
            {
                HTuple ExpTmpOutVar_0;
                HOperatorSet.TupleConcat(hv_screenparam, 1, out ExpTmpOutVar_0);
                hv_screenparam.Dispose();
                hv_screenparam = ExpTmpOutVar_0;
            }


            //**改product矩阵
            hv_productparam.Dispose();
            hv_productparam = new HTuple();
            for (hv_Index2 = 0; (int)hv_Index2 <= (int)((new HTuple(hv_HomTemp.TupleLength())) - 1); hv_Index2 = (int)hv_Index2 + 1)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleConcat(hv_productparam, hv_HomTemp.TupleSelect(hv_Index2),
                        out ExpTmpOutVar_0);
                    hv_productparam.Dispose();
                    hv_productparam = ExpTmpOutVar_0;
                }
            }
            //tuple_concat (productparam, 1, productparam)


            //# 根据参数名查找数值↓
            hv_Names.Dispose();
            hv_Names = new HTuple();
            HTuple end_val35 = new HTuple(hvec_ElementsOut.Length) - 1;
            HTuple step_val35 = 1;
            for (hv_i = 0; hv_i.Continue(end_val35, step_val35); hv_i = hv_i.TupleAdd(step_val35))
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Names = hv_Names.TupleConcat(
                            hvec_ElementsOut[hv_i][0].T);
                        hv_Names.Dispose();
                        hv_Names = ExpTmpLocalVar_Names;
                    }
                }
            }
            hv_value.Dispose();
            hv_value = new HTuple();

            //std
            hv_param2.Dispose();
            hv_param2 = "HomStdData";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_param2);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
                )) - 1); hv_valueCount = (int)hv_valueCount + 1)
            {
                hv_foundValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                        hv_valueCount)][1].T);
                }
                if ((int)(hv_foundValue.TupleIsNumber()) != 0)
                {
                    //tuple_number (foundValue1Out, foundValue1Out)
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_ElementsOut[hv_index.TupleSelect(
                            hv_valueCount)][1] = dh.Add(new HTupleVector(hv_stdparam.TupleSelect(
                            hv_valueCount)));
                    }
                }
                //value1 := [value1, foundValue1Out]
            }

            //screen
            hv_param.Dispose();
            hv_param = "HomScreenData";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_param);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
                )) - 1); hv_valueCount = (int)hv_valueCount + 1)
            {
                hv_foundValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                        hv_valueCount)][1].T);
                }
                if ((int)(hv_foundValue.TupleIsNumber()) != 0)
                {
                    //tuple_number (foundValue, foundValue)
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_ElementsOut[hv_index.TupleSelect(
                            hv_valueCount)][1] = dh.Add(new HTupleVector(hv_screenparam.TupleSelect(
                            hv_valueCount)));
                    }
                }
            }

            //****改  ****产品矩阵参数
            hv_param3.Dispose();
            hv_param3 = "HomProj";
            hv_index3.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index3 = hv_Names.TupleFind(
                    hv_param3);
            }
            if ((int)(new HTuple(hv_index3.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index3.TupleLength()
                )) - 1); hv_valueCount = (int)hv_valueCount + 1)
            {
                hv_foundValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_foundValue = new HTuple(hvec_ElementsOut[hv_index3.TupleSelect(
                        hv_valueCount)][1].T);
                }
                if ((int)(hv_foundValue.TupleIsNumber()) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_ElementsOut[hv_index3.TupleSelect(
                            hv_valueCount)][1] = dh.Add(new HTupleVector(hv_productparam.TupleSelect(
                            hv_valueCount)));
                    }
                }
            }

            //其他参数
            //ThreshStdMax
            hv_ThreshStdMax.Dispose();
            hv_ThreshStdMax = "ThreshStdMaxs";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_ThreshStdMax);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
                )) - 1); hv_valueCount = (int)hv_valueCount + 1)
            {
                hv_foundValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                        hv_valueCount)][1].T);
                }
                if ((int)(hv_foundValue.TupleIsNumber()) != 0)
                {
                    //tuple_number (foundValue, foundValue)
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_ElementsOut[hv_index.TupleSelect(
                            hv_valueCount)][1] = dh.Add(new HTupleVector(hv_StdMaxs.TupleSelect(hv_valueCount)));
                    }
                }
            }

            //ThreshStdMax
            hv_ThreshStdMin.Dispose();
            hv_ThreshStdMin = "ThreshStdMins";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_ThreshStdMin);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
                )) - 1); hv_valueCount = (int)hv_valueCount + 1)
            {
                hv_foundValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                        hv_valueCount)][1].T);
                }
                if ((int)(hv_foundValue.TupleIsNumber()) != 0)
                {
                    //tuple_number (foundValue, foundValue)
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_ElementsOut[hv_index.TupleSelect(
                            hv_valueCount)][1] = dh.Add(new HTupleVector(hv_StdMins.TupleSelect(hv_valueCount)));
                    }
                }
            }

            //ThreshProductMax
            hv_ThreshProductMax.Dispose();
            hv_ThreshProductMax = "ThreshProductMaxs";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_ThreshProductMax);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
                )) - 1); hv_valueCount = (int)hv_valueCount + 1)
            {
                hv_foundValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                        hv_valueCount)][1].T);
                }
                if ((int)(hv_foundValue.TupleIsNumber()) != 0)
                {
                    //tuple_number (foundValue, foundValue)
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_ElementsOut[hv_index.TupleSelect(
                            hv_valueCount)][1] = dh.Add(new HTupleVector(hv_ProductMaxs.TupleSelect(
                            hv_valueCount)));
                    }
                }
            }


            //ThreshProductMin
            hv_ThreshProductMin.Dispose();
            hv_ThreshProductMin = "ThreshProductMins";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_ThreshProductMin);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            for (hv_valueCount = 0; (int)hv_valueCount <= (int)((new HTuple(hv_index.TupleLength()
                )) - 1); hv_valueCount = (int)hv_valueCount + 1)
            {
                hv_foundValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                        hv_valueCount)][1].T);
                }
                if ((int)(hv_foundValue.TupleIsNumber()) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_ElementsOut[hv_index.TupleSelect(
                            hv_valueCount)][1] = dh.Add(new HTupleVector(hv_ProductMins.TupleSelect(
                            hv_valueCount)));
                    }
                }
            }

            //SizePixelX
            hv_SizePixelX.Dispose();
            hv_SizePixelX = "SizePixelX";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_SizePixelX);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            hv_foundValue.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                    0)][1].T);
            }
            if ((int)(hv_foundValue.TupleIsNumber()) != 0)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_ElementsOut[hv_index.TupleSelect(
                        0)][1] = dh.Add(new HTupleVector(hv_pixelSizeX));
                }
            }


            //SizePixelY
            hv_SizePixelY.Dispose();
            hv_SizePixelY = "SizePixelY";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_SizePixelY);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            hv_foundValue.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                    0)][1].T);
            }
            if ((int)(hv_foundValue.TupleIsNumber()) != 0)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_ElementsOut[hv_index.TupleSelect(
                        0)][1] = dh.Add(new HTupleVector(hv_pixelSizeY));
                }
            }

            //ScreenPixSize

            hv_ScaleRate.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ScaleRate = (hv_widRate + hv_heightRate) / 2;
            }

            hv_ScreenPixSize.Dispose();
            hv_ScreenPixSize = "ScreenPixSize";
            hv_index.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_index = hv_Names.TupleFind(
                    hv_ScreenPixSize);
            }
            if ((int)(new HTuple(hv_index.TupleLess(0))) != 0)
            {
                hv_value.Dispose();
                hv_value = -1;

                hv_Filepath.Dispose();
                hv_stdparam.Dispose();
                hv_Index0.Dispose();
                hv_screenparam.Dispose();
                hv_Index1.Dispose();
                hv_productparam.Dispose();
                hv_Index2.Dispose();
                hv_Names.Dispose();
                hv_i.Dispose();
                hv_param2.Dispose();
                hv_index.Dispose();
                hv_valueCount.Dispose();
                hv_foundValue.Dispose();
                hv_param.Dispose();
                hv_param3.Dispose();
                hv_index3.Dispose();
                hv_ThreshStdMax.Dispose();
                hv_ThreshStdMin.Dispose();
                hv_ThreshProductMax.Dispose();
                hv_ThreshProductMin.Dispose();
                hv_SizePixelX.Dispose();
                hv_SizePixelY.Dispose();
                hv_ScaleRate.Dispose();
                hv_ScreenPixSize.Dispose();
                hv_writeFileHandle.Dispose();
                hvec_ElementsOut.Dispose();

                return;
            }
            hv_foundValue.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_foundValue = new HTuple(hvec_ElementsOut[hv_index.TupleSelect(
                    0)][1].T);
            }
            if ((int)(hv_foundValue.TupleIsNumber()) != 0)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_ElementsOut[hv_index.TupleSelect(
                        0)][1] = dh.Add(new HTupleVector(hv_ScaleRate));
                }
            }



            //# 写入XML文件

            hv_writeFileHandle.Dispose();
            HOperatorSet.OpenFile(hv_Filepath, "output", out hv_writeFileHandle);
            HOperatorSet.FwriteString(hv_writeFileHandle, "<?xml version=\"1.0\" encoding=\"utf-16\"?>\n");
            HOperatorSet.FwriteString(hv_writeFileHandle, "<XD_PhaseDeflectionPara xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\n");
            HTuple end_val193 = new HTuple(hvec_ElementsOut.Length) - 1;
            HTuple step_val193 = 1;
            for (hv_valueCount = 0; hv_valueCount.Continue(end_val193, step_val193); hv_valueCount = hv_valueCount.TupleAdd(step_val193))
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.FwriteString(hv_writeFileHandle, ((((("\t<" + hvec_ElementsOut[hv_valueCount][0].T) + ">") + hvec_ElementsOut[hv_valueCount][1].T) + "</") + hvec_ElementsOut[hv_valueCount][0].T) + ">\n");
                }
            }
            HOperatorSet.FwriteString(hv_writeFileHandle, "</XD_PhaseDeflectionPara>\n");
            HOperatorSet.ClearHandle(hv_writeFileHandle);
            HOperatorSet.CloseFile(hv_writeFileHandle);

            hv_Filepath.Dispose();
            hv_stdparam.Dispose();
            hv_Index0.Dispose();
            hv_screenparam.Dispose();
            hv_Index1.Dispose();
            hv_productparam.Dispose();
            hv_Index2.Dispose();
            hv_Names.Dispose();
            hv_i.Dispose();
            hv_param2.Dispose();
            hv_index.Dispose();
            hv_valueCount.Dispose();
            hv_foundValue.Dispose();
            hv_param.Dispose();
            hv_param3.Dispose();
            hv_index3.Dispose();
            hv_ThreshStdMax.Dispose();
            hv_ThreshStdMin.Dispose();
            hv_ThreshProductMax.Dispose();
            hv_ThreshProductMin.Dispose();
            hv_SizePixelX.Dispose();
            hv_SizePixelY.Dispose();
            hv_ScaleRate.Dispose();
            hv_ScreenPixSize.Dispose();
            hv_writeFileHandle.Dispose();
            hvec_ElementsOut.Dispose();

            return;
        }







    }
}
