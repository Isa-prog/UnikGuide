package com.unikguide.service;

import com.unikguide.DTO.ReviewDTO;
import com.unikguide.entity.Review;
import com.unikguide.entity.University;
import com.unikguide.entity.User;
import com.unikguide.exception.ResourceNotFoundException;
import com.unikguide.mapper.ReviewMapper;
import com.unikguide.repository.ReviewRepository;
import com.unikguide.repository.UniversityRepository;
import com.unikguide.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class ReviewService {

    @Autowired
    private ReviewRepository reviewRepository;
    @Autowired
    private UserRepository userRepository;
    @Autowired
    private UniversityRepository universityRepository;

    @Transactional
    public ReviewDTO createReview(ReviewDTO dto) {
        User user = userRepository.findById(dto.userId())
                .orElseThrow(() -> new ResourceNotFoundException("User not found with id: " + dto.userId()));
        University university = universityRepository.findById(dto.universityId())
                .orElseThrow(() -> new ResourceNotFoundException("University not found with id: " + dto.universityId()));
        Review review = ReviewMapper.toEntity(dto, user, university);
        Review savedReview = reviewRepository.save(review);
        return ReviewMapper.toDTO(savedReview);
    }

    @Transactional(readOnly = true)
    public List<ReviewDTO> getAllReviews() {
        return reviewRepository.findAll().stream()
                .map(ReviewMapper::toDTO)
                .collect(Collectors.toList());
    }

    @Transactional(readOnly = true)
    public ReviewDTO getReviewById(Long id) {
        Review review = reviewRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Review not found with id: " + id));
        return ReviewMapper.toDTO(review);
    }

    @Transactional
    public ReviewDTO updateReview(Long id, ReviewDTO dto) {
        Review existingReview = reviewRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Review not found with id: " + id));
        User user = userRepository.findById(dto.userId())
                .orElseThrow(() -> new ResourceNotFoundException("User not found with id: " + dto.userId()));
        University university = universityRepository.findById(dto.universityId())
                .orElseThrow(() -> new ResourceNotFoundException("University not found with id: " + dto.universityId()));

        Review review = ReviewMapper.toEntity(dto, user, university);
        review.setId(existingReview.getId());
        Review updatedReview = reviewRepository.save(review);
        return ReviewMapper.toDTO(updatedReview);
    }

    @Transactional
    public void deleteReview(Long id) {
        if (!reviewRepository.existsById(id)) {
            throw new ResourceNotFoundException("Review not found with id: " + id);
        }
        reviewRepository.deleteById(id);
    }
}