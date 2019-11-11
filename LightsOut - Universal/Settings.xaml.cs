using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LightsOut___Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        private LightsOutGame game;
        public Settings()
        {
            this.InitializeComponent();
        }

        private void SizeChanged_Checked(object sender, RoutedEventArgs e)
        {
           if(_3Radio.IsChecked == true)
            {
                game.GridSize = 3;

            }else if(_4Radio.IsChecked == true)
            {
                game.GridSize = 4;
                
            }else if(_5Radio.IsChecked == true)
            {
                game.GridSize = 5;
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            string json = JsonConvert.SerializeObject(game);
            ApplicationData.Current.LocalSettings.Values["gameData"] = json;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("gameData"))
            {
                string json = ApplicationData.Current.LocalSettings.Values["gameData"] as string;
                game = JsonConvert.DeserializeObject<LightsOutGame>(json);

                if (game.GridSize == 3)
                {
                    _3Radio.IsChecked = true;
                }else if(game.GridSize == 4)
                {
                    _4Radio.IsChecked = true;
                }
                else if(game.GridSize == 5)
                {
                    _5Radio.IsChecked = true;
                }
                
            }
          
        }
    }
}
