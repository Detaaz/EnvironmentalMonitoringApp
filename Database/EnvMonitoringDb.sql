CREATE TABLE [measurement] (
  [timestamp] datetime,
  [quantity_id] int,
  [value] nvarchar(255),
  [measurement_unit] nvarchar(255),
  [measurement_id] int PRIMARY KEY
);

CREATE TABLE [incident_measurement] (
  [measurement_id] int,
  [incident_id] int,
  PRIMARY KEY ([measurement_id], [incident_id])
);

CREATE TABLE [incident] (
  [priority] int,
  [responder_id] int,
  [incident_id] int PRIMARY KEY,
  [responder_comments] nvarchar(255),
  [resolved_date] datetime
);

CREATE TABLE [users] (
  [first_name] nvarchar(255),
  [last_name] nvarchar(255),
  [email] nvarchar(255),
  [role] nvarchar(255),
  [user_id] int PRIMARY KEY,
  [login_id] int
);

CREATE TABLE [login] (
  [login_id] int PRIMARY KEY,
  [username] nvarchar(255) NOT NULL,
  [password] nvarchar(255) NOT NULL
);

CREATE TABLE [physical_quantity] (
  [quantity_name] nvarchar(255),
  [quantity_id] int PRIMARY KEY,
  [sensor_id] int,
  [lower_warning_threshold] float,
  [upper_warning_threshold] float,
  [lower_emergency_threshold] float,
  [upper_emergency_threshold] float
);

CREATE TABLE [sensor] (
  [sensor_type] nvarchar(255),
  [sensor_id] int PRIMARY KEY,
  [status] nvarchar(255),
  [deployment_date] datetime,
  [latitude] float,
  [longitude] float,
  [height] float,
  [orientation] int,
  [measurement_frequency] nvarchar(255)
);

CREATE TABLE [maintenance] (
  [maintenance_id] int PRIMARY KEY,
  [maintenance_date] datetime,
  [maintainer_id] int,
  [maintainer_comments] nvarchar(255),
  [sensor_id] int
);

CREATE TABLE [configuration] (
  [setting_name] nvarchar(255),
  [minimum_value] float,
  [maximum_value] float,
  [current_value] float,
  [setting_id] int PRIMARY KEY,
  [sensor_id] int
);

-- Add a UNIQUE constraint to login_id in users
ALTER TABLE [users]
ADD CONSTRAINT unique_login_id UNIQUE ([login_id]);

-- Foreign key constraints
ALTER TABLE [incident_measurement] ADD CONSTRAINT [generates] FOREIGN KEY ([measurement_id]) REFERENCES [measurement] ([measurement_id]);
ALTER TABLE [incident_measurement] ADD CONSTRAINT [includes] FOREIGN KEY ([incident_id]) REFERENCES [incident] ([incident_id]);
ALTER TABLE [physical_quantity] ADD CONSTRAINT [measures] FOREIGN KEY ([sensor_id]) REFERENCES [sensor] ([sensor_id]);
ALTER TABLE [incident] ADD CONSTRAINT [deals_with] FOREIGN KEY ([responder_id]) REFERENCES [users] ([user_id]);
ALTER TABLE [login] ADD CONSTRAINT [logs_in] FOREIGN KEY ([login_id]) REFERENCES [users] ([login_id]);
ALTER TABLE [measurement] ADD CONSTRAINT [represented_by] FOREIGN KEY ([quantity_id]) REFERENCES [physical_quantity] ([quantity_id]);
ALTER TABLE [configuration] ADD CONSTRAINT [defined_by] FOREIGN KEY ([sensor_id]) REFERENCES [sensor] ([sensor_id]);
ALTER TABLE [maintenance] ADD CONSTRAINT [inspected_by] FOREIGN KEY ([sensor_id]) REFERENCES [sensor] ([sensor_id]);
ALTER TABLE [maintenance] ADD CONSTRAINT [records] FOREIGN KEY ([maintainer_id]) REFERENCES [users] ([user_id]);
