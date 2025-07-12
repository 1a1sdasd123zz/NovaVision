using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using NvtLvmSdk;

namespace NovaVision.Hardware
{
    internal static class CSVOperator
    {
        public static void SaveCSV(List<int[]> receiveBuffer, string savePath, long width, long higth)
        {
            FileInfo fi = new FileInfo(savePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            StreamWriter sw = new StreamWriter(savePath, append: false, Encoding.ASCII);
            string data = "";
            for (int i = 0; i < higth; i++)
            {
                data = "";
                for (int j = 0; j < width; j++)
                {
                    data = data + receiveBuffer[i][j].ToString("f4") + ",";
                }
                data += "\r\n";
                sw.Write(data);
            }
            sw.Dispose();
            sw.Close();
        }

        public static void SaveCSV(IntPtr profileBuffer, string savePath, int width, int hight)
        {
            int length = width * hight;
            short[] profileData = new short[length];
            Marshal.Copy(profileBuffer, profileData, 0, length);
            short[,] receiveBuffer = new short[width, hight];
            for (int j = 0; j < width; j++)
            {
                for (int k = 0; k < hight; k++)
                {
                    receiveBuffer[j, k] = profileData[k * width + j];
                }
            }
            FileInfo fi = new FileInfo(savePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            StreamWriter sw = new StreamWriter(savePath, append: false, Encoding.ASCII);
            string data = "";
            for (int i = 0; i < hight; i++)
            {
                data = "";
                for (int l = 0; l < width - 1; l++)
                {
                    data = data + receiveBuffer[l, i].ToString("f4") + ",";
                }
                data = data + receiveBuffer[width - 1, i].ToString("f4") + "\r\n";
                sw.Write(data);
            }
            sw.Dispose();
            sw.Close();
        }

        public static void SaveCSV(double[,] laserData, string savePath, int width, int height)
        {
            FileInfo fi = new FileInfo(savePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            StreamWriter sw = new StreamWriter(savePath, append: false, Encoding.ASCII);
            string data = "";
            for (int i = 0; i < height; i++)
            {
                data = "";
                for (int j = 0; j < width - 1; j++)
                {
                    data = data + laserData[i, j].ToString("f4") + ",";
                }
                data = data + laserData[i, width - 1].ToString("f4") + "\r\n";
                sw.Write(data);
            }
            sw.Dispose();
            sw.Close();
        }

        public static double[,] GetPointCloudDataKeyence(IntPtr profileBuffer, int width, int height, DataContext dataContext, short z_offset_pixel = short.MinValue)
        {
            int length = width * height;
            byte[] byteArray = new byte[2 * length];
            ushort[] profileData = new ushort[length];
            Marshal.Copy(profileBuffer, byteArray, 0, 2 * length);
            for (int j = 0; j < length; j++)
            {
                profileData[j] = BitConverter.ToUInt16(byteArray, 2 * j);
            }
            double[,] receiveBuffer = new double[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int k = 0; k < width; k++)
                {
                    short value = (short)(profileData[i * width + k] + z_offset_pixel);
                    if (value == short.MinValue)
                    {
                        receiveBuffer[i, k] = double.NaN;
                    }
                    else
                    {
                        receiveBuffer[i, k] = (double)value * dataContext.zResolution + dataContext.zOffset;
                    }
                }
            }
            return receiveBuffer;
        }

        public static double[,] GetPointCloudDataLVM(CameraModel.p3d_t[] pointClouds, int width, int height, DataContext dataContext)
        {
            double[,] receiveBuffer = new double[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    float value = pointClouds[i * width + j].z;
                    if (value == 0f)
                    {
                        receiveBuffer[i, j] = double.NaN;
                    }
                    else
                    {
                        receiveBuffer[i, j] = value;
                    }
                }
            }
            return receiveBuffer;
        }
    }
}
