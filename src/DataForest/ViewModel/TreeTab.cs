﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace DataForest.ViewModel
{
    class TreeTab : Tab
    {
        private DataTable data;
        private ObservableCollection<Shape> tree;
    }
}