-- Insert Account:
INSERT INTO accounts (NAME,PASSWORD,last_logged) VALUE ('name','password','00/00/0000 00:00:00');

-- Delete Account:
DELETE FROM accounts WHERE id='';

-- Update Account:
UPDATE accounts SET NAME='name1',PASSWORD='password1',last_logged='11/11/1111 11:11:11' WHERE id='4';

-- Update Password:
UPDATE accounts SET PASSWORD='password2' WHERE id='4';

-- Account Exist/Check Password:
SELECT * FROM accounts WHERE NAME='';
