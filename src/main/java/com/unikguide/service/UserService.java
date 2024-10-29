package com.unikguide.service;

import com.unikguide.DTO.UserDTO;
import com.unikguide.entity.User;
import com.unikguide.exception.ResourceNotFoundException;
import com.unikguide.mapper.UserMapper;
import com.unikguide.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class UserService {

    @Autowired
    private UserRepository userRepository;

    @Transactional
    public UserDTO createUser(UserDTO dto) {
        User user = UserMapper.toEntity(dto);
        User savedUser = userRepository.save(user);
        return UserMapper.toDTO(savedUser);
    }

    @Transactional(readOnly = true)
    public List<UserDTO> getAllUsers() {
        return userRepository.findAll().stream()
                .map(UserMapper::toDTO)
                .collect(Collectors.toList());
    }

    @Transactional(readOnly = true)
    public UserDTO getUserById(Long id) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("User not found with id: " + id));
        return UserMapper.toDTO(user);
    }

    @Transactional
    public UserDTO updateUser(Long id, UserDTO dto) {
        User existingUser = userRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("User not found with id: " + id));
        User user = UserMapper.toEntity(dto);
        user.setId(existingUser.getId());
        User updatedUser = userRepository.save(user);
        return UserMapper.toDTO(updatedUser);
    }

    @Transactional
    public void deleteUser(Long id) {
        if (!userRepository.existsById(id)) {
            throw new ResourceNotFoundException("User not found with id: " + id);
        }
        userRepository.deleteById(id);
    }
}