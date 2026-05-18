using System;
using System.Threading;
using System.Windows.Forms;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using NUnit.Framework;

namespace RecyclingRecognizer.UITests
{
    public class MainFormTests
    {
        // Укажите правильные пути!
        private const string AppPath = @"D:\DZ\ТП\лаба3\RecyclingRecognizer\RecyclingRecognizer.AppNet\bin\Debug\net10.0-windows\RecyclingRecognizer.AppNet.exe";
        private const string TestImagePath = @"D:\DZ\ТП\лаба4\kod_dlya_neskolkih_upakovok.png";

        private T WaitForElement<T>(Func<T> getter, int timeoutMs = 5000)
        {
            var retry = Retry.WhileNull(getter, TimeSpan.FromMilliseconds(timeoutMs));
            if (!retry.Success)
                Assert.Fail($"Элемент не найден за {timeoutMs} мс");
            return retry.Result;
        }

        [Test]
        public void T001_Elements_Exist()
        {
            var app = FlaUI.Core.Application.Launch(AppPath);
            using var automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);

            // Проверяем наличие всех элементов (используем AsLabel для PictureBox)
            Assert.That(WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId("btnHistory")).AsButton()), Is.Not.Null);
            Assert.That(WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId("btnSettings")).AsButton()), Is.Not.Null);
            Assert.That(WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId("contentArea")).AsLabel()), Is.Not.Null);
            Assert.That(WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId("btnActionLeft")).AsButton()), Is.Not.Null);
            Assert.That(WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId("btnActionMain")).AsButton()), Is.Not.Null);
            Assert.That(WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId("resultLabel")).AsLabel()), Is.Not.Null);

            app.Close();
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void T002_Recognize_Success()
        {
            var app = FlaUI.Core.Application.Launch(AppPath);
            using var automation = new UIA3Automation();
            var window = app.GetMainWindow(automation);

            var mainButton = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId("btnActionMain")).AsButton());
            var resultLabel = WaitForElement(() => window.FindFirstDescendant(cf => cf.ByAutomationId("resultLabel")).AsLabel());

            mainButton.Click();

            // Ждём, пока текст появится
            var retry = Retry.WhileException(() =>
            {
                var text = resultLabel.Text;
                Assert.That(text, Does.Contain("Материал:"), "Результат не содержит материал");
                Assert.That(text, Does.Contain("Переработка: Да"), "Вердикт не 'Да'");
            }, TimeSpan.FromMilliseconds(5000));

            Assert.That(retry.Success, Is.True, "Результат распознавания не появился или неверен");
            app.Close();
        }

    }
}