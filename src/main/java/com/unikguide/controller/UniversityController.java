package com.unikguide.controller;

import com.unikguide.DTO.UniversityDTO;
import com.unikguide.service.UniversityService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/universities")
public class UniversityController {

    @Autowired
    private UniversityService universityService;

    @PostMapping
    public ResponseEntity<UniversityDTO> createUniversity(@RequestBody UniversityDTO dto) {
        UniversityDTO createdUniversity = universityService.createUniversity(dto);
        return ResponseEntity.ok(createdUniversity);
    }

    @GetMapping
    public ResponseEntity<List<UniversityDTO>> getAllUniversities() {
        List<UniversityDTO> universities = universityService.getAllUniversities();
        return ResponseEntity.ok(universities);
    }

    @GetMapping("/{id}")
    public ResponseEntity<UniversityDTO> getUniversityById(@PathVariable Long id) {
        UniversityDTO university = universityService.getUniversityById(id);
        return ResponseEntity.ok(university);
    }

    @PutMapping("/{id}")
    public ResponseEntity<UniversityDTO> updateUniversity(@PathVariable Long id, @RequestBody UniversityDTO dto) {
        UniversityDTO updatedUniversity = universityService.updateUniversity(id, dto);
        return ResponseEntity.ok(updatedUniversity);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteUniversity(@PathVariable Long id) {
        universityService.deleteUniversity(id);
        return ResponseEntity.noContent().build();
    }
}
