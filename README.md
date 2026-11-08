# IS902-RPGMarket
Proyecto individual para el seguimiento de clase de Sistemas Expertos

# PROYECTO-Complementario: RPGMarket (In-Game Marketplace / Casa de Subastas)
1. VISIÓN GLOBAL Y ENUNCIADO DEL PROYECTO
PixelMarket es una plataforma web de mercado virtual donde los jugadores pueden listar, comprar y vender objetos virtuales (armas, armaduras, consumibles) utilizando la moneda interna del juego ("Gold"). Cuenta con un panel administrativo para regular la economía global y un estricto control de acceso por roles mediante JWT.

Objetivo Académico:
Este proyecto tiene como finalidad practicar lo aprendido en clase posterior a explicacion en clase.

# 2. ESQUEMA RELACIONAL (Exactamente 3 Tablas)

Tabla 1: Users (Identidad, Roles y Billetera)
- Id (INT, PK, Identity)
- Username (VARCHAR, Unique, Not Null)
- Email (VARCHAR, Unique, Not Null)
- PasswordHash (VARCHAR, Not Null)
- Balance (DECIMAL(18,2), Default = 1000.00) -> Oro virtual inicial
- Role (VARCHAR/Enum: "Player", "Admin")
- CreatedAt (DATETIME2, Default = GETDATE())

Tabla 2: Items (Catálogo Global de Objetos del Juego)
- Id (INT, PK, Identity)
- Name (VARCHAR, Not Null) -> Ej. "Espada de Fuego +5"
- Category (VARCHAR, Not Null) -> Ej. "Arma", "Consumible", "Armadura"
- Rarity (VARCHAR, Not Null) -> Ej. "Común", "Raro", "Legendario"
- BasePrice (DECIMAL(18,2), Not Null)
- ImageUrl (VARCHAR, Nullable)

Tabla 3: Listings (El Mercado / Órdenes de Venta y Transacciones)
- Id (INT, PK, Identity)
- SellerId (INT, FK -> Users.Id, Not Null)
- BuyerId (INT, FK -> Users.Id, Nullable) -> Nulo si sigue a la venta
- ItemId (INT, FK -> Items.Id, Not Null)
- Price (DECIMAL(18,2), Not Null) -> Precio fijado por el vendedor
- Status (VARCHAR/Enum: "Active", "Sold", "Cancelled")
- CreatedAt (DATETIME2, Default = GETDATE())

# 3. SCRIPT SQL DE CREACIÓN Y DATOS DE PRUEBA (SQL Server / T-SQL)

CREATE DATABASE RPGMarketDB;
GO

USE RPGMarketDB;
GO

-- Creación de Tablas
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Balance DECIMAL(18,2) NOT NULL DEFAULT 1000.00,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('Player', 'Admin')),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE Items (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL CHECK (Category IN ('Arma', 'Armadura', 'Consumible', 'Material')),
    Rarity VARCHAR(30) NOT NULL CHECK (Rarity IN ('Común', 'Raro', 'Épico', 'Legendario')),
    BasePrice DECIMAL(18,2) NOT NULL,
    ImageUrl VARCHAR(255) NULL
);
GO

CREATE TABLE Listings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SellerId INT NOT NULL,
    BuyerId INT NULL,
    ItemId INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Active' CHECK (Status IN ('Active', 'Sold', 'Cancelled')),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_Listings_Seller FOREIGN KEY (SellerId) REFERENCES Users(Id),
    CONSTRAINT FK_Listings_Buyer FOREIGN KEY (BuyerId) REFERENCES Users(Id),
    CONSTRAINT FK_Listings_Item FOREIGN KEY (ItemId) REFERENCES Items(Id)
);
GO

-- Datos de Prueba (Seed Data)
INSERT INTO Users (Username, Email, PasswordHash, Balance, Role) VALUES
('GameMaster_Admin', 'admin@rpgmarket.com', '$2a$11$E...AdminHashMock...', 50000.00, 'Admin'),
('Arthas99', 'arthas@blizzard.com', '$2a$11$E...PlayerHashMock...', 1500.50, 'Player'),
('SylvanasX', 'sylvanas@blizzard.com', '$2a$11$E...PlayerHashMock...', 3400.00, 'Player'),
('Thrall_Shaman', 'thrall@blizzard.com', '$2a$11$E...PlayerHashMock...', 850.00, 'Player');
GO

INSERT INTO Items (Name, Category, Rarity, BasePrice, ImageUrl) VALUES
('Espada de Escarcha +5', 'Arma', 'Legendario', 2500.00, '/images/items/frostmourne.png'),
('Arco de Sylvanas', 'Arma', 'Épico', 1200.00, '/images/items/sylvanas_bow.png'),
('Poción de Maná Mayor', 'Consumible', 'Común', 50.00, '/images/items/mana_potion.png'),
('Armadura de Placas de Acero', 'Armadura', 'Raro', 800.00, '/images/items/steel_plate.png'),
('Mineral de Saronita', 'Material', 'Raro', 120.00, '/images/items/saronite.png');
GO

INSERT INTO Listings (SellerId, BuyerId, ItemId, Price, Status) VALUES
(2, NULL, 1, 2800.00, 'Active'),
(3, NULL, 2, 1350.00, 'Active'),
(4, 2, 3, 45.00, 'Sold'),
(4, NULL, 4, 750.00, 'Active');
GO

# 4. DEFINICIÓN DEL PRODUCTO MÍNIMO VIABLE (MVP)

El MVP incluye el conjunto funcional indispensable para cubrir el ciclo de desarrollo:
- Autenticación JWT con roles ("Player" / "Admin").
- Panel Backoffice exclusivo para administradores (CRUD sobre Items).
- Publicación y visualización de ofertas en el mercado (Listings).
- Motor transaccional atómico para la compra segura de ítems y actualización de saldos.
- Billetera personal e historial de transacciones.

Fuera de Alcance (MVP): Subastas en tiempo real con WebSockets, pasarelas de pago con dinero real y chat interno entre usuarios.

# 5. ÉPICAS DEL SISTEMA

Épica 1: Identidad, Seguridad y Gestión de Cuentas (IAM)
- HU-01: Registro de Usuarios. Creación de cuentas "Player" con saldo inicial de oro y validación de unicidad de email.
- HU-02: Inicio de Sesión (Login). Autenticación en .NET que emite un JWT firmado con los claims de identidad y rol.

Épica 2: Catálogo Global e Inventario (Backoffice)
- HU-03: Gestión de Ítems (CRUD Admin). Panel protegido para que el rol "Admin" administre el catálogo global en la tabla `Items`.
- HU-04: Visualización del Catálogo. Interfaz para que los jugadores consulten los objetos disponibles en el juego.

Épica 3: El Mercado (Listings y Órdenes de Venta)
- HU-05: Publicación de Ítems. Un "Player" pone a la venta un objeto registrando un `Listing` en estado "Active".
- HU-06: Exploración del Mercado. Listado en React de todas las ofertas de venta activas en tiempo real.

Épica 4: Motor Transaccional de Compra y Billetera
- HU-07: Compra Segura de Ítems. Lógica de negocio en .NET que valida el balance del comprador, transfiere el oro al vendedor, actualiza el estado a "Sold" y asigna el `BuyerId` dentro de una transacción de base de datos atómica.
- HU-08: Historial y Billetera Personal. Vista del perfil del usuario con su saldo actual y el registro de compras/ventas.
