package com.unikguide.mapper;

import com.unikguide.DTO.ReviewDTO;
import com.unikguide.entity.Review;
import com.unikguide.entity.University;
import com.unikguide.entity.User;

import java.util.Date;

public class ReviewMapper {
    public static ReviewDTO toDTO(Review review) {
        return new ReviewDTO(
                review.getId(),
                review.getUser().getId(),
                review.getUniversity().getId(),
                review.getRating(),
                review.getComment()
        );
    }

    public static Review toEntity(ReviewDTO dto, User user, University university) {
        Review review = new Review();
        review.setId(dto.id());
        review.setUser(user);
        review.setUniversity(university);
        review.setRating(dto.rating());
        review.setComment(dto.comment());
        review.setDate(new Date()); // Date is set to current date
        return review;
    }
}
