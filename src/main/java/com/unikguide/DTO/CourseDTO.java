package com.unikguide.DTO;

public record CourseDTO(
        Long id,
        String name,
        String description,
        Long facultyId
) {}
