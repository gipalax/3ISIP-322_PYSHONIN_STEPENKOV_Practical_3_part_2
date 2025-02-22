using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhoneCallCost
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(DurationTextBox.Text) || string.IsNullOrWhiteSpace(PriceTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка выбора дня недели
            RadioButton selectedDay = DaySelection.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
            if (selectedDay == null)
            {
                MessageBox.Show("Выберите день недели!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка ввода чисел
            if (!double.TryParse(DurationTextBox.Text, out double duration) || !double.TryParse(PriceTextBox.Text, out double price))
            {
                MessageBox.Show("Введите корректные числовые значения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Вычисление стоимости
            double totalCost = duration * price;

            // Применение скидки за выходной (15%)
            if (selectedDay.Content.ToString() == "Суббота" || selectedDay.Content.ToString() == "Воскресенье")
            {
                totalCost *= 0.85;
            }

            // Применение скидки за длительные звонки (30% после 30-й минуты)
            if (duration > 30)
            {
                double discountedMinutes = duration - 30;
                totalCost -= discountedMinutes * price * 0.30;
            }

            ResultLabel.Content = $"Стоимость: {totalCost:F2} руб.";
        }
    }
}
