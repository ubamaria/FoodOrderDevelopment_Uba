﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string StorageName { get; set; }
        public List<StorageDishViewModel> StorageDishes { get; set; }
    }
}
