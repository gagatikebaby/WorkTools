﻿using System.Windows;
using WorkToolsSln.VIewModel;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace WorkToolsSln.View
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : INavigableView<SettingVM>
    {
        public SettingVM ViewModel { get; }

        public SettingPage(SettingVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

    }
}
