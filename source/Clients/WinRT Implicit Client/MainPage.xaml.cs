﻿using Sample;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using Thinktecture.IdentityModel.WinRT;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinRT_Implicit_Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        AuthorizeResponse _response;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void LoginOnlyButton_Click(object sender, RoutedEventArgs e)
        {
            await StartFlowAsync("id_token", "openid");
        }

        private async Task StartFlowAsync(string responseType, string scope)
        {
            Exception exception = null;

            try
            {
                _response = await WebAuthentication.DoImplicitFlowAsync(
                    new Uri(Constants.AuthorizeEndpoint),
                    "implicitclient",
                    responseType,
                    scope);

                Output.Text = _response.Raw;
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                var md = new MessageDialog(exception.ToString());
                await md.ShowAsync();
            }
        }
    }
}