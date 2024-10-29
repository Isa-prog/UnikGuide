-- Create University Table
CREATE TABLE university
(
    id          BIGSERIAL PRIMARY KEY,
    name        VARCHAR(255) NOT NULL,
    location    VARCHAR(255) NOT NULL,
    description TEXT
);

-- Create Faculty Table
CREATE TABLE faculty
(
    id            BIGSERIAL PRIMARY KEY,
    name          VARCHAR(255) NOT NULL,
    university_id BIGINT       NOT NULL,
    CONSTRAINT fk_university
        FOREIGN KEY (university_id)
            REFERENCES university (id)
);

-- Create Course Table
CREATE TABLE course
(
    id          BIGSERIAL PRIMARY KEY,
    name        VARCHAR(255) NOT NULL,
    description TEXT,
    faculty_id  BIGINT       NOT NULL,
    CONSTRAINT fk_faculty
        FOREIGN KEY (faculty_id)
            REFERENCES faculty (id)
);

-- Create User Table
CREATE TABLE users
(
    id       BIGSERIAL PRIMARY KEY,
    email    VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role     VARCHAR(50)  NOT NULL
);

-- Create Review Table
CREATE TABLE review
(
    id            BIGSERIAL PRIMARY KEY,
    user_id       BIGINT    NOT NULL,
    university_id BIGINT    NOT NULL,
    rating        INT       NOT NULL,
    comment       TEXT,
    date          TIMESTAMP NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_user
        FOREIGN KEY (user_id)
            REFERENCES users (id),
    CONSTRAINT fk_university_review
        FOREIGN KEY (university_id)
            REFERENCES university (id)
);

-- Insert Initial Data into University Table
INSERT INTO university (name, location, description)
VALUES ('Ala-Too International University', 'Bishkek, Kyrgyzstan',
        'One of the leading universities in Kyrgyzstan focused on engineering and business'),
       ('Kyrgyz State University', 'Bishkek, Kyrgyzstan', 'A major public university with a wide range of programs.');

-- Insert Initial Data into Faculty Table
INSERT INTO faculty (name, university_id)
VALUES ('Engineering Faculty', 1),
       ('Business Faculty', 1),
       ('Computer Science Faculty', 2);

-- Insert Initial Data into Course Table
INSERT INTO course (name, description, faculty_id)
VALUES ('Introduction to Programming', 'A course covering the basics of programming.', 1),
       ('Advanced Business Management', 'An in-depth course on business management practices.', 2),
       ('Data Structures', 'An intermediate course on data structures.', 3);

-- Insert Initial Data into User Table
INSERT INTO users (email, password, role)
VALUES ('user1@example.com', 'password123', 'STUDENT'),
       ('admin@example.com', 'adminpassword', 'ADMIN');

-- Insert Initial Data into Review Table
INSERT INTO review (user_id, university_id, rating, comment)
VALUES (1, 1, 5, 'Great university with excellent facilities!'),
       (2, 2, 4, 'Good overall experience, but needs improvement in management.');
