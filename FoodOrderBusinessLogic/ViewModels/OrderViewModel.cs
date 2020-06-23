using FoodOrderBusinessLogic.Attribures;
using FoodOrderBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel : BaseViewModel
    {
        [DataMember]
        public int? ClientId { get; set; }
        [DataMember]
        public int SetId { get; set; }
        [DataMember]
        [DisplayName("Клиент")]
        [Column(title: "Клиент", width: 100)]
        public string ClientFIO { get; set; }
        [Column(title: "Набор", width: 100)]
        [DisplayName("Набор")]
        public string SetName { get; set; }
        [DataMember]
        public int? ImplementerId { get; set; }
        [DataMember]
        [DisplayName("Исполнитель")]
        [Column(title: "Исполнитель", width: 100)]
        public string ImplementerFIO { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        [Column(title: "Количество", width: 100)]
        public int Count { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        [Column(title: "Сумма", width: 50)]
        public decimal Sum { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        [Column(title: "Статус", width: 100)]
        public OrderStatus Status { get; set; }
        [DataMember]
        [DisplayName("Дата создания")]
        [Column(title: "Дата создания", width: 100)]
        public DateTime DateCreate { get; set; }
        [DataMember]
        [DisplayName("Дата выполнения")]
        [Column(title: "Дата выполнения", width: 100)]
        public DateTime? DateImplement { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "ClientFIO",
            "SetName",
            "ImplementerFIO",
            "Count",
            "Sum",
            "Status",
            "DateCreate",
            "DateImplement"
        };
    }
}
