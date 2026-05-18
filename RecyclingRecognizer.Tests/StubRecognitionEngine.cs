using System;
using RecyclingRecognizer.Core;

namespace RecyclingRecognizer.Tests
{
    public class StubRecognitionEngine : IRecognitionEngine
    {
        public AnalysisResult Recognize(string imagePath)
        {
            if (imagePath.Contains("corrupted"))
                throw new InvalidOperationException("Изображение повреждено");

            if (imagePath.Contains("no_symbol"))
            {
                return new AnalysisResult
                {
                    Success = false,
                    SymbolCode = "",
                    Message = "Значок не найден"
                };
            }

            if (imagePath.Contains("unreadable"))
            {
                return new AnalysisResult
                {
                    Success = false,
                    SymbolCode = "",
                    Message = "Не удалось определить код значка"
                };
            }

            if (imagePath.Contains("valid_symbol_"))
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                string[] parts = fileName.Split('_');
                string code = parts[parts.Length - 1];
                return new AnalysisResult
                {
                    Success = true,
                    SymbolCode = code,
                    Message = "Распознано успешно"
                };
            }

            return new AnalysisResult
            {
                Success = false,
                SymbolCode = "",
                Message = "Значок не найден"
            };
        }
    }
}