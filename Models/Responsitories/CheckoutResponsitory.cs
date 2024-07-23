
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project.Models;

public class CheckoutResponsitory : ICheckoutResponsitory
{
    private readonly DatabaseContext _context;
    public CheckoutResponsitory(DatabaseContext context)
    {
        _context = context;
    }
    public IEnumerable<Address> checkAddressAccount(int userID)
    {
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        return _context.Addresses.FromSqlRaw("EXEC sp_CheckAddressAccount @FK_iUserID", userIDParam);
    }

    public IEnumerable<AddressChoose> getAddressChoose()
    {
        return _context.AddressChooses.FromSqlRaw("EXEC sp_GetAddressChoose");
    }

    public IEnumerable<Address> getAddressesByID(int addressID, int userID)
    {
        SqlParameter addressIDParam = new SqlParameter("@PK_iAddressID", addressID);
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        return _context.Addresses.FromSqlRaw("EXEC sp_GetAddressAccountByID @PK_iAddressID, @FK_iUserID", addressIDParam, userIDParam);
    }

    public IEnumerable<City> getCities()
    {
        return _context.Cities.FromSqlRaw("EXEC sp_GetCities");
    }

    public IEnumerable<District> getDistricts()
    {
        return _context.Districts.FromSqlRaw("EXEC sp_GetDistricts");
    }

    public bool insertAddressAccount(int userID, string phone = "", string address = "")
    {
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        SqlParameter phoneParam = new SqlParameter("@sPhone", phone);
        SqlParameter addressParam = new SqlParameter("@sAddress", address);
        _context.Database.ExecuteSqlRaw("EXEC sp_InsertAddressAccount @FK_iUserID, @sPhone, @sAddress", userIDParam, phoneParam, addressParam);
        return true;
    }

    public bool updateAddressAccountByID(int addressID, int userID, string phone = "", string address = "")
    {
        SqlParameter addressIDParam = new SqlParameter("@PK_iAddressID", addressID);
        SqlParameter userIDParam = new SqlParameter("@FK_iUserID", userID);
        SqlParameter phoneParam = new SqlParameter("@sPhone", phone);
        SqlParameter addressParam = new SqlParameter("@sAddress", address);
        _context.Database.ExecuteSqlRaw("sp_UpdateAddressAccountByID @PK_iAddressID, @FK_iUserID, @sPhone, @sAddress", addressIDParam, userIDParam, phoneParam, addressParam);
        return true;
    }

    public bool updateAddressAccountUserByID(int userID, string fullname = "")
    {
        SqlParameter userIDParam = new SqlParameter("@PK_iUserID", userID);
        SqlParameter fullnameParam = new SqlParameter("@sFullName", fullname);
        _context.Database.ExecuteSqlRaw("sp_UpdateAddressAccountUserByID @PK_iUserID, @sFullName", userIDParam, fullnameParam);
        return true;
    }
}