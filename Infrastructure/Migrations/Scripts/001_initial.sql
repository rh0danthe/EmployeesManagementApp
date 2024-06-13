CREATE TABLE IF NOT EXISTS "Companies"(
    "Id" serial PRIMARY KEY,
    "Name" text NOT NULL
);

CREATE TABLE IF NOT EXISTS "Departments"(
    "Id" serial PRIMARY KEY,
    "Name" text NOT NULL,
    "Phone" text NOT NULL,
    "CompanyId" int REFERENCES "Companies"("Id")
);

CREATE TABLE IF NOT EXISTS "Passports"(
    "Id" serial PRIMARY KEY,
    "Type" text NOT NULL,
    "Number" text NOT NULL
);

CREATE TABLE IF NOT EXISTS "Employees" (
   "Id" serial PRIMARY KEY,
   "Name" text NOT NULL,
   "Surname" text NOT NULL,
   "Phone" text NOT NULL,
   "CompanyId" int REFERENCES "Companies"("Id"),
   "DepartmentId" int REFERENCES "Departments"("Id"),
   "PassportId" int REFERENCES "Passports"("Id") ON DELETE CASCADE
);