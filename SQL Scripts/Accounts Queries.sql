-- Insert Account:
INSERT INTO accounts (NAME,PASSWORD,last_logged) VALUE ('name','password','00/00/0000 00:00:00');

-- Delete Account:
DELETE FROM accounts WHERE id='';

-- Update Account:
UPDATE accounts SET NAME='',PASSWORD='',last_logged='' WHERE id='';

-- Update Password:
UPDATE accounts SET PASSWORD='' WHERE id='';

-- Account Exist/Check Password:
SELECT * FROM accounts WHERE NAME='';

-- Remove auto_increment from the id
ALTER TABLE accounts AUTO_INCREMENT = 1;

-- Update character ids
UPDATE accounts
SET characterid_1='',characterid_2='',characterid_3='',characterid_4='',characterid_5='' 
WHERE id='';