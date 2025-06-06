﻿namespace WhiteLemon.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for user response.
    /// Αντικείμενο μεταφοράς δεδομένων (DTO) για την aπόκριση χρήστη.
    /// </summary>
    public record ResponseRegisterUserDto(Guid UserId, string? Name, string? Email, string? photoUrl);

}

