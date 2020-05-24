﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FoodOrderBusinessLogic.BindingModels
{
    [DataContract]
    public class CreateOrderBindingModel
    {
        [DataMember]
        public int SetId { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
