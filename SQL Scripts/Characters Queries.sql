-- Insert character into database
INSERT INTO characters 
(NAME,X,Y,z,direction,step,aim_direction,sprite) 
VALUES
('','','','','','','','');
-- Update character in database
UPDATE characters
SET NAME='',X='',Y='',z='',direction='',step='',aim_direction='',sprite=''
WHERE id='';
-- Delete chatacter from database
DELETE FROM characters WHERE id='';
-- Test query
INSERT INTO characters 
(NAME,X,Y,z,direction,step,aim_direction,sprite) 
VALUES
('sfortune','1','1','1','1','1','1','0');