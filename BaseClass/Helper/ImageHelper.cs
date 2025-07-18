using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaVision.BaseClass.Helper;

    internal class ImageHelper
    {
        // 支持的图片扩展名列表（不区分大小写）

        public static readonly HashSet<string> SupportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp"
        };
    }

