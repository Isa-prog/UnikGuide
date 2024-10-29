package com.unikguide.service;

import com.unikguide.DTO.UniversityDTO;
import com.unikguide.entity.University;
import com.unikguide.exception.ResourceNotFoundException;
import com.unikguide.mapper.UniversityMapper;
import com.unikguide.repository.UniversityRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class UniversityService {

    @Autowired
    private UniversityRepository universityRepository;

    @Transactional
    public UniversityDTO createUniversity(UniversityDTO dto) {
        University university = UniversityMapper.toEntity(dto);
        University savedUniversity = universityRepository.save(university);
        return UniversityMapper.toDTO(savedUniversity);
    }

    @Transactional(readOnly = true)
    public List<UniversityDTO> getAllUniversities() {
        return universityRepository.findAll().stream()
                .map(UniversityMapper::toDTO)
                .collect(Collectors.toList());
    }

    @Transactional(readOnly = true)
    public UniversityDTO getUniversityById(Long id) {
        University university = universityRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("University not found with id: " + id));
        return UniversityMapper.toDTO(university);
    }

    @Transactional
    public UniversityDTO updateUniversity(Long id, UniversityDTO dto) {
        University existingUniversity = universityRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("University not found with id: " + id));
        University university = UniversityMapper.toEntity(dto);
        university.setId(existingUniversity.getId());
        University updatedUniversity = universityRepository.save(university);
        return UniversityMapper.toDTO(updatedUniversity);
    }

    @Transactional
    public void deleteUniversity(Long id) {
        if (!universityRepository.existsById(id)) {
            throw new ResourceNotFoundException("University not found with id: " + id);
        }
        universityRepository.deleteById(id);
    }
}