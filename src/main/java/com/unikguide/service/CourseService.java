package com.unikguide.service;

import com.unikguide.DTO.CourseDTO;
import com.unikguide.entity.Course;
import com.unikguide.entity.Faculty;
import com.unikguide.exception.ResourceNotFoundException;
import com.unikguide.mapper.CourseMapper;
import com.unikguide.repository.CourseRepository;
import com.unikguide.repository.FacultyRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class CourseService {

    @Autowired
    private CourseRepository courseRepository;
    @Autowired
    private FacultyRepository facultyRepository;

    @Transactional
    public CourseDTO createCourse(CourseDTO dto) {
        Faculty faculty = facultyRepository.findById(dto.facultyId())
                .orElseThrow(() -> new ResourceNotFoundException("Faculty not found with id: " + dto.facultyId()));
        Course course = CourseMapper.toEntity(dto, faculty);
        Course savedCourse = courseRepository.save(course);
        return CourseMapper.toDTO(savedCourse);
    }

    @Transactional(readOnly = true)
    public List<CourseDTO> getAllCourses() {
        return courseRepository.findAll().stream()
                .map(CourseMapper::toDTO)
                .collect(Collectors.toList());
    }

    @Transactional(readOnly = true)
    public CourseDTO getCourseById(Long id) {
        Course course = courseRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Course not found with id: " + id));
        return CourseMapper.toDTO(course);
    }

    @Transactional
    public CourseDTO updateCourse(Long id, CourseDTO dto) {
        Course existingCourse = courseRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Course not found with id: " + id));
        Faculty faculty = facultyRepository.findById(dto.facultyId())
                .orElseThrow(() -> new ResourceNotFoundException("Faculty not found with id: " + dto.facultyId()));

        Course course = CourseMapper.toEntity(dto, faculty);
        course.setId(existingCourse.getId());
        Course updatedCourse = courseRepository.save(course);
        return CourseMapper.toDTO(updatedCourse);
    }

    @Transactional
    public void deleteCourse(Long id) {
        if (!courseRepository.existsById(id)) {
            throw new ResourceNotFoundException("Course not found with id: " + id);
        }
        courseRepository.deleteById(id);
    }
}