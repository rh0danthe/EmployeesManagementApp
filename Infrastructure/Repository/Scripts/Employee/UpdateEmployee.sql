UPDATE "Employees" SET "Name" = COALESCE(@Name, "Name"), "Surname" = COALESCE(@Surname, "Surname"), "Phone" = COALESCE(@Phone, "Phone"),
"CompanyId" = COALESCE(@CompanyId, "CompanyId"), "DepartmentId" = COALESCE(@DepartmentId, "DepartmentId")
WHERE "Id" = @id RETURNING *;