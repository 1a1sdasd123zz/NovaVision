using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro3D;

namespace NovaVision.Hardware._011_SDK_SSZN3DTool
{
    public class SSZNImageData
    {
        public static CogImage16Range LoadSSZN3DImage(string fileName, int missingPixelvalue = -10000000)
        {
            CogImage16Range cogImage16Range = null;
            try
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("图片文件不存在！文件名：" + fileName, "加载图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                cogImage16Range = LoadSSZN3DImage(fs, missingPixelvalue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载SSZN图片错误!\r\nDetail:" + ex.Message, "加载图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return cogImage16Range;
        }

        public static CogImage16Range LoadSSZN3DImage(Stream stream, int missingPixelvalue = -10000000)
        {
            CogImage16Range cogImage16Range = null;
            try
            {
                byte[] buffer;
                using (Stream fs = stream)
                {
                    buffer = new byte[fs.Length];
                    long rem = fs.Length;
                    int num = 0;
                    int i = 0;
                    while (i < fs.Length)
                    {
                        num = ((rem <= int.MaxValue) ? fs.Read(buffer, 0, (int)rem) : fs.Read(buffer, 0, int.MaxValue));
                        i += num;
                        rem -= num;
                    }
                    fs.Close();
                }
                cogImage16Range = LoadSSZN3DImage(buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换SSZN图片错误!\r\nDetail:" + ex.Message, "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return cogImage16Range;
        }

        public unsafe static CogImage16Range LoadSSZN3DImage(byte[] buffer, int missingPixelvalue = -10000000)
        {
            CogImage16Range cogImage16Range = null;
            try
            {
                if (buffer.Length < 10240)
                {
                    MessageBox.Show($@"数据长度不够文件头长度:
Length = {buffer.Length}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                int[] wh = new int[2];
                double[] scales = new double[2];
                try
                {
                    fixed (byte* p = &buffer[0])
                    {
                        IntPtr ptr = new IntPtr(p);
                        Marshal.Copy(ptr + 4, wh, 0, 2);
                        Marshal.Copy(ptr + 16, scales, 0, 2);
                        double scaleX = scales[0];
                        double scaleY = scales[1];
                        int width = wh[0];
                        int height = wh[1];
                        int[] rawData = new int[width * height];
                        if (buffer.Length < 10240 + width * height * 4)
                        {
                            MessageBox.Show($@"数据长度不够:
Length = {buffer.Length}
width = {width}
height = {height}
应该长度 = {10240 + width * height * 4}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        Marshal.Copy(ptr + 10240, rawData, 0, width * height);
                        int max = 0;
                        int min = 0;
                        calUpperAndLower(rawData, height, width, ref max, ref min);
                        if (min >= max)
                        {
                            max = 100000000;
                            min = -100000000;
                        }
                        double max_mm = (double)max * 1E-05;
                        double min_mm = (double)min * 1E-05;
                        double scaleZ = (max_mm - min_mm) / 65534.0;
                        if (scaleZ <= 0.0 || scaleX <= 0.0 || scaleY <= 0.0)
                        {
                            MessageBox.Show(string.Format("缩放比例小于或等于0!\r\nscaleX = {0}\r\nscaleY = {0}\r\nscaleZ = {0}", scaleX, scaleY, scaleZ), "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return null;
                        }
                        CogImage8Grey maskImage = new CogImage8Grey(width, height);
                        Cognex.VisionPro.ICogImage8PixelMemory cogImage8PixelMemory = maskImage.Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height);
                        byte* grey8_Scan0 = (byte*)(void*)cogImage8PixelMemory.Scan0;
                        int grey8_Stride = cogImage8PixelMemory.Stride;
                        CogImage16Grey cogImage16Grey = new CogImage16Grey(width, height);
                        CogTransform2DLinear transform2Dlinear = new CogTransform2DLinear();
                        transform2Dlinear.SetScalingsRotationsTranslation(1.0 / scaleX, 1.0 / scaleY, Math.PI * 2.0, Math.PI * 2.0, 0.0, 0.0);
                        cogImage16Grey.CoordinateSpaceTree.AddSpace("@", "Sensor2D", transform2Dlinear, copyTransform: true, CogAddSpaceConstants.ReplaceDuplicate);
                        Cog3DMatrix3x3 m3DMatrix3x3 = new Cog3DMatrix3x3(1.0 / scaleX, 0.0, 0.0, 0.0, 1.0 / scaleY, 0.0, 0.0, 0.0, 1.0 / scaleZ);
                        Cog3DVect3 m3DVect3 = new Cog3DVect3(1.0, 1.0, 1.0);
                        Cog3DTransformLinear transform = new Cog3DTransformLinear(m3DMatrix3x3, m3DVect3);
                        cogImage16Range = new CogImage16Range(cogImage16Grey, maskImage, transform);
                        CogImage16Grey grey16Img = cogImage16Range.GetPixelData();
                        Cognex.VisionPro.ICogImage16PixelMemory mImg16PixelMemory = cogImage16Grey.Get16GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height);
                        ushort* grey16_Scan0 = (ushort*)(void*)mImg16PixelMemory.Scan0;
                        int grey16_Stride = mImg16PixelMemory.Stride;
                        bool visible = true;
                        for (int i = 0; i < height; i++)
                        {
                            byte* tmpGrey17 = grey8_Scan0;
                            ushort* tmpGrey16 = grey16_Scan0;
                            for (int j = 0; j < width; j++)
                            {
                                visible = rawData[i * width + j] > missingPixelvalue;
                                *tmpGrey17 = (byte)(visible ? byte.MaxValue : 0);
                                *tmpGrey16 = (ushort)(visible ? ((ushort)(((double)rawData[i * width + j] * 1E-05 - min_mm) / scaleZ + 1.0)) : 0);
                                tmpGrey17++;
                                tmpGrey16++;
                            }
                            grey8_Scan0 += grey8_Stride;
                            grey16_Scan0 += grey16_Stride;
                        }
                        cogImage8PixelMemory.Dispose();
                        mImg16PixelMemory.Dispose();
                    }
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换SSZN图片错误!\r\nDetail:" + ex.Message, "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return cogImage16Range;
        }

        public unsafe static CogImage16Range LoadSSZN3DImage(int[] data, int width, int height, double xInterval, double yInterval, int missingPixelvalue = -10000000)
        {
            CogImage16Range cogImage16Range = null;
            try
            {
                if (data == null)
                {
                    MessageBox.Show(string.Format("数据为空:\r\ndata=null!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (width <= 0)
                {
                    MessageBox.Show(string.Format("数据宽度错误:\r\nwidth<=0!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (height <= 0)
                {
                    MessageBox.Show(string.Format("数据长度错误:\r\nheight<=0!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (data.Length != width * height)
                {
                    MessageBox.Show(string.Format("数据长度与数据量不等:\r\ndata.Length != width*height!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (xInterval <= 0.0)
                {
                    MessageBox.Show(string.Format("数据X间距错误:\r\nxInternal<=0!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (yInterval <= 0.0)
                {
                    MessageBox.Show(string.Format("数据Y间距错误:\r\nyInternal<=0!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                int max = 0;
                int min = 0;
                calUpperAndLower(data, height, width, ref max, ref min);
                if (min >= max)
                {
                    max = 100000000;
                    min = -100000000;
                }
                double max_mm = (double)max * 1E-05;
                double min_mm = (double)min * 1E-05;
                double scaleZ = (max_mm - min_mm) / 65534.0;
                if (scaleZ <= 0.0)
                {
                    MessageBox.Show(string.Format("缩放比例小于或等于0!\r\nscaleX = {0}", "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                CogImage8Grey maskImage = new CogImage8Grey(width, height);
                Cognex.VisionPro.ICogImage8PixelMemory cogImage8PixelMemory = maskImage.Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height);
                byte* grey8_Scan0 = (byte*)(void*)cogImage8PixelMemory.Scan0;
                int grey8_Stride = cogImage8PixelMemory.Stride;
                CogImage16Grey cogImage16Grey = new CogImage16Grey(width, height);
                CogTransform2DLinear transform2Dlinear = new CogTransform2DLinear();
                transform2Dlinear.SetScalingsRotationsTranslation(1.0 / xInterval, 1.0 / yInterval, Math.PI * 2.0, Math.PI * 2.0, 0.0, 0.0);
                cogImage16Grey.CoordinateSpaceTree.AddSpace("@", "Sensor2D", transform2Dlinear, copyTransform: true, CogAddSpaceConstants.ReplaceDuplicate);
                Cog3DMatrix3x3 m3DMatrix3x3 = new Cog3DMatrix3x3(1.0 / xInterval, 0.0, 0.0, 0.0, 1.0 / yInterval, 0.0, 0.0, 0.0, 1.0 / scaleZ);
                Cog3DVect3 m3DVect3 = new Cog3DVect3(1.0, 1.0, 1.0);
                Cog3DTransformLinear transform = new Cog3DTransformLinear(m3DMatrix3x3, m3DVect3);
                cogImage16Range = new CogImage16Range(cogImage16Grey, maskImage, transform);
                CogImage16Grey grey16Img = cogImage16Range.GetPixelData();
                Cognex.VisionPro.ICogImage16PixelMemory mImg16PixelMemory = cogImage16Grey.Get16GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height);
                ushort* grey16_Scan0 = (ushort*)(void*)mImg16PixelMemory.Scan0;
                int grey16_Stride = mImg16PixelMemory.Stride;
                bool visible = true;
                for (int i = 0; i < height; i++)
                {
                    byte* tmpGrey17 = grey8_Scan0;
                    ushort* tmpGrey16 = grey16_Scan0;
                    for (int j = 0; j < width; j++)
                    {
                        visible = data[i * width + j] > missingPixelvalue && (double)data[i * width + j] * 1E-05 <= max_mm && (double)data[i * width + j] * 1E-05 >= min_mm;
                        *tmpGrey17 = (byte)(visible ? byte.MaxValue : 0);
                        *tmpGrey16 = (ushort)(visible ? ((ushort)(((double)data[i * width + j] * 1E-05 - min_mm) / scaleZ + 1.0)) : 0);
                        tmpGrey17++;
                        tmpGrey16++;
                    }
                    grey8_Scan0 += grey8_Stride;
                    grey16_Scan0 += grey16_Stride;
                }
                cogImage8PixelMemory.Dispose();
                mImg16PixelMemory.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换SSZN图片错误!\r\nDetail:" + ex.Message, "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return cogImage16Range;
        }

        public static CogImage16Range LoadSSZN3DImage(string fileName, double min_mm, double max_mm, int missingPixelvalue = -10000000)
        {
            CogImage16Range cogImage16Range = null;
            try
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("图片文件不存在！文件名：" + fileName, "加载图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                cogImage16Range = LoadSSZN3DImage(fs, min_mm, max_mm, missingPixelvalue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载SSZN图片错误!\r\nDetail:" + ex.Message, "加载图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return cogImage16Range;
        }

        public static CogImage16Range LoadSSZN3DImage(Stream stream, double min_mm, double max_mm, int missingPixelvalue = -10000000)
        {
            CogImage16Range cogImage16Range = null;
            try
            {
                if (min_mm >= max_mm)
                {
                    MessageBox.Show($@"参数错误:
min_mm>=max_mm
min_mm = {min_mm}
max_mm = {max_mm}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                if (min_mm <= (double)missingPixelvalue)
                {
                    MessageBox.Show($@"参数错误:
min_mm <= missingPixelvalue_mm
min_mm = {min_mm}
missingPixelvalue = {missingPixelvalue}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                byte[] buffer;
                using (Stream fs = stream)
                {
                    buffer = new byte[fs.Length];
                    long rem = fs.Length;
                    int num = 0;
                    int i = 0;
                    while (i < fs.Length)
                    {
                        num = ((rem <= int.MaxValue) ? fs.Read(buffer, 0, (int)rem) : fs.Read(buffer, 0, int.MaxValue));
                        i += num;
                        rem -= num;
                    }
                    fs.Close();
                }
                cogImage16Range = LoadSSZN3DImage(buffer, min_mm, max_mm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换SSZN图片错误!\r\nDetail:" + ex.Message, "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return cogImage16Range;
        }

        public unsafe static CogImage16Range LoadSSZN3DImage(byte[] buffer, double min_mm, double max_mm, int missingPixelvalue = -10000000)
        {
            CogImage16Range cogImage16Range = null;
            try
            {
                if (min_mm >= max_mm)
                {
                    MessageBox.Show($@"参数错误:
min_mm>=max_mm
min_mm = {min_mm}
max_mm = {max_mm}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                if (min_mm <= (double)missingPixelvalue)
                {
                    MessageBox.Show($@"参数错误:
min_mm <= missingPixelvalue_mm
min_mm = {min_mm}
missingPixelvalue = {missingPixelvalue}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                if (buffer.Length < 10240)
                {
                    MessageBox.Show($@"数据长度不够文件头长度:
Length = {buffer.Length}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                int[] wh = new int[2];
                double[] scales = new double[2];
                try
                {
                    fixed (byte* p = &buffer[0])
                    {
                        IntPtr ptr = new IntPtr(p);
                        Marshal.Copy(ptr + 4, wh, 0, 2);
                        Marshal.Copy(ptr + 16, scales, 0, 2);
                        double scaleX = scales[0];
                        double scaleY = scales[1];
                        int width = wh[0];
                        int height = wh[1];
                        int[] rawData = new int[width * height];
                        if (buffer.Length < 10240 + width * height * 4)
                        {
                            MessageBox.Show($@"数据长度不够:
Length = {buffer.Length}
width = {width}
height = {height}
应该长度 = {10240 + width * height * 4}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        Marshal.Copy(ptr + 10240, rawData, 0, width * height);
                        double scaleZ = (max_mm - min_mm) / 65534.0;
                        if (scaleZ <= 0.0 || scaleX <= 0.0 || scaleY <= 0.0)
                        {
                            MessageBox.Show(string.Format("缩放比例小于或等于0!\r\nscaleX = {0}\r\nscaleY = {0}\r\nscaleZ = {0}", scaleX, scaleY, scaleZ), "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return null;
                        }
                        CogImage8Grey maskImage = new CogImage8Grey(width, height);
                        Cognex.VisionPro.ICogImage8PixelMemory cogImage8PixelMemory = maskImage.Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height);
                        byte* grey8_Scan0 = (byte*)(void*)cogImage8PixelMemory.Scan0;
                        int grey8_Stride = cogImage8PixelMemory.Stride;
                        CogImage16Grey cogImage16Grey = new CogImage16Grey(width, height);
                        Cog3DMatrix3x3 m3DMatrix3x3 = new Cog3DMatrix3x3(1.0 / scaleX, 0.0, 0.0, 0.0, 1.0 / scaleY, 0.0, 0.0, 0.0, 1.0 / scaleZ);
                        Cog3DVect3 m3DVect3 = new Cog3DVect3(1.0, 1.0, 1.0);
                        Cog3DTransformLinear transform = new Cog3DTransformLinear(m3DMatrix3x3, m3DVect3);
                        cogImage16Range = new CogImage16Range(cogImage16Grey, maskImage, transform);
                        CogImage16Grey grey16Img = cogImage16Range.GetPixelData();
                        Cognex.VisionPro.ICogImage16PixelMemory mImg16PixelMemory = cogImage16Grey.Get16GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height);
                        ushort* grey16_Scan0 = (ushort*)(void*)mImg16PixelMemory.Scan0;
                        int grey16_Stride = mImg16PixelMemory.Stride;
                        bool visible = true;
                        for (int i = 0; i < height; i++)
                        {
                            byte* tmpGrey17 = grey8_Scan0;
                            ushort* tmpGrey16 = grey16_Scan0;
                            for (int j = 0; j < width; j++)
                            {
                                visible = rawData[i * width + j] > missingPixelvalue && (double)rawData[i * width + j] * 1E-05 <= max_mm && (double)rawData[i * width + j] * 1E-05 >= min_mm;
                                *tmpGrey17 = (byte)(visible ? byte.MaxValue : 0);
                                *tmpGrey16 = (ushort)(visible ? ((ushort)(((double)rawData[i * width + j] * 1E-05 - min_mm) / scaleZ + 1.0)) : 0);
                                tmpGrey17++;
                                tmpGrey16++;
                            }
                            grey8_Scan0 += grey8_Stride;
                            grey16_Scan0 += grey16_Stride;
                        }
                        cogImage8PixelMemory.Dispose();
                        mImg16PixelMemory.Dispose();
                    }
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换SSZN图片错误!\r\nDetail:" + ex.Message, "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return cogImage16Range;
        }

        public unsafe static CogImage16Range LoadSSZN3DImage(int[] data, int width, int height, double xInterval, double yInterval, double min_mm, double max_mm, int missingPixelvalue = -10000000)
        {
            CogImage16Range cogImage16Range = null;
            try
            {
                if (data == null)
                {
                    MessageBox.Show(string.Format("数据为空:\r\ndata=null!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (width <= 0)
                {
                    MessageBox.Show(string.Format("数据宽度错误:\r\nwidth<=0!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (height <= 0)
                {
                    MessageBox.Show(string.Format("数据长度错误:\r\nheight<=0!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (data.Length != width * height)
                {
                    MessageBox.Show(string.Format("数据长度与数据量不等:\r\ndata.Length != width*height!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (xInterval <= 0.0)
                {
                    MessageBox.Show(string.Format("数据X间距错误:\r\nxInternal<=0!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (yInterval <= 0.0)
                {
                    MessageBox.Show(string.Format("数据Y间距错误:\r\nyInternal<=0!", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                if (min_mm >= max_mm)
                {
                    MessageBox.Show($@"参数错误:
min_mm>=max_mm
min_mm = {min_mm}
max_mm = {max_mm}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                if (min_mm <= (double)missingPixelvalue)
                {
                    MessageBox.Show($@"参数错误:
min_mm <= missingPixelvalue_mm
min_mm = {min_mm}
missingPixelvalue = {missingPixelvalue}", "图像转换参数错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return null;
                }
                int[] wh = new int[2];
                double scaleZ = (max_mm - min_mm) / 65534.0;
                if (scaleZ <= 0.0)
                {
                    MessageBox.Show(string.Format("缩放比例小于或等于0!\r\nscaleX = {0}", "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand));
                    return null;
                }
                CogImage8Grey maskImage = new CogImage8Grey(width, height);
                Cognex.VisionPro.ICogImage8PixelMemory cogImage8PixelMemory = maskImage.Get8GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height);
                byte* grey8_Scan0 = (byte*)(void*)cogImage8PixelMemory.Scan0;
                int grey8_Stride = cogImage8PixelMemory.Stride;
                CogImage16Grey cogImage16Grey = new CogImage16Grey(width, height);
                CogTransform2DLinear transform2Dlinear = new CogTransform2DLinear();
                transform2Dlinear.SetScalingsRotationsTranslation(1.0 / xInterval, 1.0 / yInterval, Math.PI * 2.0, Math.PI * 2.0, 0.0, 0.0);
                cogImage16Grey.CoordinateSpaceTree.AddSpace("@", "Sensor2D", transform2Dlinear, copyTransform: true, CogAddSpaceConstants.ReplaceDuplicate);
                Cog3DMatrix3x3 m3DMatrix3x3 = new Cog3DMatrix3x3(1.0 / xInterval, 0.0, 0.0, 0.0, 1.0 / yInterval, 0.0, 0.0, 0.0, 1.0 / scaleZ);
                Cog3DVect3 m3DVect3 = new Cog3DVect3(1.0, 1.0, 1.0);
                Cog3DTransformLinear transform = new Cog3DTransformLinear(m3DMatrix3x3, m3DVect3);
                cogImage16Range = new CogImage16Range(cogImage16Grey, maskImage, transform);
                CogImage16Grey grey16Img = cogImage16Range.GetPixelData();
                Cognex.VisionPro.ICogImage16PixelMemory mImg16PixelMemory = cogImage16Grey.Get16GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height);
                ushort* grey16_Scan0 = (ushort*)(void*)mImg16PixelMemory.Scan0;
                int grey16_Stride = mImg16PixelMemory.Stride;
                bool visible = true;
                for (int i = 0; i < height; i++)
                {
                    byte* tmpGrey17 = grey8_Scan0;
                    ushort* tmpGrey16 = grey16_Scan0;
                    for (int j = 0; j < width; j++)
                    {
                        visible = data[i * width + j] > missingPixelvalue && (double)data[i * width + j] * 1E-05 <= max_mm && (double)data[i * width + j] * 1E-05 >= min_mm;
                        *tmpGrey17 = (byte)(visible ? byte.MaxValue : 0);
                        *tmpGrey16 = (ushort)(visible ? ((ushort)(((double)data[i * width + j] * 1E-05 - min_mm) / scaleZ + 1.0)) : 0);
                        tmpGrey17++;
                        tmpGrey16++;
                    }
                    grey8_Scan0 += grey8_Stride;
                    grey16_Scan0 += grey16_Stride;
                }
                cogImage8PixelMemory.Dispose();
                mImg16PixelMemory.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("转换SSZN图片错误!\r\nDetail:" + ex.Message, "转换图片错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return cogImage16Range;
        }

        private static void calUpperAndLower(int[] points, int height, int width, ref int upper, ref int lower)
        {
            int radio = 100000;
            lower = 100 * radio;
            upper = -100 * radio;
            for (int i = 0; i < height * width; i++)
            {
                if (points[i] > -99 * radio && points[i] < 99 * radio)
                {
                    if (points[i] < lower)
                    {
                        lower = points[i];
                    }
                    if (points[i] > upper)
                    {
                        upper = points[i];
                    }
                }
            }
        }
    }
}
