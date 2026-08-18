using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MeinGame.Model
{
    public partial class Wuerfel : ObservableObject
    {

        [ObservableProperty] private int _wert = 1;

        [ObservableProperty] private bool _istGesperrt = false;
        
        
    }
}
