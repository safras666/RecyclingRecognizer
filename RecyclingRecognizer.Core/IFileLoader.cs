using System;
using System.Collections.Generic;
using System.Text;

namespace RecyclingRecognizer.Core
{
    public interface IFileLoader
    {
        byte[] LoadImage(string path);
    }
}
