﻿using FoodOrderBusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public int? ImplementerId { get; set; }

        public int ClientId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Set Set { get; set; }
        public Client Client { get; set; }
        public Implementer Implementer { get; set; }
    }
}
