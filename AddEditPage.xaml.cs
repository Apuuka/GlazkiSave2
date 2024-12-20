﻿using GlazkiSave.res;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GlazkiSave
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        public AddEditPage()
        {
            InitializeComponent();
        }

        private Agent _currentAgent = new Agent();
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();
            if (SelectedAgent != null)
                _currentAgent = SelectedAgent;
            DataContext = _currentAgent;
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                _currentAgent.Logo = myOpenFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");

            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");

            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
                errors.AppendLine("Укажите ФИО директора");

            if (ComboType.SelectedIndex == -1)
                errors.AppendLine("Укажите тип агента");

            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");

            if (_currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");

            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");

            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");

            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");

            else
            {
                string ph = _currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11)
                    || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно телефон агента");
            }

            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
                errors.AppendLine("Укажите почту агента");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentAgent.AgentTypeID = ComboType.SelectedIndex;
            //добавить в контекст текущие значения новой услуги
            if (_currentAgent.ID == 0)
                Bikbulatov_GlazkiEntities.GetContext().Agent.Add(_currentAgent);
            //сохранить изменения, если никаких ошибок не получилось при этом
            try
            {
                Bikbulatov_GlazkiEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }




        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            var currentClientService = Bikbulatov_GlazkiEntities.GetContext().ProductSale.ToList();
            currentClientService = currentClientService.Where(p => p.AgentID == _currentAgent.ID).ToList();

            if (currentClientService.Count != 0)
                MessageBox.Show("Невозможно удалить");
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Bikbulatov_GlazkiEntities.GetContext().Agent.Remove(_currentAgent);
                        Bikbulatov_GlazkiEntities.GetContext().SaveChanges();
                        Manager.MainFrame.GoBack();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                }
            }
        }
        private void RealizeHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            HistorySales window = new HistorySales(_currentAgent);
            window.ShowDialog();
        }
    }
}
