INSERT INTO [users] ([first_name], [last_name], [email], [role], [user_id], [login_id])
VALUES ('John', 'Admin', 'jmin@example.com', 'Administrator', 001, 1);

INSERT INTO [users] ([first_name], [last_name], [email], [role], [user_id], [login_id])
VALUES ('Steve', 'Ops', 'sops@example.com', 'Operations Manager', 002, 2);

INSERT INTO [users] ([first_name], [last_name], [email], [role], [user_id], [login_id])
VALUES ('Mark', 'Sci', 'msci@example.com', 'Environmental Scientist', 003, 3);

INSERT INTO [sensor] ([sensor_type], [sensor_id], [status], [deployment_date], [latitude], [longitude], [height], [orientation], [measurement_frequency])
VALUES ('Air', 001, 'Operational', '2024-12-25', 55, 65, 10, 180, '1 hour')

INSERT INTO [physical_quantity]([quantity_name], [quantity_id], [sensor_id], [lower_warning_threshold], [upper_warning_threshold], [lower_emergency_threshold], [upper_emergency_threshold])
VALUES ('Example Name', 001, 001, 48, 88, 40, 100)

INSERT INTO [measurement] ([timestamp], [quantity_id], [value], [measurement_unit], [measurement_id])
VALUES ('2024-12-25 00:01:00', 001, 56, 'psi', 001)

INSERT INTO [measurement] ([timestamp], [quantity_id], [value], [measurement_unit], [measurement_id])
VALUES ('2024-12-25 00:02:00', 001, 70, 'psi', 002)

INSERT INTO [measurement] ([timestamp], [quantity_id], [value], [measurement_unit], [measurement_id])
VALUES ('2024-12-25 00:03:00', 001, 60, 'psi', 003)