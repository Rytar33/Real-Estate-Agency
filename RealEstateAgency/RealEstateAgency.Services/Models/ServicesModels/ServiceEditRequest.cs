﻿namespace RealEstateAgency.Services.Models.ServicesModels
{
    public class ServiceEditRequest
    {
        public string? NameService { get; set; }
        public string? DescriptionService { get; set; }
        public double? PriceService { get; set; }
        public string? TypeService { get; set; }
    }
}
