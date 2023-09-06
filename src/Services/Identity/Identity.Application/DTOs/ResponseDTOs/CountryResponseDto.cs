﻿namespace Identity.Application.DTOs.ResponseDTOs
{
    public class CountryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<CityResponseDto> Cities { get; set; } = null!;
    }
}
