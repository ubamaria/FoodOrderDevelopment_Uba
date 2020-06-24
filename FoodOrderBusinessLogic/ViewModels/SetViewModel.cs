using FoodOrderBusinessLogic.Attribures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    [DataContract]
    public class SetViewModel : BaseViewModel
    {
        [DataMember]
        [DisplayName("Название набора")]
        [Column(title: "Название набора", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string SetName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> SetOfDishes { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "SetName",
            "Price"
        };
    }
}
