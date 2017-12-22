-- Insert character into database
INSERT INTO characters 
(NAME,class,race,LEVEL,experience,health,max_health,resource,max_resource,direction,step,aim_direction,sprite) 
VALUES
('','','','','','','','','','','','','');
-- Update character in database
UPDATE characters
SET NAME='',class='',race='',LEVEL='',experience='',health='',max_health='',resource='',max_resource='',direction='',step='',aim_direction='',sprite=''
WHERE id='';
-- Delete chatacter from database
DELETE FROM characters WHERE id='';