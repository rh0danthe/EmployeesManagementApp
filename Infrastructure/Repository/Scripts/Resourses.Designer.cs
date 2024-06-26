﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Repository.Scripts {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resourses {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resourses() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Infrastructure.Repository.Scripts.Resourses", typeof(Resourses).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Passports&quot; WHERE &quot;Number&quot; = @Number;.
        /// </summary>
        internal static string CheckPassportIfExistByNumber {
            get {
                return ResourceManager.GetString("CheckPassportIfExistByNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO &quot;Companies&quot; (&quot;Name&quot;) VALUES(@Name) RETURNING *;.
        /// </summary>
        internal static string CreateCompany {
            get {
                return ResourceManager.GetString("CreateCompany", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO &quot;Departments&quot; (&quot;Name&quot;, &quot;Phone&quot;, &quot;CompanyId&quot;) VALUES(@Name, @Phone, @CompanyId) RETURNING *;.
        /// </summary>
        internal static string CreateDepartment {
            get {
                return ResourceManager.GetString("CreateDepartment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO &quot;Employees&quot; (&quot;Name&quot;, &quot;Surname&quot;, &quot;Phone&quot;, &quot;CompanyId&quot;,&quot;DepartmentId&quot;) VALUES(@Name, @Surname, @Phone,@CompanyId, @DepartmentId) RETURNING *;.
        /// </summary>
        internal static string CreateEmployee {
            get {
                return ResourceManager.GetString("CreateEmployee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на INSERT INTO &quot;Passports&quot; (&quot;Type&quot;, &quot;Number&quot;, &quot;EmployeeId&quot;) VALUES(@Type, @Number, @EmployeeId) RETURNING *;.
        /// </summary>
        internal static string CreatePassport {
            get {
                return ResourceManager.GetString("CreatePassport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на DELETE FROM &quot;Companies&quot; WHERE &quot;Id&quot; = @id;.
        /// </summary>
        internal static string DeleteCompany {
            get {
                return ResourceManager.GetString("DeleteCompany", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на DELETE FROM &quot;Departments&quot; WHERE &quot;Id&quot; = @id;.
        /// </summary>
        internal static string DeleteDepartment {
            get {
                return ResourceManager.GetString("DeleteDepartment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на DELETE FROM &quot;Employees&quot; WHERE &quot;Id&quot; = @id;.
        /// </summary>
        internal static string DeleteEmployee {
            get {
                return ResourceManager.GetString("DeleteEmployee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Companies&quot;;.
        /// </summary>
        internal static string GetAllCompanies {
            get {
                return ResourceManager.GetString("GetAllCompanies", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Departments&quot;;.
        /// </summary>
        internal static string GetAllDepartments {
            get {
                return ResourceManager.GetString("GetAllDepartments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Departments&quot; WHERE &quot;CompanyId&quot; = @CompanyId;.
        /// </summary>
        internal static string GetAllDepartmentsByCompanyId {
            get {
                return ResourceManager.GetString("GetAllDepartmentsByCompanyId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Employees&quot; WHERE &quot;CompanyId&quot; = @id;.
        /// </summary>
        internal static string GetAllEmployeesByCompanyId {
            get {
                return ResourceManager.GetString("GetAllEmployeesByCompanyId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Employees&quot; WHERE &quot;DepartmentId&quot; = @id;.
        /// </summary>
        internal static string GetAllEmployeesByDepartment {
            get {
                return ResourceManager.GetString("GetAllEmployeesByDepartment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Companies&quot; WHERE &quot;Id&quot; = @Id;.
        /// </summary>
        internal static string GetCompanyById {
            get {
                return ResourceManager.GetString("GetCompanyById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Departments&quot; WHERE &quot;Id&quot; = @Id.
        /// </summary>
        internal static string GetDepartmentById {
            get {
                return ResourceManager.GetString("GetDepartmentById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Departments&quot; WHERE LOWER(&quot;Name&quot;) = LOWER(@Name) AND &quot;CompanyId&quot; = @CompanyId;.
        /// </summary>
        internal static string GetDepartmentByName {
            get {
                return ResourceManager.GetString("GetDepartmentByName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Employees&quot; WHERE &quot;Id&quot; = @Id;.
        /// </summary>
        internal static string GetEmployeeById {
            get {
                return ResourceManager.GetString("GetEmployeeById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на SELECT * FROM &quot;Passports&quot; WHERE &quot;EmployeeId&quot; = @Id;.
        /// </summary>
        internal static string GetPassportByEmployeesId {
            get {
                return ResourceManager.GetString("GetPassportByEmployeesId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на UPDATE &quot;Companies&quot; SET &quot;Name&quot; = @Name WHERE &quot;Id&quot; = @id RETURNING *;.
        /// </summary>
        internal static string UpdateCompany {
            get {
                return ResourceManager.GetString("UpdateCompany", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на UPDATE &quot;Departments&quot; SET &quot;Name&quot; = @Name, &quot;Phone&quot; = @Phone, &quot;CompanyId&quot; = @CompanyId WHERE &quot;Id&quot; = @Id RETURNING *;.
        /// </summary>
        internal static string UpdateDepartment {
            get {
                return ResourceManager.GetString("UpdateDepartment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на UPDATE &quot;Employees&quot; SET &quot;Name&quot; = COALESCE(@Name, &quot;Name&quot;), &quot;Surname&quot; = COALESCE(@Surname, &quot;Surname&quot;), &quot;Phone&quot; = COALESCE(@Phone, &quot;Phone&quot;),
        ///&quot;CompanyId&quot; = COALESCE(@CompanyId, &quot;CompanyId&quot;), &quot;DepartmentId&quot; = COALESCE(@DepartmentId, &quot;DepartmentId&quot;)
        ///WHERE &quot;Id&quot; = @id RETURNING *;.
        /// </summary>
        internal static string UpdateEmployee {
            get {
                return ResourceManager.GetString("UpdateEmployee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на UPDATE &quot;Passports&quot; SET &quot;Type&quot; = COALESCE(@Type, &quot;Type&quot;), &quot;Number&quot; = COALESCE(@Number, &quot;Number&quot;) WHERE &quot;Id&quot; = @Id RETURNING *;.
        /// </summary>
        internal static string UpdatePassport {
            get {
                return ResourceManager.GetString("UpdatePassport", resourceCulture);
            }
        }
    }
}
