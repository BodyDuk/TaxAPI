﻿namespace TaxCalculation.Core.Entities.TaxCalculation
{
    public interface ITaxSettings
    {
        public decimal BandA { get; set; }
        public decimal BandB { get; set; }
        public decimal BasicRate { get; set;}
        public decimal HigherRate { get; set;}
        public int AnnualMonthCount { get; set;}
    }
}