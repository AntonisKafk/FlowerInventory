-- Check if database exists
IF DB_ID('FlowerInventory') IS NOT NULL
BEGIN
    ALTER DATABASE FlowerInventory SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE FlowerInventory;
END
GO

-- Create the database
CREATE DATABASE FlowerInventory;
GO

USE FlowerInventory;
GO

-- Create tables for categories and flowers
:r .\tables.sql

-- Seed Data
:r .\seed_data.sql