package com.unikguide;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.jdbc.datasource.DriverManagerDataSource;
import javax.sql.DataSource;

@SpringBootApplication
public class UnikGuideApplication {
	public static void main(String[] args) {
		SpringApplication.run(UnikGuideApplication.class, args);
	}

	@Bean
	public PasswordEncoder passwordEncoder() {
		return new BCryptPasswordEncoder();
	}

	@Bean
	public DataSource dataSource(
			@Value("${spring.datasource.url}") String url,
			@Value("${spring.datasource.username}") String username,
			@Value("${spring.datasource.password}") String password) {
		DriverManagerDataSource dataSource = new DriverManagerDataSource();
		dataSource.setDriverClassName("org.postgresql.Driver");
		dataSource.setUrl(url);
		dataSource.setUsername(username);
		dataSource.setPassword(password);
		return dataSource;
	}
}

