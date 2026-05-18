using System;
using System.Collections.Generic;
using System.Text;

namespace RecyclingRecognizer.Core
{
    public interface IRecognitionEngine
    {
        AnalysisResult Recognize(string imagePath);
    }
}
