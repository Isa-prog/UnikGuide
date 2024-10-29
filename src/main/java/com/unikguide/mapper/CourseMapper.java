package com.unikguide.mapper;

import com.unikguide.DTO.CourseDTO;
import com.unikguide.entity.Course;
import com.unikguide.entity.Faculty;

public class CourseMapper {
    public static CourseDTO toDTO(Course course) {
        return new CourseDTO(
                course.getId(),
                course.getName(),
                course.getDescription(),
                course.getFaculty().getId()
        );
    }

    public static Course toEntity(CourseDTO dto, Faculty faculty) {
        Course course = new Course();
        course.setId(dto.id());
        course.setName(dto.name());
        course.setDescription(dto.description());
        course.setFaculty(faculty);
        return course;
    }
}

