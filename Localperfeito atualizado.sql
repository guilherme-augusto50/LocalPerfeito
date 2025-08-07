-- Recriar banco de dados
DROP DATABASE IF EXISTS localperfeito;
CREATE DATABASE localperfeito
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE localperfeito;


-- Tabela de Usuários
CREATE TABLE usuarios (
    id_usuarios INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100),
    foto_url varchar(100),
    senha VARCHAR(255) NOT NULL
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

-- Tabela de Categorias
CREATE TABLE Categoria (
    Id_categoria INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

-- Tabela de Preferências dos Usuários
CREATE TABLE PreferenciaUsuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT,
    categoria_id INT,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id_usuarios) ON DELETE CASCADE,
    FOREIGN KEY (categoria_id) REFERENCES Categoria(Id_categoria) ON DELETE CASCADE
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

-- Tabela de Planos (ex: Free, Premium)
CREATE TABLE Planos (
    id_plano INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(50) NOT NULL UNIQUE,
    preco DECIMAL(10,2) DEFAULT 0.00,
    descricao TEXT,
    duracao_dias INT DEFAULT 30
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

-- Tabela de Empresas (com login especial)
CREATE TABLE Empresas (
    id_restaurante INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    plano_id INT,
    data_inicio_plano DATE,
    data_fim_plano DATE,
    ativo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (plano_id) REFERENCES Planos(id_plano) ON DELETE SET NULL
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

-- Tabela de Locais (ligada a categoria e ao restaurante)
CREATE TABLE Locais (
    Id_local INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    descricao TEXT,
    edereco VARCHAR(200),
    preco VARCHAR(20),
    tags VARCHAR(200),
    contato varchar(200),
    categoria_id INT,
    restaurante_id INT,
    avaliacao_id int,
    FOREIGN KEY (categoria_id) REFERENCES Categoria(Id_categoria) ON DELETE SET NULL,
    FOREIGN KEY (restaurante_id) REFERENCES Empresas(id_restaurante) ON DELETE SET NULL
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

-- Tabela de Favoritos dos Usuários
CREATE TABLE Favorito (
    Id_favorito INT AUTO_INCREMENT PRIMARY KEY,
    usuarios_id INT,
    local_id INT,
    FOREIGN KEY (usuarios_id) REFERENCES usuarios(id_usuarios) ON DELETE CASCADE,
    FOREIGN KEY (local_id) REFERENCES Locais(Id_local) ON DELETE CASCADE
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

-- Tabela de Pagamentos feitos por Restaurantes
CREATE TABLE Pagamentos (
    id_pagamento INT PRIMARY KEY AUTO_INCREMENT,
    restaurante_id INT,
    plano_id INT,
    valor_pago DECIMAL(10,2),
    data_pagamento DATE,
    data_inicio DATE,
    data_fim DATE,
    metodo_pagamento VARCHAR(50),
    status VARCHAR(20) DEFAULT 'confirmado',
    FOREIGN KEY (restaurante_id) REFERENCES Empresas(id_restaurante) ON DELETE CASCADE,
    FOREIGN KEY (plano_id) REFERENCES Planos(id_plano) ON DELETE SET NULL
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

-- Tabela de Avaliaçoes 
CREATE TABLE Avaliacao (
    id_avaliacao INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT,
    id_local INT,
    estrelas INT CHECK (estrelas >= 1 AND estrelas <= 5),
    comentario TEXT,
    data_avaliacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuarios) ON DELETE CASCADE,
    FOREIGN KEY (id_local) REFERENCES Locais(Id_local) ON DELETE CASCADE
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

CREATE TABLE Agendamento (
    id_agendamento INT AUTO_INCREMENT PRIMARY KEY,
    id_local INT,
    id_usuario INT,
    dia DATE,
    FOREIGN KEY (id_local) REFERENCES Locais(Id_local) ON DELETE CASCADE,
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id_usuarios) ON DELETE CASCADE
) CHARSET=utf8mb4
COLLATE=utf8mb4_unicode_ci;

    
