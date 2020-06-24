﻿using FoodOrderBusinessLogic.Attribures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace FoodOrderBusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel : BaseViewModel
    {
        [DataMember] public string MessageId { get; set; }
        [DataMember]
        [DisplayName("Отправитель")] 
        [Column(title: "Отправитель", width: 100)]    
        public string SenderName { get; set; }
        [DataMember]
        [DisplayName("Дата письма")]
        [Column(title: "Дата письма", width: 100)] 
        public DateTime DateDelivery { get; set; }
        [DataMember]
        [DisplayName("Заголовок")]
        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }
        [DataMember]
        [DisplayName("Текст")]
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "MessageId",
            "SenderName",
            "DateDelivery",
            "Subject",
            "Body"
        };
    }
}
