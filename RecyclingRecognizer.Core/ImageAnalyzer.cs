using System;
using System.IO;
using System.Linq;

namespace RecyclingRecognizer.Core
{
    public class ImageAnalyzer
    {
        private readonly IRecognitionEngine _recognitionEngine;

        public ImageAnalyzer(IRecognitionEngine recognitionEngine)
        {
            _recognitionEngine = recognitionEngine;
        }

        public AnalysisResult Analyze(string imagePath)
        {
            // 1. Проверка на null
            if (imagePath == null)
                throw new ArgumentNullException(nameof(imagePath));

            // 2. Проверка на пустую строку или только пробелы
            if (string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Путь к изображению не может быть пустым", nameof(imagePath));

            // 3. Проверка на недопустимые символы в пути
            char[] invalidChars = Path.GetInvalidPathChars();
            if (imagePath.IndexOfAny(invalidChars) != -1)
                throw new ArgumentException("Путь содержит недопустимые символы", nameof(imagePath));

            // 4. Проверка на валидность ссылки (простейшая — наличие расширения .png/.jpg и т.п.)
            string extension = Path.GetExtension(imagePath).ToLower();
            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
                throw new ArgumentException("Файл должен иметь расширение .png, .jpg или .jpeg", nameof(imagePath));

            return _recognitionEngine.Recognize(imagePath);
        }
    }
}