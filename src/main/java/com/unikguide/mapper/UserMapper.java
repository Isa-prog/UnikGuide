package com.unikguide.mapper;

import com.unikguide.DTO.UserDTO;
import com.unikguide.entity.User;

public class UserMapper {
    public static UserDTO toDTO(User user) {
        return new UserDTO(
                user.getId(),
                user.getEmail(),
                user.getRole()
        );
    }

    public static User toEntity(UserDTO dto) {
        User user = new User();
        user.setId(dto.id());
        user.setEmail(dto.email());
        user.setRole(dto.role());
        return user;
    }
}
