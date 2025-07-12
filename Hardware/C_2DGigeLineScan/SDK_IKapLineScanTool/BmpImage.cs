using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace NovaVision.Hardware.C_2DGigeLineScan.SDK_IKapLineScanTool
{
    internal class BmpImage
    {
        private bool bCompletion = false;

        private ColorPalette m_palette = null;

        private Bitmap m_bitmap = null;

        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

        public bool CreateImage(int nWidth, int nHeight, int nDepth, int nChannels)
        {
            PixelFormat nPixelFromat = PixelFormat.Undefined;
            switch (nChannels)
            {
                case 1:
                    nPixelFromat = ((nDepth != 8) ? PixelFormat.Format16bppGrayScale : PixelFormat.Format8bppIndexed);
                    break;
                case 3:
                    nPixelFromat = ((nDepth != 8) ? PixelFormat.Format48bppRgb : PixelFormat.Format24bppRgb);
                    break;
                case 4:
                    nPixelFromat = ((nDepth != 8) ? PixelFormat.Format64bppArgb : PixelFormat.Format32bppPArgb);
                    break;
            }
            m_bitmap = new Bitmap(nWidth, nHeight, nPixelFromat);
            if (nPixelFromat == PixelFormat.Format8bppIndexed)
            {
                m_palette = m_bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    m_palette.Entries[i] = Color.FromArgb(i, i, i);
                }
                m_bitmap.Palette = m_palette;
            }
            int nStep = nChannels * nWidth * nDepth / 8;
            if (nStep % 4 == 0)
            {
                bCompletion = false;
            }
            else
            {
                bCompletion = true;
            }
            return true;
        }

        public bool ReleaseImage()
        {
            if (m_bitmap != null)
            {
                m_bitmap.Dispose();
            }
            m_bitmap = null;
            return true;
        }

        public Bitmap WriteImageData(IntPtr imageData, int imageDataLen)
        {
            if (m_bitmap == null)
            {
                return null;
            }
            Rectangle rect = new Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height);
            BitmapData bitmapData = m_bitmap.LockBits(rect, ImageLockMode.ReadWrite, m_bitmap.PixelFormat);
            int nStribe = imageDataLen / m_bitmap.Height;
            if (!bCompletion)
            {
                IntPtr iptr = bitmapData.Scan0;
                CopyMemory(iptr, imageData, imageDataLen);
            }
            else
            {
                for (int i = 0; i < m_bitmap.Height; i++)
                {
                    IntPtr iptrDst = bitmapData.Scan0 + bitmapData.Stride * i;
                    IntPtr iptrSrc = imageData + nStribe * i;
                    CopyMemory(iptrDst, iptrSrc, nStribe);
                }
            }
            m_bitmap.UnlockBits(bitmapData);
            return m_bitmap;
        }
    }
}
