﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MWMP.ViewModels;
using MWMP.Models;

namespace MWMP
{
	/// <summary>
	/// Interaction logic for PlayerControl.xaml
	/// </summary>
	public partial class PlayerControl : UserControl
	{
		public PlayerControl()
		{
			this.InitializeComponent();
		}

        private void MediaTimeline_CurrentTimeInvalidated(object sender, EventArgs e)
        {

        }
	}
}