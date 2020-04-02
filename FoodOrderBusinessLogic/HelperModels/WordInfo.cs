﻿using FoodOrderBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace FoodOrderBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<SetViewModel> Sets { get; set; }
    }
}
