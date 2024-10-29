package com.unikguide.controller;

import com.unikguide.DTO.ReviewDTO;
import com.unikguide.service.ReviewService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/reviews")
public class ReviewController {

    @Autowired
    private ReviewService reviewService;

    @PostMapping
    public ResponseEntity<ReviewDTO> createReview(@RequestBody ReviewDTO dto) {
        ReviewDTO createdReview = reviewService.createReview(dto);
        return ResponseEntity.ok(createdReview);
    }

    @GetMapping
    public ResponseEntity<List<ReviewDTO>> getAllReviews() {
        List<ReviewDTO> reviews = reviewService.getAllReviews();
        return ResponseEntity.ok(reviews);
    }

    @GetMapping("/{id}")
    public ResponseEntity<ReviewDTO> getReviewById(@PathVariable Long id) {
        ReviewDTO review = reviewService.getReviewById(id);
        return ResponseEntity.ok(review);
    }

    @PutMapping("/{id}")
    public ResponseEntity<ReviewDTO> updateReview(@PathVariable Long id, @RequestBody ReviewDTO dto) {
        ReviewDTO updatedReview = reviewService.updateReview(id, dto);
        return ResponseEntity.ok(updatedReview);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteReview(@PathVariable Long id) {
        reviewService.deleteReview(id);
        return ResponseEntity.noContent().build();
    }
}
