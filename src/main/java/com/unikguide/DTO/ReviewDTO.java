package com.unikguide.DTO;

public record ReviewDTO(
        Long id,
        Long userId,
        Long universityId,
        int rating,
        String comment
) {}
