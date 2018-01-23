-- Create database if not exist
CREATE DATABASE IF NOT EXISTS `scorched`;
-- Check if database exists using the SCHEMATA table's SCHEMA_NAME column
SELECT COUNT(*) FROM information_schema.schemata WHERE SCHEMA_NAME='scorched';
