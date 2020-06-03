using FoodOrderBusinessLogic.Attribures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    [DataContract]
    public class DishViewModel : BaseViewModel
    {
        [DataMember]
        [DisplayName("Название блюда")]
        [Column(title: "Название блюда", width: 150)]
        public string DishName { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "DishName"
        };
    }
}
