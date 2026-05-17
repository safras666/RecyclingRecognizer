using NUnit.Framework;
using RecyclingRecognizer.Core;
using System;

namespace RecyclingRecognizer.Tests
{
    [TestFixture]
    public class ImageAnalyzerTests
    {
        private ImageAnalyzer _analyzer;

        [SetUp]
        public void Setup()
        {

            var stubLoader = new StubFileLoader();
            _analyzer = new ImageAnalyzer(stubLoader);
        }

        // Тест 001: позитивный сценарий с кодом "1"
        [Test]
        public void T001_Analyze_ValidPhotoWithCode1_ReturnsSuccess()
        {
            string imagePath = "valid_symbol_1.png";
            AnalysisResult result = null;
            Assert.DoesNotThrow(() => { result = _analyzer.Analyze(imagePath); });
            Assert.That(result.Success, Is.True);
            Assert.That(result.SymbolCode, Is.EqualTo("1"));
        }

        // Тест 002: пустой путь — исключение
        [Test]
        public void T002_Analyze_NullPath_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _analyzer.Analyze(null));
        }

        // Тест 003: нет значка на фото
        [Test]
        public void T003_Analyze_PhotoNoSymbol_Failure()
        {
            string imagePath = "no_symbol.png";
            var result = _analyzer.Analyze(imagePath);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Значок не найден"));
        }

        // Тест 004: значок есть, но код нечитаем
        [Test]
        public void T004_Analyze_PhotoUnreadableCode_Failure()
        {
            string imagePath = "unreadable_code.png";
            var result = _analyzer.Analyze(imagePath);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Не удалось определить код значка"));
        }

        // Тест 005: повреждённый файл — исключение
        [Test]
        public void T005_Analyze_CorruptedPhoto_ThrowsInvalidOperationException()
        {
            string imagePath = "corrupted.jpg";
            Assert.Throws<InvalidOperationException>(() => _analyzer.Analyze(imagePath));
        }

        // Тест 006: множественные данные (TestCase)
        [TestCase("valid_symbol_1.png", "1")]
        [TestCase("valid_symbol_2.png", "2")]
        [TestCase("valid_symbol_3.png", "3")]
        [TestCase("valid_symbol_5.png", "5")]
        public void T006_Analyze_ValidPhotos_ReturnsCorrectCode(string imagePath, string expectedCode)
        {
            var result = _analyzer.Analyze(imagePath);
            Assert.That(result.Success, Is.True);
            Assert.That(result.SymbolCode, Is.EqualTo(expectedCode));
        }

        // Тест 007
        [Test]
        public void Analyze_StubLoaderCanBeInjected_DoesNotThrow()
        {
            var stubLoader = new StubFileLoader();
            var analyzer = new ImageAnalyzer(stubLoader);
            Assert.DoesNotThrow(() => analyzer.Analyze("valid_symbol_1.png"));
        }
    }
}