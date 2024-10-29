package com.unikguide.service;

import com.unikguide.DTO.FacultyDTO;
import com.unikguide.entity.Faculty;
import com.unikguide.entity.University;
import com.unikguide.exception.ResourceNotFoundException;
import com.unikguide.mapper.FacultyMapper;
import com.unikguide.repository.FacultyRepository;
import com.unikguide.repository.UniversityRepository;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class FacultyService {

    @Autowired
    private FacultyRepository facultyRepository;
    @Autowired
    private UniversityRepository universityRepository;

    @Transactional
    public FacultyDTO createFaculty(FacultyDTO dto) {
        University university = universityRepository.findById(dto.universityId())
                .orElseThrow(() -> new ResourceNotFoundException("University not found with id: " + dto.universityId()));
        Faculty faculty = FacultyMapper.toEntity(dto, university);
        Faculty savedFaculty = facultyRepository.save(faculty);
        return FacultyMapper.toDTO(savedFaculty);
    }

    @Transactional(readOnly = true)
    public List<FacultyDTO> getAllFaculties() {
        return facultyRepository.findAll().stream()
                .map(FacultyMapper::toDTO)
                .collect(Collectors.toList());
    }

    @Transactional(readOnly = true)
    public FacultyDTO getFacultyById(Long id) {
        Faculty faculty = facultyRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Faculty not found with id: " + id));
        return FacultyMapper.toDTO(faculty);
    }

    @Transactional
    public FacultyDTO updateFaculty(Long id, FacultyDTO dto) {
        Faculty existingFaculty = facultyRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Faculty not found with id: " + id));
        University university = universityRepository.findById(dto.universityId())
                .orElseThrow(() -> new ResourceNotFoundException("University not found with id: " + dto.universityId()));

        Faculty faculty = FacultyMapper.toEntity(dto, university);
        faculty.setId(existingFaculty.getId());
        Faculty updatedFaculty = facultyRepository.save(faculty);
        return FacultyMapper.toDTO(updatedFaculty);
    }

    @Transactional
    public void deleteFaculty(Long id) {
        if (!facultyRepository.existsById(id)) {
            throw new ResourceNotFoundException("Faculty not found with id: " + id);
        }
        facultyRepository.deleteById(id);
    }
}