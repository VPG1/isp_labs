using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab5.Domain;

public class PartsWarehouse
{
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    
    public PartsWarehouse(){}

    public PartsWarehouse(decimal length, decimal width, decimal height)
    {
        Length = length;
        Width = width;
        Height = height;
    }
}