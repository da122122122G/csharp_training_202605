TRUNCATE TABLE
    employee,
    department
RESTART IDENTITY CASCADE;

Insert into department (name) values ('未所属');
INSERT INTO department (name) VALUES ('総務部');
INSERT INTO department (name) VALUES ('経理部');
INSERT INTO department (name) VALUES ('人事部');
INSERT INTO department (name) VALUES ('開発部');
INSERT INTO department (name) VALUES ('営業部');

INSERT INTO employee (name, dept_id, phone_number, e_mail) VALUES ('田中太郎',2,'012-3456-7890','afdbv@awerv');
INSERT INTO employee (name, dept_id, phone_number, e_mail) VALUES ('鈴木三郎',1,'123-4567-8901','okjnbg@okjh');
INSERT INTO employee (name, dept_id, phone_number, e_mail) VALUES ('佐藤花子',4,'234-5678-9012','aeruvn@kerun');
INSERT INTO employee (name, dept_id, phone_number, e_mail) VALUES ('中田彩子',5,'345-6789-0123','ureid@cxmvae');
INSERT INTO employee (name, dept_id, phone_number, e_mail) VALUES ('加藤圭太',3,'45-6789-0123','asdgv@qer');
