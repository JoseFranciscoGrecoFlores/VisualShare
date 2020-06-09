using System.Collections.Generic;

namespace VisualShare.Shared
{
    public static class ExtensionMethods
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

        public static bool IsExtensionImage(this string extension) => ImageExtensions.Contains(extension.ToUpperInvariant());
        public static bool IsExtensionVideo(this string extension) => !ImageExtensions.Contains(extension.ToUpperInvariant());
    }
}