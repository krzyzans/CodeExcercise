using System;

namespace CodeExcercise.Common.Models.DTO;

public class DatabaseCustomer
{
    public DatabaseCustomer(Guid ident, string firstname, string surname)
    {
        Ident = ident;
        Firstname = firstname;
        Surname = surname;
    }

    public Int64 Id { get; set; }
    public Guid Ident { get; set; }
    public string Firstname { get; set; }
    public string Surname { get; set; }
}