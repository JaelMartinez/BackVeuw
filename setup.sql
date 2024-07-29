-- Crear tabla Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);

-- Crear tabla Thumbnails
CREATE TABLE Thumbnails (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(100) NOT NULL,
	Categoria NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    ImagenUrl NVARCHAR(255),
    FechaPublicacion DATETIME NOT NULL DEFAULT GETDATE()
);
CREATE TABLE Favorites (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    ImageSrc NVARCHAR(255) NOT NULL,
    VideoSrc NVARCHAR(255) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Usuarios(Id)
);
-- Generar e insertar 10 usuarios aleatorios en la tabla Usuarios
DECLARE @i INT = 1;
WHILE @i <= 10
BEGIN
    INSERT INTO Usuarios (Nombre, Email, Password, FechaCreacion)
    VALUES (
        CONCAT('Usuario', @i), -- Nombre
        CONCAT('usuario', @i, '@example.com'), -- Email
        CONCAT('password', @i, '123'), -- Password
        GETDATE() -- Fecha de Creación
    );
    SET @i = @i + 1;
END;


-- Insertar Películas de Tendencia
INSERT INTO Thumbnails (Titulo, Categoria, Descripcion, ImagenUrl, FechaPublicacion)
VALUES 
('Avatar: The Way of Water', 'Science Fiction, Adventure, Action', 'Set more than a decade after the events of the first film, learn the story of the Sully family (Jake, Neytiri, and their kids), the trouble that follows them, the lengths they go to keep each other safe, the battles they fight to stay alive, and the tragedies they endure.', 'https://image.tmdb.org/t/p/original/am8Zbct7hbZYGzet3Ub1Sa9Xskb.jpg', '2024-07-02'),
('Dune', 'Adventure, Drama, Sci-Fi', 'Feature adaptation of Frank Herbert''s science fiction novel, about the son of a noble family entrusted with the protection of the most valuable asset and most vital element in the galaxy.', 'https://image.tmdb.org/t/p/original/eZ239CUp1d6OryZEBPnO2n87gMG.jpg', '2024-07-06'),
('Deadpool and Wolverine', 'Action, Comedy, Superhero', 'Deadpool and Wolverine team up for an action-packed adventure filled with humor, action, and unexpected twists. As they navigate through a dangerous mission, their clashing personalities and unique abilities make for an unforgettable duo.', 'https://image.tmdb.org/t/p/original/9l1eZiJHmhr5jIlthMdJN5WYoff.jpg', '2024-07-08');

-- Insertar Series de Tendencia
INSERT INTO Thumbnails (Titulo, Categoria, Descripcion, ImagenUrl, FechaPublicacion)
VALUES 
('The Boys', 'Sci-Fi & Fantasy, Action & Adventure', 'A group of vigilantes known informally as “The Boys” set out to take down corrupt superheroes with no more than blue-collar grit and a willingness to fight dirty.', 'https://image.tmdb.org/t/p/original/8S6UjhrqbSC1KIIsHfHQusunD1c.jpg', '2024-07-03');

select * from Usuarios

select * from Favorites

select * from Thumbnails

