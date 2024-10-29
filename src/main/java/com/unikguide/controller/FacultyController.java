package com.unikguide.controller;


import com.unikguide.DTO.FacultyDTO;
import com.unikguide.service.FacultyService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/faculties")
public class FacultyController {

    @Autowired
    private FacultyService facultyService;

    @PostMapping
    public ResponseEntity<FacultyDTO> createFaculty(@RequestBody FacultyDTO dto) {
        FacultyDTO createdFaculty = facultyService.createFaculty(dto);
        return ResponseEntity.ok(createdFaculty);
    }

    @GetMapping
    public ResponseEntity<List<FacultyDTO>> getAllFaculties() {
        List<FacultyDTO> faculties = facultyService.getAllFaculties();
        return ResponseEntity.ok(faculties);
    }

    @GetMapping("/{id}")
    public ResponseEntity<FacultyDTO> getFacultyById(@PathVariable Long id) {
        FacultyDTO faculty = facultyService.getFacultyById(id);
        return ResponseEntity.ok(faculty);
    }

    @PutMapping("/{id}")
    public ResponseEntity<FacultyDTO> updateFaculty(@PathVariable Long id, @RequestBody FacultyDTO dto) {
        FacultyDTO updatedFaculty = facultyService.updateFaculty(id, dto);
        return ResponseEntity.ok(updatedFaculty);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteFaculty(@PathVariable Long id) {
        facultyService.deleteFaculty(id);
        return ResponseEntity.noContent().build();
    }
}
