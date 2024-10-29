package com.unikguide.mapper;

import com.unikguide.DTO.UniversityDTO;
import com.unikguide.entity.University;

public class UniversityMapper {
    public static UniversityDTO toDTO(University university) {
        return new UniversityDTO(
                university.getId(),
                university.getName(),
                university.getLocation(),
                university.getDescription()
        );
    }

    public static University toEntity(UniversityDTO dto) {
        University university = new University();
        university.setId(dto.id());
        university.setName(dto.name());
        university.setLocation(dto.location());
        university.setDescription(dto.description());
        return university;
    }
}
