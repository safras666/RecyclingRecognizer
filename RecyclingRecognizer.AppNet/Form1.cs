using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using RecyclingRecognizer.Core;  // твоя библиотека

namespace RecyclingRecognizer.App
{
    public class Form1 : Form
    {
        private Button btnHistory;
        private Label lblTitle;
        private Button btnSettings;
        private PictureBox contentArea;
        private Button btnActionLeft;
        private Button btnActionMain;
        private Label resultLabel;
        private OpenFileDialog openFileDialog;

        public Form1()
        {
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "ЭкоПомощник";
            this.ClientSize = new Size(412, 917);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.Black;

            // Верхняя панель
            Panel topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 75,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            btnHistory = new Button
            {
                Name = "btnHistory",
                Text = "📋",
                Font = new Font("Segoe UI", 18),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Size = new Size(45, 45),
                Location = new Point(15, 15)
            };
            btnHistory.FlatAppearance.BorderSize = 0;
            btnHistory.Click += BtnHistory_Click;

            lblTitle = new Label
            {
                Text = "ЭкоПомощник",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                AutoSize = true
            };
            topPanel.Resize += (s, e) =>
                lblTitle.Location = new Point((topPanel.Width - lblTitle.Width) / 2, 20);
            lblTitle.Location = new Point((topPanel.Width - lblTitle.Width) / 2, 20);

            btnSettings = new Button
            {
                Name = "btnSettings",
                Text = "⚙️",
                Font = new Font("Segoe UI", 18),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                Size = new Size(45, 45)
            };
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.Click += BtnSettings_Click;
            topPanel.Resize += (s, e) =>
                btnSettings.Location = new Point(topPanel.Width - 60, 15);
            btnSettings.Location = new Point(topPanel.Width - 60, 15);

            topPanel.Controls.Add(btnHistory);
            topPanel.Controls.Add(lblTitle);
            topPanel.Controls.Add(btnSettings);

            // Центральная область (чёрная)
            contentArea = new PictureBox
            {
                Name = "contentArea",
                Dock = DockStyle.Fill,
                BackColor = Color.Black,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Нижняя панель
            Panel bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = Color.FromArgb(230, 230, 230)
            };

            btnActionLeft = new Button
            {
                Name = "btnActionLeft",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Size = new Size(45, 45),
                Text = "●",
                Font = new Font("Segoe UI", 20)
            };
            MakeRoundButton(btnActionLeft);
            btnActionLeft.FlatAppearance.BorderSize = 0;
            btnActionLeft.Click += BtnActionLeft_Click;
            bottomPanel.Resize += (s, e) =>
                btnActionLeft.Location = new Point(30, (bottomPanel.Height - 45) / 2);
            btnActionLeft.Location = new Point(30, (bottomPanel.Height - 45) / 2);

            btnActionMain = new Button
            {
                Name = "btnActionMain",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(200, 200, 200),
                Size = new Size(90, 90),
                Text = "📷",
                Font = new Font("Segoe UI", 32)
            };
            MakeRoundButton(btnActionMain);
            btnActionMain.FlatAppearance.BorderSize = 0;
            btnActionMain.Click += BtnActionMain_Click;
            bottomPanel.Resize += (s, e) =>
                btnActionMain.Location = new Point((bottomPanel.Width - 90) / 2, (bottomPanel.Height - 90) / 2);
            btnActionMain.Location = new Point((bottomPanel.Width - 90) / 2, (bottomPanel.Height - 90) / 2);

            bottomPanel.Controls.Add(btnActionLeft);
            bottomPanel.Controls.Add(btnActionMain);

            // Метка для результата
            resultLabel = new Label
            {
                Name = "resultLabel",
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = ""
            };

            // Диалог выбора файла
            openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Выберите фото упаковки"
            };

            this.Controls.Add(contentArea);
            this.Controls.Add(resultLabel);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(topPanel);
        }

        private void MakeRoundButton(Button btn)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, btn.Width, btn.Height);
            btn.Region = new Region(path);
        }

        // Обработчик главной кнопки (камера / загрузка фото)
        private void BtnActionMain_Click(object sender, EventArgs e)
        {
            // Для целей UI-тестирования просто эмулируем успешное распознавание
            resultLabel.Text = "Материал: PET\nПереработка: Да";
        }

        // Остальные обработчики (пока пустые, можно добавить заглушки)
        private void BtnHistory_Click(object sender, EventArgs e)
        {
            resultLabel.Text = "История (в разработке)";
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            resultLabel.Text = "Настройки (в разработке)";
        }

        private void BtnActionLeft_Click(object sender, EventArgs e)
        {
            resultLabel.Text = "Доп. действие";
        }
    }
}