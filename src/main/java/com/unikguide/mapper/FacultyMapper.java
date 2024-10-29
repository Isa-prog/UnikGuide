package com.unikguide.mapper;

import com.unikguide.DTO.FacultyDTO;
import com.unikguide.entity.Faculty;
import com.unikguide.entity.University;

public class FacultyMapper {
    public static FacultyDTO toDTO(Faculty faculty) {
        return new FacultyDTO(
                faculty.getId(),
                faculty.getName(),
                faculty.getUniversity().getId()
        );
    }

    public static Faculty toEntity(FacultyDTO dto, University university) {
        Faculty faculty = new Faculty();
        faculty.setId(dto.id());
        faculty.setName(dto.name());
        faculty.setUniversity(university);
        return faculty;
    }
}