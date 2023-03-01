-- -----------------------------------------------------
-- CREATE TABLE: Products
-- -----------------------------------------------------

CREATE TABLE [Autoglass].[dbo].[Products] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,                 -- Código do produto (sequencial e não nulo)          
    [Description] NVARCHAR(200) NOT NULL,               -- Descrição do produto (não nulo)
    [Status] INT NOT NULL,                              -- Situação do produto (Ativo ou Inativo)
    [ManufacturingDate] DATE NOT NULL,                  -- Data de fabricação
    [ExpirationDate] DATE NOT NULL,                     -- Data de validade
    [SupplierId] INT NOT NULL,                          -- Código do fornecedor
    [SupplierDescription] NVARCHAR(200) NOT NULL,       -- Descrição do fornecedor
    [SupplierDocument] NVARCHAR(18) NULL                -- CNPJ do fornecedor
)
GO