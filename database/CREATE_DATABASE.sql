-- Verifica se o banco de dados existe e cria somente se não existir
DO $$
BEGIN
    IF NOT EXISTS (SELECT FROM pg_database WHERE datname = 'found_flow_db') THEN
        CREATE DATABASE found_flow_db;
    END IF;
END $$;

-- Conexão com o banco de dados
\c found_flow_db;

-- Exclui as tabelas existentes, se houver
DROP TABLE IF EXISTS transactions, categories, users CASCADE;

-- Criação da tabela users
CREATE TABLE users (
    user_id UUID PRIMARY KEY,
    user_name VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    notification_enabled BOOLEAN,
    blocked BOOLEAN,
    creation_date TIMESTAMP NOT NULL
);

-- Criação do índice único para o campo email
CREATE UNIQUE INDEX IF NOT EXISTS idx_email ON users (email);

-- Criação da tabela categories
CREATE TABLE categories (
    category_id UUID PRIMARY KEY,
    user_id UUID NOT NULL,
    category_name VARCHAR(255) NOT NULL,
    color VARCHAR(50),
    creation_date TIMESTAMP NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
);

-- Criação da tabela transactions
CREATE TABLE transactions (
    transaction_id UUID PRIMARY KEY,
    category_id UUID NOT NULL,
    user_id UUID NOT NULL,
    transaction_name VARCHAR(255) NOT NULL,
    amount NUMERIC NOT NULL,
    transaction_type VARCHAR(20) CHECK (transaction_type IN ('RECEITA', 'DESPESA')) NOT NULL,
    creation_date TIMESTAMP NOT NULL,
    payment_date TIMESTAMP NULL,
    payment_status VARCHAR(100) NULL,
    FOREIGN KEY (category_id) REFERENCES categories(category_id) ON DELETE RESTRICT,
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE
);

-- Criação dos índices
CREATE INDEX IF NOT EXISTS idx_transaction_type ON transactions (transaction_type);
CREATE INDEX IF NOT EXISTS idx_creation_date ON transactions (creation_date);
CREATE INDEX IF NOT EXISTS idx_payment_date ON transactions (payment_date);
CREATE INDEX IF NOT EXISTS idx_payment_status ON transactions (payment_status);